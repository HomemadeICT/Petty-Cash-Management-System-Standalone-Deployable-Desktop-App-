' ============================================================================
' Program.vb - Application Entry Point (SQLite Edition)
' Petty Cash Management System — CEB Haliela
' ============================================================================

Imports System.IO
Imports System.Windows.Forms

Public Module Program
    <STAThread>
    Public Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        Try
            ' 1. Check/Initialize SQLite Database
            InitializeDatabase()

            ' 2. Ensure permissions have correct keys (auto-repair for existing databases)
            EnsurePermissionsSeeded()

            ' 3. Enforce Admin Password Reset (Demo/Development feature)
            ' IMPORTANT: In production, change the default credentials immediately!
            ' Default demo login: admin / admin123
            ResetAdminPassword()

            ' 4. Launch Login Form
            Application.Run(New LoginForm())

        Catch ex As Exception
            ' Build full error chain for display
            Dim details As String = ""
            Dim current = ex
            Dim level = 0
            While current IsNot Nothing
                details &= If(level > 0, vbCrLf & "Caused by: ", "") & current.Message
                current = current.InnerException
                level += 1
            End While

            MessageBox.Show(
                "Critical Startup Error:" & vbCrLf & vbCrLf &
                details & vbCrLf & vbCrLf &
                "Check 'db_startup_error.log' in the app folder for diagnostics.",
                "Startup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Ensures the SQLite database exists and ALL tables are present.
    ''' Runs on EVERY startup — the schema uses CREATE TABLE IF NOT EXISTS
    ''' so it is 100% safe to run repeatedly (idempotent).
    '''
    ''' WHY: Previously this only ran when the .db file was missing. But on a
    ''' fresh install the DB file can be created (empty) before the schema
    ''' script executes. After a crash/power loss the file exists but tables
    ''' are absent, so every CRUD call fails with "no such table".
    ''' Running always (with IF NOT EXISTS) guarantees a fully-initialised DB.
    ''' </summary>
    Private Sub InitializeDatabase()
        ' Always ensure the data directory exists
        Dim baseDir = AppDomain.CurrentDomain.BaseDirectory

        ' Find the schema script
        Dim schemaPath As String = ""
        Dim found As Boolean = False

        ' 1. Try local SQL folder (Published/Installer location)
        Dim localPath = Path.Combine(baseDir, "SQL", "schema_sqlite.sql")
        If File.Exists(localPath) Then
            schemaPath = localPath
            found = True
        End If

        ' 2. If not found, search up parent directories (Development/Debug location)
        If Not found Then
            Dim tempDir As New DirectoryInfo(baseDir)
            For i As Integer = 1 To 5
                If tempDir.Parent Is Nothing Then Exit For
                tempDir = tempDir.Parent
                Dim checkPath = Path.Combine(tempDir.FullName, "SQL", "schema_sqlite.sql")
                If File.Exists(checkPath) Then
                    schemaPath = checkPath
                    found = True
                    Exit For
                End If
            Next
        End If

        If Not found Then
            Throw New FileNotFoundException(
                "Database schema file not found. Please ensure 'SQL\schema_sqlite.sql' exists." & vbCrLf &
                "Checked path: " & localPath)
        End If

        Try
            Dim script = File.ReadAllText(schemaPath)
            DbContext.ExecuteScript(script)
        Catch ex As Exception
            Throw New Exception("Failed to execute database schema script: " & ex.Message, ex)
        End Try

        ' Run migrations for existing databases
        MigrateReportMonthColumns()
    End Sub

    ''' <summary>
    ''' Adds report_month and report_year columns if they don't exist yet (migration for existing DBs).
    ''' Then backfills any NULL values using the entry_date, applying the Dec 15-31 → January rule.
    ''' Safe to run on every startup.
    ''' </summary>
    Private Sub MigrateReportMonthColumns()
        Try
            ' SQLite doesn't have IF NOT EXISTS for ALTER TABLE, so we check pragma first
            Dim hasColumn As Boolean = False
            Using conn = DbContext.GetConnection()
                Using cmd As New System.Data.SQLite.SQLiteCommand("PRAGMA table_info(petty_cash_entries)", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            If reader("name").ToString().Equals("report_month", StringComparison.OrdinalIgnoreCase) Then
                                hasColumn = True
                                Exit While
                            End If
                        End While
                    End Using
                End Using
            End Using

            ' Add columns if they don't exist
            If Not hasColumn Then
                DbContext.ExecuteNonQuery("ALTER TABLE petty_cash_entries ADD COLUMN report_month INTEGER")
                DbContext.ExecuteNonQuery("ALTER TABLE petty_cash_entries ADD COLUMN report_year INTEGER")
            End If

            ' Backfill any NULL report_month/report_year values from entry_date
            ' Rule: Dec 15-31 → January of next year; all other dates → their natural month
            DbContext.ExecuteNonQuery("
                UPDATE petty_cash_entries 
                SET report_month = CASE
                        WHEN CAST(strftime('%m', entry_date) AS INTEGER) = 12 
                             AND CAST(strftime('%d', entry_date) AS INTEGER) >= 15 
                        THEN 1
                        ELSE CAST(strftime('%m', entry_date) AS INTEGER)
                    END,
                    report_year = CASE
                        WHEN CAST(strftime('%m', entry_date) AS INTEGER) = 12 
                             AND CAST(strftime('%d', entry_date) AS INTEGER) >= 15 
                        THEN CAST(strftime('%Y', entry_date) AS INTEGER) + 1
                        ELSE CAST(strftime('%Y', entry_date) AS INTEGER)
                    END
                WHERE report_month IS NULL OR report_year IS NULL")

        Catch ex As Exception
            ' Log but don't block startup
            Console.WriteLine("Warning: Could not migrate report month columns. " & ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Ensures all permission keys are present in the database.
    ''' This auto-repairs existing databases that were created before the permission key fix.
    ''' Safe to run on every startup (uses INSERT OR IGNORE).
    ''' </summary>
    Private Sub EnsurePermissionsSeeded()
        Try
            Dim sql = "
                INSERT OR IGNORE INTO permissions (permission_key, description) VALUES 
                ('EXPENSE_ADD',             'Can add new petty cash entries'),
                ('EXPENSE_EDIT',            'Can edit existing entries'),
                ('EXPENSE_DELETE',          'Can remove entries'),
                ('EXPENSE_VIEW',            'Can view expense entries'),
                ('REPORT_GENERATE',         'Can generate and view reports'),
                ('REPORT_PRINT',            'Can print reports'),
                ('REPORT_FINALIZE',         'Can lock monthly reports'),
                ('REPORT_EXPORT_EXCEL',     'Can export reports to Excel'),
                ('DASHBOARD_VIEW',          'Can access the dashboard'),
                ('DASHBOARD_NAVIGATE',      'Can navigate between months'),
                ('USER_CREATE',             'Can create new users'),
                ('USER_EDIT',               'Can edit user details'),
                ('USER_DEACTIVATE',         'Can deactivate users'),
                ('USER_RESET_PASSWORD',     'Can reset user passwords'),
                ('USER_MANAGE_PERMISSIONS', 'Can manage user permissions'),
                ('SETTINGS_VIEW',           'Can view admin settings'),
                ('SETTINGS_EDIT',           'Can change system settings'),
                ('AUDIT_VIEW',              'Can view the audit log'),
                ('CATEGORY_MANAGE',         'Can manage categories'),
                ('ITEM_MANAGE',             'Can manage items library'),
                ('SELF_CHANGE_PASSWORD',    'Can change own password'),
                ('BACKUP_DATABASE',         'Can backup and restore the database')"
            
            DbContext.ExecuteNonQuery(sql)

            ' Ensure Admin role gets all permissions (in case new ones were added)
            DbContext.ExecuteNonQuery("
                INSERT OR IGNORE INTO role_default_permissions (role_name, permission_id, is_granted)
                SELECT 'Admin', permission_id, 1 FROM permissions")

        Catch ex As Exception
            ' Log but don't block startup
            Console.WriteLine("Warning: Could not seed permissions. " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Enforces the admin password to 'admin123' on every startup.
    ''' This prevents lockouts during the migration/testing phase.
    ''' </summary>
    Private Sub ResetAdminPassword()
        Try
            Dim userRepo As New UserRepository()
            Dim adminUser = userRepo.GetByUsername("admin")
            
            If adminUser IsNot Nothing Then
                ' Hash of "admin123"
                Dim defaultHash = BCrypt.Net.BCrypt.HashPassword("admin123")
                userRepo.UpdatePassword(adminUser.UserId, defaultHash)
            End If
        Catch ex As Exception
            ' Log error but don't block startup
            Console.WriteLine("Warning: Could not reset admin password. " & ex.Message)
        End Try
    End Sub
End Module
