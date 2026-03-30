<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BackupForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.grpBackup = New System.Windows.Forms.GroupBox()
        Me.lblFolderLabel = New System.Windows.Forms.Label()
        Me.txtBackupFolder = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnBackupNow = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.grpHistory = New System.Windows.Forms.GroupBox()
        Me.lstBackups = New System.Windows.Forms.ListBox()
        Me.btnRefreshList = New System.Windows.Forms.Button()
        Me.btnRestore = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.grpBackup.SuspendLayout()
        Me.grpHistory.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(580, 70)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(20, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(200, 30)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Database Backup"
        '
        'lblSubTitle
        '
        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.LightGray
        Me.lblSubTitle.Location = New System.Drawing.Point(22, 44)
        Me.lblSubTitle.Name = "lblSubTitle"
        Me.lblSubTitle.Size = New System.Drawing.Size(300, 15)
        Me.lblSubTitle.TabIndex = 1
        Me.lblSubTitle.Text = "Create a full backup of the Petty Cash database (.bak)"
        '
        'grpBackup
        '
        Me.grpBackup.Controls.Add(Me.lblFolderLabel)
        Me.grpBackup.Controls.Add(Me.txtBackupFolder)
        Me.grpBackup.Controls.Add(Me.btnBrowse)
        Me.grpBackup.Controls.Add(Me.btnBackupNow)
        Me.grpBackup.Controls.Add(Me.lblStatus)
        Me.grpBackup.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grpBackup.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.grpBackup.Location = New System.Drawing.Point(20, 85)
        Me.grpBackup.Name = "grpBackup"
        Me.grpBackup.Size = New System.Drawing.Size(540, 160)
        Me.grpBackup.TabIndex = 1
        Me.grpBackup.TabStop = False
        Me.grpBackup.Text = "Backup Settings"
        '
        'lblFolderLabel
        '
        Me.lblFolderLabel.AutoSize = True
        Me.lblFolderLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblFolderLabel.ForeColor = System.Drawing.Color.Black
        Me.lblFolderLabel.Location = New System.Drawing.Point(15, 30)
        Me.lblFolderLabel.Name = "lblFolderLabel"
        Me.lblFolderLabel.Size = New System.Drawing.Size(90, 15)
        Me.lblFolderLabel.TabIndex = 0
        Me.lblFolderLabel.Text = "Backup Folder:"
        '
        'txtBackupFolder
        '
        Me.txtBackupFolder.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular)
        Me.txtBackupFolder.ForeColor = System.Drawing.Color.Black
        Me.txtBackupFolder.Location = New System.Drawing.Point(15, 50)
        Me.txtBackupFolder.Name = "txtBackupFolder"
        Me.txtBackupFolder.Size = New System.Drawing.Size(420, 25)
        Me.txtBackupFolder.TabIndex = 1
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.FromArgb(70, 130, 180)
        Me.btnBrowse.FlatAppearance.BorderSize = 0
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnBrowse.ForeColor = System.Drawing.Color.White
        Me.btnBrowse.Location = New System.Drawing.Point(445, 48)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(80, 28)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'btnBackupNow
        '
        Me.btnBackupNow.BackColor = System.Drawing.Color.FromArgb(0, 150, 80)
        Me.btnBackupNow.FlatAppearance.BorderSize = 0
        Me.btnBackupNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackupNow.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.btnBackupNow.ForeColor = System.Drawing.Color.White
        Me.btnBackupNow.Location = New System.Drawing.Point(15, 90)
        Me.btnBackupNow.Name = "btnBackupNow"
        Me.btnBackupNow.Size = New System.Drawing.Size(170, 40)
        Me.btnBackupNow.TabIndex = 3
        Me.btnBackupNow.Text = "🗄 Backup Now"
        Me.btnBackupNow.UseVisualStyleBackColor = False
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblStatus.ForeColor = System.Drawing.Color.Gray
        Me.lblStatus.Location = New System.Drawing.Point(195, 100)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(330, 20)
        Me.lblStatus.TabIndex = 4
        Me.lblStatus.Text = "Ready to backup."
        '
        'grpHistory
        '
        Me.grpHistory.Controls.Add(Me.lstBackups)
        Me.grpHistory.Controls.Add(Me.btnRefreshList)
        Me.grpHistory.Controls.Add(Me.btnRestore)
        Me.grpHistory.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grpHistory.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.grpHistory.Location = New System.Drawing.Point(20, 260)
        Me.grpHistory.Name = "grpHistory"
        Me.grpHistory.Size = New System.Drawing.Size(540, 200)
        Me.grpHistory.TabIndex = 2
        Me.grpHistory.TabStop = False
        Me.grpHistory.Text = "Backup History"
        '
        'lstBackups
        '
        Me.lstBackups.Font = New System.Drawing.Font("Consolas", 9.0!)
        Me.lstBackups.ForeColor = System.Drawing.Color.Black
        Me.lstBackups.FormattingEnabled = True
        Me.lstBackups.ItemHeight = 14
        Me.lstBackups.Location = New System.Drawing.Point(15, 28)
        Me.lstBackups.Name = "lstBackups"
        Me.lstBackups.Size = New System.Drawing.Size(510, 130)
        Me.lstBackups.TabIndex = 0
        '
        'btnRefreshList
        '
        Me.btnRefreshList.BackColor = System.Drawing.Color.FromArgb(70, 130, 180)
        Me.btnRefreshList.FlatAppearance.BorderSize = 0
        Me.btnRefreshList.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshList.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefreshList.ForeColor = System.Drawing.Color.White
        Me.btnRefreshList.Location = New System.Drawing.Point(15, 165)
        Me.btnRefreshList.Name = "btnRefreshList"
        Me.btnRefreshList.Size = New System.Drawing.Size(100, 28)
        Me.btnRefreshList.TabIndex = 1
        Me.btnRefreshList.Text = "Refresh"
        Me.btnRefreshList.UseVisualStyleBackColor = False
        '
        'btnRestore
        '
        Me.btnRestore.BackColor = System.Drawing.Color.FromArgb(200, 120, 0)
        Me.btnRestore.FlatAppearance.BorderSize = 0
        Me.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestore.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRestore.ForeColor = System.Drawing.Color.White
        Me.btnRestore.Location = New System.Drawing.Point(125, 165)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(150, 28)
        Me.btnRestore.TabIndex = 2
        Me.btnRestore.Text = "🔄 Restore Selected"
        Me.btnRestore.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.Gray
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(470, 475)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(90, 35)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'BackupForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(240, 240, 245)
        Me.ClientSize = New System.Drawing.Size(580, 520)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.grpHistory)
        Me.Controls.Add(Me.grpBackup)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BackupForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Database Backup"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.grpBackup.ResumeLayout(False)
        Me.grpBackup.PerformLayout()
        Me.grpHistory.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblSubTitle As System.Windows.Forms.Label
    Friend WithEvents grpBackup As System.Windows.Forms.GroupBox
    Friend WithEvents lblFolderLabel As System.Windows.Forms.Label
    Friend WithEvents txtBackupFolder As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents btnBackupNow As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents grpHistory As System.Windows.Forms.GroupBox
    Friend WithEvents lstBackups As System.Windows.Forms.ListBox
    Friend WithEvents btnRefreshList As System.Windows.Forms.Button
    Friend WithEvents btnRestore As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button

End Class
