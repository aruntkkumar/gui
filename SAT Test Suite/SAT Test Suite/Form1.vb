﻿Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Drawing.Imaging

Public Class Form1
    'Define your Excel Objects
    Dim xlApp As New Excel.Application
    Dim xlWorkBook As Excel.Workbook
    Dim xlWorkSheet As Excel.Worksheet
    Dim range As Excel.Range
    Dim dialog As OpenFileDialog = New OpenFileDialog()
    Dim dialog1 As SaveFileDialog = New SaveFileDialog()
    '   Dim strFileName As String
    Dim names(50) As String
    Dim x As Integer = 0
    Dim start As Integer
    Dim ports As Integer
    Dim eeName As String
    'Dim Sparameters(200, 200) As Double
    'Dim Spara(200) As Double
    'Dim freq(200) As Double
    Dim array(100) As Integer
    Dim numRow As Integer
    Dim numColumn As Integer
    '    Dim FNameRng As Excel.Range
    '    Dim AveRng As Excel.Range
    '    Dim AveCLRng As Excel.Range
    '    Dim AveUCLRng As Excel.Range
    '    Dim FNameArry As New ArrayList()
    '    Dim AveArry As New ArrayList()
    '    Dim AveCLArry As New ArrayList()
    '    Dim AveUCLArry As New ArrayList()


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dialog.InitialDirectory = "C:\"
        Chart1.Series.Clear()
        Chart1.Series.Add(" ")
        Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 0
        Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000000
        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000
        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.#}"  'Use a Comma to divide by 1000 or Use a % to Multiply by 100
        Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"        '.# to provide one decimal part; For 2 decimal part it is .##
        'Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1
        'Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1
        'Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.2
        'Chart1.ChartAreas("ChartArea1").AxisY.Title = ""
        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -50
        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 0
        Chart1.ChartAreas("ChartArea1").AxisY.Interval = -5
        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0}"
        Chart1.ChartAreas("ChartArea1").AxisY.Title = "dB"
        Chart1.Series(" ").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
        For i As Integer = 0 To 100
            array(i) = 0
            Chart1.Series(" ").Points.AddXY(i, array(i))
        Next
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        If xlWorkBook Is Nothing Then
            Me.Close()
        Else
            xlApp.Quit()
            Me.Close()
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If xlWorkBook Is Nothing Then
        Else
            xlApp.Quit()
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        dialog.Title = "Open File"
        dialog.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Save")
        dialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls|CSV (Comma delimited) (*.csv)|*.csv|All files (*.*)|*.*"
        'dialog.Filter = "All files (*.*)|*.*"
        dialog.FilterIndex = 1
        dialog.RestoreDirectory = True
        If dialog.ShowDialog() = DialogResult.OK Then
            Chart1.Series.Clear()
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 0
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000000
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.#}"
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -50
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 0
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = -10
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.#}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "dB"
            'Try
            xlApp.Workbooks.OpenText(Filename:=dialog.FileName, StartRow:=1, DataType:=Excel.XlTextParsingType.xlDelimited, ConsecutiveDelimiter:=True, Space:=True)
                xlWorkBook = xlApp.ActiveWorkbook
                xlApp.Visible = True
                xlWorkSheet = xlWorkBook.Sheets(1)
                'range = xlWorkSheet.UsedRange
                numRow = xlWorkSheet.UsedRange.Rows.Count
                numColumn = xlWorkSheet.UsedRange.Columns.Count
                'If numColumn = 8-1 Then
                '    names = {"S11 Re", "S11 Im", "S12 Re", "S12 Im", "S21 Re", "S21 Im", "S22 Re", "S22 Im"}
                '    Chart1.Series.Add("S11 Re")
                '    Chart1.Series("S11 Re").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                '    Chart1.Series("S11 Re").BorderWidth = 3
                '    Chart1.Series.Add("S11 Im")
                '    Chart1.Series("S11 Im").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                '    Chart1.Series("S11 Im").BorderWidth = 3
                '    Chart1.Series.Add("S12 Re")
                '    Chart1.Series("S12 Re").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                '    Chart1.Series("S12 Re").BorderWidth = 3
                '    Chart1.Series.Add("S12 Im")
                '    Chart1.Series("S12 Im").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                '    Chart1.Series("S12 Im").BorderWidth = 3
                '    Chart1.Series.Add("S21 Re")
                '    Chart1.Series("S21 Re").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                '    Chart1.Series("S21 Re").BorderWidth = 3
                '    Chart1.Series.Add("S21 Im")
                '    Chart1.Series("S21 Im").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                '    Chart1.Series("S21 Im").BorderWidth = 3
                '    Chart1.Series.Add("S22 Re")
                '    Chart1.Series("S22 Re").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                '    Chart1.Series("S22 Re").BorderWidth = 3
                '    Chart1.Series.Add("S22 Im")
                '    Chart1.Series("S22 Im").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                '    Chart1.Series("S22 Im").BorderWidth = 3
                'End If

                'For j As Integer = 2 To numColumn
                '    For i As Integer = 1 To numRow
                '        Chart1.Series(names(j - 2)).Points.AddXY(xlWorkSheet.Cells(i, 1).value, xlWorkSheet.Cells(i, j).value)
                '        'Chart1.Series("S11 Re").Points.AddXY(xlWorkSheet.Cells(i, 1).value, xlWorkSheet.Cells(i, 2).value)
                '        'Chart1.Series("S11 Im").Points.AddXY(xlWorkSheet.Cells(i, 1).value, xlWorkSheet.Cells(i, 3).value)
                '        'Chart1.Series("S12 Re").Points.AddXY(xlWorkSheet.Cells(i, 1).value, xlWorkSheet.Cells(i, 4).value)
                '        'Chart1.Series("S12 Im").Points.AddXY(xlWorkSheet.Cells(i, 1).value, xlWorkSheet.Cells(i, 5).value)
                '        'Chart1.Series("S21 Re").Points.AddXY(xlWorkSheet.Cells(i, 1).value, xlWorkSheet.Cells(i, 6).value)
                '        'Chart1.Series("S21 Im").Points.AddXY(xlWorkSheet.Cells(i, 1).value, xlWorkSheet.Cells(i, 7).value)
                '        'Chart1.Series("S22 Re").Points.AddXY(xlWorkSheet.Cells(i, 1).value, xlWorkSheet.Cells(i, 8).value)
                '        'Chart1.Series("S22 Im").Points.AddXY(xlWorkSheet.Cells(i, 1).value, xlWorkSheet.Cells(i, 9).value)
                '    Next
                'Next
                Dim Spara(numRow - 6) As Double
                Dim freq(numRow - 6) As Double
                ports = Math.Sqrt((numColumn - 2) / 2)

                For m As Integer = 1 To numRow
                    If xlWorkSheet.Cells(m, 1).value.ToString = "#" Then
                        Select Case xlWorkSheet.Cells(m, 2).value.ToString
                            Case "hz", "Hz", "HZ", "hZ"
                                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000000
                                Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000
                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.#}"
                                GoTo End_Of_For
                            Case "khz", "kHz", "khZ", "kHZ", "Khz", "KHz", "KhZ", "KHZ"
                                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000
                                Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000
                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.#}"
                                GoTo End_Of_For
                            Case "mhz", "mHz", "mhZ", "mHZ", "Mhz", "MHz", "MhZ", "MHZ"
                                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000
                                Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500
                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.#}"
                                GoTo End_Of_For
                            Case "ghz", "gHz", "ghZ", "gHZ", "Ghz", "GHz", "GhZ", "GHZ"
                                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6
                                Chart1.ChartAreas("ChartArea1").AxisX.Interval = 0.5
                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.#}"
                                GoTo End_Of_For
                        End Select
                    End If
                Next
