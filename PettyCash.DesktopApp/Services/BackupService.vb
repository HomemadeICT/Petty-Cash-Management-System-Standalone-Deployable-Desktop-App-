' ============================================================================
' BackupService.vb - SQLite Database Backup & Restore Service
' Petty Cash Management System
' ============================================================================

Imports System.IO
Imports System.Data.SQLite

Public Class BackupService

    ''' <summary>
    ''' Backs up the SQLite database file to the specified destination.
    ''' </summary>
    Public Function CreateBackup(destinationPath As String) As OperationResult
        Dim result As New OperationResult()
        
        Try
            Dim sourcePath = DbContext.DatabaseFilePath

            If Not File.Exists(sourcePath) Then
                result.IsSuccess = False
                result.ErrorMessage = "Source database file not found."
                Return result
            End If

            ' Perform simple file copy
            File.Copy(sourcePath, destinationPath, True)

            result.IsSuccess = True
            result.Message = "Backup created successfully."
        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "Backup failed: " & ex.Message
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Compatible alias for CreateBackup.
    ''' </summary>
    Public Function BackupDatabase(destinationPath As String) As OperationResult
        Return CreateBackup(destinationPath)
    End Function

    ''' <summary>
    ''' Restores the database from a backup file.
    ''' </summary>
    Public Function RestoreBackup(backupPath As String) As OperationResult
        Dim result As New OperationResult()

        Try
            If Not File.Exists(backupPath) Then
                result.IsSuccess = False
                result.ErrorMessage = "Backup file not found."
                Return result
            End If

            Dim targetPath = DbContext.DatabaseFilePath

            ' Copy backup to target
            File.Copy(backupPath, targetPath, True)

            result.IsSuccess = True
            result.Message = "Database restored successfully."
        Catch ex As Exception
            result.IsSuccess = False
            result.ErrorMessage = "Restore failed: " & ex.Message
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Gets the default folder for database backups.
    ''' </summary>
    Public Function GetDefaultBackupFolder() As String
        Dim folder = Path.Combine(DbContext.DataDirectory, "Backups")
        If Not Directory.Exists(folder) Then Directory.CreateDirectory(folder)
        Return folder
    End Function

    ''' <summary>
    ''' Generates a default filename for a new backup.
    ''' </summary>
    Public Function GetDefaultBackupFileName() As String
        Return $"PettyCash_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.sqlite_bak"
    End Function

    ''' <summary>
    ''' Lists all backup files in the specified folder.
    ''' </summary>
    Public Function ListBackups(Optional folderPath As String = Nothing) As List(Of FileInfo)
        Dim folder = If(folderPath, GetDefaultBackupFolder())
        If Not Directory.Exists(folder) Then Return New List(Of FileInfo)()
        
        Dim di As New DirectoryInfo(folder)
        Return di.GetFiles("*.sqlite_bak").OrderByDescending(Function(f) f.LastWriteTime).ToList()
    End Function

End Class
