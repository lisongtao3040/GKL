Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MProjectDA

    Public SqlHelperNew As New SqlHelperNew

    ''' <summary>
    ''' 
    ''' 工程MSInfoを検索する
    ''' </summary>
    '''<param name="projectId_key">工程ID</param>
    '''<param name="lineId_key">生产线</param>
    ''' <returns>工程MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelMProject(ByVal projectId_key As String, _
           ByVal lineId_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：工程MS : m_project
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("project_id")                                                '工程ID
        sb.AppendLine(", line_id")                                                 '生产线
        sb.AppendLine(", project_name")                                            '工程名

        sb.AppendLine("FROM m_project")
        sb.AppendLine("WHERE 1=1")
        If projectId_key <> "" Then
            sb.AppendLine("AND project_id=@project_id_key")   '工程ID
        End If
        If lineId_key <> "" Then

            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        sb.AppendLine("ORDER BY line_id,project_id")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@project_id_key", SqlDbType.VarChar, 10, projectId_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_project", paramList.ToArray)

        Return dsInfo.Tables("m_project")

    End Function

    ''' <summary>
    ''' 
    ''' 工程MSInfoを更新する
    ''' </summary>
    '''<param name="projectId_key">工程ID</param>
    '''<param name="lineId_key">生产线</param>
    '''<param name="projectId">工程ID</param>
    '''<param name="lineId">生产线</param>
    '''<param name="projectName">工程名</param>
    ''' <returns>工程MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMProject(ByVal projectId_key As String, _
               ByVal lineId_key As String, _
               ByVal projectId As String, _
               ByVal lineId As String, _
               ByVal projectName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：工程MS : m_project
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE m_project")
        sb.AppendLine("SET")
        sb.AppendLine("project_id=@project_id")   '工程ID
        sb.AppendLine(", line_id=@line_id")                                            '生产线
        sb.AppendLine(", project_name=@project_name")   '工程名
        sb.AppendLine(", upd_user='" & menu_user & "'")
        sb.AppendLine(", upd_date=getdate()")

        sb.AppendLine("FROM m_project")
        sb.AppendLine("WHERE 1=1")
        If projectId_key <> "" Then
            sb.AppendLine("AND project_id=@project_id_key")   '工程ID
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@project_id_key", SqlDbType.VarChar, 10, projectId_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        paramList.Add(SqlHelperNew.MakeParam("@project_id", SqlDbType.VarChar, 10, projectId))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@project_name", SqlDbType.NVarChar, 40, projectName))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 工程MSInfoを登録する
    ''' </summary>
    '''<param name="projectId">工程ID</param>
    '''<param name="lineId">生产线</param>
    '''<param name="projectName">工程名</param>
    ''' <returns>工程MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsMProject(ByVal projectId As String, _
               ByVal lineId As String, _
               ByVal projectName As String, ByVal menu_user As String) As Boolean
        'EMAB　ＥＲＲ
        'SQLコメント
        '--**テーブル：工程MS : m_project
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO  m_project")
        sb.AppendLine("(")
        sb.AppendLine("project_id")                                                '工程ID
        sb.AppendLine(", line_id")                                                 '生产线
        sb.AppendLine(", project_name")                                            '工程名
        sb.AppendLine(", ins_user")
        sb.AppendLine(", ins_date")
        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@project_id")                                                   '工程ID
        sb.AppendLine(", @line_id")                                                    '生产线
        sb.AppendLine(", @project_name")                                               '工程名
        sb.AppendLine(", '" & menu_user & "'")
        sb.AppendLine(", getdate()")
        sb.AppendLine(")")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@project_id", SqlDbType.VarChar, 10, projectId))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@project_name", SqlDbType.NVarChar, 40, projectName))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 工程MSInfoを削除する
    ''' </summary>
    '''<param name="projectId_key">工程ID</param>
    '''<param name="lineId_key">生产线</param>
    ''' <returns>工程MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelMProject(ByVal projectId_key As String, _
               ByVal lineId_key As String) As Boolean
        'SQLコメント
        '--**テーブル：工程MS : m_project
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM m_project")
        sb.AppendLine("WHERE 1=1")
        If projectId_key <> "" Then
            sb.AppendLine("AND project_id=@project_id_key")   '工程ID
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@project_id_key", SqlDbType.VarChar, 10, projectId_key))
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
