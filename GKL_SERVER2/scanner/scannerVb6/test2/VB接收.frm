VERSION 5.00
Object = "{648A5603-2C6E-101B-82B6-000000000014}#1.1#0"; "MSCOMM32.OCX"
Begin VB.Form Form1 
   Caption         =   "Scan"
   ClientHeight    =   2820
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   7800
   LinkTopic       =   "Form1"
   ScaleHeight     =   2820
   ScaleWidth      =   7800
   StartUpPosition =   3  '窗口缺省
   Begin VB.CommandButton btnClose 
      Caption         =   "关闭"
      Height          =   975
      Left            =   5640
      TabIndex        =   7
      Top             =   240
      Width           =   2055
   End
   Begin VB.CommandButton btnConnCom 
      Caption         =   "连接"
      Height          =   975
      Left            =   3120
      TabIndex        =   6
      Top             =   240
      Width           =   2055
   End
   Begin VB.ComboBox cbPI 
      Height          =   300
      ItemData        =   "VB接收.frx":0000
      Left            =   1800
      List            =   "VB接收.frx":000A
      TabIndex        =   5
      Text            =   "9600"
      Top             =   840
      Width           =   1215
   End
   Begin VB.ComboBox Combo1 
      Height          =   300
      Left            =   1800
      TabIndex        =   2
      Text            =   "Combo1"
      Top             =   360
      Width           =   1215
   End
   Begin MSCommLib.MSComm MSComm1 
      Left            =   4320
      Top             =   1440
      _ExtentX        =   1005
      _ExtentY        =   1005
      _Version        =   393216
      CommPort        =   3
      DTREnable       =   -1  'True
      RThreshold      =   1
      BaudRate        =   19200
   End
   Begin VB.TextBox Text1 
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   15
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   240
      TabIndex        =   0
      Top             =   2280
      Width           =   7455
   End
   Begin VB.Label Label4 
      Caption         =   "波特率"
      Height          =   255
      Left            =   240
      TabIndex        =   4
      Top             =   840
      Width           =   1215
   End
   Begin VB.Label Label3 
      Caption         =   "可用串口"
      Height          =   255
      Left            =   240
      TabIndex        =   3
      Top             =   360
      Width           =   1215
   End
   Begin VB.Label Label1 
      Caption         =   "状态"
      BeginProperty Font 
         Name            =   "宋体"
         Size            =   21.75
         Charset         =   134
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   675
      Left            =   240
      TabIndex        =   1
      Top             =   1440
      Width           =   3375
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

Private PortIdx As Integer

Private barcode As String

'//开始Com连接
Private Sub btnConnCom_Click()

    Call ConnectCom
        

End Sub

Private Sub ConnectCom()
On Error GoTo ErrHL

    MSComm1.CommPort = Val(Me.Combo1.Text)
    MSComm1.Settings = Me.cbPI.Text & ",n,8,1"
    If MSComm1.PortOpen = False Then
        MSComm1.PortOpen = True
        MSComm1.Output = "OK"
        Label1.Caption = "Com3打开正常"
        Call SetBtnStaus(True)
        MsgBox "连接成功Com" & Val(Me.Combo1.Text) & "，程序将最小化"
        
        Me.WindowState = vbMinimized
        
    Else
        Label1.Caption = "Com3已打开"
        MSComm1.Output = "Fail"
        Call SetBtnStaus(False)
    End If
    
    Exit Sub
    
ErrHL:
    MsgBox Err.Description

End Sub

Private Sub SetBtnStaus(ByVal connKbn)
    Me.btnConnCom.Enabled = Not connKbn
    Me.btnClose.Enabled = connKbn
    Me.cbPI.Enabled = Not connKbn
    Me.Combo1.Enabled = Not connKbn
    Me.Text1.Enabled = Not connKbn
    If connKbn Then
        Label1.Caption = "Com已打开"
    Else
        Label1.Caption = "Com已关闭"
    End If
    

