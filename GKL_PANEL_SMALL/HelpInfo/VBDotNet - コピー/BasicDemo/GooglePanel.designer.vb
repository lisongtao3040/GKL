<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GooglePanel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GooglePanel))
        Me.PanelChrome = New System.Windows.Forms.Panel()
        Me.wb = New System.Windows.Forms.WebBrowser()
        Me.label1 = New System.Windows.Forms.Label()
        Me.cbComName = New System.Windows.Forms.ComboBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.btnStopConnect = New System.Windows.Forms.Button()
        Me.comboBox_comPl = New System.Windows.Forms.ComboBox()
        Me.btnSnap = New System.Windows.Forms.Button()
        Me.btnCam = New System.Windows.Forms.Button()
        Me.btnCamClose = New System.Windows.Forms.Button()
        Me.btnTEST = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lstDevices = New System.Windows.Forms.ComboBox()
        Me.PicCapture = New System.Windows.Forms.PictureBox()
        Me.PanelChrome.SuspendLayout
        Me.Panel1.SuspendLayout
        CType(Me.PicCapture,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'PanelChrome
        '
        Me.PanelChrome.Controls.Add(Me.wb)
        Me.PanelChrome.Location = New System.Drawing.Point(0, 50)
        Me.PanelChrome.Name = "PanelChrome"
        Me.PanelChrome.Size = New System.Drawing.Size(1033, 518)
        Me.PanelChrome.TabIndex = 0
        '
        'wb
        '
        Me.wb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wb.Location = New System.Drawing.Point(0, 0)
        Me.wb.MinimumSize = New System.Drawing.Size(20, 20)
        Me.wb.Name = "wb"
        Me.wb.Size = New System.Drawing.Size(1033, 518)
        Me.wb.TabIndex = 0
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Microsoft YaHei", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(-1, 7)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(55, 26)
        Me.label1.TabIndex = 9
        Me.label1.Text = "串口:"
        '
        'cbComName
        '
        Me.cbComName.Font = New System.Drawing.Font("Microsoft YaHei", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbComName.FormattingEnabled = True
        Me.cbComName.Location = New System.Drawing.Point(60, 3)
        Me.cbComName.Name = "cbComName"
        Me.cbComName.Size = New System.Drawing.Size(76, 30)
        Me.cbComName.TabIndex = 8
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Font = New System.Drawing.Font("Microsoft YaHei", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(142, 7)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(74, 26)
        Me.label2.TabIndex = 10
        Me.label2.Text = "波特率:"
        '
        'btnConnect
        '
        Me.btnConnect.Font = New System.Drawing.Font("Microsoft YaHei", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConnect.Location = New System.Drawing.Point(331, 1)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(74, 35)
        Me.btnConnect.TabIndex = 12
        Me.btnConnect.Text = "连接"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'btnStopConnect
        '
        Me.btnStopConnect.Font = New System.Drawing.Font("Microsoft YaHei", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStopConnect.Location = New System.Drawing.Point(411, 1)
        Me.btnStopConnect.Name = "btnStopConnect"
        Me.btnStopConnect.Size = New System.Drawing.Size(75, 35)
        Me.btnStopConnect.TabIndex = 13
        Me.btnStopConnect.Text = "停止"
        Me.btnStopConnect.UseVisualStyleBackColor = True
        '
        'comboBox_comPl
        '
        Me.comboBox_comPl.Font = New System.Drawing.Font("Microsoft YaHei", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboBox_comPl.FormattingEnabled = True
        Me.comboBox_comPl.Items.AddRange(New Object() {"19200"})
        Me.comboBox_comPl.Location = New System.Drawing.Point(222, 3)
        Me.comboBox_comPl.Name = "comboBox_comPl"
        Me.comboBox_comPl.Size = New System.Drawing.Size(103, 30)
        Me.comboBox_comPl.TabIndex = 14
        Me.comboBox_comPl.Text = "9600"
        '
        'btnSnap
        '
        Me.btnSnap.Enabled = False
        Me.btnSnap.Font = New System.Drawing.Font("Microsoft YaHei", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSnap.Location = New System.Drawing.Point(626, 5)
        Me.btnSnap.Name = "btnSnap"
        Me.btnSnap.Size = New System.Drawing.Size(140, 35)
        Me.btnSnap.TabIndex = 16
        Me.btnSnap.Text = "拍照"
        Me.btnSnap.UseVisualStyleBackColor = True
        Me.btnSnap.Visible = False
        '
        'btnCam
        '
        Me.btnCam.Font = New System.Drawing.Font("Microsoft YaHei", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCam.Location = New System.Drawing.Point(489, 5)
        Me.btnCam.Name = "btnCam"
        Me.btnCam.Size = New System.Drawing.Size(60, 35)
        Me.btnCam.TabIndex = 17
        Me.btnCam.Text = "打开摄像头"
        Me.btnCam.UseVisualStyleBackColor = True
        Me.btnCam.Visible = False
        '
        'btnCamClose
        '
        Me.btnCamClose.Font = New System.Drawing.Font("Microsoft YaHei", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCamClose.Location = New System.Drawing.Point(555, 7)
        Me.btnCamClose.Name = "btnCamClose"
        Me.btnCamClose.Size = New System.Drawing.Size(65, 35)
        Me.btnCamClose.TabIndex = 19
        Me.btnCamClose.Text = "关闭摄像头"
        Me.btnCamClose.UseVisualStyleBackColor = True
        Me.btnCamClose.Visible = False
        '
        'btnTEST
        '
        Me.btnTEST.Font = New System.Drawing.Font("Microsoft YaHei", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTEST.Location = New System.Drawing.Point(1098, 50)
        Me.btnTEST.Name = "btnTEST"
        Me.btnTEST.Size = New System.Drawing.Size(95, 35)
        Me.btnTEST.TabIndex = 20
        Me.btnTEST.Text = "测试"
        Me.btnTEST.UseVisualStyleBackColor = True
        Me.btnTEST.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cbComName)
        Me.Panel1.Controls.Add(Me.label1)
        Me.Panel1.Controls.Add(Me.label2)
        Me.Panel1.Controls.Add(Me.btnConnect)
        Me.Panel1.Controls.Add(Me.btnStopConnect)
        Me.Panel1.Controls.Add(Me.comboBox_comPl)
        Me.Panel1.Location = New System.Drawing.Point(0, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(489, 40)
        Me.Panel1.TabIndex = 22
        '
        'lstDevices
        '
        Me.lstDevices.Enabled = False
        Me.lstDevices.FormattingEnabled = True
        Me.lstDevices.Location = New System.Drawing.Point(1082, 4)
        Me.lstDevices.Name = "lstDevices"
        Me.lstDevices.Size = New System.Drawing.Size(225, 21)
        Me.lstDevices.TabIndex = 28
        Me.lstDevices.Visible = False
        '
        'PicCapture
        '
        Me.PicCapture.BackColor = System.Drawing.Color.LightCyan
        Me.PicCapture.Location = New System.Drawing.Point(626, 37)
        Me.PicCapture.Name = "PicCapture"
        Me.PicCapture.Size = New System.Drawing.Size(245, 204)
        Me.PicCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicCapture.TabIndex = 29
        Me.PicCapture.TabStop = False
        Me.PicCapture.Visible = False
        '
        'GooglePanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SkyBlue
        Me.ClientSize = New System.Drawing.Size(1433, 819)
        Me.Controls.Add(Me.PicCapture)
        Me.Controls.Add(Me.lstDevices)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnTEST)
        Me.Controls.Add(Me.btnCamClose)
        Me.Controls.Add(Me.btnCam)
        Me.Controls.Add(Me.btnSnap)
        Me.Controls.Add(Me.PanelChrome)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "GooglePanel"
        Me.Text = "Google"
        Me.PanelChrome.ResumeLayout(false)
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        CType(Me.PicCapture,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents PanelChrome As System.Windows.Forms.Panel
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents cbComName As System.Windows.Forms.ComboBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents btnConnect As System.Windows.Forms.Button
    Private WithEvents btnStopConnect As System.Windows.Forms.Button
    Private WithEvents comboBox_comPl As System.Windows.Forms.ComboBox
    Private WithEvents btnSnap As System.Windows.Forms.Button
    Private WithEvents btnCam As System.Windows.Forms.Button
    Private WithEvents btnCamClose As System.Windows.Forms.Button
    Private WithEvents btnTEST As System.Windows.Forms.Button
    Friend WithEvents wb As System.Windows.Forms.WebBrowser
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lstDevices As System.Windows.Forms.ComboBox
    Friend WithEvents PicCapture As System.Windows.Forms.PictureBox

End Class
