<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ExpenseEntryForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlMain = New Panel()
        lblRemainingInfo = New Label()
        txtAmount = New TextBox()
        lblAmountLabel = New Label()
        txtDescription = New TextBox()
        lblDescLabel = New Label()
        cboCategory = New ComboBox()
        lblCategoryLabel = New Label()
        txtBillNo = New TextBox()
        lblBillNoLabel = New Label()
        dtpEntryDate = New DateTimePicker()
        lblDateLabel = New Label()
        lblTitle = New Label()
        pnlWarnings = New Panel()
        lblWarnings = New Label()
        pnlButtons = New Panel()
        btnCancel = New Button()
        btnSave = New Button()
        pnlMain.SuspendLayout()
        pnlWarnings.SuspendLayout()
        pnlButtons.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = Color.White
        pnlMain.Controls.Add(lblRemainingInfo)
        pnlMain.Controls.Add(txtAmount)
        pnlMain.Controls.Add(lblAmountLabel)
        pnlMain.Controls.Add(txtDescription)
        pnlMain.Controls.Add(lblDescLabel)
        pnlMain.Controls.Add(cboCategory)
        pnlMain.Controls.Add(lblCategoryLabel)
        pnlMain.Controls.Add(txtBillNo)
        pnlMain.Controls.Add(lblBillNoLabel)
        pnlMain.Controls.Add(dtpEntryDate)
        pnlMain.Controls.Add(lblDateLabel)
        pnlMain.Controls.Add(lblTitle)
        pnlMain.Dock = DockStyle.Top
        pnlMain.Location = New Point(0, 0)
        pnlMain.Margin = New Padding(3, 4, 3, 4)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(29, 33, 29, 33)
        pnlMain.Size = New Size(514, 507)
        pnlMain.TabIndex = 0
        ' 
        ' lblRemainingInfo
        ' 
        lblRemainingInfo.Font = New Font("Segoe UI", 9F)
        lblRemainingInfo.ForeColor = Color.Gray
        lblRemainingInfo.Location = New Point(263, 409)
        lblRemainingInfo.Name = "lblRemainingInfo"
        lblRemainingInfo.Size = New Size(206, 40)
        lblRemainingInfo.TabIndex = 11
        lblRemainingInfo.Text = "Remaining: LKR 25,000.00"
        lblRemainingInfo.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtAmount
        ' 
        txtAmount.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        txtAmount.Location = New Point(29, 409)
        txtAmount.Margin = New Padding(3, 4, 3, 4)
        txtAmount.Name = "txtAmount"
        txtAmount.Size = New Size(205, 34)
        txtAmount.TabIndex = 10
        txtAmount.TextAlign = HorizontalAlignment.Right
        ' 
        ' lblAmountLabel
        ' 
        lblAmountLabel.AutoSize = True
        lblAmountLabel.Font = New Font("Segoe UI", 10F)
        lblAmountLabel.Location = New Point(29, 380)
        lblAmountLabel.Name = "lblAmountLabel"
        lblAmountLabel.Size = New Size(119, 23)
        lblAmountLabel.TabIndex = 9
        lblAmountLabel.Text = "Amount (LKR):"
        ' 
        ' txtDescription
        ' 
        txtDescription.Font = New Font("Segoe UI", 10F)
        txtDescription.Location = New Point(29, 283)
        txtDescription.Margin = New Padding(3, 4, 3, 4)
        txtDescription.Multiline = True
        txtDescription.Name = "txtDescription"
        txtDescription.ScrollBars = ScrollBars.Vertical
        txtDescription.Size = New Size(439, 79)
        txtDescription.TabIndex = 8
        ' 
        ' lblDescLabel
        ' 
        lblDescLabel.AutoSize = True
        lblDescLabel.Font = New Font("Segoe UI", 10F)
        lblDescLabel.Location = New Point(29, 253)
        lblDescLabel.Name = "lblDescLabel"
        lblDescLabel.Size = New Size(100, 23)
        lblDescLabel.TabIndex = 7
        lblDescLabel.Text = "Description:"
        ' 
        ' cboCategory
        ' 
        cboCategory.DropDownStyle = ComboBoxStyle.DropDownList
        cboCategory.Font = New Font("Segoe UI", 10F)
        cboCategory.FormattingEnabled = True
        cboCategory.Location = New Point(29, 203)
        cboCategory.Margin = New Padding(3, 4, 3, 4)
        cboCategory.Name = "cboCategory"
        cboCategory.Size = New Size(439, 31)
        cboCategory.TabIndex = 6
        ' 
        ' lblCategoryLabel
        ' 
        lblCategoryLabel.AutoSize = True
        lblCategoryLabel.Font = New Font("Segoe UI", 10F)
        lblCategoryLabel.Location = New Point(29, 173)
        lblCategoryLabel.Name = "lblCategoryLabel"
        lblCategoryLabel.Size = New Size(83, 23)
        lblCategoryLabel.TabIndex = 5
        lblCategoryLabel.Text = "Category:"
        ' 
        ' txtBillNo
        ' 
        txtBillNo.Font = New Font("Segoe UI", 10F)
        txtBillNo.Location = New Point(263, 123)
        txtBillNo.Margin = New Padding(3, 4, 3, 4)
        txtBillNo.Name = "txtBillNo"
        txtBillNo.Size = New Size(205, 30)
        txtBillNo.TabIndex = 4
        ' 
        ' lblBillNoLabel
        ' 
        lblBillNoLabel.AutoSize = True
        lblBillNoLabel.Font = New Font("Segoe UI", 10F)
        lblBillNoLabel.Location = New Point(263, 93)
        lblBillNoLabel.Name = "lblBillNoLabel"
        lblBillNoLabel.Size = New Size(104, 23)
        lblBillNoLabel.TabIndex = 3
        lblBillNoLabel.Text = "Bill Number:"
        ' 
        ' dtpEntryDate
        ' 
        dtpEntryDate.Font = New Font("Segoe UI", 10F)
        dtpEntryDate.Format = DateTimePickerFormat.Short
        dtpEntryDate.Location = New Point(29, 123)
        dtpEntryDate.Margin = New Padding(3, 4, 3, 4)
        dtpEntryDate.Name = "dtpEntryDate"
        dtpEntryDate.Size = New Size(205, 30)
        dtpEntryDate.TabIndex = 2
        ' 
        ' lblDateLabel
        ' 
        lblDateLabel.AutoSize = True
        lblDateLabel.Font = New Font("Segoe UI", 10F)
        lblDateLabel.Location = New Point(29, 93)
        lblDateLabel.Name = "lblDateLabel"
        lblDateLabel.Size = New Size(50, 23)
        lblDateLabel.TabIndex = 1
        lblDateLabel.Text = "Date:"
        ' 
        ' lblTitle
        ' 
        lblTitle.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(CByte(0), CByte(51), CByte(102))
        lblTitle.Location = New Point(29, 27)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(457, 47)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Add Expense"
        ' 
        ' pnlWarnings
        ' 
        pnlWarnings.BackColor = Color.FromArgb(CByte(255), CByte(248), CByte(220))
        pnlWarnings.Controls.Add(lblWarnings)
        pnlWarnings.Dock = DockStyle.Top
        pnlWarnings.Location = New Point(0, 507)
        pnlWarnings.Margin = New Padding(3, 4, 3, 4)
        pnlWarnings.Name = "pnlWarnings"
        pnlWarnings.Padding = New Padding(29, 13, 29, 13)
        pnlWarnings.Size = New Size(514, 0)
        pnlWarnings.TabIndex = 1
        pnlWarnings.Visible = False
        ' 
        ' lblWarnings
        ' 
        lblWarnings.Dock = DockStyle.Fill
        lblWarnings.Font = New Font("Segoe UI", 9F)
        lblWarnings.ForeColor = Color.FromArgb(CByte(180), CByte(120), CByte(0))
        lblWarnings.Location = New Point(29, 13)
        lblWarnings.Name = "lblWarnings"
        lblWarnings.Size = New Size(456, 0)
        lblWarnings.TabIndex = 0
        ' 
        ' pnlButtons
        ' 
        pnlButtons.BackColor = Color.FromArgb(CByte(240), CByte(240), CByte(245))
        pnlButtons.Controls.Add(btnCancel)
        pnlButtons.Controls.Add(btnSave)
        pnlButtons.Dock = DockStyle.Bottom
        pnlButtons.Location = New Point(0, 520)
        pnlButtons.Margin = New Padding(3, 4, 3, 4)
        pnlButtons.Name = "pnlButtons"
        pnlButtons.Size = New Size(514, 80)
        pnlButtons.TabIndex = 2
        ' 
        ' btnCancel
        ' 
        btnCancel.BackColor = Color.Gray
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        btnCancel.ForeColor = Color.White
        btnCancel.Location = New Point(343, 16)
        btnCancel.Margin = New Padding(3, 4, 3, 4)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(114, 51)
        btnCancel.TabIndex = 1
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = False
        ' 
        ' btnSave
        ' 
        btnSave.BackColor = Color.FromArgb(CByte(0), CByte(150), CByte(80))
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(194, 16)
        btnSave.Margin = New Padding(3, 4, 3, 4)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(137, 51)
        btnSave.TabIndex = 0
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = False
        ' 
        ' ExpenseEntryForm
        ' 
        AcceptButton = btnSave
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        CancelButton = btnCancel
        ClientSize = New Size(514, 600)
        Controls.Add(pnlButtons)
        Controls.Add(pnlWarnings)
        Controls.Add(pnlMain)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(3, 4, 3, 4)
        MaximizeBox = False
        MinimizeBox = False
        Name = "ExpenseEntryForm"
        StartPosition = FormStartPosition.CenterParent
        Text = "Add Expense"
        pnlMain.ResumeLayout(False)
        pnlMain.PerformLayout()
        pnlWarnings.ResumeLayout(False)
        pnlButtons.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlMain As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblDateLabel As Label
    Friend WithEvents dtpEntryDate As DateTimePicker
    Friend WithEvents lblBillNoLabel As Label
    Friend WithEvents txtBillNo As TextBox
    Friend WithEvents lblCategoryLabel As Label
    Friend WithEvents cboCategory As ComboBox
    Friend WithEvents lblDescLabel As Label
    Friend WithEvents txtDescription As TextBox
    Friend WithEvents lblAmountLabel As Label
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents lblRemainingInfo As Label
    Friend WithEvents pnlWarnings As Panel
    Friend WithEvents lblWarnings As Label
    Friend WithEvents pnlButtons As Panel
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button

End Class
