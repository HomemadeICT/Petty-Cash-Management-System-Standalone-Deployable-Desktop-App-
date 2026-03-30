' ============================================================================
' CategoryManagementForm.vb - Category Management Form
' Petty Cash Management System
' ============================================================================
' Purpose: Admin UI to Add, Edit, and Delete expense category codes & names
' ============================================================================

Imports System.Windows.Forms
Imports System.Data.SQLite

Public Class CategoryManagementForm
    Inherits Form

#Region "Private Fields"
    Private _categoryRepo As CategoryRepository
    Private _auditService As AuditService
    Private _editingId As Integer = -1   ' -1 = adding new, >0 = editing
#End Region

#Region "Controls"
    Private WithEvents dgvCategories As DataGridView
    Private WithEvents txtCode As TextBox
    Private WithEvents txtName As TextBox
    Private WithEvents btnSave As Button
    Private WithEvents btnClear As Button
    Private WithEvents btnDelete As Button
    Private WithEvents btnClose As Button
    Private pnlHeader As Panel
    Private pnlForm As Panel
    Private pnlGrid As Panel
    Private lblTitle As Label
    Private lblCode As Label
    Private lblName As Label
    Private lblFormTitle As Label
#End Region

#Region "Constructor"
    Public Sub New()
        InitializeControls()
        InitializeServices()
    End Sub
#End Region

