Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Drawing.Imaging
Imports System
Imports System.IO

Public Class Form1
    'Define your Excel Objects
    Dim xlApp As New Excel.Application
    Dim xlWorkBook As Excel.Workbook
    Dim xlWorkSheet As Excel.Worksheet
    Dim range As Excel.Range
    Dim dialog As OpenFileDialog = New OpenFileDialog()
    Dim dialog1 As SaveFileDialog = New SaveFileDialog()
    Dim names(50) As String
    Dim x As Integer
    Dim y As Integer
    Dim z As Integer
    Dim row As Integer = 1
    Dim column As Integer = 1
    Dim ports As Integer
    'Dim spacelocator As String
    'Dim numlocator As Double
    Dim frequnit As String
    Dim parameter As String
    Dim format As String
    Dim matrix As String
    Dim extension As String
    Dim fullstring As String
    Dim line As String
    Dim value As String()
    'Dim Sparameters(200, 200) As Double
    'Dim Spara(200) As Double
    'Dim freq(200) As Double
    Dim array(100) As Integer
    Dim numRow As Integer
    Dim numColumn As Integer
    Declare Function AllocConsole Lib "kernel32" () As Int32
    Declare Function FreeConsole Lib "kernel32" () As Int32


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dialog.InitialDirectory = "C:\"
        Chart1.Series.Clear()
        Chart1.Series.Add(" ")
        Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 0
        Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000000
        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000
        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.#}"  'Use a Comma to divide by 1000 or Use a % to Multiply by 100
        Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"        '.# to provide one decimal part; For 2 decimal part it is .##
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
        dialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls|CSV (Comma delimited) (*.csv)|*.csv|Touchstone files (*.snp)|*.s*p|All files (*.*)|*.*"
        'dialog.Filter = "All files (*.*)|*.*"
        dialog.FilterIndex = 4
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
            extension = System.IO.Path.GetExtension(dialog.FileName)
            ports = System.Text.RegularExpressions.Regex.Replace(extension, "[^\d]", "")    'Remove Characters from a Numeric String
            column = ((Math.Pow(ports, 2) * 2) + 1)
            'Try
            matrix = "full"
            Using sr As New StreamReader(dialog.FileName)

                While Not sr.EndOfStream
                    line = sr.ReadLine()
                    If line.Contains("#") Then
                        If line.ToLower.Contains("khz") Then
                            frequnit = "khz"
                        ElseIf line.ToLower.Contains("mhz") Then
                            frequnit = "mhz"
                        ElseIf line.ToLower.Contains("ghz") Then
                            frequnit = "ghz"
                        ElseIf line.ToLower.Contains("hz") Then
                            frequnit = "hz"
                        End If

                        If line.ToLower.Contains("s") Then
                            parameter = "s"
                        ElseIf line.ToLower.Contains("y") Then
                            parameter = "y"
                        ElseIf line.ToLower.Contains("z") Then
                            parameter = "z"
                        ElseIf line.ToLower.Contains("h") Then
                            parameter = "h"
                        ElseIf line.ToLower.Contains("g") Then
                            parameter = "g"
                        End If

                        If line.ToLower.Contains("ri") Then
                            format = "ri"
                        ElseIf line.ToLower.Contains("ma") Then
                            format = "ma"
                        ElseIf line.ToLower.Contains("db") Then
                            format = "db"
                        End If

                    ElseIf line.ToLower.Contains("matrix format") Then
                        If line.ToLower.Contains("lower") Then
                            matrix = "lower"
                        ElseIf line.ToLower.Contains("upper") Then
                            matrix = "upper"
                            'Else
                            'matrix = "full"
                        End If
                    ElseIf line.Contains("!") Then
                    Else
                        Exit While
                    End If
                End While
                fullstring = sr.ReadToEnd()
            End Using

            fullstring = line & vbCrLf & fullstring     'Adding the starting line which was used in the While condition
            fullstring = System.Text.RegularExpressions.Regex.Replace(fullstring, " {2,}", " ") ' Replace multiple spaces with single space
            'GC.Collect()
            'GC.WaitForPendingFinalizers()
            fullstring = System.Text.RegularExpressions.Regex.Replace(fullstring, " ", ",")
            'fullstring = fullstring.Replace(" ", ",")   'Replace spaces with a coma (Using this causing Out of Memory Error)

            'x = fullstring.IndexOf("#")
            'line = fullstring.Substring(x)
            MetroFramework.MetroMessageBox.Show(Me, "The frequency unit is " & frequnit & ", parameter is " & parameter & ", format is " & format & ", the matrix is " & matrix & " and the columns are " & column, "Header Values", MessageBoxButtons.OK, MessageBoxIcon.Error)
            AllocConsole() 'show console

            'value = fullstring.Split(" ")
            'For Each s As String In value
            '    Console.WriteLine(s)

            'Next

            Console.WriteLine(fullstring)    'the "Console" object works here, just like it does in a "console application"
            FreeConsole() 'hide console 

            Exit Sub

                xlApp.DisplayAlerts = False     'Overwriting the ReadOnly File Error
                xlApp.Workbooks.OpenText(Filename:=dialog.FileName, StartRow:=1, DataType:=Excel.XlTextParsingType.xlDelimited, ConsecutiveDelimiter:=True, Space:=True)
                xlApp.DisplayAlerts = True
                xlWorkBook = xlApp.ActiveWorkbook
                'xlApp.Visible = True
                xlWorkSheet = xlWorkBook.Sheets(1)
                'range = xlWorkSheet.UsedRange
                numRow = xlWorkSheet.UsedRange.Rows.Count
                numColumn = xlWorkSheet.UsedRange.Columns.Count


            row = 1
                column = 1
                For m As Integer = 1 To numRow
                    If xlWorkSheet.Cells(m, 1).value.ToString = "#" Then
                        frequnit = xlWorkSheet.Cells(m, 2).value.ToString
                        parameter = xlWorkSheet.Cells(m, 3).value.ToString
                        format = xlWorkSheet.Cells(m, 4).value.ToString
                        Select Case frequnit
                        Case "hz", "Hz", "HZ", "hZ"
                            'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000000
                            'Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000
                            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0: 0,,,.#}"
                                GoTo Freq_Format_Check_Exit
                            Case "khz", "kHz", "khZ", "kHZ", "Khz", "KHz", "KhZ", "KHZ"
                                'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000
                                'Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000
                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.#}"
                                GoTo Freq_Format_Check_Exit
                            Case "mhz", "mHz", "mhZ", "mHZ", "Mhz", "MHz", "MhZ", "MHZ"
                                'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000
                                'Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500
                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.#}"
                                GoTo Freq_Format_Check_Exit
                            Case "ghz", "gHz", "ghZ", "gHZ", "Ghz", "GHz", "GhZ", "GHZ"
                                'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6
                                'Chart1.ChartAreas("ChartArea1").AxisX.Interval = 0.5
                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.#}"
                                GoTo Freq_Format_Check_Exit
                        End Select
                    End If
                Next
