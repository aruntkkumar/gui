Public Class Form4

    Dim test As Double

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (GlobalVariables.series.Length <> 0) Then
            For i As Integer = 0 To GlobalVariables.seriesnames.Length - 1
                If GlobalVariables.series(i) = 1 Then
                    ComboBox1.Items.Add(GlobalVariables.seriesnames(i))
                End If
            Next
        Else
            TextBox1.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex = -1 Then
            If (GlobalVariables.series.Length = 0) Then
                MyBase.Close()
                Exit Sub
            Else
                MetroFramework.MetroMessageBox.Show(Me, "No trace selected. Please select one from a drop down list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox1.Text = "" Then
            MetroFramework.MetroMessageBox.Show(Me, "No frequency value. Please enter the frequency in the textbox provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox1.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Invalid data. Please enter a valid frequency value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If test < 0.0 Then
                MetroFramework.MetroMessageBox.Show(Me, "Invalid data. Please enter a valid frequency value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        GlobalVariables.Markertraceindex = ComboBox1.SelectedIndex
        GlobalVariables.Markerfreq = test
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub
End Class