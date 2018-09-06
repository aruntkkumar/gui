Imports Excel
Imports System.IO

Public Class Form6
    Dim excelReader As IExcelDataReader
    Dim dialog1 As OpenFileDialog = New OpenFileDialog()
    Dim dialog2 As SaveFileDialog = New SaveFileDialog()
    Dim test As Double = 0.0
    Dim flag As Integer = 0

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RemoveHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
        DataGridView1.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 13.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        DataGridView1.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        If Not GlobalVariables.avgteststring Is Nothing Then
            For i As Integer = 0 To GlobalVariables.avgteststring.Length - 1
                DataGridView1.Rows.Add(GlobalVariables.avgteststring(i), GlobalVariables.avgtestxaxisstart(i), GlobalVariables.avgtestxaxisstop(i))
            Next
        End If
        AddHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        RemoveHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
        DataGridView1.Rows.Clear()
        AddHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            dialog1.Title = "Load Average Data"
            dialog1.Multiselect = False
            dialog1.Filter = "Excel Workbook (*.xlsx)|*.xlsx|Excel 97-2003 Workbook (*.xls)|*.xls"
            dialog1.FilterIndex = 1
            dialog1.RestoreDirectory = True
            dialog1.FileName = ""
            If dialog1.ShowDialog() = DialogResult.OK Then
                Dim stream As FileStream = File.Open(dialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                If System.IO.Path.GetExtension(dialog1.FileName).ToLower() = ".xls" Then       ' Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream)
                ElseIf System.IO.Path.GetExtension(dialog1.FileName).ToLower() = ".xlsx" Then  ' Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                End If
                Dim result As DataSet = excelReader.AsDataSet()                     ' DataSet - The result of each spreadsheet will be created in the result.Tables
                excelReader.Close()                                                 ' Free resources (IExcelDataReader is IDisposable)
                excelReader.Dispose()
                If (result.Tables(0).Columns.Count = 3) Then
                    If (result.Tables(0).Rows.Count = 1) Then
                        MetroFramework.MetroMessageBox.Show(Me, "No data found. Please check the file and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    For i As Integer = 1 To result.Tables(0).Rows.Count - 1
                        For j As Integer = 1 To result.Tables(0).Columns.Count - 1  ' 1 to 2 (2nd and 3rd columns)
                            Try
                                test = CDbl(result.Tables(0).Rows(i)(j))
                            Catch ex As Exception
                                MetroFramework.MetroMessageBox.Show(Me, "Invalid data at (""" & i + 1 & "," & j + 1 & """). Please check and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End Try

                            'Routine to check the frequency values
                            If j = 1 Then
                                If (result.Tables(0).Rows(i)(j) < 0) Or (result.Tables(0).Rows(i)(j) > 100) Then
                                    MetroFramework.MetroMessageBox.Show(Me, "The start frequency " & result.Tables(0).Rows(i)(j) & "GHz from the cell (" & i + 1 & "," & j + 1 & ") cannot be smaller than 0 or greater than 100GHz. Please reconfirm the value in GHz and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            ElseIf j = 2 Then
                                If (result.Tables(0).Rows(i)(j) < 0) Or (result.Tables(0).Rows(i)(j) > 100) Then
                                    MetroFramework.MetroMessageBox.Show(Me, "The stop frequency " & result.Tables(0).Rows(i)(j) & "GHz from the cell (" & i + 1 & "," & j + 1 & ") cannot be smaller than 0 or greater than 100GHz. Please reconfirm the values in GHz and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                If (result.Tables(0).Rows(i)(j)) < (result.Tables(0).Rows(i)(j - 1)) Then
                                    MetroFramework.MetroMessageBox.Show(Me, "The stop frequency " & result.Tables(0).Rows(i)(j) & "GHz from the cell (" & i + 1 & "," & j + 1 & ") cannot be smaller than the start frequency. Please reconfirm the value in GHz and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If

                        Next
                    Next
                    RemoveHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
                    For i As Integer = 1 To result.Tables(0).Rows.Count - 1
                        DataGridView1.Rows.Add(result.Tables(0).Rows(i)(0), result.Tables(0).Rows(i)(1), result.Tables(0).Rows(i)(2))
                    Next
                    AddHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
                Else
                    MetroFramework.MetroMessageBox.Show(Me, "Unsupported data file. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged

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
                    Dim xlApp As New Microsoft.Office.Interop.Excel.Application
                    Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
                    Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
                    Dim misValue As Object = System.Reflection.Missing.Value

                    xlWorkBook = xlApp.Workbooks.Add(misValue)
                    xlWorkSheet = xlWorkBook.Sheets("sheet1")

                    For i As Integer = 0 To DataGridView1.RowCount - 2
                        For j As Integer = 0 To DataGridView1.ColumnCount - 1
                            For k As Integer = 1 To DataGridView1.Columns.Count
                                xlWorkSheet.Cells(1, k) = DataGridView1.Columns(k - 1).HeaderText
                                xlWorkSheet.Cells(i + 2, j + 1) = DataGridView1(j, i).Value.ToString()
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If DataGridView1.RowCount > 1 Then
                For i As Integer = 0 To DataGridView1.RowCount - 2
                    For j As Integer = 0 To DataGridView1.ColumnCount - 1
                        'Routine to check if the cell is empty or not.
                        If DataGridView1.Item(j, i).Value Is Nothing Then
                            MetroFramework.MetroMessageBox.Show(Me, "The cell (" & i + 1 & "," & j + 1 & ") is empty. Please enter the data or delete the row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        'Routine to check if the search phrase is any of the series names
                        If j = 0 Then
                            flag = 0
                            For k As Integer = 0 To GlobalVariables.seriesnames.Length - 1
                                If GlobalVariables.seriesnames(k).ToLower.Contains(DataGridView1.Item(0, i).Value.ToString().ToLower) Then
                                    flag = 1
                                    Exit For
                                End If
                            Next
                            If flag = 0 Then
                                MetroFramework.MetroMessageBox.Show(Me, "The search phrase """ & DataGridView1.Item(0, i).Value.ToString() & """ from the cell (" & i + 1 & "," & j + 1 & ") was not found among the plotted series names. Please reconfirm and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        End If
                        'Routine to check the frequency values
                        If j = 1 Then
                            Try
                                test = CDbl(DataGridView1.Item(j, i).Value)
                            Catch ex As Exception
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value for the cell (" & i + 1 & "," & j + 1 & ") between 0 to 100GHz as start frequency", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End Try
                            If (DataGridView1.Item(j, i).Value < 0) Or (DataGridView1.Item(j, i).Value > 100) Then
                                MetroFramework.MetroMessageBox.Show(Me, "The start frequency " & DataGridView1.Item(1, i).Value & "GHz from the cell (" & i + 1 & "," & j + 1 & ") cannot be smaller than 0 or greater than 100GHz. Please reconfirm the value in GHz and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        ElseIf j = 2 Then
                            Try
                                test = CDbl(DataGridView1.Item(j, i).Value)
                            Catch ex As Exception
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value for the cell (" & i + 1 & "," & j + 1 & ") between 0 to 100GHz as stop frequency", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End Try
                            If (DataGridView1.Item(j, i).Value < 0) Or (DataGridView1.Item(j, i).Value > 100) Then
                                MetroFramework.MetroMessageBox.Show(Me, "The stop frequency " & DataGridView1.Item(2, i).Value & "GHz from the cell (" & i + 1 & "," & j + 1 & ") cannot be smaller than 0 or greater than 100GHz. Please reconfirm the values in GHz and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                            If DataGridView1.Item(j, i).Value < DataGridView1.Item(j - 1, i).Value Then
                                MetroFramework.MetroMessageBox.Show(Me, "The stop frequency " & DataGridView1.Item(2, i).Value & "GHz from the cell (" & i + 1 & "," & j + 1 & ") cannot be smaller than the start frequency. Please reconfirm the value in GHz and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        End If
                    Next
                Next
            End If
            GlobalVariables.avgteststring = New String(DataGridView1.RowCount - 2) {}
            GlobalVariables.avgtestxaxisstop = New Double(DataGridView1.RowCount - 2) {}
            GlobalVariables.avgtestxaxisstart = New Double(DataGridView1.RowCount - 2) {}
            For i As Integer = 0 To DataGridView1.RowCount - 2
                GlobalVariables.avgteststring(i) = DataGridView1.Item(0, i).Value.ToString()
                GlobalVariables.avgtestxaxisstart(i) = DataGridView1.Item(1, i).Value
                GlobalVariables.avgtestxaxisstop(i) = DataGridView1.Item(2, i).Value
            Next
            GlobalVariables.okbutton = "ok"
            If Not Me.IsDisposed() Then
                Try
                    Me.Dispose()
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Exit Sub
        End Try
    End Sub

    Private Sub Form6_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub
End Class