' ============================================================================
' UserEditForm.vb - User Add/Edit Form
' Petty Cash Management System
' ============================================================================
' Purpose: Form for adding or editing user details
' ============================================================================

Imports System.Windows.Forms

Public Class UserEditForm

#Region "Private Fields"
    Private _user As User
    Private _isNewUser As Boolean
    Private _userManagementService As UserManagementService
#End Region

#Region "Constructor"
    Public Sub New(user As User, userManagementService As UserManagementService)
        InitializeComponent()
        _userManagementService = userManagementService

        If user Is Nothing Then
            _isNewUser = True
            _user = New User()
            Me.Text = "Add New User"
            lblFormTitle.Text = "Add New User"
        Else
            _isNewUser = False
            _user = user
            Me.Text = $"Edit User: {user.Username}"
            lblFormTitle.Text = $"Edit User: {user.Username}"
        End If
    End Sub
#End Region

#Region "Form Events"

    Private Sub UserEditForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormIconHelper.ApplyIcon(Me, FormIconHelper.FormType.UserEdit)
        ' Setup role combobox
        cmbRole.Items.Clear()
        cmbRole.Items.Add("Admin")
        cmbRole.Items.Add("Clerk")
        cmbRole.Items.Add("Viewer")

        If _isNewUser Then
            cmbRole.SelectedIndex = 1 ' Default to Clerk
        Else
            LoadUserData()
        End If

        ' Show/hide password fields
        lblPassword.Visible = _isNewUser
        txtPassword.Visible = _isNewUser
        lblConfirm.Visible = _isNewUser
        txtConfirm.Visible = _isNewUser
    End Sub

#End Region

#Region "Data Loading"

    Private Sub LoadUserData()
        txtUsername.Text = _user.Username
        txtUsername.ReadOnly = True ' Cannot change username
        txtFullName.Text = _user.FullName
        cmbRole.SelectedItem = _user.Role
        txtEmail.Text = _user.Email
        txtWhatsApp.Text = _user.WhatsAppNo
    End Sub

#End Region

#Region "Button Events"

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validate
        If String.IsNullOrWhiteSpace(txtUsername.Text) Then
            MessageBox.Show("Username is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUsername.Focus()
            Return
        End If

        If String.IsNullOrWhiteSpace(txtFullName.Text) Then
            MessageBox.Show("Full name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtFullName.Focus()
            Return
        End If

        If cmbRole.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a role.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbRole.Focus()
            Return
        End If

        If _isNewUser Then
            If String.IsNullOrWhiteSpace(txtPassword.Text) Then
                MessageBox.Show("Password is required for new users.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPassword.Focus()
                Return
            End If

            If txtPassword.Text <> txtConfirm.Text Then
                MessageBox.Show("Passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtConfirm.Focus()
                Return
            End If
        End If

        ' Update user object
        _user.Username = txtUsername.Text.Trim()
        _user.FullName = txtFullName.Text.Trim()
        _user.Role = cmbRole.SelectedItem.ToString()
        _user.Email = txtEmail.Text.Trim()
        _user.WhatsAppNo = txtWhatsApp.Text.Trim()

        ' Save
        Dim result As OperationResult
        If _isNewUser Then
            result = _userManagementService.RegisterUser(_user, txtPassword.Text, SessionManager.CurrentUserId)
        Else
            result = _userManagementService.UpdateUser(_user, SessionManager.CurrentUserId)
        End If

        If result.IsSuccess Then
            MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

End Class
