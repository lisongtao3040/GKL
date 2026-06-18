VERSION 5.00
Begin VB.Form help 
   Caption         =   "磁卡读写器"
   ClientHeight    =   3435
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   7260
   LinkTopic       =   "Form1"
   ScaleHeight     =   3435
   ScaleWidth      =   7260
   StartUpPosition =   3  '窗口缺省
   Begin VB.CommandButton Command1 
      Caption         =   "OK"
      Height          =   495
      Left            =   480
      TabIndex        =   3
      Top             =   2760
      Width           =   1575
   End
   Begin VB.Label Label3 
      Caption         =   "磁卡读写器      1.00"
      Height          =   615
      Left            =   5400
      TabIndex        =   2
      Top             =   2160
      Width           =   1095
   End
   Begin VB.Line Line1 
      BorderWidth     =   4
      X1              =   360
      X2              =   6960
      Y1              =   1680
      Y2              =   1680
   End
   Begin VB.Label Label2 
      Caption         =   "参考程序：施伟滨老师编写的读写器部分源代码"
      Height          =   735
      Left            =   1920
      TabIndex        =   1
      Top             =   1320
      Width           =   4935
   End
   Begin VB.Label Label1 
      Caption         =   "本读写器由106A、104A、108A共同完成"
      BeginProperty Font 
         Name            =   "黑体"
         Size            =   18
         Charset         =   134
         Weight          =   700
         Underline       =   0   'False
         Italic          =   -1  'True
         Strikethrough   =   0   'False
      EndProperty
      Height          =   855
      Left            =   240
      TabIndex        =   0
      Top             =   360
      Width           =   7095
   End
End
Attribute VB_Name = "help"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()
Me.Hide
End Sub
