' ============================================================================
' SinhalaMonthSettings.vb - Sinhala Month Name Settings Helper
' Petty Cash Management System
' ============================================================================
' Purpose: Provides editable Sinhala month names used in reports and exports.
'          Names are stored in a plain-text file (one per line) located at:
'          %APPDATA%\PettyCashCEB\sinhala_months.txt
'          Falls back to built-in defaults if the file does not exist.
' Layer:   Utilities (Shared/Static)
' ============================================================================

Imports System.IO

''' <summary>
''' Manages Sinhala month names that appear in printed reports and XLSX exports.
''' Names can be customised by the user and are persisted between sessions.
''' </summary>
Public Class SinhalaMonthSettings

#Region "Constants"
    Private Const SETTINGS_FOLDER As String = "PettyCashCEB"
    Private Const SETTINGS_FILE As String = "sinhala_months.txt"
#End Region

#Region "Default Month Names"
    ''' <summary>
    ''' Built-in default Sinhala month names (January … December).
    ''' Used when the user has not saved custom names yet.
    ''' </summary>
    Private Shared ReadOnly DefaultMonths() As String = {
        "ජනවාරි",       ' January
        "පෙබරවාරි",     ' February
        "මාර්තු",       ' March
        "අප්‍රේල්",    ' April
        "මැයි",         ' May
        "ජූනි",         ' June
        "ජූලි",         ' July
        "අගෝස්තු",     ' August
        "සැප්තැම්බර්",  ' September
        "ඔක්තෝබර්",    ' October
        "නොවැම්බර්",    ' November
        "දෙසැම්බර්"     ' December
    }
#End Region

#Region "Public API"

    ''' <summary>
    ''' Returns the Sinhala month name for the given month number (1 = January).
    ''' </summary>
    Public Shared Function GetMonthName(month As Integer) As String
        Dim names = GetAllMonthNames()
        If month >= 1 AndAlso month <= 12 Then
            Return names(month - 1)
        End If
        Return "Unknown"
    End Function

    ''' <summary>
    ''' Returns all 12 Sinhala month names in a String array (index 0 = January).
    ''' </summary>
    Public Shared Function GetAllMonthNames() As String()
        Try
            Dim filePath = GetSettingsFilePath()
            If File.Exists(filePath) Then
                Dim lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8)
                If lines.Length >= 12 Then
                    ' Return exactly 12 names; trim whitespace from each
                    Dim names(11) As String
                    For i = 0 To 11
                        names(i) = If(String.IsNullOrWhiteSpace(lines(i)), DefaultMonths(i), lines(i).Trim())
                    Next
                    Return names
                End If
            End If
        Catch
            ' Fall through to defaults on any IO error
        End Try

        ' Return a copy of the defaults so callers can mutate freely
        Dim defaults(11) As String
        Array.Copy(DefaultMonths, defaults, 12)
        Return defaults
    End Function

    ''' <summary>
    ''' Persists the given 12 Sinhala month names to the settings file.
    ''' </summary>
    ''' <param name="names">Array of exactly 12 names (index 0 = January).</param>
    Public Shared Sub SaveMonthNames(names() As String)
        If names Is Nothing OrElse names.Length < 12 Then
            Throw New ArgumentException("Exactly 12 month names are required.")
        End If

        Dim filePath = GetSettingsFilePath()
        Dim dir = Path.GetDirectoryName(filePath)
        If Not Directory.Exists(dir) Then
            Directory.CreateDirectory(dir)
        End If

        File.WriteAllLines(filePath, names, System.Text.Encoding.UTF8)
    End Sub

    ''' <summary>
    ''' Resets the stored names to the built-in defaults by deleting the settings file.
    ''' </summary>
    Public Shared Sub ResetToDefaults()
        Try
            Dim filePath = GetSettingsFilePath()
            If File.Exists(filePath) Then File.Delete(filePath)
        Catch
            ' Ignore errors on reset
        End Try
    End Sub

#End Region

#Region "Private Helpers"

    Private Shared Function GetSettingsFilePath() As String
        Dim appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Return Path.Combine(appData, SETTINGS_FOLDER, SETTINGS_FILE)
    End Function

#End Region

End Class
