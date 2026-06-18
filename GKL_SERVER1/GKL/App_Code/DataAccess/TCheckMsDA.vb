



Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class TCheckMsDA
    Public SqlHelperNew As New SqlHelperNew
    ''' <summary>
    ''' 
    ''' 检查结果Infoを検索する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelTCheckMs(ByVal chkNo_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("chk_no")                                                    '检查No
        sb.AppendLine(", chk_method_id")   '检查项目ID
        sb.AppendLine(", chk_flg")                                                 '检查flg
        sb.AppendLine(", in_1")                                                    '入力値1
        sb.AppendLine(", in_2")                                                    '入力値2
        sb.AppendLine(", chk_result")                                              '检查结果
        sb.AppendLine(", mark")                                                    '备考
        sb.AppendLine(", kj_0")                                                    '基准
        sb.AppendLine(", kj_1")                                                    '工差1
        sb.AppendLine(", kj_2")                                                    '工差2
        sb.AppendLine(", kj_explain")                                              '基准説明
        sb.AppendLine(", ins_user")                                                '登録者
        sb.AppendLine(", ins_date")                                                '登録日

        sb.AppendLine("FROM t_check_ms")
        sb.AppendLine("WHERE 1=1")
        If chkNo_key <> "" Then
            sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "t_check_ms", paramList.ToArray)

        Return dsInfo.Tables("t_check_ms")

    End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを更新する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="chkNo">检查No</param>
    '''<param name="chkMethodId">检查项目ID</param>
    '''<param name="chkFlg">检查flg</param>
    '''<param name="in1">入力値1</param>
    '''<param name="in2">入力値2</param>
    '''<param name="chkResult">检查结果</param>
    '''<param name="mark">备考</param>
    '''<param name="kj0">基准</param>
    '''<param name="kj1">工差1</param>
    '''<param name="kj2">工差2</param>
    '''<param name="kjExplain">基准説明</param>
    '''<param name="insUser">登録者</param>
    '''<param name="insDate">登録日</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdTCheckMs(ByVal chkNo_key As String, _
               ByVal chkNo As String, _
               ByVal chkMethodId As String, _
               ByVal chkFlg As String, _
               ByVal in1 As String, _
               ByVal in2 As String, _
               ByVal chkResult As String, _
               ByVal mark As String, _
               ByVal kj0 As String, _
               ByVal kj1 As String, _
               ByVal kj2 As String, _
               ByVal kjExplain As String, _
               ByVal insUser As String, _
               ByVal insDate As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE t_check_ms")
        sb.AppendLine("SET")
        sb.AppendLine("chk_no=@chk_no")                                                '检查No
        sb.AppendLine(", chk_method_id=@chk_method_id")   '检查项目ID
        sb.AppendLine(", chk_flg=@chk_flg")                                            '检查flg
        sb.AppendLine(", in_1=@in_1")                                                  '入力値1
        sb.AppendLine(", in_2=@in_2")                                                  '入力値2
        sb.AppendLine(", chk_result=@chk_result")   '检查结果
        sb.AppendLine(", mark=@mark")                                                  '备考
        sb.AppendLine(", kj_0=@kj_0")                                                  '基准
        sb.AppendLine(", kj_1=@kj_1")                                                  '工差1
        sb.AppendLine(", kj_2=@kj_2")                                                  '工差2
        sb.AppendLine(", kj_explain=@kj_explain")   '基准説明
        sb.AppendLine(", ins_user=@ins_user")   '登録者
        sb.AppendLine(", ins_date=@ins_date")   '登録日

        sb.AppendLine("FROM t_check_ms")
        sb.AppendLine("WHERE 1=1")
        If chkNo_key <> "" Then
            sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))

        paramList.Add(SqlHelperNew.MakeParam("@chk_no", SqlDbType.VarChar, 20, chkNo))
        paramList.Add(SqlHelperNew.MakeParam("@chk_method_id", SqlDbType.VarChar, 10, chkMethodId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_flg", SqlDbType.VarChar, 1, chkFlg))
        paramList.Add(SqlHelperNew.MakeParam("@in_1", SqlDbType.VarChar, 20, in1))
        paramList.Add(SqlHelperNew.MakeParam("@in_2", SqlDbType.VarChar, 20, in2))
        paramList.Add(SqlHelperNew.MakeParam("@chk_result", SqlDbType.VarChar, 20, chkResult))
        paramList.Add(SqlHelperNew.MakeParam("@mark", SqlDbType.nvarchar, 200, mark))
        paramList.Add(SqlHelperNew.MakeParam("@kj_0", SqlDbType.nvarchar, 100, kj0))
        paramList.Add(SqlHelperNew.MakeParam("@kj_1", SqlDbType.VarChar, 20, kj1))
        paramList.Add(SqlHelperNew.MakeParam("@kj_2", SqlDbType.VarChar, 20, kj2))
        paramList.Add(SqlHelperNew.MakeParam("@kj_explain", SqlDbType.nvarchar, 200, kjExplain))
        paramList.Add(SqlHelperNew.MakeParam("@ins_user", SqlDbType.VarChar, 20, insUser))
        paramList.Add(SqlHelperNew.MakeParam("@ins_date", SqlDbType.DateTime, 3, IIf(insDate = "", DBNull.Value, insDate)))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを登録する
    ''' </summary>
    '''<param name="chkNo">检查No</param>
    '''<param name="chkMethodId">检查项目ID</param>
    '''<param name="chkFlg">检查flg</param>
    '''<param name="in1">入力値1</param>
    '''<param name="in2">入力値2</param>
    '''<param name="chkResult">检查结果</param>
    '''<param name="mark">备考</param>
    '''<param name="kj0">基准</param>
    '''<param name="kj1">工差1</param>
    '''<param name="kj2">工差2</param>
    '''<param name="kjExplain">基准説明</param>
    '''<param name="insUser">登録者</param>
    '''<param name="insDate">登録日</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsTCheckMs(ByVal chkNo As String, _
               ByVal chkMethodId As String, _
               ByVal chkFlg As String, _
               ByVal in1 As String, _
               ByVal in2 As String, _
               ByVal chkResult As String, _
               ByVal mark As String, _
               ByVal kj0 As String, _
               ByVal kj1 As String, _
               ByVal kj2 As String, _
               ByVal kjExplain As String, _
               ByVal insUser As String, _
               ByVal insDate As String) As Object

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO  t_check_ms")
        sb.AppendLine("(")
        sb.AppendLine("chk_no")                                                    '检查No
        sb.AppendLine(", chk_method_id")   '检查项目ID
        sb.AppendLine(", chk_flg")                                                 '检查flg
        sb.AppendLine(", in_1")                                                    '入力値1
        sb.AppendLine(", in_2")                                                    '入力値2
        sb.AppendLine(", chk_result")                                              '检查结果
        sb.AppendLine(", mark")                                                    '备考
        sb.AppendLine(", kj_0")                                                    '基准
        sb.AppendLine(", kj_1")                                                    '工差1
        sb.AppendLine(", kj_2")                                                    '工差2
        sb.AppendLine(", kj_explain")                                              '基准説明
        sb.AppendLine(", ins_user")                                                '登録者
        sb.AppendLine(", ins_date")                                                '登録日

        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@chk_no")                                                       '检查No
        sb.AppendLine(", @chk_method_id")                                              '检查项目ID
        sb.AppendLine(", @chk_flg")                                                    '检查flg
        sb.AppendLine(", @in_1")                                                       '入力値1
        sb.AppendLine(", @in_2")                                                       '入力値2
        sb.AppendLine(", @chk_result")                                                 '检查结果
        sb.AppendLine(", @mark")                                                       '备考
        sb.AppendLine(", @kj_0")                                                       '基准
        sb.AppendLine(", @kj_1")                                                       '工差1
        sb.AppendLine(", @kj_2")                                                       '工差2
        sb.AppendLine(", @kj_explain")                                                 '基准説明
        sb.AppendLine(", @ins_user")                                                   '登録者
        sb.AppendLine(", @ins_date")                                                   '登録日

        sb.AppendLine(")")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no", SqlDbType.VarChar, 20, chkNo))
        paramList.Add(SqlHelperNew.MakeParam("@chk_method_id", SqlDbType.VarChar, 10, chkMethodId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_flg", SqlDbType.VarChar, 1, chkFlg))
        paramList.Add(SqlHelperNew.MakeParam("@in_1", SqlDbType.VarChar, 20, in1))
        paramList.Add(SqlHelperNew.MakeParam("@in_2", SqlDbType.VarChar, 20, in2))
        paramList.Add(SqlHelperNew.MakeParam("@chk_result", SqlDbType.VarChar, 20, chkResult))
        paramList.Add(SqlHelperNew.MakeParam("@mark", SqlDbType.NVarChar, 200, mark))
        paramList.Add(SqlHelperNew.MakeParam("@kj_0", SqlDbType.NVarChar, 100, kj0))
        paramList.Add(SqlHelperNew.MakeParam("@kj_1", SqlDbType.VarChar, 20, kj1))
        paramList.Add(SqlHelperNew.MakeParam("@kj_2", SqlDbType.VarChar, 20, kj2))
        paramList.Add(SqlHelperNew.MakeParam("@kj_explain", SqlDbType.NVarChar, 200, kjExplain))
        paramList.Add(SqlHelperNew.MakeParam("@ins_user", SqlDbType.VarChar, 20, insUser))
        paramList.Add(SqlHelperNew.MakeParam("@ins_date", SqlDbType.DateTime, 3, IIf(insDate = "", DBNull.Value, insDate)))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 检查结果Infoを削除する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelTCheckMs(ByVal chkNo_key As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM t_check_ms")
        sb.AppendLine("WHERE 1=1")
        If chkNo_key <> "" Then
            sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' GetIntValue
    ''' </summary>
    ''' <param name="v"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetIntValue(ByVal v As Object) As Object

        If v Is DBNull.Value Or v.ToString = "" Then
            Return DBNull.Value

        Else
            Return Convert.ToInt32(v)
        End If

    End Function





    ''' <summary>
    ''' 
    ''' 检查结果Infoを検索する
    ''' </summary>
    '''<param name="chkNo_key">检查No</param>
    ''' <returns>检查结果Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelTCheckMs(ByVal chkNo_key As String, ByVal line_id As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder

        With sb


            .AppendLine("declare @make_no varchar(80)")
            .AppendLine("select top 1 @make_no = make_no from t_check_result where chk_no = '240829_9015513921_1'")
            .AppendLine(" ")
            .AppendLine(" ")
            .AppendLine(" ")
            .AppendLine(" ")


            'SQL文
            .AppendLine("SELECT t_check_ms.chk_no, ")
            .AppendLine("       t_check_ms.chk_method_id, ")
            .AppendLine("       t_check_ms.chk_flg, ")
            .AppendLine("       t_check_ms.in_1, ")
            .AppendLine("       t_check_ms.in_2, ")
            .AppendLine("       t_check_ms.chk_result, ")
            .AppendLine("       t_check_ms.mark, ")
            .AppendLine("       t_check_ms.kj_0, ")
            .AppendLine("       t_check_ms.kj_1, ")
            .AppendLine("       t_check_ms.kj_2, ")
            .AppendLine("       t_check_ms.kj_explain, ")
            .AppendLine("       t_check_ms.ins_user, ")
            .AppendLine("       t_check_ms.ins_date, ")


            '.AppendLine("       m_temp.project_name, ")
            '.AppendLine("       m_temp.pic_id, ")
            '.AppendLine("       m_temp.pic_name, ")
            '.AppendLine("       m_temp.chk_km_name, ")
            '.AppendLine("       m_temp.chk_name, ")
            '.AppendLine("       m_temp.tool_id, ")
            '.AppendLine("       m_temp.pic_sign       AS pic_sign, ")
            '.AppendLine("       m_temp.kj_0       AS kj_0_Expr, ")
            '.AppendLine("       m_temp.kj_1       AS kj_1_Expr, ")
            '.AppendLine("       m_temp.kj_2       AS kj_2_Expr, ")
            '.AppendLine("       m_temp.kj_explain AS kj_explain_Expr, ")
            '.AppendLine("       m_temp.line_id, ")
            '.AppendLine("       m_temp.temp_id ,")
            '.AppendLine("	    m_temp.chk_id ,")
            '.AppendLine("		m_check_method.chk_method, ")
            '.AppendLine("		m_check_method.chk_formula, ")
            '.AppendLine("		ISNULL(m_tools.tool_name,m_check_method.verify_method_explain ) verify_method_explain")



            .AppendLine("       t_check_ms.project_name, ")
            .AppendLine("       t_check_ms.pic_id, ")
            .AppendLine("       t_check_ms.pic_name, ")
            .AppendLine("       t_check_ms.chk_km_name, ")
            .AppendLine("       t_check_ms.chk_name, ")
            .AppendLine("       t_check_ms.tool_id, ")
            .AppendLine("       t_check_ms.pic_sign       AS pic_sign, ")
            .AppendLine("       t_check_ms.kj_0       AS kj_0_Expr, ")
            .AppendLine("       t_check_ms.kj_1       AS kj_1_Expr, ")
            .AppendLine("       t_check_ms.kj_2       AS kj_2_Expr, ")
            .AppendLine("       t_check_ms.kj_explain AS kj_explain_Expr, ")
            .AppendLine("       t_check_result.line_id, ")
            .AppendLine("       t_check_result.temp_id ,")
            .AppendLine("	    t_check_ms.chk_id ,")
            .AppendLine("		t_check_ms.chk_method, ")
            .AppendLine("		t_check_ms.chk_formula, ")
            .AppendLine("		t_check_ms.verify_method_explain,")

            .AppendLine("		TCM_BianPlan.DW, ")
            .AppendLine("		TCM_BianPlan.DH ")

            .AppendLine("")
            .AppendLine("FROM   t_check_ms ")
            .AppendLine("INNER JOIN t_check_result ")
            .AppendLine("   ON t_check_ms.line_id = t_check_result.line_id")
            .AppendLine("   AND t_check_ms.chk_no = t_check_result.chk_no")

            .AppendLine("LEFT JOIN   (select * from TCM_BianPlan where ZuoFan in(@make_no)) TCM_BianPlan ")
            .AppendLine("   ON t_check_result.make_no = TCM_BianPlan.ZuoFan COLLATE japanese_xjis_100_bin2")

            If (Right(line_id, 1) = "A") Then
                sb.AppendLine("AND 'SRM1'+TCM_BianPlan.[WorkLineCode]+'A'='" & line_id & "'")   '计划No
            ElseIf (Right(line_id, 1) = "B") Then
                sb.AppendLine("AND 'SRM1'+TCM_BianPlan.[WorkLineCode]+'B'='" & line_id & "'")   '计划No
            End If


            '.AppendLine("       LEFT JOIN m_temp ")
            '.AppendLine("               ON t_check_ms.chk_method_id = m_temp.chk_method_id ")
            '.AppendLine("               AND t_check_ms.line_id = m_temp.line_id ")
            '.AppendLine("               AND t_check_result.temp_id = m_temp.temp_id ")

            '.AppendLine("       LEFT JOIN m_check_method ")
            '.AppendLine("               ON m_temp.chk_id = m_check_method.chk_id ")
            '.AppendLine("       LEFT JOIN m_tools ")
            '.AppendLine("               ON m_temp.tool_id = m_tools.tool_id ")
            '.AppendLine("                  AND m_temp.line_id = m_tools.line_id ")
            .AppendLine("WHERE")
            .AppendLine("	 t_check_ms.chk_no = '" & chkNo_key & "'")
            .AppendLine("	 AND  t_check_ms.line_id = '" & line_id & "'")
            .AppendLine("ORDER BY t_check_ms.chk_method_id")
        End With

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "t_check_ms")

        Return dsInfo.Tables("t_check_ms")

    End Function







    Public Function UpdTCheckMs(ByVal chkNo_key As String, _
               ByVal in1 As String, _
               ByVal chkResult As String, _
               ByVal mark As String, _
               ByVal kj0 As String, _
               ByVal kj1 As String, _
               ByVal kj2 As String, _
               ByVal insUser As String, _
               ByVal line_id As String, _
               ByVal chk_method_id As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE t_check_ms")
        sb.AppendLine("SET")
        sb.AppendLine(" in_1=@in_1")                                                  '入力値1
        sb.AppendLine(", chk_result=@chk_result")   '检查结果
        sb.AppendLine(", mark=@mark")                                                  '备考
        sb.AppendLine(", kj_0=@kj_0")                                                  '基准
        sb.AppendLine(", kj_1=@kj_1")                                                  '工差1
        sb.AppendLine(", kj_2=@kj_2")                                                  '工差2
        sb.AppendLine(", ins_user=@ins_user")   '登録者
        sb.AppendLine(", ins_date=getdate()")   '登録日
        sb.AppendLine("FROM t_check_ms")
        sb.AppendLine("WHERE chk_no=@chk_no_key")   '检查No
        sb.AppendLine("	 AND  chk_method_id = '" & chk_method_id & "'")
        sb.AppendLine("	 AND  line_id = '" & line_id & "'")


        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@in_1", SqlDbType.VarChar, 20, in1))
        paramList.Add(SqlHelperNew.MakeParam("@chk_result", SqlDbType.VarChar, 20, chkResult))
        paramList.Add(SqlHelperNew.MakeParam("@mark", SqlDbType.NVarChar, 200, mark))
        paramList.Add(SqlHelperNew.MakeParam("@kj_0", SqlDbType.NVarChar, 100, kj0))
        paramList.Add(SqlHelperNew.MakeParam("@kj_1", SqlDbType.VarChar, 20, kj1))
        paramList.Add(SqlHelperNew.MakeParam("@kj_2", SqlDbType.VarChar, 20, kj2))
        paramList.Add(SqlHelperNew.MakeParam("@ins_user", SqlDbType.VarChar, 20, insUser))



        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function




    Public Function UpdTCheckResultMS(ByVal chkNo_key As String, _
               ByVal line_id As String, ByVal insUser As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        With sb
            .AppendLine("DECLARE @chk_no VARCHAR(20) ")
            .AppendLine("DECLARE @line_id VARCHAR(20) ")
            .AppendLine("")
            .AppendLine("SET @chk_no = '" & chkNo_key & "' ")
            .AppendLine("SET @line_id = '" & line_id & "' ")
            .AppendLine("")


            If (System.Configuration.ConfigurationManager.AppSettings.Get("camera").ToString().IndexOf(line_id.ToString()) >= 0) Then

                Dim dsInfo As New Data.DataSet
                SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, "select * from m_snap where chk_no='" & chkNo_key & "'", dsInfo, "imgexist")

                If dsInfo.Tables(0).Rows.Count > 0 Then

                    'If System.IO.Directory.Exists(img_save_path) AndAlso System.IO.Directory.GetFiles(img_save_path, "*.jpg").Length > 0 Then
                    .AppendLine("IF EXISTS(SELECT * ")
                    .AppendLine("          FROM   t_check_ms ")
                    .AppendLine("          WHERE  chk_no = @chk_no ")
                    .AppendLine("                 AND line_id = @line_id ")
                    .AppendLine("                 AND (ISNULL(chk_result,'') = 'NG' OR ISNULL(chk_result,'') = ''))")
                    .AppendLine("  BEGIN ")
                    .AppendLine("      UPDATE [t_check_result] ")
                    .AppendLine("      SET    [chk_result] = '9',status = '2',chk_end_date=getdate(), chk_user = '" & insUser & "' ")
                    .AppendLine("      WHERE  chk_no = @chk_no ")
                    .AppendLine("             AND line_id = @line_id ")
                    .AppendLine("  END ")
                    .AppendLine("ELSE ")
                    .AppendLine("  BEGIN ")
                    .AppendLine("      UPDATE [t_check_result] ")
                    .AppendLine("      SET    [chk_result] = '1',status = '2',chk_end_date=getdate(), chk_user = '" & insUser & "' ")
                    .AppendLine("      WHERE  chk_no = @chk_no ")
                    .AppendLine("             AND line_id = @line_id ")
                    .AppendLine("  END ")

                Else
                    .AppendLine("      UPDATE [t_check_result] ")
                    .AppendLine("      SET    [chk_result] = '9',status = '2',chk_end_date=getdate(), chk_user = '" & insUser & "' ")
                    .AppendLine("      WHERE  chk_no = @chk_no ")
                    .AppendLine("             AND line_id = @line_id ")

                End If

            Else
                .AppendLine("IF EXISTS(SELECT * ")
                .AppendLine("          FROM   t_check_ms ")
                .AppendLine("          WHERE  chk_no = @chk_no ")
                .AppendLine("                 AND line_id = @line_id ")
                .AppendLine("                 AND (ISNULL(chk_result,'') = 'NG' OR ISNULL(chk_result,'') = ''))")
                .AppendLine("  BEGIN ")
                .AppendLine("      UPDATE [t_check_result] ")
                .AppendLine("      SET    [chk_result] = '9',status = '2',chk_end_date=getdate(), chk_user = '" & insUser & "' ")
                .AppendLine("      WHERE  chk_no = @chk_no ")
                .AppendLine("             AND line_id = @line_id ")
                .AppendLine("  END ")
                .AppendLine("ELSE ")
                .AppendLine("  BEGIN ")
                .AppendLine("      UPDATE [t_check_result] ")
                .AppendLine("      SET    [chk_result] = '1',status = '2',chk_end_date=getdate(), chk_user = '" & insUser & "' ")
                .AppendLine("      WHERE  chk_no = @chk_no ")
                .AppendLine("             AND line_id = @line_id ")
                .AppendLine("  END ")



            End If







        End With





        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString())

        Return True

    End Function




    'Public Function UpdTCheckResultMSWanliao(ByVal chkNo_key As String, _
    '           ByVal line_id As String) As Boolean

    '    'SQLコメント
    '    '--**テーブル：检查结果 : t_check_ms
    '    Dim sb As New StringBuilder
    '    'SQL文
    '    With sb
    '        .AppendLine("DECLARE @chk_no VARCHAR(20) ")
    '        .AppendLine("DECLARE @line_id VARCHAR(20) ")
    '        .AppendLine("")
    '        .AppendLine("SET @chk_no = '" & chkNo_key & "' ")
    '        .AppendLine("SET @line_id = '" & line_id & "' ")

    '        .AppendLine("      UPDATE [t_check_result] ")
    '        .AppendLine("      SET    status = '2',chk_end_date=getdate() ")
    '        .AppendLine("      WHERE  chk_no = @chk_no ")
    '        .AppendLine("             AND line_id = @line_id ")

    '    End With





    '    SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString())

    '    Return True

    'End Function



    Public Function InsImgInfo(ByVal chkNo As String, _
               ByVal picPath As String) As Object

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("declare @idx int")
        sb.AppendLine("select @idx = isnull(max(idx),0)+1 from [m_snap]")
        sb.AppendLine("INSERT INTO m_snap")
        sb.AppendLine("SELECT ")
        sb.AppendLine("'" & chkNo & "'")                                                    '检查No
        sb.AppendLine(", @idx")
        sb.AppendLine(",'" & picPath & "'")

        'PARAM
        Dim paramList As New List(Of SqlParameter)

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function



    Public Function SelImgInfo(ByVal chkNo_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM m_snap")
        sb.AppendLine("WHERE 1=1")

        sb.AppendLine("AND chk_no='" & chkNo_key & "'")   '检查No


        'PARAM
        Dim paramList As New List(Of SqlParameter)

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_snap", paramList.ToArray)

        Return dsInfo.Tables("m_snap")

    End Function


    Public Function DelImgInfo(ByVal picPath As String, ByVal chkNo As String) As Object

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM m_snap")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND chk_no='" & chkNo & "'")
        sb.AppendLine("AND picPath='" & picPath & "'")


        'PARAM
        Dim paramList As New List(Of SqlParameter)

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    Public Function GetZuofanAndLine(ByVal chkNo_key As String) As DataTable

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT top 1 line_id,make_no,ins_user ")
        sb.AppendLine("FROM t_check_result")
        sb.AppendLine("WHERE")
        sb.AppendLine("chk_no='" & chkNo_key & "'")   '检查No

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetZuofanAndLine")

        Return dsInfo.Tables("GetZuofanAndLine")

    End Function

    Public Function GetResultByChkNo(ByVal chkNo_key As String, ByVal line_id As String) As DataTable

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT top 1 chk_result ")
        sb.AppendLine("FROM t_check_result")
        sb.AppendLine("WHERE")
        sb.AppendLine("chk_no='" & chkNo_key & "'")   '检查No
        sb.AppendLine("and line_id='" & line_id & "'")   '检查No
        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetResultByChkNo")

        Return dsInfo.Tables("GetResultByChkNo")

    End Function


    Public Function GetResultByChkNo(ByVal chkNo_key As String) As DataTable

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT top 1 chk_result ")
        sb.AppendLine("FROM t_check_result")
        sb.AppendLine("WHERE")
        sb.AppendLine("chk_no='" & chkNo_key & "'")   '检查No

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetResultByChkNo")

        Return dsInfo.Tables("GetResultByChkNo")

    End Function




    Public Function UpdTCheckResultOK(ByVal chkNo_key As String,
               ByVal line_id As String, ByVal insUser As String) As Boolean

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        With sb
            .AppendLine("DECLARE @chk_no VARCHAR(20) ")
            .AppendLine("DECLARE @line_id VARCHAR(20) ")
            .AppendLine("")
            .AppendLine("SET @chk_no = '" & chkNo_key & "' ")
            .AppendLine("SET @line_id = '" & line_id & "' ")
            .AppendLine("      UPDATE [t_check_result] ")
            .AppendLine("      SET    [chk_result] = '1',status = '2',chk_end_date=getdate(), chk_user = '" & insUser & "' ")
            .AppendLine("      WHERE  chk_no = @chk_no ")
            .AppendLine("             AND line_id = @line_id ")

        End With

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString())

        Return True

    End Function

    Public Function GetDbDate() As String

        'SQLコメント
        '--**テーブル：检查结果 : t_check_ms
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT getdate() ")
        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "GetZuofanAndLine")

        Return CDate(dsInfo.Tables("GetZuofanAndLine").Rows(0).Item(0)).ToString("yyyy-MM-dd HH:mm:sss.fff")

    End Function

End Class
