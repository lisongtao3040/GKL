Imports AForge.Video
Imports AForge.Video.DirectShow
Imports AForge.Controls

Public Class Form1
    'private bool DeviceExist = false;
    '        private FilterInfoCollection videoDevices;
    '        private VideoCaptureDevice videoSource = null;

    Dim DeviceExist As Boolean = False
    Dim videoDevices As FilterInfoCollection
    Dim WithEvents videoSource As VideoCaptureDevice
    Private VideoSourcePlayer As VideoSourcePlayer

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub



    'private void getCamList()
    '       {
    '           try
    '           {
    '               videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
    '               comboBox1.Items.Clear();
    '               if (videoDevices.Count == 0)
    '                   throw new ApplicationException();

    '               DeviceExist = true;
    '               foreach (FilterInfo device in videoDevices)
    '               {
    '                   comboBox1.Items.Add(device.Name);
    '               }
    '               comboBox1.SelectedIndex = 0; //将第一个设置为默认选择蟻E
    '           }
    '           catch (ApplicationException)
    '           {
    '               DeviceExist = false;
    '               comboBox1.Items.Add("没有紒E獾绞悠瞪璞?");
    '           }
    '       }
    Private Sub getCamList()
        Try
            videoDevices = New FilterInfoCollection(FilterCategory.VideoInputDevice)
            Me.comboBox1.Items.Clear()

            Me.comboBox1.Items.Add(videoDevices.Item(0).Name)
            DeviceExist = True
        Catch ex As Exception

        End Try


    End Sub


    Private Sub rfsh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rfsh.Click
        getCamList()
        Me.comboBox1.SelectedIndex = 0
    End Sub

    Private Sub CloseVideoSource()

        If Not videoSource Is Nothing Then
            If (videoSource.IsRunning) Then

                videoSource.SignalToStop()
                videoSource = Nothing
            End If
        End If
    End Sub
    Private Sub start_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles start.Click

        If DeviceExist = True Then
            videoSource = New VideoCaptureDevice(videoDevices(0).MonikerString)



            videoSource.DesiredFrameSize = New Size(436, 360)
            videoSource.DesiredFrameRate = 10
            videoSource.Start()
            label2.Text = "设备正常运行中..."
            start.Text = "&停止"
            Timer1.Enabled = True



        End If



    End Sub

    Private Sub videoSource_NewFrame(ByVal sender As Object, ByVal eventArgs As AForge.Video.NewFrameEventArgs) Handles videoSource.NewFrame
        Dim img As Bitmap

        img = eventArgs.Frame.Clone()
        pictureBox1.Image = img
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PicCapture.BackgroundImage = Image.FromHbitmap(VideoSourcePlayer.GetCurrentVideoFrame().GetHbitmap())
        Dim ms As New MemoryStream()
        PicCapture.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
        'PicCapture.BackgroundImage.Save("d:\" & Now.ToString("yyyymmddhhmmss") & ".jpg")
        Dim rtv As String = My.WebServices.api.SaveIMG(chkNo_key, line_id, chk_method_id, ms.ToArray)
    End Sub
End Class