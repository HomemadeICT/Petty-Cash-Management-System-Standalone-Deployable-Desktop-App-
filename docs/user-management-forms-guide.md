# User Management Forms — Implementation Guide

This document provides implementation guidance for the three admin forms needed for user management.

---

## Forms Overview

| Form | Purpose | Key Features |
|------|---------|--------------|
| **UserManagementForm** | Main admin panel for user management | User list, Add/Edit/Delete/Permissions buttons |
| **UserEditForm** | Add or edit user details | Username, Full Name, Role, Email, WhatsApp, Password (new only) |
| **UserPermissionForm** | Manage per-user permissions | Checkboxes grouped by category, Reset to Defaults |

---

## 1. UserManagementForm

### Layout

```
┌─────────────────────────────────────────────────────────┐
│ User Management                                    [X]  │
├─────────────────────────────────────────────────────────┤
│                                                         │
│ ┌─────────────────────────────────────────────────────┐│
│ │ DataGridView: Users List                           ││
│ │ Columns: Username | Full Name | Role | Status      ││
│ │                                                     ││
│ └─────────────────────────────────────────────────────┘│
│                                                         │
│ [Add User] [Edit User] [Deactivate] [Reset Password]   │
│ [Manage Permissions]                          [Close]  │
└─────────────────────────────────────────────────────────┘
```

### Key Code Points

**Form Load:**
```vb
Private Sub UserManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    _userManagementService = New UserManagementService(...)
    LoadUsers()
End Sub

Private Sub LoadUsers()
    Dim users = _userManagementService.GetAllUsers()
    dgvUsers.DataSource = users.Select(Function(u) New With {
        u.UserId,
        u.Username,
        u.FullName,
        u.Role,
        .Status = If(u.IsActive, "Active", "Inactive")
    }).ToList()
End Sub
```

**Add User Button:**
```vb
Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
    Using frm As New UserEditForm(Nothing)  ' Nothing = new user
        If frm.ShowDialog() = DialogResult.OK Then
            LoadUsers()  ' Refresh list
        End If
    End Using
End Sub
```

**Edit User Button:**
```vb
Private Sub btnEditUser_Click(sender As Object, e As EventArgs) Handles btnEditUser.Click
    If dgvUsers.SelectedRows.Count = 0 Then Return
    
    Dim userId = CInt(dgvUsers.SelectedRows(0).Cells("UserId").Value)
    Dim user = _userManagementService.GetUserById(userId)
    
    Using frm As New UserEditForm(user)  ' Pass existing user
        If frm.ShowDialog() = DialogResult.OK Then
            LoadUsers()
        End If
    End Using
End Sub
```

**Manage Permissions Button:**
```vb
Private Sub btnManagePermissions_Click(sender As Object, e As EventArgs) Handles btnManagePermissions.Click
    If dgvUsers.SelectedRows.Count = 0 Then Return
    
    Dim userId = CInt(dgvUsers.SelectedRows(0).Cells("UserId").Value)
    Dim user = _userManagementService.GetUserById(userId)
    
    Using frm As New UserPermissionForm(user)
        frm.ShowDialog()
    End Using
End Sub
```

---

## 2. UserEditForm

### Layout

```
┌─────────────────────────────────────────────────────────┐
│ Add/Edit User                                      [X]  │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  Username:        [________________]                    │
│  Full Name:       [________________]                    │
│  Role:            [Admin ▼]                             │
│  Email:           [________________]                    │
│  WhatsApp:        [________________]                    │
│                                                         │
│  Password:        [________________]  (New users only)  │
│  Confirm:         [________________]                    │
│                                                         │
│                              [Save]  [Cancel]           │
└─────────────────────────────────────────────────────────┘
```

### Key Code Points

**Constructor:**
```vb
Private _user As User
Private _isNewUser As Boolean

Public Sub New(user As User)
    InitializeComponent()
    
    If user Is Nothing Then
        _isNewUser = True
        _user = New User()
        Me.Text = "Add New User"
    Else
        _isNewUser = False
        _user = user
        Me.Text = "Edit User"
        LoadUserData()
    End If
    
    ' Hide password fields for existing users
    lblPassword.Visible = _isNewUser
    txtPassword.Visible = _isNewUser
    lblConfirm.Visible = _isNewUser
    txtConfirm.Visible = _isNewUser
End Sub
```

**Load User Data:**
```vb
Private Sub LoadUserData()
    txtUsername.Text = _user.Username
    txtUsername.ReadOnly = True  ' Can't change username
    txtFullName.Text = _user.FullName
    cmbRole.SelectedItem = _user.Role
    txtEmail.Text = _user.Email
    txtWhatsApp.Text = _user.WhatsAppNo
End Sub
```

**Save Button:**
```vb
Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    ' Validate
    If String.IsNullOrWhiteSpace(txtUsername.Text) Then
        MessageBox.Show("Username is required.")
        Return
    End If
    
    If _isNewUser AndAlso txtPassword.Text <> txtConfirm.Text Then
        MessageBox.Show("Passwords do not match.")
        Return
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
        MessageBox.Show(result.Message, "Success")
        Me.DialogResult = DialogResult.OK
        Me.Close()
    Else
        MessageBox.Show(result.ErrorMessage, "Error")
    End If
End Sub
```

---

## 3. UserPermissionForm

### Layout

