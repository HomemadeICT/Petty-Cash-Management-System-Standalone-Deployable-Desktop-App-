' ============================================================================
' NotificationService.vb - Unified Notification Service
' Petty Cash Management System
' ============================================================================
' Purpose: Sends notifications via Email and WhatsApp for alerts and reports
' Layer: Business Logic Layer
' Dependencies: ConfigManager, WhatsAppService
' ============================================================================

Imports System.Net.Mail

''' <summary>
''' Unified notification service - sends via both Email and WhatsApp.
''' </summary>
Public Class NotificationService

#Region "Private Fields"
    ' Email settings
    Private ReadOnly _smtpHost As String
    Private ReadOnly _smtpPort As Integer
    Private ReadOnly _senderEmail As String
    Private ReadOnly _senderPassword As String
    Private ReadOnly _emailEnabled As Boolean
    Private ReadOnly _adminEmail As String

    ' WhatsApp service
    Private ReadOnly _whatsAppService As WhatsAppService
#End Region

#Region "Constructor"
    Public Sub New()
        ' Load email configuration
        _smtpHost = ConfigManager.GetSetting("SmtpHost", "smtp.gmail.com")
        _smtpPort = Integer.Parse(ConfigManager.GetSetting("SmtpPort", "587"))
        _senderEmail = ConfigManager.GetSetting("SenderEmail", "")
        _senderPassword = ConfigManager.GetSetting("SenderPassword", "")
        _emailEnabled = Boolean.Parse(ConfigManager.GetSetting("EmailNotificationsEnabled", "False"))
        _adminEmail = ConfigManager.GetSetting("AdminEmail", "")

        ' Initialize WhatsApp service
        _whatsAppService = New WhatsAppService()
    End Sub
#End Region

#Region "Public Methods - Unified Notifications"

    ''' <summary>
    ''' Sends overuse notification via all enabled channels.
    ''' </summary>
    Public Function SendOveruseNotification(monthName As String, currentTotal As Decimal, limit As Decimal) As NotificationResult
        Dim result As New NotificationResult()

        ' Send Email
        If _emailEnabled AndAlso Not String.IsNullOrEmpty(_adminEmail) Then
            result.EmailSent = SendOveruseEmail(_adminEmail, monthName, currentTotal, limit)
        End If

        ' Send WhatsApp
        If _whatsAppService.IsConfigured() Then
            result.WhatsAppSent = _whatsAppService.SendOveruseNotification(monthName, currentTotal, limit)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Sends high-value bill warning via all enabled channels.
    ''' </summary>
    Public Function SendHighValueWarning(billNo As String, amount As Decimal, monthName As String) As NotificationResult
        Dim result As New NotificationResult()

        ' Send Email
        If _emailEnabled AndAlso Not String.IsNullOrEmpty(_adminEmail) Then
            result.EmailSent = SendHighValueEmail(_adminEmail, billNo, amount, monthName)
        End If

        ' Send WhatsApp
        If _whatsAppService.IsConfigured() Then
            result.WhatsAppSent = _whatsAppService.SendHighValueWarning(billNo, amount, monthName)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Sends monthly report via all enabled channels.
    ''' </summary>
    Public Function SendMonthlyReport(report As MonthlyReportDTO) As NotificationResult
        Dim result As New NotificationResult()

        ' Send Email
        If _emailEnabled AndAlso Not String.IsNullOrEmpty(_adminEmail) Then
            result.EmailSent = SendMonthlyReportEmail(_adminEmail, report)
        End If

        ' Send WhatsApp
        If _whatsAppService.IsConfigured() Then
            result.WhatsAppSent = _whatsAppService.SendMonthlyReport(report)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Gets the status of notification services.
    ''' </summary>
    Public Function GetServiceStatus() As String
        Dim status = "Notification Status:" & vbCrLf
        status &= $"  Email: {If(_emailEnabled, "Enabled", "Disabled")}" & vbCrLf
        status &= $"  WhatsApp: {If(_whatsAppService.IsConfigured(), "Configured", "Not Configured")}"
        Return status
    End Function

#End Region

#Region "Legacy Methods (Backward Compatibility)"

    ''' <summary>
    ''' Sends overuse notification via email only (legacy).
    ''' </summary>
    Public Function SendOveruseNotification(recipientEmail As String, monthName As String, currentTotal As Decimal, limit As Decimal) As Boolean
        If Not _emailEnabled OrElse String.IsNullOrEmpty(recipientEmail) Then Return False
        Return SendOveruseEmail(recipientEmail, monthName, currentTotal, limit)
    End Function

    ''' <summary>
    ''' Sends high-value warning via email only (legacy).
    ''' </summary>
    Public Function SendHighValueWarning(recipientEmail As String, billNo As String, amount As Decimal, monthName As String) As Boolean
        If Not _emailEnabled OrElse String.IsNullOrEmpty(recipientEmail) Then Return False
        Return SendHighValueEmail(recipientEmail, billNo, amount, monthName)
    End Function

    ''' <summary>
    ''' Sends monthly report via email only (legacy).
    ''' </summary>
    Public Function SendMonthlyReport(recipientEmail As String, report As MonthlyReportDTO) As Boolean
        If Not _emailEnabled OrElse String.IsNullOrEmpty(recipientEmail) Then Return False
        Return SendMonthlyReportEmail(recipientEmail, report)
    End Function

