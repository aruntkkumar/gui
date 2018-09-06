Imports Excel

Public Class Form7
    Dim dialog2 As SaveFileDialog = New SaveFileDialog()
    Dim excelReader As IExcelDataReader

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RemoveHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 13.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        DataGridView1.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        DataGridView1.DataSource = GlobalVariables.dt
        If GlobalVariables.chartformat = "logmag" Then
            DataGridView1.Columns(5).HeaderText = "Average Value (dB)"
        ElseIf (GlobalVariables.chartformat = "linearmag") Or (GlobalVariables.chartformat = "real") Or (GlobalVariables.chartformat = "imaginary") Then
            DataGridView1.Columns(5).HeaderText = "Average Value (val)"
        ElseIf GlobalVariables.chartformat = "phase" Then
            DataGridView1.Columns(5).HeaderText = "Average Value (deg)"
        End If
        AddHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

    End Sub

    Private Sub Form7_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            If DataGridView1.RowCount > 1 Then
                dialog2.Title = "Save Average Data"
                dialog2.Filter = "Excel Workbook (*.xlsx)|*.xlsx"
                dialog2.FilterIndex = 1
                dialog2.RestoreDirectory = True
                dialog2.FileName = ""
                If dialog2.ShowDialog() = DialogResult.OK Then

                    Dim _excel As New Microsoft.Office.Interop.Excel.Application
                    Dim wBook As Microsoft.Office.Interop.Excel.Workbook
                    Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
                    Dim misvalue1 As Object = System.Reflection.Missing.Value

                    wBook = _excel.Workbooks.Add(misvalue1)
                    wSheet = wBook.Sheets("Sheet1")

                    Dim colIndex As Integer = 0
                    Dim rowIndex As Integer = 0
                    Dim rowcount As Integer = GlobalVariables.dt.Rows.Count  'Number of rows

                    '(Write column headers and data)

                    For Each dc In GlobalVariables.dt.Columns
                        colIndex = colIndex + 1
                        wSheet.Cells(1, colIndex) = dc.ColumnName   'Column headers
                        '(Data).You can use CDbl instead of Cobj If your data is of type Double
                        wSheet.Cells(2, colIndex).Resize(rowcount, ).Value = _excel.Application.transpose(GlobalVariables.dt.Rows.OfType(Of DataRow)().[Select](Function(k) CObj(k(dc.ColumnName))).ToArray())
                    Next

                    wSheet.SaveAs(dialog2.FileName)
                    wBook.Close()
                    _excel.Quit()

                    releaseObject(_excel)
                    releaseObject(wBook)
                    releaseObject(wSheet)

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
End Class