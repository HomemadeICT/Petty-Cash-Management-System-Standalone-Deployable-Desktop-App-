<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AdminSettingsForm
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlHeader = New Panel()
        lblTitle = New Label()
        lblUserInfo = New Label()
        pnlContent = New FlowLayoutPanel()
        pnlFooter = New Panel()
        btnClose = New Button()
        pnlHeader.SuspendLayout()
        pnlFooter.SuspendLayout()
        SuspendLayout()
        '
        ' pnlHeader
        '
        pnlHeader.BackColor = Color.FromArgb(CByte(0), CByte(51), CByte(102))
        pnlHeader.Controls.Add(lblTitle)
        pnlHeader.Controls.Add(lblUserInfo)
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Location = New Point(0, 0)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Size = New Size(760, 88)
        pnlHeader.TabIndex = 0
        '
        ' lblTitle
        '
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(22, 14)
        lblTitle.Name = "lblTitle"
        lblTitle.TabIndex = 0
        lblTitle.Text = "⚙  Settings"
        '
        ' lblUserInfo
        '
        lblUserInfo.AutoSize = True
        lblUserInfo.Font = New Font("Segoe UI", 9F, FontStyle.Italic)
        lblUserInfo.ForeColor = Color.FromArgb(CByte(180), CByte(210), CByte(255))
        lblUserInfo.Location = New Point(25, 60)
        lblUserInfo.Name = "lblUserInfo"
        lblUserInfo.TabIndex = 1
        lblUserInfo.Text = "Logged in as: ..."
        '
        ' pnlContent
        '
        pnlContent.AutoScroll = True
        pnlContent.BackColor = Color.FromArgb(CByte(236), CByte(239), CByte(248))
        pnlContent.Dock = DockStyle.Fill
        pnlContent.FlowDirection = FlowDirection.LeftToRight
        pnlContent.Location = New Point(0, 88)
        pnlContent.Name = "pnlContent"
        pnlContent.Padding = New Padding(20, 22, 10, 10)
        pnlContent.Size = New Size(760, 412)
        pnlContent.TabIndex = 1
        pnlContent.WrapContents = True
        '
        ' pnlFooter
        '
        pnlFooter.BackColor = Color.FromArgb(CByte(220), CByte(223), CByte(235))
        pnlFooter.Controls.Add(btnClose)
        pnlFooter.Dock = DockStyle.Bottom
        pnlFooter.Location = New Point(0, 500)
        pnlFooter.Name = "pnlFooter"
        pnlFooter.Size = New Size(760, 56)
        pnlFooter.TabIndex = 2
        '
        ' btnClose
        '
        btnClose.Anchor = AnchorStyles.Right
        btnClose.BackColor = Color.FromArgb(CByte(100), CByte(110), CByte(130))
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.Location = New Point(635, 12)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(110, 34)
        btnClose.TabIndex = 0
        btnClose.Text = "Close"
        btnClose.UseVisualStyleBackColor = False
        '
        ' AdminSettingsForm
        '
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(236), CByte(239), CByte(248))
        ClientSize = New Size(760, 556)
        Controls.Add(pnlContent)
        Controls.Add(pnlFooter)
        Controls.Add(pnlHeader)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(3, 4, 3, 4)
        MaximizeBox = False
        MinimizeBox = False
        Name = "AdminSettingsForm"
        StartPosition = FormStartPosition.CenterParent
        Text = "Settings"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        pnlFooter.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblUserInfo As Label
    Friend WithEvents pnlContent As FlowLayoutPanel
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents btnClose As Button

End Class
