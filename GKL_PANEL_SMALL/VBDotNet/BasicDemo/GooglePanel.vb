'Imports CefSharp
'Imports CefSharp.WinForms

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.IO.Ports
Imports System.Threading
Imports System.Configuration.ConfigurationSettings

Imports System.Runtime.InteropServices
Imports System.Threading.Thread
Imports System.Net.IPAddress

Imports Size = System.Drawing.Size
Imports System.IO

Imports AForge.Video
Imports AForge.Video.DirectShow
Imports AForge.Controls
Imports System.Configuration

Public Class GooglePanel

    Public IsDebug As Boolean = False

#Region "Create constant using attend in function of DLL file"
    Const WM_CAP As Short = &H400S
    Const WM_CAP_DRIVER_CONNECT As Integer = WM_CAP + 10
    Const WM_CAP_DRIVER_DISCONNECT As Integer = WM_CAP + 11
    Const WM_CAP_EDIT_COPY As Integer = WM_CAP + 30
    Const WM_CAP_SET_PREVIEW As Integer = WM_CAP + 50
    Const WM_CAP_SET_PREVIEWRATE As Integer = WM_CAP + 52
    Const WM_CAP_SET_SCALE As Integer = WM_CAP + 53
    Const WS_CHILD As Integer = &H40000000
    Const WS_VISIBLE As Integer = &H10000000
    Const SWP_NOMOVE As Short = &H2S
    Const SWP_NOSIZE As Short = 1
    Const SWP_NOZORDER As Short = &H4S
    Const HWND_BOTTOM As Short = 1

    Dim iDevice As Integer = 0  ' Normal device ID 
    Dim hHwnd As Integer  ' Handle value to preview window
#End Region


#Region "Declare function from AVI capture DLL"
    'Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
    '    (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, _
    '     ByVal lParam As Object) As Integer

    Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" _
      (ByVal hwnd As Integer, ByVal msg As Integer, _
       ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    'Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" _
    '  　　　　　(ByVal hWnd As Long, _
    '   　　　　　ByVal Msg As Long, _
    '   　　　　　ByVal wParam As Long, _
    '   　　　　　ByVal lParam As String) As Long

    Declare Function SetWindowPos Lib "user32" Alias "SetWindowPos" (ByVal hwnd As Integer, _
        ByVal hWndInsertAfter As Integer, ByVal x As Integer, ByVal y As Integer, _
        ByVal cx As Integer, ByVal cy As Integer, ByVal wFlags As Integer) As Integer

    Declare Function DestroyWindow Lib "user32" (ByVal hndw As Integer) As Boolean

    Declare Function capCreateCaptureWindowA Lib "avicap32.dll" _
        (ByVal lpszWindowName As String, ByVal dwStyle As Integer, _
        ByVal x As Integer, ByVal y As Integer, ByVal nWidth As Integer, _
        ByVal nHeight As Short, ByVal hWndParent As Integer, _
        ByVal nID As Integer) As Integer

    Declare Function capGetDriverDescriptionA Lib "avicap32.dll" (ByVal wDriver As Short, _
        ByVal lpszName As String, ByVal cbName As Integer, ByVal lpszVer As String, _
        ByVal cbVer As Integer) As Boolean

    Declare Sub Sleep Lib "kernel32" (ByVal milliseconds As Long)

#End Region

    ' Connect to the device.
#Region "Connect to the device"
    'Private Sub LoadDeviceList()
    '    Dim strName As String = Space(100)
    '    Dim strVer As String = Space(100)
    '    Dim bReturn As Boolean
    '    Dim x As Integer = 0

    '    ' Load name of all avialable devices into the lstDevices .

    '    Do
    '        '   Get Driver name and version
    '        bReturn = capGetDriverDescriptionA(x, strName, 100, strVer, 100)
    '        ' If there was a device add device name to the list 
    '        If bReturn Then lstDevices.Items.Add(strName.Trim)
    '        x += 1
    '    Loop Until bReturn = False
    'End Sub
#End Region

    ' To Create main Sub of video.
    Public DeviceExist As Boolean = False
    Public videoDevices As FilterInfoCollection
    Public WithEvents videoSource As VideoCaptureDevice
    Private VideoSourcePlayer As VideoSourcePlayer

#Region "To create main sub of video"

    ' To display the output from a video capture device, you need to create a capture window.




    Private Sub videoSource_NewFrame(ByVal sender As Object, ByVal eventArgs As AForge.Video.NewFrameEventArgs) Handles videoSource.NewFrame
        'Dim img As Bitmap
        'img = eventArgs.Frame.Clone()
        PicCapture.Image = eventArgs.Frame.Clone()
    End Sub


    Private Sub ClosePreviewWindow()
        ' Disconnect from device
        SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0)
        ' close window 
        DestroyWindow(hHwnd)
    End Sub

