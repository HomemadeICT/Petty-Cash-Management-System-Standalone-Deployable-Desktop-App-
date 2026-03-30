' ============================================================================
' AdminSettingsForm.vb - Settings Hub
' Petty Cash Management System
' ============================================================================
' Purpose: Central settings hub showing permission-filtered shortcut tiles
'          for every setting the logged-in user is allowed to access.
' ============================================================================

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class AdminSettingsForm

#Region "Form Events"

    Private Sub AdminSettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormIconHelper.ApplyIcon(Me, FormIconHelper.FormType.Settings)

        ' Show who is logged in
        If SessionManager.CurrentUser IsNot Nothing Then
            lblUserInfo.Text = $"Logged in as: {SessionManager.CurrentUser.FullName}  ({SessionManager.CurrentUser.Role})"
        Else
            lblUserInfo.Text = "Logged in as: Guest"
        End If

        ' Build tile grid based on permissions
        BuildSettingsTiles()
    End Sub

#End Region

#Region "Tile Builder"

    ''' <summary>
    ''' Creates a list of every possible setting tile, then adds only
    ''' those the current user has permission for.
    ''' </summary>
    Private Sub BuildSettingsTiles()
        pnlContent.Controls.Clear()

        ' ----- Define all tiles -----
        Dim allTiles As New List(Of TileInfo) From {
            New TileInfo With {
                .Title = "Manage Categories",
                .Subtitle = "Add, edit or remove expense categories",
                .Emoji = "📂",
                .BackColor = Color.FromArgb(70, 130, 180),
                .PermissionKey = PermissionKeys.SETTINGS_VIEW,
                .ClickAction = Sub() OpenForm(New CategoryManagementForm())
            },
            New TileInfo With {
                .Title = "සිංහල මාස නම්",
                .Subtitle = "Edit Sinhala month name mappings",
                .Emoji = "🗓",
                .BackColor = Color.FromArgb(0, 128, 96),
                .PermissionKey = PermissionKeys.SETTINGS_VIEW,
                .ClickAction = Sub() OpenForm(New SinhalaMonthSettingsForm())
            },
            New TileInfo With {
                .Title = "Manage Users",
                .Subtitle = "Create, edit or disable user accounts",
                .Emoji = "👥",
                .BackColor = Color.FromArgb(100, 60, 160),
                .PermissionKey = PermissionKeys.USER_CREATE,
                .AltPermissionKey = PermissionKeys.USER_EDIT,
                .ClickAction = Sub() OpenForm(New UserManagementForm())
            },
            New TileInfo With {
                .Title = "Backup && Restore",
                .Subtitle = "Back up or restore the database",
                .Emoji = "🗄",
                .BackColor = Color.FromArgb(200, 120, 0),
                .PermissionKey = PermissionKeys.BACKUP_DATABASE,
                .ClickAction = Sub() OpenForm(New BackupForm())
            },
            New TileInfo With {
                .Title = "Audit Log",
                .Subtitle = "View recent system activity",
                .Emoji = "📋",
                .BackColor = Color.FromArgb(90, 90, 90),
                .PermissionKey = PermissionKeys.AUDIT_VIEW,
                .ClickAction = Sub() ShowAuditLog()
            }
        }

        ' ----- Filter by permission and add tiles -----
        For Each tile In allTiles
            Dim hasAccess As Boolean = SessionManager.HasPermission(tile.PermissionKey)
            If Not hasAccess AndAlso Not String.IsNullOrEmpty(tile.AltPermissionKey) Then
                hasAccess = SessionManager.HasPermission(tile.AltPermissionKey)
            End If

            If hasAccess Then
                pnlContent.Controls.Add(CreateTilePanel(tile))
            End If
        Next

        ' If nothing visible, show a friendly message
        If pnlContent.Controls.Count = 0 Then
            Dim lbl As New Label() With {
                .Text = "You do not have access to any settings at this time.",
                .Font = New Font("Segoe UI", 11, FontStyle.Italic),
                .ForeColor = Color.Gray,
                .AutoSize = True,
                .Padding = New Padding(10, 30, 0, 0)
            }
            pnlContent.Controls.Add(lbl)
        End If
    End Sub

#End Region