End_Of_For:
            For k As Integer = 1 To numRow      'To find the row location of starting data
                eeName = CStr(xlWorkSheet.Cells(k, 1).value)
                If (String.IsNullOrEmpty(eeName)) Then
                    start = k
                    GoTo End_Of_For2
                End If
            Next
End_Of_For2:
                For k As Integer = start To numRow
                    freq(k - start) = xlWorkSheet.Cells(k, 2).value
                Next
                x = 0
                For i As Integer = 1 To ports
                    For j As Integer = 1 To ports
                        Chart1.Series.Add("S" & i & j)
                        Chart1.Series("S" & i & j).ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                        Chart1.Series("S" & i & j).BorderWidth = 3
                        For k As Integer = start To numRow
                            Spara(k - start) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(k, ((x * 2) + 3)).value, 2) + Math.Pow(xlWorkSheet.Cells(k, ((x * 2) + 4)).value, 2))))
                        Next
                        Chart1.Series("S" & i & j).Points.DataBindXY(freq, Spara)
                        x += 1
                    Next
                Next


                'names = {"S11", "S12", "S21", "S22"}
                'Chart1.Series.Add("S11")
                'Chart1.Series("S11").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                'Chart1.Series("S11").BorderWidth = 3
                'Chart1.Series.Add("S12")
                'Chart1.Series("S12").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                'Chart1.Series("S12").BorderWidth = 3
                'Chart1.Series.Add("S21")
                'Chart1.Series("S21").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                'Chart1.Series("S21").BorderWidth = 3
                'Chart1.Series.Add("S22")
                'Chart1.Series("S22").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                'Chart1.Series("S22").BorderWidth = 3
                'Dim S11(200) As Double
                'Dim S12(200) As Double
                'Dim S21(200) As Double
                'Dim S22(200) As Double

                'For i As Integer = 6 To numRow
                '    freq(i - 6) = xlWorkSheet.Cells(i, 2).value
                '    S11(i - 6) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((2) + 2)).value, 2))))
                '    S12(i - 6) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((4) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((4) + 2)).value, 2))))
                '    S21(i - 6) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((6) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((6) + 2)).value, 2))))
                '    S22(i - 6) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((8) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((8) + 2)).value, 2))))
                'Next

                'Chart1.Series(names(0)).Points.DataBindXY(freq, S11)
                'Chart1.Series(names(1)).Points.DataBindXY(freq, S12)
                'Chart1.Series(names(2)).Points.DataBindXY(freq, S21)
                'Chart1.Series(names(3)).Points.DataBindXY(freq, S22)

                'For j As Integer = 1 To 4
                '    For i As Integer = 6 To numRow
                '        Chart1.Series(names(j - 1)).Points.AddXY(xlWorkSheet.Cells(i, 2).value, (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))))
                '        'If ((10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))) < Chart1.ChartAreas("ChartArea1").AxisY.Minimum) Then
                '        While ((10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))) < Chart1.ChartAreas("ChartArea1").AxisY.Minimum)
                '            'Chart1.ChartAreas("ChartArea1").AxisY.Minimum = CInt(Math.Round(((10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))) / 10) * 10))
                '            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = Chart1.ChartAreas("ChartArea1").AxisY.Minimum - 5
                '        End While   'Loop to check and adjust the Y axis minimum
                '        'End If
                '        'If ((10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))) > Chart1.ChartAreas("ChartArea1").AxisY.Maximum) Then
                '        While ((10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))) > Chart1.ChartAreas("ChartArea1").AxisY.Maximum)
                '            'Chart1.ChartAreas("ChartArea1").AxisY.Maximum = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2))))
                '            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = Chart1.ChartAreas("ChartArea1").AxisY.Maximum + 5
                '        End While   'Loop to check and adjust the Y axis maximum
                '        'End If
                '        While (((Chart1.ChartAreas("ChartArea1").AxisY.Maximum - Chart1.ChartAreas("ChartArea1").AxisY.Minimum) / 5) Mod 5 <> 0)
                '            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = Chart1.ChartAreas("ChartArea1").AxisY.Minimum - 5
                '        End While   'Loop to keep the interval a multiple of 5
                '    Next
                'Next

                xlWorkBook.Close()
                xlApp.Quit()

            'Catch Ex As Exception
            '    MetroFramework.MetroMessageBox.Show(Me, Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End Try
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        dialog1.Filter = "JPEG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp"
        If (dialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
            Chart1.SaveImage(dialog1.FileName, System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub
End Class