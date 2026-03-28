<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UserManagementForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.dgvUsers = New System.Windows.Forms.DataGridView()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.btnEditUser = New System.Windows.Forms.Button()
        Me.btnDeactivate = New System.Windows.Forms.Button()
        Me.btnResetPassword = New System.Windows.Forms.Button()
        Me.btnManagePermissions = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlActions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(100, 60, 160)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(800, 50)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(20, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(200, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "User Management"
        '
        'pnlGrid
        '
        Me.pnlGrid.Controls.Add(Me.dgvUsers)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 50)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlGrid.Size = New System.Drawing.Size(800, 350)
        Me.pnlGrid.TabIndex = 1
        '
        'dgvUsers
        '
        Me.dgvUsers.AllowUserToAddRows = False
        Me.dgvUsers.AllowUserToDeleteRows = False
        Me.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvUsers.BackgroundColor = System.Drawing.Color.White
        Me.dgvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvUsers.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.dgvUsers.GridColor = System.Drawing.Color.FromArgb(230, 230, 235)
        Me.dgvUsers.Location = New System.Drawing.Point(15, 15)
        Me.dgvUsers.MultiSelect = False
        Me.dgvUsers.Name = "dgvUsers"
        Me.dgvUsers.ReadOnly = True
        Me.dgvUsers.RowHeadersVisible = False
        Me.dgvUsers.RowTemplate.Height = 35
        Me.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUsers.Size = New System.Drawing.Size(770, 320)
        Me.dgvUsers.TabIndex = 0
        Me.dgvUsers.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(100, 60, 160)
        Me.dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White
        Me.dgvUsers.EnableHeadersVisualStyles = False
        Me.dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 250)
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.FromArgb(240, 240, 245)
        Me.pnlActions.Controls.Add(Me.btnClose)
        Me.pnlActions.Controls.Add(Me.btnManagePermissions)
        Me.pnlActions.Controls.Add(Me.btnResetPassword)
        Me.pnlActions.Controls.Add(Me.btnDeactivate)
        Me.pnlActions.Controls.Add(Me.btnEditUser)
        Me.pnlActions.Controls.Add(Me.btnAddUser)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(0, 400)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(800, 50)
        Me.pnlActions.TabIndex = 2
        '
        'btnAddUser
        '
        Me.btnAddUser.BackColor = System.Drawing.Color.FromArgb(0, 150, 80)
        Me.btnAddUser.FlatAppearance.BorderSize = 0
        Me.btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddUser.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnAddUser.ForeColor = System.Drawing.Color.White
        Me.btnAddUser.Location = New System.Drawing.Point(15, 10)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(100, 30)
        Me.btnAddUser.TabIndex = 0
        Me.btnAddUser.Text = "+ Add User"
        Me.btnAddUser.UseVisualStyleBackColor = False
        '
        'btnEditUser
        '
        Me.btnEditUser.BackColor = System.Drawing.Color.FromArgb(50, 100, 180)
        Me.btnEditUser.FlatAppearance.BorderSize = 0
        Me.btnEditUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditUser.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnEditUser.ForeColor = System.Drawing.Color.White
        Me.btnEditUser.Location = New System.Drawing.Point(125, 10)
        Me.btnEditUser.Name = "btnEditUser"
        Me.btnEditUser.Size = New System.Drawing.Size(80, 30)
        Me.btnEditUser.TabIndex = 1
        Me.btnEditUser.Text = "Edit"
        Me.btnEditUser.UseVisualStyleBackColor = False
        '
        'btnDeactivate
        '
        Me.btnDeactivate.BackColor = System.Drawing.Color.FromArgb(200, 120, 0)
        Me.btnDeactivate.FlatAppearance.BorderSize = 0
        Me.btnDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeactivate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnDeactivate.ForeColor = System.Drawing.Color.White
        Me.btnDeactivate.Location = New System.Drawing.Point(215, 10)
        Me.btnDeactivate.Name = "btnDeactivate"
        Me.btnDeactivate.Size = New System.Drawing.Size(100, 30)
        Me.btnDeactivate.TabIndex = 2
        Me.btnDeactivate.Text = "Deactivate"
        Me.btnDeactivate.UseVisualStyleBackColor = False
        '
        'btnResetPassword
        '
        Me.btnResetPassword.BackColor = System.Drawing.Color.FromArgb(180, 50, 50)
        Me.btnResetPassword.FlatAppearance.BorderSize = 0
        Me.btnResetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetPassword.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnResetPassword.ForeColor = System.Drawing.Color.White
        Me.btnResetPassword.Location = New System.Drawing.Point(325, 10)
        Me.btnResetPassword.Name = "btnResetPassword"
        Me.btnResetPassword.Size = New System.Drawing.Size(120, 30)
        Me.btnResetPassword.TabIndex = 3
        Me.btnResetPassword.Text = "Reset Password"
        Me.btnResetPassword.UseVisualStyleBackColor = False
        '
        'btnManagePermissions
        '
        Me.btnManagePermissions.BackColor = System.Drawing.Color.FromArgb(100, 60, 160)
        Me.btnManagePermissions.FlatAppearance.BorderSize = 0
        Me.btnManagePermissions.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnManagePermissions.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnManagePermissions.ForeColor = System.Drawing.Color.White
        Me.btnManagePermissions.Location = New System.Drawing.Point(455, 10)
        Me.btnManagePermissions.Name = "btnManagePermissions"
        Me.btnManagePermissions.Size = New System.Drawing.Size(130, 30)
        Me.btnManagePermissions.TabIndex = 4
        Me.btnManagePermissions.Text = "Permissions"
        Me.btnManagePermissions.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.Gray
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(710, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 30)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'UserManagementForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UserManagementForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "User Management"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlActions.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlGrid As Panel
    Friend WithEvents dgvUsers As DataGridView
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnAddUser As Button
    Friend WithEvents btnEditUser As Button
    Friend WithEvents btnDeactivate As Button
    Friend WithEvents btnResetPassword As Button
    Friend WithEvents btnManagePermissions As Button
    Friend WithEvents btnClose As Button

End Class
