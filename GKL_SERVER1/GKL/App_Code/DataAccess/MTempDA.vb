
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MTempDA

    Public SqlHelperNew As New SqlHelperNew

    ''' <summary>
    ''' 
    ''' 模板MSInfoを検索する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
'''<param name="tempId_key">检查模板编号</param>
'''<param name="chkMethodId_key">检查项目ID</param>
    ''' <returns>模板MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelMTemp(ByVal lineId_key As String, _
           ByVal tempId_key As String, _
           ByVal chkMethodId_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：模板MS : m_temp
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("line_id")                                                   '生产线
        sb.AppendLine(", temp_id")                                                 '检查模板编号
        sb.AppendLine(", chk_method_id")   '检查项目ID
        sb.AppendLine(", project_id")                                              '工程ID
        sb.AppendLine(", project_name")                                            '工程名
        sb.AppendLine(", pic_id")                                                  '图片ID
        sb.AppendLine(", pic_name")                                                '图片名称
        sb.AppendLine(", chk_km_name")                                             '检查项目
        sb.AppendLine(", pic_sign")                                                '图片标记
        sb.AppendLine(", chk_id")                                                  '检查方法ID
        sb.AppendLine(", chk_name")                                                '检查方法名
        sb.AppendLine(", tool_id")                                                 '治具ID
        sb.AppendLine(", kj_0")                                                    '基准
        sb.AppendLine(", kj_1")                                                    '工差1
        sb.AppendLine(", kj_2")                                                    '工差2
        sb.AppendLine(", kj_explain")                                              '基准説明

        sb.AppendLine("FROM m_temp")
        sb.AppendLine("WHERE 1=1")
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        If tempId_key <> "" Then
            sb.AppendLine("AND temp_id=@temp_id_key")   '检查模板编号
        End If
        If chkMethodId_key <> "" Then
            sb.AppendLine("AND chk_method_id=@chk_method_id_key")   '检查项目ID
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id_key", SqlDbType.nvarchar, 10, tempId_key))
        paramList.Add(SqlHelperNew.MakeParam("@chk_method_id_key", SqlDbType.VarChar, 10, chkMethodId_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_temp", paramList.ToArray)

        Return dsInfo.Tables("m_temp")

    End Function


    Public Function SelTempIds(ByVal lineId_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：模板MS : m_temp
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")

        sb.AppendLine("Distinct temp_id")                                                 '检查模板编号


        sb.AppendLine("FROM m_temp")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND line_id=@line_id_key")   '生产线
      
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_temp", paramList.ToArray)

        Return dsInfo.Tables("m_temp")

    End Function

    ''' <summary>
    ''' 
    ''' 模板MSInfoを更新する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    '''<param name="chkMethodId_key">检查项目ID</param>
    '''<param name="lineId">生产线</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="chkMethodId">检查项目ID</param>
    '''<param name="projectId">工程ID</param>
    '''<param name="projectName">工程名</param>
    '''<param name="picId">图片ID</param>
    '''<param name="picName">图片名称</param>
    '''<param name="chkKmName">检查项目</param>
    '''<param name="picSign">图片标记</param>
    '''<param name="chkId">检查方法ID</param>
    '''<param name="chkName">检查方法名</param>
    '''<param name="toolId">治具ID</param>
    '''<param name="kj0">基准</param>
    '''<param name="kj1">工差1</param>
    '''<param name="kj2">工差2</param>
    '''<param name="kjExplain">基准説明</param>
    ''' <returns>模板MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMTemp(ByVal lineId_key As String, _
               ByVal tempId_key As String, _
               ByVal chkMethodId_key As String, _
               ByVal lineId As String, _
               ByVal tempId As String, _
               ByVal chkMethodId As String, _
               ByVal projectId As String, _
               ByVal projectName As String, _
               ByVal picId As String, _
               ByVal picName As String, _
               ByVal chkKmName As String, _
               ByVal picSign As String, _
               ByVal chkId As String, _
               ByVal chkName As String, _
               ByVal toolId As String, _
               ByVal kj0 As String, _
               ByVal kj1 As String, _
               ByVal kj2 As String, _
               ByVal kjExplain As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：模板MS : m_temp
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE m_temp")
        sb.AppendLine("SET")
        sb.AppendLine("line_id=@line_id")                                              '生产线
        sb.AppendLine(", temp_id=@temp_id")                                            '检查模板编号
        sb.AppendLine(", chk_method_id=@chk_method_id")   '检查项目ID
        sb.AppendLine(", project_id=@project_id")   '工程ID
        sb.AppendLine(", project_name=@project_name")   '工程名
        sb.AppendLine(", pic_id=@pic_id")                                              '图片ID
        sb.AppendLine(", pic_name=@pic_name")   '图片名称
        sb.AppendLine(", chk_km_name=@chk_km_name")   '检查项目
        sb.AppendLine(", pic_sign=@pic_sign")   '图片标记
        sb.AppendLine(", chk_id=@chk_id")                                              '检查方法ID
        sb.AppendLine(", chk_name=@chk_name")   '检查方法名
        sb.AppendLine(", tool_id=@tool_id")                                            '治具ID
        sb.AppendLine(", kj_0=@kj_0")                                                  '基准
        sb.AppendLine(", kj_1=@kj_1")                                                  '工差1
        sb.AppendLine(", kj_2=@kj_2")                                                  '工差2
        sb.AppendLine(", kj_explain=@kj_explain")   '基准説明
        sb.AppendLine(", upd_user='" & menu_user & "'")
        sb.AppendLine(", upd_date=getdate()")

        sb.AppendLine("FROM m_temp")
        sb.AppendLine("WHERE 1=1")
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        If tempId_key <> "" Then
            sb.AppendLine("AND temp_id=@temp_id_key")   '检查模板编号
        End If
        If chkMethodId_key <> "" Then
            sb.AppendLine("AND chk_method_id=@chk_method_id_key")   '检查项目ID
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id_key", SqlDbType.NVarChar, 10, tempId_key))
        paramList.Add(SqlHelperNew.MakeParam("@chk_method_id_key", SqlDbType.VarChar, 10, chkMethodId_key))

        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id", SqlDbType.NVarChar, 10, tempId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_method_id", SqlDbType.VarChar, 10, chkMethodId))
        paramList.Add(SqlHelperNew.MakeParam("@project_id", SqlDbType.VarChar, 10, projectId))
        paramList.Add(SqlHelperNew.MakeParam("@project_name", SqlDbType.NVarChar, 40, projectName))
        paramList.Add(SqlHelperNew.MakeParam("@pic_id", SqlDbType.VarChar, 10, picId))
        paramList.Add(SqlHelperNew.MakeParam("@pic_name", SqlDbType.NVarChar, 200, picName))
        paramList.Add(SqlHelperNew.MakeParam("@chk_km_name", SqlDbType.NVarChar, 200, chkKmName))
        paramList.Add(SqlHelperNew.MakeParam("@pic_sign", SqlDbType.VarChar, 10, picSign))
        paramList.Add(SqlHelperNew.MakeParam("@chk_id", SqlDbType.VarChar, 10, chkId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_name", SqlDbType.NVarChar, 20, chkName))
        paramList.Add(SqlHelperNew.MakeParam("@tool_id", SqlDbType.VarChar, 40, toolId))
        paramList.Add(SqlHelperNew.MakeParam("@kj_0", SqlDbType.VarChar, 100, kj0))
        paramList.Add(SqlHelperNew.MakeParam("@kj_1", SqlDbType.VarChar, 20, kj1))
        paramList.Add(SqlHelperNew.MakeParam("@kj_2", SqlDbType.VarChar, 20, kj2))
        paramList.Add(SqlHelperNew.MakeParam("@kj_explain", SqlDbType.NVarChar, 200, kjExplain))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 模板MSInfoを登録する
    ''' </summary>
    '''<param name="lineId">生产线</param>
    '''<param name="tempId">检查模板编号</param>
    '''<param name="chkMethodId">检查项目ID</param>
    '''<param name="projectId">工程ID</param>
    '''<param name="projectName">工程名</param>
    '''<param name="picId">图片ID</param>
    '''<param name="picName">图片名称</param>
    '''<param name="chkKmName">检查项目</param>
    '''<param name="picSign">图片标记</param>
    '''<param name="chkId">检查方法ID</param>
    '''<param name="chkName">检查方法名</param>
    '''<param name="toolId">治具ID</param>
    '''<param name="kj0">基准</param>
    '''<param name="kj1">工差1</param>
    '''<param name="kj2">工差2</param>
    '''<param name="kjExplain">基准説明</param>
    ''' <returns>模板MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsMTemp(ByVal lineId As String, _
               ByVal tempId As String, _
               ByVal chkMethodId As String, _
               ByVal projectId As String, _
               ByVal projectName As String, _
               ByVal picId As String, _
               ByVal picName As String, _
               ByVal chkKmName As String, _
               ByVal picSign As String, _
               ByVal chkId As String, _
               ByVal chkName As String, _
               ByVal toolId As String, _
               ByVal kj0 As String, _
               ByVal kj1 As String, _
               ByVal kj2 As String, _
               ByVal kjExplain As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：模板MS : m_temp
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO  m_temp")
        sb.AppendLine("(")
        sb.AppendLine("line_id")                                                   '生产线
        sb.AppendLine(", temp_id")                                                 '检查模板编号
        sb.AppendLine(", chk_method_id")   '检查项目ID
        sb.AppendLine(", project_id")                                              '工程ID
        sb.AppendLine(", project_name")                                            '工程名
        sb.AppendLine(", pic_id")                                                  '图片ID
        sb.AppendLine(", pic_name")                                                '图片名称
        sb.AppendLine(", chk_km_name")                                             '检查项目
        sb.AppendLine(", pic_sign")                                                '图片标记
        sb.AppendLine(", chk_id")                                                  '检查方法ID
        sb.AppendLine(", chk_name")                                                '检查方法名
        sb.AppendLine(", tool_id")                                                 '治具ID
        sb.AppendLine(", kj_0")                                                    '基准
        sb.AppendLine(", kj_1")                                                    '工差1
        sb.AppendLine(", kj_2")                                                    '工差2
        sb.AppendLine(", kj_explain")                                              '基准説明
        sb.AppendLine(", ins_user")
        sb.AppendLine(", ins_date")
        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@line_id")                                                      '生产线
        sb.AppendLine(", @temp_id")                                                    '检查模板编号
        sb.AppendLine(", @chk_method_id")                                              '检查项目ID
        sb.AppendLine(", @project_id")                                                 '工程ID
        sb.AppendLine(", @project_name")                                               '工程名
        sb.AppendLine(", @pic_id")                                                     '图片ID
        sb.AppendLine(", @pic_name")                                                   '图片名称
        sb.AppendLine(", @chk_km_name")                                                '检查项目
        sb.AppendLine(", @pic_sign")                                                   '图片标记
        sb.AppendLine(", @chk_id")                                                     '检查方法ID
        sb.AppendLine(", @chk_name")                                                   '检查方法名
        sb.AppendLine(", @tool_id")                                                    '治具ID
        sb.AppendLine(", @kj_0")                                                       '基准
        sb.AppendLine(", @kj_1")                                                       '工差1
        sb.AppendLine(", @kj_2")                                                       '工差2
        sb.AppendLine(", @kj_explain")                                                 '基准説明

        sb.AppendLine(", '" & menu_user & "'")
        sb.AppendLine(", getdate()")
        sb.AppendLine(")")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id", SqlDbType.NVarChar, 10, tempId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_method_id", SqlDbType.VarChar, 10, chkMethodId))
        paramList.Add(SqlHelperNew.MakeParam("@project_id", SqlDbType.VarChar, 10, projectId))
        paramList.Add(SqlHelperNew.MakeParam("@project_name", SqlDbType.NVarChar, 40, projectName))
        paramList.Add(SqlHelperNew.MakeParam("@pic_id", SqlDbType.VarChar, 10, picId))
        paramList.Add(SqlHelperNew.MakeParam("@pic_name", SqlDbType.NVarChar, 200, picName))
        paramList.Add(SqlHelperNew.MakeParam("@chk_km_name", SqlDbType.NVarChar, 200, chkKmName))
        paramList.Add(SqlHelperNew.MakeParam("@pic_sign", SqlDbType.VarChar, 10, picSign))
        paramList.Add(SqlHelperNew.MakeParam("@chk_id", SqlDbType.VarChar, 10, chkId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_name", SqlDbType.NVarChar, 20, chkName))
        paramList.Add(SqlHelperNew.MakeParam("@tool_id", SqlDbType.VarChar, 40, toolId))
        paramList.Add(SqlHelperNew.MakeParam("@kj_0", SqlDbType.VarChar, 100, kj0))
        paramList.Add(SqlHelperNew.MakeParam("@kj_1", SqlDbType.VarChar, 20, kj1))
        paramList.Add(SqlHelperNew.MakeParam("@kj_2", SqlDbType.VarChar, 20, kj2))
        paramList.Add(SqlHelperNew.MakeParam("@kj_explain", SqlDbType.NVarChar, 200, kjExplain))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 模板MSInfoを削除する
    ''' </summary>
    '''<param name="lineId_key">生产线</param>
    '''<param name="tempId_key">检查模板编号</param>
    '''<param name="chkMethodId_key">检查项目ID</param>
    ''' <returns>模板MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelMTemp(ByVal lineId_key As String, _
               ByVal tempId_key As String, _
               ByVal chkMethodId_key As String) As Boolean

        'SQLコメント
        '--**テーブル：模板MS : m_temp
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM m_temp")
        sb.AppendLine("WHERE 1=1")
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   '生产线
        End If
        If tempId_key <> "" Then
            sb.AppendLine("AND temp_id=@temp_id_key")   '检查模板编号
        End If
        If chkMethodId_key <> "" Then
            sb.AppendLine("AND chk_method_id=@chk_method_id_key")   '检查项目ID
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id_key", SqlDbType.nvarchar, 10, tempId_key))
        paramList.Add(SqlHelperNew.MakeParam("@chk_method_id_key", SqlDbType.VarChar, 10, chkMethodId_key))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function



    Public Function CopyTemp(ByVal lineId_key As String, _
                            ByVal tempId_key As String, _
                            ByVal tempId_key_new As String, ByVal menu_user As String) As Boolean

        'SQLコメント
        '--**テーブル：模板MS : m_temp
        Dim sb As New StringBuilder
        'SQL文

        sb.AppendLine("DELETE FROM m_temp")
        sb.AppendLine("WHERE line_id=@line_id_key")   '生产线
        sb.AppendLine("AND temp_id='" & tempId_key_new & "'")   '检查模板编号


        sb.AppendLine("INSERT INTO [m_temp]")
        sb.AppendLine("SELECT [line_id]")
        sb.AppendLine("      ,'" & tempId_key_new & "'")
        sb.AppendLine("      ,[chk_method_id]")
        sb.AppendLine("      ,[project_id]")
        sb.AppendLine("      ,[project_name]")
        sb.AppendLine("      ,[pic_id]")
        sb.AppendLine("      ,[pic_name]")
        sb.AppendLine("      ,[chk_km_name]")
        sb.AppendLine("      ,[pic_sign]")
        sb.AppendLine("      ,[chk_id]")
        sb.AppendLine("      ,[chk_name]")
        sb.AppendLine("      ,[tool_id]")
        sb.AppendLine("      ,[kj_0]")
        sb.AppendLine("      ,[kj_1]")
        sb.AppendLine("      ,[kj_2]")
        sb.AppendLine("      ,[kj_explain]")
        sb.AppendLine(", '" & menu_user & "'")
        sb.AppendLine(", getdate()")
        sb.AppendLine(", '" & menu_user & "'")
        sb.AppendLine(", getdate()")
        sb.AppendLine("  FROM [m_temp]")


        sb.AppendLine("WHERE line_id=@line_id_key")   '生产线
        sb.AppendLine("AND temp_id=@temp_id_key")   '检查模板编号



        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id_key", SqlDbType.NVarChar, 10, tempId_key))


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




    Public Function SelMTempChk(ByVal lineId_key As String, _
           ByVal tempId_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：模板MS : m_temp
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("case when m_project.project_id IS null then N'工程不存在'  else '' end as 工程检查")
        sb.AppendLine(",case when m_temp_name.line_id IS null then N'模板名不存在'  else '' end as 模板名检查")
        sb.AppendLine(",case when m_tools.line_id IS null and ISNULL(m_temp.tool_id,'') <>'' then N'治具不存在' else '' end as 治具检查")
        sb.AppendLine(",case when m_picture.line_id IS null and ISNULL(m_temp.pic_id,'') <>'' then N'图片不存在' else '' end as 图片检查")
        sb.AppendLine(",case when m_check_method.chk_id IS null then N'检查规则不存在'  else '' end as 检查规则检查")
        sb.AppendLine("	, m_temp.line_id")
        sb.AppendLine("	, m_temp.temp_id")
        sb.AppendLine("	, m_temp.chk_method_id")
        sb.AppendLine("	, m_temp.project_id")
        'sb.AppendLine("	, m_temp.project_name")
        sb.AppendLine("	, m_temp.pic_id")
        'sb.AppendLine("	, m_temp.pic_name")
        'sb.AppendLine("	, m_temp.chk_km_name")
        'sb.AppendLine("	, m_temp.pic_sign")
        sb.AppendLine("	, m_temp.chk_id")
        'sb.AppendLine("	, m_temp.chk_name")
        sb.AppendLine("	, m_temp.tool_id")
        'sb.AppendLine("	, m_temp.kj_0")
        'sb.AppendLine("	, m_temp.kj_1")
        'sb.AppendLine("	, m_temp.kj_2")
        'sb.AppendLine("	, m_temp.kj_explain")
        sb.AppendLine("FROM m_temp ")
        sb.AppendLine("LEFT JOIN m_project ")
        sb.AppendLine("	ON m_temp.project_id = m_project.project_id ")
        sb.AppendLine("	AND m_temp.line_id = m_project.line_id ")
        sb.AppendLine("LEFT JOIN m_temp_name ")
        sb.AppendLine("	ON m_temp.line_id = m_temp_name.line_id ")
        sb.AppendLine("	AND m_temp.temp_id = m_temp_name.temp_id ")
        sb.AppendLine("LEFT JOIN m_tools ")
        sb.AppendLine("	ON m_temp.tool_id = m_tools.tool_id ")
        sb.AppendLine("	AND m_temp.line_id = m_tools.line_id ")
        sb.AppendLine("LEFT JOIN m_check_method ")
        sb.AppendLine("	ON m_temp.chk_id = m_check_method.chk_id")
        sb.AppendLine("LEFT JOIN m_picture ")
        sb.AppendLine("	ON m_temp.pic_id = m_picture.pic_id ")
        sb.AppendLine("	AND m_temp.line_id = m_picture.line_id ")

        sb.AppendLine("WHERE 1=1")
        If lineId_key <> "" Then
            sb.AppendLine("AND m_temp.line_id=@line_id_key")   '生产线
        End If
        If tempId_key <> "" Then
            sb.AppendLine("AND m_temp.temp_id=@temp_id_key")   '检查模板编号
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))
        paramList.Add(SqlHelperNew.MakeParam("@temp_id_key", SqlDbType.NVarChar, 10, tempId_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_temp", paramList.ToArray)

        Return dsInfo.Tables("m_temp")

    End Function
End Class