#End Region

#Region "Private Email Methods"

    Private Function SendOveruseEmail(recipientEmail As String, monthName As String, currentTotal As Decimal, limit As Decimal) As Boolean
        Dim subject = $"[ALERT] Petty Cash Limit Exceeded - {monthName}"
        Dim body = $"
Dear Supervisor,

This is an automated notification from the Petty Cash Management System.

ALERT: Monthly limit has been exceeded.

Month: {monthName}
Current Total: LKR {currentTotal:N2}
Monthly Limit: LKR {limit:N2}
Exceeded By: LKR {(currentTotal - limit):N2}

Please review the petty cash entries and take appropriate action.

Best regards,
Petty Cash Management System
Ceylon Electricity Board - Haliela

---
This is an automated message. Please do not reply to this email.
"
        Return SendEmail(recipientEmail, subject, body)
    End Function

    Private Function SendHighValueEmail(recipientEmail As String, billNo As String, amount As Decimal, monthName As String) As Boolean
        Dim subject = $"[WARNING] High-Value Bill Entered - {billNo}"
        Dim body = $"
Dear Supervisor,

This is an automated notification from the Petty Cash Management System.

WARNING: A high-value bill has been entered.

Bill Number: {billNo}
Amount: LKR {amount:N2}
Month: {monthName}

This is the 2nd or subsequent bill exceeding LKR 3,000 this month.
No action is required, this is for your information only.

Best regards,
Petty Cash Management System
Ceylon Electricity Board - Haliela

---
This is an automated message. Please do not reply to this email.
"
        Return SendEmail(recipientEmail, subject, body)
    End Function

    Private Function SendMonthlyReportEmail(recipientEmail As String, report As MonthlyReportDTO) As Boolean
        Dim subject = $"Petty Cash Monthly Report - {report.MonthName}"
        
        Dim categoryBreakdown = ""
        If report.CategorySummaries IsNot Nothing Then
            categoryBreakdown = String.Join(Environment.NewLine, 
                report.CategorySummaries.Select(Function(c) $"  • {c.CategoryCode} ({c.CategoryName}): LKR {c.Total:N2}"))
        End If
        
        Dim body = $"
Dear Supervisor,

Please find below the monthly petty cash summary for {report.MonthName}.

═══════════════════════════════════════════════
MONTHLY SUMMARY
═══════════════════════════════════════════════

Total Expenses: LKR {report.GrandTotal:N2}
Total Bills: {report.TotalBillCount}
Remaining Balance: LKR {report.RemainingBalance:N2}

CATEGORY BREAKDOWN:
{categoryBreakdown}

═══════════════════════════════════════════════

Please log into the Petty Cash Management System to view the full report and print for submission.

Best regards,
Petty Cash Management System
Ceylon Electricity Board - Haliela

---
This is an automated message. Please do not reply to this email.
"
        Return SendEmail(recipientEmail, subject, body)
    End Function

    Private Function SendEmail(toAddress As String, subject As String, body As String) As Boolean
        If String.IsNullOrEmpty(_senderEmail) OrElse String.IsNullOrEmpty(_senderPassword) Then
            Return False
        End If

        Try
            Using client As New SmtpClient(_smtpHost, _smtpPort)
                client.EnableSsl = True
                client.Credentials = New System.Net.NetworkCredential(_senderEmail, _senderPassword)
                client.DeliveryMethod = SmtpDeliveryMethod.Network

                Using message As New MailMessage()
                    message.From = New MailAddress(_senderEmail, "Petty Cash System")
                    message.To.Add(toAddress)
                    message.Subject = subject
                    message.Body = body
                    message.IsBodyHtml = False

                    client.Send(message)
                End Using
            End Using

            Return True

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"Email send failed: {ex.Message}")
            Return False
        End Try
    End Function

#End Region

End Class

''' <summary>
''' Result of a notification operation.
''' </summary>
Public Class NotificationResult
    Public Property EmailSent As Boolean = False
    Public Property WhatsAppSent As Boolean = False

    Public ReadOnly Property AnySent As Boolean
        Get
            Return EmailSent OrElse WhatsAppSent
        End Get
    End Property

    Public ReadOnly Property Summary As String
        Get
            If Not AnySent Then Return "No notifications sent"
            Dim channels As New List(Of String)
            If EmailSent Then channels.Add("Email")
            If WhatsAppSent Then channels.Add("WhatsApp")
            Return $"Sent via: {String.Join(", ", channels)}"
        End Get
    End Property
End Class
