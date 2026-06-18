<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainWinForm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbbCOM = New System.Windows.Forms.ComboBox()
        Me.gvMainData = New System.Windows.Forms.DataGridView()
        Me.COMPUTER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.F_SIGN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.F_VALUE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.INS_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UPD_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IDX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UpdateDate = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.DeleteData = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.gvMainData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "COM名"
        '
        'cbbCOM
        '
        Me.cbbCOM.FormattingEnabled = True
        Me.cbbCOM.Items.AddRange(New Object() {"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8"})
        Me.cbbCOM.Location = New System.Drawing.Point(15, 25)
        Me.cbbCOM.Name = "cbbCOM"
        Me.cbbCOM.Size = New System.Drawing.Size(121, 21)
        Me.cbbCOM.TabIndex = 1
        '
        'gvMainData
        '
        Me.gvMainData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gvMainData.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.COMPUTER, Me.F_SIGN, Me.F_VALUE, Me.INS_DATE, Me.UPD_DATE, Me.IDX, Me.UpdateDate, Me.DeleteData})
        Me.gvMainData.Location = New System.Drawing.Point(15, 68)
        Me.gvMainData.Name = "gvMainData"
        Me.gvMainData.Size = New System.Drawing.Size(745, 173)
        Me.gvMainData.TabIndex = 2
        '
        'COMPUTER
        '
        Me.COMPUTER.HeaderText = "电脑名"
        Me.COMPUTER.Name = "COMPUTER"
        '
        'F_SIGN
        '
        Me.F_SIGN.HeaderText = "符号"
        Me.F_SIGN.Name = "F_SIGN"
        '
        'F_VALUE
        '
        Me.F_VALUE.HeaderText = "值"
        Me.F_VALUE.Name = "F_VALUE"
        '
        'INS_DATE
        '
        Me.INS_DATE.HeaderText = "登录时间"
        Me.INS_DATE.Name = "INS_DATE"
        '
        'UPD_DATE
        '
        Me.UPD_DATE.HeaderText = "更新时间"
        Me.UPD_DATE.Name = "UPD_DATE"
        '
        'IDX
        '
        Me.IDX.HeaderText = "ID"
        Me.IDX.Name = "IDX"
        Me.IDX.Visible = False
        '
        'UpdateDate
        '
        Me.UpdateDate.HeaderText = ""
        Me.UpdateDate.Name = "UpdateDate"
        Me.UpdateDate.Text = "选择"
        '
        'DeleteData
        '
        Me.DeleteData.HeaderText = ""
        Me.DeleteData.Name = "DeleteData"
        Me.DeleteData.Text = "删除"
        '
        'MainWinForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(864, 419)
        Me.Controls.Add(Me.gvMainData)
        Me.Controls.Add(Me.cbbCOM)
        Me.Controls.Add(Me.Label1)
        Me.Name = "MainWinForm"
        Me.Text = "Form1"
        CType(Me.gvMainData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbbCOM As System.Windows.Forms.ComboBox
    Friend WithEvents gvMainData As System.Windows.Forms.DataGridView
    Friend WithEvents COMPUTER As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents F_SIGN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents F_VALUE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents INS_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UPD_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDX As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UpdateDate As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents DeleteData As System.Windows.Forms.DataGridViewButtonColumn

End Class
