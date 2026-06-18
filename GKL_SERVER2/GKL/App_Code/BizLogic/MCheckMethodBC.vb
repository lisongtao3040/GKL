Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class MCheckMethodBC
Public DA AS NEW MCheckMethodDA

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

Public Function SelMCheckMethod(Byval chkId_key AS String, _
           Byval chkName_key AS String) As Data.DataTable

    'SQLコメント
    Return DA.SelMCheckMethod( _
           chkId_key, _
           chkName_key)
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

Public Function UpdMCheckMethod(Byval chkId_key AS String, _
           Byval chkName_key AS String, _
           Byval chkId AS String, _
           Byval chkName AS String, _
           Byval chkMethod AS String, _
           Byval chkFormula AS String, _
           Byval verifyMethodExplain AS String) As Boolean

    'SQLコメント
    '--**テーブル：检查方法MS : m_check_method
    Return DA.UpdMCheckMethod( _
           chkId_key, _
           chkName_key, _
           chkId, _
           chkName, _
           chkMethod, _
           chkFormula, _
           verifyMethodExplain)

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

Public Function InsMCheckMethod(Byval chkId AS String, _
           Byval chkName AS String, _
           Byval chkMethod AS String, _
           Byval chkFormula AS String, _
           Byval verifyMethodExplain AS String) As Boolean

    'SQLコメント
    '--**テーブル：检查方法MS : m_check_method
    Return DA.InsMCheckMethod( _
           chkId, _
           chkName, _
           chkMethod, _
           chkFormula, _
           verifyMethodExplain)

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

Public Function DelMCheckMethod(Byval chkId_key AS String, _
           Byval chkName_key AS String) As Boolean

    'SQLコメント
    '--**テーブル：检查方法MS : m_check_method
    Return DA.DelMCheckMethod( _
           chkId_key, _
           chkName_key)


End Function

End Class