#End Region

    'Form control sub (Event).

#Region "Form control sub (Event)"

    ''' <summary>
    ''' 清除 Internet Explorer 的历史记录中指定日期之前的所有文件。
    ''' </summary>
    ''' <param name="daysToKeep">要保留的天数</param>
    ''' <returns>操作结果的描述</returns>
    Function ClearIEHistory(daysToKeep As Integer) As String
        Try
            ' 获取当前日期
            Dim currentDate As DateTime = DateTime.Now

            ' 计算截止日期
            Dim cutoffDate As DateTime = currentDate.AddDays(-daysToKeep)

            ' 获取 Internet Explorer 的历史记录路径
            Dim historyPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft\Windows\INetHistory")
            'Dim historyPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft\Windows\INetHistory")

            ' 检查路径是否存在
            If Not Directory.Exists(historyPath) Then
                Return "历史记录目录不存在。"
            End If

            ' 获取历史记录文件
            Dim historyFiles As String() = Directory.GetFiles(historyPath, "*.*", SearchOption.AllDirectories)

            Dim deletedFiles As New List(Of String)()

            For Each file As String In historyFiles
                Dim fileInfo As FileInfo = New FileInfo(file)
                If fileInfo.LastWriteTime < cutoffDate Then
                    Try
                        ' 删除旧的历史记录文件
                        fileInfo.Delete()
                        deletedFiles.Add(fileInfo.FullName)
                    Catch ex As Exception
                        Return $"删除 {fileInfo.FullName} 时发生错误: {ex.Message}"
                    End Try
                End If
            Next

            Return $"已删除以下文件: {String.Join(", ", deletedFiles)}"
        Catch ex As Exception
            Return $"发生错误: {ex.Message}"
        End Try
    End Function

    'Form Load
    Private Sub GooglePanel_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim videoDevices As New FilterInfoCollection(FilterCategory.VideoInputDevice)

        'If (videoDevices.Count = 0) Then
        '    MessageBox.Show("未发现视频设备！")
        '    Exit Sub

        'End If

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

            If Not IsDebug Then
                '清除 Internet Explorer 的历史记录中指定日期之前的所有文件。
                ClearIEHistory(1)
                'ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 8", "", 0)
                'ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 2", "", 0)
                'ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 1", "", 0)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        ' http://10.160.192.114/gkl2020_panel/publish.htm


        '设置窗口大小 最大化
        Me.WindowState = FormWindowState.Maximized

        '大平板使用
        '发行地址
        '\\10.160.192.114\gkl2020_panel
        ''------------------------------------
        ''COM 口设置  默认连接COM1
        'ComNameInit()
        'If Me.cbComName.Items.Count >= 2 Then
        '    Me.cbComName.SelectedIndex = 1
        '    ConnectCom()
        'End If

        'http://10.160.192.114/gkl2020_panel_small/publish.htm
        '小平板使用
        '发行地址
        '\\10.160.192.114\gkl2020_panel_small
        Panel1.Visible = False

        '装载网页
        InitBrowser()

        '设置网页大小
        InitPanelChrome()

        '获得摄像头列表
        GetCamList()

    End Sub


    Private Sub GooglePanel_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Try
            VideoSourcePlayer.SignalToStop()
            VideoSourcePlayer.WaitForStop()
            videoSource.SignalToStop()
            videoSource.WaitForStop()
            serialPort.Close()

        Catch ex1 As Exception

        End Try
    End Sub

    '打开摄像头
    Private Sub btnStart_Click(sender As Object, e As EventArgs)
        Call OpenPreviewWindow()
    End Sub
    Private Sub OpenPreviewWindow()

        If PicCapture Is Nothing Then
            PicCapture = New Windows.Forms.PictureBox
            Me.Controls.Add(PicCapture)
        End If

        Dim iHeight As Integer = PicCapture.Height
        Dim iWidth As Integer = PicCapture.Width

        If DeviceExist = True Then

            If VideoSourcePlayer Is Nothing Then
                VideoSourcePlayer = New AForge.Controls.VideoSourcePlayer()
                VideoSourcePlayer.Width = 1920
                VideoSourcePlayer.Height = 1080
                'Me.Controls.Add(VideoSourcePlayer)
            End If

            If VideoSourcePlayer.IsRunning = True Then
                VideoSourcePlayer.SignalToStop()
                VideoSourcePlayer.WaitForStop()
            End If

            videoSource = New VideoCaptureDevice(videoDevices(lstDevices.SelectedIndex).MonikerString)
            'videoSource.DesiredFrameSize = New Size(iWidth, iHeight)
            'videoSource.DesiredFrameSize = New Size(1920, 1080)
            'videoSource.DesiredFrameRate = 1

            Dim wh As Integer = 0
            For Each capab In videoSource.VideoCapabilities
                If wh < capab.FrameSize.Width + capab.FrameSize.Height Then
                    wh = capab.FrameSize.Width + capab.FrameSize.Height
                    videoSource.VideoResolution = capab
                End If
            Next

            'videoSource.Start()
            VideoSourcePlayer.VideoSource = videoSource
            VideoSourcePlayer.Start()

            While Not VideoSourcePlayer.IsRunning
                Application.DoEvents()              'こちらだけではCPUの使用率が100％になる
                '指定した時間だけ現在のスレッドを中断します。
                System.Threading.Thread.Sleep(10)   'これだけでは他の処理を受け付けない

            End While

            Me.btnSnap.Enabled = True
            Me.PicCapture.Visible = True

            'label2.Text = "设备正常运行中..."
        End If

        sWaitTime(500)

    End Sub
    '关闭摄像头
    Private Sub btnCamClose_Click(sender As Object, e As EventArgs) Handles btnCamClose.Click

        'PicCapture.Stop()

        '打开摄像头
        Me.btnCam.Enabled = True
        '关闭摄像头
        Me.btnCamClose.Enabled = False
        '拍照
        Me.btnSnap.Enabled = False
        '摄像画面
        Me.PicCapture.Visible = False

        Try
            VideoSourcePlayer.SignalToStop()
            VideoSourcePlayer.WaitForStop()
            videoSource.SignalToStop()
            videoSource.WaitForStop()
        Catch ex As Exception

        End Try


        SnapShowFlg = False

    End Sub

    'btnStop_Click
    Private Sub btnStop_Click1(sender As Object, e As EventArgs)
        Call ClosePreviewWindow()
    End Sub

    Private Sub wb_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles wb.DocumentCompleted

        ''debug
        'Me.btnSnap.Visible = True
        'Me.btnCam.Visible = True
        'Me.btnCamClose.Visible = True
        'PicCapture.Visible = True
        'Exit Sub

        If IsDebug Then
            Me.btnSnap.Visible = True
            Me.btnCam.Visible = True
            Me.btnCamClose.Visible = True
            Me.PicCapture.Visible = True
            Exit Sub
        End If

        'If wb.ReadyState = WebBrowserReadyState.Complete AndAlso wb.IsBusy = False Then

        If wb.DocumentTitle = "检查明細" Then
            'If btnSnap.Enabled Then
            '    '打开摄像头
            '    Me.btnCam.Enabled = False
            '    '关闭摄像头
            '    Me.btnCamClose.Enabled = True

            'Else
            '    '打开摄像头
            '    Me.btnCam.Enabled = True
            '    '关闭摄像头
            '    Me.btnCamClose.Enabled = False
            'End If

            Try
                If wb.Document.GetElementById("camera_flg").GetAttribute("value") = "1" Then
                    Me.btnSnap.Visible = True
                    Me.btnCam.Visible = True
                    Me.btnCamClose.Visible = True
                    PicCapture.Visible = False
                End If
            Catch ex As Exception
            End Try
        Else
            ''打开摄像头
            'Me.btnCam.Enabled = False
            ''关闭摄像头
            'Me.btnCamClose.Enabled = False
            Me.btnSnap.Visible = False
            Me.btnCam.Visible = False
            Me.btnCamClose.Visible = False
            PicCapture.Visible = False
        End If

        'End If

    End Sub

#End Region

    '------------------------------------------------------------------------------------------
    'COM
    '------------------------------------------------------------------------------------------
    'Public browser As ChromiumWebBrowser
    Public serialPort As SerialPort


#Region "System"
    Private WM_KEYDOWN As Integer = &H100
    Public scanAllTxt As String = ""
    Public oldScanTime As TimeSpan = New TimeSpan(Now.Ticks)


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


    '获得摄像头列表
    Private Sub GetCamList()
        Try
            videoDevices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
            Me.lstDevices.Items.Clear()

            For i As Integer = 0 To videoDevices.Count - 1
                Me.lstDevices.Items.Add(videoDevices.Item(i).Name)
            Next


            If lstDevices.Items.Count > 0 Then
                'lstDevices.SelectedIndex = lstDevices.Items.Count - 1
                lstDevices.SelectedIndex = 0
            End If
            DeviceExist = True
        Catch ex As Exception

        End Try
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
    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStopConnect.Click
        Try
            SetControlStaus(False)
            serialPort.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
        'browser = New ChromiumWebBrowser("http://10.160.192.114/GKL/")
        'browser = New ChromiumWebBrowser("http://localhost:60663/")
        'PanelChrome.Controls.Add(browser)
        'browser.Dock = DockStyle.Fill
        'wb.Url = New Uri("http://localhost:60663/")
        'wb.Url = New Uri("http://home001/GKL/Default.aspx")
        'wb.Url = New Uri("http://10.160.192.114/GKL/")
        'wb.Url = New Uri("http://ot5600/GKL/Default.aspx")
        'wb.Url = New Uri("http://ot5600/GKL/Default.aspx")
        'wb.Url = New Uri("http://jxs001/GKL/Default.aspx")


        'wb.Url = New Uri("http://10.160.192.114/GKL_TEST/Default.aspx")

        '元服务器
        'wb.Url = New Uri("http://10.160.192.114/GKL2020/Default.aspx")
        wb.Url = New Uri("http://localhost:60669/Default.aspx")

        '新2号服务器
        'wb.Url = New Uri("http://DLTDLFSDB004/GKL2020/Default.aspx")
        '
        'wb.Url = New Uri("http://www.baidu.com")
        'wb.Url = New Uri(ConfigurationManager.AppSettings.Item("main_url"))

        'wb.Url = New Uri("http://localhost:60663/HtmlPage3.html")


        wb.ObjectForScripting = Me
    End Sub

#End Region


#Region "Com"

    'COM LIST
    Private Sub ComNameInit()
        Me.btnConnect.Enabled = True
        Me.btnStopConnect.Enabled = False
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
        Me.btnStopConnect.Enabled = connectStaus
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
    'Public SnapOpen As Boolean = False
    '------------------------------------------------------------------------------------------
    '打开摄像头
    '------------------------------------------------------------------------------------------
    Private Sub btnCam_Click(sender As Object, e As EventArgs) Handles btnCam.Click

        Me.PicCapture.Visible = True

        Dim openFlg As Boolean = True
        Dim e1 As Boolean = Me.btnCam.Enabled
        Dim e2 As Boolean = Me.btnCamClose.Enabled
        Dim e3 As Boolean = Me.btnSnap.Enabled

        Try
            Call OpenPreviewWindow()
            'If SnapOpen = False Then

            '    SnapOpen = True
            'Else
            '    Me.PicCapture.Visible = True
            'End If

        Catch ex As Exception
            openFlg = False
        End Try

        If openFlg Then
            '打开摄像头
            Me.btnCam.Enabled = False
            '关闭摄像头
            Me.btnCamClose.Enabled = True
            '拍照
            Me.btnSnap.Enabled = True

            Me.PicCapture.Visible = True

        Else

            Me.btnCam.Enabled = e1
            Me.btnCamClose.Enabled = e2
            Me.btnSnap.Enabled = e3

        End If

        SnapShowFlg = PicCapture.Visible

    End Sub


    '拍照
    Private Sub btnSnap_Click(sender As Object, e As EventArgs) Handles btnSnap.Click

        If wb.DocumentTitle <> "检查明細" Then
            MsgBox("不是检查画面")
            Exit Sub
        End If

        Me.btnSnap.Enabled = False
        btnSnap.Text = "保存中"

        'While Not (wb.ReadyState = WebBrowserReadyState.Complete AndAlso wb.IsBusy = False)
        '    Application.DoEvents()              'こちらだけではCPUの使用率が100％になる
        '    '指定した時間だけ現在のスレッドを中断します。
        '    System.Threading.Thread.Sleep(10)   'これだけでは他の処理を受け付けない
        'End While

        Dim Td As System.Threading.Thread

        Td = New System.Threading.Thread(AddressOf SubSnapping) '实例化,指向abc过程

        Td.Start() '开始运行线程


        'Me.btnSnap.Enabled = False

        'Dim doc As HtmlDocument = wb.Document
        'Dim line_id As String = wb.Document.GetElementById("hidLineId").GetAttribute("value")
        'Dim chkNo_key As String = wb.Document.GetElementById("hidChkNo").GetAttribute("value")
        'Dim chk_method_id = doc.InvokeScript("returnString").ToString()

        'btnSnap.Text = "保存中"





        ''videoSource.GetCameraProperty 
        'PicCapture.BackgroundImage = Image.FromHbitmap(VideoSourcePlayer.GetCurrentVideoFrame().GetHbitmap())
        'Dim ms As New MemoryStream()
        'PicCapture.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        ''PicCapture.BackgroundImage.Save("d:\" & Now.ToString("yyyymmddhhmmss") & ".jpg")

        'Try
        '    Dim rtv As String = My.WebServices.api.SaveIMG(chkNo_key, line_id, chk_method_id, ms.ToArray)
        '    If rtv = "OK" Then
        '        MsgBox("图片保存成功")
        '        wb.Document.InvokeScript("ImgsInit")
        '    Else
        '        MsgBox(rtv)
        '    End If
        'Catch ex As Exception

        'End Try


        'btnSnap.Text = "拍照"

        'Me.btnSnap.Enabled = True


    End Sub

    Delegate Sub Dg(ByRef picBox As System.Windows.Forms.PictureBox, ByRef wbIn As WebBrowser, ByRef btn As Windows.Forms.Button)

    Sub SubSnapping()
        Dim Dg_txt As New Dg(AddressOf Snapping)
        Me.Invoke(Dg_txt, PicCapture, wb, btnSnap)
    End Sub

    Sub Snapping(ByRef picBox As System.Windows.Forms.PictureBox, ByRef wbIn As WebBrowser, ByRef btn As Windows.Forms.Button)


        Me.btnSnap.Enabled = False
        btnSnap.Text = "保存中"
        Dim rtv As String
        Try
            Dim doc As HtmlDocument = wb.Document
            Dim line_id As String = wb.Document.GetElementById("hidLineId").GetAttribute("value")
            Dim chkNo_key As String = wb.Document.GetElementById("hidChkNo").GetAttribute("value")
            Dim chk_method_id = doc.InvokeScript("returnString").ToString()
            picBox.BackgroundImage = Image.FromHbitmap(VideoSourcePlayer.GetCurrentVideoFrame().GetHbitmap())
            Dim ms As New MemoryStream()
            picBox.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)


            VideoSourcePlayer.SignalToStop()
            VideoSourcePlayer.WaitForStop()
            videoSource.SignalToStop()
            videoSource.WaitForStop()
            '打开摄像头
            Me.btnCam.Enabled = True
            '关闭摄像头
            Me.btnCamClose.Enabled = False

            rtv = My.WebServices.api.SaveIMG(chkNo_key, line_id, chk_method_id, ms.ToArray)

        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("图片保存Error")
            btn.Text = "拍照"
            btn.Enabled = True
            Exit Sub
        End Try

        If rtv = "OK" Then
            Try
                wbIn.Document.InvokeScript("ImgsInit")
            Catch ex As Exception

            End Try
            MsgBox("图片保存成功")
        Else
            MsgBox(rtv)
        End If
        btn.Text = "拍照"

        Me.PicCapture.Visible = False
        'btn.Enabled = True

    End Sub


    Private Sub CloseVideoSource()

        If Not videoSource Is Nothing Then
            If (videoSource.IsRunning) Then

                videoSource.SignalToStop()
                videoSource = Nothing
            End If
        End If
    End Sub

#End Region


    Private Sub btnTEST_Click(sender As Object, e As EventArgs) Handles btnTEST.Click

        wb.Document.InvokeScript("ImgsInit")

    End Sub


End Class
