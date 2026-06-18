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
        Me.btnStop = New System.Windows.Forms.Button()
        Me.comboBox_comPl = New System.Windows.Forms.ComboBox()
        Me.btnSnap = New System.Windows.Forms.Button()
        Me.btnCam = New System.Windows.Forms.Button()
        Me.PictureBoxDisplay = New System.Windows.Forms.PictureBox()
        Me.btnCamClose = New System.Windows.Forms.Button()
        Me.btnTEST = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelChrome.SuspendLayout()
        CType(Me.PictureBoxDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
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
        Me.label1.Location = New System.Drawing.Point(-1, 6)
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
        Me.label2.Location = New System.Drawing.Point(142, 6)
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
        'btnStop
        '
        Me.btnStop.Font = New System.Drawing.Font("Microsoft YaHei", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStop.Location = New System.Drawing.Point(411, 1)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(75, 35)
        Me.btnStop.TabIndex = 13
        Me.btnStop.Text = "停止"
        Me.btnStop.UseVisualStyleBackColor = True
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
        'PictureBoxDisplay
        '
        Me.PictureBoxDisplay.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.PictureBoxDisplay.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.PictureBoxDisplay.Location = New System.Drawing.Point(770, 3)
        Me.PictureBoxDisplay.Name = "PictureBoxDisplay"
        Me.PictureBoxDisplay.Size = New System.Drawing.Size(240, 240)
        Me.PictureBoxDisplay.TabIndex = 18
        Me.PictureBoxDisplay.TabStop = False
        Me.PictureBoxDisplay.Visible = False
        '
        'btnCamClose
        '
        Me.btnCamClose.Font = New System.Drawing.Font("Microsoft YaHei", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCamClose.Location = New System.Drawing.Point(555, 6)
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
        Me.btnTEST.Location = New System.Drawing.Point(831, 6)
        Me.btnTEST.Name = "btnTEST"
        Me.btnTEST.Size = New System.Drawing.Size(95, 35)
        Me.btnTEST.TabIndex = 20
        Me.btnTEST.Text = "测试"
        Me.btnTEST.UseVisualStyleBackColor = True
        Me.btnTEST.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(932, 14)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(101, 20)
        Me.TextBox1.TabIndex = 21
        Me.TextBox1.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.cbComName)
        Me.Panel1.Controls.Add(Me.label1)
        Me.Panel1.Controls.Add(Me.label2)
        Me.Panel1.Controls.Add(Me.btnConnect)
        Me.Panel1.Controls.Add(Me.btnStop)
        Me.Panel1.Controls.Add(Me.comboBox_comPl)
        Me.Panel1.Location = New System.Drawing.Point(0, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(489, 40)
        Me.Panel1.TabIndex = 22
        '
        'GooglePanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SkyBlue
        Me.ClientSize = New System.Drawing.Size(1433, 819)
        Me.Controls.Add(Me.PictureBoxDisplay)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnTEST)
        Me.Controls.Add(Me.btnCamClose)
        Me.Controls.Add(Me.btnCam)
        Me.Controls.Add(Me.btnSnap)
        Me.Controls.Add(Me.PanelChrome)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "GooglePanel"
        Me.Text = "Google"
        Me.PanelChrome.ResumeLayout(False)
        CType(Me.PictureBoxDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelChrome As System.Windows.Forms.Panel
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents cbComName As System.Windows.Forms.ComboBox
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents btnConnect As System.Windows.Forms.Button
    Private WithEvents btnStop As System.Windows.Forms.Button
    Private WithEvents comboBox_comPl As System.Windows.Forms.ComboBox
    Private WithEvents btnSnap As System.Windows.Forms.Button
    Private WithEvents btnCam As System.Windows.Forms.Button
    Friend WithEvents PictureBoxDisplay As System.Windows.Forms.PictureBox
    Private WithEvents btnCamClose As System.Windows.Forms.Button
    Private WithEvents btnTEST As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents wb As System.Windows.Forms.WebBrowser
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class
