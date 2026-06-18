Imports System
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports Newtonsoft.Json

''' <summary>
''' 生产线权限管理 Web服务
''' </summary>
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<System.Web.Script.Services.ScriptService()> _
Public Class lines_kengen
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' 响应结果类
    ''' </summary>
    Public Class ResponseResult
        Public Property success As Boolean
        Public Property message As String
        Public Property data As Object
        
        Public Sub New()
            success = False
            message = ""
            data = Nothing
        End Sub
        
        Public Sub New(success As Boolean, message As String, data As Object)
            Me.success = success
            Me.message = message
            Me.data = data
        End Sub
    End Class

    ''' <summary>
    ''' 获取所有生产线权限
    ''' </summary>
    <WebMethod()> _
    Public Function GetAllLines(searchText As String) As ResponseResult
        Try
            Dim dt As DataTable = GetLinesFromDB(searchText)
            Return New ResponseResult(True, "获取成功", dt)
        Catch ex As Exception
            Return New ResponseResult(False, "获取失败: " & ex.Message, Nothing)
        End Try
    End Function

    ''' <summary>
    ''' 新增生产线权限
    ''' </summary>
    <WebMethod()> _
    Public Function InsertLine(lineId As String) As ResponseResult
        Try
            If String.IsNullOrEmpty(lineId) Then
                Return New ResponseResult(False, "生产线ID不能为空", Nothing)
            End If
            
            If lineId.Length > 10 Then
                Return New ResponseResult(False, "生产线ID不能超过10个字符", Nothing)
            End If
            
            ' 检查是否已存在
            If CheckLineExists(lineId) Then
                Return New ResponseResult(False, "生产线ID已存在", Nothing)
            End If
            
            Dim result As Boolean = InsertLineToDB(lineId)
            If result Then
                Return New ResponseResult(True, "新增成功", Nothing)
            Else
                Return New ResponseResult(False, "新增失败", Nothing)
            End If
        Catch ex As Exception
            Return New ResponseResult(False, "新增失败: " & ex.Message, Nothing)
        End Try
    End Function

    ''' <summary>
    ''' 更新生产线权限
    ''' </summary>
    <WebMethod()> _
    Public Function UpdateLine(oldLineId As String, newLineId As String) As ResponseResult
        Try
            If String.IsNullOrEmpty(oldLineId) OrElse String.IsNullOrEmpty(newLineId) Then
                Return New ResponseResult(False, "生产线ID不能为空", Nothing)
            End If
            
            If newLineId.Length > 10 Then
                Return New ResponseResult(False, "生产线ID不能超过10个字符", Nothing)
            End If
            
            ' 检查原生产线是否存在
            If Not CheckLineExists(oldLineId) Then
                Return New ResponseResult(False, "原生产线ID不存在", Nothing)
            End If
            
            ' 检查新生产线是否已存在（如果不同）
            If oldLineId <> newLineId AndAlso CheckLineExists(newLineId) Then
                Return New ResponseResult(False, "新生产线ID已存在", Nothing)
            End If
            
            Dim result As Boolean = UpdateLineInDB(oldLineId, newLineId)
            If result Then
                Return New ResponseResult(True, "更新成功", Nothing)
            Else
                Return New ResponseResult(False, "更新失败", Nothing)
            End If
        Catch ex As Exception
            Return New ResponseResult(False, "更新失败: " & ex.Message, Nothing)
        End Try
    End Function

    ''' <summary>
    ''' 删除生产线权限
    ''' </summary>
    <WebMethod()> _
    Public Function DeleteLine(lineId As String) As ResponseResult
        Try
            If String.IsNullOrEmpty(lineId) Then
                Return New ResponseResult(False, "生产线ID不能为空", Nothing)
            End If
            
            ' 检查生产线是否存在
            If Not CheckLineExists(lineId) Then
                Return New ResponseResult(False, "生产线ID不存在", Nothing)
            End If
            
            Dim result As Boolean = DeleteLineFromDB(lineId)
            If result Then
                Return New ResponseResult(True, "删除成功", Nothing)
            Else
                Return New ResponseResult(False, "删除失败", Nothing)
            End If
        Catch ex As Exception
            Return New ResponseResult(False, "删除失败: " & ex.Message, Nothing)
        End Try
    End Function

    ''' <summary>
    ''' 从数据库获取生产线权限
    ''' </summary>
    Private Function GetLinesFromDB(searchText As String) As DataTable
        Dim dt As New DataTable()
        Dim connStr As String = DataAccessManager.Connection
        
        Using conn As New SqlConnection(connStr)
            Dim sql As String = "SELECT line_id_gen FROM [dbo].[m_all_lines_kengen]"
            
            If Not String.IsNullOrEmpty(searchText) Then
                sql &= " WHERE line_id_gen LIKE @searchText"
            End If
            
            sql &= " ORDER BY line_id_gen"
            
            Using cmd As New SqlCommand(sql, conn)
                If Not String.IsNullOrEmpty(searchText) Then
                    cmd.Parameters.AddWithValue("@searchText", "%" & searchText & "%")
                End If
                
                conn.Open()
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        
        Return dt
    End Function

    ''' <summary>
    ''' 检查生产线是否存在
    ''' </summary>
    Private Function CheckLineExists(lineId As String) As Boolean
        Dim exists As Boolean = False
        Dim connStr As String = DataAccessManager.Connection
        
        Using conn As New SqlConnection(connStr)
            Dim sql As String = "SELECT COUNT(*) FROM [dbo].[m_all_lines_kengen] WHERE line_id_gen = @lineId"
            
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@lineId", lineId)
                
                conn.Open()
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                exists = (count > 0)
            End Using
        End Using
        
        Return exists
    End Function

    ''' <summary>
    ''' 插入生产线到数据库
    ''' </summary>
    Private Function InsertLineToDB(lineId As String) As Boolean
        Dim result As Boolean = False
        Dim connStr As String = DataAccessManager.Connection
        
        Using conn As New SqlConnection(connStr)
            Dim sql As String = "INSERT INTO [dbo].[m_all_lines_kengen] (line_id_gen) VALUES (@lineId)"
            
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@lineId", lineId)
                
                conn.Open()
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                result = (rowsAffected > 0)
            End Using
        End Using
        
        Return result
    End Function

    ''' <summary>
    ''' 更新数据库中的生产线
    ''' </summary>
    Private Function UpdateLineInDB(oldLineId As String, newLineId As String) As Boolean
        Dim result As Boolean = False
        Dim connStr As String = DataAccessManager.Connection
        
        Using conn As New SqlConnection(connStr)
            Dim sql As String = "UPDATE [dbo].[m_all_lines_kengen] SET line_id_gen = @newLineId WHERE line_id_gen = @oldLineId"
            
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@newLineId", newLineId)
                cmd.Parameters.AddWithValue("@oldLineId", oldLineId)
                
                conn.Open()
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                result = (rowsAffected > 0)
            End Using
        End Using
        
        Return result
    End Function

    ''' <summary>
    ''' 从数据库删除生产线
    ''' </summary>
    Private Function DeleteLineFromDB(lineId As String) As Boolean
        Dim result As Boolean = False
        Dim connStr As String = DataAccessManager.Connection
        
        Using conn As New SqlConnection(connStr)
            Dim sql As String = "DELETE FROM [dbo].[m_all_lines_kengen] WHERE line_id_gen = @lineId"
            
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@lineId", lineId)
                
                conn.Open()
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                result = (rowsAffected > 0)
            End Using
        End Using
        
        Return result
    End Function

End Class