#Region "Initialization"
    Private Sub InitializeServices()
        _categoryRepo = New CategoryRepository()
        Dim auditRepo As New AuditLogRepository()
        _auditService = New AuditService(auditRepo)
    End Sub

    Private Sub InitializeControls()
        '── Form ──
        Me.Text = "Manage Categories"
        Me.Size = New Drawing.Size(680, 620)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Drawing.Color.FromArgb(240, 240, 245)

        '── Header panel ──
        pnlHeader = New Panel() With {
            .BackColor = Drawing.Color.FromArgb(0, 51, 102),
            .Dock = DockStyle.Top,
            .Height = 60
        }
        lblTitle = New Label() With {
            .Text = "📂 Category Management",
            .Font = New Drawing.Font("Segoe UI", 15, Drawing.FontStyle.Bold),
            .ForeColor = Drawing.Color.White,
            .Location = New Drawing.Point(20, 15),
            .AutoSize = True
        }
        pnlHeader.Controls.Add(lblTitle)
        Me.Controls.Add(pnlHeader)

        '── Entry form panel ──
        pnlForm = New Panel() With {
            .BackColor = Drawing.Color.White,
            .Location = New Drawing.Point(0, 60),
            .Size = New Drawing.Size(660, 155),
            .Padding = New Padding(20)
        }

        lblFormTitle = New Label() With {
            .Text = "Add New Category",
            .Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold),
            .ForeColor = Drawing.Color.FromArgb(0, 51, 102),
            .Location = New Drawing.Point(20, 12),
            .AutoSize = True
        }
        pnlForm.Controls.Add(lblFormTitle)

        lblCode = New Label() With {
            .Text = "Category Code:",
            .Font = New Drawing.Font("Segoe UI", 9),
            .Location = New Drawing.Point(20, 42),
            .AutoSize = True
        }
        pnlForm.Controls.Add(lblCode)

        txtCode = New TextBox() With {
            .Font = New Drawing.Font("Segoe UI", 10),
            .Location = New Drawing.Point(140, 38),
            .Size = New Drawing.Size(140, 28),
            .MaxLength = 10,
            .CharacterCasing = CharacterCasing.Upper
        }
        pnlForm.Controls.Add(txtCode)

        Dim lblCodeHint As New Label() With {
            .Text = "(e.g. E5200)",
            .Font = New Drawing.Font("Segoe UI", 8, Drawing.FontStyle.Italic),
            .ForeColor = Drawing.Color.Gray,
            .Location = New Drawing.Point(290, 45),
            .AutoSize = True
        }
        pnlForm.Controls.Add(lblCodeHint)

        lblName = New Label() With {
            .Text = "Category Name:",
            .Font = New Drawing.Font("Segoe UI", 9),
            .Location = New Drawing.Point(20, 82),
            .AutoSize = True
        }
        pnlForm.Controls.Add(lblName)

        txtName = New TextBox() With {
            .Font = New Drawing.Font("Segoe UI", 10),
            .Location = New Drawing.Point(140, 78),
            .Size = New Drawing.Size(340, 28),
            .MaxLength = 100
        }
        pnlForm.Controls.Add(txtName)

        '── Save button ──
        btnSave = New Button() With {
            .Text = "💾 Save",
            .Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold),
            .BackColor = Drawing.Color.FromArgb(0, 150, 80),
            .ForeColor = Drawing.Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Size = New Drawing.Size(100, 32),
            .Location = New Drawing.Point(500, 55)
        }
        btnSave.FlatAppearance.BorderSize = 0
        pnlForm.Controls.Add(btnSave)

        '── Clear button ──
        btnClear = New Button() With {
            .Text = "✖ Clear",
            .Font = New Drawing.Font("Segoe UI", 9),
            .BackColor = Drawing.Color.FromArgb(130, 130, 130),
            .ForeColor = Drawing.Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Size = New Drawing.Size(100, 32),
            .Location = New Drawing.Point(500, 95)
        }
        btnClear.FlatAppearance.BorderSize = 0
        pnlForm.Controls.Add(btnClear)

        Me.Controls.Add(pnlForm)

        '── Grid panel ──
        pnlGrid = New Panel() With {
            .BackColor = Drawing.Color.White,
            .Location = New Drawing.Point(0, 220),
            .Size = New Drawing.Size(660, 295),
            .Padding = New Padding(10)
        }

        dgvCategories = New DataGridView() With {
            .Dock = DockStyle.Fill,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .ReadOnly = True,
            .MultiSelect = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .BackgroundColor = Drawing.Color.White,
            .BorderStyle = BorderStyle.None,
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
            .RowHeadersVisible = False,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            .Font = New Drawing.Font("Segoe UI", 10)
        }
        dgvCategories.ColumnHeadersDefaultCellStyle.BackColor = Drawing.Color.FromArgb(0, 51, 102)
        dgvCategories.ColumnHeadersDefaultCellStyle.ForeColor = Drawing.Color.White
        dgvCategories.ColumnHeadersDefaultCellStyle.Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold)
        dgvCategories.EnableHeadersVisualStyles = False
        dgvCategories.RowTemplate.Height = 32
        dgvCategories.AlternatingRowsDefaultCellStyle.BackColor = Drawing.Color.FromArgb(245, 245, 250)
        pnlGrid.Controls.Add(dgvCategories)
        Me.Controls.Add(pnlGrid)

        '── Bottom action buttons ──
        Dim pnlBottom As New Panel() With {
            .BackColor = Drawing.Color.FromArgb(240, 240, 245),
            .Location = New Drawing.Point(0, 520),
            .Size = New Drawing.Size(660, 60)
        }

        btnDelete = New Button() With {
            .Text = "🗑 Delete Selected",
            .Font = New Drawing.Font("Segoe UI", 9, Drawing.FontStyle.Bold),
            .BackColor = Drawing.Color.FromArgb(200, 50, 50),
            .ForeColor = Drawing.Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Size = New Drawing.Size(160, 34),
            .Location = New Drawing.Point(20, 10)
        }
        btnDelete.FlatAppearance.BorderSize = 0
        pnlBottom.Controls.Add(btnDelete)

        btnClose = New Button() With {
            .Text = "Close",
            .Font = New Drawing.Font("Segoe UI", 9),
            .BackColor = Drawing.Color.Gray,
            .ForeColor = Drawing.Color.White,
            .FlatStyle = FlatStyle.Flat,
            .Size = New Drawing.Size(100, 34),
            .Location = New Drawing.Point(550, 10)
        }
        btnClose.FlatAppearance.BorderSize = 0
        pnlBottom.Controls.Add(btnClose)
        Me.Controls.Add(pnlBottom)
    End Sub
#End Region

#Region "Form Events"
    Private Sub CategoryManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormIconHelper.ApplyIcon(Me, FormIconHelper.FormType.Categories)
        LoadGrid()
        ClearForm()
    End Sub
#End Region