```
┌─────────────────────────────────────────────────────────┐
│ Manage Permissions: John Doe (Clerk)              [X]  │
├─────────────────────────────────────────────────────────┤
│                                                         │
│ ┌─────────────────────────────────────────────────────┐│
│ │ DataGridView with Checkboxes                       ││
│ │ Category | Permission | Granted | Source           ││
│ │ ──────────────────────────────────────────────────  ││
│ │ Expense  │ Add Expense    │ ☑ │ Role Default       ││
│ │ Expense  │ Edit Expense   │ ☑ │ Role Default       ││
│ │ Expense  │ Delete Expense │ ☑ │ User Override      ││
│ │ ...                                                 ││
│ └─────────────────────────────────────────────────────┘│
│                                                         │
│ [Reset to Role Defaults]              [Save]  [Cancel] │
└─────────────────────────────────────────────────────────┘
```

### Key Code Points

**Constructor:**
```vb
Private _user As User
Private _permissionService As PermissionService
Private _effectivePermissions As List(Of EffectivePermission)

Public Sub New(user As User)
    InitializeComponent()
    _user = user
    Me.Text = $"Manage Permissions: {user.FullName} ({user.Role})"
End Sub
```

**Form Load:**
```vb
Private Sub UserPermissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    _permissionService = New PermissionService(...)
    LoadPermissions()
End Sub

Private Sub LoadPermissions()
    _effectivePermissions = _permissionService.GetUserPermissionsDetailed(_user.UserId)
    
    dgvPermissions.DataSource = _effectivePermissions.Select(Function(p) New With {
        p.Category,
        .Permission = p.DisplayName,
        .Granted = p.HasPermission,
        .Source = p.PermissionSource,
        p.PermissionKey
    }).ToList()
    
    ' Make Granted column a checkbox column
    Dim chkCol As New DataGridViewCheckBoxColumn()
    chkCol.DataPropertyName = "Granted"
    chkCol.HeaderText = "Granted"
    dgvPermissions.Columns("Granted") = chkCol
End Sub
```

**Save Button:**
```vb
Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    For Each row As DataGridViewRow In dgvPermissions.Rows
        Dim permissionKey = row.Cells("PermissionKey").Value.ToString()
        Dim isGranted = CBool(row.Cells("Granted").Value)
        Dim originalPerm = _effectivePermissions.First(Function(p) p.PermissionKey = permissionKey)
        
        ' Only update if changed
        If originalPerm.HasPermission <> isGranted Then
            If isGranted Then
                _permissionService.GrantPermission(_user.UserId, permissionKey, SessionManager.CurrentUserId)
            Else
                _permissionService.RevokePermission(_user.UserId, permissionKey, SessionManager.CurrentUserId)
            End If
        End If
    Next
    
    MessageBox.Show("Permissions updated successfully.", "Success")
    Me.DialogResult = DialogResult.OK
    Me.Close()
End Sub
```

**Reset to Defaults Button:**
```vb
Private Sub btnResetDefaults_Click(sender As Object, e As EventArgs) Handles btnResetDefaults.Click
    If MessageBox.Show("Reset all permissions to role defaults?", "Confirm", 
                       MessageBoxButtons.YesNo) = DialogResult.Yes Then
        Dim result = _permissionService.ResetToDefaults(_user.UserId, SessionManager.CurrentUserId)
        If result.IsSuccess Then
            LoadPermissions()  ' Refresh
            MessageBox.Show(result.Message, "Success")
        End If
    End If
End Sub
```

---

## Integration Notes

### SessionManager

You'll need a `SessionManager` class to track the currently logged-in user:

```vb
Public Class SessionManager
    Public Shared Property CurrentUserId As Integer
    Public Shared Property CurrentUser As User
    Public Shared Property IsLoggedIn As Boolean
    
    Public Shared Sub StartSession(user As User)
        CurrentUser = user
        CurrentUserId = user.UserId
        IsLoggedIn = True
    End Sub
    
    Public Shared Sub EndSession()
        CurrentUser = Nothing
        CurrentUserId = 0
        IsLoggedIn = False
    End Sub
End Class
```

Update `AuthService.AuthenticateUser` to call `SessionManager.StartSession(user)` on successful login.

---

## Designer Files

The `.Designer.vb` files for these forms will contain the standard WinForms control initialization code. Key controls:

**UserManagementForm.Designer.vb:**
- `dgvUsers` (DataGridView)
- `btnAddUser`, `btnEditUser`, `btnDeactivate`, `btnResetPassword`, `btnManagePermissions`, `btnClose` (Buttons)

**UserEditForm.Designer.vb:**
- `txtUsername`, `txtFullName`, `txtEmail`, `txtWhatsApp`, `txtPassword`, `txtConfirm` (TextBoxes)
- `cmbRole` (ComboBox with items: Admin, Clerk, Viewer)
- `btnSave`, `btnCancel` (Buttons)

**UserPermissionForm.Designer.vb:**
- `dgvPermissions` (DataGridView)
- `btnResetDefaults`, `btnSave`, `btnCancel` (Buttons)

---

## Next Steps

1. Create the three forms using Visual Studio's WinForms designer
2. Copy the code-behind logic from this guide
3. Test each form individually
4. Integrate with DashboardForm (add "Manage Users" button)
5. Add permission checks to existing forms
