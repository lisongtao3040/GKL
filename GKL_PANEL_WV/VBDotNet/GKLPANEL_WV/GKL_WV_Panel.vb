'Imports CefSharp
'Imports CefSharp.WinForms

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO.Ports
Imports System.Threading
Imports System.Configuration.ConfigurationSettings

Imports System.Runtime.InteropServices
Imports System.Threading.Thread
Imports System.Net.IPAddress
Imports MvCamCtrl.NET
Imports System.Configuration
Imports System.Threading.Tasks
Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms



Public Class GooglePanel

    '------------------------------------------------------------------------------------------
    'COM
    '------------------------------------------------------------------------------------------
    'Public browser As ChromiumWebBrowser
    Public serialPort As SerialPort


    '------------------------------------------------------------------------------------------
    'CAMERA
    '------------------------------------------------------------------------------------------
    Dim dev As MyCamera = New MyCamera

    ' ch:用于从驱动获取图像的缓存 | en:Buffer to get image from driver
    Dim m_nBufSizeForDriver As UInt32 = 3072 * 2048 * 3
    Dim m_pBufForDriver(m_nBufSizeForDriver) As Byte

    ' ch:用于保存图像的缓存 | en:Buffer to save image
    Dim m_nBufSizeForSaveImage As UInt32 = 3072 * 2048 * 3 * 3 + 2048
    Dim m_pBufForSaveImage(m_nBufSizeForSaveImage) As Byte

    Dim m_nByteImageBuffer As UInt32 = 3072 * 2048 * 3
    Dim m_byteImageBuffer(m_nByteImageBuffer) As Byte

    ' ch:成员变量，用于控制相机 | en:Member variable to control camera
    Dim m_stDeviceInfoList As MyCamera.MV_CC_DEVICE_INFO_LIST = New MyCamera.MV_CC_DEVICE_INFO_LIST
    Dim m_stDeviceInfo As MyCamera.MV_CC_DEVICE_INFO = New MyCamera.MV_CC_DEVICE_INFO
    Dim m_handle As IntPtr
    Dim m_nDeviceIndex As UInt32
    Dim m_bIsOpen As Boolean
    Dim m_bIsGrabbing As Boolean
    Dim m_stFrameOutInfo As MyCamera.MV_FRAME_OUT_INFO_EX = New MyCamera.MV_FRAME_OUT_INFO_EX()


