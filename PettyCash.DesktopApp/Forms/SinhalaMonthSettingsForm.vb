' ============================================================================
' SinhalaMonthSettingsForm.vb - Sinhala Month Names Settings Form Code-Behind
' Petty Cash Management System
' ============================================================================
' Purpose: Lets the user view and edit the Sinhala month names that appear
'          in printed reports and Excel exports.
' ============================================================================

Imports System.Windows.Forms

Public Class SinhalaMonthSettingsForm

#Region "Private Fields"
    ' Ordered array of the 12 text-boxes (index 0 = January)
    Private ReadOnly _monthBoxes() As TextBox
#End Region

#Region "Constructor"

    Public Sub New()
        InitializeComponent()

        ' Build the ordered array once — same order as the Designer
        _monthBoxes = {
            txtM1, txtM2, txtM3, txtM4, txtM5, txtM6,
            txtM7, txtM8, txtM9, txtM10, txtM11, txtM12
        }
    End Sub

#End Region

#Region "Form Events"

    Private Sub SinhalaMonthSettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormIconHelper.ApplyIcon(Me, FormIconHelper.FormType.MonthPicker)
        LoadCurrentNames()
    End Sub

#End Region

#Region "Load / Save"

    Private Sub LoadCurrentNames()
        Dim names = SinhalaMonthSettings.GetAllMonthNames()
        For i = 0 To 11
            _monthBoxes(i).Text = names(i)
        Next
    End Sub

    Private Function CollectNames() As String()
        Dim names(11) As String
        For i = 0 To 11
            names(i) = _monthBoxes(i).Text.Trim()
        Next
        Return names
    End Function

#End Region

#Region "Button Events"

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validate — no blank names allowed
        Dim names = CollectNames()
        For i = 0 To 11
            If String.IsNullOrWhiteSpace(names(i)) Then
                MessageBox.Show($"Month {i + 1} name cannot be blank.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _monthBoxes(i).Focus()
                Return
            End If
        Next

        Try
            SinhalaMonthSettings.SaveMonthNames(names)
            MessageBox.Show("Sinhala month names saved successfully!" & Environment.NewLine &
                            "The new names will appear in all new reports and exports.",
                            "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Catch ex As Exception
            MessageBox.Show($"Error saving month names: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If MessageBox.Show("Reset all month names to the built-in defaults?",
                           "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            SinhalaMonthSettings.ResetToDefaults()
            LoadCurrentNames()   ' Reload the default values into the boxes
            MessageBox.Show("Month names reset to defaults.", "Reset",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

#End Region

End Class