Freq_Format_Check_Exit:
                For k As Integer = 1 To numRow      'To find the row location of starting data
                    If (String.IsNullOrEmpty(CStr(xlWorkSheet.Cells(k, 1).value))) Then
                        row = k
                        column = 2
                        'ports = Math.Sqrt((numColumn - 2) / 2)
                        GoTo Space_Locator_Exit
                    End If
                Next
Space_Locator_Exit:
                If row = 1 Then
                    For k As Integer = 1 To numRow
                        If IsNumeric(xlWorkSheet.Cells(k, 1).value) Then
                            row = k
                            column = 1
                            'ports = Math.Sqrt((numColumn - 1) / 2)
                            GoTo Num_Locator_Exit
                        End If
                    Next
                End If
Num_Locator_Exit:
                y = row
                z = 0
                While (y < numRow)          'To find out the number of Rows to display and use this to declare the Spara and freq arrays
                    y = y + ((numRow - row) / 200)
                    z += 1
                End While
                Dim Spara(z) As Double
                Dim freq(z) As Double
                'For k As Integer = row To numRow
                '    freq(k - row) = xlWorkSheet.Cells(k, column).value
                'Next
                'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = freq(numRow - row)
                y = row
                z = 0
                While (y < numRow)          'To add the frequency values to freq array
                    freq(z) = xlWorkSheet.Cells(y, column).value
                    y = y + ((numRow - row) / 200)
                    z += 1
                End While
                freq(z) = xlWorkSheet.Cells(y, column).value        'Add the last value in case of an uneven number division
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = xlWorkSheet.Cells(numRow, column).value

                Select Case frequnit
                    Case "hz", "Hz", "HZ", "hZ"
                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000
                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 500000000
                        End While
                        GoTo Freq_Format_Check_Exit2
                    Case "khz", "kHz", "khZ", "kHZ", "Khz", "KHz", "KhZ", "KHZ"
                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000
                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 500000
                        End While
                        GoTo Freq_Format_Check_Exit2
                    Case "mhz", "mHz", "mhZ", "mHZ", "Mhz", "MHz", "MhZ", "MHZ"
                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500
                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 500
                        End While
                        GoTo Freq_Format_Check_Exit2
                    Case "ghz", "gHz", "ghZ", "gHZ", "Ghz", "GHz", "GhZ", "GHZ"
                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 0.5
                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 0.5
                        End While
                        GoTo Freq_Format_Check_Exit2
                End Select