End Sub

'关闭连接
Private Sub btnClose_Click()
    Call SetBtnStaus(False)
    If MSComm1.PortOpen Then MSComm1.PortOpen = False
End Sub


Private Sub Form_Load()
    '扫描计算机的前16个端口
    Call GetAvailablePorts
    
    Call ConnectCom
    
    'Me.WindowState = vbNormal
    
End Sub
Private Sub Form_Unload(Cancel As Integer)
    If MSComm1.PortOpen Then MSComm1.PortOpen = False
End Sub

'---------------------------
'扫描计算机的前16个端口
'---------------------------
Private Sub GetAvailablePorts()
    Dim i As Integer
    Dim blnNoPort As Boolean
    
    PortIdx = 0
    With Combo1
        .Clear
        '.AddItem ("请选择COM口")
        
        '尝试打开COM1到COM16
        For i = 1 To 16
            MSComm1.CommPort = i
            '打开错误陷阱
            On Error Resume Next
            MSComm1.PortOpen = True
            '如果串口被成功打开，则这个串口存在
            If Err.Number = 0 Then
                .AddItem i
                .ItemData(.NewIndex) = i
                If PortIdx = 0 Then PortIdx = i
            End If
            Err.Clear
            ' 关闭已打开的串口
            If MSComm1.PortOpen Then MSComm1.PortOpen = False
            ' 关闭错误陷阱
            On Error GoTo 0
        Next
        blnNoPort = .ListCount = 0
    End With
     
    If blnNoPort Then
        MsgBox "计算机上没有串行通信接口"
    Else
        Me.Combo1.Text = PortIdx
    End If
End Sub



Private Sub MSComm1_OnComm()

On Error GoTo ErrHL
    Dim i As Integer
    Dim S() As Byte
    Dim SS(1024) As Byte
    Static N As Long
    Static T As Variant
    Dim jg
    If (MSComm1.CommEvent = comEvEOF) Then
        MsgBox 1
        
    End If

    If MSComm1.InputMode = comInputModeBinary Then
   
        S = MSComm1.Input
        If (MSComm1.CommEvent = comEvReceive) Then
                                 '只要有数据就收进来，哪怕只是一个
            If (Timer - T > 0.1) Then            '间隔10MS以上就认为是一个新的包
                Text1 = ""                     'text1用于搜集和显示接收(HEX格式)
                N = 0
            End If
            T = Timer
            For i = 0 To UBound(S)               '一个数据包可能产生若干个oncomm事件
                Text1.Text = Text1.Text & Right("0" & Hex(S(i)), 2) + " "
                SS(N + i) = S(i)                 '接收数据包缓存于SS()
                N = N + UBound(S)
            Next i
        End If
    Else
    
    
        
'        If Len(barcode) > 300 Then
'            barcode = ""
'        End If
        If (MSComm1.CommEvent = comEvReceive) Then
        
            


                jg = Timer - T
                If (jg > 0.4) Then            '间隔10MS以上就认为是一个新的包
                    barcode = ""                     'text1用于搜集和显示接收(HEX格式)
                End If
                
                barcode = barcode & MSComm1.Input

                T = Timer
                
                If InStr(barcode, vbCr) > 0 Or InStr(barcode, vbLf) > 0 Then
                    If barcode <> "" Then
                        barcode = Replace(barcode, vbCr, "")
                        barcode = Replace(barcode, vbLf, "")
                    
                        Text1.Text = barcode
                        Me.btnClose.Enabled = False
                        SendKeys ("{F8}")
                        Call Sleep(300)
                        SendKeys (barcode)
                        SendKeys ("{ENTER}")
                        Me.btnClose.Enabled = True
                    End If
                      
                    'barcode = ""
                End If
        End If
        


    End If
    Exit Sub
    
ErrHL:
    MsgBox Err.Description

End Sub

