<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UserPermissionForm
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
        Me.lblRole = New System.Windows.Forms.Label()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.clbPermissions = New System.Windows.Forms.CheckedListBox()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnGrantSelected = New System.Windows.Forms.Button()
        Me.btnResetDefaults = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(100, 60, 160)
        Me.pnlHeader.Controls.Add(Me.lblRole)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(500, 55)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(15, 8)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(200, 21)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Permissions for: User"
        '
        'lblRole
        '
        Me.lblRole.AutoSize = True
        Me.lblRole.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point)
        Me.lblRole.ForeColor = System.Drawing.Color.FromArgb(220, 220, 255)
        Me.lblRole.Location = New System.Drawing.Point(17, 32)
        Me.lblRole.Name = "lblRole"
        Me.lblRole.Size = New System.Drawing.Size(80, 15)
        Me.lblRole.TabIndex = 1
        Me.lblRole.Text = "Role: Admin"
        '
        'pnlContent
        '
        Me.pnlContent.Controls.Add(Me.clbPermissions)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 55)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlContent.Size = New System.Drawing.Size(500, 345)
        Me.pnlContent.TabIndex = 1
        '
        'clbPermissions
        '
        Me.clbPermissions.BackColor = System.Drawing.Color.White
        Me.clbPermissions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.clbPermissions.CheckOnClick = True
        Me.clbPermissions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.clbPermissions.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.clbPermissions.FormattingEnabled = True
        Me.clbPermissions.IntegralHeight = False
        Me.clbPermissions.Location = New System.Drawing.Point(15, 15)
        Me.clbPermissions.Name = "clbPermissions"
        Me.clbPermissions.Size = New System.Drawing.Size(470, 315)
        Me.clbPermissions.TabIndex = 0
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.FromArgb(240, 240, 245)
        Me.pnlActions.Controls.Add(Me.btnClose)
        Me.pnlActions.Controls.Add(Me.btnResetDefaults)
        Me.pnlActions.Controls.Add(Me.btnGrantSelected)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(0, 400)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(500, 50)
        Me.pnlActions.TabIndex = 2
        '
        'btnGrantSelected
        '
        Me.btnGrantSelected.BackColor = System.Drawing.Color.FromArgb(0, 150, 80)
        Me.btnGrantSelected.FlatAppearance.BorderSize = 0
        Me.btnGrantSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGrantSelected.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnGrantSelected.ForeColor = System.Drawing.Color.White
        Me.btnGrantSelected.Location = New System.Drawing.Point(15, 10)
        Me.btnGrantSelected.Name = "btnGrantSelected"
        Me.btnGrantSelected.Size = New System.Drawing.Size(150, 32)
        Me.btnGrantSelected.TabIndex = 0
        Me.btnGrantSelected.Text = "Apply Changes"
        Me.btnGrantSelected.UseVisualStyleBackColor = False
        '
        'btnResetDefaults
        '
        Me.btnResetDefaults.BackColor = System.Drawing.Color.FromArgb(200, 120, 0)
        Me.btnResetDefaults.FlatAppearance.BorderSize = 0
        Me.btnResetDefaults.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnResetDefaults.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnResetDefaults.ForeColor = System.Drawing.Color.White
        Me.btnResetDefaults.Location = New System.Drawing.Point(175, 10)
        Me.btnResetDefaults.Name = "btnResetDefaults"
        Me.btnResetDefaults.Size = New System.Drawing.Size(150, 32)
        Me.btnResetDefaults.TabIndex = 1
        Me.btnResetDefaults.Text = "Reset to Defaults"
        Me.btnResetDefaults.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.Gray
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(410, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 32)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'UserPermissionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(500, 450)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "UserPermissionForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "User Permissions"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.pnlActions.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblRole As Label
    Friend WithEvents pnlContent As Panel
    Friend WithEvents clbPermissions As CheckedListBox
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnGrantSelected As Button
    Friend WithEvents btnResetDefaults As Button
    Friend WithEvents btnClose As Button

End Class
