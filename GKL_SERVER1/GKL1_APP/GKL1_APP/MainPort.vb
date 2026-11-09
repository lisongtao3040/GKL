Imports System.Diagnostics
Imports System.IO
Imports System.Net
Imports System.Text

Public Class MainPort

    Private _listener As HttpListener
    Private _isRunning As Boolean = False
    Private ReadOnly _port As Integer = 5000

    ''' <summary>
    ''' 窗体加载时自动启动 HTTP 监听
    ''' </summary>
    Private Sub MainPort_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StartHttpServer(_port)
    End Sub

    ''' <summary>
    ''' 窗体关闭时安全停止监听，释放端口
    ''' </summary>
    Private Sub MainPort_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        StopHttpServer()
    End Sub

    ''' <summary>
    ''' 启动 HTTP 服务
    ''' </summary>
    Private Sub StartHttpServer(port As Integer)
        Try
            _listener = New HttpListener()
            _listener.Prefixes.Add(String.Format("http://localhost:{0}/", port))
            _listener.Start()
            _isRunning = True

            lblStatus.Text = String.Format("服务运行中：http://localhost:{0}/", port)
            lblStatus.ForeColor = Color.Green
            AppendLog(String.Format("服务已就绪，正在监听端口 {0}...", port))

            ' 开始异步监听接入
            _listener.BeginGetContext(AddressOf OnContextReceived, Nothing)

        Catch ex As Exception
            _isRunning = False
            lblStatus.Text = "启动失败: " & ex.Message
            lblStatus.ForeColor = Color.Red
            AppendLog("[错误] 启动服务失败: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 异步接收到请求的回调
    ''' </summary>
    Private Sub OnContextReceived(ar As IAsyncResult)
        If Not _isRunning OrElse _listener Is Nothing OrElse Not _listener.IsListening Then
            Return
        End If

        Try
            ' 1. 获取请求上下文
            Dim context As HttpListenerContext = _listener.EndGetContext(ar)

            ' 2. 立即启动下一次异步监听，保障后续并发响应
            _listener.BeginGetContext(AddressOf OnContextReceived, Nothing)

            ' 3. 处理当前请求
            ProcessRequest(context)

        Catch ex As ObjectDisposedException
            ' 窗体关闭时正常释放，忽略
        Catch ex As HttpListenerException
            If _isRunning Then
                AppendLog("[异常] 监听错误: " & ex.Message)
            End If
        Catch ex As Exception
            AppendLog("[异常] 请求处理异常: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 处理 HTTP 请求并跨域响应
    ''' </summary>
    Private Sub ProcessRequest(context As HttpListenerContext)
        Dim request As HttpListenerRequest = context.Request
        Dim response As HttpListenerResponse = context.Response

        ' 1. 跨域头设置 (CORS)
        response.Headers.Add("Access-Control-Allow-Origin", "*")
        response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS")
        response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept")

        ' 2. 响应 OPTIONS 预检请求
        If request.HttpMethod.ToUpper() = "OPTIONS" Then
            response.StatusCode = 204
            response.Close()
            Return
        End If

        Dim responseString As String = ""
        response.ContentType = "application/json; charset=utf-8"

        Try
            Dim path As String = request.Url.AbsolutePath.ToLower()

            Select Case path
                ' 心跳接口: GET /api/ping
                Case "/api/ping"
                    responseString = "{""status"":""ok"",""msg"":""MainPort is online""}"

                ' 启动本地程序接口: POST /api/launch
                Case "/api/launch"
                    If request.HttpMethod.ToUpper() = "POST" Then
                        Dim requestBody As String = ""
                        Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
                            requestBody = reader.ReadToEnd()
                        End Using

                        AppendLog(String.Format("收到启动指令，参数: {0}", requestBody))

                        ' 调用本地 EXE
                        Dim success As Boolean = LaunchLocalApp(requestBody)

                        If success Then
                            responseString = "{""status"":""success"",""msg"":""本地程序已唤起""}"
                        Else
                            response.StatusCode = 500
                            responseString = "{""status"":""error"",""msg"":""启动失败，请检查 EXE 路径""}"
                        End If
                    Else
                        response.StatusCode = 405
                        responseString = "{""status"":""error"",""msg"":""仅支持 POST 请求""}"
                    End If

                Case Else
                    response.StatusCode = 404
                    responseString = "{""status"":""error"",""msg"":""404 Not Found""}"
            End Select

        Catch ex As Exception
            response.StatusCode = 500
            responseString = String.Format("{{""status"":""error"",""msg"":""{0}""}}", ex.Message.Replace("""", "\"""))
        End Try

        ' 3. 输出数据并关闭响应
        Try
            Dim buffer As Byte() = Encoding.UTF8.GetBytes(responseString)
            response.ContentLength64 = buffer.Length
            Using output As Stream = response.OutputStream
                output.Write(buffer, 0, buffer.Length)
            End Using
        Catch
            ' 前端提前断开等情况忽略
        Finally
            response.Close()
        End Try
    End Sub

    ''' <summary>
    ''' 启动本地应用程序
    ''' </summary>
    Private Function LaunchLocalApp(arguments As String) As Boolean
        Try
            ' 实际需要调用的本地程序路径（按需修改）
            Dim targetExe As String = "C:\YourAppFolder\PictureImport.exe"

            ' 测试可用系统记事本：
            ' Dim targetExe As String = "notepad.exe"

            Dim psi As New ProcessStartInfo()
            psi.FileName = targetExe
            psi.Arguments = arguments
            psi.UseShellExecute = True

            Process.Start(psi)
            Return True
        Catch ex As Exception
            AppendLog("[启动错误] " & ex.Message)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 线程安全的 UI 日志追加
    ''' </summary>
    Private Sub AppendLog(message As String)
        If Me.InvokeRequired Then
            Me.BeginInvoke(New Action(Of String)(AddressOf AppendLog), message)
            Return
        End If

        Dim timeStamp As String = DateTime.Now.ToString("HH:mm:ss")
        txtLog.AppendText(String.Format("[{0}] {1}{2}", timeStamp, message, Environment.NewLine))
    End Sub

    ''' <summary>
    ''' 清空日志按钮事件
    ''' </summary>
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtLog.Clear()
    End Sub

    ''' <summary>
    ''' 安全关闭监听
    ''' </summary>
    Private Sub StopHttpServer()
        _isRunning = False
        If _listener IsNot Nothing Then
            Try
                _listener.Stop()
                _listener.Close()
            Catch
            End Try
        End If
    End Sub

End Class