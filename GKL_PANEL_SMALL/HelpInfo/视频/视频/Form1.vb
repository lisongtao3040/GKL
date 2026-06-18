Public Class Form1

    ' Create constant using attend in function of DLL file.

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

    ' Declare function from AVI capture DLL.

#Region "Declare function from AVI capture DLL"
    'Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
    '    (ByVal hwnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, _
    '     ByVal lParam As Object) As Integer

    Private Declare Function SendMessage Lib "user32.dll" Alias "SendMessageA" _
  (ByVal hwnd As Integer, ByVal msg As Integer, _
   ByVal wParam As Integer, ByVal lParam As Integer) As Integer

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

    Private Sub LoadDeviceList()
        Dim strName As String = Space(100)
        Dim strVer As String = Space(100)
        Dim bReturn As Boolean
        Dim x As Integer = 0

        ' Load name of all avialable devices into the lstDevices .

        Do
            '   Get Driver name and version
            bReturn = capGetDriverDescriptionA(x, strName, 100, strVer, 100)
            ' If there was a device add device name to the list 
            If bReturn Then lstDevices.Items.Add(strName.Trim)
            x += 1
        Loop Until bReturn = False
    End Sub
#End Region

    ' To Create main Sub of video.

#Region "To create main sub of video"

    ' To display the output from a video capture device, you need to create a capture window.

    Private Sub OpenPreviewWindow()
        Dim iHeight As Integer = PicCapture.Height
        Dim iWidth As Integer = PicCapture.Width

        ' Open Preview window in picturebox .
        ' Create a child window with capCreateCaptureWindowA so you can display it in a picturebox.

        hHwnd = capCreateCaptureWindowA(iDevice, WS_VISIBLE Or WS_CHILD, 0, 0, 640, _
            480, PicCapture.Handle.ToInt32, 0)

        ' Connect to device
        If SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) Then

            ' Set the preview scale
            SendMessage(hHwnd, WM_CAP_SET_SCALE, True, 0)

            ' Set the preview rate in milliseconds
            SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0)

            ' Start previewing the image from the camera 
            SendMessage(hHwnd, WM_CAP_SET_PREVIEW, True, 0)

            ' Resize window to fit in picturebox 
            SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, PicCapture.Width, PicCapture.Height, _
                                   SWP_NOMOVE Or SWP_NOZORDER)
            btnSave.Enabled = True
            btnStop.Enabled = True
            btnStart.Enabled = False
        Else
            ' Error connecting to device close window 
            DestroyWindow(hHwnd)
            btnSave.Enabled = False
        End If
    End Sub

    ' Use SendMessage to copy the data to the clipboard Then transfer the image to the picture box. 

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        sfdImage.Filter = "图片|*.jpg|所有文件|*.*"
        Dim data As IDataObject
        Dim bmap As Image
        ' Copy image to clipboard 
        SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0)
        ' Get image from clipboard and convert it to a bitmap 
        data = Clipboard.GetDataObject()
        If data.GetDataPresent(GetType(System.Drawing.Bitmap)) Then
            bmap = CType(data.GetData(GetType(System.Drawing.Bitmap)), Image)
            PicCapture.Image = bmap
            ClosePreviewWindow()
            btnSave.Enabled = False
            btnStop.Enabled = False
            btnStart.Enabled = True
            If sfdImage.ShowDialog = DialogResult.OK Then
                bmap.Save(sfdImage.FileName, Imaging.ImageFormat.Bmp)
            End If
        End If
    End Sub

    ' Finally, to close the preview window, disconnect from the device and destroy the preview window. 

    Private Sub ClosePreviewWindow()
        ' Disconnect from device
        SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0)

        ' close window 
        DestroyWindow(hHwnd)
    End Sub

#End Region

    'Form control sub (Event).

#Region "Form control sub (Event)"

    'Form_load

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call LoadDeviceList()
    End Sub

    'btnStart_Click

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Call OpenPreviewWindow()
    End Sub

    'btnStop_Click

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Call ClosePreviewWindow()
    End Sub

#End Region

End Class