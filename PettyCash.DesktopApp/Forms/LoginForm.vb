' ============================================================================
' LoginForm.vb - Login Form Code-Behind
' Petty Cash Management System
' ============================================================================
' Purpose: Handles user authentication
' ============================================================================

Imports System.Windows.Forms
Imports BCrypt.Net

Public Class LoginForm

#Region "Private Fields"

    Private _userRepository As UserRepository
    Private _auditLogRepository As AuditLogRepository
#End Region

#Region "Constructor"
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub
#End Region

#Region "Form Events"

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormIconHelper.ApplyIcon(Me, FormIconHelper.FormType.Login)
        Try
            ' Initialize repositories
            _userRepository = New UserRepository()
            _auditLogRepository = New AuditLogRepository()
            Dim auditService As New AuditService(_auditLogRepository)

            ' Initialize auth service


            ' Set version label
            lblVersion.Text = $"Version {Constants.APP_VERSION}"

            ' Focus on username
            If txtUsername IsNot Nothing Then txtUsername.Focus()

            ' Center the login panel
            CenterLoginPanel()
        Catch ex As Exception
            MessageBox.Show($"Form Load Error: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoginForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        CenterLoginPanel()
    End Sub

#End Region

#Region "Button Events"

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' Clear previous status
        lblStatus.Text = ""

        ' Validate inputs
        If String.IsNullOrWhiteSpace(txtUsername.Text) Then
            ShowError("Please enter your username.")
            txtUsername.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtPassword.Text) Then
            ShowError("Please enter your password.")
            txtPassword.Focus()
            Return
        End If

        ' Disable button during login
        btnLogin.Enabled = False
        btnLogin.Text = "Logging in..."
        Application.DoEvents()

        Try
            ' Check if service is initialized
            ' Check if repository is initialized
            If _userRepository Is Nothing Then
                Throw New Exception("UserRepository is not initialized. Form load may have failed.")
            End If

            ' Authenticate User
            Dim username As String = txtUsername.Text.Trim()
            Dim password As String = txtPassword.Text
            Dim user As User = _userRepository.GetByUsername(username)

            If user IsNot Nothing AndAlso user.IsActive AndAlso BCrypt.Net.BCrypt.Verify(password, user.PasswordHash) Then
                 ' Audit Success
                 ' Direct usage of auditService if available or skip
                 
                 ' Initialize PermissionService and start session
                 Dim permissionRepo As New PermissionRepository()
                 Dim auditSvc As New AuditService(_auditLogRepository)
                 Dim permissionService As New PermissionService(permissionRepo, auditSvc)
                 
                 Try
                     SessionManager.StartSession(user, System.Guid.NewGuid().ToString(), permissionService)
                 Catch ex As System.Data.SQLite.SQLiteException
            ' Handle missing tables gracefully on first run if initialization failed
            If ex.Message.ToLower().Contains("no such table") Then
                MessageBox.Show("Database is being initialized. Please try logging in again in a moment.", "System Initializing", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                Throw ' Re-throw if it's a different SQL error
            End If
                 End Try

                 ' Open Dashboard
                 Dim dashboardForm As New DashboardForm()
                 Me.Hide()
                 dashboardForm.ShowDialog()
                 Me.Close()
            Else
                 ShowError("Invalid username or password.")
                 txtPassword.Clear()
                 txtPassword.Focus()
            End If

        Catch ex As Exception
            ShowError($"Login failed: {ex.Message}")
            ' Log to debug output for development troubleshooting
            System.Diagnostics.Debug.WriteLine($"Login error: {ex.ToString()}")

        Finally
            btnLogin.Enabled = True
            btnLogin.Text = "Login"
        End Try
    End Sub

#End Region

#Region "TextBox Events"

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            btnLogin_Click(sender, e)
        End If
    End Sub

    Private Sub txtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True
            txtPassword.Focus()
        End If
    End Sub

#End Region

#Region "Private Methods"

    Private Sub CenterLoginPanel()
        pnlMain.Left = (Me.ClientSize.Width - pnlMain.Width) \ 2
        pnlMain.Top = (Me.ClientSize.Height - pnlMain.Height) \ 2
    End Sub

    Private Sub ShowError(message As String)
        lblStatus.Text = message
        lblStatus.ForeColor = Color.Red
    End Sub

#End Region

End Class