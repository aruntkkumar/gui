Imports Microsoft.Office.Interop.Excel
Imports System.Drawing.Imaging
Imports System
Imports System.Text
Imports System.IO
Imports System.Windows.Forms.DataVisualization.Charting
Imports Excel

Public Class Form1
    Dim dialog As OpenFileDialog = New OpenFileDialog()
    Dim dialog1 As SaveFileDialog = New SaveFileDialog()
    Dim names(1) As String
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
    Dim value2 As String()
    Dim table(1, 1) As Double
    Dim rand As New Random
    'Dim Sparameters(200, 200) As Double
    'Dim Spara(200) As Double
    'Dim freq(200) As Double
    Dim freq1(1) As Double
    Dim para1(1) As Double
    Dim array(100) As Integer
    Dim numRow As Integer
    Dim numColumn As Integer
    Declare Function AllocConsole Lib "kernel32" () As Int32
    Declare Function FreeConsole Lib "kernel32" () As Int32
    'Dim tooltip As New ToolTip()
    'Dim prevPosition As Point? = Nothing
    Dim checkboxnum As Integer
    Private selectedPoint As DataPoint
    Private selectedPointIndex As Integer
    Private selectedSeries As DataVisualization.Charting.Series
    Dim prevxval As Double = 0
    Dim prevyval As Double = 0
    Dim seriesname(20) As String
    Dim seriespointindex(20) As Integer
    Dim line1 As String
    Dim line2 As String
    Dim eff1(1) As Double
    Dim excelReader As IExcelDataReader

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dialog.InitialDirectory = "C:\"
        Chart1.Series.Clear()
        Chart1.Series.Add(" ")
        Chart1.ChartAreas("ChartArea1").AxisX.Enabled = AxisEnabled.True        'Keeps the axis when all the plots are deselected.
        Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.True
        Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 0
        Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000000
        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000
        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"  'Use a Comma to divide by 1000 or Use a % to Multiply by 100
        Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"        '.# to provide one decimal part; For 2 decimal part it is .##
        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -50
        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 0
        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 5
        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0}"
        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
        Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dash
        Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dash
        Chart1.Series(" ").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
        For i As Integer = 0 To 100
            array(i) = 0
            Chart1.Series(" ").Points.AddXY(i, array(i))
        Next
        AddToolStripMenuItem.Enabled = False
        ClearAllMarkersToolStripMenuItem.Enabled = False
        'ClearSelectedMarkerToolStripMenuItem.Enabled = False
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        'If xlWorkBook Is Nothing Then
        Me.Close()
        'Else
        '    xlApp.Quit()
        '    Me.Close()
        'End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'If xlWorkBook Is Nothing Then
        'Else
        '    xlApp.Quit()
        'End If
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        dialog.Title = "Open File"
        dialog.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Save")
        'dialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls|CSV (Comma delimited) (*.csv)|*.csv|Touchstone files (*.snp)|*.s*p|All files (*.*)|*.*"
        'dialog.FilterIndex = 4
        dialog.Filter = "Touchstone files (*.snp)|*.s*p|All files (*.*)|*.*"
        dialog.FilterIndex = 1
        dialog.RestoreDirectory = True
        If dialog.ShowDialog() = DialogResult.OK Then
            CheckedListBox1.Items.Clear()
            Chart1.Series.Clear()
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 0
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000000
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -50
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 0
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = 5
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.#}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
            extension = System.IO.Path.GetExtension(dialog.FileName)
            ports = System.Text.RegularExpressions.Regex.Replace(extension, "[^\d]", "")    'Remove Characters from a Numeric String
            column = ((Math.Pow(ports, 2) * 2) + 1)
            checkboxnum = 1
            Erase names
            names = New String(1) {}
            Try
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
                    sr.Dispose()
                End Using

                fullstring = line & vbCrLf & fullstring     'Adding the starting line which was used in the While condition
                fullstring = System.Text.RegularExpressions.Regex.Replace(fullstring, "\s{1,}", ",")    'Replaces white spaces (Space, tab, linefeed, carriage-return, formfeed, vertical-tab, and newline characters) with a comma 
                'x = fullstring.IndexOf("#")
                'line = fullstring.Substring(x)
                'MetroFramework.MetroMessageBox.Show(Me, "The frequency unit is " & frequnit & ", parameter is " & parameter & ", format is " & format & ", the matrix is " & matrix & " and the columns are " & column, "Header Values", MessageBoxButtons.OK, MessageBoxIcon.Error)
                'AllocConsole() 'show console

                value = System.Text.RegularExpressions.Regex.Split(fullstring, ",")
                row = (value.Length - 2) / column
                table = New Double(row - 1, column - 1) {}
                freq1 = New Double(row - 1) {}
                para1 = New Double(row - 1) {}
                x = 0
                y = 0
                For Each s As String In value
                    If String.IsNullOrWhiteSpace(s) Then
                    Else
                        table(x, y) = CDbl(s)
                        y += 1
                        If y >= column Then
                            y = 0
                            x += 1
                        End If
                    End If
                Next
                fullstring = vbNullString   '   Releasing memory by setting values as Null
                line = vbNullString
                value = Nothing
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = table(row - 1, 0)
                Select Case frequnit
                    Case "hz"
                        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"    'Two decimal adjustment (if required)
                        If Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 500000000 <> 0 Then    'X axis maximum adjustment
                            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = ((Chart1.ChartAreas("ChartArea1").AxisX.Maximum - (Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 500000000)) + 500000000)
                        End If
                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000                  'X axis interval adjustment
                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 500000000
                        End While
                    Case "khz"
                        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}"
                        If Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 500000 <> 0 Then
                            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = ((Chart1.ChartAreas("ChartArea1").AxisX.Maximum - (Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 500000)) + 500000)
                        End If
                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000
                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 500000
                        End While
                    Case "mhz"
                        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}"
                        If Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 500 <> 0 Then
                            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = ((Chart1.ChartAreas("ChartArea1").AxisX.Maximum - (Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 500)) + 5000)
                        End If
                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500
                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 500
                        End While
                    Case "ghz"
                        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
                        If Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 0.5 <> 0 Then
                            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = ((Chart1.ChartAreas("ChartArea1").AxisX.Maximum - (Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 0.5)) + 0.5)
                        End If
                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 0.5
                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 0.5
                        End While
                End Select
                For i As Integer = 0 To row - 1
                    freq1(i) = table(i, 0)
                    'Console.WriteLine(freq1(i))
                Next
                If matrix = "lower" Then
                    GlobalVariables.seriesnames = New String(ports * (ports + 1) / 2) {}
                    GlobalVariables.series = New Integer(ports * (ports + 1) / 2) {}
                ElseIf matrix = "upper" Then
                    GlobalVariables.seriesnames = New String(ports * (ports + 1) / 2) {}
                    GlobalVariables.series = New Integer(ports * (ports + 1) / 2) {}
                Else
                    GlobalVariables.seriesnames = New String(ports * ports) {}
                    GlobalVariables.series = New Integer(ports * ports) {}
                End If
                x = 1
                y = 0
                For a As Integer = 1 To ports
                    For b As Integer = 1 To ports
                        If matrix = "lower" Then
                            If a < b Then
                                Exit For
                            End If
                        ElseIf matrix = "upper" Then
                            While a > b
                                b += 1
                            End While
                        End If
                        Chart1.Series.Add("S(" & a & "," & b & ")")
                        Chart1.Series("S(" & a & "," & b & ")").ChartType = DataVisualization.Charting.SeriesChartType.Line
                        Chart1.Series("S(" & a & "," & b & ")").BorderWidth = 2
                        Chart1.Series("S(" & a & "," & b & ")").Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                        For j As Integer = 0 To row - 1
                            Select Case parameter
                                Case "s"
                                    Select Case format
                                        Case "db"
                                            para1(j) = table(j, x)
                                        Case "ma"
                                            para1(j) = (20 * Math.Log10(table(j, x)))
                                        Case "ri"
                                            para1(j) = (10 * Math.Log10((Math.Pow(table(j, x), 2) + Math.Pow(table(j, x + 1), 2))))
                                    End Select
                                Case "y"
                                Case "z"
                                Case "h"
                                Case "g"
                            End Select
                            While para1(j) < Chart1.ChartAreas("ChartArea1").AxisY.Minimum
                                Chart1.ChartAreas("ChartArea1").AxisY.Minimum -= 5
                            End While
                            While para1(j) > Chart1.ChartAreas("ChartArea1").AxisY.Maximum
                                Chart1.ChartAreas("ChartArea1").AxisY.Maximum += 5
                            End While
                            'While (((Chart1.ChartAreas("ChartArea1").AxisY.Maximum - Chart1.ChartAreas("ChartArea1").AxisY.Minimum) / 5) Mod 5 <> 0)
                            '    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = Chart1.ChartAreas("ChartArea1").AxisY.Minimum - 5
                            'End While   'Loop to keep the interval a multiple of 5
                            While (((Chart1.ChartAreas("ChartArea1").AxisY.Maximum - Chart1.ChartAreas("ChartArea1").AxisY.Minimum) / Chart1.ChartAreas("ChartArea1").AxisY.Interval) > 10)
                                Chart1.ChartAreas("ChartArea1").AxisY.Interval += 5
                            End While   'Loop to keep the interval a multiple of 5
                        Next
                        x += 2
                        Chart1.Series("S(" & a & "," & b & ")").Points.DataBindXY(freq1, para1)
                        GlobalVariables.seriesnames(y) = "S(" & a & "," & b & ")"
                        GlobalVariables.series(y) = 1
                        y += 1
                    Next
                Next
                Erase freq1         '   Releasing memory
                Erase para1
                Erase table
                'Chart1.Series("S(1,1)").Enabled = False
            Catch Ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            AddToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        dialog1.Filter = "JPEG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp"
        dialog1.FilterIndex = 3
        If (dialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
            Chart1.SaveImage(dialog1.FileName, System.Drawing.Imaging.ImageFormat.Png)
        End If
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        GlobalVariables.yaxismax = Chart1.ChartAreas("ChartArea1").AxisY.Maximum
        GlobalVariables.yaxismin = Chart1.ChartAreas("ChartArea1").AxisY.Minimum
        GlobalVariables.yaxisint = Chart1.ChartAreas("ChartArea1").AxisY.Interval
        If Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True Then
            GlobalVariables.y2axis = True
            GlobalVariables.y2axismax = Chart1.ChartAreas("ChartArea1").AxisY2.Maximum
            GlobalVariables.y2axismin = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum
            GlobalVariables.y2axisint = Chart1.ChartAreas("ChartArea1").AxisY2.Interval
        Else
            GlobalVariables.y2axis = False
        End If
        If Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}" Then
            GlobalVariables.xaxismax = Chart1.ChartAreas("ChartArea1").AxisX.Maximum / 1000000000
            GlobalVariables.xaxismin = Chart1.ChartAreas("ChartArea1").AxisX.Minimum / 1000000000
            GlobalVariables.xaxisint = Chart1.ChartAreas("ChartArea1").AxisX.Interval / 1000000000
        ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}" Then
            GlobalVariables.xaxismax = Chart1.ChartAreas("ChartArea1").AxisX.Maximum / 1000000
            GlobalVariables.xaxismin = Chart1.ChartAreas("ChartArea1").AxisX.Minimum / 1000000
            GlobalVariables.xaxisint = Chart1.ChartAreas("ChartArea1").AxisX.Interval / 1000000
        ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}" Then
            GlobalVariables.xaxismax = Chart1.ChartAreas("ChartArea1").AxisX.Maximum / 1000
            GlobalVariables.xaxismin = Chart1.ChartAreas("ChartArea1").AxisX.Minimum / 1000
            GlobalVariables.xaxisint = Chart1.ChartAreas("ChartArea1").AxisX.Interval / 1000
        Else
            GlobalVariables.xaxismax = Chart1.ChartAreas("ChartArea1").AxisX.Maximum
            GlobalVariables.xaxismin = Chart1.ChartAreas("ChartArea1").AxisX.Minimum
            GlobalVariables.xaxisint = Chart1.ChartAreas("ChartArea1").AxisX.Interval
        End If
        If (ports = 0) Or (ports = vbNull) Then
            GlobalVariables.ports = 0
        Else
            GlobalVariables.ports = ports
        End If
        Form2.ShowDialog()
        If GlobalVariables.button = "ok" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
            If Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}" Then
                Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin * 1000000000
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax * 1000000000
                Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint * 1000000000
            ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}" Then
                Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin * 1000000
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax * 1000000
                Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint * 1000000
            ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}" Then
                Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin * 1000
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax * 1000
                Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint * 1000
            Else
                Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
                Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
            End If
            If Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True Then
                Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = GlobalVariables.y2axismax
                Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = GlobalVariables.y2axismin
                Chart1.ChartAreas("ChartArea1").AxisY2.Interval = GlobalVariables.y2axisint
            End If
            For i As Integer = 0 To GlobalVariables.series.Length - 2
                If GlobalVariables.series(i) = 1 Then
                    Chart1.Series(GlobalVariables.seriesnames(i)).Enabled = True
                Else
                    Chart1.Series(GlobalVariables.seriesnames(i)).Enabled = False
                End If
            Next
        End If
    End Sub

    Private Sub Chart1_MouseClick(sender As Object, e As MouseEventArgs) Handles Chart1.MouseClick
        Dim result As HitTestResult = Chart1.HitTest(e.X, e.Y)
        Dim selectedDataPoint As DataPoint = Nothing
        If result.ChartElementType = ChartElementType.DataPoint Then
            Dim xval = result.ChartArea.AxisX.PixelPositionToValue(e.X)
            Dim yval = result.ChartArea.AxisY.PixelPositionToValue(e.Y)
            If xval <> prevxval AndAlso yval <> prevyval Then
                selectedPointIndex = result.PointIndex
                selectedSeries = result.Series
                'selectedPoint = result.Series.Points(selectedPointIndex)
                selectedDataPoint = CType(result.Object, DataPoint)
                For i As Integer = 0 To checkboxnum - 1
                    If seriesname(i) = selectedSeries.Name AndAlso seriespointindex(i) = selectedPointIndex Then
                        Exit Sub
                    End If
                Next
                If checkboxnum > 20 Then
                    checkboxnum = 1
                    CheckedListBox1.Items.Clear()
                    For i As Integer = 0 To 19
                        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = ""
                        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.None
                    Next
                    ClearAllMarkersToolStripMenuItem.Enabled = False
                    'seriesname = Nothing
                    'seriespointindex = Nothing
                End If
                RemoveHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
                If Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}" Then
                    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(selectedDataPoint.XValue / 1000000000, 3) & ", Y=" & Math.Round(selectedDataPoint.YValues(0), 3), isChecked:=True)
                ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}" Then
                    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(selectedDataPoint.XValue / 1000000, 3) & ", Y=" & Math.Round(selectedDataPoint.YValues(0), 3), isChecked:=True)
                ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}" Then
                    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(selectedDataPoint.XValue / 1000, 3) & ", Y=" & Math.Round(selectedDataPoint.YValues(0), 3), isChecked:=True)
                Else
                    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(selectedDataPoint.XValue, 3) & ", Y=" & Math.Round(selectedDataPoint.YValues(0), 3), isChecked:=True)
                End If
                AddHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
                Chart1.Series(selectedSeries.Name).Points.Item(selectedPointIndex).Label = checkboxnum
                Chart1.Series(selectedSeries.Name).Points.Item(selectedPointIndex).MarkerStyle = MarkerStyle.Triangle
                Chart1.Series(selectedSeries.Name).Points.Item(selectedPointIndex).MarkerSize = 10
                Chart1.Series(selectedSeries.Name).Points.Item(selectedPointIndex).MarkerColor = Chart1.Series(selectedSeries.Name).Color
                seriesname(checkboxnum - 1) = selectedSeries.Name
                seriespointindex(checkboxnum - 1) = selectedPointIndex
                ClearAllMarkersToolStripMenuItem.Enabled = True
                checkboxnum += 1
                prevxval = xval
                prevyval = yval
            End If
        End If
        result = Nothing
        selectedDataPoint = Nothing
    End Sub

    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        dialog.Title = "Open File"
        dialog.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Save")
        dialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls|CSV (Comma delimited) (*.csv)|*.csv|All files (*.*)|*.*"
        dialog.FilterIndex = 1
        dialog.RestoreDirectory = True
        If dialog.ShowDialog() = DialogResult.OK Then
            'Try
            Dim stream As FileStream = File.Open(dialog.FileName, FileMode.Open, FileAccess.Read)
            If System.IO.Path.GetExtension(dialog.FileName) = ".xls" Then       ' Reading from a binary Excel file ('97-2003 format; *.xls)
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream)
            ElseIf System.IO.Path.GetExtension(dialog.FileName) = ".xlsx" Then  ' Reading from a OpenXml Excel file (2007 format; *.xlsx)
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
            End If
            Dim result As DataSet = excelReader.AsDataSet()                     ' DataSet - The result of each spreadsheet will be created in the result.Tables
            excelReader.Close()                                                 ' Free resources (IExcelDataReader is IDisposable)
            excelReader.Dispose()
            fullstring = ""
            line1 = ""
            line2 = ""
            numRow = 0
            While numRow < result.Tables(0).Rows.Count
                For i As Integer = 0 To result.Tables(0).Columns.Count - 1
                    If numRow > 1 Then
                        fullstring += result.Tables(0).Rows(numRow)(i).ToString() + ","
                    ElseIf numRow = 1 Then
                        line2 += result.Tables(0).Rows(numRow)(i).ToString() + ","
                    Else
                        line1 += result.Tables(0).Rows(numRow)(i).ToString() + ","
                    End If
                Next
                If numRow > 1 Then
                    fullstring += vbLf
                End If
                numRow += 1
            End While
            If line2.ToLower.Contains("ghz") Then
            ElseIf line2.ToLower.Contains("mhz") Then
            ElseIf line2.ToLower.Contains("khz") Then
            ElseIf line2.ToLower.Contains("hz") Then
            Else
                MetroFramework.MetroMessageBox.Show(Me, "The selected spreadsheet is not supported by SAT Test Suite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If line2.ToLower.Contains("db") Then
                format = "db"
            ElseIf line2.ToLower.Contains("percentage") Or line2.ToLower.Contains("%") Then
                format = "percentage"
            Else
                MetroFramework.MetroMessageBox.Show(Me, "The selected spreadsheet is not supported by SAT Test Suite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True
            'Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = 100 * (Math.Pow(10, (Chart1.ChartAreas("ChartArea1").AxisY.Maximum) / 10))
            'Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = 100 * (Math.Pow(10, (Chart1.ChartAreas("ChartArea1").AxisY.Minimum) / 10))
            Chart1.ChartAreas("ChartArea1").AxisY2.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
            numRow = result.Tables(0).Rows.Count - 2
            numColumn = result.Tables(0).Columns.Count
            value = System.Text.RegularExpressions.Regex.Split(line1, ",")
            value2 = System.Text.RegularExpressions.Regex.Split(line2, ",")
            'Dim names(numColumn) As String
            column = 0
            If names.Length - 2 = 0 Then
                names = New String(numColumn) {}
            Else
                column = names.Length - 2
                ReDim Preserve names(column + numColumn)
            End If
            table = New Double(numRow - 1, numColumn - 1) {}
            freq1 = New Double(numRow - 1) {}
            eff1 = New Double(numRow - 1) {}
            x = 0
            For Each s As String In value
                y = 0
                For Each s2 As String In value2
                    If x = y Then
                        If x = 0 Then
                            names(column + x) = s2
                        Else
                            For i As Integer = 0 To names.Length - 2
                                If names(i) = String.Concat(s + " " + s2) Then
                                    MetroFramework.MetroMessageBox.Show(Me, "The title name '" + String.Concat(s + " " + s2) + "' is already a member of the Chart Series", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            Next
                            names(column + x) = String.Concat(s + " " + s2)
                        End If
                    End If
                    y += 1
                Next
                x += 1
            Next
            value = System.Text.RegularExpressions.Regex.Split(fullstring, ",")
            x = 0
            y = 0
            For Each s As String In value
                If String.IsNullOrWhiteSpace(s) Then
                Else
                    table(x, y) = CDbl(s)
                    y += 1
                    If y >= numColumn Then
                        y = 0
                        x += 1
                    End If
                End If
            Next
            If frequnit = "hz" Then
                If line2.ToString.ToLower.Contains("ghz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) * 1000000000
                    Next
                ElseIf line2.ToString.ToLower.Contains("mhz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) * 1000000
                    Next
                ElseIf line2.ToString.ToLower.Contains("khz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) * 1000
                    Next
                Else
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0)
                    Next
                End If
            ElseIf frequnit = "khz" Then
                If line2.ToString.ToLower.Contains("ghz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) * 1000000
                    Next
                ElseIf line2.ToString.ToLower.Contains("mhz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) * 1000
                    Next
                ElseIf line2.ToString.ToLower.Contains("khz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0)
                    Next
                Else
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) / 1000
                    Next
                End If
            ElseIf frequnit = "mhz" Then
                If line2.ToString.ToLower.Contains("ghz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) * 1000
                    Next
                ElseIf line2.ToString.ToLower.Contains("mhz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0)
                    Next
                ElseIf line2.ToString.ToLower.Contains("khz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) / 1000
                    Next
                Else
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) / 1000000
                    Next
                End If
            ElseIf frequnit = "ghz" Then
                If line2.ToString.ToLower.Contains("ghz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0)
                    Next
                ElseIf line2.ToString.ToLower.Contains("mhz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) / 1000
                    Next
                ElseIf line2.ToString.ToLower.Contains("khz") Then
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) / 1000000
                    Next
                Else
                    For i As Integer = 0 To numRow - 1
                        freq1(i) = table(i, 0) / 1000000000
                    Next
                End If
            End If
            While freq1.Max > Chart1.ChartAreas("ChartArea1").AxisX.Maximum
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum += Chart1.ChartAreas("ChartArea1").AxisX.Interval
            End While
            Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = 100
            Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = 0
            Chart1.ChartAreas("ChartArea1").AxisY2.Interval = 10
            Chart1.ChartAreas("ChartArea1").AxisY2.LabelStyle.Format = "{0:0.##}"   'Use a Comma to divide by 1000 or Use a % to Multiply by 100
            Chart1.ChartAreas("ChartArea1").AxisY2.Title = "Efficiency in %"        '.# to provide one decimal part; For 2 decimal part it is .###
            'If line2.ToLower.Contains("db") Then
            '    format = "db"
            '    'Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = 0
            '    'Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = -15
            '    'Chart1.ChartAreas("ChartArea1").AxisY2.Interval = 2
            '    'Chart1.ChartAreas("ChartArea1").AxisY2.LabelStyle.Format = "{0:0.##}"   'Use a Comma to divide by 1000 or Use a % to Multiply by 100
            '    'Chart1.ChartAreas("ChartArea1").AxisY2.Title = "Efficiency in dB"       '.# to provide one decimal part; For 2 decimal part it is .##
            'Else
            '    format = "percentage"
            'End If
            x = GlobalVariables.seriesnames.Length
            ReDim Preserve GlobalVariables.seriesnames(x + numColumn - 2)
            ReDim Preserve GlobalVariables.series(x + numColumn - 2)
            For i As Integer = 1 To numColumn - 1
                Chart1.Series.Add(names(column + i))
                Chart1.Series(names(column + i)).XAxisType = AxisType.Primary
                Chart1.Series(names(column + i)).YAxisType = AxisType.Secondary
                Chart1.Series(names(column + i)).ChartType = DataVisualization.Charting.SeriesChartType.Line
                Chart1.Series(names(column + i)).BorderWidth = 2
                Chart1.Series(names(column + i)).BorderDashStyle = ChartDashStyle.Dash
                Chart1.Series(names(column + i)).Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                For j As Integer = 0 To numRow - 1
                    If format = "db" Then
                        eff1(j) = 100 * (Math.Pow(10, (table(j, i) / 10)))
                    Else
                        eff1(j) = table(j, i)
                    End If
                Next
                Chart1.Series(names(column + i)).Points.DataBindXY(freq1, eff1)
                GlobalVariables.seriesnames(x + i - 2) = names(column + i)
                GlobalVariables.series(x + i - 2) = 1
            Next
            '    AllocConsole() 'show console
            'For i As Integer = 0 To GlobalVariables.seriesnames.Length - 2
            '    Console.WriteLine(GlobalVariables.seriesnames(i))
            'Next
            fullstring = vbNullString   ' Releasing memory by setting values as Null
            line1 = vbNullString
            line2 = vbNullString
            value = Nothing
            value2 = Nothing
            result.Dispose()
            Erase freq1
            Erase eff1
            Erase table
            'Catch ex As Exception
            '    MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End Try
        End If
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        Me.BeginInvoke(DirectCast(Sub() ItemChecker(), MethodInvoker))
    End Sub

    Sub ItemChecker()
        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemChecked(i) = True Then
                Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = i + 1
                Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.Triangle
            Else
                Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = ""
                Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.None
            End If
        Next
    End Sub

    Private Sub ClearAllMarkersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearAllMarkersToolStripMenuItem.Click
        CheckedListBox1.Items.Clear()
        For i As Integer = 0 To checkboxnum - 2
            Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = ""
            Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.None
        Next
        checkboxnum = 1
        ClearAllMarkersToolStripMenuItem.Enabled = False
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    'Private Sub ClearSelectedMarkerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearSelectedMarkerToolStripMenuItem.Click
    '    'For i As Integer = 0 To checkboxnum - 2
    '    '    If i = CheckedListBox1.SelectedIndex() Then
    '    '        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = ""
    '    '        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.None
    '    '        CheckedListBox1.Items.RemoveAt(CheckedListBox1.SelectedIndex())
    '    '        'For j As Integer = i To checkboxnum - 2
    '    '        '    seriesname(i) = seriesname(i + 1)
    '    '        '    seriespointindex(i) = seriespointindex(i + 1)
    '    '        'Next
    '    '        'checkboxnum -= 1
    '    '        Exit For
    '    '    End If
    '    'Next
    '    'For i As Integer = 0 To CheckedListBox1.Items.Count - 1
    '    '    Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = i + 1
    '    '    Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.Triangle
    '    'Next
    '    AllocConsole() 'show console
    '    Console.WriteLine(CheckedListBox1.SelectedIndex())
    '    Console.WriteLine(checkboxnum - 2)
    '    Chart1.Series(seriesname(CheckedListBox1.SelectedIndex())).Points.Item(seriespointindex(CheckedListBox1.SelectedIndex())).Label = ""
    '    Chart1.Series(seriesname(CheckedListBox1.SelectedIndex())).Points.Item(seriespointindex(CheckedListBox1.SelectedIndex())).MarkerStyle = MarkerStyle.None
    '    For i As Integer = CheckedListBox1.SelectedIndex() To checkboxnum - 2
    '        If i < checkboxnum - 2 Then
    '            seriesname(i) = seriesname(i + 1)
    '            seriespointindex(i) = seriespointindex(i + 1)
    '        End If
    '        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = i + 1
    '        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.Triangle
    '        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerSize = 10
    '        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerColor = Chart1.Series(seriesname(i)).Color
    '        Console.WriteLine(i)
    '    Next
    '    checkboxnum -= 1
    '    CheckedListBox1.Items.RemoveAt(CheckedListBox1.SelectedIndex())
    '    ClearSelectedMarkerToolStripMenuItem.Enabled = False
    'End Sub

    'Private Sub CheckedListBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedValueChanged
    '    ClearSelectedMarkerToolStripMenuItem.Enabled = True
    'End Sub
End Class

Public Class GlobalVariables
    Public Shared xaxismin As Double = 0
    Public Shared xaxismax As Double = 6
    Public Shared xaxisint As Double = 0.5
    Public Shared yaxismin As Double = -50
    Public Shared yaxismax As Double = 0
    Public Shared yaxisint As Double = 5
    Public Shared y2axismin As Double = vbNull
    Public Shared y2axismax As Double = vbNull
    Public Shared y2axisint As Double = vbNull
    Public Shared button As String
    Public Shared ports As Integer
    Public Shared series() As Integer
    Public Shared seriesnames() As String
    Public Shared y2axis As Boolean
End Class


'Private Sub Chart1_MouseClick(sender As Object, e As MouseEventArgs) Handles Chart1.MouseClick
'    Dim pos As Point = e.Location
'    If prevPosition.HasValue AndAlso pos = prevPosition.Value Then
'        Exit Sub
'    End If
'    'ToolTip.RemoveAll()
'    prevPosition = pos
'    'Dim results = Chart1.HitTest(pos.X, pos.Y, False, ChartElementType.DataPoint)
'    Dim results As HitTestResult = Chart1.HitTest(e.X, e.Y)
'    'Dim prevxval As Double
'    'Dim prevyval As Double
'    'For Each result As HitTestResult In results
'    If results.ChartElementType = ChartElementType.DataPoint Then

'        Dim prop As DataPoint = results.Object
'        If prop IsNot Nothing Then
'            Dim xpixel = results.ChartArea.AxisX.ValueToPixelPosition(prop.XValue)
'            Dim ypixel = results.ChartArea.AxisY.ValueToPixelPosition(prop.YValues(0))
'            If (Math.Abs(pos.X - xpixel) < 100) AndAlso (Math.Abs(pos.Y - ypixel) < 100) Then
'                If checkboxnum > 20 Then
'                    checkboxnum = 1
'                    CheckedListBox1.Items.Clear()
'                End If
'                If Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}" Then
'                    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(prop.XValue / 1000000000, 3) & ", Y=" & Math.Round(prop.YValues(0), 3), isChecked:=True)
'                ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}" Then
'                    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(prop.XValue / 1000000, 3) & ", Y=" & Math.Round(prop.YValues(0), 3), isChecked:=True)
'                ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}" Then
'                    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(prop.XValue / 1000, 3) & ", Y=" & Math.Round(prop.YValues(0), 3), isChecked:=True)
'                Else
'                    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(prop.XValue, 3) & ", Y=" & Math.Round(prop.YValues(0), 3), isChecked:=True)
'                End If
'                'ToolTip.Show("X=" & xpixel & ", Y=" & yval, Me.Chart1, pos.X, (pos.Y - 15))
'                checkboxnum += 1
'                Dim myBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Red)
'                Dim formGraphics As System.Drawing.Graphics
'                formGraphics = sender.CreateGraphics()
'                formGraphics.FillEllipse(myBrush, New Rectangle(xpixel, ypixel, 15, 15))
'                myBrush.Dispose()
'                formGraphics.Dispose()
'            End If
'        End If


'        'Dim xval = results.ChartArea.AxisX.PixelPositionToValue(pos.X)
'        '    Dim yval = results.ChartArea.AxisY.PixelPositionToValue(pos.Y)
'        '    'If result.ChartElementType = ChartElementType.DataPoint Then
'        '    '    Dim xval = result.ChartArea.AxisX.PixelPositionToValue(pos.X)
'        '    '    Dim yval = result.ChartArea.AxisY.PixelPositionToValue(pos.Y)
'        '    'tooltip.Show("X=" & xval & ", Y=" & yval, Me.Chart1, pos.X, (pos.Y - 15))
'        '    'If xval <> prevxval AndAlso yval <> prevyval Then
'        '    If checkboxnum > 20 Then
'        '        checkboxnum = 1
'        '        CheckedListBox1.Items.Clear()
'        '    End If
'        'If Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}" Then
'        '    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(xval / 1000000000, 3) & ", Y=" & Math.Round(yval, 3), isChecked:=True)
'        'ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}" Then
'        '    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(xval / 1000000, 3) & ", Y=" & Math.Round(yval, 3), isChecked:=True)
'        'ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}" Then
'        '    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(xval / 1000, 3) & ", Y=" & Math.Round(yval, 3), isChecked:=True)
'        'Else
'        '    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(xval, 3) & ", Y=" & Math.Round(yval, 3), isChecked:=True)
'        'End If

'        'Dim pt As DataPoint = Chart1.Series(0).Points(CInt(Math.Max(xval - 1), 0)
'        'Dim pt As DataPoint = DirectCast(sender, Chart).Series(0).Points(CInt(xval) - 1).XValues()
'        'pt.MarkerStyle = MarkerStyle.Square
'        'Dim myBrush As New System.Drawing.SolidBrush(System.Drawing.Color.Red)
'        '    Dim formGraphics As System.Drawing.Graphics
'        '    formGraphics = sender.CreateGraphics()
'        '    'formGraphics.FillEllipse(myBrush, New Rectangle(result.ChartArea.AxisX.ValueToPixelPosition(xval), result.ChartArea.AxisY.ValueToPixelPosition(yval), 15, 15))
'        '    formGraphics.FillEllipse(myBrush, New Rectangle(results.ChartArea.AxisX.ValueToPixelPosition(xval), results.ChartArea.AxisY.ValueToPixelPosition(yval), 15, 15))
'        '    myBrush.Dispose()
'        '    formGraphics.Dispose()
'        '    checkboxnum += 1
'        '    'prevxval = xval
'        '    'prevyval = yval
'        '    'End If


'    End If
'    'Next
'End Sub

'Previously used code
'            Exit Sub

'                xlApp.DisplayAlerts = False     'Overwriting the ReadOnly File Error
'                xlApp.Workbooks.OpenText(Filename:=dialog.FileName, StartRow:=1, DataType:=Excel.XlTextParsingType.xlDelimited, ConsecutiveDelimiter:=True, Space:=True)
'                xlApp.DisplayAlerts = True
'                xlWorkBook = xlApp.ActiveWorkbook
'                'xlApp.Visible = True
'                xlWorkSheet = xlWorkBook.Sheets(1)
'                'range = xlWorkSheet.UsedRange
'                numRow = xlWorkSheet.UsedRange.Rows.Count
'                numColumn = xlWorkSheet.UsedRange.Columns.Count


'            row = 1
'                column = 1
'                For m As Integer = 1 To numRow
'                    If xlWorkSheet.Cells(m, 1).value.ToString = "#" Then
'                        frequnit = xlWorkSheet.Cells(m, 2).value.ToString
'                        parameter = xlWorkSheet.Cells(m, 3).value.ToString
'                        format = xlWorkSheet.Cells(m, 4).value.ToString
'                        Select Case frequnit
'                        Case "hz", "Hz", "HZ", "hZ"
'                            'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000000
'                            'Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000
'                            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0: 0,,,.#}"
'                                GoTo Freq_Format_Check_Exit
'                            Case "khz", "kHz", "khZ", "kHZ", "Khz", "KHz", "KhZ", "KHZ"
'                                'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000000
'                                'Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000
'                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.#}"
'                                GoTo Freq_Format_Check_Exit
'                            Case "mhz", "mHz", "mhZ", "mHZ", "Mhz", "MHz", "MhZ", "MHZ"
'                                'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6000
'                                'Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500
'                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.#}"
'                                GoTo Freq_Format_Check_Exit
'                            Case "ghz", "gHz", "ghZ", "gHZ", "Ghz", "GHz", "GhZ", "GHZ"
'                                'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 6
'                                'Chart1.ChartAreas("ChartArea1").AxisX.Interval = 0.5
'                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.#}"
'                                GoTo Freq_Format_Check_Exit
'                        End Select
'                    End If
'                Next
'Freq_Format_Check_Exit:
'                For k As Integer = 1 To numRow      'To find the row location of starting data
'                    If (String.IsNullOrEmpty(CStr(xlWorkSheet.Cells(k, 1).value))) Then
'                        row = k
'                        column = 2
'                        'ports = Math.Sqrt((numColumn - 2) / 2)
'                        GoTo Space_Locator_Exit
'                    End If
'                Next
'Space_Locator_Exit:
'                If row = 1 Then
'                    For k As Integer = 1 To numRow
'                        If IsNumeric(xlWorkSheet.Cells(k, 1).value) Then
'                            row = k
'                            column = 1
'                            'ports = Math.Sqrt((numColumn - 1) / 2)
'                            GoTo Num_Locator_Exit
'                        End If
'                    Next
'                End If
'Num_Locator_Exit:
'                y = row
'                z = 0
'                While (y < numRow)          'To find out the number of Rows to display and use this to declare the Spara and freq arrays
'                    y = y + ((numRow - row) / 200)
'                    z += 1
'                End While
'                Dim Spara(z) As Double
'                Dim freq(z) As Double
'                'For k As Integer = row To numRow
'                '    freq(k - row) = xlWorkSheet.Cells(k, column).value
'                'Next
'                'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = freq(numRow - row)
'                y = row
'                z = 0
'                While (y < numRow)          'To add the frequency values to freq array
'                    freq(z) = xlWorkSheet.Cells(y, column).value
'                    y = y + ((numRow - row) / 200)
'                    z += 1
'                End While
'                freq(z) = xlWorkSheet.Cells(y, column).value        'Add the last value in case of an uneven number division
'                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = xlWorkSheet.Cells(numRow, column).value

'                Select Case frequnit
'                    Case "hz", "Hz", "HZ", "hZ"
'                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000000
'                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
'                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 500000000
'                        End While
'                        GoTo Freq_Format_Check_Exit2
'                    Case "khz", "kHz", "khZ", "kHZ", "Khz", "KHz", "KhZ", "KHZ"
'                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500000
'                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
'                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 500000
'                        End While
'                        GoTo Freq_Format_Check_Exit2
'                    Case "mhz", "mHz", "mhZ", "mHZ", "Mhz", "MHz", "MhZ", "MHZ"
'                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 500
'                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
'                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 500
'                        End While
'                        GoTo Freq_Format_Check_Exit2
'                    Case "ghz", "gHz", "ghZ", "gHZ", "Ghz", "GHz", "GhZ", "GHZ"
'                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 0.5
'                        While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
'                            Chart1.ChartAreas("ChartArea1").AxisX.Interval += 0.5
'                        End While
'                        GoTo Freq_Format_Check_Exit2
'                End Select
'Freq_Format_Check_Exit2:
'                'Chart1.ChartAreas("ChartArea1").AxisX.Interval = Chart1.ChartAreas("ChartArea1").AxisX.Maximum / 20
'                x = column
'                Select Case parameter
'                    Case "S", "s"
'                        For i As Integer = 1 To ports
'                            For j As Integer = 1 To ports
'                                Chart1.Series.Add("S" & i & j)
'                                Chart1.Series("S" & i & j).ChartType = DataVisualization.Charting.SeriesChartType.FastLine
'                                Chart1.Series("S" & i & j).BorderWidth = 3
'                                Select Case format
'                                    Case "RI", "ri", "Ri", "rI"
'                                        If column = 1 Then
'                                            'For k As Integer = row To numRow
'                                            '    Spara(k - row) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(k, ((x * 2))).value, 2) + Math.Pow(xlWorkSheet.Cells(k, ((x * 2) + 1)).value, 2))))
'                                            'Next
'                                            y = row
'                                            z = 0
'                                            While (y < numRow)
'                                                Spara(z) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(y, ((x * 2))).value, 2) + Math.Pow(xlWorkSheet.Cells(y, ((x * 2) + 1)).value, 2))))
'                                                y = y + ((numRow - row) / 200)
'                                                z += 1
'                                            End While
'                                            Spara(z) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(y, ((x * 2))).value, 2) + Math.Pow(xlWorkSheet.Cells(y, ((x * 2) + 1)).value, 2))))
'                                        ElseIf column = 2 Then
'                                            y = row
'                                            z = 0
'                                            While (y < numRow)
'                                                Spara(z) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(y, ((x * 2) - 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(y, ((x * 2))).value, 2))))
'                                                y = y + ((numRow - row) / 200)
'                                                z += 1
'                                            End While
'                                            Spara(z) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(y, ((x * 2) - 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(y, ((x * 2))).value, 2))))
'                                        End If
'                                        GoTo S_Format_Exit
'                                    Case "DB", "db", "Db", "dB"
'                                        If column = 1 Then
'                                            y = row
'                                            z = 0
'                                            While (y < numRow)
'                                                Spara(z) = xlWorkSheet.Cells(y, ((x * 2))).value
'                                                y = y + ((numRow - row) / 200)
'                                                z += 1
'                                            End While
'                                            Spara(z) = xlWorkSheet.Cells(y, ((x * 2))).value
'                                        ElseIf column = 2 Then
'                                            y = row
'                                            z = 0
'                                            While (y < numRow)
'                                                Spara(z) = xlWorkSheet.Cells(y, ((x * 2) - 1)).value
'                                                y = y + ((numRow - row) / 200)
'                                                z += 1
'                                            End While
'                                            Spara(z) = xlWorkSheet.Cells(y, ((x * 2) - 1)).value
'                                        End If
'                                        GoTo S_Format_Exit
'                                    Case "MA", "ma", "Ma", "mA"
'                                        If column = 1 Then
'                                            y = row
'                                            z = 0
'                                            While (y < numRow)
'                                                Spara(z) = (20 * Math.Log10(xlWorkSheet.Cells(y, ((x * 2))).value))
'                                                y = y + ((numRow - row) / 200)
'                                                z += 1
'                                            End While
'                                            Spara(z) = (20 * Math.Log10(xlWorkSheet.Cells(y, ((x * 2))).value))
'                                        ElseIf column = 2 Then
'                                            y = row
'                                            z = 0
'                                            While (y < numRow)
'                                                Spara(z) = (20 * Math.Log10(xlWorkSheet.Cells(y, ((x * 2) - 1)).value))
'                                                y = y + ((numRow - row) / 200)
'                                                z += 1
'                                            End While
'                                            Spara(z) = (20 * Math.Log10(xlWorkSheet.Cells(y, ((x * 2) - 1)).value))
'                                        End If
'                                        GoTo S_Format_Exit
'                                End Select
'S_Format_Exit:
'                                Chart1.Series("S" & i & j).Points.DataBindXY(freq, Spara)
'                                x += 1
'                            Next
'                        Next
'                        GoTo Parameter_Exit
'                    Case "Y", "y"
'                        GoTo Parameter_Exit
'                    Case "Z", "z"
'                        GoTo Parameter_Exit
'                    Case "H", "h"
'                        GoTo Parameter_Exit
'                    Case "G", "g"
'                        GoTo Parameter_Exit
'                End Select
'Parameter_Exit:
'                'names = {"S11", "S12", "S21", "S22"}
'                'Chart1.Series.Add("S11")
'                'Chart1.Series("S11").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
'                'Chart1.Series("S11").BorderWidth = 3
'                'Chart1.Series.Add("S12")
'                'Chart1.Series("S12").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
'                'Chart1.Series("S12").BorderWidth = 3
'                'Chart1.Series.Add("S21")
'                'Chart1.Series("S21").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
'                'Chart1.Series("S21").BorderWidth = 3
'                'Chart1.Series.Add("S22")
'                'Chart1.Series("S22").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
'                'Chart1.Series("S22").BorderWidth = 3
'                'Dim S11(200) As Double
'                'Dim S12(200) As Double
'                'Dim S21(200) As Double
'                'Dim S22(200) As Double

