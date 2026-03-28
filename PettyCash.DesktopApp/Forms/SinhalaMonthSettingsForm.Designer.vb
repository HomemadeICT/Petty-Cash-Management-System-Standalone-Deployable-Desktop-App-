' ============================================================================
' SinhalaMonthSettingsForm.Designer.vb - Sinhala Month Names Settings Form (Designer)
' Petty Cash Management System
' ============================================================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SinhalaMonthSettingsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSubTitle = New System.Windows.Forms.Label()
        Me.pnlContent = New System.Windows.Forms.Panel()
        Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblM1 = New System.Windows.Forms.Label()
        Me.txtM1 = New System.Windows.Forms.TextBox()
        Me.lblM2 = New System.Windows.Forms.Label()
        Me.txtM2 = New System.Windows.Forms.TextBox()
        Me.lblM3 = New System.Windows.Forms.Label()
        Me.txtM3 = New System.Windows.Forms.TextBox()
        Me.lblM4 = New System.Windows.Forms.Label()
        Me.txtM4 = New System.Windows.Forms.TextBox()
        Me.lblM5 = New System.Windows.Forms.Label()
        Me.txtM5 = New System.Windows.Forms.TextBox()
        Me.lblM6 = New System.Windows.Forms.Label()
        Me.txtM6 = New System.Windows.Forms.TextBox()
        Me.lblM7 = New System.Windows.Forms.Label()
        Me.txtM7 = New System.Windows.Forms.TextBox()
        Me.lblM8 = New System.Windows.Forms.Label()
        Me.txtM8 = New System.Windows.Forms.TextBox()
        Me.lblM9 = New System.Windows.Forms.Label()
        Me.txtM9 = New System.Windows.Forms.TextBox()
        Me.lblM10 = New System.Windows.Forms.Label()
        Me.txtM10 = New System.Windows.Forms.TextBox()
        Me.lblM11 = New System.Windows.Forms.Label()
        Me.txtM11 = New System.Windows.Forms.TextBox()
        Me.lblM12 = New System.Windows.Forms.Label()
        Me.txtM12 = New System.Windows.Forms.TextBox()
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.tableLayoutPanel1.SuspendLayout()
        Me.pnlButtons.SuspendLayout()
        Me.SuspendLayout()

        ' ── pnlHeader ─────────────────────────────────────────────────────────
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.pnlHeader.Controls.Add(Me.lblSubTitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Height = 70
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Size = New System.Drawing.Size(400, 70)

        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(12, 10)
        Me.lblTitle.Text = "සිංහල මස් නම් සංස්කරණය"

        Me.lblSubTitle.AutoSize = True
        Me.lblSubTitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(200, 220, 255)
        Me.lblSubTitle.Location = New System.Drawing.Point(14, 42)
        Me.lblSubTitle.Text = "Edit Sinhala month names used in reports and Excel exports"

        ' ── pnlContent ────────────────────────────────────────────────────────
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.Controls.Add(Me.tableLayoutPanel1)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 70)
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(12, 10, 12, 0)

        ' ── tableLayoutPanel1 ─────────────────────────────────────────────────
        Me.tableLayoutPanel1.AutoSize = True
        Me.tableLayoutPanel1.ColumnCount = 4
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85))   ' English label
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50))    ' TextBox L
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85))   ' English label R
        Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50))    ' TextBox R
        Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.tableLayoutPanel1.Location = New System.Drawing.Point(12, 10)
        Me.tableLayoutPanel1.RowCount = 6

        ' Row heights
        For r = 0 To 5
            Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40))
        Next

        ' Helper: add a label
        Dim colNames() As String = {"January", "February", "March", "April", "May", "June",
                                     "July", "August", "September", "October", "November", "December"}
        Dim lblControls() As Label = {Me.lblM1, Me.lblM2, Me.lblM3, Me.lblM4, Me.lblM5, Me.lblM6,
                                       Me.lblM7, Me.lblM8, Me.lblM9, Me.lblM10, Me.lblM11, Me.lblM12}
        Dim txtControls() As TextBox = {Me.txtM1, Me.txtM2, Me.txtM3, Me.txtM4, Me.txtM5, Me.txtM6,
                                         Me.txtM7, Me.txtM8, Me.txtM9, Me.txtM10, Me.txtM11, Me.txtM12}

        For i = 0 To 11
            Dim row = i \ 2
            Dim startCol = (i Mod 2) * 2

            ' Label
            lblControls(i).AutoSize = False
            lblControls(i).Dock = DockStyle.Fill
            lblControls(i).Font = New System.Drawing.Font("Segoe UI", 9.5!)
            lblControls(i).Text = $"{i + 1}. {colNames(i)}"
            lblControls(i).TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.tableLayoutPanel1.Controls.Add(lblControls(i), startCol, row)

            ' TextBox
            txtControls(i).Dock = DockStyle.Fill
            txtControls(i).Font = New System.Drawing.Font("Iskoola Pota", 11.0!)
            txtControls(i).Margin = New System.Windows.Forms.Padding(0, 4, 8, 4)
            Me.tableLayoutPanel1.Controls.Add(txtControls(i), startCol + 1, row)
        Next

        ' ── pnlButtons ────────────────────────────────────────────────────────
        Me.pnlButtons.BackColor = System.Drawing.Color.FromArgb(245, 245, 248)
        Me.pnlButtons.Controls.Add(Me.btnReset)
        Me.pnlButtons.Controls.Add(Me.btnCancel)
        Me.pnlButtons.Controls.Add(Me.btnSave)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButtons.Height = 55
        Me.pnlButtons.Padding = New System.Windows.Forms.Padding(8, 8, 8, 8)

        ' Save button
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right Or System.Windows.Forms.AnchorStyles.Top
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 122, 204)
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(288, 10)
        Me.btnSave.Size = New System.Drawing.Size(96, 34)
        Me.btnSave.Text = "💾 Save"

        ' Cancel button
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right Or System.Windows.Forms.AnchorStyles.Top
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(108, 117, 125)
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(188, 10)
        Me.btnCancel.Size = New System.Drawing.Size(96, 34)
        Me.btnCancel.Text = "Cancel"

        ' Reset button
        Me.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Top
        Me.btnReset.BackColor = System.Drawing.Color.FromArgb(220, 53, 69)
        Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReset.FlatAppearance.BorderSize = 0
        Me.btnReset.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnReset.ForeColor = System.Drawing.Color.White
        Me.btnReset.Location = New System.Drawing.Point(8, 10)
        Me.btnReset.Size = New System.Drawing.Size(120, 34)
        Me.btnReset.Text = "↩ Reset Defaults"

        ' ── Form ──────────────────────────────────────────────────────────────
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(400, 370)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Sinhala Month Names"

        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.pnlContent.PerformLayout()
        Me.tableLayoutPanel1.ResumeLayout(False)
        Me.tableLayoutPanel1.PerformLayout()
        Me.pnlButtons.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblSubTitle As System.Windows.Forms.Label
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblM1 As System.Windows.Forms.Label
    Friend WithEvents txtM1 As System.Windows.Forms.TextBox
    Friend WithEvents lblM2 As System.Windows.Forms.Label
    Friend WithEvents txtM2 As System.Windows.Forms.TextBox
    Friend WithEvents lblM3 As System.Windows.Forms.Label
    Friend WithEvents txtM3 As System.Windows.Forms.TextBox
    Friend WithEvents lblM4 As System.Windows.Forms.Label
    Friend WithEvents txtM4 As System.Windows.Forms.TextBox
    Friend WithEvents lblM5 As System.Windows.Forms.Label
    Friend WithEvents txtM5 As System.Windows.Forms.TextBox
    Friend WithEvents lblM6 As System.Windows.Forms.Label
    Friend WithEvents txtM6 As System.Windows.Forms.TextBox
    Friend WithEvents lblM7 As System.Windows.Forms.Label
    Friend WithEvents txtM7 As System.Windows.Forms.TextBox
    Friend WithEvents lblM8 As System.Windows.Forms.Label
    Friend WithEvents txtM8 As System.Windows.Forms.TextBox
    Friend WithEvents lblM9 As System.Windows.Forms.Label
    Friend WithEvents txtM9 As System.Windows.Forms.TextBox
    Friend WithEvents lblM10 As System.Windows.Forms.Label
    Friend WithEvents txtM10 As System.Windows.Forms.TextBox
    Friend WithEvents lblM11 As System.Windows.Forms.Label
    Friend WithEvents txtM11 As System.Windows.Forms.TextBox
    Friend WithEvents lblM12 As System.Windows.Forms.Label
    Friend WithEvents txtM12 As System.Windows.Forms.TextBox
    Friend WithEvents pnlButtons As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
End Class
