' ============================================================================
' FormIconHelper.vb - Form Icon Assignment Utility
' Petty Cash Management System
' ============================================================================
' Purpose: Assigns meaningful Windows shell icons to each form.
'          Uses shell32.dll / imageres.dll icons — no external .ico files needed.
' ============================================================================

Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

''' <summary>
''' Extracts built-in Windows icons from system DLLs for use on forms.
''' </summary>
Public Class FormIconHelper

#Region "Win32 Import"

    <DllImport("shell32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function ExtractIconEx(
        lpszFile As String,
        nIconIndex As Integer,
        ByRef phiconLarge As IntPtr,
        ByRef phiconSmall As IntPtr,
        nIcons As UInteger) As UInteger
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function DestroyIcon(hIcon As IntPtr) As Boolean
    End Function

#End Region

#Region "Icon Index Constants (shell32.dll)"

    ' Common shell32.dll icon indices (Windows 10/11)
    Public Enum FormType
        Login           ' Lock / Key
        Dashboard       ' Home / Computer
        ExpenseEntry    ' Money / Document
        Report          ' Printer
        Settings        ' Gear / Settings
        UserManagement  ' Group of people
        UserEdit        ' Person / Edit
        Permissions     ' Shield / Security
        Backup          ' Save / Database
        Categories      ' Tag / List
        MonthPicker     ' Calendar
    End Enum

    ' shell32.dll icon indices for each purpose
    Private Shared ReadOnly _shell32Icons As New Dictionary(Of FormType, Integer) From {
        {FormType.Login,          47},  ' Lock
        {FormType.Dashboard,      15},  ' Computer/Monitor
        {FormType.ExpenseEntry,   132}, ' Document with lines
        {FormType.Report,         17},  ' Printer
        {FormType.Settings,       21},  ' Settings gear
        {FormType.UserManagement, 112}, ' Group of users
        {FormType.UserEdit,       265}, ' Single person
        {FormType.Permissions,    48},  ' Lock/Shield
        {FormType.Backup,         135}, ' Floppy/Save
        {FormType.Categories,     277}, ' Folder with tag
        {FormType.MonthPicker,    316}  ' Calendar
    }

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Applies the appropriate icon to the given form.
    ''' Dashboard uses the application's own embedded PettyCash icon.
    ''' All other forms use shell32 system icons.
    ''' Falls back gracefully if extraction fails.
    ''' </summary>
    Public Shared Sub ApplyIcon(form As Form, formType As FormType)
        ' Attempt to use the official PettyCash icon from the application EXE as the primary icon.
        ' This ensures consistent taskbar branding and satisfies the "default icon" requirement.
        Try
            Dim appIcon As Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)
            If appIcon IsNot Nothing Then
                form.Icon = appIcon
                ' If this is specifically for a sub-form that should have its own shell icon 
                ' and ONLY show the app icon in the taskbar, we would handle that differently.
                ' But for this requirement, we use the brand icon everywhere.
                Return
            End If
        Catch
            ' Silently fall through to shell32 icons if extraction fails
        End Try

        ' Fallback to shell32 icons (original behavior) if the primary brand icon is unavailable
        Try
            Dim icon As Icon = ExtractShell32Icon(_shell32Icons(formType))
            If icon IsNot Nothing Then
                form.Icon = icon
            End If
        Catch
            ' Silently fall back — icon is cosmetic only
        End Try
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' Extracts a single icon from shell32.dll by index.
    ''' </summary>
    Private Shared Function ExtractShell32Icon(index As Integer) As Icon
        Dim shell32Path As String = IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.System),
            "shell32.dll")

        Dim hLarge As IntPtr = IntPtr.Zero
        Dim hSmall As IntPtr = IntPtr.Zero

        Try
            Dim count = ExtractIconEx(shell32Path, index, hLarge, hSmall, 1)

            If count > 0 AndAlso hLarge <> IntPtr.Zero Then
                ' Clone the icon so we can destroy the handle safely
                Dim icon As Icon = CType(Icon.FromHandle(hLarge).Clone(), Icon)
                Return icon
            End If
        Finally
            If hLarge <> IntPtr.Zero Then DestroyIcon(hLarge)
            If hSmall <> IntPtr.Zero Then DestroyIcon(hSmall)
        End Try

        Return Nothing
    End Function

#End Region

End Class
