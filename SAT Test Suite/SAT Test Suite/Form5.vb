Imports Excel

Public Class Form5
    Dim dialog2 As SaveFileDialog = New SaveFileDialog()
    Dim excelReader As IExcelDataReader

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RemoveHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 13.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        DataGridView1.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        If GlobalVariables.chartformat = "logmag" Then
            DataGridView1.Columns(3).HeaderText = "Maximum value (dB)"
            DataGridView1.Columns(4).HeaderText = "Minimum value (dB)"
        ElseIf (GlobalVariables.chartformat = "linearmag") Or (GlobalVariables.chartformat = "real") Or (GlobalVariables.chartformat = "imaginary") Then
            DataGridView1.Columns(3).HeaderText = "Maximum value (val)"
            DataGridView1.Columns(4).HeaderText = "Minimum value (val)"
        ElseIf GlobalVariables.chartformat = "phase" Then
            DataGridView1.Columns(3).HeaderText = "Maximum value (deg)"
            DataGridView1.Columns(4).HeaderText = "Minimum value (deg)"
        End If
        If Not GlobalVariables.teststring Is Nothing Then
            For i As Integer = 0 To GlobalVariables.teststring.Length - 1
                DataGridView1.Rows.Add(GlobalVariables.teststring(i), GlobalVariables.testxaxisstart(i), GlobalVariables.testxaxisstop(i), GlobalVariables.testvaluemax(i), GlobalVariables.testvaluemin(i))
            Next
        End If
        AddHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged

        RemoveHandler DataGridView2.CellValueChanged, AddressOf DataGridView2_CellValueChanged
        DataGridView2.Columns.Clear()
        DataGridView2.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 13.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        DataGridView2.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        DataGridView2.DataSource = GlobalVariables.dt
        AddHandler DataGridView2.CellValueChanged, AddressOf DataGridView2_CellValueChanged
        TabControl1.SelectedTab = TabPage1
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged

    End Sub


    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If DataGridView2.RowCount > 1 Then
                dialog2.Title = "Save Data"
                dialog2.Filter = "Excel Workbook (*.xlsx)|*.xlsx"
                dialog2.FilterIndex = 1
                dialog2.RestoreDirectory = True
                dialog2.FileName = ""
                If dialog2.ShowDialog() = DialogResult.OK Then
                    Dim xlApp As New Microsoft.Office.Interop.Excel.Application
                    Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
                    Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
                    Dim misValue As Object = System.Reflection.Missing.Value

                    xlWorkBook = xlApp.Workbooks.Add(misValue)
                    xlWorkSheet = xlWorkBook.Sheets.Add(Count:=1)
                    Dim xlWorkSheet1 As Microsoft.Office.Interop.Excel.Worksheet = xlWorkBook.Sheets(1)
                    Dim xlWorkSheet2 As Microsoft.Office.Interop.Excel.Worksheet = xlWorkBook.Sheets(2)

                    xlWorkSheet2 = xlWorkBook.Sheets("sheet2")      'To bring Sheet1 as the first page

                    For i As Integer = 0 To DataGridView1.RowCount - 1
                        For j As Integer = 0 To DataGridView1.ColumnCount - 1
                            For k As Integer = 1 To DataGridView1.Columns.Count
                                xlWorkSheet2.Cells(1, k) = DataGridView1.Columns(k - 1).HeaderText
                                xlWorkSheet2.Cells(i + 2, j + 1) = DataGridView1(j, i).Value.ToString()
                            Next
                        Next
                    Next

                    xlWorkSheet1 = xlWorkBook.Sheets("sheet1")

                    For i As Integer = 0 To DataGridView2.RowCount - 1
                        For j As Integer = 0 To DataGridView2.ColumnCount - 1
                            For k As Integer = 1 To DataGridView2.Columns.Count
                                xlWorkSheet1.Cells(1, k) = DataGridView2.Columns(k - 1).HeaderText
                                xlWorkSheet1.Cells(i + 2, j + 1) = DataGridView2(j, i).Value.ToString()
                            Next
                        Next
                    Next

                    xlWorkSheet.SaveAs(dialog2.FileName)
                    xlWorkBook.Close()
                    xlApp.Quit()

                    releaseObject(xlApp)
                    releaseObject(xlWorkBook)
                    releaseObject(xlWorkSheet)

                End If
            Else
                MetroFramework.MetroMessageBox.Show(Me, "No data available to save. Please reconfirm the values and try again.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
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

    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub
End Class