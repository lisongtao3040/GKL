<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnSyuturyoku = New System.Windows.Forms.Button()
        Me.tbxMakeNo = New System.Windows.Forms.TextBox()
        Me.tbxLineCd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbxSavePath = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbxPassword = New System.Windows.Forms.TextBox()
        Me.tbxUser = New System.Windows.Forms.TextBox()
        Me.btnLoginIn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnSyuturyoku
        '
        Me.btnSyuturyoku.Enabled = False
        Me.btnSyuturyoku.Location = New System.Drawing.Point(425, 171)
        Me.btnSyuturyoku.Name = "btnSyuturyoku"
        Me.btnSyuturyoku.Size = New System.Drawing.Size(75, 23)
        Me.btnSyuturyoku.TabIndex = 0
        Me.btnSyuturyoku.Text = "出力"
        Me.btnSyuturyoku.UseVisualStyleBackColor = True
        '
        'tbxMakeNo
        '
        Me.tbxMakeNo.Location = New System.Drawing.Point(81, 130)
        Me.tbxMakeNo.Name = "tbxMakeNo"
        Me.tbxMakeNo.Size = New System.Drawing.Size(411, 20)
        Me.tbxMakeNo.TabIndex = 1
        Me.tbxMakeNo.Text = "076010082,076010083"
        '
        'tbxLineCd
        '
        Me.tbxLineCd.Enabled = False
        Me.tbxLineCd.Location = New System.Drawing.Point(81, 104)
        Me.tbxLineCd.Name = "tbxLineCd"
        Me.tbxLineCd.Size = New System.Drawing.Size(100, 20)
        Me.tbxLineCd.TabIndex = 2
        Me.tbxLineCd.Text = "SRM1312A"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "生产线："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 137)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "作番："
        '
        'tbxSavePath
        '
        Me.tbxSavePath.Location = New System.Drawing.Point(81, 39)
        Me.tbxSavePath.Name = "tbxSavePath"
        Me.tbxSavePath.Size = New System.Drawing.Size(411, 20)
        Me.tbxSavePath.TabIndex = 5
        Me.tbxSavePath.Text = "C:\GKL_DATA\"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "保存路径"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(190, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "密码："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "用户："
        '
        'tbxPassword
        '
        Me.tbxPassword.Location = New System.Drawing.Point(233, 12)
        Me.tbxPassword.Name = "tbxPassword"
        Me.tbxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tbxPassword.Size = New System.Drawing.Size(100, 20)
        Me.tbxPassword.TabIndex = 14
        '
        'tbxUser
        '
        Me.tbxUser.Location = New System.Drawing.Point(74, 12)
        Me.tbxUser.Name = "tbxUser"
        Me.tbxUser.Size = New System.Drawing.Size(100, 20)
        Me.tbxUser.TabIndex = 13
        '
        'btnLoginIn
        '
        Me.btnLoginIn.Location = New System.Drawing.Point(339, 12)
        Me.btnLoginIn.Name = "btnLoginIn"
        Me.btnLoginIn.Size = New System.Drawing.Size(75, 23)
        Me.btnLoginIn.TabIndex = 17
        Me.btnLoginIn.Text = "LogIn"
        Me.btnLoginIn.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 206)
        Me.Controls.Add(Me.btnLoginIn)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbxPassword)
        Me.Controls.Add(Me.tbxUser)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbxSavePath)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbxLineCd)
        Me.Controls.Add(Me.tbxMakeNo)
        Me.Controls.Add(Me.btnSyuturyoku)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "帐票"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSyuturyoku As System.Windows.Forms.Button
    Friend WithEvents tbxMakeNo As System.Windows.Forms.TextBox
    Friend WithEvents tbxLineCd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbxSavePath As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tbxPassword As System.Windows.Forms.TextBox
    Friend WithEvents tbxUser As System.Windows.Forms.TextBox
    Friend WithEvents btnLoginIn As System.Windows.Forms.Button

End Class
