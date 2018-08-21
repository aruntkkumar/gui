Imports Microsoft.Office.Interop.Excel
Imports System.Drawing.Imaging
Imports System
Imports System.Text
Imports System.IO
Imports System.Windows.Forms.DataVisualization.Charting
Imports Excel
Imports System.Net
Imports Microsoft.Win32
Imports System.Threading

Public Class Form1
    Dim dialog As OpenFileDialog = New OpenFileDialog()
    Dim dialog1 As SaveFileDialog = New SaveFileDialog()
    Dim names(-1) As String
    Dim x As Integer
    Dim y As Integer
    Dim z As Integer
    Dim row As Integer = 1
    Dim column As Integer = 1
    Dim ports As Integer = 0
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
    Dim seriesname(50) As String
    Dim seriespointindex(50) As Integer
    Dim line1 As String
    Dim line2 As String
    Dim eff1(1) As Double
    Dim excelReader As IExcelDataReader
    Dim a As Double
    Dim xmax As Double
    'Dim x2max As Double = 0
    Dim xmin As Double
    Dim ymax As Double
    Dim ymin As Double
    'Dim y2max1 As Double = 0
    'Dim y2min1 As Double = 0
    Dim y2max As Double
    Dim y2min As Double
    Dim preVISAAddress As String
    Dim newtoolbar As Boolean = False
    Dim addtoolbar As Boolean = False
    Dim generic As Boolean = False
    Dim db As Boolean = False
    Dim values() As String
    Dim colourcounter As Integer = 1
    Dim c As Color
    Dim interval As Double = 0.0
    Dim empty(0) As Double
    Dim decimalpart As Integer = 0
    Dim nullpoints(0) As Integer
    Dim lastvalue As Decimal = 0.0
    Dim secondarynames(-1) As String
    Dim device As Boolean = False
    Dim devicedata As String
    Dim compare As Integer = 1
    Dim filelocation(-1) As String
    Dim readvalue As String
    Dim xaxis(2) As Double
    Dim yaxis(2) As Double
    Dim firsttime As Boolean = True
    Dim livemarkername(-1) As String
    Dim livemarkerlocation(-1) As Integer
    Dim b As String
    Dim d As Integer
    Dim filename As String
    Dim yvalue As Double = 0.0

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'AddToolStripMenuItem.Enabled = False
        ClearAllMarkersToolStripMenuItem.Enabled = False
        ClearSelectedMarkerToolStripMenuItem.Enabled = False
        'preVISAAddress = VISAAddressEdit.Text
        GlobalVariables.series = New Integer(-1) {}     'Array with zero elements
        GlobalVariables.seriesnames = New String(-1) {}
        GlobalVariables.DeviceName = New String(2) {}   '2 initialises three elements
        GlobalVariables.DeviceAddress = New String(2) {}
        GlobalVariables.DeviceName(0) = "Agilent Technologies N5230A"
        GlobalVariables.DeviceName(1) = "Rohde & Schwarz ZVL6"
        GlobalVariables.DeviceName(2) = "Keysight E5071C"

        'Routine has been reactivated
        If System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT Test Suite\Data.txt") Then
            values = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT Test Suite\Data.txt").Split("|"c)
            If (values.Length = 13) Then
                Try
                    GlobalVariables.xaxismax = CDbl(values(0))
                    GlobalVariables.xaxismin = CDbl(values(1))
                    GlobalVariables.xaxisint = CDbl(values(2))
                    GlobalVariables.yaxismax = CDbl(values(3))
                    GlobalVariables.yaxismin = CDbl(values(4))
                    GlobalVariables.yaxisint = CDbl(values(5))
                Catch ex As Exception
                    GlobalVariables.xaxismax = 6.0
                    GlobalVariables.xaxismin = 0.5
                    GlobalVariables.xaxisint = 0.1
                    GlobalVariables.yaxismax = 0
                    GlobalVariables.yaxismin = -40.0
                    GlobalVariables.yaxisint = 2.0
                End Try
                If (GlobalVariables.xaxismax < 0) Or (GlobalVariables.xaxismax > 100) Or (GlobalVariables.xaxismin < 0) Or (GlobalVariables.xaxismin > 100) Or (GlobalVariables.yaxismax < -1000) Or (GlobalVariables.yaxismax > 1000) Or (GlobalVariables.yaxismin < -1000) Or (GlobalVariables.yaxismin > 1000) Or (GlobalVariables.xaxisint > (GlobalVariables.xaxismax - GlobalVariables.xaxismin)) Or (GlobalVariables.xaxisint <= 0) Or (GlobalVariables.yaxisint > (GlobalVariables.yaxismax - GlobalVariables.yaxismin)) Or (GlobalVariables.yaxisint <= 0) Then
                    GlobalVariables.xaxismax = 6.0
                    GlobalVariables.xaxismin = 0.5
                    GlobalVariables.xaxisint = 0.1
                    GlobalVariables.yaxismax = 0
                    GlobalVariables.yaxismin = -40.0
                    GlobalVariables.yaxisint = 2.0
                End If
                If values(7) = "" Then
                    GlobalVariables.DeviceAddress(0) = "TCPIP0::10.1.100.174::hpib7,16::INSTR"
                Else
                    GlobalVariables.DeviceAddress(0) = values(7)
                End If
                If values(9) = "" Then
                    GlobalVariables.DeviceAddress(1) = "TCPIP0::10.1.100.161::inst0::INSTR"
                Else
                    GlobalVariables.DeviceAddress(1) = values(9)
                End If
                If values(11) = "" Then
                    GlobalVariables.DeviceAddress(2) = "USB0::0x0957::0x0D09::MY46528005::0::INSTR"
                Else
                    GlobalVariables.DeviceAddress(2) = values(11)
                End If
                If values(12) = "" Then
                    GlobalVariables.chartformat = "logmag"
                Else
                    GlobalVariables.chartformat = values(12)
                End If
            Else
                GlobalVariables.DeviceAddress(0) = "TCPIP0::10.1.100.174::hpib7,16::INSTR"
                GlobalVariables.DeviceAddress(1) = "TCPIP0::10.1.100.161::inst0::INSTR"
                GlobalVariables.DeviceAddress(2) = "USB0::0x0957::0x0D09::MY46528005::0::INSTR"
                GlobalVariables.xaxismax = 6.0
                GlobalVariables.xaxismin = 0.5
                GlobalVariables.xaxisint = 0.1
                GlobalVariables.yaxismax = 0
                GlobalVariables.yaxismin = -40.0
                GlobalVariables.yaxisint = 2.0
                GlobalVariables.chartformat = "logmag"
            End If
        Else
            GlobalVariables.DeviceAddress(0) = "TCPIP0::10.1.100.174::hpib7,16::INSTR"
            GlobalVariables.DeviceAddress(1) = "TCPIP0::10.1.100.161::inst0::INSTR"
            GlobalVariables.DeviceAddress(2) = "USB0::0x0957::0x0D09::MY46528005::0::INSTR"
            GlobalVariables.xaxismax = 6.0
            GlobalVariables.xaxismin = 0.5
            GlobalVariables.xaxisint = 0.1
            GlobalVariables.yaxismax = 0
            GlobalVariables.yaxismin = -40.0
            GlobalVariables.yaxisint = 2.0
            GlobalVariables.chartformat = "logmag"
        End If

        dialog.InitialDirectory = "C:\"
        Chart1.Series.Clear()
        Chart1.Series.Add(" ")
        Chart1.ChartAreas("ChartArea1").AxisX.Enabled = AxisEnabled.True        'Keeps the axis when all the plots are deselected.
        Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.True
        Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
        Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
        Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
        'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = GlobalVariables.xaxismin
        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
        'Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"  'Use a Comma to divide by 1000 or Use a % to Multiply by 100
        Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"        '.# to provide one decimal part; For 2 decimal part it is .##
        If GlobalVariables.chartformat = "logmag" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
            LogMagToolStripMenuItem.Checked = True
        ElseIf GlobalVariables.chartformat = "linearmag" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = 0.0
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in value"
            LinearMagToolStripMenuItem.Checked = True
        ElseIf GlobalVariables.chartformat = "phase" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -200
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 200
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = 20
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Phase in degree"
            PhaseToolStripMenuItem.Checked = True
        ElseIf GlobalVariables.chartformat = "real" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Real in value"
            RealToolStripMenuItem.Checked = True
        ElseIf GlobalVariables.chartformat = "imaginary" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Imaginary in value"
            ImaginaryToolStripMenuItem.Checked = True
        End If
        Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dash
        Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dash
        Chart1.Series(" ").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
        For i As Double = 0 To 0.000000001 Step 0.000000001             'Bug found when X values are 0.5 to 3. The plot adds values from 1 to 100 which would be visible from
            array(i) = 0                                                '0.5 to 3
            Chart1.Series(" ").Points.AddXY(i, array(i))
        Next

        DeviceOptionsToolStripMenuItem.Visible = False 'Deactivated
        ToolStripMenuItem5.Visible = False   'Grey line separating Options and Chart Format (Temporarily deactivated for testing)
        ChartTypeToolStripMenuItem.Visible = True  'Temporarily deactivated for testing
        TabControl1.SelectedTab = TabPage1
        'ThirdDevice.Visible = False
        ComparisonModeToolStripMenuItem.Visible = False
        Label1.Visible = False      'Temporarily disabled for testing
        Toggle1.Visible = False
        'readvalue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "Common Documents", Nothing)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        'Routine has been reactivated
        If (Not System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT Test Suite\")) Then
            System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT Test Suite\")
        End If
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT Test Suite\Data.txt", String.Join("|", New String() {GlobalVariables.xaxismax, GlobalVariables.xaxismin, GlobalVariables.xaxisint, GlobalVariables.yaxismax, GlobalVariables.yaxismin, GlobalVariables.yaxisint, GlobalVariables.DeviceName(0), GlobalVariables.DeviceAddress(0), GlobalVariables.DeviceName(1), GlobalVariables.DeviceAddress(1), GlobalVariables.DeviceName(2), GlobalVariables.DeviceAddress(2), GlobalVariables.chartformat}))

        'If xlWorkBook Is Nothing Then
        Me.Close()
        'Else
        '    xlApp.Quit()
        '    Me.Close()
        'End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        'Routine has been reactivated
        If (Not System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT Test Suite\")) Then
            System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT Test Suite\")
        End If
        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT Test Suite\Data.txt", String.Join("|", New String() {GlobalVariables.xaxismax, GlobalVariables.xaxismin, GlobalVariables.xaxisint, GlobalVariables.yaxismax, GlobalVariables.yaxismin, GlobalVariables.yaxisint, GlobalVariables.DeviceName(0), GlobalVariables.DeviceAddress(0), GlobalVariables.DeviceName(1), GlobalVariables.DeviceAddress(1), GlobalVariables.DeviceName(2), GlobalVariables.DeviceAddress(2), GlobalVariables.chartformat}))

        'If xlWorkBook Is Nothing Then
        'Else
        '    xlApp.Quit()
        'End If
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        dialog.Title = "Open File"
        dialog.Multiselect = True
        dialog.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Save")
        'dialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls|CSV (Comma delimited) (*.csv)|*.csv|Touchstone files (*.snp)|*.s*p|All files (*.*)|*.*"
        'dialog.FilterIndex = 4
        dialog.Filter = "Touchstone files (*.snp)|*.s*p"
        dialog.FilterIndex = 1
        dialog.RestoreDirectory = True
        dialog.FileName = ""
        If dialog.ShowDialog() = DialogResult.OK Then
            'If dialog.FileNames.Count > 1 Then '(Permanently enabled Comparison Mode)
            '    ComparisonModeToolStripMenuItem.Checked = True
            'End If
            For Each filename In dialog.FileNames
                If TextBox1.Text.Contains(filename) Then
                    MetroFramework.MetroMessageBox.Show(Me, System.IO.Path.GetFileName(filename) & " is already added to the chart.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    GoTo Checknextfilename
                End If
                extension = System.IO.Path.GetExtension(filename)
                Try
                    ports = System.Text.RegularExpressions.Regex.Replace(extension, "[^\d]", "")    'Remove Characters from a Numeric String
                Catch ex As Exception
                    MetroFramework.MetroMessageBox.Show(Me, "The selected file is not a compatible Touchstone file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    GoTo Checknextfilename
                End Try
                NewToolRoutine()
Checknextfilename:
            Next filename
        End If
    End Sub

    Sub NewToolRoutine()
        If GlobalVariables.series.Length = 0 Then
            'If ComparisonModeToolStripMenuItem.Checked = False Or (ComparisonModeToolStripMenuItem.Checked = True AndAlso compare = 1 AndAlso device = False) Then
            colourcounter = 1
            ClearMarkers()
            Chart1.Series.Clear()
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
            'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = GlobalVariables.xaxismin
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
            Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.True
            If GlobalVariables.chartformat = "logmag" Then
                Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
                Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
                Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
                Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
            ElseIf GlobalVariables.chartformat = "linearmag" Then
                Chart1.ChartAreas("ChartArea1").AxisY.Minimum = 0.0
                Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in value"
            ElseIf GlobalVariables.chartformat = "phase" Then
                Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -200
                Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 200
                Chart1.ChartAreas("ChartArea1").AxisY.Interval = 20
                Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                Chart1.ChartAreas("ChartArea1").AxisY.Title = "Phase in degree"
            ElseIf GlobalVariables.chartformat = "real" Then
                Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
                Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                Chart1.ChartAreas("ChartArea1").AxisY.Title = "Real in value"
            ElseIf GlobalVariables.chartformat = "imaginary" Then
                Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
                Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                Chart1.ChartAreas("ChartArea1").AxisY.Title = "Imaginary in value"
            End If
            Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.False
            checkboxnum = 1
            TextBox1.Text = ""
        End If
        column = ((Math.Pow(ports, 2) * 2) + 1)

        'y2max1 = 0
        'y2min1 = 0
        'x2max = 0
        Erase names
        names = New String(-1) {}
        Try
            matrix = "full"
            Using sr As New StreamReader(filename)

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
                    ElseIf line = "" Then
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

            Select Case frequnit
                Case "hz"
                    For i As Integer = 0 To row - 1
                        freq1(i) = table(i, 0) / 1000000000
                    Next
                Case "khz"
                    For i As Integer = 0 To row - 1
                        freq1(i) = table(i, 0) / 1000000
                    Next
                Case "mhz"
                    For i As Integer = 0 To row - 1
                        freq1(i) = table(i, 0) / 1000
                    Next
                Case "ghz"
                    For i As Integer = 0 To row - 1
                        freq1(i) = table(i, 0)
                    Next
            End Select
            'xmax = freq1.Max
            'xmin = freq1.Min

            'If GlobalVariables.autobutton = True Then
            '    xaxisadjust()
            'End If

            'xmax = table(row - 1, 0)
            'xmin = table(0, 0)
            'Select Case frequnit
            '    Case "hz"
            '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"    'Two decimal adjustment (if required)
            '        'Chart1.ChartAreas("ChartArea1").AxisX.Interval = 100000000                  'X axis interval adjustment
            '        'If Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 100000000 <> 0 Then    'X axis maximum adjustment
            '        '    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = ((Chart1.ChartAreas("ChartArea1").AxisX.Maximum - (Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 100000000)) + 100000000)
            '        'End If
            '        'While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
            '        '    Chart1.ChartAreas("ChartArea1").AxisX.Interval += 100000000
            '        'End While
            '    Case "khz"
            '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}"
            '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000
            '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000
            '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000
            '        'If Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 100000 <> 0 Then
            '        '    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = ((Chart1.ChartAreas("ChartArea1").AxisX.Maximum - (Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 100000)) + 100000)
            '        'End If
            '        'While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
            '        '    Chart1.ChartAreas("ChartArea1").AxisX.Interval += 100000
            '        'End While
            '    Case "mhz"
            '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}"
            '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000
            '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000
            '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000
            '        'If Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 100 <> 0 Then
            '        '    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = ((Chart1.ChartAreas("ChartArea1").AxisX.Maximum - (Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 100)) + 100)
            '        'End If
            '        'While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
            '        '    Chart1.ChartAreas("ChartArea1").AxisX.Interval += 100
            '        'End While
            '    Case "ghz"
            '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
            '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000000
            '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000000
            '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000000
            '        'If Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 0.1 <> 0 Then
            '        '    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = ((Chart1.ChartAreas("ChartArea1").AxisX.Maximum - (Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod 0.1)) + 0.1)
            '        'End If
            '        'While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 15
            '        '    Chart1.ChartAreas("ChartArea1").AxisX.Interval += 0.1
            '        'End While
            'End Select
            'If GlobalVariables.autobutton = True Then
            '    xaxisadjust()
            'End If
            'For i As Integer = 0 To row - 1
            '    freq1(i) = table(i, 0)
            '    'Console.WriteLine(freq1(i))
            'Next
            'If ComparisonModeToolStripMenuItem.Checked = True Then '(Permanently enabled Comparison Mode)
            z = GlobalVariables.seriesnames.Length
            If matrix = "lower" Or matrix = "upper" Then
                If z = 0 Then
                    GlobalVariables.seriesnames = New String((ports * (ports + 1) / 2) - 1) {}
                    GlobalVariables.series = New Integer((ports * (ports + 1) / 2) - 1) {}
                Else
                    System.Array.Resize(Of String)(GlobalVariables.seriesnames, (z + (ports * (ports + 1) / 2)))   'Need to one more than the normal ReDim Preserve
                    System.Array.Resize(Of Integer)(GlobalVariables.series, (z + (ports * (ports + 1) / 2)))
                End If

            Else
                If z = 0 Then
                    GlobalVariables.seriesnames = New String((ports * ports) - 1) {}
                    GlobalVariables.series = New Integer((ports * ports) - 1) {}
                Else
                    System.Array.Resize(Of String)(GlobalVariables.seriesnames, (z + ((ports * ports))))  'Need to one more than the normal ReDim Preserve
                    System.Array.Resize(Of Integer)(GlobalVariables.series, (z + (ports * ports)))
                End If

            End If
            'Else
            '    If matrix <> "lower" Or matrix <> "upper" Then
            '        GlobalVariables.seriesnames = New String((ports * ports) - 1) {}
            '        GlobalVariables.series = New Integer((ports * ports) - 1) {}
            '    Else
            '        GlobalVariables.seriesnames = New String((ports * (ports + 1) / 2) - 1) {}
            '        GlobalVariables.series = New Integer((ports * (ports + 1) / 2) - 1) {}
            '    End If
            'End If
            'ymax = 0
            'ymin = 0
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
                    'z = 0
                    For j As Integer = 0 To row - 1
                        'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = GlobalVariables.xaxismin
                        Select Case parameter
                            Case "s"
                                If LogMagToolStripMenuItem.Checked = True Then
                                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
                                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
                                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
                                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
                                    Select Case format
                                        Case "db"
                                            para1(j) = table(j, x)
                                        Case "ma"
                                            para1(j) = (20 * Math.Log10(table(j, x)))
                                        Case "ri"
                                            para1(j) = (10 * Math.Log10((Math.Pow(table(j, x), 2) + Math.Pow(table(j, x + 1), 2))))
                                    End Select
                                ElseIf LinearMagToolStripMenuItem.Checked = True Then
                                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = 0.0
                                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in value"
                                    Select Case format
                                        Case "db"
                                            para1(j) = Math.Pow(10, (table(j, x) / 20))
                                        Case "ma"
                                            para1(j) = table(j, x)
                                        Case "ri"
                                            para1(j) = Math.Sqrt((Math.Pow(table(j, x), 2) + Math.Pow(table(j, x + 1), 2)))
                                    End Select
                                ElseIf PhaseToolStripMenuItem.Checked = True Then
                                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -200
                                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 200
                                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 20
                                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Phase in degree"
                                    Select Case format
                                        Case "db"
                                            para1(j) = table(j, x + 1)
                                        Case "ma"
                                            para1(j) = table(j, x + 1)
                                        Case "ri"
                                            para1(j) = Math.Atan2(table(j, x + 1), table(j, x)) * 180 / Math.PI
                                    End Select
                                ElseIf UnwrappedPhaseToolStripMenuItem.Checked = True Then
                                ElseIf PolarToolStripMenuItem.Checked = True Then
                                    Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 0
                                    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 360
                                    Chart1.ChartAreas("ChartArea1").AxisX.Interval = 45
                                    'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = 90
                                    'Chart1.ChartAreas("ChartArea1").AlignmentOrientation = 
                                    'Chart1.ChartAreas("ChartArea1").AxisX.IsReversed = True
                                    Chart1.ChartAreas("ChartArea1").AxisX.Title = "Magnitude in value"
                                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = 0
                                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.2
                                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.2
                                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Phase in degree"
                                    Select Case format
                                        Case "db"
                                            freq1(j) = table(j, x + 1)
                                            para1(j) = Math.Pow(10, (table(j, x) / 20))
                                        Case "ma"
                                            freq1(j) = table(j, x + 1)
                                            para1(j) = table(j, x)
                                        Case "ri"
                                            freq1(j) = Math.Atan2(table(j, x + 1), table(j, x)) * 180 / Math.PI
                                            para1(j) = Math.Sqrt((Math.Pow(table(j, x), 2) + Math.Pow(table(j, x + 1), 2)))
                                    End Select
                                ElseIf SmithChartToolStripMenuItem.Checked = True Then
                                ElseIf InverseSmithChartToolStripMenuItem.Checked = True Then
                                ElseIf GroupDelayToolStripMenuItem.Checked = True Then
                                ElseIf SWRToolStripMenuItem.Checked = True Then
                                ElseIf RealToolStripMenuItem.Checked = True Then
                                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
                                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Real in value"
                                    Select Case format
                                        Case "db"
                                            para1(j) = Math.Pow(10, (table(j, x) / 20)) * Math.Cos(table(j, x + 1) * Math.PI / 180)
                                        Case "ma"
                                            para1(j) = table(j, x) * Math.Cos(table(j, x + 1) * Math.PI / 180)
                                        Case "ri"
                                            para1(j) = table(j, x)
                                    End Select
                                ElseIf ImaginaryToolStripMenuItem.Checked = True Then
                                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
                                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Imaginary in value"
                                    Select Case format
                                        Case "db"
                                            para1(j) = Math.Pow(10, (table(j, x) / 20)) * Math.Sin(table(j, x + 1) * Math.PI / 180)
                                        Case "ma"
                                            para1(j) = table(j, x) * Math.Sin(table(j, x + 1) * Math.PI / 180)
                                        Case "ri"
                                            para1(j) = table(j, x + 1)
                                    End Select
                                End If
                            Case "y"
                            Case "z"
                            Case "h"
                            Case "g"
                        End Select
                        'If CStr(para1(j)) <> "-Infinity" Then
                        '    'If ymax < para1(j) Then
                        '    '    ymax = para1(j)
                        '    'End If
                        '    'If ymin > para1(j) Then
                        '    '    ymin = para1(j)
                        '    'End If
                        'Else
                        '    z = 1
                        'End If
                        If CStr(para1(j)) = "-Infinity" Then
                            para1(j) = -1000
                        End If
                    Next
                    x += 2
                    'If z = 0 Then
                    'If ymax < para1.Max Then
                    '    ymax = para1.Max
                    'End If
                    'If ymin > para1.Min Then
                    '    ymin = para1.Min
                    'End If
                    'If GlobalVariables.autobutton = True Then
                    '    yaxisadjust()
                    'End If
                    'If ComparisonModeToolStripMenuItem.Checked = True Then '(Permanently enabled Comparison Mode)
                    Chart1.Series.Add("S(" & a & "," & b & ") #" & compare)
                    Chart1.Series("S(" & a & "," & b & ") #" & compare).BorderWidth = 2
                    'Chart1.Series("S(" & a & "," & b & ")").Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                    colourpicker()
                    Chart1.Series("S(" & a & "," & b & ") #" & compare).Color = c
                    Chart1.Series("S(" & a & "," & b & ") #" & compare).Points.DataBindXY(freq1, para1)
                    If PolarToolStripMenuItem.Checked = True Or SmithChartToolStripMenuItem.Checked = True Or InverseSmithChartToolStripMenuItem.Checked = True Then
                        Chart1.Series("S(" & a & "," & b & ") #" & compare).ChartType = DataVisualization.Charting.SeriesChartType.Polar
                    Else
                        Chart1.Series("S(" & a & "," & b & ") #" & compare).ChartType = DataVisualization.Charting.SeriesChartType.Line
                    End If
                    GlobalVariables.seriesnames(y + z) = "S(" & a & "," & b & ") #" & compare
                    GlobalVariables.series(y + z) = 1
                    'Else
                    '    Chart1.Series.Add("S(" & a & "," & b & ")")
                    '    Chart1.Series("S(" & a & "," & b & ")").BorderWidth = 2
                    '    'Chart1.Series("S(" & a & "," & b & ")").Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                    '    colourpicker()
                    '    Chart1.Series("S(" & a & "," & b & ")").Color = c
                    '    Chart1.Series("S(" & a & "," & b & ")").Points.DataBindXY(freq1, para1)
                    '    If PolarToolStripMenuItem.Checked = True Or SmithChartToolStripMenuItem.Checked = True Or InverseSmithChartToolStripMenuItem.Checked = True Then
                    '        Chart1.Series("S(" & a & "," & b & ")").ChartType = DataVisualization.Charting.SeriesChartType.Polar
                    '    Else
                    '        Chart1.Series("S(" & a & "," & b & ")").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    '    End If
                    '    GlobalVariables.seriesnames(y) = "S(" & a & "," & b & ")"
                    '    GlobalVariables.series(y) = 1
                    'End If
                    y += 1
                    'Else
                    '    MetroFramework.MetroMessageBox.Show(Me, "S(" & a & "," & b & ") has been skipped due to a Math error", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'End If
                Next
            Next
            If GlobalVariables.autobutton = True Then
                axisvalues()
            End If
            Erase freq1         '   Releasing memory
            Erase para1
            Erase table
            'Chart1.Series("S(1,1)").Enabled = False
            'AddToolStripMenuItem.Enabled = True
            newtoolbar = True
            db = False
            device = False
            'If ComparisonModeToolStripMenuItem.Checked = True Then '(Permanently enabled Comparison Mode)
            TextBox1.Text &= compare & ". " & filename & vbCrLf
            compare += 1
            'Else
            '    TextBox1.Text = filename & vbCrLf
            'End If
        Catch Ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub axisvalues()
        xmax = 0
        xmin = 100
        ymax = -1000
        ymin = 1000
        y2max = -1000
        y2min = 1000
        For i As Integer = 0 To GlobalVariables.seriesnames.Length - 1
            If GlobalVariables.series(i) = 1 Then
                line = Chart1.Series(GlobalVariables.seriesnames(i)).Points.FindMinByValue("X", 0).ToString()
                If CDbl(line.Substring(line.IndexOf("X") + 2, line.IndexOf(",", line.IndexOf("X")) - line.IndexOf("X") - 2)) < xmin Then
                    xmin = CDbl(line.Substring(line.IndexOf("X") + 2, line.IndexOf(",", line.IndexOf("X")) - line.IndexOf("X") - 2))
                    xmin = Math.Round(xmin, 2)
                    If xmin Mod 0.5 <> 0 Then
                        xmin = xmin - (xmin Mod 0.5)
                    End If
                End If
                line = Chart1.Series(GlobalVariables.seriesnames(i)).Points.FindMaxByValue("X", 0).ToString()
                If CDbl(line.Substring(line.IndexOf("X") + 2, line.IndexOf(",", line.IndexOf("X")) - line.IndexOf("X") - 2)) > xmax Then
                    xmax = CDbl(line.Substring(line.IndexOf("X") + 2, line.IndexOf(",", line.IndexOf("X")) - line.IndexOf("X") - 2))
                    xmax = Math.Round(xmax, 2)
                    If xmax Mod 0.5 <> 0 Then
                        xmax = xmax + (0.5 - (xmax Mod 0.5))
                    End If
                End If
                If Chart1.Series(GlobalVariables.seriesnames(i)).YAxisType = 0 Then
                    line = Chart1.Series(GlobalVariables.seriesnames(i)).Points.FindMinByValue().ToString()
                    If CDbl(line.Substring(line.IndexOf("Y") + 2, line.IndexOf("}", line.IndexOf("Y")) - line.IndexOf("Y") - 2)) < ymin Then
                        ymin = CDbl(line.Substring(line.IndexOf("Y") + 2, line.IndexOf("}", line.IndexOf("Y")) - line.IndexOf("Y") - 2))
                        ymin = Math.Round(ymin, 2)
                        If ymin > 0 Then
                            If ymin Mod 5 <> 0 Then
                                ymin = ymin - (ymin Mod 5)
                            End If
                        ElseIf ymin < 0 Then
                            If ymin Mod 5 <> 0 Then
                                ymin = ymin - (5 - (Math.Abs(ymin) Mod 5))
                            End If
                        End If
                        ymin = Math.Round(ymin, 0)
                    End If
                    line = Chart1.Series(GlobalVariables.seriesnames(i)).Points.FindMaxByValue().ToString()
                    If CDbl(line.Substring(line.IndexOf("Y") + 2, line.IndexOf("}", line.IndexOf("Y")) - line.IndexOf("Y") - 2)) > ymax Then
                        ymax = CDbl(line.Substring(line.IndexOf("Y") + 2, line.IndexOf("}", line.IndexOf("Y")) - line.IndexOf("Y") - 2))
                        ymax = Math.Round(ymax, 2)
                        If ymax > 0 Then
                            If ymax Mod 5 <> 0 Then
                                ymax = ymax + (5 - (ymax Mod 5))
                            End If
                        ElseIf ymax < 0 Then
                            If ymax Mod 5 <> 0 Then
                                ymax = ymax + (Math.Abs(ymax) Mod 5)
                            End If
                        End If
                        ymax = Math.Round(ymax, 0)
                    End If
                Else
                    line = Chart1.Series(GlobalVariables.seriesnames(i)).Points.FindMinByValue().ToString()
                    If CDbl(line.Substring(line.IndexOf("Y") + 2, line.IndexOf("}", line.IndexOf("Y")) - line.IndexOf("Y") - 2)) < y2min Then
                        y2min = CDbl(line.Substring(line.IndexOf("Y") + 2, line.IndexOf("}", line.IndexOf("Y")) - line.IndexOf("Y") - 2))
                        y2min = Math.Round(y2min, 2)
                        If GlobalVariables.plot = "efficiency" Then
                            If y2min Mod 5 <> 0 Then
                                y2min = y2min - (y2min Mod 5)
                            End If
                            y2min = Math.Round(y2min, 0)
                        End If
                    End If
                    line = Chart1.Series(GlobalVariables.seriesnames(i)).Points.FindMaxByValue().ToString()
                    If CDbl(line.Substring(line.IndexOf("Y") + 2, line.IndexOf("}", line.IndexOf("Y")) - line.IndexOf("Y") - 2)) > y2max Then
                        y2max = CDbl(line.Substring(line.IndexOf("Y") + 2, line.IndexOf("}", line.IndexOf("Y")) - line.IndexOf("Y") - 2))
                        y2max = Math.Round(y2max, 2)
                        If GlobalVariables.plot = "efficiency" Then
                            If y2max Mod 5 <> 0 Then
                                y2max = y2max + (5 - (y2max Mod 5))
                            End If
                            y2max = Math.Round(y2max, 0)
                        End If
                    End If
                End If
            End If
        Next
        If xmax = xmin Then     'Routine to check if the plot has only a single data point
            If xmin = 0 Then
                xmax += 0.5
            Else
                xmin -= 0.5
                xmax += 0.5
            End If
        End If
        If ymax = ymin Then
            ymax += 5
            ymin -= 5
        End If
        If y2max = y2min Then
            If y2max = 100 Then
                y2min -= 5
            ElseIf y2min = 0 Then
                y2max += 5
            Else
                y2max += 5
                y2min -= 5
            End If
        End If
        If xmax = 0 AndAlso xmin = 100 Then             'To check if the Y or Y2 axis series are disabled or not
            xmax = Chart1.ChartAreas("ChartArea1").AxisX.Maximum
            xmin = Chart1.ChartAreas("ChartArea1").AxisX.Minimum
        End If
        If ymax = -1000 AndAlso ymin = 1000 Then        'Y and Y2 axis needs to be disabled/enabled depending on the value
            Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.False
            ymax = Chart1.ChartAreas("ChartArea1").AxisY.Maximum
            ymin = Chart1.ChartAreas("ChartArea1").AxisY.Minimum
        Else
            Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.True
        End If
        If y2max = -1000 AndAlso y2min = 1000 Then
            Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.False
            y2max = Chart1.ChartAreas("ChartArea1").AxisY2.Maximum
            y2min = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum
        Else
            Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True
        End If
        axisadjust()
    End Sub

    Sub axisadjust()
        Chart1.ChartAreas("ChartArea1").AxisX.Maximum = xmax
        Chart1.ChartAreas("ChartArea1").AxisX.Minimum = xmin
        a = Chart1.ChartAreas("ChartArea1").AxisX.Interval
        While ((Chart1.ChartAreas("ChartArea1").AxisX.Maximum - Chart1.ChartAreas("ChartArea1").AxisX.Minimum) / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 25
            Chart1.ChartAreas("ChartArea1").AxisX.Interval += a
        End While
        If Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.True Then
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = ymax
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = ymin
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = 5
            While (((Chart1.ChartAreas("ChartArea1").AxisY.Maximum - Chart1.ChartAreas("ChartArea1").AxisY.Minimum) / Chart1.ChartAreas("ChartArea1").AxisY.Interval) > 10)
                Chart1.ChartAreas("ChartArea1").AxisY.Interval += 5
            End While   'Loop to keep the interval a multiple of 5
        End If
        If Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True Then

            Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Clear()
            Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = y2max
            Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = y2min
            Chart1.ChartAreas("ChartArea1").AxisY2.Interval = (y2max - y2min) / 10
            If GlobalVariables.plot = "efficiency" Then
                For i As Double = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum To Chart1.ChartAreas("ChartArea1").AxisY2.Maximum Step Chart1.ChartAreas("ChartArea1").AxisY2.Interval
                    If i = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum Then
                        If i = 0 Then
                            Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Add(i, i + 0.01, CStr(i))
                        Else
                            Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Add(i, i + 0.01, CStr(i) + " (" + CStr(Math.Round(10 * Math.Log10(i / 100), 1)) + " in dB)")
                        End If
                    End If
                    Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Add(i, i - 0.01, CStr(i) + " (" + CStr(Math.Round(10 * Math.Log10(i / 100), 1)) + " in dB)")
                Next
            Else
                For i As Double = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum To (Chart1.ChartAreas("ChartArea1").AxisY2.Maximum + Chart1.ChartAreas("ChartArea1").AxisY2.Interval) Step Chart1.ChartAreas("ChartArea1").AxisY2.Interval
                    If i = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum Then
                        Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Add(i, i + 0.01, CStr(Math.Round(i, 2)))
                    End If
                    Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Add(i, i - 0.01, CStr(Math.Round(i, 2)))
                Next
            End If
        End If
    End Sub

    'Sub xaxisadjust()
    '    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = xmax
    '    If Chart1.ChartAreas("ChartArea1").AxisX.Minimum > xmin Then
    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 0
    '    End If
    '    a = Chart1.ChartAreas("ChartArea1").AxisX.Interval
    '    'While Chart1.ChartAreas("ChartArea1").AxisX.Minimum >= table(0, 0)
    '    '    Chart1.ChartAreas("ChartArea1").AxisX.Minimum -= a
    '    'End While
    '    'If table(0, 0) < Chart1.ChartAreas("ChartArea1").AxisX.Minimum Then
    '    '    Chart1.ChartAreas("ChartArea1").AxisX.Minimum = table(0, 0)
    '    'End If
    '    'If Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod a <> 0 Then    'X axis maximum adjustment
    '    '    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = ((Chart1.ChartAreas("ChartArea1").AxisX.Maximum + (Chart1.ChartAreas("ChartArea1").AxisX.Maximum Mod a)) + a)
    '    'End If
    '    While (Chart1.ChartAreas("ChartArea1").AxisX.Maximum / Chart1.ChartAreas("ChartArea1").AxisX.Interval) > 21
    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval += a
    '    End While
    'End Sub

    'Sub yaxisadjust()
    '    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 5
    '    While ymin < Chart1.ChartAreas("ChartArea1").AxisY.Minimum
    '        Chart1.ChartAreas("ChartArea1").AxisY.Minimum -= 5
    '    End While
    '    While ymax > Chart1.ChartAreas("ChartArea1").AxisY.Maximum
    '        Chart1.ChartAreas("ChartArea1").AxisY.Maximum += 5
    '    End While
    '    'While (((Chart1.ChartAreas("ChartArea1").AxisY.Maximum - Chart1.ChartAreas("ChartArea1").AxisY.Minimum) / 5) Mod 5 <> 0)
    '    '    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = Chart1.ChartAreas("ChartArea1").AxisY.Minimum - 5
    '    'End While   'Loop to keep the interval a multiple of 5
    '    While (((Chart1.ChartAreas("ChartArea1").AxisY.Maximum - Chart1.ChartAreas("ChartArea1").AxisY.Minimum) / Chart1.ChartAreas("ChartArea1").AxisY.Interval) > 10)
    '        Chart1.ChartAreas("ChartArea1").AxisY.Interval += 5
    '    End While   'Loop to keep the interval a multiple of 5
    'End Sub

    'Sub newyaxisadjust()
    '    Dim newymin As Double = 0.0
    '    For i As Integer = 0 To GlobalVariables.series.Length - 1
    '        If GlobalVariables.series(i) = 1 Then
    '            line = Chart1.Series(GlobalVariables.seriesnames(i)).Points.FindMinByValue().ToString()
    '            If CDbl(line.Substring(line.IndexOf("Y") + 2, line.IndexOf("}", line.IndexOf("Y")) - line.IndexOf("Y") - 2)) < newymin Then
    '                newymin = CDbl(line.Substring(line.IndexOf("Y") + 2, line.IndexOf("}", line.IndexOf("Y")) - line.IndexOf("Y") - 2))
    '                newymin = Math.Round(newymin, 0)
    '                If newymin Mod 5 <> 0 Then
    '                    newymin = newymin - (5 + (newymin Mod 5))
    '                End If
    '            End If
    '        End If
    '    Next
    '    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = newymin
    '    While ymax > Chart1.ChartAreas("ChartArea1").AxisY.Maximum
    '        Chart1.ChartAreas("ChartArea1").AxisY.Maximum += 5
    '    End While
    '    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 5
    '    While (((Chart1.ChartAreas("ChartArea1").AxisY.Maximum - Chart1.ChartAreas("ChartArea1").AxisY.Minimum) / Chart1.ChartAreas("ChartArea1").AxisY.Interval) > 10)
    '        Chart1.ChartAreas("ChartArea1").AxisY.Interval += 5
    '    End While   'Loop to keep the interval a multiple of 5
    'End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If device = False Then
            dialog1.Filter = "JPEG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp"
            dialog1.FilterIndex = 3
        Else
            If ports = 1 Then
                dialog1.Filter = "JPEG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp|Touchstone (*.s1p)|*.s1p"
            ElseIf ports = 2 Then
                dialog1.Filter = "JPEG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp|Touchstone (*.s2p)|*.s2p"
            ElseIf ports = 3 Then
                dialog1.Filter = "JPEG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp|Touchstone (*.s3p)|*.s3p"
            Else
                dialog1.Filter = "JPEG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp|Touchstone (*.s4p)|*.s4p"
            End If
            dialog1.FilterIndex = 5
        End If
        dialog1.FileName = ""

        Dim graph As Graphics = Nothing         ' Image capture if the CheckedListBox is not empty
        Dim frmleft As System.Drawing.Point = Me.Bounds.Location
        Dim img As New Bitmap(Me.Bounds.Width + 0, Me.Bounds.Height + 0)
        graph = Graphics.FromImage(img)
        Dim screenx As Integer = frmleft.X
        Dim screeny As Integer = frmleft.Y
        graph.CopyFromScreen(screenx - 0, screeny - 0, 0, 0, img.Size)
        Me.BackgroundImageLayout = ImageLayout.Stretch
        Me.BackgroundImage = img

        If (dialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
            If dialog1.FilterIndex = 5 Then
                System.IO.File.WriteAllText(dialog1.FileName, devicedata)
                If ports = 1 Then
                    System.IO.File.Move(dialog1.FileName, dialog1.FileName.Replace(".txt", ".s1p"))
                ElseIf ports = 2 Then
                    System.IO.File.Move(dialog1.FileName, dialog1.FileName.Replace(".txt", ".s2p"))
                ElseIf ports = 3 Then
                    System.IO.File.Move(dialog1.FileName, dialog1.FileName.Replace(".txt", ".s3p"))
                Else
                    System.IO.File.Move(dialog1.FileName, dialog1.FileName.Replace(".txt", ".s4p"))
                End If
            Else

                If CheckedListBox1.Items.Count > 0 Then
                    img.Save(dialog1.FileName, System.Drawing.Imaging.ImageFormat.Png)
                Else
                    Chart1.SaveImage(dialog1.FileName, System.Drawing.Imaging.ImageFormat.Png)
                End If
            End If

        End If
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        xaxis(0) = GlobalVariables.xaxismax
        xaxis(1) = GlobalVariables.xaxismin
        xaxis(2) = GlobalVariables.xaxisint
        yaxis(0) = GlobalVariables.yaxismax
        yaxis(1) = GlobalVariables.yaxismin
        yaxis(2) = GlobalVariables.yaxisint
        GlobalVariables.yaxismax = Chart1.ChartAreas("ChartArea1").AxisY.Maximum
        GlobalVariables.yaxismin = Chart1.ChartAreas("ChartArea1").AxisY.Minimum
        GlobalVariables.yaxisint = Chart1.ChartAreas("ChartArea1").AxisY.Interval
        GlobalVariables.y2axis = False
        If GlobalVariables.series.Length <> 0 Then
            For i As Integer = 0 To GlobalVariables.series.Length - 1
                If Chart1.Series(GlobalVariables.seriesnames(i)).YAxisType = 1 Then
                    GlobalVariables.y2axis = True
                    GlobalVariables.y2axismax = Chart1.ChartAreas("ChartArea1").AxisY2.Maximum
                    GlobalVariables.y2axismin = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum
                    GlobalVariables.y2axisint = Chart1.ChartAreas("ChartArea1").AxisY2.Interval
                End If
            Next
        End If
        'If Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True Then
        '    GlobalVariables.y2axis = True
        '    GlobalVariables.y2axismax = Chart1.ChartAreas("ChartArea1").AxisY2.Maximum
        '    GlobalVariables.y2axismin = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum
        '    GlobalVariables.y2axisint = Chart1.ChartAreas("ChartArea1").AxisY2.Interval
        'Else
        '    GlobalVariables.y2axis = False
        'End If
        'If Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}" Then
        '    GlobalVariables.xaxismax = Chart1.ChartAreas("ChartArea1").AxisX.Maximum / 1000000000
        '    GlobalVariables.xaxismin = Chart1.ChartAreas("ChartArea1").AxisX.Minimum / 1000000000
        '    GlobalVariables.xaxisint = Chart1.ChartAreas("ChartArea1").AxisX.Interval / 1000000000
        'ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}" Then
        '    GlobalVariables.xaxismax = Chart1.ChartAreas("ChartArea1").AxisX.Maximum / 1000000
        '    GlobalVariables.xaxismin = Chart1.ChartAreas("ChartArea1").AxisX.Minimum / 1000000
        '    GlobalVariables.xaxisint = Chart1.ChartAreas("ChartArea1").AxisX.Interval / 1000000
        'ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}" Then
        '    GlobalVariables.xaxismax = Chart1.ChartAreas("ChartArea1").AxisX.Maximum / 1000
        '    GlobalVariables.xaxismin = Chart1.ChartAreas("ChartArea1").AxisX.Minimum / 1000
        '    GlobalVariables.xaxisint = Chart1.ChartAreas("ChartArea1").AxisX.Interval / 1000
        'Else
        GlobalVariables.xaxismax = Chart1.ChartAreas("ChartArea1").AxisX.Maximum
        GlobalVariables.xaxismin = Chart1.ChartAreas("ChartArea1").AxisX.Minimum
        GlobalVariables.xaxisint = Chart1.ChartAreas("ChartArea1").AxisX.Interval
        'End If
        'If (ports = 0) Then
        '    GlobalVariables.ports = 0
        'Else
        '    GlobalVariables.ports = ports
        'End If
        'AllocConsole() 'show console
        'For i As Integer = 0 To GlobalVariables.seriesnames.Length - 1
        '    Console.WriteLine(GlobalVariables.seriesnames(i))
        'Next
        Form2.ShowDialog()
        If GlobalVariables.okbutton = "ok" Then
            If GlobalVariables.autobutton = False Then
                Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
                Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
                Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
                'If Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}" Then
                '    Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin * 1000000000
                '    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax * 1000000000
                '    Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint * 1000000000
                'ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}" Then
                '    Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin * 1000000
                '    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax * 1000000
                '    Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint * 1000000
                'ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}" Then
                '    Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin * 1000
                '    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax * 1000
                '    Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint * 1000
                'Else
                Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
                Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
                'End If
                If GlobalVariables.y2axis = True Then
                    Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = GlobalVariables.y2axismax
                    Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = GlobalVariables.y2axismin
                    Chart1.ChartAreas("ChartArea1").AxisY2.Interval = GlobalVariables.y2axisint
                    y2axisadjust()
                End If
                x = 0
                y = 0
                If GlobalVariables.series.Length <> 0 Then
                    For i As Integer = 0 To GlobalVariables.series.Length - 1
                        If GlobalVariables.series(i) = 1 Then
                            If Chart1.Series(GlobalVariables.seriesnames(i)).YAxisType = 0 Then
                                x = 1
                            End If
                            If Chart1.Series(GlobalVariables.seriesnames(i)).YAxisType = 1 Then
                                y = 1
                            End If
                        End If
                    Next
                    If x = 0 Then
                        Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.False
                    Else
                        Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.True
                    End If
                    If y = 0 Then
                        Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.False
                    Else
                        Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True
                    End If
                End If
            End If
            If GlobalVariables.series.Length <> 0 Then
                'If GlobalVariables.ports <> 0 Then
                For i As Integer = 0 To GlobalVariables.series.Length - 1
                    'If GlobalVariables.seriesnames(i) <> "" Then
                    If GlobalVariables.series(i) = 1 Then
                        Chart1.Series(GlobalVariables.seriesnames(i)).Enabled = True
                    Else
                        Chart1.Series(GlobalVariables.seriesnames(i)).Enabled = False
                    End If
                    'End If
                Next
                If GlobalVariables.autobutton = True Then
                    axisvalues()
                    'Exit Sub
                    'If GlobalVariables.ports <> 0 Or db = True Then
                    '    xaxisadjust()
                    '    'yaxisadjust()
                    '    newyaxisadjust()
                    'End If
                    'If Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True Then
                    '    'x2axisadjust()
                    '    xaxisadjust()
                    '    y2axisadjust()
                    'End If
                    GlobalVariables.xaxismax = xaxis(0)
                    GlobalVariables.xaxismin = xaxis(1)
                    GlobalVariables.xaxisint = xaxis(2)
                    GlobalVariables.yaxismax = yaxis(0)
                    GlobalVariables.yaxismin = yaxis(1)
                    GlobalVariables.yaxisint = yaxis(2)
                End If
            End If
        End If
        If LogMagToolStripMenuItem.Checked = False Then
            GlobalVariables.yaxismax = yaxis(0)
            GlobalVariables.yaxismin = yaxis(1)
            GlobalVariables.yaxisint = yaxis(2)
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
                If checkboxnum > 50 Then
                    checkboxnum = 1
                    CheckedListBox1.Items.Clear()
                    For i As Integer = 0 To 49
                        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = ""
                        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.None
                    Next
                    ClearAllMarkersToolStripMenuItem.Enabled = False
                    'seriesname = Nothing
                    'seriespointindex = Nothing
                End If
                RemoveHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
                'If Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}" Then
                '    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(selectedDataPoint.XValue / 1000000000, 3) & ", Y=" & Math.Round(selectedDataPoint.YValues(0), 3), isChecked:=True)
                'ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}" Then
                '    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(selectedDataPoint.XValue / 1000000, 3) & ", Y=" & Math.Round(selectedDataPoint.YValues(0), 3), isChecked:=True)
                'ElseIf Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}" Then
                '    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(selectedDataPoint.XValue / 1000, 3) & ", Y=" & Math.Round(selectedDataPoint.YValues(0), 3), isChecked:=True)
                'Else
                CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(selectedDataPoint.XValue, 3) & ", Y=" & Math.Round(selectedDataPoint.YValues(0), 3), isChecked:=True)
                'End If
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
                If Toggle1.Checked = True AndAlso (FirstDevice.Checked = True Or SecondDevice.Checked = True Or ThirdDevice.Checked = True) Then
                    System.Array.Resize(Of String)(livemarkername, livemarkername.Length + 1)  'Need to one more than the normal ReDim Preserve
                    System.Array.Resize(Of Integer)(livemarkerlocation, livemarkerlocation.Length + 1)
                    livemarkername(livemarkername.Length - 1) = selectedSeries.Name
                    livemarkerlocation(livemarkerlocation.Length - 1) = selectedPointIndex
                End If
            End If
        End If
        result = Nothing
        selectedDataPoint = Nothing
    End Sub

    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        dialog.Title = "Open File"
        dialog.Multiselect = True
        dialog.InitialDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Save")
        dialog.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls"
        dialog.FilterIndex = 1
        dialog.RestoreDirectory = True
        dialog.FileName = ""
        If dialog.ShowDialog() = DialogResult.OK Then
            Try
                For Each filename In dialog.FileNames
                    If TextBox1.Text.Contains(filename) Then
                        MetroFramework.MetroMessageBox.Show(Me, System.IO.Path.GetFileName(filename) & " is already added to the chart.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        GoTo Endfilename
                    End If
                    AddToolRoutine()
Endfilename:
                Next filename
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Sub AddToolRoutine()
        Dim stream As FileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        If System.IO.Path.GetExtension(filename).ToLower() = ".xls" Then       ' Reading from a binary Excel file ('97-2003 format; *.xls)
            excelReader = ExcelReaderFactory.CreateBinaryReader(stream)
        ElseIf System.IO.Path.GetExtension(filename).ToLower() = ".xlsx" Then  ' Reading from a OpenXml Excel file (2007 format; *.xlsx)
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
        numRow = result.Tables(0).Rows.Count - 2
        numColumn = result.Tables(0).Columns.Count
        'If line1.ToLower.Contains("eff") Or line2.ToLower.Contains("eff") Then
        If line1.Contains("!") Or line1.Contains("#") Or line2.Contains("!") Or line2.Contains("#") Then
            MetroFramework.MetroMessageBox.Show(Me, "The selected spreadsheet is not supported by SAT Test Suite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
            'GoTo Endfilename
        End If
        If line2.ToLower.Contains("eff") Then
        ElseIf line1.ToLower.Contains("eff") AndAlso line1.ToLower.Contains("hz") AndAlso (line1.ToLower.Contains("db") Or line1.ToLower.Contains("percent") Or line1.ToLower.Contains("%")) Then
            efficiencyplot()
            Exit Sub
            'GoTo Endfilename
        ElseIf line1.ToLower.Contains("hz") AndAlso line1.ToLower.Contains("db") Then
            If LogMagToolStripMenuItem.Checked = True Then
                dbplot()
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Please select the Chart Format as ""Log Mag"" and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Exit Sub
            'GoTo Endfilename
        ElseIf line1.ToLower.Contains("hz") Then
            genericplot()
            Exit Sub
            'GoTo Endfilename
        Else
            MetroFramework.MetroMessageBox.Show(Me, "The selected spreadsheet is not supported by SAT Test Suite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
            'GoTo Endfilename
        End If
        If line2.ToLower.Contains("hz") Then    'Includes GHz, MHz, KHz and Hz
        Else
            MetroFramework.MetroMessageBox.Show(Me, "The selected spreadsheet is not supported by SAT Test Suite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
            'GoTo Endfilename
        End If
        If line2.ToLower.Contains("db") AndAlso line2.ToLower.Contains("eff") Then
            format = "db"
        ElseIf (line2.ToLower.Contains("percent") Or line2.ToLower.Contains("%")) AndAlso line2.ToLower.Contains("eff") Then
            format = "percentage"
        Else
            MetroFramework.MetroMessageBox.Show(Me, "The selected spreadsheet is not supported by SAT Test Suite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
            'GoTo Endfilename
        End If
        If newtoolbar = False AndAlso addtoolbar = False AndAlso generic = False AndAlso db = False Then
            colourcounter = 1
            ClearMarkers()
            Chart1.Series.Clear()
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
            'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = GlobalVariables.xaxismin
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
            Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.False
            frequnit = "ghz"
            checkboxnum = 1
            TextBox1.Text = ""
            'x2max = 0
            'Else
        End If
        If generic = True Then
            ClearMarkers()
            'Dim i As Integer
            'If matrix = "lower" Then
            '    i = (ports * (ports + 1) / 2)
            'ElseIf matrix = "upper" Then
            '    i = (ports * (ports + 1) / 2)
            'Else
            '    i = ports * ports
            'End If
            Dim series1 As DataVisualization.Charting.Series
            For j As Integer = 0 To GlobalVariables.seriesnames.Length - 1
                For k As Integer = 0 To secondarynames.Length - 1
                    If secondarynames(k) = GlobalVariables.seriesnames(j) Then
                        series1 = Chart1.Series(GlobalVariables.seriesnames(j))
                        Chart1.Series.Remove(series1)
                        Dim lines As New List(Of String)(TextBox1.Lines)
                        TextBox1.Text = ""
                        For Each line As String In lines
                            If Not line.Contains(GlobalVariables.seriesnames(j).Substring(GlobalVariables.seriesnames(j).IndexOf("#") + 1, (GlobalVariables.seriesnames(j).Length - 1) - (GlobalVariables.seriesnames(j).IndexOf("#"))) & ". ") Then
                                TextBox1.Text &= line & vbLf
                            End If
                        Next
                        For l As Integer = j To GlobalVariables.seriesnames.Length - 2
                            GlobalVariables.seriesnames(l) = GlobalVariables.seriesnames(l + 1)
                        Next
                        GlobalVariables.seriesnames(GlobalVariables.seriesnames.Length - 1) = ""
                        'ReDim Preserve GlobalVariables.seriesnames(i - 1)
                        'ReDim Preserve GlobalVariables.series(i - 1)
                    End If
                Next
            Next
            System.Array.Resize(Of String)(GlobalVariables.seriesnames, GlobalVariables.seriesnames.Length - secondarynames.Length)  'Need to one more than the normal ReDim Preserve
            System.Array.Resize(Of Integer)(GlobalVariables.series, GlobalVariables.series.Length - secondarynames.Length)
            'x2max = 0
            'Chart1.ChartAreas("ChartArea1").AxisX.Minimum = xmin
            'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = xmax
            For i As Integer = 0 To filelocation.Length - 1
                Dim strLine() As String = TextBox1.Text.Split(CChar(vbLf))
                TextBox1.Clear()
                For Each ln As String In strLine
                    If ln <> "" AndAlso ln <> filelocation(i) Then
                        TextBox1.Text &= ln & vbCrLf
                    End If
                Next
            Next
            filelocation = New String(-1) {}
        End If
        'If newtoolbar = False AndAlso generic = True Then
        '    colourcounter = 1
        '    ClearMarkers()
        '    Chart1.Series.Clear()
        '    Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 500000000
        '    Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 3000000000
        '    Chart1.ChartAreas("ChartArea1").AxisX.Interval = 100000000
        '    Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"
        '    Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
        '    Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.False
        '    frequnit = "ghz"
        '    checkboxnum = 1
        '    x2max = 0
        'End If
        Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True
        Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Clear()
        'Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = 100 * (Math.Pow(10, (Chart1.ChartAreas("ChartArea1").AxisY.Maximum) / 10))
        'Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = 100 * (Math.Pow(10, (Chart1.ChartAreas("ChartArea1").AxisY.Minimum) / 10))
        Chart1.ChartAreas("ChartArea1").AxisY2.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
        value = System.Text.RegularExpressions.Regex.Split(line1, ",")
        value2 = System.Text.RegularExpressions.Regex.Split(line2, ",")
        'Dim names(numColumn) As String
        If generic = True Then
            names = New String(numColumn - 1) {}
        End If
        column = 0
        'If names.Length - 1 = 0 Then
        If GlobalVariables.series.Length = 0 Then
            names = New String(numColumn - 1) {}
        Else
            'column = names.Length - 1
            column = names.Length
            'ReDim Preserve names(column + numColumn - 1)
            System.Array.Resize(Of String)(names, column + numColumn)
        End If
        table = New Double(numRow - 1, numColumn - 1) {}
        freq1 = New Double(numRow - 1) {}
        eff1 = New Double(numRow - 1) {}
        x = 0
        For Each s As String In value
            y = 0
            For Each s2 As String In value2
                If x = y Then
                    If s <> "" AndAlso s2 <> "" Then
                        If x = 0 Then
                            names(column + x) = s2
                        Else
                            For i As Integer = 0 To names.Length - 1
                                If names(i) = String.Concat(s + " " + s2) Then
                                    MetroFramework.MetroMessageBox.Show(Me, "The title name '" + String.Concat(s + " " + s2) + "' is already a member of the Chart Series", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            Next
                            names(column + x) = String.Concat(s + " " + s2)
                        End If
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

        'If frequnit = "hz" Then
        '    If line2.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000000
        '        Next
        '    ElseIf line2.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000
        '        Next
        '    ElseIf line2.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    End If
        'ElseIf frequnit = "khz" Then
        '    If line2.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000
        '        Next
        '    ElseIf line2.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    ElseIf line2.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    End If
        'ElseIf frequnit = "mhz" Then
        '    If line2.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    ElseIf line2.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    ElseIf line2.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000
        '        Next
        '    End If
        'ElseIf frequnit = "ghz" Then
        '    If line2.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    ElseIf line2.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    ElseIf line2.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000000
        '        Next
        '    End If
        'End If



        'If newtoolbar = False AndAlso addtoolbar = False AndAlso generic = False AndAlso db = False Then
        '    xmax = freq1.Max
        '    xmin = freq1.Min
        '    'Select Case frequnit
        '    '    Case "hz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"    'Two decimal adjustment (if required)
        '    '    Case "khz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000
        '    '    Case "mhz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000
        '    '    Case "ghz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000000
        '    'End Select
        '    'If GlobalVariables.autobutton = True Then
        '    '    xaxisadjust()
        '    'End If
        'End If
        'If freq1.Max > xmax Then
        '    xmax = freq1.Max
        'End If
        'If freq1.Min < xmin Then
        '    xmin = freq1.Min
        'End If
        'If GlobalVariables.autobutton = True Then
        '    xaxisadjust()
        'End If
        ''If freq1.Max > x2max Then
        ''    x2max = freq1.Max
        ''End If
        ''If GlobalVariables.autobutton = True Then
        ''    x2axisadjust()
        ''End If
        'For i As Integer = 0 To numRow - 1
        '    For j As Integer = 1 To numColumn - 1
        '        If i = 0 AndAlso j = 1 Then
        '            If y2max1 = 0 AndAlso y2min1 = 0 Then
        '                y2max1 = table(0, 1)
        '                y2min1 = table(0, 1)
        '            End If
        '        End If
        '        If table(i, j) > y2max1 Then
        '            y2max1 = table(i, j)
        '        End If
        '        If table(i, j) < y2min1 Then
        '            y2min1 = table(i, j)
        '        End If
        '    Next
        'Next
        'y2max = Math.Round(100 * (Math.Pow(10, (y2max1 / 10))), 0)
        'If y2max Mod 5 <> 0 Then
        '    y2max = y2max + (5 - (y2max Mod 5))
        'End If
        'y2min = Math.Round(100 * (Math.Pow(10, (y2min1 / 10))), 0)
        'If y2min Mod 5 <> 0 Then
        '    y2min = y2min - (y2min Mod 5)
        'End If
        'If GlobalVariables.autobutton = True Then
        '    Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = y2max
        '    Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = y2min
        '    Chart1.ChartAreas("ChartArea1").AxisY2.Interval = (y2max - y2min) / 10
        'Else
        Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = 100
        Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = 0
        Chart1.ChartAreas("ChartArea1").AxisY2.Interval = 10
        'End If
        GlobalVariables.plot = "efficiency"
        'y2axisadjust()
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
        If GlobalVariables.seriesnames.Length <> 0 Then
            'If newtoolbar = True Then
            x = GlobalVariables.seriesnames.Length
            'ReDim Preserve GlobalVariables.seriesnames(x + numColumn - 2)
            'ReDim Preserve GlobalVariables.series(x + numColumn - 2)
            System.Array.Resize(Of String)(GlobalVariables.seriesnames, x + numColumn - 1)  'Need to one more than the normal ReDim Preserve
            System.Array.Resize(Of Integer)(GlobalVariables.series, x + numColumn - 1)
        Else
            'If addtoolbar = True Then
            '    x = GlobalVariables.seriesnames.Length
            '    'ReDim Preserve GlobalVariables.seriesnames(x + numColumn - 2)
            '    'ReDim Preserve GlobalVariables.series(x + numColumn - 2)
            '    System.Array.Resize(Of String)(GlobalVariables.seriesnames, x + numColumn - 1)  'Need to one more than the normal ReDim Preserve
            '    System.Array.Resize(Of Integer)(GlobalVariables.series, x + numColumn - 1)
            'Else
            x = 0
            GlobalVariables.seriesnames = New String(numColumn - 2) {}
            GlobalVariables.series = New Integer(numColumn - 2) {}
            'End If
        End If
        If addtoolbar = False Then
            secondarynames = New String(0) {}
            y = 0
        Else
            System.Array.Resize(Of String)(secondarynames, secondarynames.Length + 1)
            y = secondarynames.Length - 1
        End If
        For i As Integer = 1 To numColumn - 1
            names(column + i) = names(column + i) & " #" & compare
            secondarynames(y + i - 1) = names(column + i)
            System.Array.Resize(Of String)(secondarynames, secondarynames.Length + 1)
            Chart1.Series.Add(names(column + i))
            Chart1.Series(names(column + i)).XAxisType = AxisType.Primary
            Chart1.Series(names(column + i)).YAxisType = AxisType.Secondary
            Chart1.Series(names(column + i)).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(names(column + i)).BorderWidth = 2
            Chart1.Series(names(column + i)).BorderDashStyle = ChartDashStyle.Dash
            'Chart1.Series(names(column + i)).Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
            colourpicker()
            Chart1.Series(names(column + i)).Color = c
            For j As Integer = 0 To numRow - 1
                If format = "db" Then
                    eff1(j) = 100 * (Math.Pow(10, (table(j, i) / 10)))
                Else
                    eff1(j) = table(j, i)
                End If
            Next
            Chart1.Series(names(column + i)).Points.DataBindXY(freq1, eff1)
            GlobalVariables.seriesnames(x + i - 1) = names(column + i)
            GlobalVariables.series(x + i - 1) = 1
        Next
        System.Array.Resize(Of String)(secondarynames, secondarynames.Length - 1)
        If GlobalVariables.autobutton = True Then
            axisvalues()
        Else
            y2axisadjust()
        End If
        'AllocConsole() 'show console
        'For i As Integer = 0 To secondarynames.Length - 1
        '    Console.WriteLine(secondarynames(i))
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
        addtoolbar = True
        generic = False
        device = False
        'TextBox1.Text &= filename & vbCrLf
        TextBox1.Text &= compare & ". " & filename & vbCrLf
        compare += 1
        System.Array.Resize(Of String)(filelocation, (filelocation.Length + 1))
        filelocation(filelocation.Length - 1) = filename
    End Sub

    'Sub x2axisadjust()
    '    While x2max > Chart1.ChartAreas("ChartArea1").AxisX.Maximum
    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum += Chart1.ChartAreas("ChartArea1").AxisX.Interval
    '    End While
    'End Sub

    Sub y2axisadjust()
        Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Clear()
        'If GlobalVariables.autobutton = True Then
        '    Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = y2max
        '    Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = y2min
        '    Chart1.ChartAreas("ChartArea1").AxisY2.Interval = (y2max - y2min) / 10
        'End If
        If GlobalVariables.plot = "efficiency" Then
            'If Chart1.ChartAreas("ChartArea1").AxisY2.Minimum <> 0 Or Chart1.ChartAreas("ChartArea1").AxisY2.Maximum <> 100 Or Chart1.ChartAreas("ChartArea1").AxisY2.Interval <> 10 Then
            '    For i As Double = 0 To 100 Step 10
            '        Dim remLabel = Chart1.ChartAreas("ChartArea1").AxisX.CustomLabels.SingleOrDefault(Function(cl) cl.FromPosition = i AndAlso cl.ToPosition = i + 0.01)
            '        Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Remove(remLabel)
            '    Next
            'End If
            For i As Double = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum To Chart1.ChartAreas("ChartArea1").AxisY2.Maximum Step Chart1.ChartAreas("ChartArea1").AxisY2.Interval
                If i = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum Then
                    If i = 0 Then
                        Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Add(i, i + 0.01, CStr(i))
                    Else
                        Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Add(i, i + 0.01, CStr(i) + " (" + CStr(Math.Round(10 * Math.Log10(i / 100), 1)) + " in dB)")
                    End If
                End If
                Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Add(i, i - 0.01, CStr(i) + " (" + CStr(Math.Round(10 * Math.Log10(i / 100), 1)) + " in dB)")
            Next
        Else
            For i As Double = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum To (Chart1.ChartAreas("ChartArea1").AxisY2.Maximum + Chart1.ChartAreas("ChartArea1").AxisY2.Interval) Step Chart1.ChartAreas("ChartArea1").AxisY2.Interval
                If i = Chart1.ChartAreas("ChartArea1").AxisY2.Minimum Then
                    Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Add(i, i + 0.01, CStr(Math.Round(i, 2)))
                End If
                Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Add(i, i - 0.01, CStr(Math.Round(i, 2)))
            Next
        End If
    End Sub

    Sub dbplot()
        If newtoolbar = True Or db = True Then
        ElseIf addtoolbar = True Or generic = True Then
            Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.True
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
        Else
            colourcounter = 1
            ClearMarkers()
            Chart1.Series.Clear()
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
            'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = GlobalVariables.xaxismin
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
            Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.True
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
            frequnit = "ghz"
            checkboxnum = 1
            'x2max = 0
        End If
        fullstring = line2 + vbLf + fullstring
        numRow += 1
        value = System.Text.RegularExpressions.Regex.Split(line1, ",")
        If GlobalVariables.series.Length = 0 Then
            column = 0
            names = New String(numColumn - 2) {}
        Else
            column = names.Length
            'ReDim Preserve names(column + numColumn - 2)
            System.Array.Resize(Of String)(names, column + numColumn - 1)
        End If
        table = New Double(numRow - 1, numColumn - 1) {}
        freq1 = New Double(numRow - 1) {}
        'eff1 = New Double(numRow - 1) {}
        x = 0
        For Each s As String In value
            If String.IsNullOrWhiteSpace(s) Then
            Else
                'If x = 0 Then
                If s.ToLower.Contains("hz") Then
                    'names(column + x) = s
                Else
                    For i As Integer = 0 To GlobalVariables.seriesnames.Length - 1
                        If GlobalVariables.seriesnames(i) = s Then
                            MetroFramework.MetroMessageBox.Show(Me, "The title name '" + s + "' is already a member of the Chart Series", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    Next
                    names(column + x) = s
                    x += 1
                End If
            End If
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

        If line1.ToString.ToLower.Contains("ghz") Then
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0)
            Next
        ElseIf line1.ToString.ToLower.Contains("mhz") Then
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0) / 1000
            Next
        ElseIf line1.ToString.ToLower.Contains("khz") Then
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0) / 1000000
            Next
        Else
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0) / 1000000000
            Next
        End If

        'If frequnit = "hz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    End If
        'ElseIf frequnit = "khz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    End If
        'ElseIf frequnit = "mhz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000
        '        Next
        '    End If
        'ElseIf frequnit = "ghz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000000
        '        Next
        '    End If
        'End If



        'If newtoolbar = False AndAlso addtoolbar = False AndAlso generic = False AndAlso db = False Then
        '    xmax = freq1.Max
        '    xmin = freq1.Min
        '    'Select Case frequnit
        '    '    Case "hz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"    'Two decimal adjustment (if required)
        '    '    Case "khz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000
        '    '    Case "mhz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000
        '    '    Case "ghz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000000
        '    'End Select
        '    'If GlobalVariables.autobutton = True Then
        '    '    xaxisadjust()
        '    'End If
        'End If
        'If freq1.Max > xmax Then
        '    xmax = freq1.Max
        'End If
        'If freq1.Min < xmin Then
        '    xmin = freq1.Min
        'End If
        'If GlobalVariables.autobutton = True Then
        '    xaxisadjust()
        'End If
        If GlobalVariables.seriesnames.Length <> 0 Then
            x = GlobalVariables.seriesnames.Length
            'ReDim Preserve GlobalVariables.seriesnames(x + numColumn - 2)
            'ReDim Preserve GlobalVariables.series(x + numColumn - 2)
            System.Array.Resize(Of String)(GlobalVariables.seriesnames, x + numColumn - 1)  'Need to one more than the normal ReDim Preserve
            System.Array.Resize(Of Integer)(GlobalVariables.series, x + numColumn - 1)
        Else
            x = 0
            GlobalVariables.seriesnames = New String(numColumn - 2) {}
            GlobalVariables.series = New Integer(numColumn - 2) {}
        End If

        'interval = freq1(1) - freq1(0)
        'interval = Math.Round((freq1(1) - freq1(0)), 3)
        'If frequnit = "ghz" Then
        interval = 0.05 'Interval needs to more flexible
        'ElseIf frequnit = "mhz" Then
        '    interval = 50
        'ElseIf frequnit = "khz" Then
        '    interval = 50000
        'Else
        '    interval = 50000000
        'End If

        If interval.ToString().IndexOf(".") = -1 Then
            decimalpart = 0     'No decimal part                                     
        Else
            decimalpart = interval.ToString().Substring(interval.ToString().IndexOf(".") + 1).Length    'To find the number of decimal part
        End If
        lastvalue = freq1(freq1.Length - 1)
        Dim k As Integer = 0
        y = 0
        z = 0
        nullpoints = New Integer(0) {}
        While (freq1(k) <> lastvalue)
            'If Math.Round((freq1(k + 1) - freq1(k)), 3) > interval Then
            If Math.Round((freq1(k + 1) - freq1(k)), decimalpart) > interval Then
                'MsgBox("OK")
                System.Array.Resize(Of Double)(freq1, freq1.Length + 1)
                For i As Integer = freq1.Length - 1 To k + 1 Step -1
                    freq1(i) = freq1(i - 1)
                Next
                freq1(k + 1) = freq1(k) + interval
                nullpoints(z) = k + 1
                z += 1
                System.Array.Resize(Of Integer)(nullpoints, z + 1)
            End If
            k += 1
        End While
        If z <> 0 Then
            System.Array.Resize(Of Integer)(nullpoints, z)
        End If
        eff1 = New Double(freq1.Length - 1) {}
        For i As Integer = 1 To numColumn - 1
            y = 0
            z = 0
            names(column + i - 1) = names(column + i - 1) & " #" & compare
            Chart1.Series.Add(names(column + i - 1))
            Chart1.Series(names(column + i - 1)).XAxisType = AxisType.Primary
            Chart1.Series(names(column + i - 1)).YAxisType = AxisType.Primary
            Chart1.Series(names(column + i - 1)).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(names(column + i - 1)).BorderWidth = 2

            Chart1.Series(names(column + i - 1)).EmptyPointStyle.BorderColor = Color.Transparent
            Chart1.Series(names(column + i - 1)).EmptyPointStyle.BorderDashStyle = ChartDashStyle.NotSet
            Chart1.Series(names(column + i - 1)).EmptyPointStyle.MarkerStyle = MarkerStyle.None
            Chart1.Series(names(column + i - 1)).EmptyPointStyle.MarkerColor = Color.Transparent
            'Chart1.Series(names(column + i - 1)).BorderDashStyle = ChartDashStyle.Dash

            'Chart1.Series(names(column + i)).MarkerSize = 5
            'Chart1.Series(names(column + i)).Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
            colourpicker()
            Chart1.Series(names(column + i - 1)).Color = c
            For j As Integer = 0 To numRow - 1
                If y <= freq1.Length - 1 Then
                    If y <> 0 AndAlso z <= (nullpoints.Length - 1) AndAlso nullpoints(z) = y Then
                        'If z < (nullpoints.Length - 1) AndAlso nullpoints(z) = y Then
                        'If nullpoints(z) = y Then
                        eff1(y) = Double.NaN
                        z += 1
                        'End If
                        j -= 1
                    Else
                        eff1(y) = table(j, i)
                    End If
                    'If eff1(y) <> Double.NaN Then
                    '    If ymax < eff1(y) Then
                    '        ymax = eff1(y)
                    '    End If
                    '    If ymin > eff1(y) Then
                    '        ymin = eff1(y)
                    '    End If
                    'End If
                    y += 1
                Else
                    Exit Sub
                End If
            Next
            Chart1.Series(names(column + i - 1)).Points.DataBindXY(freq1, eff1)
            GlobalVariables.seriesnames(x + i - 1) = names(column + i - 1)
            GlobalVariables.series(x + i - 1) = 1

            'If ymax < eff1.Max Then
            '    ymax = eff1.Max
            'End If
            'If ymin > eff1.Min Then
            '    ymin = eff1.Min
            'End If
            'If GlobalVariables.autobutton = True Then
            '    yaxisadjust()
            'End If
        Next
        If GlobalVariables.autobutton = True Then
            axisvalues()
        End If
        fullstring = vbNullString   ' Releasing memory by setting values as Null
        line1 = vbNullString
        line2 = vbNullString
        value = Nothing
        value2 = Nothing

        Erase freq1
        Erase eff1
        Erase table
        'addtoolbar = False
        'generic = False
        db = True
        device = False
        'TextBox1.Text &= filename & vbCrLf
        TextBox1.Text &= compare & ". " & filename & vbCrLf
        compare += 1
    End Sub

    Sub efficiencyplot()
        If line1.ToLower.Contains("db") AndAlso line1.ToLower.Contains("eff") Then
            format = "db"
        ElseIf (line1.ToLower.Contains("percent") Or line1.ToLower.Contains("%")) AndAlso line1.ToLower.Contains("eff") Then
            format = "percentage"
        Else
            MetroFramework.MetroMessageBox.Show(Me, "The selected spreadsheet is not supported by SAT Test Suite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If newtoolbar = False AndAlso addtoolbar = False AndAlso generic = False AndAlso db = False Then
            colourcounter = 1
            ClearMarkers()
            Chart1.Series.Clear()
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
            'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = GlobalVariables.xaxismin
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
            Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.False
            frequnit = "ghz"
            checkboxnum = 1
            'x2max = 0
        End If
        If generic = True Then
            ClearMarkers()
            Dim series1 As DataVisualization.Charting.Series
            For j As Integer = 0 To GlobalVariables.seriesnames.Length - 1
                For k As Integer = 0 To secondarynames.Length - 1
                    If secondarynames(k) = GlobalVariables.seriesnames(j) Then
                        series1 = Chart1.Series(GlobalVariables.seriesnames(j))
                        Chart1.Series.Remove(series1)
                        Dim lines As New List(Of String)(TextBox1.Lines)
                        TextBox1.Text = ""
                        For Each line As String In lines
                            If Not line.Contains(GlobalVariables.seriesnames(j).Substring(GlobalVariables.seriesnames(j).IndexOf("#") + 1, (GlobalVariables.seriesnames(j).Length - 1) - (GlobalVariables.seriesnames(j).IndexOf("#"))) & ". ") Then
                                TextBox1.Text &= line & vbLf
                            End If
                        Next
                        For l As Integer = j To GlobalVariables.seriesnames.Length - 2
                            GlobalVariables.seriesnames(l) = GlobalVariables.seriesnames(l + 1)
                        Next
                        GlobalVariables.seriesnames(GlobalVariables.seriesnames.Length - 1) = ""
                    End If
                Next
            Next
            System.Array.Resize(Of String)(GlobalVariables.seriesnames, GlobalVariables.seriesnames.Length - secondarynames.Length)  'Need to one more than the normal ReDim Preserve
            System.Array.Resize(Of Integer)(GlobalVariables.series, GlobalVariables.series.Length - secondarynames.Length)
            'x2max = 0
            For i As Integer = 0 To filelocation.Length - 1
                Dim strLine() As String = TextBox1.Text.Split(CChar(vbLf))
                TextBox1.Clear()
                For Each ln As String In strLine
                    If ln <> "" AndAlso ln <> filelocation(i) Then
                        TextBox1.Text &= ln & vbCrLf
                    End If
                Next
            Next
            filelocation = New String(-1) {}
        End If
        Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True
        Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Clear()
        Chart1.ChartAreas("ChartArea1").AxisY2.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
        fullstring = line2 + vbLf + fullstring
        numRow += 1
        value = System.Text.RegularExpressions.Regex.Split(line1, ",")
        If generic = True Then
            names = New String(numColumn - 1) {}
        End If
        column = 0
        If GlobalVariables.series.Length = 0 Then
            names = New String(numColumn - 1) {}
        Else
            column = names.Length
            'ReDim Preserve names(column + numColumn - 1)
            System.Array.Resize(Of String)(names, column + numColumn)
        End If
        table = New Double(numRow - 1, numColumn - 1) {}
        freq1 = New Double(numRow - 1) {}
        eff1 = New Double(numRow - 1) {}
        x = 0
        For Each s As String In value
            If String.IsNullOrWhiteSpace(s) Then
            Else
                If x = 0 Then
                    names(column + x) = s
                Else
                    For i As Integer = 0 To names.Length - 1
                        If names(i) = s Then
                            MetroFramework.MetroMessageBox.Show(Me, "The title name '" + s + "' is already a member of the Chart Series", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    Next
                    names(column + x) = s
                End If
                x += 1
            End If
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

        If line1.ToString.ToLower.Contains("ghz") Then
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0)
            Next
        ElseIf line1.ToString.ToLower.Contains("mhz") Then
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0) / 1000
            Next
        ElseIf line1.ToString.ToLower.Contains("khz") Then
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0) / 1000000
            Next
        Else
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0) / 1000000000
            Next
        End If


        'If frequnit = "hz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    End If
        'ElseIf frequnit = "khz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    End If
        'ElseIf frequnit = "mhz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000
        '        Next
        '    End If
        'ElseIf frequnit = "ghz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000000
        '        Next
        '    End If
        'End If



        'If newtoolbar = False AndAlso addtoolbar = False AndAlso generic = False AndAlso db = False Then
        '    xmax = freq1.Max
        '    xmin = freq1.Min
        '    'Select Case frequnit
        '    '    Case "hz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"    'Two decimal adjustment (if required)
        '    '    Case "khz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000
        '    '    Case "mhz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000
        '    '    Case "ghz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000000
        '    'End Select
        '    'If GlobalVariables.autobutton = True Then
        '    '    xaxisadjust()
        '    'End If
        'End If
        'If freq1.Max > xmax Then
        '    xmax = freq1.Max
        'End If
        'If freq1.Min < xmin Then
        '    xmin = freq1.Min
        'End If
        'If GlobalVariables.autobutton = True Then
        '    xaxisadjust()
        'End If
        ''If freq1.Max > x2max Then
        ''    x2max = freq1.Max
        ''End If
        ''If GlobalVariables.autobutton = True Then
        ''    x2axisadjust()
        ''End If
        'For i As Integer = 0 To numRow - 1
        '    For j As Integer = 1 To numColumn - 1
        '        If i = 0 AndAlso j = 1 Then
        '            If y2max1 = 0 AndAlso y2min1 = 0 Then
        '                y2max1 = table(0, 1)
        '                y2min1 = table(0, 1)
        '            End If
        '        End If
        '        If table(i, j) > y2max1 Then
        '            y2max1 = table(i, j)
        '        End If
        '        If table(i, j) < y2min1 Then
        '            y2min1 = table(i, j)
        '        End If
        '    Next
        'Next
        'y2max = Math.Round(100 * (Math.Pow(10, (y2max1 / 10))), 0)
        'If y2max Mod 5 <> 0 Then
        '    y2max = y2max + (5 - (y2max Mod 5))
        'End If
        'y2min = Math.Round(100 * (Math.Pow(10, (y2min1 / 10))), 0)
        'If y2min Mod 5 <> 0 Then
        '    y2min = y2min - (y2min Mod 5)
        'End If
        'If GlobalVariables.autobutton = True Then
        '    Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = y2max
        '    Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = y2min
        '    Chart1.ChartAreas("ChartArea1").AxisY2.Interval = (y2max - y2min) / 10
        'Else
        Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = 100
        Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = 0
        Chart1.ChartAreas("ChartArea1").AxisY2.Interval = 10
        'End If
        GlobalVariables.plot = "efficiency"
        'y2axisadjust()
        Chart1.ChartAreas("ChartArea1").AxisY2.LabelStyle.Format = "{0:0.##}"   'Use a Comma to divide by 1000 or Use a % to Multiply by 100
        Chart1.ChartAreas("ChartArea1").AxisY2.Title = "Efficiency in %"        '.# to provide one decimal part; For 2 decimal part it is .###
        If GlobalVariables.seriesnames.Length <> 0 Then
            x = GlobalVariables.seriesnames.Length
            System.Array.Resize(Of String)(GlobalVariables.seriesnames, x + numColumn - 1)  'Need to one more than the normal ReDim Preserve
            System.Array.Resize(Of Integer)(GlobalVariables.series, x + numColumn - 1)
        Else
            x = 0
            GlobalVariables.seriesnames = New String(numColumn - 2) {}
            GlobalVariables.series = New Integer(numColumn - 2) {}
        End If
        If addtoolbar = False Then
            secondarynames = New String(0) {}
            y = 0
        Else
            System.Array.Resize(Of String)(secondarynames, secondarynames.Length + 1)
            y = secondarynames.Length - 1
        End If
        For i As Integer = 1 To numColumn - 1
            names(column + i) = names(column + i) & " #" & compare
            secondarynames(y + i - 1) = names(column + i)
            System.Array.Resize(Of String)(secondarynames, secondarynames.Length + 1)
            Chart1.Series.Add(names(column + i))
            Chart1.Series(names(column + i)).XAxisType = AxisType.Primary
            Chart1.Series(names(column + i)).YAxisType = AxisType.Secondary
            Chart1.Series(names(column + i)).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(names(column + i)).BorderWidth = 2
            Chart1.Series(names(column + i)).BorderDashStyle = ChartDashStyle.Dash
            'Chart1.Series(names(column + i)).Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
            colourpicker()
            Chart1.Series(names(column + i)).Color = c
            For j As Integer = 0 To numRow - 1
                If format = "db" Then
                    eff1(j) = 100 * (Math.Pow(10, (table(j, i) / 10)))
                Else
                    eff1(j) = table(j, i)
                End If
            Next
            Chart1.Series(names(column + i)).Points.DataBindXY(freq1, eff1)
            GlobalVariables.seriesnames(x + i - 1) = names(column + i)
            GlobalVariables.series(x + i - 1) = 1
        Next
        System.Array.Resize(Of String)(secondarynames, secondarynames.Length - 1)
        If GlobalVariables.autobutton = True Then
            axisvalues()
        Else
            y2axisadjust()
        End If
        fullstring = vbNullString   ' Releasing memory by setting values as Null
        line1 = vbNullString
        line2 = vbNullString
        value = Nothing
        value2 = Nothing

        Erase freq1
        Erase eff1
        Erase table
        addtoolbar = True
        generic = False
        device = False
        'TextBox1.Text &= filename & vbCrLf
        TextBox1.Text &= compare & ". " & filename & vbCrLf
        compare += 1
        System.Array.Resize(Of String)(filelocation, (filelocation.Length + 1))
        filelocation(filelocation.Length - 1) = filename
    End Sub

    Sub genericplot()
        If newtoolbar = False AndAlso addtoolbar = False AndAlso generic = False AndAlso db = False Then
            colourcounter = 1
            ClearMarkers()
            Chart1.Series.Clear()
            Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
            Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
            Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
            'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = GlobalVariables.xaxismin
            Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
            Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.False
            frequnit = "ghz"
            checkboxnum = 1
            'x2max = 0
        End If
        If addtoolbar = True AndAlso generic = False Then
            ClearMarkers()
            Dim series1 As DataVisualization.Charting.Series
            For j As Integer = 0 To GlobalVariables.seriesnames.Length - 1
                For k As Integer = 0 To secondarynames.Length - 1
                    If secondarynames(k) = GlobalVariables.seriesnames(j) Then
                        series1 = Chart1.Series(GlobalVariables.seriesnames(j))
                        Chart1.Series.Remove(series1)
                        Dim lines As New List(Of String)(TextBox1.Lines)
                        TextBox1.Text = ""
                        For Each line As String In lines
                            If Not line.Contains(GlobalVariables.seriesnames(j).Substring(GlobalVariables.seriesnames(j).IndexOf("#") + 1, (GlobalVariables.seriesnames(j).Length - 1) - (GlobalVariables.seriesnames(j).IndexOf("#"))) & ". ") Then
                                TextBox1.Text &= line & vbLf
                            End If
                        Next
                        For l As Integer = j To GlobalVariables.seriesnames.Length - 2
                            GlobalVariables.seriesnames(l) = GlobalVariables.seriesnames(l + 1)
                        Next
                        GlobalVariables.seriesnames(GlobalVariables.seriesnames.Length - 1) = ""
                    End If
                Next
            Next
            System.Array.Resize(Of String)(GlobalVariables.seriesnames, GlobalVariables.seriesnames.Length - secondarynames.Length)  'Need to one more than the normal ReDim Preserve
            System.Array.Resize(Of Integer)(GlobalVariables.series, GlobalVariables.series.Length - secondarynames.Length)
            'x2max = 0
            'Chart1.ChartAreas("ChartArea1").AxisX.Minimum = xmin
            'Chart1.ChartAreas("ChartArea1").AxisX.Maximum = xmax
            For i As Integer = 0 To filelocation.Length - 1
                Dim strLine() As String = TextBox1.Text.Split(CChar(vbLf))
                TextBox1.Clear()
                For Each ln As String In strLine
                    If ln <> "" AndAlso ln <> filelocation(i) Then
                        TextBox1.Text &= ln & vbCrLf
                    End If
                Next
            Next
            filelocation = New String(-1) {}
        End If
        numRow += 1
        fullstring = line2 & vbCrLf & fullstring     'Adding the starting line which was used in the While condition
        Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.True
        Chart1.ChartAreas("ChartArea1").AxisY2.CustomLabels.Clear()
        Chart1.ChartAreas("ChartArea1").AxisY2.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dot
        value = System.Text.RegularExpressions.Regex.Split(line1, ",")
        column = 0
        If GlobalVariables.series.Length = 0 Then
            'If names.Length - 1 = 0 Then
            names = New String(numColumn - 1) {}
        Else
            column = names.Length
            'ReDim Preserve names(column + numColumn - 1)
            System.Array.Resize(Of String)(names, column + numColumn)
        End If
        table = New Double(numRow - 1, numColumn - 1) {}
        freq1 = New Double(numRow - 1) {}
        para1 = New Double(numRow - 1) {}
        Dim result As String = ""
        Dim s1 As String = ""
        x = 0
        z = 0
        For Each s As String In value
            If String.IsNullOrWhiteSpace(s) Then
            Else
                If x > 0 Then
                    If x = 1 Then
                        s1 = s
                    Else
                        For i As Integer = 0 To names.Length - 1
                            If names(i) = String.Join(" ", s1.Split(" "c).Intersect(s.Split(" "c))) Then    'To find if there is any matching names to be added as the secondary Y axis title
                                z = 1   'No matching titles found
                            End If
                            If names(i) = s Then
                                MetroFramework.MetroMessageBox.Show(Me, "The title name '" + s + "' is already a member of the Chart Series", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        Next
                        s1 = String.Join(" ", s1.Split(" "c).Intersect(s.Split(" "c)))
                    End If
                    names(x) = s

                Else
                    names(x) = s
                End If
                x += 1
            End If
        Next
        If z = 1 Then
            s1 = ""
        End If
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

        If line1.ToString.ToLower.Contains("ghz") Then
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0)
            Next
        ElseIf line1.ToString.ToLower.Contains("mhz") Then
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0) / 1000
            Next
        ElseIf line1.ToString.ToLower.Contains("khz") Then
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0) / 1000000
            Next
        Else
            For i As Integer = 0 To numRow - 1
                freq1(i) = table(i, 0) / 1000000000
            Next
        End If


        'If frequnit = "hz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    End If
        'ElseIf frequnit = "khz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    End If
        'ElseIf frequnit = "mhz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) * 1000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000
        '        Next
        '    End If
        'ElseIf frequnit = "ghz" Then
        '    If line1.ToString.ToLower.Contains("ghz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0)
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("mhz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000
        '        Next
        '    ElseIf line1.ToString.ToLower.Contains("khz") Then
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000
        '        Next
        '    Else
        '        For i As Integer = 0 To numRow - 1
        '            freq1(i) = table(i, 0) / 1000000000
        '        Next
        '    End If
        'End If



        'If newtoolbar = False AndAlso addtoolbar = False AndAlso generic = False AndAlso db = False Then
        '    xmax = freq1.Max
        '    xmin = freq1.Min
        '    'Select Case frequnit
        '    '    Case "hz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"    'Two decimal adjustment (if required)
        '    '    Case "khz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000
        '    '    Case "mhz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000
        '    '    Case "ghz"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000000
        '    '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000000
        '    'End Select
        '    'If GlobalVariables.autobutton = True Then
        '    '    xaxisadjust()
        '    'End If
        'End If
        'If freq1.Max > xmax Then
        '    xmax = freq1.Max
        'End If
        'If freq1.Min < xmin Then
        '    xmin = freq1.Min
        'End If
        'If GlobalVariables.autobutton = True Then
        '    xaxisadjust()
        'End If
        ''If x2max = 0 Then
        ''    x2max = freq1.Max
        ''End If
        ''x2max = freq1.Max
        ''If GlobalVariables.autobutton = True Then
        ''    x2axisadjust()
        ''End If
        For i As Integer = 0 To numRow - 1
            For j As Integer = 1 To numColumn - 1
                If i = 0 AndAlso j = 1 Then
                    y2max = table(0, 1)
                    y2min = table(0, 1)
                End If
                If table(i, j) > y2max Then
                    y2max = table(i, j)
                End If
                If table(i, j) < y2min Then
                    y2min = table(i, j)
                End If
            Next
        Next
        Chart1.ChartAreas("ChartArea1").AxisY2.Maximum = y2max
        Chart1.ChartAreas("ChartArea1").AxisY2.Minimum = y2min
        Chart1.ChartAreas("ChartArea1").AxisY2.Interval = (y2max - y2min) / 10
        GlobalVariables.plot = "generic"
        'y2axisadjust()
        Chart1.ChartAreas("ChartArea1").AxisY2.LabelStyle.Format = "{0:0.##}"   'Use a Comma to divide by 1000 or Use a % to Multiply by 100
        Chart1.ChartAreas("ChartArea1").AxisY2.Title = s1                       '.# to provide one decimal part; For 2 decimal part it is .###
        If GlobalVariables.seriesnames.Length <> 0 Then
            x = GlobalVariables.seriesnames.Length
            System.Array.Resize(Of String)(GlobalVariables.seriesnames, x + numColumn - 1)  'Need to one more than the normal ReDim Preserve
            System.Array.Resize(Of Integer)(GlobalVariables.series, x + numColumn - 1)
        Else
            x = 0
            GlobalVariables.seriesnames = New String(numColumn - 2) {}
            GlobalVariables.series = New Integer(numColumn - 2) {}
        End If
        secondarynames = New String(0) {}
        For i As Integer = 1 To numColumn - 1
            names(i) = names(i) & " #" & compare
            secondarynames(i - 1) = names(i)
            System.Array.Resize(Of String)(secondarynames, i + 1)
            Chart1.Series.Add(names(i))
            Chart1.Series(names(i)).XAxisType = AxisType.Primary
            Chart1.Series(names(i)).YAxisType = AxisType.Secondary
            Chart1.Series(names(i)).ChartType = DataVisualization.Charting.SeriesChartType.Line
            Chart1.Series(names(i)).BorderWidth = 2
            Chart1.Series(names(i)).BorderDashStyle = ChartDashStyle.Dash
            'Chart1.Series(names(i)).Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
            colourpicker()
            Chart1.Series(names(i)).Color = c
            For j As Integer = 0 To numRow - 1
                para1(j) = table(j, i)
            Next
            Chart1.Series(names(i)).Points.DataBindXY(freq1, para1)
            GlobalVariables.seriesnames(x + i - 1) = names(i)
            GlobalVariables.series(x + i - 1) = 1
        Next
        System.Array.Resize(Of String)(secondarynames, secondarynames.Length - 1)
        If GlobalVariables.autobutton = True Then
            axisvalues()
        Else
            y2axisadjust()
        End If
        fullstring = vbNullString   ' Releasing memory by setting values as Null
        line1 = vbNullString
        line2 = vbNullString
        value = Nothing
        Erase freq1
        Erase para1
        Erase table
        'AddToolStripMenuItem.Enabled = False
        addtoolbar = False
        generic = True
        device = False
        'TextBox1.Text &= filename & vbCrLf
        TextBox1.Text &= compare & ". " & filename & vbCrLf
        compare += 1
        System.Array.Resize(Of String)(filelocation, (filelocation.Length + 1))
        filelocation(filelocation.Length - 1) = filename
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs)
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
        ClearMarkers()
        If livemarkername IsNot Nothing Then
            For i As Integer = 0 To livemarkername.Length - 1
                Chart1.Series(livemarkername(i)).Points.Item(livemarkerlocation(i)).Label = ""
                Chart1.Series(livemarkername(i)).Points.Item(livemarkerlocation(i)).MarkerStyle = MarkerStyle.None
            Next
        End If
        livemarkername = New String(-1) {}
        livemarkerlocation = New Integer(-1) {}
    End Sub

    Sub ClearMarkers()
        CheckedListBox1.Items.Clear()
        For i As Integer = 0 To checkboxnum - 2
            Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = ""
            Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.None
            seriesname(i) = Nothing
            seriespointindex(i) = Nothing
        Next
        If FirstDevice.Checked = False AndAlso SecondDevice.Checked = False AndAlso ThirdDevice.Checked = False Then
            If livemarkername IsNot Nothing Then
                For i As Integer = 0 To livemarkername.Length - 1
                    Chart1.Series(livemarkername(i)).Points.Item(livemarkerlocation(i)).Label = ""
                    Chart1.Series(livemarkername(i)).Points.Item(livemarkerlocation(i)).MarkerStyle = MarkerStyle.None
                Next
            End If
            livemarkername = New String(-1) {}
            livemarkerlocation = New Integer(-1) {}
        End If
        checkboxnum = 1
        ClearAllMarkersToolStripMenuItem.Enabled = False
        ClearSelectedMarkerToolStripMenuItem.Enabled = False
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

    Private Sub FirstDevice_Click(sender As Object, e As EventArgs) Handles FirstDevice.Click
        Dim livemarkername(-1) As String
        Dim livemarkerlocation(-1) As Integer
        Do
            Try
                Dim ioMgrexample As New Ivi.Visa.Interop.ResourceManager
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "VISA COM and the required IVI drivers are missing. Kindly install the driver files before connecting to the Agilent N5230A NA.", "Driver Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Dim ioMgr As New Ivi.Visa.Interop.ResourceManager
            Dim instrument As New Ivi.Visa.Interop.FormattedIO488
            'Dim commas As Integer
            Dim commas() As String
            Dim returnstring As String
            'Dim ipaddress As String = GlobalVariables.DeviceAddress(0)
            'ipaddress = ipaddress.Substring(ipaddress.IndexOf(":"c) + 2, (ipaddress.LastIndexOf(":"c) - ipaddress.IndexOf(":"c) + 1))
            'ipaddress = ipaddress.Substring(0, ipaddress.IndexOf(":"c))             'Two steps to extract the IP address from VISA address
            'Dim host As System.Net.IPHostEntry = Dns.GetHostEntry(ipaddress)   'To find the IP address
            'Dim hostname As String = host.HostName                                  'To find the host name of N5230A
            'readvalue = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, hostname).OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Explorer").OpenSubKey("Shell Folders").GetValue("Common Documents")
            'MsgBox("""" & readvalue & """")
            'Exit Sub
            readvalue = "C:\"
            Try
                'instrument.IO = ioMgr.Open("TCPIP0::10.1.100.174::hpib7,16::INSTR") ' use instrument specific address for Open() parameter
                'instrument.IO = ioMgr.Open(VISAAddressEdit.Text)
                instrument.IO = ioMgr.Open(GlobalVariables.DeviceAddress(0))

                'instrument.WriteString("*IDN?", True)  'returns the identification of the PNA
                'returnstring = instrument.ReadString()
                'MsgBox("The IDN String is: " & returnstring, vbOKOnly, "IDN Result")
                'instrument.WriteString("SENS:FREQ:STAR?;STOP?", True)  'Read start and stop frequency from pna
                'returnstring = instrument.ReadString()
                'MsgBox("The Frequency Start and Stop is: " & returnstring, vbOKOnly, "Frequency Values")
                'instrument.WriteString("SENSe:SWEep:POIN?", True)      'Returns the number of measurement points
                'returnstring = instrument.ReadString()
                'MsgBox("The number of measurement points are " & returnstring, vbOKOnly, "Measurement Points")

                'instrument.IO.Timeout = 60000    'Making sure that the timeout is set to 2 seconds
                instrument.WriteString("*CLS", True)
                instrument.WriteString("DISP:WIND:CAT?", True)
                returnstring = instrument.ReadString()
                returnstring = System.Text.RegularExpressions.Regex.Replace(returnstring, "\s{1,}", "") 'Removing the line feed
                returnstring = returnstring.Replace("""", "")   'Removing the double quotes
                If returnstring = "EMPTY" Then
                    'instrument.WriteString("DISP:WIND:STAT ON", True)
                    instrument.WriteString("CALC:PAR:DEF:EXT 'CH1_S11_1',S11", True)
                    instrument.WriteString("DISP:WIND:TRAC:FEED 'CH1_S11_1'", True)
                End If
                instrument.WriteString("CALC:PAR:CAT:EXT?", True)
                returnstring = instrument.ReadString()
                commas = returnstring.Split(",")
                'MsgBox("The selected value is " & commas(0) & """", vbOKOnly, "Calc Para")
                'instrument.WriteString("CALC:PAR:SEL 'CH1_S11_1'", True)
                instrument.WriteString("CALC:PAR:SEL " & commas(0) & """", True)
                'instrument.WriteString("CALC:PAR:SEL?", True)
                'returnstring = instrument.ReadString()
                'MsgBox("The selected parameter is " & returnstring, vbOKOnly, "Calc Para")
                'Exit Sub
                If returnstring.Contains("S14") Or returnstring.Contains("S24") Or returnstring.Contains("S34") Or returnstring.Contains("S44") Or returnstring.Contains("S41") Or returnstring.Contains("S42") Or returnstring.Contains("S43") Then
                    ports = 4
                ElseIf returnstring.Contains("S13") Or returnstring.Contains("S23") Or returnstring.Contains("S33") Or returnstring.Contains("S31") Or returnstring.Contains("S32") Then
                    ports = 3
                ElseIf returnstring.Contains("S12") Or returnstring.Contains("S22") Or returnstring.Contains("S21") Then
                    ports = 2
                Else
                    ports = 1
                End If
                'commas = returnstring.Split(",").Length - 1
                'instrument.WriteString("CALC:PAR:DEF:EXT 'CH1_S11_1', 'S11'", True)
                'instrument.WriteString("CALC:PAR:SEL " & """" & "CH1_S11_1" & """", True)
                'instrument.WriteString("CALC:PAR:SEL?", True)
                instrument.WriteString("format:data ascii", True)
                instrument.WriteString("mmem:stor:trac:form:snp DB", True)
                instrument.IO.Timeout = 60000   'Changing the timeout to 1 minute
                If ports = 1 Then
                    'instrument.WriteString("CALC:DATA:SNP:PORTs:Save '1','c:\users\public\documents\MyData.s1p'", True)
                    'instrument.WriteString("MMEMory:TRANsfer? 'c:\users\public\documents\MyData.s1p'", True)
                    instrument.WriteString("CALC:DATA:SNP:PORTs:Save '1','" & readvalue & "\MyData.s1p'", True)
                    instrument.WriteString("MMEMory:TRANsfer? '" & readvalue & "\MyData.s1p'", True)
                ElseIf ports = 2 Then
                    instrument.WriteString("CALC:DATA:SNP:PORTs:Save '1,2','" & readvalue & "\MyData.s2p'", True)
                    instrument.WriteString("MMEMory:TRANsfer? '" & readvalue & "\MyData.s2p'", True)
                ElseIf ports = 3 Then
                    instrument.WriteString("CALC:DATA:SNP:PORTs:Save '1,2,3','" & readvalue & "\MyData.s3p'", True)
                    instrument.WriteString("MMEMory:TRANsfer? '" & readvalue & "\MyData.s3p'", True)
                ElseIf ports = 4 Then
                    instrument.WriteString("CALC:DATA:SNP:PORTs:Save '1,2,3,4','" & readvalue & "\MyData.s4p'", True)
                    instrument.WriteString("MMEMory:TRANsfer? '" & readvalue & "\MyData.s4p'", True)
                End If
                'AllocConsole() 'show console
                devicedata = ""
                matrix = "full"
                Using sr As New StringReader(instrument.ReadString())
                    While True
                        line = sr.ReadLine()
                        devicedata = devicedata & line & vbCrLf
                        'Console.WriteLine(line)
                        If line.Contains("!") Then      'This includes a line with # and ! appearing together
                            'No action. This is for the first line that contains a number which could be a result of the previous computations
                        ElseIf line.Contains("#") Then
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
                            'ElseIf line.Contains("!") Then
                        ElseIf line = "" Then
                        Else
                            Exit While
                        End If
                    End While
                    fullstring = sr.ReadToEnd()
                    'Console.Write(fullstring)
                    sr.Dispose()
                End Using
                instrument.IO.Timeout = 2000    'Reverting back the timeout to 2 seconds
                If ports = 1 Then
                    'instrument.WriteString("MMEM:DEL 'c:\users\public\documents\MyData.s4p'", True)    'Deletes the file
                    instrument.WriteString("MMEM:DEL '" & readvalue & "\MyData.s1p'", True)    'Deletes the file
                ElseIf ports = 2 Then
                    instrument.WriteString("MMEM:DEL '" & readvalue & "\MyData.s2p'", True)    'Deletes the file
                ElseIf ports = 3 Then
                    instrument.WriteString("MMEM:DEL '" & readvalue & "\MyData.s3p'", True)    'Deletes the file
                ElseIf ports = 4 Then
                    instrument.WriteString("MMEM:DEL '" & readvalue & "\MyData.s4p'", True)    'Deletes the file
                End If
                colourcounter = 1
                ClearMarkers()
                Chart1.Series.Clear()
                Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
                Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
                'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = GlobalVariables.xaxismin
                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
                Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
                Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
                Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
                Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
                Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
                Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.False
                column = ((Math.Pow(ports, 2) * 2) + 1)
                TextBox1.Text = ""
                'y2max1 = 0
                'y2min1 = 0
                'x2max = 0
                Erase names
                names = New String(-1) {}
                devicedata = devicedata & fullstring
                devicedata = devicedata.Substring(devicedata.IndexOf("!"))  'Removing the extra numbers added in the start
                fullstring = line & vbCrLf & fullstring     'Adding the starting line which was used in the While condition
                fullstring = System.Text.RegularExpressions.Regex.Replace(fullstring, "\s{1,}", ",")    'Replaces white spaces (Space, tab, linefeed, carriage-return, formfeed, vertical-tab, and newline characters) with a comma 

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

                Select Case frequnit
                    Case "hz"
                        For i As Integer = 0 To row - 1
                            freq1(i) = table(i, 0) / 1000000000
                        Next
                    Case "khz"
                        For i As Integer = 0 To row - 1
                            freq1(i) = table(i, 0) / 1000000
                        Next
                    Case "mhz"
                        For i As Integer = 0 To row - 1
                            freq1(i) = table(i, 0) / 1000
                        Next
                    Case "ghz"
                        For i As Integer = 0 To row - 1
                            freq1(i) = table(i, 0)
                        Next
                End Select
                'xmax = freq1.Max
                'xmin = freq1.Min

                'If GlobalVariables.autobutton = True Then
                '    xaxisadjust()
                'End If

                'xmax = table(row - 1, 0)
                'xmin = table(0, 0)
                'Select Case frequnit
                '    Case "hz"
                '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,,.##}"    'Two decimal adjustment (if required)
                '    Case "khz"
                '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,,.##}"
                '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000
                '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000
                '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000
                '    Case "mhz"
                '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0,.##}"
                '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000
                '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000
                '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000
                '    Case "ghz"
                '        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
                '        Chart1.ChartAreas("ChartArea1").AxisX.Interval /= 1000000000
                '        Chart1.ChartAreas("ChartArea1").AxisX.Maximum /= 1000000000
                '        Chart1.ChartAreas("ChartArea1").AxisX.Minimum /= 1000000000
                'End Select
                'If GlobalVariables.autobutton = True Then
                '    xaxisadjust()
                'End If
                'For i As Integer = 0 To row - 1
                '    freq1(i) = table(i, 0)
                'Next


                'If ComparisonModeToolStripMenuItem.Checked = True Then '(Permanently enabled Comparison Mode)
                z = GlobalVariables.seriesnames.Length
                'If matrix = "lower" Or matrix = "upper" Then
                '    If z = 0 Then
                '        GlobalVariables.seriesnames = New String((ports * (ports + 1) / 2) - 1) {}
                '        GlobalVariables.series = New Integer((ports * (ports + 1) / 2) - 1) {}
                '    Else
                '        System.Array.Resize(Of String)(GlobalVariables.seriesnames, (z + (ports * (ports + 1) / 2)))   'Need to one more than the normal ReDim Preserve
                '        System.Array.Resize(Of Integer)(GlobalVariables.series, (z + (ports * (ports + 1) / 2)))
                '    End If

                'Else
                '    If z = 0 Then
                '        GlobalVariables.seriesnames = New String((ports * ports) - 1) {}
                '        GlobalVariables.series = New Integer((ports * ports) - 1) {}
                '    Else
                '        System.Array.Resize(Of String)(GlobalVariables.seriesnames, (z + ((ports * ports))))  'Need to one more than the normal ReDim Preserve
                '        System.Array.Resize(Of Integer)(GlobalVariables.series, (z + (ports * ports)))
                '    End If

                'End If
                'Else
                If matrix <> "lower" Or matrix <> "upper" Then
                    GlobalVariables.seriesnames = New String((ports * ports) - 1) {}
                    GlobalVariables.series = New Integer((ports * ports) - 1) {}
                Else
                    GlobalVariables.seriesnames = New String((ports * (ports + 1) / 2) - 1) {}
                    GlobalVariables.series = New Integer((ports * (ports + 1) / 2) - 1) {}
                End If
                'End If
                'ymax = 0
                'ymin = 0
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
                        For j As Integer = 0 To row - 1
                            Select Case parameter
                                Case "s"
                                    If LogMagToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
                                        Select Case format
                                            Case "db"
                                                para1(j) = table(j, x)
                                            Case "ma"
                                                para1(j) = (20 * Math.Log10(table(j, x)))
                                            Case "ri"
                                                para1(j) = (10 * Math.Log10((Math.Pow(table(j, x), 2) + Math.Pow(table(j, x + 1), 2))))
                                        End Select
                                    ElseIf LinearMagToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = 0.0
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in value"
                                        Select Case format
                                            Case "db"
                                                para1(j) = Math.Pow(10, (table(j, x) / 20))
                                            Case "ma"
                                                para1(j) = table(j, x)
                                            Case "ri"
                                                para1(j) = Math.Sqrt((Math.Pow(table(j, x), 2) + Math.Pow(table(j, x + 1), 2)))
                                        End Select
                                    ElseIf PhaseToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -200
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 200
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 20
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Phase in degree"
                                        Select Case format
                                            Case "db"
                                                para1(j) = table(j, x + 1)
                                            Case "ma"
                                                para1(j) = table(j, x + 1)
                                            Case "ri"
                                                para1(j) = Math.Atan2(table(j, x + 1), table(j, x)) * 180 / Math.PI
                                        End Select
                                    ElseIf UnwrappedPhaseToolStripMenuItem.Checked = True Then
                                    ElseIf PolarToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 0
                                        Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 360
                                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 45
                                        'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = 90
                                        'Chart1.ChartAreas("ChartArea1").AlignmentOrientation = 
                                        'Chart1.ChartAreas("ChartArea1").AxisX.IsReversed = True
                                        Chart1.ChartAreas("ChartArea1").AxisX.Title = "Magnitude in value"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = 0
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.2
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.2
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Phase in degree"
                                        Select Case format
                                            Case "db"
                                                freq1(j) = table(j, x + 1)
                                                para1(j) = Math.Pow(10, (table(j, x) / 20))
                                            Case "ma"
                                                freq1(j) = table(j, x + 1)
                                                para1(j) = table(j, x)
                                            Case "ri"
                                                freq1(j) = Math.Atan2(table(j, x + 1), table(j, x)) * 180 / Math.PI
                                                para1(j) = Math.Sqrt((Math.Pow(table(j, x), 2) + Math.Pow(table(j, x + 1), 2)))
                                        End Select
                                    ElseIf SmithChartToolStripMenuItem.Checked = True Then
                                    ElseIf InverseSmithChartToolStripMenuItem.Checked = True Then
                                    ElseIf GroupDelayToolStripMenuItem.Checked = True Then
                                    ElseIf SWRToolStripMenuItem.Checked = True Then
                                    ElseIf RealToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Real in value"
                                        Select Case format
                                            Case "db"
                                                para1(j) = Math.Pow(10, (table(j, x) / 20)) * Math.Cos(table(j, x + 1) * Math.PI / 180)
                                            Case "ma"
                                                para1(j) = table(j, x) * Math.Cos(table(j, x + 1) * Math.PI / 180)
                                            Case "ri"
                                                para1(j) = table(j, x)
                                        End Select
                                    ElseIf ImaginaryToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Imaginary in value"
                                        Select Case format
                                            Case "db"
                                                para1(j) = Math.Pow(10, (table(j, x) / 20)) * Math.Sin(table(j, x + 1) * Math.PI / 180)
                                            Case "ma"
                                                para1(j) = table(j, x) * Math.Sin(table(j, x + 1) * Math.PI / 180)
                                            Case "ri"
                                                para1(j) = table(j, x + 1)
                                        End Select
                                    End If
                                Case "y"
                                Case "z"
                                Case "h"
                                Case "g"
                            End Select
                            If CStr(para1(j)) = "-Infinity" Then
                                para1(j) = -1000
                            End If
                        Next
                        x += 2
                        'If ymax < para1.Max Then
                        '    ymax = para1.Max
                        'End If
                        'If ymin > para1.Min Then
                        '    ymin = para1.Min
                        'End If
                        'If GlobalVariables.autobutton = True Then
                        '    yaxisadjust()
                        'End If

                        'If firsttime = False Then
                        '    Dim series1 As Microsoft.Office.Interop.Excel.Series = Chart1.Series("S(" & a & "," & b & ")")
                        '    Chart1.Series.Remove(series1)
                        'End If

                        Chart1.Series.Add("S(" & a & "," & b & ")")
                        Chart1.Series("S(" & a & "," & b & ")").ChartType = DataVisualization.Charting.SeriesChartType.Line
                        Chart1.Series("S(" & a & "," & b & ")").BorderWidth = 2
                        'Chart1.Series("S(" & a & "," & b & ")").Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                        colourpicker()
                        Chart1.Series("S(" & a & "," & b & ")").Color = c
                        Chart1.Series("S(" & a & "," & b & ")").Points.DataBindXY(freq1, para1)
                        GlobalVariables.seriesnames(y) = "S(" & a & "," & b & ")"
                        GlobalVariables.series(y) = 1
                        y += 1
                    Next
                Next
                If GlobalVariables.autobutton = True Then
                    axisvalues()
                End If
                Erase freq1         '   Releasing memory
                Erase para1
                Erase table
                newtoolbar = True
                device = True
                compare = 1
                firsttime = False
                checkboxnum = 1
                If Toggle1.Checked = True Then
                    FirstDevice.Checked = True
                    AddMarker()
                End If
            Catch ex As Exception
                If ex.Message.Contains("HRESULT") Then  'Previous value was If ex.Message = "HRESULT = 80040000" Then
                    MetroFramework.MetroMessageBox.Show(Me, "Error while connecting to Agilent N5230A NA. Please check if the device is available and connected to the network.", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                'Finally
                Try
                    instrument.IO.Close()
                Catch ea As Exception
                End Try
                Try
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(instrument)
                Catch ey As Exception
                End Try
                Try
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ioMgr)
                Catch ez As Exception
                End Try
                FirstDevice.Checked = False
                Exit Sub
            End Try
            System.Windows.Forms.Application.DoEvents()
            If Me.IsDisposed Then
                Exit Sub
            End If
        Loop Until Toggle1.Checked = False
        FirstDevice.Checked = False
    End Sub

    Sub AddMarker()
        'For i As Integer = 0 To checkboxnum - 1
        '    If seriesname(i) = b AndAlso seriespointindex(i) = d Then
        '        Exit Sub
        '    End If
        'Next
        If livemarkername IsNot Nothing Then
            For k As Integer = 0 To livemarkername.Length - 1
                If checkboxnum > 50 Then
                    checkboxnum = 1
                    CheckedListBox1.Items.Clear()
                    For i As Integer = 0 To 49
                        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = ""
                        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.None
                    Next
                    ClearAllMarkersToolStripMenuItem.Enabled = False
                End If
                RemoveHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
                CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(Chart1.Series(livemarkername(k)).Points.Item(livemarkerlocation(k)).XValue, 3) & ", Y=" & Math.Round(Chart1.Series(livemarkername(k)).Points.Item(livemarkerlocation(k)).YValues(0), 3), isChecked:=True)
                AddHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
                Chart1.Series(livemarkername(k)).Points.Item(livemarkerlocation(k)).Label = checkboxnum
                Chart1.Series(livemarkername(k)).Points.Item(livemarkerlocation(k)).MarkerStyle = MarkerStyle.Triangle
                Chart1.Series(livemarkername(k)).Points.Item(livemarkerlocation(k)).MarkerSize = 10
                Chart1.Series(livemarkername(k)).Points.Item(livemarkerlocation(k)).MarkerColor = Chart1.Series(livemarkername(k)).Color
                seriesname(checkboxnum - 1) = livemarkername(k)
                seriespointindex(checkboxnum - 1) = livemarkerlocation(k)
                ClearAllMarkersToolStripMenuItem.Enabled = True
                checkboxnum += 1
            Next
        End If
    End Sub

    'Private Sub VISAAddressEdit_Click(sender As Object, e As EventArgs) Handles VISAAddressEdit.Click
    '    preVISAAddress = VISAAddressEdit.Text
    '    VISAAddressEdit.Text = InputBox("Enter the new VISA Address", "VISA Address", VISAAddressEdit.Text)
    '    If VISAAddressEdit.Text = "" Then
    '        VISAAddressEdit.Text = preVISAAddress
    '    End If
    'End Sub

    Private Sub DeviceOptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeviceOptionsToolStripMenuItem.Click
        Form3.ShowDialog()
    End Sub

    Private Sub ClearChartAreaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearChartAreaToolStripMenuItem.Click
        ClearAll()
    End Sub

    Sub ClearAll()
        colourcounter = 1
        ClearMarkers()
        Chart1.Series.Clear()
        Chart1.Series.Add(" ")
        Chart1.ChartAreas("ChartArea1").AxisX.Enabled = AxisEnabled.True        'Keeps the axis when all the plots are deselected.
        Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.True
        Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
        Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
        Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
        'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = GlobalVariables.xaxismin
        Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"  'Use a Comma to divide by 1000 or Use a % to Multiply by 100
        Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"        '.# to provide one decimal part; For 2 decimal part it is .##
        If GlobalVariables.chartformat = "logmag" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
        ElseIf GlobalVariables.chartformat = "linearmag" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = 0.0
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in value"
        ElseIf GlobalVariables.chartformat = "phase" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -200
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 200
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = 20
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Phase in degree"
        ElseIf GlobalVariables.chartformat = "real" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Real in value"
        ElseIf GlobalVariables.chartformat = "imaginary" Then
            Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
            Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
            Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
            Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
            Chart1.ChartAreas("ChartArea1").AxisY.Title = "Imaginary in value"
        End If
        Chart1.ChartAreas("ChartArea1").AxisX.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dash
        Chart1.ChartAreas("ChartArea1").AxisY.MajorGrid.LineDashStyle = DataVisualization.Charting.ChartDashStyle.Dash
        Chart1.Series(" ").ChartType = DataVisualization.Charting.SeriesChartType.FastLine
        For i As Double = 0 To 0.000000001 Step 0.000000001
            array(i) = 0
            Chart1.Series(" ").Points.AddXY(i, array(i))
        Next
        Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.False
        names = New String(-1) {}
        filelocation = New String(-1) {}
        newtoolbar = False
        addtoolbar = False
        generic = False
        db = False
        device = False
        GlobalVariables.series = New Integer(-1) {}     'Array with zeo elements
        GlobalVariables.seriesnames = New String(-1) {}
        TextBox1.Text = ""
        compare = 1
        firsttime = True
    End Sub

    Private Sub SecondDevice_Click(sender As Object, e As EventArgs) Handles SecondDevice.Click
        Dim livemarkername(-1) As String
        Dim livemarkerlocation(-1) As Integer
        Do
            Try
                Dim ioMgrexample As New Ivi.Visa.Interop.ResourceManager
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "VISA COM and the required IVI drivers are missing. Kindly install the driver files before connecting to the Rohde && Schwarz ZVL6 NA.", "Driver Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Dim ioMgr As New Ivi.Visa.Interop.ResourceManager
            Dim instrument As New Ivi.Visa.Interop.FormattedIO488
            'Dim commas As String()
            Dim trace As String()
            Dim returnstring As String
            Dim S11(1) As Double
            Dim S12(1) As Double
            Dim S21(1) As Double
            Dim S22(1) As Double
            Dim Ang11(1) As Double
            Dim Ang12(1) As Double
            Dim Ang21(1) As Double
            Dim Ang22(1) As Double
            Try
                instrument.IO = ioMgr.Open(GlobalVariables.DeviceAddress(1))
                instrument.WriteString("*CLS", True)
                instrument.WriteString("INST:NSEL 2", True)     '1 - Spectrum analyser mode and 2 - Network analyser mode; Or INST NWA - Network Analyser
                instrument.WriteString("CALC:PAR:CAT?", True)
                returnstring = instrument.ReadString()
                returnstring = System.Text.RegularExpressions.Regex.Replace(returnstring, "'{1,}", "")
                returnstring = System.Text.RegularExpressions.Regex.Replace(returnstring, "\s{1,}", "")
                trace = System.Text.RegularExpressions.Regex.Split(returnstring, ",")
                instrument.WriteString("FORMAT ASCII", True)
                'instrument.WriteString("CALC:FORM MLOG", True) 'Disabled because it is used in the below loop
                instrument.WriteString("SWE:POIN?", True)
                row = instrument.ReadString()
                freq1 = New Double(row - 1) {}
                instrument.WriteString("CALC:DATA:STIM?", True)
                returnstring = instrument.ReadString()
                value = System.Text.RegularExpressions.Regex.Split(returnstring, ",")
                x = 0
                For Each s As String In value
                    If String.IsNullOrWhiteSpace(s) Then
                    Else
                        freq1(x) = CDbl(s) / 1000000000
                        x += 1
                    End If
                Next
                If trace.Contains("S11") AndAlso trace.Contains("S12") AndAlso trace.Contains("S21") AndAlso trace.Contains("S22") Then
                    ports = 2
                    S11 = New Double(row - 1) {}
                    S12 = New Double(row - 1) {}
                    S21 = New Double(row - 1) {}
                    S22 = New Double(row - 1) {}
                    Ang11 = New Double(row - 1) {}
                    Ang12 = New Double(row - 1) {}
                    Ang21 = New Double(row - 1) {}
                    Ang22 = New Double(row - 1) {}
                    GlobalVariables.seriesnames = New String(3) {"S(1,1)", "S(1,2)", "S(2,1)", "S(2,2)"}
                    GlobalVariables.series = New Integer(3) {1, 1, 1, 1}
                    For i As Integer = 0 To trace.Count - 1
                        If trace(i) = "S11" Or trace(i) = "S12" Or trace(i) = "S21" Or trace(i) = "S22" Then
                            instrument.WriteString("CALC:PAR:MEAS '" & trace(i - 1) & "','" & trace(i) & "'", True)
                            instrument.WriteString("CALC:FORM?", True)
                            line = instrument.ReadString()
                            instrument.WriteString("CALC:FORM MLOG", True)
                            instrument.WriteString("CALC:DATA? FDATA", True)
                            returnstring = instrument.ReadString()
                            value = System.Text.RegularExpressions.Regex.Split(returnstring, ",")
                            x = 0
                            For Each s As String In value
                                If String.IsNullOrWhiteSpace(s) Then
                                Else
                                    If trace(i) = "S11" Then
                                        S11(x) = CDbl(s)
                                    ElseIf trace(i) = "S12" Then
                                        S12(x) = CDbl(s)
                                    ElseIf trace(i) = "S21" Then
                                        S21(x) = CDbl(s)
                                    ElseIf trace(i) = "S22" Then
                                        S22(x) = CDbl(s)
                                    End If
                                    x += 1
                                End If
                            Next
                            instrument.WriteString("CALC:FORM PHAS", True)
                            instrument.WriteString("CALC:DATA? FDATA", True)
                            returnstring = instrument.ReadString()
                            value = System.Text.RegularExpressions.Regex.Split(returnstring, ",")
                            x = 0
                            For Each s As String In value
                                If String.IsNullOrWhiteSpace(s) Then
                                Else
                                    If trace(i) = "S11" Then
                                        Ang11(x) = CDbl(s)
                                    ElseIf trace(i) = "S12" Then
                                        Ang12(x) = CDbl(s)
                                    ElseIf trace(i) = "S21" Then
                                        Ang21(x) = CDbl(s)
                                    ElseIf trace(i) = "S22" Then
                                        Ang22(x) = CDbl(s)
                                    End If
                                    x += 1
                                End If
                            Next
                            instrument.WriteString("CALC:FORM " & line, True)
                        End If
                    Next
                ElseIf trace.Contains("S11") Then
                    ports = 1
                    S11 = New Double(row - 1) {}
                    Ang11 = New Double(row - 1) {}
                    GlobalVariables.seriesnames = New String(0) {"S(1,1)"}
                    GlobalVariables.series = New Integer(0) {1}
                    For i As Integer = 0 To trace.Count - 1
                        If trace(i) = "S11" Then
                            instrument.WriteString("CALC:PAR:MEAS '" & trace(i - 1) & "','" & trace(i) & "'", True)
                            instrument.WriteString("CALC:FORM?", True)
                            line = instrument.ReadString()
                            instrument.WriteString("CALC:FORM MLOG", True)
                            instrument.WriteString("CALC:DATA? FDATA", True)
                            returnstring = instrument.ReadString()
                            value = System.Text.RegularExpressions.Regex.Split(returnstring, ",")
                            x = 0
                            For Each s As String In value
                                If String.IsNullOrWhiteSpace(s) Then
                                Else
                                    S11(x) = CDbl(s)
                                    x += 1
                                End If
                            Next
                            instrument.WriteString("CALC:FORM PHAS", True)
                            instrument.WriteString("CALC:DATA? FDATA", True)
                            returnstring = instrument.ReadString()
                            value = System.Text.RegularExpressions.Regex.Split(returnstring, ",")
                            x = 0
                            For Each s As String In value
                                If String.IsNullOrWhiteSpace(s) Then
                                Else
                                    Ang11(x) = CDbl(s)
                                    x += 1
                                End If
                            Next
                            instrument.WriteString("CALC:FORM " & line, True)
                        End If
                    Next
                Else
                    MetroFramework.MetroMessageBox.Show(Me, "Cannot process the data. Please create a trace for 'S11' or for all the s parameters and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
                'commas = System.Text.RegularExpressions.Regex.Split(returnstring, ",")
                'For i As Integer = 0 To commas.Count - 1
                '    If System.Text.RegularExpressions.Regex.Replace(commas(i), "\s{1,}", "") = "S11" Then       'Removes the linefeed
                '        trace = commas(i - 1)
                '    End If
                'Next
                'For Creating Touchstone File
                'If ports = 2 Then
                '    instrument.WriteString("MMEM:STOR:TRAC '" & trace & "','c:\Test1.s2p',UNF,LOGP,POIN,SPAC", True)
                'Else
                '    instrument.WriteString("MMEM:STOR:TRAC '" & trace & "','c:\Test1.s1p',UNF,LOGP,POIN,SPAC", True)
                'End If
                'For Deleting Touchstone File
                'If ports = 2 Then
                '    instrument.WriteString("MMEM:DEL 'c:\Test1.s2p'", True)
                'Else
                '    instrument.WriteString("MMEM:DEL 'c:\Test1.s1p'", True)
                'End If
                devicedata = ""
                devicedata = "#  GHZ  S  dB  R  50.0" & vbCrLf & "! Rohde & Schwarz ZVL" & vbCrLf
                If ports = 1 Then
                    For i As Integer = 0 To freq1.Length - 1
                        devicedata = devicedata & freq1(i) & "  " & S11(i) & "  " & Ang11(i) & vbCrLf
                    Next
                Else
                    For i As Integer = 0 To freq1.Length - 1
                        devicedata = devicedata & freq1(i) & "  " & S11(i) & "  " & Ang11(i) & "  " & S12(i) & "  " & Ang12(i) & "  " & S21(i) & "  " & Ang21(i) & "  " & S22(i) & "  " & Ang22(i) & vbCrLf
                    Next
                End If
                matrix = "full"
                frequnit = "hz"
                checkboxnum = 1
                'y2max1 = 0
                'y2min1 = 0
                'x2max = 0
                'xmax = freq1.Max
                'xmin = freq1.Min
                colourcounter = 1
                ClearMarkers()
                Chart1.Series.Clear()
                Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
                Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
                Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
                Chart1.ChartAreas("ChartArea1").AxisY.Enabled = AxisEnabled.True
                If GlobalVariables.chartformat = "logmag" Then
                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
                ElseIf GlobalVariables.chartformat = "linearmag" Then
                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = 0.0
                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in value"
                ElseIf GlobalVariables.chartformat = "phase" Then
                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -200
                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 200
                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 20
                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Phase in degree"
                ElseIf GlobalVariables.chartformat = "real" Then
                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Real in value"
                ElseIf GlobalVariables.chartformat = "imaginary" Then
                    Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
                    Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                    Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                    Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                    Chart1.ChartAreas("ChartArea1").AxisY.Title = "Imaginary in value"
                End If
                Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.False
                TextBox1.Text = ""
                'If GlobalVariables.autobutton = True Then
                '    xaxisadjust()
                'End If
                'ymax = 0
                'ymin = 0
                x = 1
                y = 0
                'AllocConsole()
                If ports = 1 Then
                    For i As Integer = 0 To row - 1
                        If CStr(S11(i)) = "-Infinity" Then
                            S11(i) = -1000
                        End If
                        If LogMagToolStripMenuItem.Checked = True Then
                        ElseIf LinearMagToolStripMenuItem.Checked = True Then
                            S11(i) = Math.Pow(10, (S11(i) / 20))
                        ElseIf PhaseToolStripMenuItem.Checked = True Then
                            S11(i) = Ang11(i)
                        ElseIf UnwrappedPhaseToolStripMenuItem.Checked = True Then
                        ElseIf PolarToolStripMenuItem.Checked = True Then
                        ElseIf SmithChartToolStripMenuItem.Checked = True Then
                        ElseIf InverseSmithChartToolStripMenuItem.Checked = True Then
                        ElseIf GroupDelayToolStripMenuItem.Checked = True Then
                        ElseIf SWRToolStripMenuItem.Checked = True Then
                        ElseIf RealToolStripMenuItem.Checked = True Then
                            S11(i) = Math.Pow(10, (S11(i) / 20)) * Math.Cos(Ang11(i) * Math.PI / 180)
                        ElseIf ImaginaryToolStripMenuItem.Checked = True Then
                            S11(i) = Math.Pow(10, (S11(i) / 20)) * Math.Sin(Ang11(i) * Math.PI / 180)
                        End If
                    Next
                    'If ymax < S11.Max Then
                    '    ymax = S11.Max
                    'End If
                    'If ymin > S11.Min Then
                    '    ymin = S11.Min
                    'End If
                    Chart1.Series.Add("S(1,1)")
                    Chart1.Series("S(1,1)").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Chart1.Series("S(1,1)").BorderWidth = 2
                    'Chart1.Series("S(1,1)").Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                    colourpicker()
                    Chart1.Series("S(1,1)").Color = c
                    Chart1.Series("S(1,1)").Points.DataBindXY(freq1, S11)
                Else
                    For i As Integer = 0 To row - 1
                        If CStr(S11(i)) = "-Infinity" Then
                            S11(i) = -1000
                        End If
                        If CStr(S12(i)) = "-Infinity" Then
                            S12(i) = -1000
                        End If
                        If CStr(S21(i)) = "-Infinity" Then
                            S21(i) = -1000
                        End If
                        If CStr(S22(i)) = "-Infinity" Then
                            S22(i) = -1000
                        End If
                        If LogMagToolStripMenuItem.Checked = True Then
                        ElseIf LinearMagToolStripMenuItem.Checked = True Then
                            S11(i) = Math.Pow(10, (S11(i) / 20))
                            S12(i) = Math.Pow(10, (S12(i) / 20))
                            S21(i) = Math.Pow(10, (S21(i) / 20))
                            S22(i) = Math.Pow(10, (S22(i) / 20))
                        ElseIf PhaseToolStripMenuItem.Checked = True Then
                            S11(i) = Ang11(i)
                            S12(i) = Ang12(i)
                            S21(i) = Ang21(i)
                            S22(i) = Ang22(i)
                        ElseIf UnwrappedPhaseToolStripMenuItem.Checked = True Then
                        ElseIf PolarToolStripMenuItem.Checked = True Then
                        ElseIf SmithChartToolStripMenuItem.Checked = True Then
                        ElseIf InverseSmithChartToolStripMenuItem.Checked = True Then
                        ElseIf GroupDelayToolStripMenuItem.Checked = True Then
                        ElseIf SWRToolStripMenuItem.Checked = True Then
                        ElseIf RealToolStripMenuItem.Checked = True Then
                            S11(i) = Math.Pow(10, (S11(i) / 20)) * Math.Cos(Ang11(i) * Math.PI / 180)
                            S12(i) = Math.Pow(10, (S12(i) / 20)) * Math.Cos(Ang12(i) * Math.PI / 180)
                            S21(i) = Math.Pow(10, (S21(i) / 20)) * Math.Cos(Ang21(i) * Math.PI / 180)
                            S22(i) = Math.Pow(10, (S22(i) / 20)) * Math.Cos(Ang22(i) * Math.PI / 180)
                        ElseIf ImaginaryToolStripMenuItem.Checked = True Then
                            S11(i) = Math.Pow(10, (S11(i) / 20)) * Math.Sin(Ang11(i) * Math.PI / 180)
                            S12(i) = Math.Pow(10, (S12(i) / 20)) * Math.Sin(Ang12(i) * Math.PI / 180)
                            S21(i) = Math.Pow(10, (S21(i) / 20)) * Math.Sin(Ang21(i) * Math.PI / 180)
                            S22(i) = Math.Pow(10, (S22(i) / 20)) * Math.Sin(Ang22(i) * Math.PI / 180)
                        End If
                        'Console.WriteLine(freq1(i) & " " & S11(i) & " " & S12(i) & " " & S21(i) & " " & S22(i))
                    Next
                    'If ymax < S11.Max Then
                    '    ymax = S11.Max
                    'End If
                    'If ymax < S12.Max Then
                    '    ymax = S12.Max
                    'End If
                    'If ymax < S21.Max Then
                    '    ymax = S21.Max
                    'End If
                    'If ymax < S22.Max Then
                    '    ymax = S22.Max
                    'End If
                    'If ymin > S11.Min Then
                    '    ymin = S11.Min
                    'End If
                    'If ymin > S12.Min Then
                    '    ymin = S12.Min
                    'End If
                    'If ymin > S21.Min Then
                    '    ymin = S21.Min
                    'End If
                    'If ymin > S22.Min Then
                    '    ymin = S22.Min
                    'End If
                    Chart1.Series.Add("S(1,1)")
                    Chart1.Series("S(1,1)").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Chart1.Series("S(1,1)").BorderWidth = 2
                    'Chart1.Series("S(1,1)").Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                    colourpicker()
                    Chart1.Series("S(1,1)").Color = c
                    Chart1.Series("S(1,1)").Points.DataBindXY(freq1, S11)
                    Chart1.Series.Add("S(1,2)")
                    Chart1.Series("S(1,2)").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Chart1.Series("S(1,2)").BorderWidth = 2
                    'Chart1.Series("S(1,2)").Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                    colourpicker()
                    Chart1.Series("S(1,2)").Color = c
                    Chart1.Series("S(1,2)").Points.DataBindXY(freq1, S12)
                    Chart1.Series.Add("S(2,1)")
                    Chart1.Series("S(2,1)").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Chart1.Series("S(2,1)").BorderWidth = 2
                    'Chart1.Series("S(2,1)").Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                    colourpicker()
                    Chart1.Series("S(2,1)").Color = c
                    Chart1.Series("S(2,1)").Points.DataBindXY(freq1, S21)
                    Chart1.Series.Add("S(2,2)")
                    Chart1.Series("S(2,2)").ChartType = DataVisualization.Charting.SeriesChartType.Line
                    Chart1.Series("S(2,2)").BorderWidth = 2
                    'Chart1.Series("S(2,2)").Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                    colourpicker()
                    Chart1.Series("S(2,2)").Color = c
                    Chart1.Series("S(2,2)").Points.DataBindXY(freq1, S22)
                End If
                If GlobalVariables.autobutton = True Then
                    axisvalues()
                End If
                Erase freq1         '   Releasing memory
                Erase S11
                If ports = 2 Then
                    Erase S12
                    Erase S21
                    Erase S22
                End If
                newtoolbar = True
                device = True
                compare = 1
                firsttime = False
                checkboxnum = 1
                If Toggle1.Checked = True Then
                    SecondDevice.Checked = True
                    AddMarker()
                End If
            Catch ex As Exception
                If ex.Message.Contains("HRESULT") Then  'Previous value was If ex.Message = "HRESULT = 80040011" Then
                    MetroFramework.MetroMessageBox.Show(Me, "Error while connecting to Rohde && Schwarz ZVL6 NA. Please check if the device is available and connected to the network.", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                'Finally
                Try
                    instrument.IO.Close()
                Catch ea As Exception
                End Try
                Try
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(instrument)
                Catch ey As Exception
                End Try
                Try
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ioMgr)
                Catch ez As Exception
                End Try
                SecondDevice.Checked = False
                Exit Sub
            End Try
            System.Windows.Forms.Application.DoEvents()
            If Me.IsDisposed Then
                Exit Sub
            End If
        Loop Until Toggle1.Checked = False
        SecondDevice.Checked = False
    End Sub

    Private Sub ThirdDevice_Click(sender As Object, e As EventArgs) Handles ThirdDevice.Click
        Dim livemarkername(-1) As String
        Dim livemarkerlocation(-1) As Integer
        Do
            Try
                Dim ioMgrexample As New Ivi.Visa.Interop.ResourceManager

            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "VISA COM and the required IVI drivers are missing. Kindly install the driver files before connecting to the Keysight E5071C ENA Vector Network Analyzer.", "Driver Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Dim ioMgr As New Ivi.Visa.Interop.ResourceManager
            Dim instrument As New Ivi.Visa.Interop.FormattedIO488
            Dim returnstring As String
            readvalue = "c:\users\public\documents\"
            Try
                instrument.IO = ioMgr.Open(GlobalVariables.DeviceAddress(2))
                instrument.IO.Timeout = 60000   'Changing the timeout to 1 minute
                instrument.WriteString("*CLS", True)
                instrument.WriteString(":MMEM:STOR:SNP:FORM DB", True)
                instrument.WriteString(":FORM:DATA ASC", True)
                instrument.WriteString(":CALC1:PAR:COUN?", True)
                returnstring = instrument.ReadString()
                returnstring = System.Text.RegularExpressions.Regex.Replace(returnstring, "\s{1,}", "") 'Removing the line feed
                If returnstring = "+1" Then
                    ports = 1
                    instrument.WriteString(":MMEM:STOR:SNP:TYPE:S1P 1", True)
                    instrument.WriteString(":MMEM:STOR:SNP """ & readvalue & "MyData.s1p""", True)
                    instrument.WriteString(":MMEM:TRAN? """ & readvalue & "MyData.s1p""", True)
                Else
                    ports = 2
                    instrument.WriteString(":MMEM:STOR:SNP:TYPE:S2P 1,2", True)
                    instrument.WriteString(":MMEM:STOR:SNP """ & readvalue & "MyData.s2p""", True)
                    instrument.WriteString(":MMEM:TRAN? """ & readvalue & "MyData.s2p""", True)
                End If
                devicedata = ""
                matrix = "full"
                Using sr As New StringReader(instrument.ReadString())
                    While True
                        line = sr.ReadLine()
                        devicedata = devicedata & line & vbCrLf
                        'Console.WriteLine(line)
                        If line.Contains("!") Then      'This includes a line with # and ! appearing together
                            'No action. This is for the first line that contains a number which could be a result of the previous computations
                        ElseIf line.Contains("#") Then
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
                            End If
                            'ElseIf line.Contains("!") Then
                        ElseIf line = "" Then
                        Else
                            Exit While
                        End If
                    End While
                    fullstring = sr.ReadToEnd()
                    'Console.Write(fullstring)
                    sr.Dispose()
                End Using
                instrument.IO.Timeout = 2000    'Reverting back the timeout to 2 seconds
                If ports = 1 Then
                    instrument.WriteString(":MMEM:DEL """ & readvalue & "MyData.s1p""", True)   'Deletes the file
                Else
                    instrument.WriteString(":MMEM:DEL """ & readvalue & "MyData.s2p""", True)   'Deletes the file
                End If
                colourcounter = 1
                ClearMarkers()
                Chart1.Series.Clear()
                Chart1.ChartAreas("ChartArea1").AxisX.Minimum = GlobalVariables.xaxismin
                Chart1.ChartAreas("ChartArea1").AxisX.Maximum = GlobalVariables.xaxismax
                Chart1.ChartAreas("ChartArea1").AxisX.Interval = GlobalVariables.xaxisint
                'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = GlobalVariables.xaxismin
                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
                Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in GHz"
                Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
                Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
                Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
                Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
                Chart1.ChartAreas("ChartArea1").AxisY2.Enabled = AxisEnabled.False
                column = ((Math.Pow(ports, 2) * 2) + 1)
                TextBox1.Text = ""
                'y2max1 = 0
                'y2min1 = 0
                'x2max = 0
                Erase names
                names = New String(-1) {}
                devicedata = devicedata & fullstring
                devicedata = devicedata.Substring(devicedata.IndexOf("!"))  'Removing the extra numbers added in the start
                fullstring = line & vbCrLf & fullstring     'Adding the starting line which was used in the While condition
                fullstring = System.Text.RegularExpressions.Regex.Replace(fullstring, "\s{1,}", ",")    'Replaces white spaces (Space, tab, linefeed, carriage-return, formfeed, vertical-tab, and newline characters) with a comma 

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

                Select Case frequnit
                    Case "hz"
                        For i As Integer = 0 To row - 1
                            freq1(i) = table(i, 0) / 1000000000
                        Next
                    Case "khz"
                        For i As Integer = 0 To row - 1
                            freq1(i) = table(i, 0) / 1000000
                        Next
                    Case "mhz"
                        For i As Integer = 0 To row - 1
                            freq1(i) = table(i, 0) / 1000
                        Next
                    Case "ghz"
                        For i As Integer = 0 To row - 1
                            freq1(i) = table(i, 0)
                        Next
                End Select

                'If ComparisonModeToolStripMenuItem.Checked = True Then '(Permanently enabled Comparison Mode)
                z = GlobalVariables.seriesnames.Length
                If matrix <> "lower" Or matrix <> "upper" Then
                    GlobalVariables.seriesnames = New String((ports * ports) - 1) {}
                    GlobalVariables.series = New Integer((ports * ports) - 1) {}
                Else
                    GlobalVariables.seriesnames = New String((ports * (ports + 1) / 2) - 1) {}
                    GlobalVariables.series = New Integer((ports * (ports + 1) / 2) - 1) {}
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
                        For j As Integer = 0 To row - 1
                            Select Case parameter
                                Case "s"
                                    If LogMagToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = GlobalVariables.yaxismin
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = GlobalVariables.yaxismax
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = GlobalVariables.yaxisint
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in dB"
                                        Select Case format
                                            Case "db"
                                                para1(j) = table(j, x)
                                            Case "ma"
                                                para1(j) = (20 * Math.Log10(table(j, x)))
                                            Case "ri"
                                                para1(j) = (10 * Math.Log10((Math.Pow(table(j, x), 2) + Math.Pow(table(j, x + 1), 2))))
                                        End Select
                                    ElseIf LinearMagToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = 0.0
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Magnitude in value"
                                        Select Case format
                                            Case "db"
                                                para1(j) = Math.Pow(10, (table(j, x) / 20))
                                            Case "ma"
                                                para1(j) = table(j, x)
                                            Case "ri"
                                                para1(j) = Math.Sqrt((Math.Pow(table(j, x), 2) + Math.Pow(table(j, x + 1), 2)))
                                        End Select
                                    ElseIf PhaseToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -200
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 200
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 20
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Phase in degree"
                                        Select Case format
                                            Case "db"
                                                para1(j) = table(j, x + 1)
                                            Case "ma"
                                                para1(j) = table(j, x + 1)
                                            Case "ri"
                                                para1(j) = Math.Atan2(table(j, x + 1), table(j, x)) * 180 / Math.PI
                                        End Select
                                    ElseIf UnwrappedPhaseToolStripMenuItem.Checked = True Then
                                    ElseIf PolarToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 0
                                        Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 360
                                        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 45
                                        'Chart1.ChartAreas("ChartArea1").AxisX.Crossing = 90
                                        'Chart1.ChartAreas("ChartArea1").AlignmentOrientation = 
                                        'Chart1.ChartAreas("ChartArea1").AxisX.IsReversed = True
                                        Chart1.ChartAreas("ChartArea1").AxisX.Title = "Magnitude in value"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = 0
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.2
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.2
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Phase in degree"
                                        Select Case format
                                            Case "db"
                                                freq1(j) = table(j, x + 1)
                                                para1(j) = Math.Pow(10, (table(j, x) / 20))
                                            Case "ma"
                                                freq1(j) = table(j, x + 1)
                                                para1(j) = table(j, x)
                                            Case "ri"
                                                freq1(j) = Math.Atan2(table(j, x + 1), table(j, x)) * 180 / Math.PI
                                                para1(j) = Math.Sqrt((Math.Pow(table(j, x), 2) + Math.Pow(table(j, x + 1), 2)))
                                        End Select
                                    ElseIf SmithChartToolStripMenuItem.Checked = True Then
                                    ElseIf InverseSmithChartToolStripMenuItem.Checked = True Then
                                    ElseIf GroupDelayToolStripMenuItem.Checked = True Then
                                    ElseIf SWRToolStripMenuItem.Checked = True Then
                                    ElseIf RealToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Real in value"
                                        Select Case format
                                            Case "db"
                                                para1(j) = Math.Pow(10, (table(j, x) / 20)) * Math.Cos(table(j, x + 1) * Math.PI / 180)
                                            Case "ma"
                                                para1(j) = table(j, x) * Math.Cos(table(j, x + 1) * Math.PI / 180)
                                            Case "ri"
                                                para1(j) = table(j, x)
                                        End Select
                                    ElseIf ImaginaryToolStripMenuItem.Checked = True Then
                                        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -1.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 1.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 0.1
                                        Chart1.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "{0:0.##}"
                                        Chart1.ChartAreas("ChartArea1").AxisY.Title = "Imaginary in value"
                                        Select Case format
                                            Case "db"
                                                para1(j) = Math.Pow(10, (table(j, x) / 20)) * Math.Sin(table(j, x + 1) * Math.PI / 180)
                                            Case "ma"
                                                para1(j) = table(j, x) * Math.Sin(table(j, x + 1) * Math.PI / 180)
                                            Case "ri"
                                                para1(j) = table(j, x + 1)
                                        End Select
                                    End If
                                Case "y"
                                Case "z"
                                Case "h"
                                Case "g"
                            End Select
                            If CStr(para1(j)) = "-Infinity" Then
                                para1(j) = -1000
                            End If
                        Next
                        x += 2
                        'If ymax < para1.Max Then
                        '    ymax = para1.Max
                        'End If
                        'If ymin > para1.Min Then
                        '    ymin = para1.Min
                        'End If
                        'If GlobalVariables.autobutton = True Then
                        '    yaxisadjust()
                        'End If

                        'If firsttime = False Then
                        '    Dim series1 As Microsoft.Office.Interop.Excel.Series = Chart1.Series("S(" & a & "," & b & ")")
                        '    Chart1.Series.Remove(series1)
                        'End If

                        Chart1.Series.Add("S(" & a & "," & b & ")")
                        Chart1.Series("S(" & a & "," & b & ")").ChartType = DataVisualization.Charting.SeriesChartType.Line
                        Chart1.Series("S(" & a & "," & b & ")").BorderWidth = 2
                        'Chart1.Series("S(" & a & "," & b & ")").Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
                        colourpicker()
                        Chart1.Series("S(" & a & "," & b & ")").Color = c
                        Chart1.Series("S(" & a & "," & b & ")").Points.DataBindXY(freq1, para1)
                        GlobalVariables.seriesnames(y) = "S(" & a & "," & b & ")"
                        GlobalVariables.series(y) = 1
                        y += 1
                    Next
                Next
                If GlobalVariables.autobutton = True Then
                    axisvalues()
                End If
                Erase freq1         '   Releasing memory
                Erase para1
                Erase table
                newtoolbar = True
                device = True
                compare = 1
                firsttime = False
                checkboxnum = 1
                If Toggle1.Checked = True Then
                    ThirdDevice.Checked = True
                    AddMarker()
                End If
            Catch ex As Exception
                If ex.Message.Contains("HRESULT") Then  'Previous value was If ex.Message = "HRESULT = 80040011" Then
                    MetroFramework.MetroMessageBox.Show(Me, "Error while connecting to Keysight E5071C ENA Vector Network Analyzer. Please check if the device is available and connected to the network.", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                'Finally
                Try
                    instrument.IO.Close()
                Catch ea As Exception
                End Try
                Try
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(instrument)
                Catch ey As Exception
                End Try
                Try
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(ioMgr)
                Catch ez As Exception
                End Try
                SecondDevice.Checked = False
                Exit Sub
            End Try
            System.Windows.Forms.Application.DoEvents()
            If Me.IsDisposed Then
                Exit Sub
            End If
        Loop Until Toggle1.Checked = False
        ThirdDevice.Checked = False
    End Sub

    Sub colourpicker()
        Select Case colourcounter
            Case 1
                c = Color.Blue
            Case 2
                c = Color.Red
            Case 3
                c = Color.Black
            Case 4
                c = Color.Chocolate
            Case 5
                c = Color.Brown
            Case 6
                c = Color.OliveDrab
            Case 7
                c = Color.Orange
            Case 8
                c = Color.LimeGreen
            Case 9
                c = Color.Magenta
            Case 10
                c = Color.Maroon
            Case 11
                c = Color.BurlyWood
            Case 12
                c = Color.CadetBlue
            Case 13
                c = Color.Chartreuse
            Case 14
                c = Color.BlueViolet
            Case 15
                c = Color.Coral
            Case 16
                c = Color.CornflowerBlue
            Case 17
                c = Color.Crimson
            Case 18
                c = Color.Cyan
            Case 19
                c = Color.DarkBlue
            Case 20
                c = Color.DarkCyan
            Case 21
                c = Color.DarkGoldenrod
            Case 22
                c = Color.DarkGray
            Case 23
                c = Color.DarkGreen
            Case 24
                c = Color.DarkKhaki
            Case 25
                c = Color.DarkMagenta
            Case 26
                c = Color.DarkOliveGreen
            Case 27
                c = Color.DarkOrange
            Case 28
                c = Color.DarkOrchid
            Case 29
                c = Color.DarkRed
            Case 30
                c = Color.DarkSalmon
            Case 31
                c = Color.DarkSeaGreen
            Case 32
                c = Color.DarkSlateBlue
            Case 33
                c = Color.DarkSlateGray
            Case 34
                c = Color.DarkTurquoise
            Case 35
                c = Color.DarkViolet
            Case 36
                c = Color.DeepPink
            Case 37
                c = Color.DeepSkyBlue
            Case 38
                c = Color.DimGray
            Case 39
                c = Color.DodgerBlue
            Case 40
                c = Color.Firebrick
            Case 41
                c = Color.ForestGreen
            Case 42
                c = Color.BlanchedAlmond
            Case 43
                c = Color.Gainsboro
            Case 44
                c = Color.Gold
            Case 45
                c = Color.Goldenrod
            Case 46
                c = Color.Gray
            Case 47
                c = Color.Green
            Case 48
                c = Color.GreenYellow
            Case 49
                c = Color.HotPink
            Case 50
                c = Color.IndianRed
            Case 51
                c = Color.Indigo
            Case 52
                c = Color.Khaki
            Case 53
                c = Color.Violet
            Case 54
                c = Color.LawnGreen
            Case 55
                c = Color.Wheat
            Case 56
                c = Color.LightBlue
            Case 57
                c = Color.LightCoral
            Case 58
                c = Color.Yellow
            Case 59
                c = Color.LightGray
            Case 60
                c = Color.LightGreen
            Case 61
                c = Color.LightPink
            Case 62
                c = Color.LightSalmon
            Case 63
                c = Color.LightSeaGreen
            Case 64
                c = Color.LightSkyBlue
            Case 65
                c = Color.LightSlateGray
            Case 66
                c = Color.LightSteelBlue
            Case 67
                c = Color.Lime
            Case 68
                c = Color.Aquamarine
            Case 69
                c = Color.YellowGreen
            Case 70
                c = Color.Bisque
            Case 71
                c = Color.MediumAquamarine
            Case 72
                c = Color.MediumBlue
            Case 73
                c = Color.MediumOrchid
            Case 74
                c = Color.MediumPurple
            Case 75
                c = Color.MediumSeaGreen
            Case 76
                c = Color.MediumSlateBlue
            Case 77
                c = Color.MediumSpringGreen
            Case 78
                c = Color.MediumTurquoise
            Case 79
                c = Color.MediumVioletRed
            Case 80
                c = Color.MidnightBlue
            Case 81
                c = Color.MistyRose
            Case 82
                c = Color.Moccasin
            Case 83
                c = Color.NavajoWhite
            Case 84
                c = Color.Navy
            Case 85
                c = Color.Olive
            Case 86
                c = Color.AntiqueWhite
            Case 87
                c = Color.Aqua
            Case 88
                c = Color.OrangeRed
            Case 89
                c = Color.Orchid
            Case 90
                c = Color.PaleGoldenrod
            Case 91
                c = Color.PaleGreen
            Case 92
                c = Color.PaleTurquoise
            Case 93
                c = Color.PaleVioletRed
            Case 94
                c = Color.PeachPuff
            Case 95
                c = Color.Peru
            Case 96
                c = Color.Pink
            Case 97
                c = Color.Plum
            Case 98
                c = Color.PowderBlue
            Case 99
                c = Color.Purple
            Case 100
                c = Color.Fuchsia
            Case 101
                c = Color.RosyBrown
            Case 102
                c = Color.RoyalBlue
            Case 103
                c = Color.SaddleBrown
            Case 104
                c = Color.Salmon
            Case 105
                c = Color.SandyBrown
            Case 106
                c = Color.SeaGreen
            Case 107
                c = Color.Sienna
            Case 108
                c = Color.Silver
            Case 109
                c = Color.SkyBlue
            Case 110
                c = Color.SlateBlue
            Case 111
                c = Color.SlateGray
            Case 112
                c = Color.SpringGreen
            Case 113
                c = Color.SteelBlue
            Case 114
                c = Color.Tan
            Case 115
                c = Color.Teal
            Case 116
                c = Color.Thistle
            Case 117
                c = Color.Tomato
            Case 118
                c = Color.Turquoise
        End Select
        If colourcounter >= 118 Then
            colourcounter = 1
        Else
            colourcounter += 1
        End If
    End Sub

    Private Sub CheckedListBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged
        ClearSelectedMarkerToolStripMenuItem.Enabled = True
    End Sub

    Private Sub ClearSelectedMarkerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearSelectedMarkerToolStripMenuItem.Click
        Chart1.Series(seriesname(CheckedListBox1.SelectedIndex)).Points.Item(seriespointindex(CheckedListBox1.SelectedIndex)).Label = ""
        Chart1.Series(seriesname(CheckedListBox1.SelectedIndex)).Points.Item(seriespointindex(CheckedListBox1.SelectedIndex)).MarkerStyle = MarkerStyle.None
        seriesname(CheckedListBox1.SelectedIndex) = Nothing
        seriespointindex(CheckedListBox1.SelectedIndex) = Nothing
        For i As Integer = CheckedListBox1.SelectedIndex To checkboxnum - 3
            If i > (checkboxnum - 3) Then
                Exit For
            End If
            seriesname(i) = seriesname(i + 1)
            seriespointindex(i) = seriespointindex(i + 1)
            line = CheckedListBox1.Items(i + 1).ToString()
            CheckedListBox1.Items.Insert(i, (CInt(line.Substring(0, line.IndexOf("."))) - 1) & line.Substring(line.IndexOf(".")))
            RemoveHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
            If CheckedListBox1.GetItemCheckState(i + 2) = CheckState.Checked Then
                CheckedListBox1.SetItemCheckState(i, CheckState.Checked)
                Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = i + 1
                Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.Triangle
            Else
                CheckedListBox1.SetItemCheckState(i, CheckState.Unchecked)
                Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = ""
                Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.None
            End If
            AddHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
            CheckedListBox1.Items.RemoveAt(i + 1)
        Next
        CheckedListBox1.Items.RemoveAt(checkboxnum - 2)
        checkboxnum -= 1
        CheckedListBox1.SelectedIndex = -1
        ClearSelectedMarkerToolStripMenuItem.Enabled = False
        If checkboxnum = 1 Then
            ClearAllMarkersToolStripMenuItem.Enabled = False
        End If
    End Sub

    'Private Sub ComparisonModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ComparisonModeToolStripMenuItem.Click '(Permanently enabled Comparison Mode)
    '    If ComparisonModeToolStripMenuItem.Checked = False Then
    '        ComparisonModeToolStripMenuItem.Checked = True
    '        'AddToolStripMenuItem.Enabled = False
    '        ClearAll()
    '    Else
    '        ComparisonModeToolStripMenuItem.Checked = False
    '        'AddToolStripMenuItem.Enabled = True
    '        ClearAll()
    '    End If
    'End Sub

    Private Sub AddMarkerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddMarkerToolStripMenuItem.Click
        GlobalVariables.Markertraceindex = -1
        Form4.ShowDialog()
        If GlobalVariables.Markertraceindex <> -1 Then
            If GlobalVariables.Markertrace = "ALL" Then
                For i As Integer = 0 To GlobalVariables.seriesnames.Length - 1
                    Dim Datapoint As DataPoint = Nothing
                    Dim j As Integer = 0
                    For Each pt As DataPoint In Chart1.Series(GlobalVariables.seriesnames(i)).Points
                        Datapoint = pt
                        If pt.XValue >= GlobalVariables.Markerfreq Then
                            Exit For
                        End If
                        j += 1
                    Next
                    For a As Integer = 0 To checkboxnum - 1
                        If seriesname(a) = GlobalVariables.seriesnames(i) AndAlso seriespointindex(a) = j Then
                            GoTo NextValue
                        End If
                    Next

                    If checkboxnum > 50 Then
                        checkboxnum = 1
                        CheckedListBox1.Items.Clear()
                        For k As Integer = 0 To 49
                            Chart1.Series(seriesname(k)).Points.Item(seriespointindex(k)).Label = ""
                            Chart1.Series(seriesname(k)).Points.Item(seriespointindex(k)).MarkerStyle = MarkerStyle.None
                        Next
                        ClearAllMarkersToolStripMenuItem.Enabled = False
                    End If
                    RemoveHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
                    CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(Datapoint.XValue, 3) & ", Y=" & Math.Round(Datapoint.YValues(0), 3), isChecked:=True)
                    AddHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
                    Datapoint.Label = checkboxnum
                    Datapoint.MarkerStyle = MarkerStyle.Triangle
                    Datapoint.MarkerSize = 10
                    Datapoint.MarkerColor = Chart1.Series(GlobalVariables.seriesnames(i)).Color
                    seriesname(checkboxnum - 1) = GlobalVariables.seriesnames(i)
                    seriespointindex(checkboxnum - 1) = j
                    ClearAllMarkersToolStripMenuItem.Enabled = True
                    checkboxnum += 1
                    prevxval = Datapoint.XValue
                    prevyval = Datapoint.YValues(0)
NextValue:
                Next
            Else
                Dim Datapoint As DataPoint = Nothing
                Dim j As Integer = 0
                For Each pt As DataPoint In Chart1.Series(GlobalVariables.Markertrace).Points
                    'For Each pt As DataPoint In Chart1.Series(GlobalVariables.seriesnames(GlobalVariables.Markertraceindex)).Points
                    Datapoint = pt
                    If pt.XValue >= GlobalVariables.Markerfreq Then
                        Exit For
                    End If
                    j += 1
                Next
                For i As Integer = 0 To checkboxnum - 1
                    If seriesname(i) = GlobalVariables.Markertrace AndAlso seriespointindex(i) = j Then
                        'If seriesname(i) = GlobalVariables.seriesnames(GlobalVariables.Markertraceindex) AndAlso seriespointindex(i) = j Then
                        MetroFramework.MetroMessageBox.Show(Me, "The marker is already available in the list. Refer marker #" & i + 1 & ".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Next
                If checkboxnum > 50 Then
                    checkboxnum = 1
                    CheckedListBox1.Items.Clear()
                    For i As Integer = 0 To 49
                        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).Label = ""
                        Chart1.Series(seriesname(i)).Points.Item(seriespointindex(i)).MarkerStyle = MarkerStyle.None
                    Next
                    ClearAllMarkersToolStripMenuItem.Enabled = False
                End If
                RemoveHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
                CheckedListBox1.Items.Add(checkboxnum & ". X=" & Math.Round(Datapoint.XValue, 3) & ", Y=" & Math.Round(Datapoint.YValues(0), 3), isChecked:=True)
                AddHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
                Datapoint.Label = checkboxnum
                Datapoint.MarkerStyle = MarkerStyle.Triangle
                Datapoint.MarkerSize = 10
                Datapoint.MarkerColor = Chart1.Series(GlobalVariables.Markertrace).Color
                seriesname(checkboxnum - 1) = GlobalVariables.Markertrace
                'Datapoint.MarkerColor = Chart1.Series(GlobalVariables.seriesnames(GlobalVariables.Markertraceindex)).Color
                'seriesname(checkboxnum - 1) = GlobalVariables.seriesnames(GlobalVariables.Markertraceindex)
                seriespointindex(checkboxnum - 1) = j
                ClearAllMarkersToolStripMenuItem.Enabled = True
                checkboxnum += 1
                prevxval = Datapoint.XValue
                prevyval = Datapoint.YValues(0)
                If Toggle1.Checked = True AndAlso (FirstDevice.Checked = True Or SecondDevice.Checked = True Or ThirdDevice.Checked = True) Then
                    System.Array.Resize(Of String)(livemarkername, livemarkername.Length + 1)
                    System.Array.Resize(Of Integer)(livemarkerlocation, livemarkerlocation.Length + 1)
                    livemarkername(livemarkername.Length - 1) = GlobalVariables.Markertrace
                    livemarkerlocation(livemarkerlocation.Length - 1) = j
                End If
            End If
        End If
    End Sub

    Private Sub LogMagToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogMagToolStripMenuItem.Click
        If LogMagToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = True
            LinearMagToolStripMenuItem.Checked = False
            PhaseToolStripMenuItem.Checked = False
            UnwrappedPhaseToolStripMenuItem.Checked = False
            PolarToolStripMenuItem.Checked = False
            SmithChartToolStripMenuItem.Checked = False
            InverseSmithChartToolStripMenuItem.Checked = False
            GroupDelayToolStripMenuItem.Checked = False
            SWRToolStripMenuItem.Checked = False
            RealToolStripMenuItem.Checked = False
            ImaginaryToolStripMenuItem.Checked = False
            GlobalVariables.chartformat = "logmag"
            ClearAll()
        End If
    End Sub

    Private Sub LinearMagToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LinearMagToolStripMenuItem.Click
        If LinearMagToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = False
            LinearMagToolStripMenuItem.Checked = True
            PhaseToolStripMenuItem.Checked = False
            UnwrappedPhaseToolStripMenuItem.Checked = False
            PolarToolStripMenuItem.Checked = False
            SmithChartToolStripMenuItem.Checked = False
            InverseSmithChartToolStripMenuItem.Checked = False
            GroupDelayToolStripMenuItem.Checked = False
            SWRToolStripMenuItem.Checked = False
            RealToolStripMenuItem.Checked = False
            ImaginaryToolStripMenuItem.Checked = False
            GlobalVariables.chartformat = "linearmag"
            ClearAll()
        End If
    End Sub

    Private Sub PhaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PhaseToolStripMenuItem.Click
        If PhaseToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = False
            LinearMagToolStripMenuItem.Checked = False
            PhaseToolStripMenuItem.Checked = True
            UnwrappedPhaseToolStripMenuItem.Checked = False
            PolarToolStripMenuItem.Checked = False
            SmithChartToolStripMenuItem.Checked = False
            InverseSmithChartToolStripMenuItem.Checked = False
            GroupDelayToolStripMenuItem.Checked = False
            SWRToolStripMenuItem.Checked = False
            RealToolStripMenuItem.Checked = False
            ImaginaryToolStripMenuItem.Checked = False
            GlobalVariables.chartformat = "phase"
            ClearAll()
        End If
    End Sub

    Private Sub UnwrappedPhaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnwrappedPhaseToolStripMenuItem.Click
        If UnwrappedPhaseToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = False
            LinearMagToolStripMenuItem.Checked = False
            PhaseToolStripMenuItem.Checked = False
            UnwrappedPhaseToolStripMenuItem.Checked = True
            PolarToolStripMenuItem.Checked = False
            SmithChartToolStripMenuItem.Checked = False
            InverseSmithChartToolStripMenuItem.Checked = False
            GroupDelayToolStripMenuItem.Checked = False
            SWRToolStripMenuItem.Checked = False
            RealToolStripMenuItem.Checked = False
            ImaginaryToolStripMenuItem.Checked = False
            GlobalVariables.chartformat = "unwrapped"
            ClearAll()
        End If
    End Sub

    Private Sub PolarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PolarToolStripMenuItem.Click
        If PolarToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = False
            LinearMagToolStripMenuItem.Checked = False
            PhaseToolStripMenuItem.Checked = False
            UnwrappedPhaseToolStripMenuItem.Checked = False
            PolarToolStripMenuItem.Checked = True
            SmithChartToolStripMenuItem.Checked = False
            InverseSmithChartToolStripMenuItem.Checked = False
            GroupDelayToolStripMenuItem.Checked = False
            SWRToolStripMenuItem.Checked = False
            RealToolStripMenuItem.Checked = False
            ImaginaryToolStripMenuItem.Checked = False
            GlobalVariables.chartformat = "polar"
            ClearAll()
        End If
    End Sub

    Private Sub SmithChartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SmithChartToolStripMenuItem.Click
        If SmithChartToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = False
            LinearMagToolStripMenuItem.Checked = False
            PhaseToolStripMenuItem.Checked = False
            UnwrappedPhaseToolStripMenuItem.Checked = False
            PolarToolStripMenuItem.Checked = False
            SmithChartToolStripMenuItem.Checked = True
            InverseSmithChartToolStripMenuItem.Checked = False
            GroupDelayToolStripMenuItem.Checked = False
            SWRToolStripMenuItem.Checked = False
            RealToolStripMenuItem.Checked = False
            ImaginaryToolStripMenuItem.Checked = False
            GlobalVariables.chartformat = "smith"
            ClearAll()
        End If
    End Sub

    Private Sub InverseSmithChartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InverseSmithChartToolStripMenuItem.Click
        If InverseSmithChartToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = False
            LinearMagToolStripMenuItem.Checked = False
            PhaseToolStripMenuItem.Checked = False
            UnwrappedPhaseToolStripMenuItem.Checked = False
            PolarToolStripMenuItem.Checked = False
            SmithChartToolStripMenuItem.Checked = False
            InverseSmithChartToolStripMenuItem.Checked = True
            GroupDelayToolStripMenuItem.Checked = False
            SWRToolStripMenuItem.Checked = False
            RealToolStripMenuItem.Checked = False
            ImaginaryToolStripMenuItem.Checked = False
            GlobalVariables.chartformat = "inversesmith"
            ClearAll()
        End If
    End Sub

    Private Sub GroupDelayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GroupDelayToolStripMenuItem.Click
        If GroupDelayToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = False
            LinearMagToolStripMenuItem.Checked = False
            PhaseToolStripMenuItem.Checked = False
            UnwrappedPhaseToolStripMenuItem.Checked = False
            PolarToolStripMenuItem.Checked = False
            SmithChartToolStripMenuItem.Checked = False
            InverseSmithChartToolStripMenuItem.Checked = False
            GroupDelayToolStripMenuItem.Checked = True
            SWRToolStripMenuItem.Checked = False
            RealToolStripMenuItem.Checked = False
            ImaginaryToolStripMenuItem.Checked = False
            GlobalVariables.chartformat = "groupdelay"
            ClearAll()
        End If
    End Sub

    Private Sub SWRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SWRToolStripMenuItem.Click
        If SWRToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = False
            LinearMagToolStripMenuItem.Checked = False
            PhaseToolStripMenuItem.Checked = False
            UnwrappedPhaseToolStripMenuItem.Checked = False
            PolarToolStripMenuItem.Checked = False
            SmithChartToolStripMenuItem.Checked = False
            InverseSmithChartToolStripMenuItem.Checked = False
            GroupDelayToolStripMenuItem.Checked = False
            SWRToolStripMenuItem.Checked = True
            RealToolStripMenuItem.Checked = False
            ImaginaryToolStripMenuItem.Checked = False
            GlobalVariables.chartformat = "swr"
            ClearAll()
        End If
    End Sub

    Private Sub RealToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RealToolStripMenuItem.Click
        If RealToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = False
            LinearMagToolStripMenuItem.Checked = False
            PhaseToolStripMenuItem.Checked = False
            UnwrappedPhaseToolStripMenuItem.Checked = False
            PolarToolStripMenuItem.Checked = False
            SmithChartToolStripMenuItem.Checked = False
            InverseSmithChartToolStripMenuItem.Checked = False
            GroupDelayToolStripMenuItem.Checked = False
            SWRToolStripMenuItem.Checked = False
            RealToolStripMenuItem.Checked = True
            ImaginaryToolStripMenuItem.Checked = False
            GlobalVariables.chartformat = "real"
            ClearAll()
        End If
    End Sub

    Private Sub ImaginaryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImaginaryToolStripMenuItem.Click
        If ImaginaryToolStripMenuItem.Checked = False Then
            LogMagToolStripMenuItem.Checked = False
            LinearMagToolStripMenuItem.Checked = False
            PhaseToolStripMenuItem.Checked = False
            UnwrappedPhaseToolStripMenuItem.Checked = False
            PolarToolStripMenuItem.Checked = False
            SmithChartToolStripMenuItem.Checked = False
            InverseSmithChartToolStripMenuItem.Checked = False
            GroupDelayToolStripMenuItem.Checked = False
            SWRToolStripMenuItem.Checked = False
            RealToolStripMenuItem.Checked = False
            ImaginaryToolStripMenuItem.Checked = True
            GlobalVariables.chartformat = "imaginary"
            ClearAll()
        End If
    End Sub

    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each filename In files
            If TextBox1.Text.Contains(filename) Then
                MetroFramework.MetroMessageBox.Show(Me, System.IO.Path.GetFileName(filename) & " is already added to the chart.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                GoTo Checknextfilename2
            End If
            extension = System.IO.Path.GetExtension(filename)
            If extension.Substring(0, 2).ToLower = ".s" AndAlso extension.Substring(extension.Length - 1, 1).ToLower = "p" Then
                Try
                    ports = System.Text.RegularExpressions.Regex.Replace(extension, "[^\d]", "")    'Remove Characters from a Numeric String
                Catch ex As Exception
                    MetroFramework.MetroMessageBox.Show(Me, "The selected file is not a compatible Touchstone file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    GoTo Checknextfilename2
                End Try
                NewToolRoutine()
            ElseIf extension.ToLower = ".xls" Or extension.ToLower = ".xlsx" Then
                AddToolRoutine()
            Else
                MetroFramework.MetroMessageBox.Show(Me, "The selected file is not compatible with SAT Test Suite", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                GoTo Checknextfilename2
            End If
Checknextfilename2:
        Next
    End Sub

    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub TestModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TestModeToolStripMenuItem.Click
        GlobalVariables.okbutton = "cancel"
        'For Each name As String In TextBox1.Lines
        '    If name <> "" Then
        '        MsgBox(name.Split("\"c).Last())
        '    End If
        'Next
        'For x = 0 To TextBox1.Lines.Length - 2
        '    MsgBox(CInt(GlobalVariables.seriesnames(x).Split("#"c).Last()).ToString - 1)
        '    MsgBox(TextBox1.Lines(CInt(GlobalVariables.seriesnames(x).Split("#"c).Last()).ToString - 1))
        'Next
        'Exit Sub
        Form3.ShowDialog()
        If GlobalVariables.okbutton = "ok" Then
            GlobalVariables.dt = New Data.DataTable
            GlobalVariables.dt.Columns.AddRange(New DataColumn() {New DataColumn("File Name", GetType(String)), New DataColumn("Series Name", GetType(String)), New DataColumn("Start Frequency (GHz)", GetType(Double)), New DataColumn("Stop Frequency (GHz)", GetType(Double)), New DataColumn("Test Result", GetType(String))})
            For j As Integer = 0 To GlobalVariables.seriesnames.Length - 1
                For i As Integer = 0 To GlobalVariables.teststring.Length - 1
                    If GlobalVariables.seriesnames(j).ToLower.Contains(GlobalVariables.teststring(i).ToLower) Then
                        'MsgBox(Chart1.Series(GlobalVariables.seriesnames(j)).Points.First.XValue & " " & Chart1.Series(GlobalVariables.seriesnames(j)).Points.Last.XValue)
                        'MsgBox(Chart1.Series(GlobalVariables.seriesnames(j)).Points.FindByValue(GlobalVariables.testxaxisstart(i), "X").XValue)    'Null reference if no datapoint is found
                        If (GlobalVariables.testxaxisstart(i) >= Chart1.Series(GlobalVariables.seriesnames(j)).Points.First.XValue) AndAlso (GlobalVariables.testxaxisstart(i) <= Chart1.Series(GlobalVariables.seriesnames(j)).Points.Last.XValue) Then
                            'Check if the start X axis value is already available in the chart or not
                            Dim pt1 As New DataPoint
                            Dim pt2 As New DataPoint
                            For Each pt As DataPoint In Chart1.Series(GlobalVariables.seriesnames(j)).Points
                                If pt.XValue > GlobalVariables.testxaxisstart(i) Then
                                    pt2 = pt
                                    Exit For
                                End If
                                pt1 = pt
                            Next
                            yvalue = ((pt2.YValues(0) - pt1.YValues(0)) / (pt2.XValue - pt1.XValue) * GlobalVariables.testxaxisstart(i)) + (pt2.YValues(0) - (((pt2.YValues(0) - pt1.YValues(0)) / (pt2.XValue - pt1.XValue)) * pt2.XValue))
                            If (yvalue > GlobalVariables.testvaluemax(i)) Or (yvalue < GlobalVariables.testvaluemin(i)) Then
                                GlobalVariables.dt.Rows.Add(TextBox1.Lines(CInt(GlobalVariables.seriesnames(j).Split("#"c).Last()).ToString - 1).Split("\"c).Last(), GlobalVariables.seriesnames(j), GlobalVariables.testxaxisstart(i), GlobalVariables.testxaxisstop(i), "Fail")
                            Else
                                If (GlobalVariables.testxaxisstop(i) < Chart1.Series(GlobalVariables.seriesnames(j)).Points.Last.XValue) Then
                                    For Each pt As DataPoint In Chart1.Series(GlobalVariables.seriesnames(j)).Points
                                        If pt.XValue > GlobalVariables.testxaxisstart(i) Then
                                            pt2 = pt
                                            Exit For
                                        End If
                                        pt1 = pt
                                    Next
                                    yvalue = ((pt2.YValues(0) - pt1.YValues(0)) / (pt2.XValue - pt1.XValue) * GlobalVariables.testxaxisstop(i)) + (pt2.YValues(0) - (((pt2.YValues(0) - pt1.YValues(0)) / (pt2.XValue - pt1.XValue)) * pt2.XValue))
                                End If
                                If (yvalue > GlobalVariables.testvaluemax(i)) Or (yvalue < GlobalVariables.testvaluemin(i)) Then
                                    GlobalVariables.dt.Rows.Add(TextBox1.Lines(CInt(GlobalVariables.seriesnames(j).Split("#"c).Last()).ToString - 1).Split("\"c).Last(), GlobalVariables.seriesnames(j), GlobalVariables.testxaxisstart(i), GlobalVariables.testxaxisstop(i), "Fail")
                                Else
                                    For Each pt As DataPoint In Chart1.Series(GlobalVariables.seriesnames(j)).Points
                                        If pt.XValue >= GlobalVariables.testxaxisstart(i) Then   'Math.Round(pt.XValue, 3) to pt.XValue
                                            If pt.XValue <= GlobalVariables.testxaxisstop(i) Then
                                                If (pt.YValues(0) > GlobalVariables.testvaluemax(i)) Or (pt.YValues(0) < GlobalVariables.testvaluemin(i)) Then    'Math.Round(pt.YValues(0), 3 changed to pt.YValues(0)
                                                    GlobalVariables.dt.Rows.Add(TextBox1.Lines(CInt(GlobalVariables.seriesnames(j).Split("#"c).Last()).ToString - 1).Split("\"c).Last(), GlobalVariables.seriesnames(j), GlobalVariables.testxaxisstart(i), GlobalVariables.testxaxisstop(i), "Fail")
                                                    'MsgBox(TextBox1.Lines(CInt(GlobalVariables.seriesnames(j).Split("#"c).Last()).ToString - 1).Split("\"c).Last() & "," & GlobalVariables.seriesnames(j) & "," & GlobalVariables.testxaxisstart(i) & "," & GlobalVariables.testxaxisstop(i) & "," & "Fail")
                                                    Exit For
                                                End If
                                            Else
                                                Exit For
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                            End If
                    End If
                Next
            Next
            Form5.ShowDialog()
        End If
    End Sub

    'Private curPoint As DataPoint = Nothing
    'Private oldXY As Point = Nothing

    'Private Sub Chart1_MouseUp(sender As Object, e As MouseEventArgs) Handles Chart1.MouseUp
    '    curPoint = Nothing
    'End Sub

    'Private Sub Chart1_MouseMove(sender As Object, e As MouseEventArgs) Handles Chart1.MouseMove
    '    If e.Button.HasFlag(MouseButtons.Left) Then
    '        If e.X > oldXY.X Then
    '            MsgBox("Right")
    '        ElseIf e.X < oldXY.X Then
    '            MsgBox("Left")
    '            End If
    '            oldXY.X = e.X
    '            oldXY.Y = e.Y

    '            Dim ca As DataVisualization.Charting.ChartArea = Chart1.ChartAreas(0)
    '            Dim ax As DataVisualization.Charting.Axis = ca.AxisX
    '            Dim ay As DataVisualization.Charting.Axis = ca.AxisY

    '            Dim hit As HitTestResult = Chart1.HitTest(e.X, e.Y)
    '            If hit.PointIndex >= 0 Then
    '                curPoint = hit.Series.Points(hit.PointIndex)
    '            End If

    '            If curPoint IsNot Nothing Then
    '                Dim s As DataVisualization.Charting.Series = hit.Series
    '                Dim dx As Double = ax.PixelPositionToValue(e.X)
    '                Dim dy As Double = ay.PixelPositionToValue(e.Y)

    '                curPoint.XValue = dx
    '                curPoint.YValues(0) = dy
    '            End If
    '        End If
    'End Sub
End Class

Public Class GlobalVariables
    Public Shared xaxismin As Double = 0.5
    Public Shared xaxismax As Double = 3
    Public Shared xaxisint As Double = 0.1
    Public Shared yaxismin As Double = -30
    Public Shared yaxismax As Double = 0
    Public Shared yaxisint As Double = 3
    Public Shared y2axismin As Double = vbNull
    Public Shared y2axismax As Double = vbNull
    Public Shared y2axisint As Double = vbNull
    Public Shared okbutton As String
    Public Shared autobutton As Boolean = False
    'Public Shared ports As Integer
    Public Shared series() As Integer
    Public Shared seriesnames() As String
    Public Shared y2axis As Boolean
    Public Shared plot As String = "efficiency"
    Public Shared DeviceName() As String
    Public Shared DeviceAddress() As String
    Public Shared Markertraceindex As Integer = -1
    Public Shared Markerfreq As Double = 0
    Public Shared Markertrace As String = ""
    Public Shared chartformat As String = ""
    Public Shared teststring() As String
    Public Shared testxaxisstop() As Double
    Public Shared testxaxisstart() As Double
    Public Shared testvaluemax() As Double
    Public Shared testvaluemin() As Double
    Public Shared dt As System.Data.DataTable

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
'                                Chart1.ChartAreas("ChartArea1").AxisX.LabelStyle.Format = "{0:0.##}"
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