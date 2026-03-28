' ============================================================================
' ConfigManager.vb - Configuration Manager
' Petty Cash Management System
' ============================================================================
' Purpose: Manages application configuration and settings
' Layer: Utilities
' ============================================================================

Imports System.Configuration

''' <summary>
''' Manages application configuration settings.
''' </summary>
Public Class ConfigManager

#Region "Public Methods"

    ''' <summary>
    ''' Gets an application setting by key.
    ''' </summary>
    ''' <param name="key">The setting key.</param>
    ''' <param name="defaultValue">Default value if not found.</param>
    Public Shared Function GetSetting(key As String, Optional defaultValue As String = "") As String
        Dim value = ConfigurationManager.AppSettings(key)
        Return If(String.IsNullOrEmpty(value), defaultValue, value)
    End Function

    ''' <summary>
    ''' Gets a connection string by name.
    ''' </summary>
    Public Shared Function GetConnectionString(name As String) As String
        Return ConfigurationManager.ConnectionStrings(name)?.ConnectionString
    End Function

    ''' <summary>
    ''' Gets the application version.
    ''' </summary>
    Public Shared Function GetVersion() As String
        Return GetSetting("AppVersion", "1.0.0")
    End Function

    ''' <summary>
    ''' Gets the application name.
    ''' </summary>
    Public Shared Function GetAppName() As String
        Return GetSetting("AppName", "Petty Cash Management System")
    End Function

    ''' <summary>
    ''' Gets the organization name.
    ''' </summary>
    Public Shared Function GetOrganizationName() As String
        Return GetSetting("OrganizationName", "Ceylon Electricity Board - Haliela")
    End Function

#End Region

End Class
