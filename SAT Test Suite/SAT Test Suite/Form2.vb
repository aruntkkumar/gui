Public Class Form2

    Dim test As Double

    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = GlobalVariables.xaxismax
        TextBox2.Text = GlobalVariables.xaxismin
        TextBox3.Text = GlobalVariables.yaxismax
        TextBox4.Text = GlobalVariables.yaxismin
        GlobalVariables.button = "cancel"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then     'Check if TextBox1 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as X-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox1.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as X-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < 0) Or (test > 100) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 and +100 as X-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox2.Text = "" Then     'Check if TextBox2 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as X-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox2.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as X-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < 0) Or (test > 100) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 and +100 as X-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox3.Text = "" Then     'Check if TextBox3 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox3.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < -1000) Or (test > 1000) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between -1000 and +1000 as Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox4.Text = "" Then     'Check if TextBox4 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox4.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < -1000) Or (test > 1000) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between -1000 and +1000 as Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If (CDbl(TextBox1.Text) - CDbl(TextBox2.Text)) < 0.5 Then
            MetroFramework.MetroMessageBox.Show(Me, "The X-axis maximum value should be atleast 0.5 more than the X-axis minimum value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If (CDbl(TextBox3.Text) - CDbl(TextBox4.Text)) < 5 Then
            MetroFramework.MetroMessageBox.Show(Me, "The Y-axis maximum value should be atleast 5 more than the Y-axis minimum value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        GlobalVariables.xaxismax = TextBox1.Text
        GlobalVariables.xaxismin = TextBox2.Text
        GlobalVariables.yaxismax = TextBox3.Text
        GlobalVariables.yaxismin = TextBox4.Text
        GlobalVariables.button = "ok"
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        GlobalVariables.button = "cancel"
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub
End Class