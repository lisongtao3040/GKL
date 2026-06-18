<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.label1 = New System.Windows.Forms.Label()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.start = New System.Windows.Forms.Button()
        Me.rfsh = New System.Windows.Forms.Button()
        Me.comboBox1 = New System.Windows.Forms.ComboBox()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PicCapture = New System.Windows.Forms.PictureBox()
        Me.groupBox1.SuspendLayout
        CType(Me.pictureBox1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.PicCapture,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'label1
        '
        Me.label1.AutoSize = true
        Me.label1.Location = New System.Drawing.Point(10, 13)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(77, 12)
        Me.label1.TabIndex = 3
        Me.label1.Text = "选择视频来源"
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.label2)
        Me.groupBox1.Location = New System.Drawing.Point(12, 120)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(166, 35)
        Me.groupBox1.TabIndex = 10
        Me.groupBox1.TabStop = false
        '
        'label2
        '
        Me.label2.AutoSize = true
        Me.label2.Location = New System.Drawing.Point(3, 10)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(89, 12)
        Me.label2.TabIndex = 0
        Me.label2.Text = "视频设备状态.."
        '
        'start
        '
        Me.start.Location = New System.Drawing.Point(100, 83)
        Me.start.Name = "start"
        Me.start.Size = New System.Drawing.Size(68, 29)
        Me.start.TabIndex = 9
        Me.start.Text = "&开始"
        Me.start.UseVisualStyleBackColor = true
        '
        'rfsh
        '
        Me.rfsh.Location = New System.Drawing.Point(12, 83)
        Me.rfsh.Name = "rfsh"
        Me.rfsh.Size = New System.Drawing.Size(68, 29)
        Me.rfsh.TabIndex = 8
        Me.rfsh.Text = "&刷新"
        Me.rfsh.UseVisualStyleBackColor = true
        '
        'comboBox1
        '
        Me.comboBox1.FormattingEnabled = true
        Me.comboBox1.Location = New System.Drawing.Point(11, 50)
        Me.comboBox1.Name = "comboBox1"
        Me.comboBox1.Size = New System.Drawing.Size(167, 20)
        Me.comboBox1.TabIndex = 7
        '
        'pictureBox1
        '
        Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pictureBox1.Location = New System.Drawing.Point(203, 12)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(436, 360)
        Me.pictureBox1.TabIndex = 11
        Me.pictureBox1.TabStop = false
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(217, 499)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'PicCapture
        '
        Me.PicCapture.Location = New System.Drawing.Point(795, 83)
        Me.PicCapture.Name = "PicCapture"
        Me.PicCapture.Size = New System.Drawing.Size(527, 426)
        Me.PicCapture.TabIndex = 13
        Me.PicCapture.TabStop = false
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 12!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1447, 729)
        Me.Controls.Add(Me.PicCapture)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.start)
        Me.Controls.Add(Me.rfsh)
        Me.Controls.Add(Me.comboBox1)
        Me.Controls.Add(Me.label1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.groupBox1.ResumeLayout(false)
        Me.groupBox1.PerformLayout
        CType(Me.pictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PicCapture,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents start As System.Windows.Forms.Button
    Private WithEvents rfsh As System.Windows.Forms.Button
    Private WithEvents comboBox1 As System.Windows.Forms.ComboBox
    Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents PicCapture As System.Windows.Forms.PictureBox

End Class
