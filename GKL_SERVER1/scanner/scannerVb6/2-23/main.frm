VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{648A5603-2C6E-101B-82B6-000000000014}#1.1#0"; "MSCOMM32.OCX"
Begin VB.Form main 
   Caption         =   "021F106A-104A-108A"
   ClientHeight    =   6360
   ClientLeft      =   165
   ClientTop       =   735
   ClientWidth     =   8595
   LinkTopic       =   "Form1"
   ScaleHeight     =   6360
   ScaleWidth      =   8595
   StartUpPosition =   3  '窗口缺省
   Begin MSCommLib.MSComm MSComm1 
      Left            =   7320
      Top             =   840
      _ExtentX        =   1005
      _ExtentY        =   1005
      _Version        =   393216
      DTREnable       =   -1  'True
   End
   Begin VB.CommandButton Command4 
      Caption         =   "完全重写"
      Height          =   495
      Left            =   5760
      TabIndex        =   13
      Top             =   5400
      Width           =   1335
   End
   Begin VB.Frame Frame2 
      Caption         =   "数据栏"
      Height          =   3255
      Left            =   480
      TabIndex        =   5
      Top             =   1800
      Width           =   7695
      Begin VB.TextBox Text2 
         Height          =   1215
         Left            =   360
         TabIndex        =   10
         Top             =   1800
         Width           =   6855
      End
      Begin VB.TextBox Text1 
         Height          =   735
         Left            =   360
         TabIndex        =   9
         Top             =   600
         Width           =   6855
      End
      Begin VB.Label Label2 
         Caption         =   "第三磁道"
         Height          =   375
         Left            =   360
         TabIndex        =   12
         Top             =   1560
         Width           =   1695
      End
      Begin VB.Label Label1 
         Caption         =   "第二磁道"
         Height          =   375
         Left            =   360
         TabIndex        =   11
         Top             =   360
         Width           =   1455
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "磁道选择"
      Height          =   975
      Left            =   360
      TabIndex        =   4
      Top             =   360
      Width           =   6615
      Begin VB.OptionButton Option1 
         Caption         =   "第二磁道"
         Height          =   420
         Left            =   480
         TabIndex        =   8
         Top             =   480
         Width           =   1335
      End
      Begin VB.OptionButton Option2 
         Caption         =   "第三磁道"
         Height          =   420
         Left            =   2520
         TabIndex        =   7
         Top             =   480
         Width           =   1215
      End
      Begin VB.OptionButton Option3 
         Caption         =   "第二、三磁道"
         Height          =   540
         Left            =   4200
         TabIndex        =   6
         Top             =   360
         Width           =   2295
      End
   End
   Begin VB.CommandButton Command3 
      Caption         =   "清除"
      Height          =   495
      Left            =   4080
      TabIndex        =   3
      Top             =   5400
      Width           =   975
   End
   Begin VB.CommandButton Command2 
      Caption         =   "写数据"
      Height          =   495
      Left            =   2160
      TabIndex        =   2
      Top             =   5400
      Width           =   975
   End
   Begin VB.CommandButton Command1 
      Caption         =   "读数据"
      Height          =   495
      Left            =   480
      TabIndex        =   1
      Top             =   5400
      Width           =   975
   End
   Begin MSComctlLib.StatusBar StatusBar1 
      Align           =   2  'Align Bottom
      Height          =   315
      Left            =   0
      TabIndex        =   0
      Top             =   6045
      Width           =   8595
      _ExtentX        =   15161
      _ExtentY        =   556
      _Version        =   393216
      BeginProperty Panels {8E3867A5-8586-11D1-B16A-00C0F0283628} 
         NumPanels       =   3
         BeginProperty Panel1 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            AutoSize        =   1
            Bevel           =   0
            Object.Width           =   9895
         EndProperty
         BeginProperty Panel2 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
         EndProperty
         BeginProperty Panel3 {8E3867AB-8586-11D1-B16A-00C0F0283628} 
            Object.Width           =   2117
            MinWidth        =   2117
         EndProperty
      EndProperty
   End
   Begin VB.Menu bianji 
      Caption         =   "编辑"
      Begin VB.Menu chuankou 
         Caption         =   "串口"
      End
      Begin VB.Menu fuwei 
         Caption         =   "复位"
         Shortcut        =   ^F
      End
   End
   Begin VB.Menu shuju 
      Caption         =   "数据"
      Begin VB.Menu duru 
         Caption         =   "读入"
         Shortcut        =   ^D
      End
      Begin VB.Menu xieka 
         Caption         =   "写卡"
         Shortcut        =   ^X
      End
   End
   Begin VB.Menu exit 
      Caption         =   "退出"
   End
   Begin VB.Menu bangzhu 
      Caption         =   "帮助"
   End
