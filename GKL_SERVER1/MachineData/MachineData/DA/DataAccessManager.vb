Imports Microsoft.VisualBasic

Public Class DataAccessManager

    Public Shared connStr As String = "Data Source=ot2414;Initial Catalog=LIS_DB;Persist Security Info=True;user=sa;password=lixil@2014"

    ''' <summary>
    ''' DB接续文字取得
    ''' </summary>
    ''' <returns>DB接続文字列</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Connection() As String
        Get
            Return connStr
        End Get
    End Property

End Class
