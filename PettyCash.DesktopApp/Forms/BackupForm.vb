' ============================================================================
' BackupForm.vb - Database Backup Form Code-Behind
' Petty Cash Management System
' ============================================================================
' Purpose: UI for performing database backup operations
' ============================================================================

Imports System.IO
Imports System.Windows.Forms

Public Class BackupForm

#Region "Private Fields"
    Private _backupService As BackupService
#End Region

#Region "Form Events"

    Private Sub BackupForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _backupService = New BackupService()

        ' Set default backup folder
        Dim defaultFolder = _backupService.GetDefaultBackupFolder()
        txtBackupFolder.Text = defaultFolder

        ' Refresh backup list
        RefreshBackupList()
    End Sub

#End Region

#Region "Browse & Backup"

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Using fbd As New FolderBrowserDialog()
            fbd.Description = "Select folder to save database backup"
            fbd.SelectedPath = txtBackupFolder.Text

            If fbd.ShowDialog() = DialogResult.OK Then
                txtBackupFolder.Text = fbd.SelectedPath
            End If
        End Using
    End Sub

    Private Sub btnBackupNow_Click(sender As Object, e As EventArgs) Handles btnBackupNow.Click
        If String.IsNullOrWhiteSpace(txtBackupFolder.Text) Then
            MessageBox.Show("Please select a backup folder first.", "No Folder Selected",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Build full file path
        Dim fileName = _backupService.GetDefaultBackupFileName()
        Dim fullPath = Path.Combine(txtBackupFolder.Text, fileName)

        ' Disable controls during backup
        btnBackupNow.Enabled = False
        btnBackupNow.Text = "Backing up..."
        lblStatus.Text = "⏳ Backup in progress, please wait..."
        lblStatus.ForeColor = Color.DarkOrange
        Application.DoEvents()

        Try
            Dim result = _backupService.BackupDatabase(fullPath)

            If result.IsSuccess Then
                lblStatus.Text = $"✅ Backup completed: {fileName}"
                lblStatus.ForeColor = Color.DarkGreen
                lstBackups.Items.Insert(0, $"[{DateTime.Now:dd/MM/yyyy HH:mm}]  {fileName}")

                ' Ask if user wants to open folder
                If MessageBox.Show($"Backup completed successfully!{Environment.NewLine}{Environment.NewLine}" &
                                   $"File: {fileName}{Environment.NewLine}{Environment.NewLine}" &
                                   "Would you like to open the backup folder?",
                                   "Backup Successful", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo() With {
                        .FileName = txtBackupFolder.Text,
                        .UseShellExecute = True
                    })
                End If
            Else
                lblStatus.Text = "❌ Backup failed."
                lblStatus.ForeColor = Color.Red
                MessageBox.Show(result.ErrorMessage, "Backup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            lblStatus.Text = "❌ Unexpected error."
            lblStatus.ForeColor = Color.Red
            MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            btnBackupNow.Enabled = True
            btnBackupNow.Text = "🗄 Backup Now"
        End Try

        ' Refresh list
        RefreshBackupList()
    End Sub

#End Region

#Region "Backup List"

    Private Sub RefreshBackupList()
        lstBackups.Items.Clear()
        Dim backups = _backupService.ListBackups(txtBackupFolder.Text)

        If backups.Count = 0 Then
            lstBackups.Items.Add("(No backup files found in selected folder)")
        Else
            For Each fi In backups
                lstBackups.Items.Add($"[{fi.LastWriteTime:dd/MM/yyyy HH:mm}]  {fi.Name}  ({FormatSize(fi.Length)})")
            Next
        End If
    End Sub

    Private Sub btnRefreshList_Click(sender As Object, e As EventArgs) Handles btnRefreshList.Click
        RefreshBackupList()
    End Sub

#End Region

#Region "Helpers"

    Private Function FormatSize(bytes As Long) As String
        If bytes < 1024 Then Return $"{bytes} B"
        If bytes < 1048576 Then Return $"{bytes \ 1024} KB"
        Return $"{bytes \ 1048576} MB"
    End Function

#End Region

#Region "Close"

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

End Class
