
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions
Imports System.Configuration.ConfigurationSettings
Imports System.Collections.Generic

Public Class BaoGongDA2

    Public SqlHelperNew As New SqlHelperNew

    'Public Function SelBgList(ByVal YMD As String, ByVal lineid As String) As Data.DataTable

    '    'SQLコメント
    '    '--**テーブル：检查计划 : t_check_plan
    '    Dim sb As New StringBuilder
    '    'SQL文

    '    sb.AppendLine("SELECT ")
    '    sb.AppendLine("*")
    '    sb.AppendLine("FROM [v_bg_list_new]")
    '    sb.AppendLine("WHERE [yotei_chk_date] = '" & YMD & " 00:00:00.000'")

    '    'sb.AppendLine("AND ")
    '    If (Right(lineid, 1) = "A") Then
    '        sb.AppendLine("AND 'SRM1'+[line_cd]+'A'='" & lineid & "'")   '计划No
    '    ElseIf (Right(lineid, 1) = "B") Then
    '        sb.AppendLine("AND 'SRM1'+[line_cd]+'B'='" & lineid & "'")   '计划No
    '    End If

    '    sb.AppendLine("AND [line_cd]='" & lineid & "'")   '计划No

    '    sb.AppendLine("ORDER BY ZuoFan")


    '    'PARAM
    '    Dim paramList As New List(Of SqlParameter)



    '    Dim dsInfo As New Data.DataSet
    '    SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "SelBgList", paramList.ToArray)

    '    Return dsInfo.Tables("SelBgList")

    'End Function


    Public Function SelBgListByCd(ByVal cd As String, ByVal no As String, ByVal lineid As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文

        'sb.AppendLine("SELECT ")
        'sb.AppendLine("*")
        'sb.AppendLine("FROM [v_bg_list_new]")
        'sb.AppendLine("WHERE [ProductCode] = '" & cd & "'")
        'sb.AppendLine("AND [ZuoFan] = '" & no & "'")

        'If (Right(lineid, 1) = "A") Then
        '    sb.AppendLine("AND 'SRM1'+[line_cd]+'A'='" & lineid & "'")   '计划No
        'ElseIf (Right(lineid, 1) = "B") Then
        '    sb.AppendLine("AND 'SRM1'+[line_cd]+'B'='" & lineid & "'")   '计划No
        'End If
        If lineid.Length = 3 Then
            lineid = lineid & "D"
        End If
        '与 t_checkDA的 SelTCheckResultOkSuu 相同
        sb.AppendLine("SELECT")
        sb.AppendLine("*")                                                    '检查No
        sb.AppendLine("FROM v_bg_list_new")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND [ZuoFan]='" & no & "'")   '检查No
        sb.AppendLine("AND [ProductCode]='" & cd & "'")   '年
        'sb.AppendLine("AND line_cd ='" & lineid & "'")   '计划No

        If (Right(lineid, 1) = "A") Then
            sb.AppendLine("AND 'SRM1'+[line_cd]+'A'='" & lineid & "'")   '计划No
        ElseIf (Right(lineid, 1) = "B") Then
            sb.AppendLine("AND 'SRM1'+[line_cd]+'B'='" & lineid & "'")   '计划No
        End If

        'PARAM
        Dim paramList As New List(Of SqlParameter)

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "SelBgList", paramList.ToArray)

        Return dsInfo.Tables("SelBgList")

    End Function


    Public Function SelListData(ByVal cd As String, ByVal no As String) As Data.DataTable

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文

        sb.AppendLine("SELECT ")
        sb.AppendLine("*")
        sb.AppendLine("FROM [m_baogong_list_new]")
        sb.AppendLine("WHERE [cd] = '" & cd & "'")
        sb.AppendLine("AND [make_no] = '" & no & "'")

        'PARAM
        Dim paramList As New List(Of SqlParameter)

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "SelBgList", paramList.ToArray)

        Return dsInfo.Tables(0)

    End Function


    Public Function NewTpNo(ByVal cd As String, ByVal no As String) As Integer

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文

        sb.AppendLine("SELECT ")
        sb.AppendLine(" isnull(max(tp_no),0)+1")
        sb.AppendLine("FROM [m_baogong_ms_new]")
        sb.AppendLine("WHERE [cd] = '" & cd & "'")
        sb.AppendLine("AND [make_no] = '" & no & "'")

        'PARAM
        Dim paramList As New List(Of SqlParameter)

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "SelBgList", paramList.ToArray)

        Return CInt(dsInfo.Tables(0).Rows(0).Item(0).ToString)

    End Function

    Public Function InsListData(ByVal cd As String, ByVal no As String, ByVal user As String, ByVal suu As Integer, ByVal tp_nyu_suu As Integer, ByVal lineid As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine(" IF NOT EXISTS")
        sb.AppendLine(" (")
        sb.AppendLine("     SELECT 1")
        sb.AppendLine("     FROM m_baogong_list_new")
        sb.AppendLine("     WHERE [cd] = '" & cd & "'")
        sb.AppendLine("     AND [make_no] = '" & no & "'")
        sb.AppendLine(" )")
        sb.AppendLine(" BEGIN")

        sb.AppendLine("INSERT INTO [dbo].[m_baogong_list_new]")
        sb.AppendLine("           ([make_no]")
        sb.AppendLine("           ,[cd]")
        sb.AppendLine("           ,[sap_cd]")
        sb.AppendLine("           ,[plan_suu]") '总数
        sb.AppendLine("           ,[kb_nyu_suu]")
        sb.AppendLine("           ,[kb_suu]")
        sb.AppendLine("           ,[tp_nyu_suu]") '托盘入数
        sb.AppendLine("           ,[tp_suu]") '托盘数
        sb.AppendLine("           ,[xiangxian]")
        sb.AppendLine("           ,[kuwei]")
        sb.AppendLine("           ,[chk_ok_suu]")
        sb.AppendLine("           ,[result]")
        sb.AppendLine("           ,[mark1]")
        sb.AppendLine("           ,[mark2]")
        sb.AppendLine("           ,[mark3]")
        sb.AppendLine("           ,[deleteFlag]")
        sb.AppendLine("           ,[insertUser]")
        sb.AppendLine("           ,[insertDate]")
        sb.AppendLine("           ,[updateUser]")
        sb.AppendLine("           ,[updateDate])")
        sb.AppendLine("SELECT ")
        sb.AppendLine("           ZuoFan")
        sb.AppendLine("           ,ProductCode")
        sb.AppendLine("           ,ProductCodeSap")
        sb.AppendLine("           ,suu")
        sb.AppendLine("           ,PackageAmount")
        sb.AppendLine("           ,Package")
        sb.AppendLine("           ,tuopan_syu_suu")             '每个托盘最大装几个
        sb.AppendLine("           ,case when tuopan_syu_suu=0 then 1 else CEILING(suu/tuopan_syu_suu) end") '托盘数
        sb.AppendLine("           ,DestinationCode")
        sb.AppendLine("           ,localStorage")
        sb.AppendLine("           ,0")
        sb.AppendLine("           ,null")
        sb.AppendLine("           ,Comments")
        sb.AppendLine("           ,remark")
        sb.AppendLine("           ,''")
        sb.AppendLine("           ,'0'")
        sb.AppendLine("           ,'" & user & "'")
        sb.AppendLine("           ,getdate()")
        sb.AppendLine("           ,'" & user & "'")
        sb.AppendLine("           ,getdate()")
        sb.AppendLine("FROM [v_bg_list_new]")
        sb.AppendLine("WHERE [ProductCode] = '" & cd & "'")
        sb.AppendLine("AND [ZuoFan] = '" & no & "'")
        'sb.AppendLine("AND SUBSTRING(line_cd,1,3) ='" & Left(lineid, 3) & "'")   '计划No


        If (Right(lineid, 1) = "A") Then
            sb.AppendLine("AND 'SRM1'+[line_cd]+'A'='" & lineid & "'")   '计划No
        ElseIf (Right(lineid, 1) = "B") Then
            sb.AppendLine("AND 'SRM1'+[line_cd]+'B'='" & lineid & "'")   '计划No
        End If



        sb.AppendLine("END")



        'PARAM
        Dim paramList As New List(Of SqlParameter)

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)


        Return True

    End Function

    'Public Function InsListData983(ByVal cd As String, ByVal no As String, ByVal user As String, ByVal suu As Integer, ByVal tp_nyu_suu As Integer, ByVal lineid As String) As Boolean

    '    'SQLコメント
    '    '--**テーブル：检查计划 : t_check_plan
    '    Dim sb As New StringBuilder
    '    'SQL文
    '    sb.AppendLine(" IF NOT EXISTS")
    '    sb.AppendLine(" (")
    '    sb.AppendLine("     SELECT 1")
    '    sb.AppendLine("     FROM m_baogong_list_new")
    '    sb.AppendLine("     WHERE [cd] = '" & cd & "'")
    '    sb.AppendLine("     AND [make_no] = '" & no & "'")
    '    sb.AppendLine(" )")
    '    sb.AppendLine(" BEGIN")

    '    sb.AppendLine("INSERT INTO [dbo].[m_baogong_list_new]")
    '    sb.AppendLine("           ([make_no]")
    '    sb.AppendLine("           ,[cd]")
    '    sb.AppendLine("           ,[sap_cd]")
    '    sb.AppendLine("           ,[plan_suu]") '总数
    '    sb.AppendLine("           ,[kb_nyu_suu]")
    '    sb.AppendLine("           ,[kb_suu]")
    '    sb.AppendLine("           ,[tp_nyu_suu]") '托盘入数
    '    sb.AppendLine("           ,[tp_suu]") '托盘数
    '    sb.AppendLine("           ,[xiangxian]")
    '    sb.AppendLine("           ,[kuwei]")
    '    sb.AppendLine("           ,[chk_ok_suu]")
    '    sb.AppendLine("           ,[result]")
    '    sb.AppendLine("           ,[mark1]")
    '    sb.AppendLine("           ,[mark2]")
    '    sb.AppendLine("           ,[mark3]")
    '    sb.AppendLine("           ,[deleteFlag]")
    '    sb.AppendLine("           ,[insertUser]")
    '    sb.AppendLine("           ,[insertDate]")
    '    sb.AppendLine("           ,[updateUser]")
    '    sb.AppendLine("           ,[updateDate])")
    '    sb.AppendLine("SELECT ")
    '    sb.AppendLine("           ZuoFan")
    '    sb.AppendLine("           ,ProductCode")
    '    sb.AppendLine("           ,ProductCodeSap")
    '    sb.AppendLine("           ,suu")
    '    sb.AppendLine("           ,PackageAmount")
    '    sb.AppendLine("           ,Package")
    '    sb.AppendLine("           ,tuopan_syu_suu")             '每个托盘最大装几个
    '    sb.AppendLine("           ,CEILING(suu/tuopan_syu_suu)") '托盘数
    '    sb.AppendLine("           ,DestinationCode")
    '    sb.AppendLine("           ,localStorage")
    '    sb.AppendLine("           ,0")
    '    sb.AppendLine("           ,null")
    '    sb.AppendLine("           ,Comments")
    '    sb.AppendLine("           ,remark")
    '    sb.AppendLine("           ,''")
    '    sb.AppendLine("           ,'0'")
    '    sb.AppendLine("           ,'" & user & "'")
    '    sb.AppendLine("           ,getdate()")
    '    sb.AppendLine("           ,'" & user & "'")
    '    sb.AppendLine("           ,getdate()")
    '    sb.AppendLine("FROM [v_bg_list_new]")
    '    sb.AppendLine("WHERE [ProductCode] = '" & cd & "'")
    '    sb.AppendLine("AND [ZuoFan] = '" & no & "'")
    '    sb.AppendLine("AND SUBSTRING(line_cd,1,3) ='" & Left(lineid, 3) & "'")   '计划No

    '    sb.AppendLine("END")

    '    'If (Right(lineid, 1) = "A") Then
    '    '    sb.AppendLine("AND 'SRM1'+[line_cd]+'A'='" & lineid & "'")   '计划No
    '    'ElseIf (Right(lineid, 1) = "B") Then
    '    '    sb.AppendLine("AND 'SRM1'+[line_cd]+'B'='" & lineid & "'")   '计划No
    '    'End If


    '    'PARAM
    '    Dim paramList As New List(Of SqlParameter)

    '    SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)


    '    Return True

    'End Function




    Public Function InsMSOneRowData(ByVal cd As String, ByVal no As String, ByVal user As String,
                                    ByVal suu As Integer, ByVal tp_nyu_suu As Integer, ByVal lineid As String,
                                    ByVal tp_no As Integer, ByVal bg_suu As Integer, ByVal tp_bar_cd As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder

        Dim dt As DataTable = SelBgListByCd(cd, no, lineid)


        Dim tmpSuu As Integer = suu
        'For i As Integer = 0 To Math.Ceiling(suu / tp_nyu_suu) - 1
        sb.AppendLine("INSERT INTO [dbo].[m_baogong_ms_new]")
        sb.AppendLine("           ([make_no]")
        sb.AppendLine("           ,[cd]")
        sb.AppendLine("           ,[tp_no]")
        sb.AppendLine("           ,[tp_bar_cd]")
        sb.AppendLine("           ,[cnt]")
        sb.AppendLine("           ,[bg_bar_data]")
        sb.AppendLine("           ,[bg_suu]")
        sb.AppendLine("           ,[bg_result]")
        sb.AppendLine("           ,[bg_txt]")
        sb.AppendLine("           ,[bg_user]")
        sb.AppendLine("           ,[bg_type]")
        sb.AppendLine("           ,[mark1]")
        sb.AppendLine("           ,[mark2]")
        sb.AppendLine("           ,[mark3]")
        sb.AppendLine("           ,[deleteFlag]")
        sb.AppendLine("           ,[insertUser]")
        sb.AppendLine("           ,[insertDate]")
        sb.AppendLine("           ,[updateUser]")
        sb.AppendLine("           ,[updateDate])")
        sb.AppendLine("     VALUES(")
        sb.AppendLine("           '" & dt.Rows(0).Item("ZuoFan") & "'")
        sb.AppendLine("           ,'" & dt.Rows(0).Item("ProductCode") & "'")
        sb.AppendLine("           ," & tp_no.ToString & "")
        sb.AppendLine("           ,'" & tp_bar_cd.ToString & "'")
        sb.AppendLine("           ,0")

        'Dim bg_suu As String
        'If suu < tp_nyu_suu Then
        '    bg_suu = suu
        'ElseIf tmpSuu - tp_nyu_suu >= 0 Then

        '    bg_suu = tp_nyu_suu
        'Else

        '    bg_suu = tmpSuu Mod tp_nyu_suu
        'End If


        sb.AppendLine("           ,'" & dt.Rows(0).Item("ProductCodeSap") & "/" & dt.Rows(0).Item("ProductCode") & "/" & bg_suu & "/" & CInt(Math.Ceiling(bg_suu / CInt(dt.Rows(0).Item("Package")))) & "/" & dt.Rows(0).Item("localStorage") & "/" & dt.Rows(0).Item("DestinationCode") &
                      "/" & tp_no.ToString & "/" & dt.Rows(0).Item("ZuoFan") & "'")

        'If suu < tp_nyu_suu Then
        '    sb.AppendLine("           ," & suu & "")
        'ElseIf tmpSuu - tp_nyu_suu >= 0 Then
        '    sb.AppendLine("           ," & tp_nyu_suu & "")
        'Else
        '    sb.AppendLine("           ," & tmpSuu Mod tp_nyu_suu & "")
        'End If

        sb.AppendLine("           ," & bg_suu & "")

        'tmpSuu = tmpSuu - tp_nyu_suu

        sb.AppendLine("           ,''")
        sb.AppendLine("           ,''")
        sb.AppendLine("           ,'" & user & "'")
        sb.AppendLine("           ,N'手动'")
        sb.AppendLine("           ,''")
        sb.AppendLine("           ,''")
        sb.AppendLine("           ,''")
        sb.AppendLine("           ,'0'")
        sb.AppendLine("           ,'" & user & "'")
        sb.AppendLine("           ,getdate()")
        sb.AppendLine("           ,''")
        sb.AppendLine("           ,null")
        sb.AppendLine("           )")
        'Next


        'PARAM
        Dim paramList As New List(Of SqlParameter)

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function


    'Public Function UpdMSOneRowData(ByVal cd As String, ByVal no As String,
    '                                ByVal tp_no As Integer, ByVal tp_bar_cd As String) As Boolean

    '    'SQLコメント
    '    '--**テーブル：检查计划 : t_check_plan
    '    Dim sb As New StringBuilder
    '    sb.AppendLine("UPDATE [dbo].[m_baogong_ms_new] SET")
    '    sb.AppendLine("           [tp_bar_cd] = '" & tp_bar_cd.ToString & "'")
    '    sb.AppendLine("WHERE 1=1")
    '    sb.AppendLine("      AND [make_no] = '" & no & "'")
    '    sb.AppendLine("      AND [cd] = '" & cd & "'")
    '    sb.AppendLine("      AND [tp_no] = '" & tp_no & "'")
    '    Dim paramList As New List(Of SqlParameter)
    '    SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)
    '    Return True

    'End Function


    Public Function InsMSData(ByVal cd As String, ByVal no As String, ByVal user As String, ByVal suu As Integer, ByVal tp_nyu_suu As Integer, ByVal lineid As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder

        Dim dt As DataTable = SelBgListByCd(cd, no, lineid)


        Dim tmpSuu As Integer = suu
        For i As Integer = 0 To Math.Ceiling(suu / tp_nyu_suu) - 1
            sb.AppendLine("INSERT INTO [dbo].[m_baogong_ms_new]")
            sb.AppendLine("           ([make_no]")
            sb.AppendLine("           ,[cd]")
            sb.AppendLine("           ,[tp_no]")
            sb.AppendLine("           ,[cnt]") '默认：0  '好像是报工次数
            sb.AppendLine("           ,[bg_bar_data]")  'WYY-AF32HR-MGNM/YY-AF32HR-MGNM/6/1/4101/AX94/1/9007858617
            sb.AppendLine("           ,[bg_suu]")       '本次报工数量
            sb.AppendLine("           ,[bg_result]")
            sb.AppendLine("           ,[bg_txt]")
            sb.AppendLine("           ,[bg_user]")
            sb.AppendLine("           ,[bg_type]")
            sb.AppendLine("           ,[mark1]")
            sb.AppendLine("           ,[mark2]")
            sb.AppendLine("           ,[mark3]")
            sb.AppendLine("           ,[deleteFlag]")
            sb.AppendLine("           ,[insertUser]")
            sb.AppendLine("           ,[insertDate]")
            sb.AppendLine("           ,[updateUser]")
            sb.AppendLine("           ,[updateDate])")
            sb.AppendLine("     VALUES(")
            sb.AppendLine("           '" & dt.Rows(0).Item("ZuoFan") & "'")
            sb.AppendLine("           ,'" & dt.Rows(0).Item("ProductCode") & "'")
            sb.AppendLine("           ," & (i + 1).ToString & "")
            sb.AppendLine("           ,0")

            Dim bg_suu As String
            If suu < tp_nyu_suu Then
                bg_suu = suu
            ElseIf tmpSuu - tp_nyu_suu >= 0 Then

                bg_suu = tp_nyu_suu
            Else

                bg_suu = tmpSuu Mod tp_nyu_suu
            End If


            sb.AppendLine("           ,'" & dt.Rows(0).Item("ProductCodeSap") & "/" & dt.Rows(0).Item("ProductCode") & "/" & bg_suu & "/" & CInt(Math.Ceiling(bg_suu / CInt(dt.Rows(0).Item("Package")))) & "/" & dt.Rows(0).Item("localStorage") & "/" & dt.Rows(0).Item("DestinationCode") & "/" & (i + 1) & "/" & dt.Rows(0).Item("ZuoFan") & "'")

            If suu < tp_nyu_suu Then
                sb.AppendLine("           ," & suu & "")
            ElseIf tmpSuu - tp_nyu_suu >= 0 Then
                sb.AppendLine("           ," & tp_nyu_suu & "")
            Else
                sb.AppendLine("           ," & tmpSuu Mod tp_nyu_suu & "")
            End If


            tmpSuu = tmpSuu - tp_nyu_suu

            sb.AppendLine("           ,''")
            sb.AppendLine("           ,''")
            sb.AppendLine("           ,'" & user & "'")
            sb.AppendLine("           ,N'手动'")
            sb.AppendLine("           ,''")
            sb.AppendLine("           ,''")
            sb.AppendLine("           ,''")
            sb.AppendLine("           ,'0'")
            sb.AppendLine("           ,'" & user & "'")
            sb.AppendLine("           ,getdate()")
            sb.AppendLine("           ,''")
            sb.AppendLine("           ,null")
            sb.AppendLine("           )")
        Next


        'PARAM
        Dim paramList As New List(Of SqlParameter)

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True
    End Function


    '983生产线自己一套一套报工
    'Public Function InsMSData983(ByVal cd As String, ByVal no As String, ByVal user As String, ByVal suu As Integer, ByVal tp_nyu_suu As Integer, ByVal lineid As String) As Boolean

    '    'SQLコメント
    '    '--**テーブル：检查计划 : t_check_plan
    '    Dim sb As New StringBuilder

    '    Dim dt As DataTable = SelBgListByCd(cd, no, lineid)

    '    Dim tmpSuu As Integer = suu
    '    '983生产线自己一套一套报工
    '    For i As Integer = 0 To Math.Ceiling(suu) - 1
    '        sb.AppendLine("INSERT INTO [dbo].[m_baogong_ms_new]")
    '        sb.AppendLine("           ([make_no]")
    '        sb.AppendLine("           ,[cd]")
    '        sb.AppendLine("           ,[tp_no]")
    '        sb.AppendLine("           ,[cnt]") '默认：0  '好像是报工次数
    '        sb.AppendLine("           ,[bg_bar_data]")  'WYY-AF32HR-MGNM/YY-AF32HR-MGNM/6/1/4101/AX94/1/9007858617
    '        sb.AppendLine("           ,[bg_suu]")       '本次报工数量
    '        sb.AppendLine("           ,[bg_result]")
    '        sb.AppendLine("           ,[bg_txt]")
    '        sb.AppendLine("           ,[bg_user]")
    '        sb.AppendLine("           ,[bg_type]")
    '        sb.AppendLine("           ,[mark1]")
    '        sb.AppendLine("           ,[mark2]")
    '        sb.AppendLine("           ,[mark3]")
    '        sb.AppendLine("           ,[deleteFlag]")
    '        sb.AppendLine("           ,[insertUser]")
    '        sb.AppendLine("           ,[insertDate]")
    '        sb.AppendLine("           ,[updateUser]")
    '        sb.AppendLine("           ,[updateDate])")
    '        sb.AppendLine("     VALUES(")
    '        sb.AppendLine("           '" & dt.Rows(0).Item("ZuoFan") & "'")
    '        sb.AppendLine("           ,'" & dt.Rows(0).Item("ProductCode") & "'")
    '        sb.AppendLine("           ," & (i + 1).ToString & "")
    '        sb.AppendLine("           ,0")
    '        Dim bg_suu As String = "1"
    '        sb.AppendLine("           ,'" & dt.Rows(0).Item("ProductCodeSap") & "/" & dt.Rows(0).Item("ProductCode") & "/" & bg_suu & "/" & dt.Rows(0).Item("Package") & "/" & dt.Rows(0).Item("localStorage") & "/" & dt.Rows(0).Item("DestinationCode") & "/" & (i + 1) & "/" & dt.Rows(0).Item("ZuoFan") & "'")
    '        sb.AppendLine("           ," & 1 & "")
    '        sb.AppendLine("           ,''")
    '        sb.AppendLine("           ,''")
    '        sb.AppendLine("           ,'" & user & "'")
    '        sb.AppendLine("           ,N'手动'")
    '        sb.AppendLine("           ,''")
    '        sb.AppendLine("           ,''")
    '        sb.AppendLine("           ,''")
    '        sb.AppendLine("           ,'0'")
    '        sb.AppendLine("           ,'" & user & "'")
    '        sb.AppendLine("           ,getdate()")
    '        sb.AppendLine("           ,''")
    '        sb.AppendLine("           ,null")
    '        sb.AppendLine("           )")
    '    Next


    '    'PARAM
    '    Dim paramList As New List(Of SqlParameter)

    '    SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

    '    Return True
    'End Function

    Public Function SelMSData(ByVal cd As String, ByVal no As String) As Data.DataTable
        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT ")
        sb.AppendLine("*")
        sb.AppendLine("FROM [m_baogong_ms_new]")
        sb.AppendLine("WHERE [cd] = '" & cd & "'")
        sb.AppendLine("AND [make_no] = '" & no & "'")

        'PARAM
        Dim paramList As New List(Of SqlParameter)

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "SelBgList", paramList.ToArray)

        Return dsInfo.Tables(0)

    End Function

    'Public Function SelMSData(ByVal cd As String, ByVal no As String, ByVal tp_no As String) As Data.DataTable

    '    'SQLコメント
    '    '--**テーブル：检查计划 : t_check_plan
    '    Dim sb As New StringBuilder
    '    'SQL文

    '    sb.AppendLine("SELECT ")
    '    sb.AppendLine("*")
    '    sb.AppendLine("FROM [m_baogong_ms_new]")
    '    sb.AppendLine("WHERE [cd] = '" & cd & "'")
    '    sb.AppendLine("AND [make_no] = '" & no & "'")
    '    sb.AppendLine("AND [make_no] = '" & tp_no & "'")

    '    'PARAM
    '    Dim paramList As New List(Of SqlParameter)

    '    Dim dsInfo As New Data.DataSet
    '    SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "SelBgList", paramList.ToArray)

    '    Return dsInfo.Tables(0)

    'End Function



    Public Function UpdMSData(ByVal cd As String, ByVal no As String, ByVal tp_no As String, ByVal tp_bar_cd As String, ByVal bg_result As String, ByVal bg_txt As String, ByVal user As String, ByVal bg_user As String, ByVal bg_type As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder

        'Dim dt As DataTable = SelMSData(cd, no)

        sb.AppendLine("UPDATE [dbo].[m_baogong_ms_new] SET")
        sb.AppendLine("  [bg_result] = '" & bg_result & "'")
        sb.AppendLine(" ,[bg_txt] = N'" & bg_txt & "'")
        sb.AppendLine(" ,[bg_user] = '" & bg_user & "'")
        sb.AppendLine(" ,[bg_type] = N'" & bg_type & "'")
        sb.AppendLine(" ,[tp_bar_cd] = N'" & tp_bar_cd & "'")
        sb.AppendLine(" ,[updateUser] = '" & user & "'")
        sb.AppendLine(" ,[updateDate] = getdate()")
        sb.AppendLine(" ,[cnt] = [cnt] + 1") '点击报工 次数
        sb.AppendLine("WHERE [cd] = '" & cd & "'")
        sb.AppendLine("     AND [make_no] = '" & no & "'")
        sb.AppendLine("     AND [tp_no] = '" & tp_no & "'")


        sb.AppendLine("   declare @rt as nvarchar(100)")
        sb.AppendLine("  if (select count(*) from m_baogong_ms_new where [make_no] = '" & no & "' and [cd]='" & cd & "' and deleteFlag='0' and [bg_result]='NG' )>0 ")
        sb.AppendLine("  begin")
        sb.AppendLine("    set @rt = 'NG'")
        sb.AppendLine("  end")

        sb.AppendLine("  if (select count(*) from m_baogong_ms_new where [make_no] = '" & no & "' and [cd]='" & cd & "' and deleteFlag='0' and [bg_result]='NG' )=0 ")
        sb.AppendLine("  begin")
        sb.AppendLine("     if (select count(*) from m_baogong_ms_new where [make_no] = '" & no & "' and [cd]='" & cd & "' and deleteFlag='0' and isnull([bg_result],'')='' )=0 ")
        sb.AppendLine("     begin")
        sb.AppendLine("         set @rt = 'OK'")
        sb.AppendLine("     end")
        sb.AppendLine("  end")

        sb.AppendLine("UPDATE [dbo].[m_baogong_list_new] SET")
        sb.AppendLine(" [result] = @rt")
        sb.AppendLine(" ,[updateDate] = getdate()")

        sb.AppendLine("WHERE [cd] = '" & cd & "'")
        sb.AppendLine("AND [make_no] = '" & no & "'")

        'PARAM
        Dim paramList As New List(Of SqlParameter)

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True
    End Function

    Public Function DelAllData(ByVal cd As String, ByVal no As String) As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文

        sb.AppendLine("DELETE FROM m_baogong_list_new ")
        sb.AppendLine("WHERE [cd] = '" & cd & "'")
        sb.AppendLine("AND [make_no] = '" & no & "'")


        sb.AppendLine("DELETE FROM m_baogong_ms_new ")
        sb.AppendLine("WHERE [cd] = '" & cd & "'")
        sb.AppendLine("AND [make_no] = '" & no & "'")

        'PARAM
        Dim paramList As New List(Of SqlParameter)

        SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)

        Return True

    End Function


    '开启报工系统
    Public Function IsBaogongSysOn() As Boolean

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT ")
        sb.AppendLine("sys_on")
        sb.AppendLine("FROM [m_baotong_syson]")
        'PARAM
        Dim paramList As New List(Of SqlParameter)
        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.Connection, CommandType.Text, sb.ToString(), dsInfo, "IsBaogongSysOn", paramList.ToArray)

        If dsInfo.Tables(0).Rows.Count > 0 Then
            If dsInfo.Tables(0).Rows(0).Item(0).ToString = "1" Then
                Return True
            End If
        End If

        Return False

    End Function


    Public Function GetBaogongRltFromT_OrderConfirmList(ByVal no As String, ByVal receiveData As String, ByVal fromDateTime As String) As String

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文

        sb.AppendLine("SELECT ")
        sb.AppendLine("msgText,msgType")
        sb.AppendLine("FROM [T_OrderConfirmList]")
        sb.AppendLine("WHERE 1=1")
        sb.AppendLine("AND [OrderNo] = '" & no & "'")
        sb.AppendLine("AND [receiveData] = N'" & receiveData & "'")
        'sb.AppendLine("AND CONVERT(varchar(100), [koushin_date], 120) >= '" & fromDateTime & "'") '2011-05-16 10:57:49
        sb.AppendLine("ORDER BY koushin_date desc")
        'PARAM
        Dim paramList As New List(Of SqlParameter)

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.ConnectionBaogong, CommandType.Text, sb.ToString(), dsInfo, "T_OrderConfirmList", paramList.ToArray)


        If dsInfo.Tables(0).Rows.Count = 0 Then
            Return String.Empty & "|"
        Else
            Return dsInfo.Tables(0).Rows(0).Item(0).ToString & "|" & dsInfo.Tables(0).Rows(0).Item(1).ToString
        End If

    End Function

    Public Function GetNowByT_OrderConfirmList() As String

        'SQLコメント
        '--**テーブル：检查计划 : t_check_plan
        Dim sb As New StringBuilder
        'SQL文

        sb.AppendLine("SELECT ")
        sb.AppendLine("CONVERT(varchar(100), GETDATE(), 120)")
        'PARAM
        Dim paramList As New List(Of SqlParameter)

        Dim dsInfo As New Data.DataSet
        SqlHelperNew.FillDataset(DataAccessManager.ConnectionBaogong, CommandType.Text, sb.ToString(), dsInfo, "GetNowByT_OrderConfirmList", paramList.ToArray)
        If dsInfo.Tables(0).Rows.Count = 0 Then
            Return String.Empty
        Else
            Return dsInfo.Tables(0).Rows(0).Item(0).ToString
        End If

    End Function


    'Public Function BaogongOnOff(ByVal on_off As String) As Boolean
    '    'SQLコメント
    '    '--**テーブル：检查计划 : t_check_plan
    '    Dim sb As New StringBuilder
    '    sb.AppendLine("UPDATE [dbo].[m_baotong_syson] SET [sys_on] = '" & on_off & "'")
    '    'PARAM
    '    Dim paramList As New List(Of SqlParameter)
    '    SqlHelperNew.ExecuteNonQuery(DataAccessManager.Connection, CommandType.Text, sb.ToString(), paramList.ToArray)
    '    Return True
    'End Function


    'SQL文
End Class
