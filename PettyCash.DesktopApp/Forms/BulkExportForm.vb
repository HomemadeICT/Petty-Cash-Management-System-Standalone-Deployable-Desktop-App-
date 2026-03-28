' ============================================================================
' BulkExportForm.vb - Bulk Export Selection Form
' Petty Cash Management System
' ============================================================================
' Purpose: Dialog for selecting months and exporting to XLSX
' ============================================================================

Imports System.Windows.Forms

Public Class BulkExportForm
    Inherits System.Windows.Forms.Form

#Region "Private Fields"
    Private _selectedMonths As List(Of Integer)
    Private _currentYear As Integer
    Private _checkBoxes As New Dictionary(Of Integer, CheckBox)
#End Region

#Region "Public Properties"
    Public ReadOnly Property SelectedMonths As List(Of Integer)
        Get
            Return _selectedMonths
        End Get
    End Property

    Public ReadOnly Property SelectedYear As Integer
        Get
            Return _currentYear
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New(currentYear As Integer)
        InitializeComponent()
        _currentYear = currentYear
        _selectedMonths = New List(Of Integer)
        BulkExportForm_Load()
    End Sub
#End Region

#Region "Form Events"
    Private Sub BulkExportForm_Load()
        lblYear.Text = $"Select months to export for {_currentYear}:"

        ' Create checkboxes for all 12 months
        CreateMonthCheckboxes()
    End Sub
#End Region

#Region "Private Methods"
    Private Sub CreateMonthCheckboxes()
        Dim monthNames() As String = {"January", "February", "March", "April", "May", "June",
                                       "July", "August", "September", "October", "November", "December"}
        
        Dim pnl = pnlMonths
        pnl.Controls.Clear()
        _checkBoxes.Clear()
        
        Dim left = 10
        Dim top = 10
        Dim colCount = 0
        
        For mon = 1 To 12
            Dim chk As New CheckBox()
            chk.Text = monthNames(mon - 1)
            chk.Location = New Point(left, top)
            chk.Size = New Size(150, 25)
            chk.Tag = mon
            
            pnl.Controls.Add(chk)
            _checkBoxes.Add(mon, chk)
            
            colCount += 1
            If colCount = 2 Then
                left = 10
                top += 35
                colCount = 0
            Else
                left = 160
            End If
        Next
    End Sub
#End Region

#Region "Event Handlers"
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        For Each kvp In _checkBoxes
            kvp.Value.Checked = True
        Next
    End Sub

    Private Sub btnDeselectAll_Click(sender As Object, e As EventArgs) Handles btnDeselectAll.Click
        For Each kvp In _checkBoxes
            kvp.Value.Checked = False
        Next
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        _selectedMonths.Clear()
        
        For Each kvp In _checkBoxes
            If kvp.Value.Checked Then
                _selectedMonths.Add(kvp.Key)
            End If
        Next
        
        If _selectedMonths.Count = 0 Then
            MessageBox.Show("Please select at least one month to export.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
#End Region

#Region "Designer Generated"

    Private Sub InitializeComponent()
        Me.pnlMonths = New System.Windows.Forms.Panel()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnDeselectAll = New System.Windows.Forms.Button()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.pnlMonths.SuspendLayout()
        Me.SuspendLayout()
        
        ' 
        ' lblYear
        ' 
        Me.lblYear.AutoSize = True
        Me.lblYear.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblYear.Location = New System.Drawing.Point(20, 15)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(250, 21)
        Me.lblYear.TabIndex = 0
        Me.lblYear.Text = "Select months to export:"
        
        ' 
        ' pnlMonths
        ' 
        Me.pnlMonths.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMonths.Location = New System.Drawing.Point(20, 50)
        Me.pnlMonths.Name = "pnlMonths"
        Me.pnlMonths.Size = New System.Drawing.Size(330, 280)
        Me.pnlMonths.TabIndex = 1
        
        ' 
        ' btnSelectAll
        ' 
        Me.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(70, 130, 180)
        Me.btnSelectAll.FlatAppearance.BorderSize = 0
        Me.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAll.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnSelectAll.ForeColor = System.Drawing.Color.White
        Me.btnSelectAll.Location = New System.Drawing.Point(20, 340)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(105, 30)
        Me.btnSelectAll.TabIndex = 2
        Me.btnSelectAll.Text = "Select All"
        Me.btnSelectAll.UseVisualStyleBackColor = False
        
        ' 
        ' btnDeselectAll
        ' 
        Me.btnDeselectAll.BackColor = System.Drawing.Color.FromArgb(70, 130, 180)
        Me.btnDeselectAll.FlatAppearance.BorderSize = 0
        Me.btnDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeselectAll.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnDeselectAll.ForeColor = System.Drawing.Color.White
        Me.btnDeselectAll.Location = New System.Drawing.Point(135, 340)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(105, 30)
        Me.btnDeselectAll.TabIndex = 3
        Me.btnDeselectAll.Text = "Deselect All"
        Me.btnDeselectAll.UseVisualStyleBackColor = False
        
        ' 
        ' btnExport
        ' 
        Me.btnExport.BackColor = System.Drawing.Color.FromArgb(0, 150, 80)
        Me.btnExport.FlatAppearance.BorderSize = 0
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnExport.ForeColor = System.Drawing.Color.White
        Me.btnExport.Location = New System.Drawing.Point(170, 380)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(90, 35)
        Me.btnExport.TabIndex = 4
        Me.btnExport.Text = "✓ Export"
        Me.btnExport.UseVisualStyleBackColor = False
        
        ' 
        ' btnCancel
        ' 
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(200, 50, 50)
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(270, 380)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 35)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        
        ' 
        ' BulkExportForm
        ' 
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 430)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnDeselectAll)
        Me.Controls.Add(Me.btnSelectAll)
        Me.Controls.Add(Me.pnlMonths)
        Me.Controls.Add(Me.lblYear)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BulkExportForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Bulk Export to XLSX"
        Me.pnlMonths.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlMonths As Panel
    Friend WithEvents btnSelectAll As Button
    Friend WithEvents btnDeselectAll As Button
    Friend WithEvents btnExport As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblYear As Label

#End Region

End Class
