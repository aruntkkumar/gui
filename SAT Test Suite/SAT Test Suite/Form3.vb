Public Class Form3
    Dim pos As Integer = 0

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
        'Me.DataGridView1.Rows.Add("Agilent Technologies N5230A Network Analyzer", "TCPIP0:: 10.1.100.174::hpib7,16::INSTR")
        Me.DataGridView1.Rows.Add(GlobalVariables.DeviceName(0), GlobalVariables.DeviceAddress(0))
        Me.DataGridView1.Rows.Add(GlobalVariables.DeviceName(1), GlobalVariables.DeviceAddress(1))
        AddHandler DataGridView1.CellValueChanged, AddressOf DataGridView1_CellValueChanged
        pos = 1
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
        'GlobalVariables.DeviceName(0) = DataGridView1.Item(0, 0).Value.ToString()
        'MsgBox("'" & DataGridView1.Item(1, 0).Value & "'")
        'Exit Sub
        GlobalVariables.DeviceAddress(0) = DataGridView1.Item(1, 0).Value.ToString()
        GlobalVariables.DeviceAddress(1) = DataGridView1.Item(1, 1).Value.ToString()
        'MsgBox("Device Name is " & GlobalVariables.DeviceName(0) & " address is " & GlobalVariables.DeviceAddress(0))
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If pos = 1 Then
            If DataGridView1.Item(1, 0).Value = "" Then
                DataGridView1.Item(1, 0).Value = GlobalVariables.DeviceAddress(0)
            End If
            If DataGridView1.Item(1, 1).Value = "" Then
                DataGridView1.Item(1, 1).Value = GlobalVariables.DeviceAddress(1)
            End If
        End If
    End Sub
End Class