End
Attribute VB_Name = "main"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim bReady As Boolean
Dim sReadData As String
Dim iRWMode As Integer  '磁道选择标志
Dim a2 As Integer     '第2磁道的数据位数
Dim a3 As Integer     '第3磁道的数据位数

Private Sub Form_Load()
MSComm1.PortOpen = True
End Sub


Public Sub SoftReset()
  Dim sCmd As String
  sCmd = Chr(&H1B) + Chr(&H30)
  SendData (sCmd)
End Sub

Public Sub SendData(sData As String)
    MSComm1.Output = sData
End Sub
Public Sub ReadProcess(cRData As String)
    Dim cRTChr As String
    sReadData = ""
 Do
    cRData = MSComm1.Input
    sReadData = sReadData + cRData
    cRTChr = Right(sReadData, 2)
    Select Case cRTChr
    Case Chr(&H72) + Chr(&H70)
        '接收到正确的读写信号
        bReady = True
    Case Chr(&H72) + Chr(&H71)
        '接收到读写不正确信号
        bReady = False
        Exit Sub
    End Select
 Loop While cRTChr <> Chr(&H72) + Chr(&H70)
End Sub
Public Sub resetting()
    Dim sCmd As String
    sCmd = Chr(&H1B) + Chr(&H53)
    StatusBar1.Panels(1).Text = "就绪"
    SendData sCmd
End Sub
Public Sub ClearSPort()
    MSComm1.InBufferCount = 0
    MSComm1.OutBufferCount = 0
End Sub
Public Sub Prompt(msg As String)
    StatusBar1.Panels(1).Text = msg
End Sub

'读取数据按钮
Private Sub Command1_Click()
Dim sReadCmd As String
MSComm1.OutBufferCount = 0
MSComm1.InBufferCount = 0
MSComm1.RThreshold = 3
        Select Case iRWMode
            Case 1
                sReadCmd = Chr$(&H1B) & Chr$(&H5D) & Chr$(&H1B) & Chr$(&H6A)
                 main.SendData (sReadCmd)
                 Prompt "请刷卡..."
            Case 2
                sReadCmd = Chr$(&H1B) & Chr$(&H54) & Chr$(&H5D) & Chr$(&H1B) & Chr$(&H6A)
                 main.SendData (sReadCmd)
                Prompt "请刷卡..."
            Case 3
                sReadCmd = Chr$(&H1B) & Chr$(&H42) & Chr$(&H5D) & Chr$(&H1B) & Chr$(&H6A)
                 main.SendData (sReadCmd)
                 Prompt "请刷卡..."
            Case Else
                MsgBox "请选择磁道" + Err.Description, vbCritical, "提示"
        End Select
              
End Sub

'写入数据按钮
Private Sub Command2_Click()
 Dim sWriteCmd As String  '写数据命令
 Dim sWriData As String     '写入的数据
 Dim sWriData2 As String   '写入第三磁道的数据
 Dim sNotifyMsg As String    '提示信息
    
    ClearSPort
    '确认是否要写入文本框中的数据
    If MsgBox("确定要写入数据吗？", vbQuestion + vbYesNo) = vbYes Then
            bReady = False
            sWriteCmd = ""
            Select Case iRWMode
                Case 1          '写入第二轨道的数据
                    If Len(Text1.Text) < 1 Then
                        MsgBox "请先输入第二磁道中的数据"
                    Else
                        sWriData = Text1.Text
                        sWriteCmd = Chr$(&H1B) & Chr$(&H74) & sWriData & _
                            Chr$(&H1D) & Chr$(&H1B) & Chr$(&H5C) & Chr$(&H1B) & Chr$(&H6A)
                    End If
                Case 2          '写入第三轨道的数据
                    If Len(Text2.Text) < 1 Then
                        MsgBox "请先输入第三磁道中的数据"
                    Else
                        sWriData = Text2.Text
                        sWriteCmd = Chr$(&H1B) & Chr$(&H74) & "A" & sWriData & _
                            Chr$(&H1D) & Chr$(&H1B) & Chr$(&H5C) & Chr$(&H1B) & Chr$(&H6A)
                    End If
                Case 3          '写入第二、三轨道的数据
                    If Len(Text1.Text) < 1 Or Len(Text2.Text) < 1 Then
                        MsgBox "请先输入第二、三磁道中的数据"
                    Else
                        sWriData = Text1.Text
                        sWriData2 = Text2.Text
                        sWriteCmd = Chr$(&H1B) & Chr$(&H74) & sWriData & "A" & sWriData2 _
                           & Chr$(&H1D) & Chr$(&H1B) & Chr$(&H5C) & Chr$(&H1B) & Chr$(&H6A)
                    End If
            End Select
            Prompt "请刷卡..."
            main.SendData (sWriteCmd)    '发送数据
            ReadProcess (cRData)
            If bReady = False Then
                Select Case iRWMode
                    Case 1
                        sNotifyMsg = "写第二磁道错误"
                    Case 2
                        sNotifyMsg = "写第三磁道错误"
                    Case 3
                        sNotifyMsg = "写第二、三磁道错误"
                End Select
                MsgBox sNotifyMsg
            Else
                Prompt "写卡成功!"
            End If
    End If
