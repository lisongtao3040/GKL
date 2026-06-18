



Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class TCheckPlanDA
    Public SqlHelperNew As New SqlHelperNew
    ''' <summary>
    ''' 
    ''' 检查计划Infoを検索する
    ''' </summary>
    '''<param name="planNo_key">计划No</param>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="makeNo_key">作番</param>
    '''<param name="code_key">コード</param>
    '''<param name="lineId_key">生产线</param>
    ''' <returns>检查计划Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelTCheckPlan(ByVal planNo_key As String,
           ByVal chkNo_key As String,
           ByVal makeNo_key As String,
           ByVal code_key As String,
           ByVal lineId_key As String,
           ByVal tbxCheckDate_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("plan_no")                                                   '计划No
        sb.AppendLine(", chk_no")                                                  '检查No
        sb.AppendLine(", make_no")                                                 '作番
        sb.AppendLine(", code")                                                    'コード
        sb.AppendLine(", line_id")                                                 '生产线
        sb.AppendLine(", suu")                                                     '数量
        sb.AppendLine(", yotei_chk_date")   '预订检查日
        sb.AppendLine(", status")                                                  '状态
        sb.AppendLine(", xiangxian")                                                '登録者
        sb.AppendLine(", mark")                                                '登録日

        sb.AppendLine("FROM t_check_plan")
        sb.AppendLine("WHERE 1=1")
        If planNo_key <> "" Then
            sb.AppendLine("AND plan_no=@plan_no_key")   '计划No
        End If
        If chkNo_key <> "" Then
            sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        End If
        If makeNo_key <> "" Then
            sb.AppendLine("AND make_no=@make_no_key")   '作番
        End If
        If code_key <> "" Then
            sb.AppendLine("AND code=@code_key")   'コード
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If

        If tbxCheckDate_key <> "" Then
            sb.AppendLine("AND yotei_chk_date='" & tbxCheckDate_key & "'")   '生产线
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@plan_no_key", SqlDbType.VarChar, 20, planNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@make_no_key", SqlDbType.VarChar, 20, makeNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@code_key", SqlDbType.VarChar, 20, code_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "t_check_plan", paramList.ToArray)

        Return dsInfo.Tables("t_check_plan")

    End Function


    Public Function SelTPlanFromSap(ByVal YM As String,
          ByVal user As String, ByVal lineid As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("declare @date as datetime")
        sb.AppendLine("set @date = getdate()")
        sb.AppendLine("declare @user as varchar(30)")
        sb.AppendLine("set @user='" & user & "'")
        sb.AppendLine("")
        sb.AppendLine("SELECT ")
        sb.AppendLine("")
        sb.AppendLine("left([PlanDate],6)  as plan_no,")
        sb.AppendLine("right([PlanDate],6)+'_'+ [ZuoFan] as chk_no,")
        sb.AppendLine("[ZuoFan] as make_no,")
        sb.AppendLine("ProductCode as code,")

        If (Right(lineid, 1) = "A") Then
            sb.AppendLine("'SRM1'+WorkLineCode+'A' as line_id, --4位了 怎?弄")
        ElseIf (Right(lineid, 1) = "B") Then
            sb.AppendLine("'SRM1'+WorkLineCode+'B' as line_id, --4位了 怎?弄")
        End If

        sb.AppendLine("amount as suu,")
        sb.AppendLine("left([PlanDate],4) + '/' +substring([PlanDate],5,2)+'/'+ right([PlanDate],2) as yotei_chk_date,")
        sb.AppendLine("'0' as [status],")
        sb.AppendLine("DestinationCode as xiangxian,")
        sb.AppendLine("'' as mark,")
        sb.AppendLine("@user as ins_user,")
        sb.AppendLine("@date as ins_date,")
        sb.AppendLine("@user as upd_user,")
        sb.AppendLine("@date as upd_date")
        'sb.AppendLine("FROM [TCM_BianPlan]")
        sb.AppendLine("FROM [T_BianPlan]")
        sb.AppendLine("WHERE PlanFlag = 'ZP02'")

        sb.AppendLine("AND left([PlanDate],6) =@plan_no_key")   '计划No
        If lineid <> "" Then
            If (Right(lineid, 1) = "A") Then
                sb.AppendLine("AND 'SRM1'+WorkLineCode+'A'='" & lineid & "'")   '计划No
            ElseIf (Right(lineid, 1) = "B") Then
                sb.AppendLine("AND 'SRM1'+WorkLineCode+'B'='" & lineid & "'")   '计划No
            End If


        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@plan_no_key", SqlDbType.VarChar, 20, YM))


        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.ConnectionSAP, CommandType.Text, sb.ToString(), dsInfo, "t_check_plan", paramList.ToArray)

        Return dsInfo.Tables("t_check_plan")

    End Function

    ''' <summary>
    ''' 
    ''' 检查计划Infoを更新する
    ''' </summary>
    '''<param name="planNo_key">计划No</param>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="makeNo_key">作番</param>
    '''<param name="code_key">コード</param>
    '''<param name="lineId_key">生产线</param>
    '''<param name="planNo">计划No</param>
    '''<param name="chkNo">检查No</param>
    '''<param name="makeNo">作番</param>
    '''<param name="code">コード</param>
    '''<param name="lineId">生产线</param>
    '''<param name="suu">数量</param>
    '''<param name="yoteiChkDate">预订检查日</param>
    '''<param name="status">状态</param>
    '''<param name="insUser">登録者</param>
    '''<param name="insDate">登録日</param>
    ''' <returns>检查计划Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdTCheckPlan(ByVal planNo_key As String,
               ByVal chkNo_key As String,
               ByVal makeNo_key As String,
               ByVal code_key As String,
               ByVal lineId_key As String,
               ByVal planNo As String,
               ByVal chkNo As String,
               ByVal makeNo As String,
               ByVal code As String,
               ByVal lineId As String,
               ByVal suu As String,
               ByVal yoteiChkDate As String,
               ByVal status As String,
               ByVal insUser As String,
               ByVal insDate As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE t_check_plan")
        sb.AppendLine("SET")
        sb.AppendLine("plan_no=@plan_no")                                              '计划No
        sb.AppendLine(", chk_no=@chk_no")                                              '检查No
        sb.AppendLine(", make_no=@make_no")                                            '作番
        sb.AppendLine(", code=@code")                                                  'コード
        sb.AppendLine(", line_id=@line_id")                                            '生产线
        sb.AppendLine(", suu=@suu")                                                    '数量
        sb.AppendLine(", yotei_chk_date=@yotei_chk_date")   '预订检查日
        sb.AppendLine(", status=@status")                                              '状态
        sb.AppendLine(", xiangxian=@xiangxian")   '登録者
        sb.AppendLine(", mark=@mark")   '登録日
        sb.AppendLine(", upd_user='" & menu_user & "'")
        sb.AppendLine(", upd_date=getdate()")

        sb.AppendLine("FROM t_check_plan")
        sb.AppendLine("WHERE 1=1")
        If planNo_key <> "" Then
            sb.AppendLine("AND plan_no=@plan_no_key")   '计划No
        End If
        If chkNo_key <> "" Then
            sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        End If
        If makeNo_key <> "" Then
            sb.AppendLine("AND make_no=@make_no_key")   '作番
        End If
        If code_key <> "" Then
            sb.AppendLine("AND code=@code_key")   'コード
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@plan_no_key", SqlDbType.VarChar, 20, planNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@make_no_key", SqlDbType.VarChar, 20, makeNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@code_key", SqlDbType.VarChar, 20, code_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        paramList.Add(SqlHelperNew.MakeParam("@plan_no", SqlDbType.VarChar, 20, planNo))
        paramList.Add(SqlHelperNew.MakeParam("@chk_no", SqlDbType.VarChar, 20, chkNo))
        paramList.Add(SqlHelperNew.MakeParam("@make_no", SqlDbType.VarChar, 20, makeNo))
        paramList.Add(SqlHelperNew.MakeParam("@code", SqlDbType.VarChar, 20, code))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@suu", SqlDbType.VarChar, 10, suu))
        paramList.Add(SqlHelperNew.MakeParam("@yotei_chk_date", SqlDbType.VarChar, 20, IIf(yoteiChkDate = "", DBNull.Value, CDate(yoteiChkDate).ToString("yyyy/MM/dd"))))
        paramList.Add(SqlHelperNew.MakeParam("@status", SqlDbType.VarChar, 1, status))
        paramList.Add(SqlHelperNew.MakeParam("@xiangxian", SqlDbType.NVarChar, 20, insUser))
        paramList.Add(SqlHelperNew.MakeParam("@mark", SqlDbType.NVarChar, 500, IIf(insDate = "", DBNull.Value, insDate)))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 检查计划Infoを登録する
    ''' </summary>
    '''<param name="planNo">计划No</param>
    '''<param name="chkNo">检查No</param>
    '''<param name="makeNo">作番</param>
    '''<param name="code">コード</param>
    '''<param name="lineId">生产线</param>
    '''<param name="suu">数量</param>
    '''<param name="yoteiChkDate">预订检查日</param>
    '''<param name="status">状态</param>
    '''<param name="insUser">登録者</param>
    '''<param name="insDate">登録日</param>
    ''' <returns>检查计划Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsTCheckPlan(ByVal planNo As String,
               ByVal chkNo As String,
               ByVal makeNo As String,
               ByVal code As String,
               ByVal lineId As String,
               ByVal suu As String,
               ByVal yoteiChkDate As String,
               ByVal status As String,
               ByVal insUser As String,
               ByVal insDate As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO  t_check_plan")
        sb.AppendLine("(")
        sb.AppendLine("plan_no")                                                   '计划No
        sb.AppendLine(", chk_no")                                                  '检查No
        sb.AppendLine(", make_no")                                                 '作番
        sb.AppendLine(", code")                                                    'コード
        sb.AppendLine(", line_id")                                                 '生产线
        sb.AppendLine(", suu")                                                     '数量
        sb.AppendLine(", yotei_chk_date")   '预订检查日
        sb.AppendLine(", status")                                                  '状态
        sb.AppendLine(", xiangxian")                                                '登録者
        sb.AppendLine(", mark")                                                '登録日
        sb.AppendLine(", ins_user")
        sb.AppendLine(", ins_date")

        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@plan_no")                                                      '计划No
        sb.AppendLine(", @chk_no")                                                     '检查No
        sb.AppendLine(", @make_no")                                                    '作番
        sb.AppendLine(", @code")                                                       'コード
        sb.AppendLine(", @line_id")                                                    '生产线
        sb.AppendLine(", @suu")                                                        '数量
        sb.AppendLine(", @yotei_chk_date")                                             '预订检查日
        sb.AppendLine(", @status")                                                     '状态
        sb.AppendLine(", @xiangxian")                                                   '登録者
        sb.AppendLine(", @mark")                                                   '登録日
        sb.AppendLine(", '" & menu_user & "'")
        sb.AppendLine(", getdate()")

        sb.AppendLine(")")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@plan_no", SqlDbType.VarChar, 20, planNo))
        paramList.Add(SqlHelperNew.MakeParam("@chk_no", SqlDbType.VarChar, 20, chkNo))
        paramList.Add(SqlHelperNew.MakeParam("@make_no", SqlDbType.VarChar, 20, makeNo))
        paramList.Add(SqlHelperNew.MakeParam("@code", SqlDbType.VarChar, 20, code))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@suu", SqlDbType.VarChar, 10, suu))
        paramList.Add(SqlHelperNew.MakeParam("@yotei_chk_date", SqlDbType.VarChar, 20, IIf(yoteiChkDate = "", DBNull.Value, CDate(yoteiChkDate).ToString("yyyy/MM/dd"))))
        paramList.Add(SqlHelperNew.MakeParam("@status", SqlDbType.VarChar, 1, status))
        paramList.Add(SqlHelperNew.MakeParam("@xiangxian", SqlDbType.NVarChar, 20, insUser))
        paramList.Add(SqlHelperNew.MakeParam("@mark", SqlDbType.NVarChar, 500, IIf(insDate = "", DBNull.Value, insDate)))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 检查计划Infoを削除する
    ''' </summary>
    '''<param name="planNo_key">计划No</param>
    '''<param name="chkNo_key">检查No</param>
    '''<param name="makeNo_key">作番</param>
    '''<param name="code_key">コード</param>
    '''<param name="lineId_key">生产线</param>
    ''' <returns>检查计划Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelTCheckPlan(ByVal planNo_key As String,
               ByVal chkNo_key As String,
               ByVal makeNo_key As String,
               ByVal code_key As String,
               ByVal lineId_key As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM t_check_plan")
        sb.AppendLine("WHERE 1=1")
        If planNo_key <> "" Then
            sb.AppendLine("AND plan_no=@plan_no_key")   '计划No
        End If
        If chkNo_key <> "" Then
            sb.AppendLine("AND chk_no=@chk_no_key")   '检查No
        End If
        If makeNo_key <> "" Then
            sb.AppendLine("AND make_no=@make_no_key")   '作番
        End If
        If code_key <> "" Then
            sb.AppendLine("AND code=@code_key")   'コード
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@plan_no_key", SqlDbType.VarChar, 20, planNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@chk_no_key", SqlDbType.VarChar, 20, chkNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@make_no_key", SqlDbType.VarChar, 20, makeNo_key))
        paramList.Add(SqlHelperNew.MakeParam("@code_key", SqlDbType.VarChar, 20, code_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))


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

End Class
