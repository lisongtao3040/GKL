<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PicCapture = New System.Windows.Forms.PictureBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.sfdImage = New System.Windows.Forms.SaveFileDialog()
        Me.lstDevices = New System.Windows.Forms.ComboBox()
        CType(Me.PicCapture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'PicCapture
        '
        resources.ApplyResources(Me.PicCapture, "PicCapture")
        Me.PicCapture.Name = "PicCapture"
        Me.PicCapture.TabStop = False
        '
        'btnStart
        '
        resources.ApplyResources(Me.btnStart, "btnStart")
        Me.btnStart.Name = "btnStart"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnStop
        '
        resources.ApplyResources(Me.btnStop, "btnStop")
        Me.btnStop.Name = "btnStop"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        resources.ApplyResources(Me.btnSave, "btnSave")
        Me.btnSave.Name = "btnSave"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lstDevices
        '
        Me.lstDevices.FormattingEnabled = True
        resources.ApplyResources(Me.lstDevices, "lstDevices")
        Me.lstDevices.Name = "lstDevices"
        '
        'Form1
        '
        Me.AcceptButton = Me.btnStart
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.lstDevices)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.PicCapture)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "Form1"
        CType(Me.PicCapture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PicCapture As System.Windows.Forms.PictureBox
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents sfdImage As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lstDevices As System.Windows.Forms.ComboBox
End Class
