Imports System.Data
Imports System.Collections.Generic

Public Class MainWinForm

    Public updDA As New UpdDA

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cnt As Integer

        cnt = GetComDATA("A XXX" & vbCr & "B YYY")

        SearchDate()

    End Sub


    Public Sub SearchDate()

        Dim mainDt As DataTable = updDA.GetMainData()

        If mainDt.Rows.Count > 0 Then
            gvMainData.DataSource = mainDt
            gvMainData.Visible = True
        Else
            gvMainData.Visible = False

        End If

    End Sub

    Public Function GetComDATA(ByVal signAndValues As String) As Integer
        Dim rowsCnt As Integer
        Dim arrSignAndValues As String()
        Dim tmpSignAndValue As String()
        Dim tmpSign As String
        Dim tmpValue As String
        Dim strComputer As String

        rowsCnt = 0
        strComputer = Environment.MachineName

        arrSignAndValues = signAndValues.Split(vbCr)

        If arrSignAndValues.Count > 0 Then

            For i As Integer = 0 To arrSignAndValues.Count - 1

                tmpSignAndValue = arrSignAndValues(i).Split(" ")
                tmpSign = ""
                tmpValue = ""

                If tmpSignAndValue.Count = 1 Then
                    tmpSign = tmpSignAndValue(0)

                ElseIf tmpSignAndValue.Count = 2 Then
                    tmpSign = tmpSignAndValue(0)
                    tmpValue = tmpSignAndValue(1)

                End If

                rowsCnt = rowsCnt + updDA.InsMainData(Me.cbbCOM.SelectedText, tmpSign, tmpValue, strComputer)

            Next

        End If

        Return rowsCnt

    End Function



End Class
