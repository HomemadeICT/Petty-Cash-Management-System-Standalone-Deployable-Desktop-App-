' ============================================================================
' AdminSettingsForm.vb - Admin Settings Form Code-Behind
' Petty Cash Management System
' ============================================================================
' Purpose: View notification settings and test notifications
' ============================================================================

Imports System.Windows.Forms

Public Class AdminSettingsForm

#Region "Private Fields"
    Private _notificationService As NotificationService
#End Region

#Region "Form Events"

    Private Sub AdminSettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize notification service
        _notificationService = New NotificationService()

        ' Load settings from config
        LoadSettings()

        ' Update status labels
        UpdateStatus()
    End Sub

#End Region

#Region "Load Settings"

    Private Sub LoadSettings()
        ' Email settings
        Dim emailEnabled = Boolean.Parse(ConfigManager.GetSetting("EmailNotificationsEnabled", "False"))
        chkEmailEnabled.Checked = emailEnabled
        txtSmtpHost.Text = ConfigManager.GetSetting("SmtpHost", "")
        txtSmtpPort.Text = ConfigManager.GetSetting("SmtpPort", "")
        txtAdminEmail.Text = ConfigManager.GetSetting("AdminEmail", "")

        ' WhatsApp settings
        Dim whatsAppEnabled = Boolean.Parse(ConfigManager.GetSetting("WhatsAppNotificationsEnabled", "False"))
        chkWhatsAppEnabled.Checked = whatsAppEnabled
        
        Dim twilioSid = ConfigManager.GetSetting("TwilioAccountSid", "")
        Dim twilioToken = ConfigManager.GetSetting("TwilioAuthToken", "")
        
        ' Show masked values for security
        If Not String.IsNullOrEmpty(twilioSid) Then
            txtTwilioSid.Text = "AC" & New String("*"c, twilioSid.Length - 6) & twilioSid.Substring(Math.Max(0, twilioSid.Length - 4))
        End If
        
        If Not String.IsNullOrEmpty(twilioToken) Then
            txtTwilioToken.Text = New String("*"c, 32)
        End If
        
        txtTwilioNumber.Text = ConfigManager.GetSetting("TwilioWhatsAppNumber", "")
        txtAdminWhatsApp.Text = ConfigManager.GetSetting("AdminWhatsAppNumber", "")
    End Sub

    Private Sub UpdateStatus()
        ' Email status
        Dim emailEnabled = chkEmailEnabled.Checked
        Dim emailConfigured = Not String.IsNullOrEmpty(txtSmtpHost.Text) AndAlso 
                             Not String.IsNullOrEmpty(txtAdminEmail.Text)
        
        If emailEnabled AndAlso emailConfigured Then
            lblEmailStatus.Text = "Status: ✓ Configured and Enabled"
            lblEmailStatus.ForeColor = Color.Green
            btnTestEmail.Enabled = True
        ElseIf emailConfigured Then
            lblEmailStatus.Text = "Status: Configured but Disabled"
            lblEmailStatus.ForeColor = Color.Orange
            btnTestEmail.Enabled = False
        Else
            lblEmailStatus.Text = "Status: Not Configured"
            lblEmailStatus.ForeColor = Color.Red
            btnTestEmail.Enabled = False
        End If

        ' WhatsApp status
        Dim whatsAppEnabled = chkWhatsAppEnabled.Checked
        Dim whatsAppConfigured = Not String.IsNullOrEmpty(txtTwilioSid.Text) AndAlso 
                                Not String.IsNullOrEmpty(txtTwilioToken.Text) AndAlso
                                Not String.IsNullOrEmpty(txtAdminWhatsApp.Text)
        
        If whatsAppEnabled AndAlso whatsAppConfigured Then
            lblWhatsAppStatus.Text = "Status: ✓ Configured and Enabled"
            lblWhatsAppStatus.ForeColor = Color.Green
            btnTestWhatsApp.Enabled = True
        ElseIf whatsAppConfigured Then
            lblWhatsAppStatus.Text = "Status: Configured but Disabled"
            lblWhatsAppStatus.ForeColor = Color.Orange
            btnTestWhatsApp.Enabled = False
        Else
            lblWhatsAppStatus.Text = "Status: Not Configured"
            lblWhatsAppStatus.ForeColor = Color.Red
            btnTestWhatsApp.Enabled = False
        End If
    End Sub

#End Region

#Region "Test Buttons"

    Private Sub btnTestEmail_Click(sender As Object, e As EventArgs) Handles btnTestEmail.Click
        btnTestEmail.Enabled = False
        btnTestEmail.Text = "Sending..."
        Application.DoEvents()

        Try
            ' Send test notification
            Dim result = _notificationService.SendOveruseNotification("Test Month", 26000, 25000)
            
            If result.EmailSent Then
                MessageBox.Show("Test email sent successfully! Check the admin email inbox.", 
                              "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to send test email. Please check SMTP settings in App.config.", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show($"Error sending test email: {ex.Message}", 
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnTestEmail.Enabled = True
            btnTestEmail.Text = "Test Email"
        End Try
    End Sub

    Private Sub btnTestWhatsApp_Click(sender As Object, e As EventArgs) Handles btnTestWhatsApp.Click
        btnTestWhatsApp.Enabled = False
        btnTestWhatsApp.Text = "Sending..."
        Application.DoEvents()

        Try
            ' Send test notification
            Dim result = _notificationService.SendOveruseNotification("Test Month", 26000, 25000)
            
            If result.WhatsAppSent Then
                MessageBox.Show("Test WhatsApp message sent successfully! Check the admin WhatsApp.", 
                              "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to send test WhatsApp. Please check Twilio settings in App.config.", 
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show($"Error sending test WhatsApp: {ex.Message}", 
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnTestWhatsApp.Enabled = True
            btnTestWhatsApp.Text = "Test WhatsApp"
        End Try
    End Sub

#End Region

#Region "Close Button"

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSinhalaMonths_Click(sender As Object, e As EventArgs) Handles btnSinhalaMonths.Click
        Using frm As New SinhalaMonthSettingsForm()
            frm.ShowDialog(Me)
        End Using
    End Sub

#End Region

End Class