#Region "Tile UI Factory"

    ''' <summary>
    ''' Builds a single rounded, hover-animated tile panel from a TileInfo.
    ''' </summary>
    Private Function CreateTilePanel(info As TileInfo) As Panel
        Dim tileWidth As Integer = 220
        Dim tileHeight As Integer = 140

        ' --- Outer container ---
        Dim pnl As New Panel() With {
            .Size = New Size(tileWidth, tileHeight),
            .Margin = New Padding(8),
            .Cursor = Cursors.Hand,
            .Tag = info
        }

        ' Custom paint with rounded corners and gradient
        AddHandler pnl.Paint, Sub(s, pe)
                                  Dim rect = New Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1)
                                  Dim path = RoundedRect(rect, 14)
                                  pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias

                                  ' Gradient fill
                                  Using brush As New LinearGradientBrush(rect, info.BackColor,
                                      ControlPaint.Dark(info.BackColor, 0.15F), 45.0F)
                                      pe.Graphics.FillPath(brush, path)
                                  End Using

                                  ' Subtle border
                                  Using pen As New Pen(Color.FromArgb(60, Color.White), 1)
                                      pe.Graphics.DrawPath(pen, path)
                                  End Using
                              End Sub

        ' --- Emoji icon ---
        Dim lblEmoji As New Label() With {
            .Text = info.Emoji,
            .Font = New Font("Segoe UI Emoji", 26, FontStyle.Regular),
            .ForeColor = Color.White,
            .BackColor = Color.Transparent,
            .Location = New Point(16, 12),
            .AutoSize = True
        }

        ' --- Title ---
        Dim lblTileTitle As New Label() With {
            .Text = info.Title,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold),
            .ForeColor = Color.White,
            .BackColor = Color.Transparent,
            .Location = New Point(14, 68),
            .Size = New Size(tileWidth - 28, 24),
            .AutoSize = False
        }

        ' --- Subtitle ---
        Dim lblSubtitle As New Label() With {
            .Text = info.Subtitle,
            .Font = New Font("Segoe UI", 8, FontStyle.Regular),
            .ForeColor = Color.FromArgb(210, 230, 255),
            .BackColor = Color.Transparent,
            .Location = New Point(14, 94),
            .Size = New Size(tileWidth - 28, 36),
            .AutoSize = False
        }

        pnl.Controls.Add(lblEmoji)
        pnl.Controls.Add(lblTileTitle)
        pnl.Controls.Add(lblSubtitle)

        ' --- Hover effect on ALL child controls ---
        Dim wireHover = Sub(ctrl As Control)
                             AddHandler ctrl.MouseEnter, Sub(s, ea)
                                                             pnl.BackColor = Color.FromArgb(30, Color.White)
                                                             pnl.Invalidate()
                                                         End Sub
                             AddHandler ctrl.MouseLeave, Sub(s, ea)
                                                             pnl.BackColor = Color.Transparent
                                                             pnl.Invalidate()
                                                         End Sub
                             AddHandler ctrl.Click, Sub(s, ea) info.ClickAction?.Invoke()
                             ctrl.Cursor = Cursors.Hand
                         End Sub

        wireHover(pnl)
        wireHover(lblEmoji)
        wireHover(lblTileTitle)
        wireHover(lblSubtitle)

        Return pnl
    End Function

    ''' <summary>
    ''' Returns a GraphicsPath for a rectangle with rounded corners.
    ''' </summary>
    Private Shared Function RoundedRect(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        Dim d As Integer = radius * 2
        path.AddArc(rect.X, rect.Y, d, d, 180, 90)
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
        path.CloseFigure()
        Return path
    End Function

#End Region

#Region "Tile Actions"

    Private Sub OpenForm(frm As Form)
        Using frm
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ShowAuditLog()
        Try
            Dim auditRepo As New AuditLogRepository()
            Dim auditService As New AuditService(auditRepo)
            Dim logs = auditService.GetRecentActivity(50)
            Dim logText = String.Join(Environment.NewLine,
                logs.Select(Function(l) $"{l.ActionTimestamp:dd/MM/yyyy HH:mm} | {l.ActionType} | {l.Details}"))

            If String.IsNullOrWhiteSpace(logText) Then logText = "(No recent activity)"

            MessageBox.Show(logText, "Recent Audit Log (Last 50)", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"Error loading audit log: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Close Button"

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

#Region "TileInfo Helper Class"

    ''' <summary>
    ''' Data class describing a single settings tile.
    ''' </summary>
    Private Class TileInfo
        Public Property Title As String
        Public Property Subtitle As String
        Public Property Emoji As String
        Public Property BackColor As Color
        Public Property PermissionKey As String
        Public Property AltPermissionKey As String
        Public Property ClickAction As Action
    End Class

#End Region

End Class