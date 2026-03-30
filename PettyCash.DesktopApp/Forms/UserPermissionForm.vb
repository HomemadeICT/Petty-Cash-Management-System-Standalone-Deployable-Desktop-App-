' ============================================================================
' UserPermissionForm.vb - User Permission Management Form
' Petty Cash Management System
' ============================================================================
' Purpose: Admin panel for managing per-user permission overrides
' ============================================================================

Imports System.Windows.Forms

Public Class UserPermissionForm

#Region "Private Fields"
    Private _user As User
    Private _permissionService As PermissionService
#End Region

#Region "Constructor"
    Public Sub New(user As User, permissionService As PermissionService)
        InitializeComponent()
        _user = user
        _permissionService = permissionService
        Me.Text = $"Permissions: {user.Username} ({user.Role})"
        lblTitle.Text = $"Permissions for: {user.FullName}"
        lblRole.Text = $"Role: {user.Role}"
    End Sub
#End Region

#Region "Form Events"

    Private Sub UserPermissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormIconHelper.ApplyIcon(Me, FormIconHelper.FormType.Permissions)
        LoadPermissions()
    End Sub

#End Region

#Region "Data Loading"

    Private Sub LoadPermissions()
        ' Get all permissions and user's effective permissions
        Dim allPermissions = _permissionService.GetAllPermissions()
        Dim effectivePermissions = _permissionService.GetUserPermissions(_user.UserId)

        ' Clear the checklist
        clbPermissions.Items.Clear()

        ' Populate with all permissions
        For Each perm In allPermissions
            Dim displayText = $"{perm.Category} - {perm.DisplayName}"
            Dim isGranted = effectivePermissions.Contains(perm.PermissionKey)
            clbPermissions.Items.Add(displayText, isGranted)
        Next

        ' Store permission keys for reference (use Tag on a hidden list)
        clbPermissions.Tag = allPermissions.Select(Function(p) p.PermissionKey).ToList()
    End Sub

#End Region

#Region "Button Events"

    Private Sub btnGrantSelected_Click(sender As Object, e As EventArgs) Handles btnGrantSelected.Click
        ApplyPermissionChanges()
    End Sub

    Private Sub btnResetDefaults_Click(sender As Object, e As EventArgs) Handles btnResetDefaults.Click
        If MessageBox.Show($"Reset all permissions for '{_user.Username}' to role defaults ({_user.Role})?",
                          "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            Dim result = _permissionService.ResetToDefaults(_user.UserId, SessionManager.CurrentUserId)
            If result.IsSuccess Then
                MessageBox.Show("Permissions reset to role defaults.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadPermissions()
            Else
                MessageBox.Show("Failed to reset permissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "Private Methods"

    Private Sub ApplyPermissionChanges()
        Dim permissionKeys = TryCast(clbPermissions.Tag, List(Of String))
        If permissionKeys Is Nothing Then Return

        Dim successCount = 0
        Dim failCount = 0

        For i = 0 To clbPermissions.Items.Count - 1
            Dim isChecked = clbPermissions.GetItemChecked(i)
            Dim permKey = permissionKeys(i)

            Dim result As OperationResult
            If isChecked Then
                result = _permissionService.GrantPermission(_user.UserId, permKey, SessionManager.CurrentUserId)
            Else
                result = _permissionService.RevokePermission(_user.UserId, permKey, SessionManager.CurrentUserId)
            End If

            If result.IsSuccess Then
                successCount += 1
            Else
                failCount += 1
            End If
        Next

        Dim message = $"Permissions updated: {successCount} applied"
        If failCount > 0 Then message &= $", {failCount} failed"

        MessageBox.Show(message, "Permission Update", MessageBoxButtons.OK,
                       If(failCount > 0, MessageBoxIcon.Warning, MessageBoxIcon.Information))

        ' Refresh the cache for this user
        _permissionService.ClearCache(_user.UserId)

        ' If editing own permissions, refresh session
        If _user.UserId = SessionManager.CurrentUserId Then
            SessionManager.RefreshPermissions()
        End If
    End Sub

#End Region

End Class
