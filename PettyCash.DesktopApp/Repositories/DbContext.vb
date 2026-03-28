' ============================================================================
' DbContext.vb - Database Connection Manager (SQLite Edition)
' Petty Cash Management System
' ============================================================================
' Purpose: Manages the lifetime of SQLite connections and common DB tasks
' Layer: Data Access Layer
' Dependencies: System.Data.SQLite
' ============================================================================

Imports System.Data
Imports System.Data.SQLite
Imports System.Configuration
Imports System.IO

''' <summary>
''' Centralized database context for SQLite connection management.
''' </summary>
Public Class DbContext

    Public Shared ReadOnly Property ConnectionString As String
        Get
            Return _connectionString
        End Get
    End Property

    Private Shared ReadOnly _connectionString As String
    Private Shared _lastError As String = String.Empty
    Private Shared ReadOnly _dataDirectory As String

    ''' <summary>
    ''' Default database filename.
    ''' </summary>
    Private Const DB_FILENAME As String = "PettyCash.db"

    ''' <summary>
    ''' Default subfolder name under ProgramData.
    ''' </summary>
    Private Const APP_DATA_FOLDER As String = "PettyCashManagement"

    ''' <summary>
    ''' Gets the directory where the database file is stored.
    ''' </summary>
    Public Shared ReadOnly Property DataDirectory As String
        Get
            Return _dataDirectory
        End Get
    End Property

    ''' <summary>
    ''' Gets the full absolute path to the database file.
    ''' </summary>
    Public Shared ReadOnly Property DatabaseFilePath As String
        Get
            Try
                Dim builder As New SQLiteConnectionStringBuilder(_connectionString)
                Return builder.DataSource
            Catch
                Return Path.Combine(_dataDirectory, DB_FILENAME)
            End Try
        End Get
    End Property

    ''' <summary>
    ''' Static constructor — FOOLPROOF initialisation.
    '''
    ''' 1. Determines a writable data directory (C:\ProgramData\PettyCashManagement).
    ''' 2. Tries to read the connection string from App.config / .exe.config.
    '''    Step 2a: via ConfigurationManager (works in Debug / framework-dependent)
    '''    Step 2b: via ExeConfigurationFileMap scanning for *.exe.config or
    '''             *.dll.config next to the EXE (handles SDK-style .NET 8
    '''             self-contained publish where ConfigurationManager sees nothing)
    ''' 3. Falls back to a hardcoded default so the app ALWAYS starts.
    ''' 4. Resolves the Data Source to an absolute path inside the data directory.
    ''' 5. Writes a diagnostic log if anything goes wrong.
    ''' </summary>
    Shared Sub New()
        Try
            ' ── Step 1: Determine writable data directory ──────────────────
            _dataDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                APP_DATA_FOLDER)

            If Not Directory.Exists(_dataDirectory) Then
                Directory.CreateDirectory(_dataDirectory)
            End If

            ' ── Step 2a: Standard ConfigurationManager read ────────────────
            Dim raw As String = Nothing
            Try
                Dim cs = ConfigurationManager.ConnectionStrings("PettyCashDB")
                If cs IsNot Nothing Then
                    raw = cs.ConnectionString
                End If
            Catch
                ' ConfigurationManager unavailable — proceed to Step 2b
            End Try

            ' ── Step 2b: Manual config file scan (SDK publish fallback) ────
            ' In .NET 8 self-contained publish the config is emitted as
            '   {AssemblyName}.dll.config
            ' The installer copies this to .exe.config. Try all known names.
            If String.IsNullOrWhiteSpace(raw) Then
                Dim appDir As String = AppDomain.CurrentDomain.BaseDirectory
                Dim configCandidates As String() = {
                    Path.Combine(appDir, "Petty Cash Management System For CEB Haliela.exe.config"),
                    Path.Combine(appDir, "Petty Cash Management System For CEB Haliela.dll.config"),
                    Path.Combine(appDir, AppDomain.CurrentDomain.FriendlyName & ".config"),
                    Path.Combine(appDir, AppDomain.CurrentDomain.FriendlyName & ".exe.config")
                }

                For Each candidate As String In configCandidates
                    If Not File.Exists(candidate) Then Continue For
                    Try
                        Dim fileMap As New ExeConfigurationFileMap() With {
                            .ExeConfigFilename = candidate
                        }
                        Dim cfg As Configuration = ConfigurationManager.OpenMappedExeConfiguration(
                                                        fileMap, ConfigurationUserLevel.None)
                        Dim cs2 = cfg.ConnectionStrings.ConnectionStrings("PettyCashDB")
                        If cs2 IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cs2.ConnectionString) Then
                            raw = cs2.ConnectionString
                            Exit For
                        End If
                    Catch
                        ' This candidate failed — try the next one
                    End Try
                Next
            End If

            ' ── Step 3: Hardcoded fallback ─────────────────────────────────
            If String.IsNullOrWhiteSpace(raw) Then
                raw = "Data Source=" & DB_FILENAME & ";Version=3;"
            End If

            ' ── Step 4: Resolve Data Source to absolute path ───────────────
            Dim builder As New SQLiteConnectionStringBuilder(raw)
            Dim dbFileName As String = Path.GetFileName(builder.DataSource)
            If String.IsNullOrWhiteSpace(dbFileName) Then dbFileName = DB_FILENAME

            builder.DataSource = Path.Combine(_dataDirectory, dbFileName)
            _connectionString = builder.ToString()

        Catch ex As Exception
            ' ── Last resort: hardcode everything ──────────────────────────
            If String.IsNullOrEmpty(_dataDirectory) Then
                _dataDirectory = AppDomain.CurrentDomain.BaseDirectory
            End If
            _connectionString = "Data Source=" & Path.Combine(_dataDirectory, DB_FILENAME) & ";Version=3;"

            Try
                Dim logPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db_startup_error.log")
                File.WriteAllText(logPath,
                    "DbContext init error at " & DateTime.Now.ToString() & vbCrLf &
                    "Exception: " & ex.ToString() & vbCrLf &
                    "Data Directory: " & _dataDirectory & vbCrLf &
                    "Connection String: " & _connectionString)
            Catch
                ' Cannot write log — silently continue
            End Try
        End Try
    End Sub

    ''' <summary>Gets the last database error message.</summary>
    Public Shared ReadOnly Property LastError As String
        Get
            Return _lastError
        End Get
    End Property

    ''' <summary>
    ''' Returns a NEW open SQLite connection. Caller MUST dispose it.
    ''' </summary>
    Public Shared Function GetConnection() As SQLiteConnection
        Dim conn As New SQLiteConnection(_connectionString)
        Try
            conn.Open()
            Return conn
        Catch ex As Exception
            _lastError = ex.Message
            Throw New Exception(
                "Failed to open SQLite connection." & vbCrLf &
                "Data Source: " & DatabaseFilePath & vbCrLf &
                "Connection String: " & _connectionString, ex)
        End Try
    End Function

    ''' <summary>Checks if the SQLite database file exists.</summary>
    Public Shared Function DatabaseExists() As Boolean
        Try
            Return File.Exists(DatabaseFilePath)
        Catch ex As Exception
            _lastError = ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Executes a raw SQL script that may contain multiple semicolon-delimited
    ''' statements (e.g. the schema creation script).
    '''
    ''' FIX: Previously the entire script was passed as a single SQLiteCommand,
    ''' which is unreliable for multi-statement scripts — tables could be silently
    ''' skipped, leaving the database half-initialised and causing all CRUD to
    ''' fail with "no such table" on a fresh install.
    '''
    ''' Now each statement is split out and executed individually inside one
    ''' transaction, so either the whole schema is created or nothing is.
    ''' </summary>
    Public Shared Sub ExecuteScript(scriptContent As String)
        If String.IsNullOrWhiteSpace(scriptContent) Then Return

        ' Split on semicolons to get individual statements
        Dim rawStatements As String() = scriptContent.Split({";"c}, StringSplitOptions.RemoveEmptyEntries)

        Using conn As SQLiteConnection = GetConnection()
            Using txn As SQLiteTransaction = conn.BeginTransaction()
                Try
                    For Each rawStmt As String In rawStatements
                        ' Remove SQL line comments and trim whitespace
                        Dim lines As String() = rawStmt.Split(
                            New String() {vbCr & vbLf, vbCr, vbLf},
                            StringSplitOptions.RemoveEmptyEntries)

                        Dim cleanedLines As New List(Of String)
                        For Each ln As String In lines
                            If Not ln.TrimStart().StartsWith("--") Then
                                cleanedLines.Add(ln)
                            End If
                        Next

                        Dim stmt As String = String.Join(vbCrLf, cleanedLines).Trim()
                        If String.IsNullOrWhiteSpace(stmt) Then Continue For

                        Using cmd As New SQLiteCommand(stmt, conn, txn)
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                    txn.Commit()

                Catch ex As Exception
                    txn.Rollback()
                    Throw New Exception("Schema script execution failed: " & ex.Message, ex)
                End Try
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' Executes a single SQL statement (INSERT, UPDATE, DELETE).
    ''' </summary>
    Public Shared Sub ExecuteNonQuery(sql As String)
        If String.IsNullOrWhiteSpace(sql) Then Return

        Using conn As SQLiteConnection = GetConnection()
            Using cmd As New SQLiteCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class
