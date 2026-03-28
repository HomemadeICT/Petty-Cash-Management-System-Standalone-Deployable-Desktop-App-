' ============================================================================
' WhatsAppService.vb - WhatsApp Notification Service
' Petty Cash Management System
' ============================================================================
' Purpose: Sends WhatsApp notifications via Twilio API
' Layer: Business Logic Layer
' Dependencies: ConfigManager, Twilio NuGet package (optional)
' ============================================================================

Imports System.Net
Imports System.Net.Http
Imports System.Text

''' <summary>
''' Sends WhatsApp notifications via Twilio API.
''' </summary>
Public Class WhatsAppService

#Region "Private Fields"
    Private ReadOnly _accountSid As String
    Private ReadOnly _authToken As String
    Private ReadOnly _fromNumber As String
    Private ReadOnly _adminNumber As String
    Private ReadOnly _isEnabled As Boolean
    Private ReadOnly _httpClient As HttpClient
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Initializes the WhatsApp service with config settings.
    ''' </summary>
    Public Sub New()
        ' Load configuration from App.config
        _accountSid = ConfigManager.GetSetting("TwilioAccountSid", "")
        _authToken = ConfigManager.GetSetting("TwilioAuthToken", "")
        _fromNumber = ConfigManager.GetSetting("TwilioWhatsAppNumber", "")
        _adminNumber = ConfigManager.GetSetting("AdminWhatsAppNumber", "")
        _isEnabled = Boolean.Parse(ConfigManager.GetSetting("WhatsAppNotificationsEnabled", "False"))

        ' Initialize HTTP client for Twilio API
        _httpClient = New HttpClient()
    End Sub
#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Sends a notification when monthly limit is exceeded.
    ''' </summary>
    Public Function SendOveruseNotification(monthName As String, currentTotal As Decimal, limit As Decimal) As Boolean
        If Not CanSend() Then Return False

        Dim message = $"🚨 *PETTY CASH ALERT*{vbCrLf}" &
                      $"Monthly limit exceeded!{vbCrLf}{vbCrLf}" &
                      $"📅 Month: {monthName}{vbCrLf}" &
                      $"💰 Spent: LKR {currentTotal:N2}{vbCrLf}" &
                      $"📊 Limit: LKR {limit:N2}{vbCrLf}" &
                      $"⚠️ Exceeded: LKR {(currentTotal - limit):N2}{vbCrLf}{vbCrLf}" &
                      $"_CEB Haliela - Petty Cash System_"

        Return SendWhatsAppMessage(_adminNumber, message)
    End Function

    ''' <summary>
    ''' Sends a warning for high-value bills.
    ''' </summary>
    Public Function SendHighValueWarning(billNo As String, amount As Decimal, monthName As String) As Boolean
        If Not CanSend() Then Return False

        Dim message = $"⚠️ *HIGH VALUE BILL*{vbCrLf}" &
                      $"A bill over LKR 3,000 entered{vbCrLf}{vbCrLf}" &
                      $"📄 Bill No: {billNo}{vbCrLf}" &
                      $"💰 Amount: LKR {amount:N2}{vbCrLf}" &
                      $"📅 Month: {monthName}{vbCrLf}{vbCrLf}" &
                      $"_This is the 2nd+ high-value bill this month_"

        Return SendWhatsAppMessage(_adminNumber, message)
    End Function

    ''' <summary>
    ''' Sends the monthly summary report.
    ''' </summary>
    Public Function SendMonthlyReport(report As MonthlyReportDTO) As Boolean
        If Not CanSend() Then Return False

        Dim categories = ""
        If report.CategorySummaries IsNot Nothing Then
            For Each cat In report.CategorySummaries
                categories &= $"• {cat.CategoryCode}: LKR {cat.Total:N2}{vbCrLf}"
            Next
        End If

        Dim message = $"📊 *MONTHLY REPORT*{vbCrLf}" &
                      $"{report.MonthName} {report.Year}{vbCrLf}{vbCrLf}" &
                      $"💰 Total Spent: LKR {report.GrandTotal:N2}{vbCrLf}" &
                      $"📋 Total Bills: {report.TotalBillCount}{vbCrLf}" &
                      $"💵 Remaining: LKR {report.RemainingBalance:N2}{vbCrLf}{vbCrLf}" &
                      $"*By Category:*{vbCrLf}" &
                      categories &
                      $"{vbCrLf}_CEB Haliela - Petty Cash System_"

        Return SendWhatsAppMessage(_adminNumber, message)
    End Function

    ''' <summary>
    ''' Checks if WhatsApp service is enabled and configured.
    ''' </summary>
    Public Function IsConfigured() As Boolean
        Return _isEnabled AndAlso
               Not String.IsNullOrEmpty(_accountSid) AndAlso
               Not String.IsNullOrEmpty(_authToken) AndAlso
               Not String.IsNullOrEmpty(_fromNumber) AndAlso
               Not String.IsNullOrEmpty(_adminNumber)
    End Function

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Checks if we can send messages.
    ''' </summary>
    Private Function CanSend() As Boolean
        Return IsConfigured()
    End Function

    ''' <summary>
    ''' Sends a WhatsApp message via Twilio API.
    ''' </summary>
    Private Function SendWhatsAppMessage(toNumber As String, message As String) As Boolean
        If Not CanSend() Then Return False

        Try
            ' Twilio API endpoint
            Dim url = $"https://api.twilio.com/2010-04-01/Accounts/{_accountSid}/Messages.json"

            ' Format numbers for WhatsApp
            Dim fromWhatsApp = $"whatsapp:{_fromNumber}"
            Dim toWhatsApp = $"whatsapp:{toNumber}"

            ' Create form data
            Dim content As New FormUrlEncodedContent(New Dictionary(Of String, String) From {
                {"From", fromWhatsApp},
                {"To", toWhatsApp},
                {"Body", message}
            })

            ' Add Basic Auth header
            Dim authBytes = Encoding.ASCII.GetBytes($"{_accountSid}:{_authToken}")
            Dim authHeader = Convert.ToBase64String(authBytes)
            _httpClient.DefaultRequestHeaders.Clear()
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {authHeader}")

            ' Send request
            Dim response = _httpClient.PostAsync(url, content).Result

            If response.IsSuccessStatusCode Then
                System.Diagnostics.Debug.WriteLine($"WhatsApp sent successfully to {toNumber}")
                Return True
            Else
                Dim errorContent = response.Content.ReadAsStringAsync().Result
                System.Diagnostics.Debug.WriteLine($"WhatsApp send failed: {response.StatusCode} - {errorContent}")
                Return False
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine($"WhatsApp send error: {ex.Message}")
            Return False
        End Try
    End Function

#End Region

End Class