Freq_Format_Check_Exit2:
                'Chart1.ChartAreas("ChartArea1").AxisX.Interval = Chart1.ChartAreas("ChartArea1").AxisX.Maximum / 20
                x = column
                Select Case parameter
                    Case "S", "s"
                        For i As Integer = 1 To ports
                            For j As Integer = 1 To ports
                                Chart1.Series.Add("S" & i & j)
                                Chart1.Series("S" & i & j).ChartType = DataVisualization.Charting.SeriesChartType.FastLine
                                Chart1.Series("S" & i & j).BorderWidth = 3
                                Select Case format
                                    Case "RI", "ri", "Ri", "rI"
                                        If column = 1 Then
                                            'For k As Integer = row To numRow
                                            '    Spara(k - row) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(k, ((x * 2))).value, 2) + Math.Pow(xlWorkSheet.Cells(k, ((x * 2) + 1)).value, 2))))
                                            'Next
                                            y = row
                                            z = 0
                                            While (y < numRow)
                                                Spara(z) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(y, ((x * 2))).value, 2) + Math.Pow(xlWorkSheet.Cells(y, ((x * 2) + 1)).value, 2))))
                                                y = y + ((numRow - row) / 200)
                                                z += 1
                                            End While
                                            Spara(z) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(y, ((x * 2))).value, 2) + Math.Pow(xlWorkSheet.Cells(y, ((x * 2) + 1)).value, 2))))
                                        ElseIf column = 2 Then
                                            y = row
                                            z = 0
                                            While (y < numRow)
                                                Spara(z) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(y, ((x * 2) - 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(y, ((x * 2))).value, 2))))
                                                y = y + ((numRow - row) / 200)
                                                z += 1
                                            End While
                                            Spara(z) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(y, ((x * 2) - 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(y, ((x * 2))).value, 2))))
                                        End If
                                        GoTo S_Format_Exit
                                    Case "DB", "db", "Db", "dB"
                                        If column = 1 Then
                                            y = row
                                            z = 0
                                            While (y < numRow)
                                                Spara(z) = xlWorkSheet.Cells(y, ((x * 2))).value
                                                y = y + ((numRow - row) / 200)
                                                z += 1
                                            End While
                                            Spara(z) = xlWorkSheet.Cells(y, ((x * 2))).value
                                        ElseIf column = 2 Then
                                            y = row
                                            z = 0
                                            While (y < numRow)
                                                Spara(z) = xlWorkSheet.Cells(y, ((x * 2) - 1)).value
                                                y = y + ((numRow - row) / 200)
                                                z += 1
                                            End While
                                            Spara(z) = xlWorkSheet.Cells(y, ((x * 2) - 1)).value
                                        End If
                                        GoTo S_Format_Exit
                                    Case "MA", "ma", "Ma", "mA"
                                        If column = 1 Then
                                            y = row
                                            z = 0
                                            While (y < numRow)
                                                Spara(z) = (20 * Math.Log10(xlWorkSheet.Cells(y, ((x * 2))).value))
                                                y = y + ((numRow - row) / 200)
                                                z += 1
                                            End While
                                            Spara(z) = (20 * Math.Log10(xlWorkSheet.Cells(y, ((x * 2))).value))
                                        ElseIf column = 2 Then
                                            y = row
                                            z = 0
                                            While (y < numRow)
                                                Spara(z) = (20 * Math.Log10(xlWorkSheet.Cells(y, ((x * 2) - 1)).value))
                                                y = y + ((numRow - row) / 200)
                                                z += 1
                                            End While
                                            Spara(z) = (20 * Math.Log10(xlWorkSheet.Cells(y, ((x * 2) - 1)).value))
                                        End If
                                        GoTo S_Format_Exit
                                End Select
S_Format_Exit:
                                Chart1.Series("S" & i & j).Points.DataBindXY(freq, Spara)
                                x += 1
                            Next
                        Next
                        GoTo Parameter_Exit
                    Case "Y", "y"
                        GoTo Parameter_Exit
                    Case "Z", "z"
                        GoTo Parameter_Exit
                    Case "H", "h"
                        GoTo Parameter_Exit
                    Case "G", "g"
                        GoTo Parameter_Exit
                End Select
Parameter_Exit:
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
        dialog1.FilterIndex = 3
        If (dialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
            Chart1.SaveImage(dialog1.FileName, System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub
End Class