#Region "Grid"
    Private Sub LoadGrid()
        Dim categories = _categoryRepo.GetAll()
        dgvCategories.DataSource = Nothing
        dgvCategories.DataSource = categories.Select(Function(c) New With {
            Key .ID = c.CategoryId,
            .Code = c.CategoryCode,
            .Name = c.CategoryName
        }).ToList()

        If dgvCategories.Columns.Contains("ID") Then
            dgvCategories.Columns("ID").Visible = False
        End If
        If dgvCategories.Columns.Contains("Code") Then
            dgvCategories.Columns("Code").HeaderText = "Category Code"
            dgvCategories.Columns("Code").FillWeight = 30
        End If
        If dgvCategories.Columns.Contains("Name") Then
            dgvCategories.Columns("Name").HeaderText = "Category Name"
            dgvCategories.Columns("Name").FillWeight = 70
        End If
    End Sub

    Private Sub dgvCategories_SelectionChanged(sender As Object, e As EventArgs) Handles dgvCategories.SelectionChanged
        If dgvCategories.SelectedRows.Count = 0 Then Return

        Dim row = dgvCategories.SelectedRows(0)
        _editingId = CInt(row.Cells("ID").Value)
        txtCode.Text = row.Cells("Code").Value.ToString()
        txtName.Text = row.Cells("Name").Value.ToString()
        lblFormTitle.Text = "Edit Category"
        btnSave.Text = "💾 Update"

        ' Prevent code editing for in-use categories
        Dim inUse = _categoryRepo.IsCategoryInUse(txtCode.Text)
        txtCode.ReadOnly = inUse
        If inUse Then
            txtCode.BackColor = Drawing.Color.FromArgb(255, 248, 220)
        Else
            txtCode.BackColor = Drawing.Color.White
        End If
    End Sub
#End Region

#Region "Save"
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim code = txtCode.Text.Trim().ToUpper()
        Dim name = txtName.Text.Trim()

        ' Validate
        If String.IsNullOrWhiteSpace(code) Then
            MessageBox.Show("Category Code is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtCode.Focus()
            Return
        End If
        If String.IsNullOrWhiteSpace(name) Then
            MessageBox.Show("Category Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName.Focus()
            Return
        End If

        Try
            If _editingId = -1 Then
                ' — Add —
                Dim existing = _categoryRepo.GetByCode(code)
                If existing IsNot Nothing Then
                    MessageBox.Show($"Category code '{code}' already exists.", "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                Dim newCat As New Category With {.CategoryCode = code, .CategoryName = name}
                _categoryRepo.Add(newCat)
                _auditService.LogAction("CATEGORY_ADDED", SessionManager.CurrentUserId, $"Added category: {code} - {name}", "petty_cash_categories", 0)
                MessageBox.Show($"Category '{code} - {name}' added successfully.", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' — Update —
                Dim cat As New Category With {.CategoryId = _editingId, .CategoryCode = code, .CategoryName = name}
                _categoryRepo.Update(cat)
                _auditService.LogAction("CATEGORY_UPDATED", SessionManager.CurrentUserId, $"Updated category ID {_editingId} to: {code} - {name}", "petty_cash_categories", _editingId)
                MessageBox.Show($"Category updated successfully.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ClearForm()
            LoadGrid()

        Catch ex As Exception
            MessageBox.Show($"Error saving category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Delete"
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvCategories.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a category to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row = dgvCategories.SelectedRows(0)
        Dim catId = CInt(row.Cells("ID").Value)
        Dim code = row.Cells("Code").Value.ToString()
        Dim name = row.Cells("Name").Value.ToString()

        ' Check if in use
        If _categoryRepo.IsCategoryInUse(code) Then
            MessageBox.Show($"Cannot delete '{code} - {name}' because it is referenced by existing expense entries.",
                            "Category In Use", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Confirm
        If MessageBox.Show($"Are you sure you want to delete category '{code} - {name}'?",
                           "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                _categoryRepo.Delete(catId)
                _auditService.LogAction("CATEGORY_DELETED", SessionManager.CurrentUserId, $"Deleted category: {code} - {name}", "petty_cash_categories", catId)
                ClearForm()
                LoadGrid()
                MessageBox.Show("Category deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show($"Error deleting category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
#End Region

#Region "Clear / Close"
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub ClearForm()
        _editingId = -1
        txtCode.Text = ""
        txtName.Text = ""
        txtCode.ReadOnly = False
        txtCode.BackColor = Drawing.Color.White
        lblFormTitle.Text = "Add New Category"
        btnSave.Text = "💾 Save"
        dgvCategories.ClearSelection()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
#End Region

End Class
