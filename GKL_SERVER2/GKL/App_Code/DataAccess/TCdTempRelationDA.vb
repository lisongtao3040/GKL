



Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class TCdTempRelationDA
    Public SqlHelperNew As New SqlHelperNew
    ''' <summary>
    ''' 
    ''' Infoを検索する
    ''' </summary>
    '''<param name="lineId_key">line_id</param>
    '''<param name="code_key">code</param>
    '''<param name="tempId_key">temp_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelTCdTempRelation(ByVal lineId_key As String, _
           ByVal code_key As String, _
           ByVal tempId_key As String) As Data.DataTable



        'SQLコメント
        '--**テーブル： : t_cd_temp_relation
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        'sb.AppendLine("line_id")                                                   'line_id
        'sb.AppendLine(", code")                                                    'code
        'sb.AppendLine(", temp_id")                                                 'temp_id
        'sb.AppendLine(", color_nm")
        sb.AppendLine(" *")
        sb.AppendLine("FROM t_cd_temp_relation")
        sb.AppendLine("WHERE 1=1")
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   'line_id
        End If
        If code_key <> "" Then
            sb.AppendLine("AND code=@code_key")   'code
        End If
        If tempId_key <> "" Then
            sb.AppendLine("AND temp_id=@temp_id_key")   'temp_id
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@code_key", SqlDbType.VarChar, 20, code_key))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id_key", SqlDbType.VarChar, 10, tempId_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "t_cd_temp_relation", paramList.ToArray)

        Return dsInfo.Tables("t_cd_temp_relation")

    End Function

    ''' <summary>
    ''' 
    ''' Infoを更新する
    ''' </summary>
    '''<param name="lineId_key">line_id</param>
    '''<param name="code_key">code</param>
    '''<param name="tempId_key">temp_id</param>
    '''<param name="lineId">line_id</param>
    '''<param name="code">code</param>
    '''<param name="tempId">temp_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdTCdTempRelation(ByVal lineId_key As String, _
               ByVal code_key As String, _
               ByVal tempId_key As String, _
               ByVal lineId As String, _
               ByVal code As String, _
               ByVal tempId As String, _
               ByVal color_nm As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル： : t_cd_temp_relation
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE t_cd_temp_relation")
        sb.AppendLine("SET")
        sb.AppendLine("line_id=@line_id")                                              'line_id
        sb.AppendLine(", code=@code")                                                  'code
        sb.AppendLine(", temp_id=@temp_id")                                            'temp_id
        sb.AppendLine(", color_nm=@color_nm")


        sb.AppendLine(", upd_user='" & menu_user & "'")
        sb.AppendLine(", upd_date=getdate()")

        sb.AppendLine("FROM t_cd_temp_relation")
        sb.AppendLine("WHERE 1=1")
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   'line_id
        End If
        If code_key <> "" Then
            sb.AppendLine("AND code=@code_key")   'code
        End If
        If tempId_key <> "" Then
            sb.AppendLine("AND temp_id=@temp_id_key")   'temp_id
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@code_key", SqlDbType.VarChar, 20, code_key))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id_key", SqlDbType.VarChar, 10, tempId_key))

        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@code", SqlDbType.VarChar, 20, code))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id", SqlDbType.VarChar, 10, tempId))
        paramList.Add(SqlHelperNew.MakeParam("@color_nm", SqlDbType.NVarChar, 50, color_nm))

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' Infoを登録する
    ''' </summary>
    '''<param name="lineId">line_id</param>
    '''<param name="code">code</param>
    '''<param name="tempId">temp_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsTCdTempRelation(ByVal lineId As String,
               ByVal code As String,
               ByVal tempId As String, ByVal color_nm As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル： : t_cd_temp_relation
        Dim sb As New StringBuilder

        sb.AppendLine("DELETE FROM t_cd_temp_relation WHERE line_id=@line_id AND code=@code")
        'SQL文
        sb.AppendLine("INSERT INTO  t_cd_temp_relation")
        sb.AppendLine("(")
        sb.AppendLine("line_id")                                                   'line_id
        sb.AppendLine(", code")                                                    'code
        sb.AppendLine(", temp_id")                                                 'temp_id
        sb.AppendLine(", color_nm")                                                 'temp_id
        sb.AppendLine(", ins_user")
        sb.AppendLine(", ins_date")
        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@line_id")                                                      'line_id
        sb.AppendLine(", @code")                                                       'code
        sb.AppendLine(", @temp_id")                                                    'temp_id
        sb.AppendLine(", @color_nm")
        sb.AppendLine(", '" & menu_user & "'")
        sb.AppendLine(", getdate()")

        sb.AppendLine(")")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@code", SqlDbType.VarChar, 20, code))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id", SqlDbType.VarChar, 10, tempId))
        paramList.Add(SqlHelperNew.MakeParam("@color_nm", SqlDbType.NVarChar, 50, color_nm))

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' Infoを削除する
    ''' </summary>
    '''<param name="lineId_key">line_id</param>
    '''<param name="code_key">code</param>
    '''<param name="tempId_key">temp_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelTCdTempRelation(ByVal lineId_key As String, _
               ByVal code_key As String, _
               ByVal tempId_key As String) As Boolean

        'SQLコメント
        '--**テーブル： : t_cd_temp_relation
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM t_cd_temp_relation")
        sb.AppendLine("WHERE 1=1")
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   'line_id
        End If
        If code_key <> "" Then
            sb.AppendLine("AND code=@code_key")   'code
        End If
        If tempId_key <> "" Then
            sb.AppendLine("AND temp_id=@temp_id_key")   'temp_id
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@code_key", SqlDbType.VarChar, 20, code_key))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id_key", SqlDbType.VarChar, 10, tempId_key))


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



    Public Function Inst_colorcheck_resultLastCopy(ByVal line_cd As String, ByVal make_no As String, ByVal color As String, ByVal user As String) As String

        'SQLコメント
        '--**テーブル： : t_cd_temp_relation
        Dim sb As New StringBuilder

        Dim line_cd_3keta As String

        If line_cd.Length = 8 Then
            line_cd_3keta = Left(Right(line_cd, 4), 3)
        ElseIf line_cd.Length = 4 Then
            line_cd_3keta = Left(line_cd, 3)
        Else
            line_cd_3keta = line_cd
        End If
        sb.Length = 0
        sb.AppendLine("  select top 1 * from [m_Line_List] where [zhipinLine]='" & line_cd_3keta & "'")
        Dim dsInfo2 As New Data.DataSet
        Dim zhongjiancaiLine As String = ""
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo2, "ms")
        If dsInfo2.Tables("ms").Rows.Count > 0 Then
            zhongjiancaiLine = dsInfo2.Tables("ms").Rows(0).Item("zhongjiancaiLine").ToString().Trim
        Else
            Return "NG"
        End If


        'SQL文
        'sb.AppendLine("  select top 1 * from [t_colorcheck_result] where make_no='" & make_no & "'  AND checkResult in('OK','YES') order by insertdate desc")
        sb.Length = 0
        sb.AppendLine("  select top 1 * from [t_colorcheck_result] where linecode='" & zhongjiancaiLine & "' AND make_no='" & make_no & "'  AND checkResult in('OK','YES') order by insertdate desc")

        'PARAM
        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "ms")

        Try
            If dsInfo.Tables("ms").Rows.Count > 0 Then
                Return dsInfo.Tables("ms").Rows(0).Item("checkResult")
                'If dsInfo.Tables("ms").Rows(0).Item("colorTxt") = color Then

                '    sb.Length = 0
                '    'sb.AppendLine("insert into t_colorcheck_result(")
                '    'sb.AppendLine("     [make_no]")
                '    'sb.AppendLine("      ,[code]")
                '    'sb.AppendLine("      ,[colorTxt]")
                '    'sb.AppendLine("      ,[checkResult]")
                '    'sb.AppendLine("      ,[enable]")
                '    'sb.AppendLine("      ,[remark]")
                '    'sb.AppendLine("      ,[insertuser]")
                '    'sb.AppendLine("      ,[insertdate]")
                '    'sb.AppendLine("      ,[updateuser]")
                '    'sb.AppendLine("      ,[updatedate]")
                '    'sb.AppendLine("      )")
                '    'sb.AppendLine("select top 1 ")
                '    'sb.AppendLine("       [make_no]")
                '    'sb.AppendLine("      ,[code]")
                '    'sb.AppendLine("      ,[colorTxt]")
                '    'sb.AppendLine("      ,'OK'")
                '    'sb.AppendLine("      ,[enable]")
                '    'sb.AppendLine("      ,[remark]")
                '    'sb.AppendLine("      ,'" & user & "'")
                '    'sb.AppendLine("      ,getdate()")
                '    'sb.AppendLine("      ,'" & user & "'")
                '    'sb.AppendLine("      ,getdate()")
                '    'sb.AppendLine("from t_colorcheck_result")
                '    'sb.AppendLine("where linecode='" & line_cd & "' AND checkResult in('OK','YES')  order by insertdate desc")

                '    'SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString())

                '    Return dsInfo.Tables("ms").Rows(0).Item("checkResult")
                'Else
                '    Return "颜色与检查结果不一致"
                'End If

            Else
                Return "没有找到颜色检查结果"
            End If
        Catch ex As Exception
            Return "NG"
        End Try




    End Function

End Class
