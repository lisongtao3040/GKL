VERSION 5.00
Begin VB.Form DebugDlg 
   BorderStyle     =   3  'Fixed Dialog
   Caption         =   "对话框标题"
   ClientHeight    =   3465
   ClientLeft      =   2760
   ClientTop       =   3750
   ClientWidth     =   4560
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   3465
   ScaleWidth      =   4560
   ShowInTaskbar   =   0   'False
   Begin VB.CommandButton Command2 
      Caption         =   "清除"
      Height          =   375
      Left            =   240
      TabIndex        =   5
      Top             =   1320
      Width           =   975
   End
   Begin VB.Timer Timer1 
      Left            =   255
      Top             =   2760
   End
   Begin VB.CommandButton Command1 
      Caption         =   "发送"
      Height          =   300
      Left            =   225
      TabIndex        =   4
      Top             =   270
      Width           =   825
   End
   Begin VB.ComboBox Combo1 
      Height          =   300
      Left            =   225
      TabIndex        =   3
      Text            =   "Combo1"
      Top             =   615
      Width           =   1965
   End
   Begin VB.TextBox Text2 
      Height          =   2640
      Left            =   2325
      MultiLine       =   -1  'True
      ScrollBars      =   3  'Both
      TabIndex        =   1
      Top             =   615
      Width           =   2145
   End
   Begin VB.CommandButton OKButton 
      Caption         =   "确定"
      Height          =   375
      Left            =   240
      TabIndex        =   0
      Top             =   2040
      Width           =   1035
   End
   Begin VB.Label Label2 
      Caption         =   "接收"
      Height          =   210
      Left            =   2385
      TabIndex        =   2
      Top             =   360
      Width           =   570
   End
End
Attribute VB_Name = "DebugDlg"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Option Explicit
Dim bTime As Boolean

Private Sub Command1_Click()
    Dim sFmtRData As String     '格式化的接收数据
    Select Case Combo1.Text
    Case "1B 6A"
        main.SendData Chr(&H1B) + Chr(&H6A)
    Case "1B 65"
        main.SendData Chr(&H1B) + Chr(&H65)
   End Select
    bTime = False
    Timer1.Interval = 800
    Timer1.Enabled = True
    While main.MSComm1.InBufferCount <= 0 And Not bTime
        DoEvents
    Wend
    If main.MSComm1.InBufferCount > 0 Then
        sFmtRData = main.MSComm1.Input
        sFmtRData = Hex$(Asc(Right(sFmtRData, 2))) & Hex$(Asc(Right(sFmtRData, 1)))
        Text2.Text = Text2.Text + sFmtRData + Chr(13) + Chr(10)
    Else
        Text2.Text = Text2.Text + "未接收到数据" + Chr(13) + Chr(10)
    End If
    
End Sub

Private Sub Command2_Click()
    Text2.Text = ""
End Sub

Private Sub Form_Load()
    Combo1.AddItem "1B 6A"
    Combo1.AddItem "1B 65"
    Combo1.Text = Combo1.List(0)
End Sub

Private Sub OKButton_Click()
    Me.Hide
End Sub

Private Sub Timer1_Timer()
    Timer1.Enabled = False
    bTime = True
End Sub
