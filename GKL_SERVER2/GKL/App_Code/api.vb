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
'Imports System.Web.Script.Services.ScriptService


' この Web サービスを、スクリプトから ASP.NET AJAX を使用して呼び出せるようにするには、次の行のコメントを解除します。
<System.Web.Script.Services.ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class api
    Inherits System.Web.Services.WebService


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

End Class