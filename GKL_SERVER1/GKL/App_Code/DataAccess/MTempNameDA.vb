
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MTempNameDA

    Public SqlHelperNew As New SqlHelperNew

    ''' <summary>
    ''' 
    ''' 模板名称MSInfoを検索する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    ''' <returns>模板名称MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/09  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelMTempName(ByVal lineId_key As String, _
           ByVal tempId_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：模板名称MS : m_temp_name
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("line_id")                                                   '生产线
        sb.AppendLine(", temp_id")                                                 '检查模板编号
        sb.AppendLine(", temp_name")                                               '模板名称

        sb.AppendLine("FROM m_temp_name")
        sb.AppendLine("WHERE 1=1")
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        If tempId_key <> "" Then
            sb.AppendLine("AND temp_id=@temp_id_key")   '检查模板编号
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id_key", SqlDbType.VarChar, 10, tempId_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_temp_name", paramList.ToArray)

        Return dsInfo.Tables("m_temp_name")

    End Function

    ''' <summary>
    ''' 
    ''' 模板名称MSInfoを更新する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    '''<param name="lineId">生产线</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="tempName">模板名称</param>
    ''' <returns>模板名称MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/09  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMTempName(ByVal lineId_key As String, _
               ByVal tempId_key As String, _
               ByVal lineId As String, _
               ByVal tempId As String, _
               ByVal tempName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：模板名称MS : m_temp_name
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE m_temp_name")
        sb.AppendLine("SET")
        sb.AppendLine("line_id=@line_id")                                              '生产线
        sb.AppendLine(", temp_id=@temp_id")                                            '检查模板编号
        sb.AppendLine(", temp_name=@temp_name")   '模板名称
        sb.AppendLine(", upd_user='" & menu_user & "'")
        sb.AppendLine(", upd_date=getdate()")
        sb.AppendLine("FROM m_temp_name")
        sb.AppendLine("WHERE 1=1")
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        If tempId_key <> "" Then
            sb.AppendLine("AND temp_id=@temp_id_key")   '检查模板编号
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id_key", SqlDbType.VarChar, 10, tempId_key.Replace(vbCrLf, "")))

        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id", SqlDbType.VarChar, 10, tempId.Replace(vbCrLf, "")))
        paramList.Add(SqlHelperNew.MakeParam("@temp_name", SqlDbType.NVarChar, 200, tempName))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 模板名称MSInfoを登録する
    ''' </summary>
    '''<param name="lineId">生产线</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="tempName">模板名称</param>
    ''' <returns>模板名称MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/09  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsMTempName(ByVal lineId As String, _
               ByVal tempId As String, _
               ByVal tempName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：模板名称MS : m_temp_name
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO  m_temp_name")
        sb.AppendLine("(")
        sb.AppendLine("line_id")                                                   '生产线
        sb.AppendLine(", temp_id")                                                 '检查模板编号
        sb.AppendLine(", temp_name")                                               '模板名称
        sb.AppendLine(", ins_user")
        sb.AppendLine(", ins_date")
        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@line_id")                                                      '生产线
        sb.AppendLine(", @temp_id")                                                    '检查模板编号
        sb.AppendLine(", @temp_name")                                                  '模板名称
        sb.AppendLine(", '" & menu_user & "'")
        sb.AppendLine(", getdate()")

        sb.AppendLine(")")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id", SqlDbType.VarChar, 10, tempId.Replace(vbCrLf, "")))
        paramList.Add(SqlHelperNew.MakeParam("@temp_name", SqlDbType.NVarChar, 200, tempName))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 模板名称MSInfoを削除する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    ''' <returns>模板名称MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/09  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelMTempName(ByVal lineId_key As String, _
               ByVal tempId_key As String) As Boolean

        'SQLコメント
        '--**テーブル：模板名称MS : m_temp_name
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM m_temp_name")
        sb.AppendLine("WHERE 1=1")
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        If tempId_key <> "" Then
            sb.AppendLine("AND temp_id=@temp_id_key")   '检查模板编号
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
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

End Class
