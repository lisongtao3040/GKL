VERSION 5.00
Begin VB.Form linkset 
   Caption         =   "串口设置"
   ClientHeight    =   5745
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   3870
   LinkTopic       =   "Form1"
   ScaleHeight     =   5745
   ScaleWidth      =   3870
   StartUpPosition =   3  '窗口缺省
   Begin VB.CommandButton Command4 
      Caption         =   "串口默认设置"
      Height          =   495
      Left            =   2280
      TabIndex        =   13
      Top             =   3600
      Width           =   1215
   End
   Begin VB.CommandButton Command3 
      Caption         =   "串口设置"
      Height          =   375
      Left            =   240
      TabIndex        =   12
      Top             =   4200
      Width           =   1575
   End
   Begin VB.Frame Frame1 
      Caption         =   "串行口选择"
      Height          =   855
      Left            =   120
      TabIndex        =   10
      Top             =   360
      Width           =   1575
      Begin VB.ComboBox Combo1 
         Height          =   300
         ItemData        =   "linkset.frx":0000
         Left            =   240
         List            =   "linkset.frx":0002
         TabIndex        =   11
         Top             =   360
         Width           =   1095
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "通信速率"
      Height          =   855
      Left            =   2040
      TabIndex        =   8
      Top             =   360
      Width           =   1575
      Begin VB.ComboBox Combo2 
         Height          =   300
         Left            =   240
         TabIndex        =   9
         Top             =   360
         Width           =   1095
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "数据位数"
      Height          =   855
      Left            =   120
      TabIndex        =   6
      Top             =   1800
      Width           =   1575
      Begin VB.ComboBox Combo3 
         Height          =   300
         Left            =   240
         TabIndex        =   7
         Top             =   360
         Width           =   1095
      End
   End
   Begin VB.Frame Frame4 
      Caption         =   "校验方式"
      Height          =   855
      Left            =   2040
      TabIndex        =   4
      Top             =   1800
      Width           =   1575
      Begin VB.ComboBox Combo4 
         Height          =   300
         Left            =   240
         TabIndex        =   5
         Top             =   360
         Width           =   1095
      End
   End
   Begin VB.Frame Frame5 
      Caption         =   "停止位"
      Height          =   855
      Left            =   240
      TabIndex        =   2
      Top             =   3120
      Width           =   1575
      Begin VB.ComboBox Combo5 
         Height          =   300
         Left            =   240
         TabIndex        =   3
         Top             =   360
         Width           =   1095
      End
   End
   Begin VB.CommandButton Command2 
      Caption         =   "返回"
      Height          =   495
      Left            =   1560
      TabIndex        =   1
      Top             =   4920
      Width           =   855
   End
   Begin VB.CommandButton Command1 
      Caption         =   "调试"
      Height          =   495
      Left            =   2640
      TabIndex        =   0
      Top             =   4920
      Width           =   855
   End
End
Attribute VB_Name = "linkset"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()
DebugDlg.Show
End Sub

Private Sub Command2_Click()
main.Show
Me.Hide
End Sub

Private Sub Command3_Click()
   SetSPortParameters
End Sub

Private Sub Command4_Click()
main.MSComm1.Settings = "9600,n,8,1"         '9600波特率，无校验，8位数据位，1位停止位
End Sub

Private Sub Form_Load()
 Dim i As Integer
    Combo1.AddItem "1"         'serial_ports(i)
    Combo1.AddItem "2"
    Combo1.AddItem "3"
    Combo1.AddItem "4"
    Combo1.Text = Combo1.List(0)

    Combo2.AddItem "9600"
    Combo2.AddItem "4800"
    Combo2.AddItem "2400"
    Combo2.AddItem "1200"
    Combo2.Text = Combo2.List(0)

    Combo3.AddItem "8"
    Combo3.AddItem "7"
    Combo3.AddItem "4"
    Combo3.Text = Combo3.List(0)
    
    Combo4.AddItem "无"
    Combo4.AddItem "奇校验"
    Combo4.AddItem "偶校验"
    Combo4.Text = Combo4.List(0)
    
    Combo5.AddItem "1"
    Combo5.AddItem "1.5"
    Combo5.AddItem "2"
    Combo5.Text = Combo5.List(0)
    linkset.Move (Screen.Width - Me.Width) / 2, (Screen.Height - Me.Height) / 2
End Sub

'根据用户输入设置串行口的参数
Public Sub SetSPortParameters()
Dim sCheck As String
    Select Case Combo4.Text
    Case "无"
        sCheck = "n"
    Case "奇校验"
        sCheck = "o"
    Case "偶校验"
        sCheck = "e"
    End Select
    
    With main
        .MSComm1.CommPort = Val(Combo1.Text)
        .MSComm1.Settings = Combo2.Text + "," + sCheck + "," + Combo3.Text + "," + Combo5.Text
    End With
    main.Prompt "串行口设置成功！"
    Exit Sub
    
End Sub


