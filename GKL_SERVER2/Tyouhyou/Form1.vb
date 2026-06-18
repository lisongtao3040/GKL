Imports System.Configuration
Imports System.Threading
Imports System.Text


Public Class Form1

    Private Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As Long

    Private Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hWnd As Long, lpdwProcessId As Long) As Long
    Private Declare Function OpenProcess Lib "kernel32" (ByVal dwDesiredAccess As Long, ByVal bInheritHandle As Long, ByVal dwProcessId As Long) As Long
    Private Const PROCESS_ALL_ACCESS = &H1F0FFF
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Long) As Long
    Private Declare Function GetModuleFileNameExA Lib "psapi.dll" (ByVal hProcess As Long, ByVal hModule As Long, ByVal ModuleName As String, ByVal nSize As Long) As Long

    Private conn As String = ConfigurationManager.AppSettings("connectionString").ToString()
    Public SqlHelperNew As New SqlHelperNew

    Private Sub btnSyuturyoku_Click(sender As Object, e As EventArgs) Handles btnSyuturyoku.Click

        Dim mythread1 As Thread
        mythread1 = New Thread(AddressOf runPdf)
        mythread1.Start()

    End Sub

    Public Sub runPdf()

        Dim forudaPath As String = Application.StartupPath

        Try
            FileSystem.Rename(forudaPath & "\Tyouhyou.xlsm.exe", forudaPath & "\Tyouhyou.xlsm")
        Catch ex As Exception

        End Try


        Dim oExcel As Object = CreateObject("Excel.Application")

        '显示当前窗口 
        oExcel.Visible = False
        'oExcel.Visible = True

        '更改标题栏
        oExcel.Caption = "XXX"
        '新建
        'oExcel.WorkBooks.Add()
        '打开
        oExcel.WorkBooks.Open(forudaPath & "\Tyouhyou.xlsm")

        oExcel.Sheets("MAIN").Cells(2, 3).Value = Me.tbxSavePath.Text.Trim

        oExcel.Sheets("MAIN").Cells(3, 3).Value = Me.tbxLineCd.Text.Trim

        oExcel.Sheets("MAIN").Cells(4, 3).Value = Me.tbxMakeNo.Text.Trim

        oExcel.Sheets("DB").Cells(1, 2).Value = "Provider=SQLOLEDB.1;" & ConfigurationManager.AppSettings("connectionString").ToString()
        '图片请求URL
        oExcel.Sheets("DB").Cells(4, 2).Value = ConfigurationManager.AppSettings("requestUrl").ToString()

        Try
            oExcel.Run("MakePdf")
        Catch ex As Exception

        End Try


        '保存
        'If Not oExcel.ActiveWorkBook.Saved Then

        'End If
        'oExcel.ActiveSheet.PrintPreview()
        ''另存为
        'oExcel.SaveAs("C:\1.xls")
        ''放弃存盘 
        'oExcel.ActiveWorkBook.Saved = True



        '关闭： 
        'oExcel.WorkBooks.Close(False)

        'oExcel.Visible = True



        oExcel.ActiveWorkBook.Close(False)

        oExcel.Application.DisplayAlerts = True
        oExcel.Application.ScreenUpdating = True

        '退出
        oExcel.Quit()

        ' oExcel = Nothing

        ReleaseExcel(oExcel)
        GC.Collect()
        oExcel = Nothing

        ProcessKill()

        MsgBox("完了")

    End Sub


    Private Sub ProcessKill()

        Dim strFileName As String = ""
        Dim hWnd As Long, pId As Long, hProcess As Long

        'strFileName = String(128, Chr(0))
        hWnd = FindWindow(vbNullString, "Tyouhyou.xlsm - Excel") '以记事本为例
        GetWindowThreadProcessId(hWnd, pId)
        hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, pId)
        'strFileName = Left(strFileName, GetModuleFileNameExA(hProcess, 0, strFileName, Len(strFileName)))
        CloseHandle(hProcess)




        'Dim p As System.Diagnostics.Process

        'p = New System.Diagnostics.Process

        'For Each p In System.Diagnostics.Process.GetProcesses()

        '    If p.ProcessName.ToUpper() = "EXCEL" Then

        '        p.Kill()

        '    End If

        'Next

    End Sub

    Sub ReleaseExcel(ByRef o As Object)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(o)
    End Sub



    Private Function ChkUser()

        'SQLコメント
        '--**テーブル：用户MS : m_user
        Dim sb As New StringBuilder
        'SQL文
        sb.AppendLine("SELECT")
        sb.AppendLine("user_cd")                                                   '用户CD
        sb.AppendLine(", isnull(line_id,'') line_id")                                                 '生产线
        sb.AppendLine(", user_name")                                               '用户名
        sb.AppendLine(", user_password")
        sb.AppendLine("FROM m_user")
        sb.AppendLine("WHERE 1=1")

        sb.AppendLine("AND user_cd = '" & Me.tbxUser.Text.Trim & "'")
        sb.AppendLine("AND isnull(user_password,'') = '" & Me.tbxPassword.Text.Trim & "'")

        Dim ds As New DataSet

        SqlHelperNew.FillDataset(conn, CommandType.Text, sb.ToString, ds, "temp")

        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("line_id")
        Else
            MsgBox("用户名密码不匹配")
            Return "NG"
        End If


    End Function


    Private Sub btnLoginIn_Click(sender As Object, e As EventArgs) Handles btnLoginIn.Click

        Dim lineId As String = ChkUser()
        Me.tbxLineCd.Text = lineId

        If lineId <> "NG" Then
            Me.btnSyuturyoku.Enabled = True
            If Me.tbxUser.Text = "admin" Then
                Me.tbxLineCd.Enabled = True
            Else
                Me.tbxLineCd.Enabled = False
            End If

        Else
            Me.btnSyuturyoku.Enabled = False
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ProcessKill()
        Catch ex As Exception

        End Try

        Dim forudaPath As String = Application.StartupPath
        Try
            If System.IO.File.Exists(forudaPath & "\Tyouhyou.xlsm.exe") Then
                If System.IO.File.Exists(forudaPath & "\Tyouhyou.xlsm") Then
                    System.IO.File.Delete(forudaPath & "\Tyouhyou.xlsm")
                End If
            Else
                If System.IO.File.Exists(forudaPath & "\Tyouhyou.xlsm") Then
                    FileSystem.Rename(forudaPath & "\Tyouhyou.xlsm", forudaPath & "\Tyouhyou.xlsm.exe")
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub
End Class