'                'For i As Integer = 6 To numRow
'                '    freq(i - 6) = xlWorkSheet.Cells(i, 2).value
'                '    S11(i - 6) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((2) + 2)).value, 2))))
'                '    S12(i - 6) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((4) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((4) + 2)).value, 2))))
'                '    S21(i - 6) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((6) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((6) + 2)).value, 2))))
'                '    S22(i - 6) = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((8) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((8) + 2)).value, 2))))
'                'Next

'                'Chart1.Series(names(0)).Points.DataBindXY(freq, S11)
'                'Chart1.Series(names(1)).Points.DataBindXY(freq, S12)
'                'Chart1.Series(names(2)).Points.DataBindXY(freq, S21)
'                'Chart1.Series(names(3)).Points.DataBindXY(freq, S22)

'                'For j As Integer = 1 To 4
'                '    For i As Integer = 6 To numRow
'                '        Chart1.Series(names(j - 1)).Points.AddXY(xlWorkSheet.Cells(i, 2).value, (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))))
'                '        'If ((10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))) < Chart1.ChartAreas("ChartArea1").AxisY.Minimum) Then
'                '        While ((10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))) < Chart1.ChartAreas("ChartArea1").AxisY.Minimum)
'                '            'Chart1.ChartAreas("ChartArea1").AxisY.Minimum = CInt(Math.Round(((10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))) / 10) * 10))
'                '            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = Chart1.ChartAreas("ChartArea1").AxisY.Minimum - 5
'                '        End While   'Loop to check and adjust the Y axis minimum
'                '        'End If
'                '        'If ((10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))) > Chart1.ChartAreas("ChartArea1").AxisY.Maximum) Then
'                '        While ((10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2)))) > Chart1.ChartAreas("ChartArea1").AxisY.Maximum)
'                '            'Chart1.ChartAreas("ChartArea1").AxisY.Maximum = (10 * Math.Log10((Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 1)).value, 2) + Math.Pow(xlWorkSheet.Cells(i, ((j * 2) + 2)).value, 2))))
'                '            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = Chart1.ChartAreas("ChartArea1").AxisY.Maximum + 5
'                '        End While   'Loop to check and adjust the Y axis maximum
'                '        'End If
'                '        While (((Chart1.ChartAreas("ChartArea1").AxisY.Maximum - Chart1.ChartAreas("ChartArea1").AxisY.Minimum) / 5) Mod 5 <> 0)
'                '            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = Chart1.ChartAreas("ChartArea1").AxisY.Minimum - 5
'                '        End While   'Loop to keep the interval a multiple of 5
'                '    Next
'                'Next

'                xlWorkBook.Close()
'                xlApp.Quit()