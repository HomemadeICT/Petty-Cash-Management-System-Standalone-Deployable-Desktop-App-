<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminSettingsForm
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.grpEmail = New System.Windows.Forms.GroupBox()
        Me.lblEmailStatus = New System.Windows.Forms.Label()
        Me.btnTestEmail = New System.Windows.Forms.Button()
        Me.txtAdminEmail = New System.Windows.Forms.TextBox()
        Me.lblAdminEmail = New System.Windows.Forms.Label()
        Me.txtSmtpPort = New System.Windows.Forms.TextBox()
        Me.lblSmtpPort = New System.Windows.Forms.Label()
        Me.txtSmtpHost = New System.Windows.Forms.TextBox()
        Me.lblSmtpHost = New System.Windows.Forms.Label()
        Me.chkEmailEnabled = New System.Windows.Forms.CheckBox()
        Me.grpWhatsApp = New System.Windows.Forms.GroupBox()
        Me.lblWhatsAppStatus = New System.Windows.Forms.Label()
        Me.btnTestWhatsApp = New System.Windows.Forms.Button()
        Me.txtAdminWhatsApp = New System.Windows.Forms.TextBox()
        Me.lblAdminWhatsApp = New System.Windows.Forms.Label()
        Me.txtTwilioNumber = New System.Windows.Forms.TextBox()
        Me.lblTwilioNumber = New System.Windows.Forms.Label()
        Me.txtTwilioToken = New System.Windows.Forms.TextBox()
        Me.lblTwilioToken = New System.Windows.Forms.Label()
        Me.txtTwilioSid = New System.Windows.Forms.TextBox()
        Me.lblTwilioSid = New System.Windows.Forms.Label()
        Me.chkWhatsAppEnabled = New System.Windows.Forms.CheckBox()
        Me.grpInfo = New System.Windows.Forms.GroupBox()
        Me.lblInfoText = New System.Windows.Forms.Label()
        Me.btnSinhalaMonths = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.grpEmail.SuspendLayout()
        Me.grpWhatsApp.SuspendLayout()
        Me.grpInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(700, 60)
        Me.pnlHeader.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(250, 30)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Notification Settings"
        '
        'grpEmail
        '
        Me.grpEmail.Controls.Add(Me.lblEmailStatus)
        Me.grpEmail.Controls.Add(Me.btnTestEmail)
        Me.grpEmail.Controls.Add(Me.txtAdminEmail)
        Me.grpEmail.Controls.Add(Me.lblAdminEmail)
        Me.grpEmail.Controls.Add(Me.txtSmtpPort)
        Me.grpEmail.Controls.Add(Me.lblSmtpPort)
        Me.grpEmail.Controls.Add(Me.txtSmtpHost)
        Me.grpEmail.Controls.Add(Me.lblSmtpHost)
        Me.grpEmail.Controls.Add(Me.chkEmailEnabled)
        Me.grpEmail.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.grpEmail.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.grpEmail.Location = New System.Drawing.Point(20, 80)
        Me.grpEmail.Name = "grpEmail"
        Me.grpEmail.Size = New System.Drawing.Size(660, 200)
        Me.grpEmail.TabIndex = 1
        Me.grpEmail.TabStop = False
        Me.grpEmail.Text = "Email Notifications"
        '
        'chkEmailEnabled
        '
        Me.chkEmailEnabled.AutoSize = True
        Me.chkEmailEnabled.Enabled = False
        Me.chkEmailEnabled.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.chkEmailEnabled.ForeColor = System.Drawing.Color.Black
        Me.chkEmailEnabled.Location = New System.Drawing.Point(20, 30)
        Me.chkEmailEnabled.Name = "chkEmailEnabled"
        Me.chkEmailEnabled.Size = New System.Drawing.Size(180, 19)
        Me.chkEmailEnabled.TabIndex = 0
        Me.chkEmailEnabled.Text = "Email Notifications Enabled"
        '
        'lblSmtpHost
        '
        Me.lblSmtpHost.AutoSize = True
        Me.lblSmtpHost.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblSmtpHost.ForeColor = System.Drawing.Color.Gray
        Me.lblSmtpHost.Location = New System.Drawing.Point(20, 60)
        Me.lblSmtpHost.Name = "lblSmtpHost"
        Me.lblSmtpHost.Size = New System.Drawing.Size(70, 15)
        Me.lblSmtpHost.TabIndex = 1
        Me.lblSmtpHost.Text = "SMTP Host:"
        '
        'txtSmtpHost
        '
        Me.txtSmtpHost.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtSmtpHost.Location = New System.Drawing.Point(150, 57)
        Me.txtSmtpHost.Name = "txtSmtpHost"
        Me.txtSmtpHost.ReadOnly = True
        Me.txtSmtpHost.Size = New System.Drawing.Size(200, 23)
        Me.txtSmtpHost.TabIndex = 2
        '
        'lblSmtpPort
        '
        Me.lblSmtpPort.AutoSize = True
        Me.lblSmtpPort.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblSmtpPort.ForeColor = System.Drawing.Color.Gray
        Me.lblSmtpPort.Location = New System.Drawing.Point(370, 60)
        Me.lblSmtpPort.Name = "lblSmtpPort"
        Me.lblSmtpPort.Size = New System.Drawing.Size(32, 15)
        Me.lblSmtpPort.TabIndex = 3
        Me.lblSmtpPort.Text = "Port:"
        '
        'txtSmtpPort
        '
        Me.txtSmtpPort.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtSmtpPort.Location = New System.Drawing.Point(420, 57)
        Me.txtSmtpPort.Name = "txtSmtpPort"
        Me.txtSmtpPort.ReadOnly = True
        Me.txtSmtpPort.Size = New System.Drawing.Size(80, 23)
        Me.txtSmtpPort.TabIndex = 4
        '
        'lblAdminEmail
        '
        Me.lblAdminEmail.AutoSize = True
        Me.lblAdminEmail.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblAdminEmail.ForeColor = System.Drawing.Color.Gray
        Me.lblAdminEmail.Location = New System.Drawing.Point(20, 95)
        Me.lblAdminEmail.Name = "lblAdminEmail"
        Me.lblAdminEmail.Size = New System.Drawing.Size(78, 15)
        Me.lblAdminEmail.TabIndex = 5
        Me.lblAdminEmail.Text = "Admin Email:"
        '
        'txtAdminEmail
        '
        Me.txtAdminEmail.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtAdminEmail.Location = New System.Drawing.Point(150, 92)
        Me.txtAdminEmail.Name = "txtAdminEmail"
        Me.txtAdminEmail.ReadOnly = True
        Me.txtAdminEmail.Size = New System.Drawing.Size(350, 23)
        Me.txtAdminEmail.TabIndex = 6
        '
        'btnTestEmail
        '
        Me.btnTestEmail.BackColor = System.Drawing.Color.FromArgb(0, 120, 215)
        Me.btnTestEmail.FlatAppearance.BorderSize = 0
        Me.btnTestEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTestEmail.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnTestEmail.ForeColor = System.Drawing.Color.White
        Me.btnTestEmail.Location = New System.Drawing.Point(20, 130)
        Me.btnTestEmail.Name = "btnTestEmail"
        Me.btnTestEmail.Size = New System.Drawing.Size(150, 30)
        Me.btnTestEmail.TabIndex = 7
        Me.btnTestEmail.Text = "Test Email"
        Me.btnTestEmail.UseVisualStyleBackColor = False
        '
        'lblEmailStatus
        '
        Me.lblEmailStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point)
        Me.lblEmailStatus.ForeColor = System.Drawing.Color.Gray
        Me.lblEmailStatus.Location = New System.Drawing.Point(20, 170)
        Me.lblEmailStatus.Name = "lblEmailStatus"
        Me.lblEmailStatus.Size = New System.Drawing.Size(620, 20)
        Me.lblEmailStatus.TabIndex = 8
        Me.lblEmailStatus.Text = "Status: Not configured"
        '
        'grpWhatsApp
        '
        Me.grpWhatsApp.Controls.Add(Me.lblWhatsAppStatus)
        Me.grpWhatsApp.Controls.Add(Me.btnTestWhatsApp)
        Me.grpWhatsApp.Controls.Add(Me.txtAdminWhatsApp)
        Me.grpWhatsApp.Controls.Add(Me.lblAdminWhatsApp)
        Me.grpWhatsApp.Controls.Add(Me.txtTwilioNumber)
        Me.grpWhatsApp.Controls.Add(Me.lblTwilioNumber)
        Me.grpWhatsApp.Controls.Add(Me.txtTwilioToken)
        Me.grpWhatsApp.Controls.Add(Me.lblTwilioToken)
        Me.grpWhatsApp.Controls.Add(Me.txtTwilioSid)
        Me.grpWhatsApp.Controls.Add(Me.lblTwilioSid)
        Me.grpWhatsApp.Controls.Add(Me.chkWhatsAppEnabled)
        Me.grpWhatsApp.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.grpWhatsApp.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.grpWhatsApp.Location = New System.Drawing.Point(20, 290)
        Me.grpWhatsApp.Name = "grpWhatsApp"
        Me.grpWhatsApp.Size = New System.Drawing.Size(660, 250)
        Me.grpWhatsApp.TabIndex = 2
        Me.grpWhatsApp.TabStop = False
        Me.grpWhatsApp.Text = "WhatsApp Notifications (Twilio)"
        '
        'chkWhatsAppEnabled
        '
        Me.chkWhatsAppEnabled.AutoSize = True
        Me.chkWhatsAppEnabled.Enabled = False
        Me.chkWhatsAppEnabled.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.chkWhatsAppEnabled.ForeColor = System.Drawing.Color.Black
        Me.chkWhatsAppEnabled.Location = New System.Drawing.Point(20, 30)
        Me.chkWhatsAppEnabled.Name = "chkWhatsAppEnabled"
        Me.chkWhatsAppEnabled.Size = New System.Drawing.Size(210, 19)
        Me.chkWhatsAppEnabled.TabIndex = 0
        Me.chkWhatsAppEnabled.Text = "WhatsApp Notifications Enabled"
        '
        'lblTwilioSid
        '
        Me.lblTwilioSid.AutoSize = True
        Me.lblTwilioSid.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblTwilioSid.ForeColor = System.Drawing.Color.Gray
        Me.lblTwilioSid.Location = New System.Drawing.Point(20, 60)
        Me.lblTwilioSid.Name = "lblTwilioSid"
        Me.lblTwilioSid.Size = New System.Drawing.Size(76, 15)
        Me.lblTwilioSid.TabIndex = 1
        Me.lblTwilioSid.Text = "Account SID:"
        '
        'txtTwilioSid
        '
        Me.txtTwilioSid.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtTwilioSid.Location = New System.Drawing.Point(150, 57)
        Me.txtTwilioSid.Name = "txtTwilioSid"
        Me.txtTwilioSid.ReadOnly = True
        Me.txtTwilioSid.Size = New System.Drawing.Size(350, 23)
        Me.txtTwilioSid.TabIndex = 2
        Me.txtTwilioSid.UseSystemPasswordChar = True
        '
        'lblTwilioToken
        '
        Me.lblTwilioToken.AutoSize = True
        Me.lblTwilioToken.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblTwilioToken.ForeColor = System.Drawing.Color.Gray
        Me.lblTwilioToken.Location = New System.Drawing.Point(20, 95)
        Me.lblTwilioToken.Name = "lblTwilioToken"
        Me.lblTwilioToken.Size = New System.Drawing.Size(72, 15)
        Me.lblTwilioToken.TabIndex = 3
        Me.lblTwilioToken.Text = "Auth Token:"
        '
        'txtTwilioToken
        '
        Me.txtTwilioToken.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtTwilioToken.Location = New System.Drawing.Point(150, 92)
        Me.txtTwilioToken.Name = "txtTwilioToken"
        Me.txtTwilioToken.ReadOnly = True
        Me.txtTwilioToken.Size = New System.Drawing.Size(350, 23)
        Me.txtTwilioToken.TabIndex = 4
        Me.txtTwilioToken.UseSystemPasswordChar = True
        '
        'lblTwilioNumber
        '
        Me.lblTwilioNumber.AutoSize = True
        Me.lblTwilioNumber.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblTwilioNumber.ForeColor = System.Drawing.Color.Gray
        Me.lblTwilioNumber.Location = New System.Drawing.Point(20, 130)
        Me.lblTwilioNumber.Name = "lblTwilioNumber"
        Me.lblTwilioNumber.Size = New System.Drawing.Size(122, 15)
        Me.lblTwilioNumber.TabIndex = 5
        Me.lblTwilioNumber.Text = "Twilio WhatsApp No:"
        '
        'txtTwilioNumber
        '
        Me.txtTwilioNumber.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtTwilioNumber.Location = New System.Drawing.Point(150, 127)
        Me.txtTwilioNumber.Name = "txtTwilioNumber"
        Me.txtTwilioNumber.ReadOnly = True
        Me.txtTwilioNumber.Size = New System.Drawing.Size(200, 23)
        Me.txtTwilioNumber.TabIndex = 6
        '
        'lblAdminWhatsApp
        '
        Me.lblAdminWhatsApp.AutoSize = True
        Me.lblAdminWhatsApp.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblAdminWhatsApp.ForeColor = System.Drawing.Color.Gray
        Me.lblAdminWhatsApp.Location = New System.Drawing.Point(20, 165)
        Me.lblAdminWhatsApp.Name = "lblAdminWhatsApp"
        Me.lblAdminWhatsApp.Size = New System.Drawing.Size(120, 15)
        Me.lblAdminWhatsApp.TabIndex = 7
        Me.lblAdminWhatsApp.Text = "Admin WhatsApp No:"
        '
        'txtAdminWhatsApp
        '
        Me.txtAdminWhatsApp.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.txtAdminWhatsApp.Location = New System.Drawing.Point(150, 162)
        Me.txtAdminWhatsApp.Name = "txtAdminWhatsApp"
        Me.txtAdminWhatsApp.ReadOnly = True
        Me.txtAdminWhatsApp.Size = New System.Drawing.Size(200, 23)
        Me.txtAdminWhatsApp.TabIndex = 8
        '
        'btnTestWhatsApp
        '
        Me.btnTestWhatsApp.BackColor = System.Drawing.Color.FromArgb(37, 211, 102)
        Me.btnTestWhatsApp.FlatAppearance.BorderSize = 0
        Me.btnTestWhatsApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTestWhatsApp.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnTestWhatsApp.ForeColor = System.Drawing.Color.White
        Me.btnTestWhatsApp.Location = New System.Drawing.Point(20, 200)
        Me.btnTestWhatsApp.Name = "btnTestWhatsApp"
        Me.btnTestWhatsApp.Size = New System.Drawing.Size(150, 30)
        Me.btnTestWhatsApp.TabIndex = 9
        Me.btnTestWhatsApp.Text = "Test WhatsApp"
        Me.btnTestWhatsApp.UseVisualStyleBackColor = False
        '
        'lblWhatsAppStatus
        '
        Me.lblWhatsAppStatus.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point)
        Me.lblWhatsAppStatus.ForeColor = System.Drawing.Color.Gray
        Me.lblWhatsAppStatus.Location = New System.Drawing.Point(190, 205)
        Me.lblWhatsAppStatus.Name = "lblWhatsAppStatus"
        Me.lblWhatsAppStatus.Size = New System.Drawing.Size(450, 20)
        Me.lblWhatsAppStatus.TabIndex = 10
        Me.lblWhatsAppStatus.Text = "Status: Not configured"
        '
        'grpInfo
        '
        Me.grpInfo.Controls.Add(Me.lblInfoText)
        Me.grpInfo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.grpInfo.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.grpInfo.Location = New System.Drawing.Point(20, 550)
        Me.grpInfo.Name = "grpInfo"
        Me.grpInfo.Size = New System.Drawing.Size(660, 80)
        Me.grpInfo.TabIndex = 3
        Me.grpInfo.TabStop = False
        Me.grpInfo.Text = "Information"
        '
        'lblInfoText
        '
        Me.lblInfoText.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblInfoText.ForeColor = System.Drawing.Color.Gray
        Me.lblInfoText.Location = New System.Drawing.Point(15, 25)
        Me.lblInfoText.Name = "lblInfoText"
        Me.lblInfoText.Size = New System.Drawing.Size(630, 45)
        Me.lblInfoText.TabIndex = 0
        Me.lblInfoText.Text = "Settings are configured in App.config file. To change settings, edit the App.config file and restart the application. Test buttons will send a sample notification to verify the configuration."
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Gray
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(580, 645)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 35)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnSinhalaMonths
        '
        Me.btnSinhalaMonths.BackColor = System.Drawing.Color.FromArgb(0, 128, 96)
        Me.btnSinhalaMonths.FlatAppearance.BorderSize = 0
        Me.btnSinhalaMonths.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSinhalaMonths.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnSinhalaMonths.ForeColor = System.Drawing.Color.White
        Me.btnSinhalaMonths.Location = New System.Drawing.Point(20, 645)
        Me.btnSinhalaMonths.Name = "btnSinhalaMonths"
        Me.btnSinhalaMonths.Size = New System.Drawing.Size(240, 35)
        Me.btnSinhalaMonths.TabIndex = 5
        Me.btnSinhalaMonths.Text = "සිංහල මස් නම් සංස්කරණය"
        Me.btnSinhalaMonths.UseVisualStyleBackColor = False
        '
        'AdminSettingsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(240, 240, 245)
        Me.ClientSize = New System.Drawing.Size(700, 695)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSinhalaMonths)
        Me.Controls.Add(Me.grpInfo)
        Me.Controls.Add(Me.grpWhatsApp)
        Me.Controls.Add(Me.grpEmail)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AdminSettingsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Notification Settings"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.grpEmail.ResumeLayout(False)
        Me.grpEmail.PerformLayout()
        Me.grpWhatsApp.ResumeLayout(False)
        Me.grpWhatsApp.PerformLayout()
        Me.grpInfo.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents grpEmail As GroupBox
    Friend WithEvents chkEmailEnabled As CheckBox
    Friend WithEvents lblSmtpHost As Label
    Friend WithEvents txtSmtpHost As TextBox
    Friend WithEvents lblSmtpPort As Label
    Friend WithEvents txtSmtpPort As TextBox
    Friend WithEvents lblAdminEmail As Label
    Friend WithEvents txtAdminEmail As TextBox
    Friend WithEvents btnTestEmail As Button
    Friend WithEvents lblEmailStatus As Label
    Friend WithEvents grpWhatsApp As GroupBox
    Friend WithEvents chkWhatsAppEnabled As CheckBox
    Friend WithEvents lblTwilioSid As Label
    Friend WithEvents txtTwilioSid As TextBox
    Friend WithEvents lblTwilioToken As Label
    Friend WithEvents txtTwilioToken As TextBox
    Friend WithEvents lblTwilioNumber As Label
    Friend WithEvents txtTwilioNumber As TextBox
    Friend WithEvents lblAdminWhatsApp As Label
    Friend WithEvents txtAdminWhatsApp As TextBox
    Friend WithEvents btnTestWhatsApp As Button
    Friend WithEvents lblWhatsAppStatus As Label
    Friend WithEvents grpInfo As GroupBox
    Friend WithEvents lblInfoText As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents btnSinhalaMonths As Button
End Class
