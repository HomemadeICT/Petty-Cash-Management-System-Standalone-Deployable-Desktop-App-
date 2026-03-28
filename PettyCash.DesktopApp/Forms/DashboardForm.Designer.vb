<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DashboardForm
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
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.btnLogout = New System.Windows.Forms.Button()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.lblAppTitle = New System.Windows.Forms.Label()
        Me.pnlMonthNav = New System.Windows.Forms.Panel()
        Me.btnNextMonth = New System.Windows.Forms.Button()
        Me.lblCurrentMonth = New System.Windows.Forms.Label()
        Me.btnPrevMonth = New System.Windows.Forms.Button()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.grpCategories = New System.Windows.Forms.GroupBox()
        Me.lblCatE7510 = New System.Windows.Forms.Label()
        Me.lblCatE7800 = New System.Windows.Forms.Label()
        Me.lblCatE5300 = New System.Windows.Forms.Label()
        Me.lblCatE5200 = New System.Windows.Forms.Label()
        Me.grpSummary = New System.Windows.Forms.GroupBox()
        Me.prgMonthlyLimit = New System.Windows.Forms.ProgressBar()
        Me.lblRemainingValue = New System.Windows.Forms.Label()
        Me.lblRemainingLabel = New System.Windows.Forms.Label()
        Me.lblTotalValue = New System.Windows.Forms.Label()
        Me.lblTotalLabel = New System.Windows.Forms.Label()
        Me.pnlActions = New System.Windows.Forms.Panel()
        Me.btnBulkExport = New System.Windows.Forms.Button()
        Me.btnAuditLog = New System.Windows.Forms.Button()
        Me.btnManageUsers = New System.Windows.Forms.Button()
        Me.btnBackupDB = New System.Windows.Forms.Button()
        Me.btnGenerateReport = New System.Windows.Forms.Button()
        Me.btnDeleteEntry = New System.Windows.Forms.Button()
        Me.btnEditEntry = New System.Windows.Forms.Button()
        Me.btnAddEntry = New System.Windows.Forms.Button()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.dgvEntries = New System.Windows.Forms.DataGridView()
        Me.pnlHeader.SuspendLayout()
        Me.pnlMonthNav.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.grpCategories.SuspendLayout()
        Me.grpSummary.SuspendLayout()
        Me.pnlActions.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.dgvEntries, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.pnlHeader.Controls.Add(Me.btnLogout)
        Me.pnlHeader.Controls.Add(Me.btnSettings)
        Me.pnlHeader.Controls.Add(Me.lblWelcome)
        Me.pnlHeader.Controls.Add(Me.lblAppTitle)
        Me.pnlHeader.Controls.Add(Me.btnSettings)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1200, 60)
        Me.pnlHeader.TabIndex = 0
        '
        'lblAppTitle
        '
        Me.lblAppTitle.AutoSize = True
        Me.lblAppTitle.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblAppTitle.ForeColor = System.Drawing.Color.White
        Me.lblAppTitle.Location = New System.Drawing.Point(20, 15)
        Me.lblAppTitle.Name = "lblAppTitle"
        Me.lblAppTitle.Size = New System.Drawing.Size(300, 30)
        Me.lblAppTitle.TabIndex = 0
        Me.lblAppTitle.Text = "Petty Cash Management"
        '
        'lblWelcome
        '
        Me.lblWelcome.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblWelcome.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblWelcome.ForeColor = System.Drawing.Color.White
        Me.lblWelcome.Location = New System.Drawing.Point(900, 20)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(180, 20)
        Me.lblWelcome.TabIndex = 1
        Me.lblWelcome.Text = "Welcome, User"
        Me.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnSettings
        '
        Me.btnSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSettings.BackColor = System.Drawing.Color.FromArgb(70, 130, 180)
        Me.btnSettings.FlatAppearance.BorderSize = 0
        Me.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSettings.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnSettings.ForeColor = System.Drawing.Color.White
        Me.btnSettings.Location = New System.Drawing.Point(1000, 15)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(90, 30)
        Me.btnSettings.TabIndex = 3
        Me.btnSettings.Text = "⚙ Settings"
        Me.btnSettings.UseVisualStyleBackColor = False
        '
        'btnLogout
        '
        Me.btnLogout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogout.BackColor = System.Drawing.Color.FromArgb(200, 50, 50)
        Me.btnLogout.FlatAppearance.BorderSize = 0
        Me.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.Location = New System.Drawing.Point(1100, 15)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(80, 30)
        Me.btnLogout.TabIndex = 2
        Me.btnLogout.Text = "Logout"
        Me.btnLogout.UseVisualStyleBackColor = False
        '
        'pnlMonthNav
        '
        Me.pnlMonthNav.BackColor = System.Drawing.Color.FromArgb(240, 240, 245)
        Me.pnlMonthNav.Controls.Add(Me.btnNextMonth)
        Me.pnlMonthNav.Controls.Add(Me.lblCurrentMonth)
        Me.pnlMonthNav.Controls.Add(Me.btnPrevMonth)
        Me.pnlMonthNav.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMonthNav.Location = New System.Drawing.Point(0, 60)
        Me.pnlMonthNav.Name = "pnlMonthNav"
        Me.pnlMonthNav.Size = New System.Drawing.Size(1200, 50)
        Me.pnlMonthNav.TabIndex = 1
        '
        'btnPrevMonth
        '
        Me.btnPrevMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrevMonth.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnPrevMonth.Location = New System.Drawing.Point(450, 10)
        Me.btnPrevMonth.Name = "btnPrevMonth"
        Me.btnPrevMonth.Size = New System.Drawing.Size(40, 30)
        Me.btnPrevMonth.TabIndex = 0
        Me.btnPrevMonth.Text = "<"
        '
        'lblCurrentMonth
        '
        Me.lblCurrentMonth.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblCurrentMonth.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.lblCurrentMonth.Location = New System.Drawing.Point(500, 10)
        Me.lblCurrentMonth.Name = "lblCurrentMonth"
        Me.lblCurrentMonth.Size = New System.Drawing.Size(200, 30)
        Me.lblCurrentMonth.TabIndex = 1
        Me.lblCurrentMonth.Text = "February 2026"
        Me.lblCurrentMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnNextMonth
        '
        Me.btnNextMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNextMonth.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnNextMonth.Location = New System.Drawing.Point(710, 10)
        Me.btnNextMonth.Name = "btnNextMonth"
        Me.btnNextMonth.Size = New System.Drawing.Size(40, 30)
        Me.btnNextMonth.TabIndex = 2
        Me.btnNextMonth.Text = ">"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.White
        Me.pnlLeft.Controls.Add(Me.grpCategories)
        Me.pnlLeft.Controls.Add(Me.grpSummary)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 110)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlLeft.Size = New System.Drawing.Size(280, 540)
        Me.pnlLeft.TabIndex = 2
        '
        'grpSummary
        '
        Me.grpSummary.Controls.Add(Me.prgMonthlyLimit)
        Me.grpSummary.Controls.Add(Me.lblRemainingValue)
        Me.grpSummary.Controls.Add(Me.lblRemainingLabel)
        Me.grpSummary.Controls.Add(Me.lblTotalValue)
        Me.grpSummary.Controls.Add(Me.lblTotalLabel)
        Me.grpSummary.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpSummary.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.grpSummary.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.grpSummary.Location = New System.Drawing.Point(10, 10)
        Me.grpSummary.Name = "grpSummary"
        Me.grpSummary.Size = New System.Drawing.Size(260, 160)
        Me.grpSummary.TabIndex = 0
        Me.grpSummary.TabStop = False
        Me.grpSummary.Text = "Monthly Summary"
        '
        'lblTotalLabel
        '
        Me.lblTotalLabel.AutoSize = True
        Me.lblTotalLabel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblTotalLabel.ForeColor = System.Drawing.Color.Gray
        Me.lblTotalLabel.Location = New System.Drawing.Point(15, 35)
        Me.lblTotalLabel.Name = "lblTotalLabel"
        Me.lblTotalLabel.Size = New System.Drawing.Size(80, 19)
        Me.lblTotalLabel.TabIndex = 0
        Me.lblTotalLabel.Text = "Total Spent:"
        '
        'lblTotalValue
        '
        Me.lblTotalValue.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.lblTotalValue.Location = New System.Drawing.Point(15, 55)
        Me.lblTotalValue.Name = "lblTotalValue"
        Me.lblTotalValue.Size = New System.Drawing.Size(230, 30)
        Me.lblTotalValue.TabIndex = 1
        Me.lblTotalValue.Text = "LKR 0.00"
        '
        'lblRemainingLabel
        '
        Me.lblRemainingLabel.AutoSize = True
        Me.lblRemainingLabel.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblRemainingLabel.ForeColor = System.Drawing.Color.Gray
        Me.lblRemainingLabel.Location = New System.Drawing.Point(15, 90)
        Me.lblRemainingLabel.Name = "lblRemainingLabel"
        Me.lblRemainingLabel.Size = New System.Drawing.Size(77, 19)
        Me.lblRemainingLabel.TabIndex = 2
        Me.lblRemainingLabel.Text = "Remaining:"
        '
        'lblRemainingValue
        '
        Me.lblRemainingValue.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblRemainingValue.ForeColor = System.Drawing.Color.Green
        Me.lblRemainingValue.Location = New System.Drawing.Point(100, 88)
        Me.lblRemainingValue.Name = "lblRemainingValue"
        Me.lblRemainingValue.Size = New System.Drawing.Size(150, 25)
        Me.lblRemainingValue.TabIndex = 3
        Me.lblRemainingValue.Text = "LKR 25,000.00"
        '
        'prgMonthlyLimit
        '
        Me.prgMonthlyLimit.Location = New System.Drawing.Point(15, 125)
        Me.prgMonthlyLimit.Maximum = 25000
        Me.prgMonthlyLimit.Name = "prgMonthlyLimit"
        Me.prgMonthlyLimit.Size = New System.Drawing.Size(230, 20)
        Me.prgMonthlyLimit.TabIndex = 4
        '
        'grpCategories
        '
        Me.grpCategories.Controls.Add(Me.lblCatE7510)
        Me.grpCategories.Controls.Add(Me.lblCatE7800)
        Me.grpCategories.Controls.Add(Me.lblCatE5300)
        Me.grpCategories.Controls.Add(Me.lblCatE5200)
        Me.grpCategories.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpCategories.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.grpCategories.ForeColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.grpCategories.Location = New System.Drawing.Point(10, 170)
        Me.grpCategories.Name = "grpCategories"
        Me.grpCategories.Size = New System.Drawing.Size(260, 150)
        Me.grpCategories.TabIndex = 1
        Me.grpCategories.TabStop = False
        Me.grpCategories.Text = "By Category"
        '
        'lblCatE5200
        '
        Me.lblCatE5200.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblCatE5200.ForeColor = System.Drawing.Color.Black
        Me.lblCatE5200.Location = New System.Drawing.Point(15, 30)
        Me.lblCatE5200.Name = "lblCatE5200"
        Me.lblCatE5200.Size = New System.Drawing.Size(230, 20)
        Me.lblCatE5200.TabIndex = 0
        Me.lblCatE5200.Text = "E5200 Vehicle Parts: LKR 0.00"
        '
        'lblCatE5300
        '
        Me.lblCatE5300.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblCatE5300.ForeColor = System.Drawing.Color.Black
        Me.lblCatE5300.Location = New System.Drawing.Point(15, 55)
        Me.lblCatE5300.Name = "lblCatE5300"
        Me.lblCatE5300.Size = New System.Drawing.Size(230, 20)
        Me.lblCatE5300.TabIndex = 1
        Me.lblCatE5300.Text = "E5300 Office Items: LKR 0.00"
        '
        'lblCatE7800
        '
        Me.lblCatE7800.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblCatE7800.ForeColor = System.Drawing.Color.Black
        Me.lblCatE7800.Location = New System.Drawing.Point(15, 80)
        Me.lblCatE7800.Name = "lblCatE7800"
        Me.lblCatE7800.Size = New System.Drawing.Size(230, 20)
        Me.lblCatE7800.TabIndex = 2
        Me.lblCatE7800.Text = "E7800 Physical Hardware: LKR 0.00"
        '
        'lblCatE7510
        '
        Me.lblCatE7510.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lblCatE7510.ForeColor = System.Drawing.Color.Black
        Me.lblCatE7510.Location = New System.Drawing.Point(15, 105)
        Me.lblCatE7510.Name = "lblCatE7510"
        Me.lblCatE7510.Size = New System.Drawing.Size(230, 20)
        Me.lblCatE7510.TabIndex = 3
        Me.lblCatE7510.Text = "E7510 Treatments: LKR 0.00"
        '
        'pnlActions
        '
        Me.pnlActions.BackColor = System.Drawing.Color.FromArgb(240, 240, 245)
        Me.pnlActions.Controls.Add(Me.btnBackupDB)
        Me.pnlActions.Controls.Add(Me.btnManageUsers)
        Me.pnlActions.Controls.Add(Me.btnAuditLog)
        Me.pnlActions.Controls.Add(Me.btnBulkExport)
        Me.pnlActions.Controls.Add(Me.btnGenerateReport)
        Me.pnlActions.Controls.Add(Me.btnDeleteEntry)
        Me.pnlActions.Controls.Add(Me.btnEditEntry)
        Me.pnlActions.Controls.Add(Me.btnAddEntry)
        Me.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlActions.Location = New System.Drawing.Point(280, 600)
        Me.pnlActions.Name = "pnlActions"
        Me.pnlActions.Size = New System.Drawing.Size(920, 50)
        Me.pnlActions.TabIndex = 3
        '
        'btnAddEntry
        '
        Me.btnAddEntry.BackColor = System.Drawing.Color.FromArgb(0, 150, 80)
        Me.btnAddEntry.FlatAppearance.BorderSize = 0
        Me.btnAddEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddEntry.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnAddEntry.ForeColor = System.Drawing.Color.White
        Me.btnAddEntry.Location = New System.Drawing.Point(20, 10)
        Me.btnAddEntry.Name = "btnAddEntry"
        Me.btnAddEntry.Size = New System.Drawing.Size(120, 32)
        Me.btnAddEntry.TabIndex = 0
        Me.btnAddEntry.Text = "+ Add Entry"
        Me.btnAddEntry.UseVisualStyleBackColor = False
        '
        'btnEditEntry
        '
        Me.btnEditEntry.BackColor = System.Drawing.Color.FromArgb(50, 100, 180)
        Me.btnEditEntry.FlatAppearance.BorderSize = 0
        Me.btnEditEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditEntry.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnEditEntry.ForeColor = System.Drawing.Color.White
        Me.btnEditEntry.Location = New System.Drawing.Point(150, 10)
        Me.btnEditEntry.Name = "btnEditEntry"
        Me.btnEditEntry.Size = New System.Drawing.Size(100, 32)
        Me.btnEditEntry.TabIndex = 1
        Me.btnEditEntry.Text = "Edit"
        Me.btnEditEntry.UseVisualStyleBackColor = False
        '
        'btnDeleteEntry
        '
        Me.btnDeleteEntry.BackColor = System.Drawing.Color.FromArgb(200, 50, 50)
        Me.btnDeleteEntry.FlatAppearance.BorderSize = 0
        Me.btnDeleteEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteEntry.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnDeleteEntry.ForeColor = System.Drawing.Color.White
        Me.btnDeleteEntry.Location = New System.Drawing.Point(260, 10)
        Me.btnDeleteEntry.Name = "btnDeleteEntry"
        Me.btnDeleteEntry.Size = New System.Drawing.Size(100, 32)
        Me.btnDeleteEntry.TabIndex = 2
        Me.btnDeleteEntry.Text = "Delete"
        Me.btnDeleteEntry.UseVisualStyleBackColor = False
        '
        'btnGenerateReport
        '
        Me.btnGenerateReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGenerateReport.BackColor = System.Drawing.Color.FromArgb(0, 51, 102)
        Me.btnGenerateReport.FlatAppearance.BorderSize = 0
        Me.btnGenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerateReport.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnGenerateReport.ForeColor = System.Drawing.Color.White
        Me.btnGenerateReport.Location = New System.Drawing.Point(670, 10)
        Me.btnGenerateReport.Name = "btnGenerateReport"
        Me.btnGenerateReport.Size = New System.Drawing.Size(130, 32)
        Me.btnGenerateReport.TabIndex = 3
        Me.btnGenerateReport.Text = "Generate Report"
        Me.btnGenerateReport.UseVisualStyleBackColor = False
        '
        'btnBulkExport
        '
        Me.btnBulkExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBulkExport.BackColor = System.Drawing.Color.FromArgb(0, 100, 150)
        Me.btnBulkExport.FlatAppearance.BorderSize = 0
        Me.btnBulkExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBulkExport.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnBulkExport.ForeColor = System.Drawing.Color.White
        Me.btnBulkExport.Location = New System.Drawing.Point(570, 10)
        Me.btnBulkExport.Name = "btnBulkExport"
        Me.btnBulkExport.Size = New System.Drawing.Size(90, 32)
        Me.btnBulkExport.TabIndex = 7
        Me.btnBulkExport.Text = "⬇ Export"
        Me.btnBulkExport.UseVisualStyleBackColor = False
        '
        'btnAuditLog
        '
        Me.btnAuditLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAuditLog.BackColor = System.Drawing.Color.Gray
        Me.btnAuditLog.FlatAppearance.BorderSize = 0
        Me.btnAuditLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAuditLog.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnAuditLog.ForeColor = System.Drawing.Color.White
        Me.btnAuditLog.Location = New System.Drawing.Point(810, 10)
        Me.btnAuditLog.Name = "btnAuditLog"
        Me.btnAuditLog.Size = New System.Drawing.Size(100, 32)
        Me.btnAuditLog.TabIndex = 4
        Me.btnAuditLog.Text = "Audit Log"
        Me.btnAuditLog.UseVisualStyleBackColor = False
        '
        'btnManageUsers
        '
        Me.btnManageUsers.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnManageUsers.BackColor = System.Drawing.Color.FromArgb(100, 60, 160)
        Me.btnManageUsers.FlatAppearance.BorderSize = 0
        Me.btnManageUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnManageUsers.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnManageUsers.ForeColor = System.Drawing.Color.White
        Me.btnManageUsers.Location = New System.Drawing.Point(530, 10)
        Me.btnManageUsers.Name = "btnManageUsers"
        Me.btnManageUsers.Size = New System.Drawing.Size(130, 32)
        Me.btnManageUsers.TabIndex = 5
        Me.btnManageUsers.Text = "Manage Users"
        Me.btnManageUsers.UseVisualStyleBackColor = False
        '
        'btnBackupDB
        '
        Me.btnBackupDB.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBackupDB.BackColor = System.Drawing.Color.FromArgb(200, 120, 0)
        Me.btnBackupDB.FlatAppearance.BorderSize = 0
        Me.btnBackupDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackupDB.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.btnBackupDB.ForeColor = System.Drawing.Color.White
        Me.btnBackupDB.Location = New System.Drawing.Point(400, 10)
        Me.btnBackupDB.Name = "btnBackupDB"
        Me.btnBackupDB.Size = New System.Drawing.Size(120, 32)
        Me.btnBackupDB.TabIndex = 6
        Me.btnBackupDB.Text = "🗄 Backup DB"
        Me.btnBackupDB.UseVisualStyleBackColor = False
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.White
        Me.pnlGrid.Controls.Add(Me.dgvEntries)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(280, 110)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(10)
        Me.pnlGrid.Size = New System.Drawing.Size(920, 490)
        Me.pnlGrid.TabIndex = 4
        '
        'dgvEntries
        '
        Me.dgvEntries.AllowUserToAddRows = False
        Me.dgvEntries.AllowUserToDeleteRows = False
        Me.dgvEntries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvEntries.BackgroundColor = System.Drawing.Color.White
        Me.dgvEntries.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvEntries.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvEntries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEntries.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvEntries.GridColor = System.Drawing.Color.FromArgb(240, 240, 245)
        Me.dgvEntries.Location = New System.Drawing.Point(10, 10)
        Me.dgvEntries.MultiSelect = False
        Me.dgvEntries.Name = "dgvEntries"
        Me.dgvEntries.ReadOnly = True
        Me.dgvEntries.RowHeadersVisible = False
        Me.dgvEntries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEntries.Size = New System.Drawing.Size(900, 470)
        Me.dgvEntries.TabIndex = 0
        '
        'DashboardForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(240, 240, 245)
        Me.ClientSize = New System.Drawing.Size(1200, 650)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlActions)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlMonthNav)
        Me.Controls.Add(Me.pnlHeader)
        Me.MinimumSize = New System.Drawing.Size(1024, 600)
        Me.Name = "DashboardForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dashboard - Petty Cash Management System"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlMonthNav.ResumeLayout(False)
        Me.pnlLeft.ResumeLayout(False)
        Me.grpCategories.ResumeLayout(False)
        Me.grpSummary.ResumeLayout(False)
        Me.grpSummary.PerformLayout()
        Me.pnlActions.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.dgvEntries, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblAppTitle As Label
    Friend WithEvents lblWelcome As Label
    Friend WithEvents btnSettings As Button
    Friend WithEvents btnLogout As Button
    Friend WithEvents pnlMonthNav As Panel
    Friend WithEvents btnPrevMonth As Button
    Friend WithEvents lblCurrentMonth As Label
    Friend WithEvents btnNextMonth As Button
    Friend WithEvents pnlLeft As Panel
    Friend WithEvents grpSummary As GroupBox
    Friend WithEvents lblTotalLabel As Label
    Friend WithEvents lblTotalValue As Label
    Friend WithEvents lblRemainingLabel As Label
    Friend WithEvents lblRemainingValue As Label
    Friend WithEvents prgMonthlyLimit As ProgressBar
    Friend WithEvents grpCategories As GroupBox
    Friend WithEvents lblCatE5200 As Label
    Friend WithEvents lblCatE5300 As Label
    Friend WithEvents lblCatE7800 As Label
    Friend WithEvents lblCatE7510 As Label
    Friend WithEvents pnlActions As Panel
    Friend WithEvents btnAddEntry As Button
    Friend WithEvents btnEditEntry As Button
    Friend WithEvents btnDeleteEntry As Button
    Friend WithEvents btnGenerateReport As Button
    Friend WithEvents btnBulkExport As Button
    Friend WithEvents btnAuditLog As Button
    Friend WithEvents btnManageUsers As Button
    Friend WithEvents btnBackupDB As Button
    Friend WithEvents pnlGrid As Panel
    Friend WithEvents dgvEntries As DataGridView

End Class
