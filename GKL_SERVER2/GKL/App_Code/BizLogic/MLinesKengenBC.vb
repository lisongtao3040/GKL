Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MLinesKengenBC

    Public DA As New MLinesKengenDA

    ''' <summary>
    ''' 所有生产线权限表を検索する
    ''' </summary>
    '''<param name="lineIdGen_key">生产线ID</param>
    ''' <returns>所有生产线权限表</returns>
    ''' <remarks></remarks>
    Public Function SelMAllLinesKengen(ByVal lineIdGen_key As String) As Data.DataTable
        Return DA.SelMAllLinesKengen(lineIdGen_key)
    End Function

    ''' <summary>
    ''' 生产线参照表を検索する
    ''' </summary>
    '''<param name="lineIdGen_key">源生产线ID</param>
    '''<param name="lineIdSaki_key">目标生产线ID</param>
    ''' <returns>生产线参照表</returns>
    ''' <remarks></remarks>
    Public Function SelMLineSansyou(ByVal lineIdGen_key As String, ByVal lineIdSaki_key As String) As Data.DataTable
        Return DA.SelMLineSansyou(lineIdGen_key, lineIdSaki_key)
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
        Return DA.UpdMAllLinesKengen(
            lineIdGen_key,
            lineIdGen,
            menu_user)
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
        Return DA.UpdMLineSansyou(
            lineIdGen_key,
            lineIdSaki_key,
            lineIdGen,
            lineIdSaki,
            menu_user)
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
        Return DA.InsMAllLinesKengen(
               lineIdGen,
               menu_user)
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
        Return DA.InsMLineSansyou(
               lineIdGen,
               lineIdSaki,
               menu_user)
    End Function

    ''' <summary>
    ''' 所有生产线权限表を削除する
    ''' </summary>
    '''<param name="lineIdGen_key">生产线ID</param>
    ''' <returns>削除结果</returns>
    ''' <remarks></remarks>
    Public Function DelMAllLinesKengen(ByVal lineIdGen_key As String) As Boolean
        Return DA.DelMAllLinesKengen(
               lineIdGen_key)
    End Function

    ''' <summary>
    ''' 生产线参照表を削除する
    ''' </summary>
    '''<param name="lineIdGen_key">源生产线ID</param>
    '''<param name="lineIdSaki_key">目标生产线ID</param>
    ''' <returns>削除结果</returns>
    ''' <remarks></remarks>
    Public Function DelMLineSansyou(ByVal lineIdGen_key As String, ByVal lineIdSaki_key As String) As Boolean
        Return DA.DelMLineSansyou(
               lineIdGen_key,
               lineIdSaki_key)
    End Function
End Class