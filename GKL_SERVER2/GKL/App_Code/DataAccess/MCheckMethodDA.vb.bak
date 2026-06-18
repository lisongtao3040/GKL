



Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MCheckMethodDA
    Public SqlHelperNew As New SqlHelperNew
    ''' <summary>
    ''' 
    ''' 检查方法MSInfoを検索する
    ''' </summary>
    '''<param name="chkId_key">检查方法ID</param>
    '''<param name="chkName_key">检查方法名</param>
    ''' <returns>检查方法MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelMCheckMethod(ByVal chkId_key As String, _
           ByVal chkName_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查方法MS : m_check_method
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("chk_id")                                                    '检查方法ID
        sb.AppendLine(", chk_name")                                                '检查方法名
        sb.AppendLine(", chk_method")                                              '检查方法
        sb.AppendLine(", chk_formula")                                             '检查方公式
        sb.AppendLine(", verify_method_explain")   '核对方法说明

        sb.AppendLine("FROM m_check_method")
        sb.AppendLine("WHERE 1=1")
        If chkId_key <> "" Then
            sb.AppendLine("AND chk_id like @chk_id_key")   '检查方法ID
        End If
        If chkName_key <> "" Then
            sb.AppendLine("AND chk_name like @chk_name_key")   '检查方法名
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_id_key", SqlDbType.VarChar, 12, "%" & chkId_key & "%"))
        paramList.Add(SqlHelperNew.MakeParam("@chk_name_key", SqlDbType.NVarChar, 22, "%" & chkName_key & "%"))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_check_method", paramList.ToArray)

        Return dsInfo.Tables("m_check_method")

    End Function

    ''' <summary>
    ''' 
    ''' 检查方法MSInfoを更新する
    ''' </summary>
    '''<param name="chkId_key">检查方法ID</param>
    '''<param name="chkName_key">检查方法名</param>
    '''<param name="chkId">检查方法ID</param>
    '''<param name="chkName">检查方法名</param>
    '''<param name="chkMethod">检查方法</param>
    '''<param name="chkFormula">检查方公式</param>
    '''<param name="verifyMethodExplain">核对方法说明</param>
    ''' <returns>检查方法MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMCheckMethod(ByVal chkId_key As String, _
               ByVal chkName_key As String, _
               ByVal chkId As String, _
               ByVal chkName As String, _
               ByVal chkMethod As String, _
               ByVal chkFormula As String, _
               ByVal verifyMethodExplain As String) As Boolean

        'SQLコメント
        '--**テーブル：检查方法MS : m_check_method
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE m_check_method")
        sb.AppendLine("SET")
        sb.AppendLine("chk_id=@chk_id")                                                '检查方法ID
        sb.AppendLine(", chk_name=@chk_name")   '检查方法名
        sb.AppendLine(", chk_method=@chk_method")   '检查方法
        sb.AppendLine(", chk_formula=@chk_formula")   '检查方公式
        sb.AppendLine(", verify_method_explain=@verify_method_explain")   '核对方法说明

        sb.AppendLine("FROM m_check_method")
        sb.AppendLine("WHERE 1=1")
        If chkId_key <> "" Then
            sb.AppendLine("AND chk_id=@chk_id_key")   '检查方法ID
        End If
        If chkName_key <> "" Then
            sb.AppendLine("AND chk_name=@chk_name_key")   '检查方法名
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_id_key", SqlDbType.VarChar, 10, chkId_key))
        paramList.Add(SqlHelperNew.MakeParam("@chk_name_key", SqlDbType.nvarchar, 20, chkName_key))

        paramList.Add(SqlHelperNew.MakeParam("@chk_id", SqlDbType.VarChar, 10, chkId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_name", SqlDbType.nvarchar, 20, chkName))
        paramList.Add(SqlHelperNew.MakeParam("@chk_method", SqlDbType.nvarchar, 1, chkMethod))
        paramList.Add(SqlHelperNew.MakeParam("@chk_formula", SqlDbType.NVarChar, 200, chkFormula))
        paramList.Add(SqlHelperNew.MakeParam("@verify_method_explain", SqlDbType.nvarchar, 200, verifyMethodExplain))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 检查方法MSInfoを登録する
    ''' </summary>
    '''<param name="chkId">检查方法ID</param>
    '''<param name="chkName">检查方法名</param>
    '''<param name="chkMethod">检查方法</param>
    '''<param name="chkFormula">检查方公式</param>
    '''<param name="verifyMethodExplain">核对方法说明</param>
    ''' <returns>检查方法MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsMCheckMethod(ByVal chkId As String, _
               ByVal chkName As String, _
               ByVal chkMethod As String, _
               ByVal chkFormula As String, _
               ByVal verifyMethodExplain As String) As Boolean

        'SQLコメント
        '--**テーブル：检查方法MS : m_check_method
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO  m_check_method")
        sb.AppendLine("(")
        sb.AppendLine("chk_id")                                                    '检查方法ID
        sb.AppendLine(", chk_name")                                                '检查方法名
        sb.AppendLine(", chk_method")                                              '检查方法
        sb.AppendLine(", chk_formula")                                             '检查方公式
        sb.AppendLine(", verify_method_explain")   '核对方法说明

        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@chk_id")                                                       '检查方法ID
        sb.AppendLine(", @chk_name")                                                   '检查方法名
        sb.AppendLine(", @chk_method")                                                 '检查方法
        sb.AppendLine(", @chk_formula")                                                '检查方公式
        sb.AppendLine(", @verify_method_explain")   '核对方法说明

        sb.AppendLine(")")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_id", SqlDbType.VarChar, 10, chkId))
        paramList.Add(SqlHelperNew.MakeParam("@chk_name", SqlDbType.nvarchar, 20, chkName))
        paramList.Add(SqlHelperNew.MakeParam("@chk_method", SqlDbType.nvarchar, 1, chkMethod))
        paramList.Add(SqlHelperNew.MakeParam("@chk_formula", SqlDbType.NVarChar, 200, chkFormula))
        paramList.Add(SqlHelperNew.MakeParam("@verify_method_explain", SqlDbType.nvarchar, 200, verifyMethodExplain))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' 检查方法MSInfoを削除する
    ''' </summary>
    '''<param name="chkId_key">检查方法ID</param>
    '''<param name="chkName_key">检查方法名</param>
    ''' <returns>检查方法MSInfo</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelMCheckMethod(ByVal chkId_key As String, _
               ByVal chkName_key As String) As Boolean

        '--**テーブル：检查方法MS : m_check_method
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM m_check_method")
        sb.AppendLine("WHERE 1=1")
        If chkId_key <> "" Then
            sb.AppendLine("AND chk_id=@chk_id_key")   '检查方法ID
        End If
        If chkName_key <> "" Then
            sb.AppendLine("AND chk_name=@chk_name_key")   '检查方法名
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@chk_id_key", SqlDbType.VarChar, 10, chkId_key))
        paramList.Add(SqlHelperNew.MakeParam("@chk_name_key", SqlDbType.nvarchar, 20, chkName_key))


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
