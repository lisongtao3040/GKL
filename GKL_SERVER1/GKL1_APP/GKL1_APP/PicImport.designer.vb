<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PicImport
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbxLineCd = New System.Windows.Forms.TextBox()
        Me.gv = New System.Windows.Forms.DataGridView()
        Me.FolderBD = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnFolder = New System.Windows.Forms.Button()
        Me.lblFolder = New System.Windows.Forms.Label()
        Me.tbxFolder = New System.Windows.Forms.TextBox()
        Me.btnTouroku = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnSel = New System.Windows.Forms.Button()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.tbxUser = New System.Windows.Forms.TextBox()
        Me.tbxPassword = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.gv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(441, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "生产线："
        '
        'tbxLineCd
        '
        Me.tbxLineCd.Enabled = False
        Me.tbxLineCd.Location = New System.Drawing.Point(500, 15)
        Me.tbxLineCd.Name = "tbxLineCd"
        Me.tbxLineCd.Size = New System.Drawing.Size(100, 20)
        Me.tbxLineCd.TabIndex = 1
        '
        'gv
        '
        Me.gv.AllowUserToAddRows = False
        Me.gv.AllowUserToDeleteRows = False
        Me.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gv.Location = New System.Drawing.Point(11, 41)
        Me.gv.MultiSelect = False
        Me.gv.Name = "gv"
        Me.gv.ReadOnly = True
        Me.gv.RowTemplate.Height = 23
        Me.gv.Size = New System.Drawing.Size(526, 361)
        Me.gv.TabIndex = 2
        '
        'btnFolder
        '
        Me.btnFolder.Location = New System.Drawing.Point(543, 408)
        Me.btnFolder.Name = "btnFolder"
        Me.btnFolder.Size = New System.Drawing.Size(75, 25)
        Me.btnFolder.TabIndex = 3
        Me.btnFolder.Text = "批量上传"
        Me.btnFolder.UseVisualStyleBackColor = True
        '
        'lblFolder
        '
        Me.lblFolder.AutoSize = True
        Me.lblFolder.Location = New System.Drawing.Point(28, 473)
        Me.lblFolder.Name = "lblFolder"
        Me.lblFolder.Size = New System.Drawing.Size(0, 13)
        Me.lblFolder.TabIndex = 0
        '
        'tbxFolder
        '
        Me.tbxFolder.Location = New System.Drawing.Point(12, 408)
        Me.tbxFolder.Name = "tbxFolder"
        Me.tbxFolder.Size = New System.Drawing.Size(525, 20)
        Me.tbxFolder.TabIndex = 4
        '
        'btnTouroku
        '
        Me.btnTouroku.Location = New System.Drawing.Point(624, 408)
        Me.btnTouroku.Name = "btnTouroku"
        Me.btnTouroku.Size = New System.Drawing.Size(75, 25)
        Me.btnTouroku.TabIndex = 5
        Me.btnTouroku.Text = "登录"
        Me.btnTouroku.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(543, 41)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(303, 361)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'btnSel
        '
        Me.btnSel.Location = New System.Drawing.Point(344, 14)
        Me.btnSel.Name = "btnSel"
        Me.btnSel.Size = New System.Drawing.Size(75, 25)
        Me.btnSel.TabIndex = 7
        Me.btnSel.Text = "查询"
        Me.btnSel.UseVisualStyleBackColor = True
        '
        'btnDel
        '
        Me.btnDel.Location = New System.Drawing.Point(624, 12)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(75, 25)
        Me.btnDel.TabIndex = 8
        Me.btnDel.Text = "删除"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'tbxUser
        '
        Me.tbxUser.Location = New System.Drawing.Point(48, 15)
        Me.tbxUser.Name = "tbxUser"
        Me.tbxUser.Size = New System.Drawing.Size(100, 20)
        Me.tbxUser.TabIndex = 9
        '
        'tbxPassword
        '
        Me.tbxPassword.Location = New System.Drawing.Point(218, 15)
        Me.tbxPassword.Name = "tbxPassword"
        Me.tbxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbxPassword.Size = New System.Drawing.Size(100, 20)
        Me.tbxPassword.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "用户："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(175, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "密码："
        '
        'PicImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(852, 452)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbxPassword)
        Me.Controls.Add(Me.tbxUser)
        Me.Controls.Add(Me.btnDel)
        Me.Controls.Add(Me.btnSel)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnTouroku)
        Me.Controls.Add(Me.tbxFolder)
        Me.Controls.Add(Me.btnFolder)
        Me.Controls.Add(Me.gv)
        Me.Controls.Add(Me.tbxLineCd)
        Me.Controls.Add(Me.lblFolder)
        Me.Controls.Add(Me.Label1)
        Me.Name = "PicImport"
        Me.Text = "图片MS"
        CType(Me.gv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbxLineCd As System.Windows.Forms.TextBox
    Friend WithEvents gv As System.Windows.Forms.DataGridView
    Friend WithEvents FolderBD As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnFolder As System.Windows.Forms.Button
    Friend WithEvents lblFolder As System.Windows.Forms.Label
    Friend WithEvents tbxFolder As System.Windows.Forms.TextBox
    Friend WithEvents btnTouroku As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents tbxUser As System.Windows.Forms.TextBox
    Friend WithEvents tbxPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