End Sub

'当commevent改变时
Private Sub MSComm1_OnComm()
Dim sRData As String
Dim sRData2 As String   '同时读取二三磁道数据时，用于保存第三磁道的数据
Dim lPos As Long        '第三磁道的数据在整个数据中的位置
    Select Case MSComm1.CommEvent
    Case comEvReceive    '接收数据
       ReadProcess (sRData)
         If bReady Then
            Select Case iRWMode
            Case 1
                '显示所读取的第二磁道的数据
                sReadData = Mid(sReadData, 3, Len(sReadData) - 7)
                Text1.Text = Text1.Text + sReadData
            Case 2
                '显示所读取的第三磁道的数据
                sReadData = Mid(sReadData, 4, Len(sReadData) - 9)
                Text2.Text = Text2.Text + sReadData
            Case 3
                '显示所读取的第二、三磁道的数据
                lPos = InStr(1, sReadData, "A", vbTextCompare)
                If lPos > 0 Then
                    sRData2 = Mid(sReadData, lPos + 1, Len(sReadData) - lPos - 6)
                Else
                    MsgBox "第三磁道数据不正确。" + sReadData, vbInformation, "提示"
                End If
                sReadData = Mid(sReadData, 3, lPos - 3)
                Text1.Text = Text1.Text + sReadData
                Text2.Text = Text2.Text + sRData2
            End Select
        Else
                Select Case iRWMode
                Case 1
                    sNotifyMsg = "读写第二磁道错误"
                Case 2
                    sNotifyMsg = "读写第三磁道错误"
                Case 3
                    sNotifyMsg = "读写第二、三磁道错误"
                End Select
                SoftReset
                MsgBox sNotifyMsg
        End If
    End Select
     StatusBar1.Panels(1).Text = "就绪"
End Sub

'磁道选择
Private Sub Option1_Click()
 If Option1.Value Then
iRWMode = 1
End If
End Sub
Private Sub Option2_Click()
iRWMode = 2
End Sub
Private Sub Option3_Click()
iRWMode = 3
End Sub


'清除
Private Sub Command3_Click()
Text1.Text = ""
Text1.Text = ""
End Sub
Private Sub Command4_Click()
a2 = 0
a3 = 0
Text1.Text = ""
Text2.Text = ""
End Sub


'上层选择
Private Sub duru_Click()
Frame2.Caption = "读数据"
Text1.Text = ""
Text2.Text = ""
Text1.Enabled = False
Text2.Enabled = False
 StatusBar1.Panels(1).Text = "就绪"
Command1.Enabled = True
Command2.Enabled = False
Command3.Enabled = True
End Sub
Private Sub bangzhu_Click()
help.Show
End Sub
Private Sub chuankou_Click()
linkset.Show
Me.Hide
End Sub
Private Sub fuwei_Click()
resetting
End Sub
Private Sub exit_Click()
End
End Sub
Private Sub xieka_Click()
Frame2.Caption = "写数据"
Text1.Text = ""
Text2.Text = ""
Text1.Enabled = True
Text2.Enabled = True
 StatusBar1.Panels(1).Text = "就绪"
Command1.Enabled = False
Command2.Enabled = True
Command3.Enabled = True
End Sub


'文本点击
Private Sub Text1_KeyPress(KeyAscii As Integer)
    Select Case KeyAscii
    Case 48 To 58
        If a2 < 38 Then
            a2 = a2 + 1
        Else
            KeyAscii = 0
        End If
    Case 8
        If a2 > 0 Then
            a2 = a2 - 1
        End If
        
    Case Else
        KeyAscii = 0
    End Select
    StatusBar1.Panels(2).Text = Str(a2) + "/" + "38"
End Sub

Private Sub Text2_KeyPress(KeyAscii As Integer)
    Select Case KeyAscii
    Case 48 To 58
        If a3 < 110 Then
            a3 = a3 + 1
        Else
            KeyAscii = 0
        End If
    Case 8
        If a3 > 0 Then
            a3 = a3 - 1
        End If
        
    Case Else
        KeyAscii = 0
    End Select
    StatusBar1.Panels(3).Text = Str(a3) + "/" + "110"
End Sub
