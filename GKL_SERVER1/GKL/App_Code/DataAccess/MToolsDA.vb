



Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MToolsDA
    Public SqlHelperNew As New SqlHelperNew
    ''' <summary>
    ''' 
    ''' 治具MSInfoを検索する
    ''' </summary>
    '''<param name="toolId_key">治具ID</param>
    '''<param name="lineId_key">生产线</param>
    ''' <returns>治具MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelMTools(ByVal toolId_key As String, _
           ByVal lineId_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：治具MS : m_tools
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("tool_id")                                                   '治具ID
        sb.AppendLine(", line_id")                                                 '生产线
        sb.AppendLine(", project_name")                                            '工程
        sb.AppendLine(", tool_name")                                               '治具显示文本

        sb.AppendLine("FROM m_tools")
        sb.AppendLine("WHERE 1=1")
        If toolId_key <> "" Then
            'sb.AppendLine("AND tool_id=@tool_id_key")   '治具ID
            sb.AppendLine("AND tool_id like @tool_id_key")   '治具ID
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        sb.AppendLine("ORDER BY line_id")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@tool_id_key", SqlDbType.VarChar, 42, "%" & toolId_key & "%"))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_tools", paramList.ToArray)

        Return dsInfo.Tables("m_tools")

    End Function

    ''' <summary>
    ''' 
    ''' 治具MSInfoを更新する
    ''' </summary>
    '''<param name="toolId_key">治具ID</param>
    '''<param name="lineId_key">生产线</param>
    '''<param name="toolId">治具ID</param>
    '''<param name="lineId">生产线</param>
    '''<param name="projectName">工程</param>
    '''<param name="toolName">治具显示文本</param>
    ''' <returns>治具MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMTools(ByVal toolId_key As String, _
               ByVal lineId_key As String, _
               ByVal toolId As String, _
               ByVal lineId As String, _
               ByVal projectName As String, _
               ByVal toolName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：治具MS : m_tools
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE m_tools")
        sb.AppendLine("SET")
        sb.AppendLine("tool_id=@tool_id")                                              '治具ID
        sb.AppendLine(", line_id=@line_id")                                            '生产线
        sb.AppendLine(", project_name=@project_name")   '工程
        sb.AppendLine(", tool_name=@tool_name")   '治具显示文本
        sb.AppendLine(", upd_user='" & menu_user & "'")
        sb.AppendLine(", upd_date=getdate()")

        sb.AppendLine("FROM m_tools")
        sb.AppendLine("WHERE 1=1")
        If toolId_key <> "" Then
            sb.AppendLine("AND tool_id=@tool_id_key")   '治具ID
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@tool_id_key", SqlDbType.VarChar, 40, toolId_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        paramList.Add(SqlHelperNew.MakeParam("@tool_id", SqlDbType.VarChar, 40, toolId))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@project_name", SqlDbType.NVarChar, 40, projectName))
        paramList.Add(SqlHelperNew.MakeParam("@tool_name", SqlDbType.NVarChar, 80, toolName))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 治具MSInfoを登録する
    ''' </summary>
    '''<param name="toolId">治具ID</param>
    '''<param name="lineId">生产线</param>
    '''<param name="projectName">工程</param>
    '''<param name="toolName">治具显示文本</param>
    ''' <returns>治具MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsMTools(ByVal toolId As String, _
               ByVal lineId As String, _
               ByVal projectName As String, _
               ByVal toolName As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：治具MS : m_tools
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO  m_tools")
        sb.AppendLine("(")
        sb.AppendLine("tool_id")                                                   '治具ID
        sb.AppendLine(", line_id")                                                 '生产线
        sb.AppendLine(", project_name")                                            '工程
        sb.AppendLine(", tool_name")                                               '治具显示文本
        sb.AppendLine(", ins_user")
        sb.AppendLine(", ins_date")

        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@tool_id")                                                      '治具ID
        sb.AppendLine(", @line_id")                                                    '生产线
        sb.AppendLine(", @project_name")                                               '工程
        sb.AppendLine(", @tool_name")                                                  '治具显示文本
        sb.AppendLine(", '" & menu_user & "'")
        sb.AppendLine(", getdate()")

        sb.AppendLine(")")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@tool_id", SqlDbType.VarChar, 40, toolId))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@project_name", SqlDbType.NVarChar, 40, projectName))
        paramList.Add(SqlHelperNew.MakeParam("@tool_name", SqlDbType.NVarChar, 80, toolName))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 治具MSInfoを削除する
    ''' </summary>
    '''<param name="toolId_key">治具ID</param>
    '''<param name="lineId_key">生产线</param>
    ''' <returns>治具MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelMTools(ByVal toolId_key As String, _
               ByVal lineId_key As String) As Boolean

        'SQLコメント
        '--**テーブル：治具MS : m_tools
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM m_tools")
        sb.AppendLine("WHERE 1=1")
        If toolId_key <> "" Then
            sb.AppendLine("AND tool_id=@tool_id_key")   '治具ID
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@tool_id_key", SqlDbType.VarChar, 40, toolId_key))
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
