' ============================================================================
' MonthYearPickerForm.vb - Month/Year Jump Navigation Dialog
' Petty Cash Management System
' ============================================================================
' Purpose: Allows users to jump to a specific month/year to edit past records
' ============================================================================

Imports System.Windows.Forms

Public Class MonthYearPickerForm
    Inherits Form

    ' ── Public read-only results ──
    Public Property SelectedYear As Integer
    Public Property SelectedMonth As Integer

    ' ── Controls ──
    Private WithEvents cboMonth As ComboBox
    Private WithEvents nudYear As NumericUpDown
    Private WithEvents btnOK As Button
    Private WithEvents btnCancel As Button
    Private lblTitle As Label
    Private lblMonth As Label
    Private lblYear As Label

    ''' <summary>
    ''' Creates the picker pre-set to the given month/year.
    ''' </summary>
    Public Sub New(currentYear As Integer, currentMonth As Integer)
        SelectedYear = currentYear
        SelectedMonth = currentMonth
        InitializeControls()
    End Sub

    Private Sub InitializeControls()
        ' ── Form ──
        Me.Text = "Jump to Month"
        Me.Size = New Drawing.Size(320, 220)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' ── Title label ──
        lblTitle = New Label() With {
            .Text = "Select a month to navigate to:",
            .Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold),
            .ForeColor = Drawing.Color.FromArgb(0, 51, 102),
            .Location = New Drawing.Point(20, 15),
            .AutoSize = True
        }
        Me.Controls.Add(lblTitle)

        ' ── Month label ──
        lblMonth = New Label() With {
            .Text = "Month:",
            .Font = New Drawing.Font("Segoe UI", 10),
            .Location = New Drawing.Point(20, 55),
            .AutoSize = True
        }
        Me.Controls.Add(lblMonth)

        ' ── Month combo ──
        cboMonth = New ComboBox() With {
            .DropDownStyle = ComboBoxStyle.DropDownList,
            .Font = New Drawing.Font("Segoe UI", 10),
            .Location = New Drawing.Point(100, 52),
            .Size = New Drawing.Size(180, 28)
        }
        ' Add all 12 months
        For i As Integer = 1 To 12
            cboMonth.Items.Add(New Date(2000, i, 1).ToString("MMMM"))
        Next
        cboMonth.SelectedIndex = SelectedMonth - 1
        Me.Controls.Add(cboMonth)

        ' ── Year label ──
        lblYear = New Label() With {
            .Text = "Year:",
            .Font = New Drawing.Font("Segoe UI", 10),
            .Location = New Drawing.Point(20, 95),
            .AutoSize = True
        }
        Me.Controls.Add(lblYear)

        ' ── Year spinner ──
        nudYear = New NumericUpDown() With {
            .Font = New Drawing.Font("Segoe UI", 10),
            .Location = New Drawing.Point(100, 92),
            .Size = New Drawing.Size(100, 28),
            .Minimum = 2020,
            .Maximum = Date.Now.Year,
            .Value = SelectedYear
        }
        Me.Controls.Add(nudYear)

        ' ── OK Button ──
        btnOK = New Button() With {
            .Text = "Go",
            .Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold),
            .BackColor = Drawing.Color.FromArgb(0, 150, 80),
            .ForeColor = Drawing.Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Size = New Drawing.Size(90, 34),
            .Location = New Drawing.Point(100, 135)
        }
        btnOK.FlatAppearance.BorderSize = 0
        Me.Controls.Add(btnOK)

        ' ── Cancel Button ──
        btnCancel = New Button() With {
            .Text = "Cancel",
            .Font = New Drawing.Font("Segoe UI", 10),
            .FlatStyle = FlatStyle.Flat,
            .Size = New Drawing.Size(80, 34),
            .Location = New Drawing.Point(200, 135)
        }
        Me.Controls.Add(btnCancel)

        Me.AcceptButton = btnOK
        Me.CancelButton = btnCancel
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        SelectedMonth = cboMonth.SelectedIndex + 1
        SelectedYear = CInt(nudYear.Value)

        ' Don't allow navigating to future months
        Dim selectedDate = New Date(SelectedYear, SelectedMonth, 1)
        Dim currentDate = New Date(Date.Now.Year, Date.Now.Month, 1)

        If selectedDate > currentDate Then
            MessageBox.Show("Cannot navigate to a future month.", "Invalid Selection",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
