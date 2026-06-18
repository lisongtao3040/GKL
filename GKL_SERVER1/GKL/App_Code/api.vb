Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Text
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.Runtime.Serialization.Json
Imports System
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.IO
Imports System.Net

Imports System.Linq


Imports System.Net.Http
Imports System.ComponentModel

'Imports System.Web.Script.Services.ScriptService


' この Web サービスを、スクリプトから ASP.NET AJAX を使用して呼び出せるようにするには、次の行のコメントを解除します。
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class api
    Inherits System.Web.Services.WebService

    Private Shared ReadOnly logLock As New Object()


    <WebMethod()>
    Public Function SaveIMG(ByVal chkNo_key As String, ByVal line_id As String, ByVal chk_method_id As String, ByVal img() As Byte) As String

        'img = img.Replace("data:image/png;base64,", "").Replace(" ", "+")
        'img = img.Replace("data:image/jpeg;base64,", "")
        Dim TCheckMsBC As New TCheckMsBC
        Dim dt As DataTable = TCheckMsBC.GetZuofanAndLine(chkNo_key)
        Dim gongDanHao As String = "" '工单号
        Dim user_no As String = ""
        If dt.Rows.Count > 0 Then
            line_id = dt.Rows(0).Item("line_id").ToString
            gongDanHao = dt.Rows(0).Item("make_no").ToString
            user_no = dt.Rows(0).Item("ins_user").ToString
        End If


        Dim img_save_path As String = ConfigurationManager.AppSettings("img_save_path").ToString()
        If Not System.IO.Directory.Exists(img_save_path) Then
            System.IO.Directory.CreateDirectory(img_save_path)
        End If

        'img_save_path = img_save_path & line_id & "\"
        'If Not System.IO.Directory.Exists(img_save_path) Then
        '    System.IO.Directory.CreateDirectory(img_save_path)
        'End If

        'img_save_path = img_save_path & chkNo_key & "\"
        'If Not System.IO.Directory.Exists(img_save_path) Then
        '    System.IO.Directory.CreateDirectory(img_save_path)
        'End If

        img_save_path = img_save_path & Now.ToString("yyyyMMdd") & "\"
        If Not System.IO.Directory.Exists(img_save_path) Then
            System.IO.Directory.CreateDirectory(img_save_path)
        End If


        img_save_path = img_save_path & line_id & "\"
        If Not System.IO.Directory.Exists(img_save_path) Then
            System.IO.Directory.CreateDirectory(img_save_path)
        End If




        'Dim img_path As String = img_save_path & chk_method_id & "_" & Now.ToString("yyyyMMddHHmmssfff") & ".jpg"

        Dim img_path As String = img_save_path & gongDanHao & "_" & Now.ToString("yyyyMMddHHmmssfff") & ".jpg"




        'Dim img_path As String = img_save_path & chkNo_key & "_" & Now.ToString("MMddHHmmssfff") & ".jpg"

        'Dim signedFromUmt As String = System.Text.Encoding.GetEncoding("utf-8").GetString(Convert.FromBase64String(img))

        'Using fs As New FileStream(img_path, FileMode.Create)
        '    Using bw As New BinaryWriter(fs)
        '        Dim data() As Byte = Convert.FromBase64String(img)
        '        bw.Write(data)
        '        bw.Close()
        '    End Using
        'End Using


        Try
            'My.Computer.FileSystem.WriteAllBytes("Image.jpg", m_byteImageBuffer, False)
            My.Computer.FileSystem.WriteAllBytes(img_path, img, False)

            Dim BC As New TCheckMsBC
            BC.InsImgInfo(chkNo_key, img_path)

            'MsgBox("图片保存成功")
            'GetImgPath()
            Return "OK"
        Catch ex As Exception
            Return ex.Message
            MsgBox(ex.Message)
        End Try



    End Function







    ''' <summary>
    ''' 准备影像联动信
    ''' </summary>
    ''' <returns></returns>
    <WebMethod()>
    Public Function SetJunbiYXLD(ByVal no As String, ByVal cd As String, ByVal line As String, ByVal user As String) As String

        If Common.YXLD_INFOS.ContainsKey(user) Then
            Common.YXLD_INFOS.Item(user) = no & "|" & cd & "|" & line
        Else
            Common.YXLD_INFOS.Add(user, no & "|" & cd & "|" & line)
        End If

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        sb.AppendLine("DELETE FROM m_yxld")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND no = '" & no & "'")
        sb.AppendLine("AND cd = '" & cd & "'")
        sb.AppendLine("AND line = '" & line & "'")


        sb.AppendLine("INSERT INTO m_yxld")
        sb.AppendLine("SELECT")
        'SQL文
        sb.AppendLine(" '" & no & "'")
        sb.AppendLine(", '" & cd & "'")
        sb.AppendLine(", '" & line & "'")
        sb.AppendLine(", ''")
        sb.AppendLine(", getdate()")
        sb.AppendLine(", getdate()")
        'dateadd(day,-1,getdate())
        sb.AppendLine("DELETE FROM m_yxld_log WHERE ins_date<dateadd(day,-10,getdate())")
        sb.AppendLine("INSERT INTO m_yxld_log")
        sb.AppendLine("SELECT")
        'SQL文
        sb.AppendLine(" '" & no & "'")
        sb.AppendLine(", '" & cd & "'")
        sb.AppendLine(", '" & line & "'")
        sb.AppendLine(", ''")
        sb.AppendLine(", getdate()")
        sb.AppendLine(", N'0.准备'")

        Dim shn As New SqlHelperNew
        shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), Nothing)

        Dim BC As New TCheckMsBC()

        Return BC.GetDbDate
        'Return "OK"
    End Function

    ''' <summary>
    ''' 影像联动信息取得
    ''' </summary>
    ''' <returns></returns>
    <WebMethod()>
    Public Function GetYXLD(ByVal line As String) As String

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT top 1 * ")
        sb.AppendLine("FROM m_yxld")
        sb.AppendLine("WHERE")
        sb.AppendLine(" line = '" & line & "'")
        'sb.AppendLine("AND txt = ''")
        sb.AppendLine("ORDER BY ins_date desc")
        Dim dsInfo As New Data.DataSet
        Dim shn As New SqlHelperNew
        shn.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetYXLD")




        If dsInfo.Tables(0).Rows.Count > 0 Then
            sb.Length = 0
            sb.AppendLine("INSERT INTO m_yxld_log")
            sb.AppendLine("SELECT")
            'SQL文
            sb.AppendLine(" ''")
            sb.AppendLine(", ''")
            sb.AppendLine(", '" & line & "'")
            sb.AppendLine(", N'" & dsInfo.Tables(0).Rows(0).Item("no") & "," & dsInfo.Tables(0).Rows(0).Item("cd") & "," & dsInfo.Tables(0).Rows(0).Item("line") & "'")
            sb.AppendLine(", getdate()")
            sb.AppendLine(", N'1.接口1_OK'")
            shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), Nothing)
            Return dsInfo.Tables(0).Rows(0).Item("no") & "," & dsInfo.Tables(0).Rows(0).Item("cd") & "," & dsInfo.Tables(0).Rows(0).Item("line")
        Else
            sb.Length = 0
            sb.AppendLine("INSERT INTO m_yxld_log")
            sb.AppendLine("SELECT")
            'SQL文
            sb.AppendLine(" ''")
            sb.AppendLine(", ''")
            sb.AppendLine(", '" & line & "'")
            sb.AppendLine(", N''")
            sb.AppendLine(", getdate()")
            sb.AppendLine(", N'1.接口1_NG'")
            shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), Nothing)
            Return "NG:没有待检查数据！！"
        End If
        'Return dsInfo.Tables("GetZuofanAndLine")
        'Return "9013059044,WA-AA07H-MGL9,SRM1551B"

    End Function

    Public Shared ReadOnly Padlock As Object = New Object()
    ''' <summary>
    ''' 影像检查结果取得
    ''' </summary>
    ''' <param name="line"></param>
    ''' <returns></returns>
    <WebMethod()>
    Public Function GetYXLD_RLT(ByVal no As String, ByVal cd As String, ByVal line As String, ByVal start_time As String) As String
        Dim sb As New StringBuilder
        Dim shn As New SqlHelperNew

        SyncLock Padlock

            Threading.Thread.Sleep(10)

            Try
                'SQLコメント
                '--**テーブル：检查结果 : t_check_ms

                'SQL文
                sb.AppendLine("SELECT txt ")
                sb.AppendLine("FROM m_yxld")
                sb.AppendLine("WHERE 1=1")
                sb.AppendLine("AND no = '" & no & "'")
                sb.AppendLine("AND cd = '" & cd & "'")
                sb.AppendLine("AND line = '" & line & "'")
                sb.AppendLine("AND upd_date > '" & start_time & "'")
                sb.AppendLine("AND txt <> ''")

                Dim dsInfo As New Data.DataSet

                shn.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetYXLD_RLT")

                If dsInfo.Tables(0).Rows.Count > 0 Then
                    sb.Length = 0
                    sb.AppendLine("INSERT INTO m_yxld_log")
                    sb.AppendLine("SELECT")
                    'SQL文
                    sb.AppendLine(" '" & no & "'")
                    sb.AppendLine(", '" & cd & "'")
                    sb.AppendLine(", '" & line & "'")
                    sb.AppendLine(", N'" & dsInfo.Tables(0).Rows(0).Item("txt") & "'")
                    sb.AppendLine(", getdate()")
                    sb.AppendLine(", N'3.结果OK'")
                    shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), Nothing)
                    Threading.Thread.Sleep(10)
                    Return dsInfo.Tables(0).Rows(0).Item("txt")
                Else

                    sb.Length = 0
                    sb.AppendLine("INSERT INTO m_yxld_log")
                    sb.AppendLine("SELECT")
                    'SQL文
                    sb.AppendLine(" '" & no & "'")
                    sb.AppendLine(", '" & cd & "'")
                    sb.AppendLine(", '" & line & "'")
                    sb.AppendLine(", N''")
                    sb.AppendLine(", getdate()")
                    sb.AppendLine(", N'3.结果NEXT'")
                    shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), Nothing)
                    Threading.Thread.Sleep(10)
                    Return "NEXT"
                End If
            Catch ex As Exception
                sb.Length = 0
                sb.AppendLine("INSERT INTO m_yxld_log")
                sb.AppendLine("SELECT")
                'SQL文
                sb.AppendLine(" '" & no & "'")
                sb.AppendLine(", '" & cd & "'")
                sb.AppendLine(", '" & line & "'")
                sb.AppendLine(", N'Exception'")
                sb.AppendLine(", getdate()")
                sb.AppendLine(", N'3.结果NG'")
                shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), Nothing)
                Threading.Thread.Sleep(10)
                Return "NG"
            End Try
        End SyncLock
        'Return dsInfo.Tables("GetZuofanAndLine")
        'Return "9013059044,WA-AA07H-MGL9,SRM1551B"

    End Function

    ''' <summary>
    ''' 影像联动信息取得
    ''' </summary>
    ''' <returns></returns>
    <WebMethod()>
    Public Function SetYXLD(ByVal no As String, ByVal cd As String, ByVal line As String, ByVal txt As String) As String
        Dim sb As New StringBuilder
        Dim shn As New SqlHelperNew
        Try
            'SQLコメント
            '--**テーブル：检查结果 : t_check_ms

            sb.AppendLine("UPDATE  m_yxld SET")
            sb.AppendLine("txt = N'" & txt & "'")
            sb.AppendLine(",upd_date = getdate()")
            sb.AppendLine("WHERE 1=1")
            sb.AppendLine("AND no = '" & no & "'")
            sb.AppendLine("AND cd = '" & cd & "'")
            sb.AppendLine("AND line = '" & line & "'")


            shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), Nothing)


            sb.Length = 0
            sb.AppendLine("INSERT INTO m_yxld_log")
            sb.AppendLine("SELECT")
            'SQL文
            sb.AppendLine(" '" & no & "'")
            sb.AppendLine(", '" & cd & "'")
            sb.AppendLine(", '" & line & "'")
            sb.AppendLine(", N'" & txt & "'")
            sb.AppendLine(", getdate()")
            sb.AppendLine(", N'2.接口1_OK'")
            shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), Nothing)

            Return "OK"
        Catch ex As Exception

            sb.Length = 0
            sb.AppendLine("INSERT INTO m_yxld_log")
            sb.AppendLine("SELECT")
            'SQL文
            sb.AppendLine(" ''")
            sb.AppendLine(", ''")
            sb.AppendLine(", '" & line & "'")
            sb.AppendLine(", N'Exception'")
            sb.AppendLine(", getdate()")
            sb.AppendLine(", N'2.接口1_NG'")
            shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), Nothing)

            Return "NG:" & ex.Message

        End Try



    End Function

    <WebMethod()>
    Public Function PostURL(ByVal url As String) As String
        Try

            ServicePointManager.Expect100Continue = False
            Dim request As HttpWebRequest = WebRequest.Create(url)
            '//Post请求方式
            request.Method = "POST"

            '内容类型
            request.ContentType = "application/x-www-form-urlencoded"
            '将URL编码后的字符串转化为字节
            Dim encoding As New UTF8Encoding()
            Dim bys As Byte() = encoding.GetBytes("")
            '设置请求的 ContentLength 
            request.ContentLength = bys.Length
            '获得请 求流
            Dim newStream As Stream = request.GetRequestStream()
            'newStream.Write(bys, bys.Length)
            newStream.Write(bys, 0, bys.Length)
            newStream.Close()
            '获得响应流
            Dim sr As StreamReader = New StreamReader(request.GetResponse().GetResponseStream)
            Return sr.ReadToEnd
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <WebMethod()>
    Public Function SetServerStorage(ByVal login_user_cd As String, ByVal key As String, ByVal value As String) As String
        Dim sb As New StringBuilder
        Dim shn As New SqlHelperNew



        'CREATE Table [dbo].[m_ServerStorage](
        '[login_user_cd] [nvarchar](20) Not NULL,
        '[key] [nvarchar](20) Not NULL,
        '[value] [nvarchar](max) NULL, Constraint [PK_m_ServerStorage] PRIMARY KEY CLUSTERED 
        '(
        '[login_user_cd] Asc,
        '[key] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]




        Try

            sb.AppendLine("delete from  m_ServerStorage where")
            sb.AppendLine("login_user_cd = N'" & login_user_cd & "'")
            sb.AppendLine("AND [key] = N'" & key & "'")
            sb.AppendLine("insert into  m_ServerStorage select")
            sb.AppendLine("N'" & login_user_cd & "',")
            sb.AppendLine("N'" & key & "',")
            sb.AppendLine("N'" & value & "'")
            shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), Nothing)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod()>
    Public Function GetServerStorage(ByVal login_user_cd As String, ByVal key As String) As String
        Dim sb As New StringBuilder
        Dim shn As New SqlHelperNew

        'CREATE Table [dbo].[m_ServerStorage](
        '[login_user_cd] [nvarchar](20) Not NULL,
        '[key] [nvarchar](20) Not NULL,
        '[value] [nvarchar](max) NULL, Constraint [PK_m_ServerStorage] PRIMARY KEY CLUSTERED 
        '(
        '[login_user_cd] Asc,
        '[key] ASC
        ')WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ') ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


        Try
            sb.AppendLine("SELECT ")
            sb.AppendLine("*")
            sb.AppendLine("FROM [m_ServerStorage]")
            sb.AppendLine("where")
            sb.AppendLine("login_user_cd = N'" & login_user_cd & "'")
            sb.AppendLine("AND [key] = N'" & key & "'")

            Dim dsInfo As New Data.DataSet
            shn.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_ServerStorage")

            If dsInfo.Tables(0).Rows.Count > 0 Then
                Return dsInfo.Tables(0).Rows(0).Item("value").ToString()
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    <WebMethod()>
    Public Function WriteSerLog(ByVal user_cd As String, ByVal txt As String) As String
        'Dim logFolderPath As String = Path.Combine(Directory.GetCurrentDirectory(), "log")

        Dim logFolderPath As String = "\\\\10.160.192.114\\WebShare\\GKL_LOG"

        Dim logFilePath As String = Path.Combine(logFolderPath, user_cd & "_" & DateTime.Now.ToString("yyyyMMdd") & ".log")

        ' 检查并创建日志文件夹
        If Not Directory.Exists(logFolderPath) Then
            Directory.CreateDirectory(logFolderPath)
        End If

        ' 使用锁以确保线程安全
        SyncLock logLock

            Try
                ' 检查并创建日志文件
                If Not File.Exists(logFilePath) Then
                    File.Create(logFilePath).Dispose() ' 创建文件并释放资源
                End If

                ' 写入日志内容
                Using writer As New StreamWriter(logFilePath, True)
                    writer.WriteLine(Now.ToString("yyyy-MM-dd HH:mm:ss") & " : " & txt)
                End Using
            Catch ex As Exception
                Return ""
            End Try

        End SyncLock

        Return ""

    End Function


    <WebMethod()>
    Public Function GetHujiao(ByVal line_id As String) As String
        Dim da As New HuJiaoDA

        If line_id = "" Then
            line_id = "211"
        End If

        If line_id.Trim.Length = 8 Then
            line_id = line_id.Trim
            line_id = Left(Right(line_id, 4), 3)

        End If

        Dim dt As Data.DataTable = da.Get_m_station_list(line_id)

        Dim lst As List(Of String) = New List(Of String)()

        For i As Integer = 0 To dt.Rows.Count - 1
            lst.Add("STATION/" & dt.Rows(i).Item("stationNo").ToString())
        Next

        Return String.Join("|", lst)

    End Function

    ''' <summary>
    ''' 根据检查番号获取打印行代码关系数据
    ''' </summary>
    ''' <param name="chk_no">检查番号</param>
    ''' <returns>JSON格式的数据</returns>
    <WebMethod()>
    Public Function GetPrintLinesCodeRelationByChkNo(ByVal chk_no As String) As String
        Try
            'SQLコメント
            '--**テーブル:检查结果 : t_check_result, 打印行代码关系 : m_print_lines_code_relation
            Dim sb As New StringBuilder
            sb.AppendLine("SELECT ")
            sb.AppendLine("      a.[make_no]")
            sb.AppendLine("      ,isnull(b.CD,a.code) CD")
            sb.AppendLine("      ,b.line_CD")
            sb.AppendLine("      ,d.amount suu")
            sb.AppendLine("      ,d.[sapOderNo]")
            sb.AppendLine("      ,d.[sapIndexNo]")
            sb.AppendLine("      ,d.H")
            sb.AppendLine("      ,d.W")
            sb.AppendLine("      ,b.DH")
            sb.AppendLine("      ,b.DW")
            sb.AppendLine("      ,d.SW")
            sb.AppendLine("      ,d.FW KW")
            sb.AppendLine("      ,'' lot_no")
            sb.AppendLine("      ,d.ProduceOrder shunwei")
            sb.AppendLine("      ,a.chk_no")
            sb.AppendLine("      ,b.J_CD")
            sb.AppendLine("  FROM [t_check_result] a")
            sb.AppendLine("  LEFT JOIN [m_print_lines_code_relation] b")
            sb.AppendLine("  ON a.code = b.J_CD")
            sb.AppendLine("  LEFT JOIN [dbo].[m_print_history] c")
            sb.AppendLine("  ON a.chk_no = c.chk_no")
            sb.AppendLine("  AND b.CD = c.CD")
            sb.AppendLine("  LEFT JOIN TCM_BianPlan d")
            sb.AppendLine("  ON a.make_no = d.ZuoFan COLLATE japanese_xjis_100_bin2")
            sb.AppendLine("  WHERE a.chk_no=@chk_no")
            sb.AppendLine("  ORDER BY b.CD")

            Dim dsInfo As New Data.DataSet
            Dim shn As New SqlHelperNew

            ' 使用参数化查询防止SQL注入
            Dim parameters(0) As System.Data.SqlClient.SqlParameter
            parameters(0) = New System.Data.SqlClient.SqlParameter("@chk_no", Data.SqlDbType.NVarChar, 50)
            parameters(0).Value = chk_no

            shn.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetPrintLinesCodeRelation", parameters)

            If dsInfo.Tables.Count > 0 AndAlso dsInfo.Tables(0).Rows.Count > 0 Then
                ' 将DataTable转换为JSON
                Dim dt As Data.DataTable = dsInfo.Tables(0)
                Dim serializer As New JavaScriptSerializer()
                Dim list As New List(Of Dictionary(Of String, Object))()

                For Each row As Data.DataRow In dt.Rows
                    Dim dict As New Dictionary(Of String, Object)()
                    For Each col As Data.DataColumn In dt.Columns
                        If row(col) Is DBNull.Value Then
                            dict(col.ColumnName) = ""
                        Else
                            dict(col.ColumnName) = row(col).ToString()
                        End If
                    Next
                    list.Add(dict)
                Next

                Dim jsonResult As String = serializer.Serialize(list)
                Return "{" & Chr(34) & "success" & Chr(34) & ":true," & Chr(34) & "data" & Chr(34) & ":" & jsonResult & "}"
            Else
                Return "{" & Chr(34) & "success" & Chr(34) & ":true," & Chr(34) & "data" & Chr(34) & ":[]}"
            End If

        Catch ex As Exception
            Dim errMsg As String = ex.Message.Replace(Chr(34), Chr(34) & Chr(34)).Replace(vbCrLf, "\n")
            Return "{" & Chr(34) & "success" & Chr(34) & ":false," & Chr(34) & "message" & Chr(34) & ":" & Chr(34) & errMsg & Chr(34) & "}"
        End Try
    End Function

    ''' <summary>
    ''' 保存打印记录到 m_print_history 表
    ''' </summary>
    ''' <param name="chk_no">检查番号</param>
    ''' <param name="line_CD">生产线</param>
    ''' <param name="CD">商品CD</param>
    ''' <param name="J_CD">日向商品CD</param>
    ''' <param name="printerName">打印机名称</param>
    ''' <param name="userName">用户名</param>
    ''' <returns>JSON格式的结果</returns>
    <WebMethod()>
    Public Function SavePrintHistory(ByVal chk_no As String, ByVal line_CD As String, ByVal CD As String, ByVal J_CD As String, ByVal printerName As String, ByVal userName As String) As String
        Try
            Dim shn As New SqlHelperNew

            ' 使用参数化查询防止SQL注入
            Dim parameters(5) As System.Data.SqlClient.SqlParameter
            parameters(0) = New System.Data.SqlClient.SqlParameter("@chk_no", Data.SqlDbType.VarChar, 20)
            parameters(0).Value = If(String.IsNullOrEmpty(chk_no), DBNull.Value, chk_no)

            parameters(1) = New System.Data.SqlClient.SqlParameter("@line_CD", Data.SqlDbType.VarChar, 20)
            parameters(1).Value = If(String.IsNullOrEmpty(line_CD), DBNull.Value, line_CD)

            parameters(2) = New System.Data.SqlClient.SqlParameter("@CD", Data.SqlDbType.VarChar, 20)
            parameters(2).Value = If(String.IsNullOrEmpty(CD), DBNull.Value, CD)

            parameters(3) = New System.Data.SqlClient.SqlParameter("@J_CD", Data.SqlDbType.VarChar, 20)
            parameters(3).Value = If(String.IsNullOrEmpty(J_CD), DBNull.Value, J_CD)

            parameters(4) = New System.Data.SqlClient.SqlParameter("@printerName", Data.SqlDbType.NVarChar, 100)
            parameters(4).Value = If(String.IsNullOrEmpty(printerName), DBNull.Value, printerName)

            parameters(5) = New System.Data.SqlClient.SqlParameter("@userName", Data.SqlDbType.NVarChar, 50)
            parameters(5).Value = If(String.IsNullOrEmpty(userName), DBNull.Value, userName)

            ' 先删除已存在的记录（key: chk_no + CD）
            Dim deleteSb As New StringBuilder
            deleteSb.AppendLine("DELETE FROM [dbo].[m_print_history]")
            deleteSb.AppendLine("WHERE [chk_no] = @chk_no AND [CD] = @CD")

            shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, deleteSb.ToString(), New System.Data.SqlClient.SqlParameter() {parameters(0), parameters(2)})

            ' 再插入新记录
            Dim insertSb As New StringBuilder
            insertSb.AppendLine("INSERT INTO [dbo].[m_print_history]")
            insertSb.AppendLine("([chk_no], [line_CD], [CD], [J_CD], [PrintTime], [PrinterName], [UserName])")
            insertSb.AppendLine("VALUES")
            insertSb.AppendLine("(@chk_no, @line_CD, @CD, @J_CD, GETDATE(), @printerName, @userName)")

            Dim result As Integer = shn.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, insertSb.ToString(), parameters)

            If result > 0 Then
                Return "{" & Chr(34) & "success" & Chr(34) & ":true," & Chr(34) & "message" & Chr(34) & ":" & Chr(34) & "打印记录保存成功" & Chr(34) & "}"
            Else
                Return "{" & Chr(34) & "success" & Chr(34) & ":false," & Chr(34) & "message" & Chr(34) & ":" & Chr(34) & "保存失败" & Chr(34) & "}"
            End If

        Catch ex As Exception
            Dim errMsg As String = ex.Message.Replace(Chr(34), Chr(34) & Chr(34)).Replace(vbCrLf, "\n")
            Return "{" & Chr(34) & "success" & Chr(34) & ":false," & Chr(34) & "message" & Chr(34) & ":" & Chr(34) & errMsg & Chr(34) & "}"
        End Try
    End Function

    ''' <summary>
    ''' 获取已打印的标签列表
    ''' </summary>
    ''' <param name="chk_no">检查番号</param>
    ''' <returns>JSON格式的已打印CD列表</returns>
    <WebMethod()>
    Public Function GetPrintedLabels(ByVal chk_no As String) As String
        Try
            Dim sb As New StringBuilder
            sb.AppendLine("SELECT DISTINCT CD FROM [dbo].[m_print_history]")
            sb.AppendLine("WHERE chk_no = @chk_no")

            Dim dsInfo As New Data.DataSet
            Dim shn As New SqlHelperNew

            Dim parameters(0) As System.Data.SqlClient.SqlParameter
            parameters(0) = New System.Data.SqlClient.SqlParameter("@chk_no", Data.SqlDbType.VarChar, 20)
            parameters(0).Value = If(String.IsNullOrEmpty(chk_no), DBNull.Value, chk_no)

            shn.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetPrintedLabels", parameters)

            If dsInfo.Tables.Count > 0 AndAlso dsInfo.Tables(0).Rows.Count > 0 Then
                Dim dt As Data.DataTable = dsInfo.Tables(0)
                Dim serializer As New JavaScriptSerializer()
                Dim cdList As New List(Of String)()

                For Each row As Data.DataRow In dt.Rows
                    If Not row("CD") Is DBNull.Value Then
                        cdList.Add(row("CD").ToString())
                    End If
                Next

                Dim jsonResult As String = serializer.Serialize(cdList)
                Return "{" & Chr(34) & "success" & Chr(34) & ":true," & Chr(34) & "data" & Chr(34) & ":" & jsonResult & "}"
            Else
                Return "{" & Chr(34) & "success" & Chr(34) & ":true," & Chr(34) & "data" & Chr(34) & ":[]}"
            End If

        Catch ex As Exception
            Dim errMsg As String = ex.Message.Replace(Chr(34), Chr(34) & Chr(34)).Replace(vbCrLf, "\n")
            Return "{" & Chr(34) & "success" & Chr(34) & ":false," & Chr(34) & "message" & Chr(34) & ":" & Chr(34) & errMsg & Chr(34) & "}"
        End Try
    End Function
End Class