#Region "System"
    Private WM_KEYDOWN As Integer = &H100

    'Protected Overrides Sub WndProc(ByRef WindowsMessage As Message)



    '    Select Case WindowsMessage.Msg

    '        Case WM_KEYDOWN

    '            Dim VirtualKeyCode As Integer = CType(WindowsMessage.WParam, Integer)

    '            Select Case (VirtualKeyCode)

    '                Case Keys.A
    '                    label1.Text = "The a key was pressed"

    '            End Select

    '    End Select
    '    MyBase.WndProc(WindowsMessage)
    'End Sub

    Public scanAllTxt As String = ""
    Public oldScanTime As TimeSpan = New TimeSpan(Now.Ticks)

    'Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
    '    If keyData = Keys.Enter Then
    '        MessageBox.Show("yada")
    '        Return True
    '    End If


    '    If LCase(wb.Document.ActiveElement.TagName) = "input" Then
    '        If wb.Document.ActiveElement.GetAttribute("typ") IsNot Nothing AndAlso LCase(wb.Document.ActiveElement.GetAttribute("typ")) = "scan" Then

    '            Dim ts As TimeSpan = oldScanTime.Subtract(New TimeSpan(Now.Ticks)).Duration()

    '            If oldScanTime.Subtract(New TimeSpan(Now.Ticks)).Duration().TotalMilliseconds < 900 Then
    '                scanAllTxt = scanAllTxt & keyData.ToString
    '            Else
    '                scanAllTxt = ""
    '                scanAllTxt = scanAllTxt & keyData.ToString
    '            End If

    '            oldScanTime = New TimeSpan(Now.Ticks)

    '            wb.Document.ActiveElement.InnerText = scanAllTxt
    '            'wb.Document.ActiveElement.SetAttribute("readonly", False)

    '            'If keyData = Keys.Enter Then
    '            '    wb.Document.ActiveElement.InnerText = scanAllTxt
    '            '    scanAllTxt = ""
    '            'Else
    '            '    scanAllTxt = scanAllTxt + keyData.ToString
    '            'End If

    '        End If
    '    End If


    '    Return MyBase.ProcessCmdKey(msg, keyData)
    'End Function

    Private Sub sWaitTime(ByVal st As Long)
        '指定の時間待つ（1/1000 秒単位で指定)
        Dim lngSt As Long
        'システム起動後のミリ秒単位の経過時間を取得します。
        'システムが 24.9 日間稼動し続けた場合、この経過時間は 0 に戻ります。
        lngSt = System.Environment.TickCount
        Do While System.Environment.TickCount - lngSt < st
            'メッセージ キューに現在ある Windows メッセージをすべて処理します
            Application.DoEvents()              'こちらだけではCPUの使用率が100％になる
            '指定した時間だけ現在のスレッドを中断します。
            System.Threading.Thread.Sleep(10)   'これだけでは他の処理を受け付けない
        Loop
    End Sub


    Private Sub GooglePanel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Try
            'SetControlStaus(False)
            If serialPort IsNot Nothing Then
                serialPort.Close()

            End If

        Catch ex1 As Exception

        End Try
    End Sub

    Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" _
    (ByVal hwnd As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer

    Public Structure SHELLEXECUTEINFO
        Dim cbSize As Integer
        Dim fMask As Integer
        Dim hWnd As Integer
        Dim lpVerb As String
        Dim lpFile As String
        Dim lpParameters As String
        Dim lpDirectory As String
        Dim nShow As Integer
        Dim hInstApp As Integer
        Dim lpIDList As Integer
        Dim lpClass As String
        Dim hkeyClass As Integer
        Dim dwHotKey As Integer
        Dim hIcon As Integer
        Dim hProcess As Integer
    End Structure

    'Form Load
    Private Sub GooglePanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '原先是清 IE 的履历（RunDll32 InetCpl.cpl,ClearMyTracksByProcess 1/2/8）。
        '现在换成 WebView2，履历在 InitBrowser() 里清（那边要等 WebView2 初始化完成才能清）


        ' http://10.160.192.43/gkl2020_panel/publish.htm


        '设置窗口大小 最大化
        Me.WindowState = FormWindowState.Maximized

        '大平板使用
        '发行地址
        '\\10.160.192.43\gkl2020_panel
        ''------------------------------------
        ''COM 口设置  默认连接COM1
        ComNameInit()
        If Me.cbComName.Items.Count >= 2 Then
            Me.cbComName.SelectedIndex = 1
            ConnectCom()
        End If

        'http://10.160.192.43/gkl2020_panel_small/publish.htm
        '小平板使用
        '发行地址
        '\\10.160.192.43\gkl2020_panel_small
        'Panel1.Visible = False

        '装载网页
        InitBrowser()
        '设置网页大小
        InitPanelChrome()

    End Sub

    'Form 大小改变，Web大小跟随改变
    Private Sub GooglePanel_ClientSizeChanged(sender As Object, e As EventArgs) Handles Me.ClientSizeChanged
        InitPanelChrome()
    End Sub

    '连接COM
    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        ConnectCom()
    End Sub

    '停止Com连接
    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Try
            SetControlStaus(False)
            serialPort.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    '网页加载完成后（原来的 wb.DocumentCompleted）
    Private Async Sub wb_NavigationCompleted(sender As Object, e As CoreWebView2NavigationCompletedEventArgs) Handles wb.NavigationCompleted

        Dim title As String = Await GetDocumentTitle()
        If title = "检查明細" Then
            '打开摄像头
            Me.btnCam.Enabled = True
            '关闭摄像头
            Me.btnCamClose.Enabled = False

            Dim cameraFlg As String = Await GetElementValue("camera_flg")
            If cameraFlg = "1" Then
                Me.btnSnap.Visible = True
                Me.btnCam.Visible = True
                Me.btnCamClose.Visible = True

                PictureBoxDisplay.Visible = SnapShowFlg

            End If

        Else
            '打开摄像头
            Me.btnCam.Enabled = False
            '关闭摄像头
            Me.btnCamClose.Enabled = False

            Me.btnSnap.Visible = False
            Me.btnCam.Visible = False
            Me.btnCamClose.Visible = False
        End If

    End Sub

#End Region


#Region "WebView2"

    '设置网页大小
    Public Sub InitPanelChrome()
        PanelChrome.Width = Me.Width
        PanelChrome.Height = Me.Height - 50
    End Sub

    Public Async Sub InitBrowser()
        'CefSharp.Cef.Initialize(New CefSettings())
        'browser = New ChromiumWebBrowser("http://10.160.192.43/GKL/")
        'browser = New ChromiumWebBrowser("http://localhost:60663/")
        'PanelChrome.Controls.Add(browser)
        'browser.Dock = DockStyle.Fill
        'wb.Url = New Uri("http://localhost:60663/")
        'wb.Url = New Uri("http://home001/GKL/Default.aspx")
        'wb.Url = New Uri("http://10.160.192.43/GKL/")
        'wb.Url = New Uri("http://ot5600/GKL/Default.aspx")
        'wb.Url = New Uri("http://ot5600/GKL/Default.aspx")
        'wb.Url = New Uri("http://jxs001/GKL/Default.aspx")


        'wb.Url = New Uri("http://10.160.192.43/GKL_TEST/Default.aspx")


        '元服务器()
        ' wb.Url = New Uri("http://10.160.192.43/GKL2020/Default.aspx")
        'wb.Url = New Uri("http://10.160.192.114/GKL2020/Default.aspx")
        '新2号服务器
        'wb.Url = New Uri("http://DLTDLFSDB004/GKL2020/Default.aspx")
        'wb.Url = New Uri("http://10.160.192.123/GKL2020/Default.aspx")

        'WebView2 的用户数据目录放到本机（原来的 IE 缓存是系统管的，这里要自己指定一个可写目录）
        '程序是发布在共享盘(\\10.160.192.123\...)上安装的，不能把缓存写在 exe 同目录
        wb.CreationProperties = New CoreWebView2CreationProperties() With {
            .UserDataFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GKL_PANEL\WebView2")
        }

        '先初始化 WebView2，初始化完成后清履历（相当于原来开画面时清 IE 履历），最后再打开网页
        Try
            Await wb.EnsureCoreWebView2Async()
        Catch ex As Exception
            Return          '初始化失败。原因在 wb_CoreWebView2InitializationCompleted 里已经提示过
        End Try

        Await ClearWebViewBrowsingData()

        '装载网页（app.config 里的 main_url）
        wb.Source = New Uri(ConfigurationManager.AppSettings.Item("main_url"))

        '注意：原来是 wb.ObjectForScripting = Me，网页用 window.external 调 WinForm 的方法。
        'WebView2 不支持 ObjectForScripting，网页那边要改成 window.chrome.webview.postMessage(...)，
        '这边再用 wb.CoreWebView2.WebMessageReceived 事件接收（网页不改就收不到）。
    End Sub

    '清理 WebView2 的履历（相当于原来 Form_Load 里清 IE 的履历）
    '网页的历史记录、临时文件、Cookie 都放在 wb.CreationProperties 指定的用户数据目录里
    '（原来 IE 的这些数据由 Windows 统一管，所以只能调 RunDll32 去删）
    Private Async Function ClearWebViewBrowsingData() As Task
        If wb.CoreWebView2 Is Nothing Then
            Return
        End If

        Try
            Await wb.CoreWebView2.Profile.ClearBrowsingDataAsync(
                CoreWebView2BrowsingDataKinds.BrowsingHistory Or
                CoreWebView2BrowsingDataKinds.DiskCache Or
                CoreWebView2BrowsingDataKinds.Cookies)
        Catch ex As Exception
            '清不掉不算致命错误，照常打开网页
        End Try
    End Function

    'WebView2 初始化完成。失败一般是这台电脑没装 WebView2 运行时
    Private Sub wb_CoreWebView2InitializationCompleted(sender As Object, e As CoreWebView2InitializationCompletedEventArgs) Handles wb.CoreWebView2InitializationCompleted
        If Not e.IsSuccess Then
            MessageBox.Show("WebView2 初始化失败。请在这台电脑上安装 WebView2 Runtime。" & vbCrLf & e.InitializationException.Message)
        End If
    End Sub

    '------------------------------------------------------------------------------------------
    '网页操作。原来的 wb.DocumentTitle / wb.Document.GetElementById / InvokeScript
    'WebView2 只能通过执行 JavaScript 来读写网页
    '------------------------------------------------------------------------------------------

    '执行网页里的 JavaScript 并取回结果（相当于原来的 wb.Document.InvokeScript）
    Private Async Function RunScript(ByVal script As String) As Task(Of String)
        If wb.CoreWebView2 Is Nothing Then
            Return ""           '网页还没打开
        End If
        Return JsonToText(Await wb.CoreWebView2.ExecuteScriptAsync(script))
    End Function

    '取网页标题（相当于原来的 wb.DocumentTitle）
    Private Async Function GetDocumentTitle() As Task(Of String)
        Return Await RunScript("document.title")
    End Function

    '取网页里某个元素的值（相当于原来的 wb.Document.GetElementById(id).GetAttribute("value")）
    Private Async Function GetElementValue(ByVal id As String) As Task(Of String)
        Return Await RunScript("(function(){var e=document.getElementById(" & JsText(id) & ");return e?e.value:'';})()")
    End Function

    '当前获得焦点的是不是扫描框（相当于原来的 wb.Document.ActiveElement + typ="scan" 判断）
    Private Async Function IsScanInput() As Task(Of Boolean)
        Dim result As String = Await RunScript("(function(){var e=document.activeElement;return !!(e&&e.tagName.toLowerCase()=='input'&&String(e.getAttribute('typ')).toLowerCase()=='scan');})()")
        Return result = "true"
    End Function

    '拼一个能直接写进 JavaScript 的字符串（转义引号、反斜杠、换行）
    Private Function JsText(ByVal text As String) As String
        If text Is Nothing Then
            text = ""
        End If
        text = text.Replace("\", "\\").Replace("""", "\""").Replace(vbCr, "\r").Replace(vbLf, "\n")
        Return """" & text & """"
    End Function

    'WebView2 返回的是 JSON，这里还原成普通字符串（相当于原来 IE 直接给字符串）
    Private Function JsonToText(ByVal json As String) As String
        If json Is Nothing OrElse json = "null" Then
            Return ""
        End If
        If json.Length < 2 OrElse Not json.StartsWith("""") Then
            Return json                     '不是字符串（例如 true / false），原样返回
        End If

        '去掉两边的引号，并还原 \" 和 \\
        Dim text As String = json.Substring(1, json.Length - 2)
        text = text.Replace("\""", """").Replace("\\", "\")

        'JSON 里的中文可能写成 \uXXXX，这里转回来
        Dim result As String = ""
        Dim i As Integer = 0
        While i < text.Length
            If text(i) = "\"c AndAlso i + 5 < text.Length AndAlso text(i + 1) = "u"c Then
                result &= ChrW(Convert.ToInt32(text.Substring(i + 2, 4), 16))
                i += 6
            Else
                result &= text(i)
                i += 1
            End If
        End While
        Return result
    End Function

#End Region


#Region "Com"

    'COM LIST
    Private Sub ComNameInit()
        Me.btnConnect.Enabled = True
        Me.btnStop.Enabled = False
        cbComName.Items.Clear()
        Me.cbComName.Items.Add("请选择COM口")
        For Each portName As String In System.IO.Ports.SerialPort.GetPortNames()
            Me.cbComName.Items.Add(portName)
        Next
    End Sub

    '装车
    Private Sub ConnectCom()

        Try
            '设置COM参数，并且打开
            InstanceSerialPort()
            ''设置COM 关联控件状态可用
            SetControlStaus(True)

            Exit Sub
        Catch ex As Exception
            'MessageBox.Show(ex.Message)

            Try
                SetControlStaus(False)
                serialPort.Close()
            Catch ex1 As Exception

            End Try

        End Try


        Try
            '设置COM参数，并且打开
            InstanceSerialPort()
            ''设置COM 关联控件状态可用
            SetControlStaus(True)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try




    End Sub

    'COM PORT
    Private Sub InstanceSerialPort()
        serialPort = New SerialPort()
        serialPort.PortName = Me.cbComName.Text
        serialPort.BaudRate = Convert.ToInt32(Me.comboBox_comPl.Text)
        serialPort.Parity = Parity.None
        serialPort.StopBits = StopBits.One
        serialPort.DataBits = 8
        serialPort.DiscardNull = True
        AddHandler serialPort.DataReceived, AddressOf serialPort_DataReceived
        serialPort.Open()
    End Sub


    '设置COM 关联控件状态
    Private Sub SetControlStaus(ByVal connectStaus As Boolean)
        Me.btnConnect.Enabled = Not connectStaus
        Me.btnStop.Enabled = connectStaus
        Me.cbComName.Enabled = Not connectStaus
        Me.comboBox_comPl.Enabled = Not connectStaus
    End Sub

    '扫描数据事件
    Private Sub serialPort_DataReceived(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        Try
            Thread.Sleep(600)
            Dim serialPort As SerialPort = CType(sender, SerialPort)
            Dim threadReceiveSub As Thread = New Thread(New ParameterizedThreadStart(AddressOf ReceiveData))
            threadReceiveSub.Start(serialPort)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    '接受扫描数据
    Private Sub ReceiveData(ByVal serialPortobj As Object)
        Try
            Dim serialPort As SerialPort = CType(serialPortobj, SerialPort)

            Dim str As String = serialPort.ReadExisting()

            'Dim str2 As String = ""

            'While str2 <> serialPort.ReadExisting()
            '    Thread.Sleep(200)
            '    str2 = serialPort.ReadExisting()
            '    Thread.Sleep(200)
            'End While


            If str = String.Empty Then
                Return
            Else
                SetMessage(str)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    '扫描枪扫描事件
    Private Sub SetMessage(ByVal msg As String)
        '串口线程过来的数据，切到 UI 线程处理
        Me.BeginInvoke(New Action(Sub() SetMessageOnUi(msg)))
    End Sub

    '把扫描到的条码写进网页里获得焦点的输入框（typ="scan"），然后回车
    Private Async Sub SetMessageOnUi(ByVal msg As String)
        Try
            Me.Text = (msg)
            '原来这里还有 SendKeys.SendWait("{F8}")，现在不需要
            sWaitTime(100)

            '原来是 wb.Document.ActiveElement 判断是不是扫描框
            If Await IsScanInput() Then

                Dim e1 As Boolean = Me.btnCam.Enabled
                Dim e2 As Boolean = Me.btnCamClose.Enabled
                Dim e3 As Boolean = Me.btnSnap.Enabled
                SetSnapButtonEnabled(False)

                '原来是 wb.Document.ActiveElement.InnerText = msg
                Await RunScript("(function(){var e=document.activeElement;if(e)e.value=" & JsText(msg) & ";})()")
                sWaitTime(500)
                SendKeys.SendWait("{ENTER}")

                Me.btnCam.Enabled = e1
                Me.btnCamClose.Enabled = e2
                Me.btnSnap.Enabled = e3

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region



#Region "Snap"


    '拍照关联按钮设置
    Private Sub SetSnapButtonEnabled(ByVal kbn As Boolean)
        '打开摄像头
        Me.btnCam.Enabled = kbn
        '关闭摄像头
        Me.btnCamClose.Enabled = kbn
        '拍照
        Me.btnSnap.Enabled = kbn
    End Sub


    Public SnapShowFlg As Boolean = False
    '------------------------------------------------------------------------------------------
    '打开摄像头
    '------------------------------------------------------------------------------------------
    Private Sub btnCam_Click(sender As Object, e As EventArgs) Handles btnCam.Click
        '------------------------------------------------------------------------------------------
        ' ch:枚举设备按钮操作 | en:Button operation for device enum
        Dim Info As String
        Dim nRet As Int32 = MyCamera.MV_OK

        Dim e1 As Boolean = Me.btnCam.Enabled
        Dim e2 As Boolean = Me.btnCamClose.Enabled
        Dim e3 As Boolean = Me.btnSnap.Enabled
        SetSnapButtonEnabled(False)

        Dim openFlg As Boolean = True


        ' ch:枚举设备 | en:Enumerate devices
        nRet = MyCamera.MV_CC_EnumDevices_NET((MyCamera.MV_GIGE_DEVICE Or MyCamera.MV_USB_DEVICE), m_stDeviceInfoList)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to enumerate devices" + Convert.ToString(nRet))
            Me.btnCam.Enabled = e1
            Me.btnCamClose.Enabled = e2
            Me.btnSnap.Enabled = e3
            Return
        End If

        If (0 = m_stDeviceInfoList.nDeviceNum) Then
            MsgBox("No Find Gige | Usb Device !")
            Me.btnCam.Enabled = e1
            Me.btnCamClose.Enabled = e2
            Me.btnSnap.Enabled = e3
            Return
        End If

        ' ch:将设备信息放到设备列表中 | en:Put device information in the device list
        Dim i As Int32
        For i = 0 To m_stDeviceInfoList.nDeviceNum - 1
            Dim stDeviceInfo As MyCamera.MV_CC_DEVICE_INFO = New MyCamera.MV_CC_DEVICE_INFO
            stDeviceInfo = CType(Marshal.PtrToStructure(m_stDeviceInfoList.pDeviceInfo(i), GetType(MyCamera.MV_CC_DEVICE_INFO)), MyCamera.MV_CC_DEVICE_INFO)
            If (MyCamera.MV_GIGE_DEVICE = stDeviceInfo.nTLayerType) Then
                Dim stGigeInfoPtr As IntPtr = Marshal.AllocHGlobal(216)
                Marshal.Copy(stDeviceInfo.SpecialInfo.stGigEInfo, 0, stGigeInfoPtr, 216)
                Dim stGigeInfo As MyCamera.MV_GIGE_DEVICE_INFO
                stGigeInfo = CType(Marshal.PtrToStructure(stGigeInfoPtr, GetType(MyCamera.MV_GIGE_DEVICE_INFO)), MyCamera.MV_GIGE_DEVICE_INFO)
                Dim nIpByte1 As UInt32 = (stGigeInfo.nCurrentIp And &HFF000000) >> 24
                Dim nIpByte2 As UInt32 = (stGigeInfo.nCurrentIp And &HFF0000) >> 16
                Dim nIpByte3 As UInt32 = (stGigeInfo.nCurrentIp And &HFF00) >> 8
                Dim nIpByte4 As UInt32 = (stGigeInfo.nCurrentIp And &HFF)

                Info = "DEV[" + Convert.ToString(i) + "] NAME[" + stGigeInfo.chUserDefinedName + "]IP[" + nIpByte1.ToString() + "." + nIpByte2.ToString() + "." + nIpByte3.ToString() + "." + nIpByte4.ToString() + "]"
                'ComboBoxDeviceList.Items.Add(Info)
                m_nDeviceIndex = i
            Else
                Dim stUsbInfoPtr As IntPtr = Marshal.AllocHGlobal(540)
                Marshal.Copy(stDeviceInfo.SpecialInfo.stUsb3VInfo, 0, stUsbInfoPtr, 540)
                Dim stUsbInfo As MyCamera.MV_USB3_DEVICE_INFO
                stUsbInfo = CType(Marshal.PtrToStructure(stUsbInfoPtr, GetType(MyCamera.MV_USB3_DEVICE_INFO)), MyCamera.MV_USB3_DEVICE_INFO)
                Info = "DEV[" + Convert.ToString(i) + "] NAME[" + stUsbInfo.chUserDefinedName + "]Model[" + stUsbInfo.chSerialNumber + "]"
                'ComboBoxDeviceList.Items.Add(Info)
                m_nDeviceIndex = i
            End If
        Next i

        If m_stDeviceInfoList.nDeviceNum = 0 Then
            MsgBox("没有找到摄像头")
            openFlg = False
        End If

        '------------------------------------------------------------------------------------------
        '    ' ch:打开设备按钮操作 | en:Button operation for opening device


        ' ch:创建句柄 | en:Create handle
        'Dim nRet As Int32 = MyCamera.MV_OK
        m_stDeviceInfo = CType(Marshal.PtrToStructure(m_stDeviceInfoList.pDeviceInfo(m_nDeviceIndex), GetType(MyCamera.MV_CC_DEVICE_INFO)), MyCamera.MV_CC_DEVICE_INFO)
        nRet = dev.MV_CC_CreateDevice_NET(m_stDeviceInfo)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to create handle")
            Me.btnCam.Enabled = e1
            Me.btnCamClose.Enabled = e2
            Me.btnSnap.Enabled = e3
            Return
        End If

        ' ch:打开设备 | en:Open device
        nRet = dev.MV_CC_OpenDevice_NET()
        If MyCamera.MV_OK <> nRet Then
            dev.MV_CC_DestroyDevice_NET()
            MsgBox("Open device failed")
            Me.btnCam.Enabled = e1
            Me.btnCamClose.Enabled = e2
            Me.btnSnap.Enabled = e3
            Return
        End If

        ' ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
        If m_stDeviceInfo.nTLayerType = MyCamera.MV_GIGE_DEVICE Then
            Dim nPacketSize As Int32
            nPacketSize = dev.MV_CC_GetOptimalPacketSize_NET()
            If nPacketSize > 0 Then
                nRet = dev.MV_CC_SetIntValue_NET("GevSCPSPacketSize", nPacketSize)
                If 0 <> nRet Then
                    Console.WriteLine("Warning: Set Packet Size failed:{0:x8}", nRet)
                End If
            Else
                Console.WriteLine("Warning: Get Packet Size failed:{0:x8}", nPacketSize)
            End If
        End If

        ' ch:获取触发模式 | en:Acquire trigger mode
        Dim stTriggerMode As MyCamera.MVCC_ENUMVALUE = New MyCamera.MVCC_ENUMVALUE
        nRet = dev.MV_CC_GetEnumValue_NET("TriggerMode", stTriggerMode)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to acquire trigger mode")
            Me.btnCam.Enabled = e1
            Me.btnCamClose.Enabled = e2
            Me.btnSnap.Enabled = e3
            Return
        End If

        ' ch:获取触发源 | en:Acquire trigger source
        Dim stTriggerSource As MyCamera.MVCC_ENUMVALUE = New MyCamera.MVCC_ENUMVALUE
        nRet = dev.MV_CC_GetEnumValue_NET("TriggerSource", stTriggerSource)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to acquire trigger source")
            Me.btnCam.Enabled = e1
            Me.btnCamClose.Enabled = e2
            Me.btnSnap.Enabled = e3
            Return
        End If


        ' ch:获取曝光时间 | en:Acquire exposure time 
        Dim stExposureTime As MyCamera.MVCC_FLOATVALUE = New MyCamera.MVCC_FLOATVALUE
        nRet = dev.MV_CC_GetFloatValue_NET("ExposureTime", stExposureTime)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to acquire exposure time")
            openFlg = False
        End If

        ' ch:获取增益 | en:Acquire gain
        Dim stGain As MyCamera.MVCC_FLOATVALUE = New MyCamera.MVCC_FLOATVALUE
        nRet = dev.MV_CC_GetFloatValue_NET("Gain", stGain)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to acquire gain")
            openFlg = False
        End If

        ' ch:获取帧率 | en:Acquire frame rate
        Dim stFrameRate As MyCamera.MVCC_FLOATVALUE = New MyCamera.MVCC_FLOATVALUE
        nRet = dev.MV_CC_GetFloatValue_NET("ResultingFrameRate", stFrameRate)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to acquire frame rate")
            openFlg = False
        End If


        ' ch:将自动曝光和自动增益关闭 | en:Close auto-exposure and auto-gain

        nRet = dev.MV_CC_SetEnumValue_NET("ExposureAuto", MyCamera.MV_CAM_EXPOSURE_AUTO_MODE.MV_EXPOSURE_AUTO_MODE_OFF)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to close auto-exposure")
            openFlg = False
        End If


        nRet = dev.MV_CC_SetEnumValue_NET("GainAuto", MyCamera.MV_CAM_GAIN_MODE.MV_GAIN_MODE_OFF)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to close auto-gain")
            openFlg = False
        End If


        '------------------------------------------------------------------------------------------
        '        ' ch:开启采集 | en:Start grabbing

        nRet = dev.MV_CC_StartGrabbing_NET()
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to start grabbing")
            openFlg = False
        End If
        nRet = dev.MV_CC_Display_NET(PictureBoxDisplay.Handle)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to display image")
            openFlg = False
        End If

        If openFlg Then
            '打开摄像头
            Me.btnCam.Enabled = False
            '关闭摄像头
            Me.btnCamClose.Enabled = True
            '拍照
            Me.btnSnap.Enabled = True

            PictureBoxDisplay.Visible = True

        Else

            Me.btnCam.Enabled = e1
            Me.btnCamClose.Enabled = e2
            Me.btnSnap.Enabled = e3

        End If

        SnapShowFlg = PictureBoxDisplay.Visible


    End Sub

    '关闭摄像头
    Private Sub btnCamClose_Click(sender As Object, e As EventArgs) Handles btnCamClose.Click

        Dim nRet As Int32
        nRet = dev.MV_CC_StopGrabbing_NET()
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to stop grabbing")
        End If

        nRet = dev.MV_CC_CloseDevice_NET()
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to close device")
            Return
        End If

        nRet = dev.MV_CC_DestroyDevice_NET()
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Fail to destroy handle")
            Return
        End If


        '打开摄像头
        Me.btnCam.Enabled = True
        '关闭摄像头
        Me.btnCamClose.Enabled = False
        '拍照
        Me.btnSnap.Enabled = False


        PictureBoxDisplay.Visible = False

        SnapShowFlg = PictureBoxDisplay.Visible


    End Sub

    '拍照
    Private Async Sub btnSnap_Click(sender As Object, e As EventArgs) Handles btnSnap.Click

        Dim title As String = Await GetDocumentTitle()
        If title <> "检查明細" Then
            MsgBox("不是检查画面")
            Exit Sub
        End If

        Dim openFlg As Boolean = True

        Dim e1 As Boolean = Me.btnCam.Enabled
        Dim e2 As Boolean = Me.btnCamClose.Enabled
        Dim e3 As Boolean = Me.btnSnap.Enabled
        SetSnapButtonEnabled(False)

        Dim nRet As Int32
        Dim stIntValue As MyCamera.MVCC_INTVALUE = New MyCamera.MVCC_INTVALUE
        nRet = dev.MV_CC_GetIntValue_NET("PayloadSize", stIntValue)
        If MyCamera.MV_OK <> nRet Then
            MsgBox("Set PayloadSize failed")
            openFlg = False
        End If

        'If stIntValue.nCurValue > m_nBufSizeForDriver Then
        '    m_nBufSizeForDriver = stIntValue.nCurValue
        '    ReDim m_pBufForDriver(m_nBufSizeForDriver)

        '    ' ch:同时对保存图像的缓存做大小判断处理
        '    ' BMP图片大小：width * height * 3 + 2048(预留BMP头大小)
        '    ' en:Determine the buffer size to save image
        '    ' BMP image size: width * height * 3 + 2048 (Reserved BMP header size)
        '    m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048
        '    ReDim m_pBufForSaveImage(m_nBufSizeForSaveImage)
        'End If

        Dim pData As IntPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0)
        nRet = dev.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForSaveImage, m_stFrameOutInfo, 1000)
        If MyCamera.MV_OK = nRet Then
            Dim pImage As IntPtr = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0)
            Dim stSaveImageParam As MyCamera.MV_SAVE_IMAGE_PARAM_EX = New MyCamera.MV_SAVE_IMAGE_PARAM_EX()
            stSaveImageParam.pData = pData
            stSaveImageParam.nDataLen = m_stFrameOutInfo.nFrameLen
            stSaveImageParam.enPixelType = m_stFrameOutInfo.enPixelType
            stSaveImageParam.nWidth = m_stFrameOutInfo.nWidth
            stSaveImageParam.nHeight = m_stFrameOutInfo.nHeight
            stSaveImageParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg
            stSaveImageParam.nBufferSize = m_nBufSizeForSaveImage
            stSaveImageParam.pImageBuffer = pImage
            stSaveImageParam.nJpgQuality = 80

            nRet = dev.MV_CC_SaveImageEx_NET(stSaveImageParam)
            If (MyCamera.MV_OK <> nRet) Then
                MsgBox("Fail to convert image format：无法转换图像格式")
                Me.btnCam.Enabled = e1
                Me.btnCamClose.Enabled = e2
                Me.btnSnap.Enabled = e3
                Return
            End If

            ' ch:写文件 | en:Write file
            If stSaveImageParam.nImageLen <> m_nByteImageBuffer Then
                m_nByteImageBuffer = stSaveImageParam.nImageLen
                ReDim m_byteImageBuffer(m_nByteImageBuffer)
            End If

            Marshal.Copy(pImage, m_byteImageBuffer, 0, stSaveImageParam.nImageLen)


            '网页里的值（原来是 wb.Document.GetElementById / InvokeScript）
            Dim line_id As String = Await GetElementValue("hidLineId")
            Dim chkNo_key As String = Await GetElementValue("hidChkNo")
            Dim chk_method_id As String = Await RunScript("returnString()")

            btnSnap.Text = "保存中"

            Dim rtv As String = My.WebServices.api.SaveIMG(chkNo_key, line_id, chk_method_id, m_byteImageBuffer)

            If rtv = "OK" Then
                MsgBox("图片保存成功")
                Await RunScript("InitKamera()")
            Else
                MsgBox(rtv)
            End If

            btnSnap.Text = "拍照"

        Else
            MsgBox("Fail to get image stream within 1s")
            openFlg = False
        End If


        Me.btnCam.Enabled = e1
        Me.btnCamClose.Enabled = e2
        Me.btnSnap.Enabled = e3

    End Sub


#End Region


    Private Async Sub btnTEST_Click(sender As Object, e As EventArgs) Handles btnTEST.Click

        Await RunScript("InitKamera()")

    End Sub

    Private Sub PictureBoxDisplay_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles PictureBoxDisplay.MouseDoubleClick
        If PictureBoxDisplay.Width = 240 Then
            PictureBoxDisplay.Width = 720
            PictureBoxDisplay.Height = 720
            Dim StartPoint As New System.Drawing.Point
            StartPoint.X = 0
            StartPoint.Y = 200

            PictureBoxDisplay.Location = StartPoint

        Else
            PictureBoxDisplay.Width = 240
            PictureBoxDisplay.Height = 240
            Dim StartPoint As New System.Drawing.Point
            StartPoint.X = 770
            StartPoint.Y = 0

            PictureBoxDisplay.Location = StartPoint
        End If
    End Sub

    Private Sub GooglePanel_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub
End Class
