' ============================================================================
' UserManagementForm.vb - User Management Form
' Petty Cash Management System
' ============================================================================
' Purpose: Admin panel for managing users
' ============================================================================

Imports System.Windows.Forms

Public Class UserManagementForm

#Region "Private Fields"
    Private _userManagementService As UserManagementService
    Private _permissionService As PermissionService
#End Region

#Region "Form Events"

    Private Sub UserManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize services
        Dim userRepo As New UserRepository()
        Dim permissionRepo As New PermissionRepository()
        Dim auditRepo As New AuditLogRepository()
        Dim auditService As New AuditService(auditRepo)

        _permissionService = New PermissionService(permissionRepo, auditService)
        _userManagementService = New UserManagementService(userRepo, _permissionService, auditService)

        ' Load users
        LoadUsers()
    End Sub

#End Region

#Region "Data Loading"

    Private Sub LoadUsers()
        Dim users = _userManagementService.GetAllUsers()

        dgvUsers.DataSource = Nothing
        dgvUsers.DataSource = users.Select(Function(u) New With {
            u.UserId,
            u.Username,
            .FullName = u.FullName,
            u.Role,
            .Email = If(String.IsNullOrEmpty(u.Email), "-", u.Email),
            .Status = If(u.IsActive, "Active", "Inactive")
        }).ToList()

        ' Hide ID column
        If dgvUsers.Columns.Contains("UserId") Then
            dgvUsers.Columns("UserId").Visible = False
        End If

        ' Format grid
        dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

#End Region

#Region "Button Events"

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Using frm As New UserEditForm(Nothing, _userManagementService)
            If frm.ShowDialog() = DialogResult.OK Then
                LoadUsers()
            End If
        End Using
    End Sub

    Private Sub btnEditUser_Click(sender As Object, e As EventArgs) Handles btnEditUser.Click
        Dim user = GetSelectedUser()
        If user Is Nothing Then Return

        Using frm As New UserEditForm(user, _userManagementService)
            If frm.ShowDialog() = DialogResult.OK Then
                LoadUsers()
            End If
        End Using
    End Sub

    Private Sub btnDeactivate_Click(sender As Object, e As EventArgs) Handles btnDeactivate.Click
        Dim user = GetSelectedUser()
        If user Is Nothing Then Return

        Dim action = If(user.IsActive, "deactivate", "reactivate")
        If MessageBox.Show($"Are you sure you want to {action} user '{user.Username}'?", "Confirm",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            Dim result As OperationResult
            If user.IsActive Then
                result = _userManagementService.DeactivateUser(user.UserId, SessionManager.CurrentUserId)
            Else
                result = _userManagementService.ReactivateUser(user.UserId, SessionManager.CurrentUserId)
            End If

            If result.IsSuccess Then
                MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadUsers()
            Else
                MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnResetPassword_Click(sender As Object, e As EventArgs) Handles btnResetPassword.Click
        Dim user = GetSelectedUser()
        If user Is Nothing Then Return

        Dim newPassword = InputBox($"Enter new password for '{user.Username}':", "Reset Password")
        If String.IsNullOrWhiteSpace(newPassword) Then Return

        Dim result = _userManagementService.ResetPassword(user.UserId, newPassword, SessionManager.CurrentUserId)
        If result.IsSuccess Then
            MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnManagePermissions_Click(sender As Object, e As EventArgs) Handles btnManagePermissions.Click
        Dim user = GetSelectedUser()
        If user Is Nothing Then Return

        Using frm As New UserPermissionForm(user, _permissionService)
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvUsers_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvUsers.CellDoubleClick
        If e.RowIndex >= 0 Then
            btnEditUser_Click(sender, e)
        End If
    End Sub

#End Region

#Region "Private Methods"

    Private Function GetSelectedUser() As User
        If dgvUsers.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a user.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        End If

        Dim userId = CInt(dgvUsers.SelectedRows(0).Cells("UserId").Value)
        Return _userManagementService.GetUserById(userId)
    End Function

#End Region

End Class
