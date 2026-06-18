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
            serialPort.Close()
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

        'System.Diagnostics.Process.Start("C:\Windows\system32\cmd.exe",
        '                                "RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 8")

        'Dim p As New Process
        'p.StartInfo.FileName = "C:\Windows\system32\cmd.exe"
        'p.StartInfo.UseShellExecute = False
        'p.StartInfo.RedirectStandardInput = True
        'p.StartInfo.RedirectStandardOutput = True
        'p.StartInfo.RedirectStandardError = True
        'p.StartInfo.CreateNoWindow = True
        'p.Start()
        'p.WaitForExit()
        'p.StandardInput.WriteLine("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 8")
        'Dim strRst As String = p.StandardOutput.ReadToEnd()
        Try
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 8", "", 0)
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 2", "", 0)
            ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 1", "", 0)

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try


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


    Private Sub wb_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles wb.DocumentCompleted
        If wb.DocumentTitle = "检查明細" Then
            '打开摄像头
            Me.btnCam.Enabled = True
            '关闭摄像头
            Me.btnCamClose.Enabled = False

            If wb.Document.GetElementById("camera_flg").GetAttribute("value") = "1" Then
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


#Region "IE Broswer"

    '设置网页大小
    Public Sub InitPanelChrome()
        PanelChrome.Width = Me.Width
        PanelChrome.Height = Me.Height - 50
    End Sub

    Public Sub InitBrowser()
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
        wb.Url = New Uri(ConfigurationManager.AppSettings.Item("main_url"))


        wb.ObjectForScripting = Me
    End Sub

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
        Me.Invoke(New Action(Sub()

                                 Me.Text = (msg)

                                 'SendKeys.SendWait("{F8}")
                                 'Thread.Sleep(400)
                                 'SendKeys.Send(msg)
                                 sWaitTime(100)

                                 If LCase(wb.Document.ActiveElement.TagName) = "input" Then
                                     If wb.Document.ActiveElement.GetAttribute("typ") IsNot Nothing AndAlso LCase(wb.Document.ActiveElement.GetAttribute("typ")) = "scan" Then

                                         Dim e1 As Boolean = Me.btnCam.Enabled
                                         Dim e2 As Boolean = Me.btnCamClose.Enabled
                                         Dim e3 As Boolean = Me.btnSnap.Enabled
                                         SetSnapButtonEnabled(False)

                                         wb.Document.ActiveElement.InnerText = msg
                                         sWaitTime(500)
                                         SendKeys.SendWait("{ENTER}")

                                         Me.btnCam.Enabled = e1
                                         Me.btnCamClose.Enabled = e2
                                         Me.btnSnap.Enabled = e3

                                     End If
                                 End If
                             End Sub))


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
    Private Sub btnSnap_Click(sender As Object, e As EventArgs) Handles btnSnap.Click

        If wb.DocumentTitle <> "检查明細" Then
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


            Dim doc As HtmlDocument = wb.Document
            Dim line_id As String = wb.Document.GetElementById("hidLineId").GetAttribute("value")
            Dim chkNo_key As String = wb.Document.GetElementById("hidChkNo").GetAttribute("value")
            Dim chk_method_id = doc.InvokeScript("returnString").ToString()

            btnSnap.Text = "保存中"

            Dim rtv As String = My.WebServices.api.SaveIMG(chkNo_key, line_id, chk_method_id, m_byteImageBuffer)

            If rtv = "OK" Then
                MsgBox("图片保存成功")
                wb.Document.InvokeScript("InitKamera")
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


    Private Sub btnTEST_Click(sender As Object, e As EventArgs) Handles btnTEST.Click

        wb.Document.InvokeScript("InitKamera")

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
