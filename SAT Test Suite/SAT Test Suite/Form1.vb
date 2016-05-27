Imports MetroFramework.Controls
Imports System.Reflection
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Form1
    'Define your Excel Objects
    Dim xlApp As New Excel.Application
    Dim xlWorkBook As Excel.Workbook
    Dim x As Integer = 0
    Dim array(100) As Integer
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NumericUpDown1.Minimum = 1
        NumericUpDown1.Maximum = 100
        Chart1.Series.Clear()
        Chart1.Series.Add("A")
        Chart1.ChartAreas("ChartArea1").AxisX.Minimum = 0
        Chart1.ChartAreas("ChartArea1").AxisX.Maximum = 100
        Chart1.ChartAreas("ChartArea1").AxisX.Interval = 10
        Chart1.ChartAreas("ChartArea1").AxisX.Title = "Frequency in Hz"
        Chart1.ChartAreas("ChartArea1").AxisY.Minimum = -100
        Chart1.ChartAreas("ChartArea1").AxisY.Maximum = 100
        Chart1.ChartAreas("ChartArea1").AxisY.Interval = 20
        Chart1.ChartAreas("ChartArea1").AxisY.Title = "y"
        Chart1.Series("A").ChartType = DataVisualization.Charting.SeriesChartType.Line
        For i As Integer = 0 To 100
            array(i) = 0
            Chart1.Series("A").Points.AddXY(i, array(i))
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        If button1.Text = "Start" Then
            button1.Text = "Stop"
            Timer1.Interval = NumericUpDown1.Value
            Timer1.Start()
        Else
            button1.Text = "Start"
            Timer1.Stop()
        End If
        ''~~> Add a New Workbook
        'xlWorkBook = xlApp.Workbooks.Add

        ''~~> Display Excel
        'xlApp.Visible = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        x = x + 1
        For i As Integer = 0 To 99
            array(i) = array(i + 1)
        Next
        array(100) = 100 * (Math.Sin(0.1 * x))
        Chart1.Series("A").Points.Clear()
        For i As Integer = 0 To 100
            Chart1.Series("A").Points.AddXY(i, array(i))
        Next
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        Timer1.Interval = NumericUpDown1.Value
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dialog As FolderBrowserDialog = New FolderBrowserDialog()
        dialog.Description = "Select a spreadsheet to plot the values"
        dialog.ShowDialog(Me)
        If dialog.SelectedPath <> Nothing Then
            '~~> Opens an exisiting Workbook. Change path and filename as applicable
            xlWorkBook = xlApp.Workbooks.Open("C:\Tutorial\Sample.xlsx")

            '~~> Display Excel
            xlApp.Visible = True
        End If
    End Sub
End Class