Public Class Form3
    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RemoveHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
        Me.DataGridView1.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 13.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        Me.DataGridView1.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 12.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        Me.DataGridView1.Rows.Add("Agilent Technologies N5230A Network Analyzer", "TCPIP0:: 10.1.100.174::hpib7,16::INSTR")
        AddHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GlobalVariables.DeviceName(0) = DataGridView1.Item(0, 0).Value.ToString()
        GlobalVariables.DeviceAddress(0) = DataGridView1.Item(1, 0).Value.ToString()
        'MsgBox("Device Name is " & GlobalVariables.DeviceName(0) & " address is " & GlobalVariables.DeviceAddress(0))
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        'If DataGridView1.Item(1, 0).Value.ToString() = "" Then
        'DataGridView1.Item(1, 0).Value = "TCPIP0::10.1.100.174::hpib7,16::INSTR"
        'End If
    End Sub
End Class