



Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MPictureDA

    Public SqlHelperNew As New SqlHelperNew

    ''' <summary>
    ''' 
    ''' Infoを検索する
    ''' </summary>
    '''<param name="picId_key">pic_id</param>
    '''<param name="lineId_key">line_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function SelMPicture(ByVal picId_key As String, _
           ByVal lineId_key As String) As Data.DataTable

        'SQLコメント
        '--**テーブル： : m_picture
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("pic_id")                                                    'pic_id
        sb.AppendLine(", line_id")                                                 'line_id
        sb.AppendLine(", pic_name")                                                'pic_name

        sb.AppendLine("FROM m_picture")
        sb.AppendLine("WHERE 1=1")
        If picId_key <> "" Then
            sb.AppendLine("AND pic_id=@pic_id_key")   'pic_id
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   'line_id
        End If
        sb.AppendLine("ORDER BY line_id,pic_id")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@pic_id_key", SqlDbType.VarChar, 10, picId_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_picture", paramList.ToArray)

        Return dsInfo.Tables("m_picture")

    End Function


    Public Function GetLineListPic(ByVal picId_key As String, ByVal lineId_key As String) As Object

        '--**テーブル： : m_picture
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("pic_id ")                                                    'pic_id
        sb.AppendLine(", line_id ")                                                 'line_id
        sb.AppendLine(", pic_name ")                                                'pic_name
        sb.AppendLine(", pic_conn ")
        sb.AppendLine("FROM m_picture")
        sb.AppendLine("WHERE 1=1")
        If picId_key <> "" Then
            sb.AppendLine("AND pic_id='" & picId_key & "'")   'pic_id
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id='" & lineId_key & "'")   'line_id
        End If
        sb.AppendLine("ORDER BY line_id,pic_id")
        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "m_picture")
        Return dsInfo.Tables(0).Rows(0).Item("pic_conn")

    End Function

    ''' <summary>
    ''' 
    ''' Infoを更新する
    ''' </summary>
    '''<param name="picId_key">pic_id</param>
    '''<param name="lineId_key">line_id</param>
    '''<param name="picId">pic_id</param>
    '''<param name="lineId">line_id</param>
    '''<param name="picName">pic_name</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function UpdMPicture(ByVal picId_key As String, _
               ByVal lineId_key As String, _
               ByVal picId As String, _
               ByVal lineId As String, _
               ByVal picName As String) As Boolean

        'SQLコメント
        '--**テーブル： : m_picture
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("UPDATE m_picture")
        sb.AppendLine("SET")
        sb.AppendLine("pic_id=@pic_id")                                                'pic_id
        sb.AppendLine(", line_id=@line_id")                                            'line_id
        sb.AppendLine(", pic_name=@pic_name")   'pic_name

        sb.AppendLine("FROM m_picture")
        sb.AppendLine("WHERE 1=1")
        If picId_key <> "" Then
            sb.AppendLine("AND pic_id=@pic_id_key")   'pic_id
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   'line_id
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@pic_id_key", SqlDbType.VarChar, 10, picId_key))
        paramList.Add(SqlHelperNew.MakeParam("@line_id_key", SqlDbType.VarChar, 10, lineId_key))

        paramList.Add(SqlHelperNew.MakeParam("@pic_id", SqlDbType.VarChar, 10, picId))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@pic_name", SqlDbType.nvarchar, 200, picName))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' Infoを登録する
    ''' </summary>
    '''<param name="picId">pic_id</param>
    '''<param name="lineId">line_id</param>
    '''<param name="picName">pic_name</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function InsMPicture(ByVal picId As String, _
               ByVal lineId As String, _
               ByVal picName As String) As Boolean

        'SQLコメント
        '--**テーブル： : m_picture
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("INSERT INTO  m_picture")
        sb.AppendLine("(")
        sb.AppendLine("pic_id")                                                    'pic_id
        sb.AppendLine(", line_id")                                                 'line_id
        sb.AppendLine(", pic_name")                                                'pic_name

        sb.AppendLine(")")
        sb.AppendLine("VALUES(")
        sb.AppendLine("@pic_id")                                                       'pic_id
        sb.AppendLine(", @line_id")                                                    'line_id
        sb.AppendLine(", @pic_name")                                                   'pic_name

        sb.AppendLine(")")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@pic_id", SqlDbType.VarChar, 10, picId))
        paramList.Add(SqlHelperNew.MakeParam("@line_id", SqlDbType.VarChar, 10, lineId))
        paramList.Add(SqlHelperNew.MakeParam("@pic_name", SqlDbType.nvarchar, 200, picName))


        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function

    ''' <summary>
    ''' 
    ''' Infoを削除する
    ''' </summary>
    '''<param name="picId_key">pic_id</param>
    '''<param name="lineId_key">line_id</param>
    ''' <returns>Info</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2019/01/07  作成者：李さん 新規作成 </para>
    ''' </history>

    Public Function DelMPicture(ByVal picId_key As String, _
               ByVal lineId_key As String) As Boolean

        'SQLコメント
        '--**テーブル： : m_picture
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("DELETE FROM m_picture")
        sb.AppendLine("WHERE 1=1")
        If picId_key <> "" Then
            sb.AppendLine("AND pic_id=@pic_id_key")   'pic_id
        End If
        If lineId_key <> "" Then
            sb.AppendLine("AND line_id=@line_id_key")   'line_id
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)
        paramList.Add(SqlHelperNew.MakeParam("@pic_id_key", SqlDbType.VarChar, 10, picId_key))
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
