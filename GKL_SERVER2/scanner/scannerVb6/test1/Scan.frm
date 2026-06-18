VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3015
   ClientLeft      =   120
   ClientTop       =   465
   ClientWidth     =   4560
   LinkTopic       =   "Form1"
   ScaleHeight     =   3015
   ScaleWidth      =   4560
   StartUpPosition =   3  '窗口缺省
   Begin VB.Timer tmrScan 
      Interval        =   100
      Left            =   1680
      Top             =   1320
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'目前的条形码扫描器有点类似外接键盘（其实从消息传送上它就相当于一个键盘），把输入焦点定位到可输入的控件上，一扫描相应的条形码信息就输入到文本框中去了，但是如果没有输入焦点，或另一个不相干的程序获得输入焦点，那就有点乱套了。我想实现的是，不管什么情况，只要扫描器一工作，我的程序就能自动激活，并能获得当前输入的条形码信息。
'实现思路：我用的USB口的条形码扫描器，仔细分析了一下，扫描成功后，以键盘按键消息的形式把条形码输入信息通知给系统。这样通过键盘钩子就可以方便的获得该信息了。但是，怎样区分信息是键盘还是条形码输入的哪？
'很简单，条形码扫描器在很短的时间内输入了至少3个字符以上信息，并且以“回车”作为结束字符，在这种思想指引下，很完美的实现了预定功能。
'以下程序要在Win2000/Win XP 下才能运行成功。
 
'Form1 中的代码:
'*************************************************************************

Option Explicit
Private Sub Form_Load()
   SetHook
   
End Sub
Private Sub Form_Unload(Cancel As Integer)
   UnHook
End Sub
Private Sub tmrScan_Timer()
    Dim strBarCode As String
    strBarCode = GetBarCode
    If Len(strBarCode) > 0 Then
        MsgBox "条形码:" & strBarCode
    End If
End Sub

