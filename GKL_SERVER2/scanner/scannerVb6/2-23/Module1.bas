Attribute VB_Name = "Module1"
'Public Const serial_ports() = {"9600","4800"}
'系统初始化
Global bTest As Boolean '串行口测试标志位
Global bReady As Boolean '读写正确结束
Global bRdWt As Boolean     '指示操作是否出错，true:无，false:有
Global bStop As Boolean     '终止按扭按下
Global iRWMode As Integer '读卡方式 1:第2磁道，2：第3磁道，3：第2、3磁道
Global iWMode As Integer '写卡方式
Global sReadData As String

Public Sub Initiate()
    bTest = False
    bReady = False
    bStop = False
    bRdWt = True
    iRWMode = 1
    sReadData = ""
End Sub

