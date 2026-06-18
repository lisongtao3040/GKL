Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MLinesKengenDA
    Public SqlHelperNew As New SqlHelperNew
    ''' <summary>
    ''' 所有生产线权限表を検索する
    ''' </summary>
    '''<param name="lineIdGen_key">生产线ID</param>
    ''' <returns>所有生产线权限表</returns>
    ''' <remarks></remarks>
    Public Function SelMAllLinesKengen(ByVal lineIdGen_key As String) As Data.DataTable

        Dim sql As New StringBuilder
        sql.AppendLine("SELECT")
        sql.AppendLine("    line_id_gen")
        sql.AppendLine("FROM")
        sql.AppendLine("    m_all_lines_kengen")
        sql.AppendLine("WHERE")
        sql.AppendLine("    1 = 1")

        If lineIdGen_key <> "" Then
            sql.AppendLine("    AND line_id_gen LIKE '%' + @line_id_gen + '%'")
        End If

        sql.AppendLine("ORDER BY")
        sql.AppendLine("    line_id_gen")

        Dim paramList As New List(Of SqlParameter)
        If lineIdGen_key <> "" Then
            paramList.Add(New SqlParameter("@line_id_gen", lineIdGen_key))
        End If

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sql.ToString(), dsInfo, "dt", paramList.ToArray)
        Return dsInfo.Tables("dt")

    End Function

    ''' <summary>
    ''' 生产线参照表を検索する
    ''' </summary>
    '''<param name="lineIdGen_key">源生产线ID</param>
    '''<param name="lineIdSaki_key">目标生产线ID</param>
    ''' <returns>生产线参照表</returns>
    ''' <remarks></remarks>
    Public Function SelMLineSansyou(ByVal lineIdGen_key As String, ByVal lineIdSaki_key As String) As Data.DataTable

        Dim sql As New StringBuilder
        sql.AppendLine("SELECT")
        sql.AppendLine("    line_id_gen,")
        sql.AppendLine("    line_id_saki")
        sql.AppendLine("FROM")
        sql.AppendLine("    m_line_sansyou")
        sql.AppendLine("WHERE")
        sql.AppendLine("    1 = 1")

        If lineIdGen_key <> "" Then
            sql.AppendLine("    AND line_id_gen LIKE '%' + @line_id_gen + '%'")
        End If

        If lineIdSaki_key <> "" Then
            sql.AppendLine("    AND line_id_saki LIKE '%' + @line_id_saki + '%'")
        End If

        sql.AppendLine("ORDER BY")
        sql.AppendLine("    line_id_gen, line_id_saki")

        Dim paramList As New List(Of SqlParameter)
        If lineIdGen_key <> "" Then
            paramList.Add(New SqlParameter("@line_id_gen", lineIdGen_key))
        End If
        If lineIdSaki_key <> "" Then
            paramList.Add(New SqlParameter("@line_id_saki", lineIdSaki_key))
        End If
        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sql.ToString(), dsInfo, "dt", paramList.ToArray)
        Return dsInfo.Tables("dt")
    End Function

    ''' <summary>
    ''' 所有生产线权限表を更新する
    ''' </summary>
    '''<param name="lineIdGen_key">生产线ID（更新前）</param>
    '''<param name="lineIdGen">生产线ID（更新后）</param>
    '''<param name="menu_user">更新用户</param>
    ''' <returns>更新结果</returns>
    ''' <remarks></remarks>
    Public Function UpdMAllLinesKengen(ByVal lineIdGen_key As String,
            ByVal lineIdGen As String,
            ByVal menu_user As String) As Boolean

        Dim sql As New StringBuilder
        sql.AppendLine("UPDATE m_all_lines_kengen")
        sql.AppendLine("SET")
        sql.AppendLine("    line_id_gen = @line_id_gen")
        sql.AppendLine("WHERE")
        sql.AppendLine("    line_id_gen = @line_id_gen_key")

        Dim paramList As New List(Of SqlParameter)
        paramList.Add(New SqlParameter("@line_id_gen_key", lineIdGen_key))
        paramList.Add(New SqlParameter("@line_id_gen", lineIdGen))

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sql.ToString(), paramList.ToArray)
        Return True

    End Function

    ''' <summary>
    ''' 生产线参照表を更新する
    ''' </summary>
    '''<param name="lineIdGen_key">源生产线ID（更新前）</param>
    '''<param name="lineIdSaki_key">目标生产线ID（更新前）</param>
    '''<param name="lineIdGen">源生产线ID（更新后）</param>
    '''<param name="lineIdSaki">目标生产线ID（更新后）</param>
    '''<param name="menu_user">更新用户</param>
    ''' <returns>更新结果</returns>
    ''' <remarks></remarks>
    Public Function UpdMLineSansyou(ByVal lineIdGen_key As String,
            ByVal lineIdSaki_key As String,
            ByVal lineIdGen As String,
            ByVal lineIdSaki As String,
            ByVal menu_user As String) As Boolean

        Dim sql As New StringBuilder
        sql.AppendLine("UPDATE m_line_sansyou")
        sql.AppendLine("SET")
        sql.AppendLine("    line_id_gen = @line_id_gen,")
        sql.AppendLine("    line_id_saki = @line_id_saki")
        sql.AppendLine("WHERE")
        sql.AppendLine("    line_id_gen = @line_id_gen_key")
        sql.AppendLine("    AND line_id_saki = @line_id_saki_key")

        Dim paramList As New List(Of SqlParameter)
        paramList.Add(New SqlParameter("@line_id_gen_key", lineIdGen_key))
        paramList.Add(New SqlParameter("@line_id_saki_key", lineIdSaki_key))
        paramList.Add(New SqlParameter("@line_id_gen", lineIdGen))
        paramList.Add(New SqlParameter("@line_id_saki", lineIdSaki))

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sql.ToString(), paramList.ToArray)
        Return True
    End Function

    ''' <summary>
    ''' 所有生产线权限表を登録する
    ''' </summary>
    '''<param name="lineIdGen">生产线ID</param>
    '''<param name="menu_user">登録用户</param>
    ''' <returns>登録结果</returns>
    ''' <remarks></remarks>
    Public Function InsMAllLinesKengen(ByVal lineIdGen As String,
               ByVal menu_user As String) As Boolean

        Dim sql As New StringBuilder
        sql.AppendLine("INSERT INTO m_all_lines_kengen")
        sql.AppendLine("(")
        sql.AppendLine("    line_id_gen")
        sql.AppendLine(")")
        sql.AppendLine("VALUES")
        sql.AppendLine("(")
        sql.AppendLine("    @line_id_gen")
        sql.AppendLine(")")

        Dim paramList As New List(Of SqlParameter)
        paramList.Add(New SqlParameter("@line_id_gen", lineIdGen))

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sql.ToString(), paramList.ToArray)
        Return True
    End Function

    ''' <summary>
    ''' 生产线参照表を登録する
    ''' </summary>
    '''<param name="lineIdGen">源生产线ID</param>
    '''<param name="lineIdSaki">目标生产线ID</param>
    '''<param name="menu_user">登録用户</param>
    ''' <returns>登録结果</returns>
    ''' <remarks></remarks>
    Public Function InsMLineSansyou(ByVal lineIdGen As String,
               ByVal lineIdSaki As String,
               ByVal menu_user As String) As Boolean

        Dim sql As New StringBuilder
        sql.AppendLine("INSERT INTO m_line_sansyou")
        sql.AppendLine("(")
        sql.AppendLine("    line_id_gen,")
        sql.AppendLine("    line_id_saki")
        sql.AppendLine(")")
        sql.AppendLine("VALUES")
        sql.AppendLine("(")
        sql.AppendLine("    @line_id_gen,")
        sql.AppendLine("    @line_id_saki")
        sql.AppendLine(")")

        Dim paramList As New List(Of SqlParameter)
        paramList.Add(New SqlParameter("@line_id_gen", lineIdGen))
        paramList.Add(New SqlParameter("@line_id_saki", lineIdSaki))

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sql.ToString(), paramList.ToArray)
        Return True
    End Function

    ''' <summary>
    ''' 所有生产线权限表を削除する
    ''' </summary>
    '''<param name="lineIdGen_key">生产线ID</param>
    ''' <returns>削除结果</returns>
    ''' <remarks></remarks>
    Public Function DelMAllLinesKengen(ByVal lineIdGen_key As String) As Boolean

        Dim sql As New StringBuilder
        sql.AppendLine("DELETE FROM m_all_lines_kengen")
        sql.AppendLine("WHERE")
        sql.AppendLine("    line_id_gen = @line_id_gen_key")

        Dim paramList As New List(Of SqlParameter)
        paramList.Add(New SqlParameter("@line_id_gen_key", lineIdGen_key))

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sql.ToString(), paramList.ToArray)
        Return True
    End Function

    ''' <summary>
    ''' 生产线参照表を削除する
    ''' </summary>
    '''<param name="lineIdGen_key">源生产线ID</param>
    '''<param name="lineIdSaki_key">目标生产线ID</param>
    ''' <returns>削除结果</returns>
    ''' <remarks></remarks>
    Public Function DelMLineSansyou(ByVal lineIdGen_key As String, ByVal lineIdSaki_key As String) As Boolean

        Dim sql As New StringBuilder
        sql.AppendLine("DELETE FROM m_line_sansyou")
        sql.AppendLine("WHERE")
        sql.AppendLine("    line_id_gen = @line_id_gen_key")
        sql.AppendLine("    AND line_id_saki = @line_id_saki_key")

        Dim paramList As New List(Of SqlParameter)
        paramList.Add(New SqlParameter("@line_id_gen_key", lineIdGen_key))
        paramList.Add(New SqlParameter("@line_id_saki_key", lineIdSaki_key))

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sql.ToString(), paramList.ToArray)
        Return True
    End Function
End Class