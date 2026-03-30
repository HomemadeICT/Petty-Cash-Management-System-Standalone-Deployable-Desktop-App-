' ============================================================================
' Constants.vb - Application Constants
' Petty Cash Management System
' ============================================================================
' Purpose: Defines all application-wide constants
' Layer: Utilities
' ============================================================================

''' <summary>
''' Application-wide constants.
''' </summary>
Public Class Constants

#Region "Application Info"
    Public Const APP_NAME As String = "Petty Cash Management System"
    Public Const APP_VERSION As String = "1.0.0"
    Public Const ORGANIZATION_NAME As String = "Ceylon Electricity Board - Haliela"
#End Region

#Region "Business Rules"
    ''' <summary>BR1: Total petty cash allowable per month for the location.</summary>
    Public Const MONTHLY_LIMIT As Decimal = 25000D

    ''' <summary>BR2: Maximum amount allowed for a single bill receipt.</summary>
    Public Const SINGLE_BILL_LIMIT As Decimal = 5000D

    ''' <summary>BR4: Threshold at which a bill is considered high-value (warning only).</summary>
    Public Const HIGH_VALUE_THRESHOLD As Decimal = 3000D

    ''' <summary>BR8: Minimum characters required for the expense description.</summary>
    Public Const MIN_DESCRIPTION_LENGTH As Integer = 10
#End Region

#Region "Session"
    Public Const SESSION_TIMEOUT_MINUTES As Integer = 30
    Public Const MAX_LOGIN_ATTEMPTS As Integer = 5
#End Region

#Region "Roles"
    Public Const ROLE_ADMIN As String = "Admin"
    Public Const ROLE_CLERK As String = "Clerk"
#End Region

#Region "Categories"
    Public Const CATEGORY_E5200 As String = "E5200"  ' Vehicle Parts
    Public Const CATEGORY_E5300 As String = "E5300"  ' Office Items
    Public Const CATEGORY_E7800 As String = "E7800"  ' Physical Hardware
    Public Const CATEGORY_E7510 As String = "E7510"  ' Treatments & Staff
#End Region

#Region "Date Formats"
    Public Const DATE_FORMAT As String = "dd/MM/yyyy"
    Public Const DATETIME_FORMAT As String = "dd/MM/yyyy HH:mm"
    Public Const MONTH_YEAR_FORMAT As String = "MMMM yyyy"
#End Region

#Region "Currency"
    Public Const CURRENCY_SYMBOL As String = "LKR"
    Public Const CURRENCY_FORMAT As String = "N2"
#End Region

End Class
