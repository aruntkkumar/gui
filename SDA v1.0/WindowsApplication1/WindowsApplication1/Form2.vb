Public Class Form2
    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        If textBox1.Text = "admin1" And textBox2.Text = "admin1" Then
            MsgBox("Logged into System Mode", MsgBoxStyle.Information, "Login")
            Form5.Show()
            Me.Close()
        Else
            MsgBox("Wrong Username and/or Password combination", MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    Private Sub button2_Click(sender As Object, e As EventArgs) Handles button2.Click
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub textBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles textBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If textBox1.Text = "admin1" And textBox2.Text = "admin1" Then
                MsgBox("Logged into System Mode", MsgBoxStyle.Information, "Login")
                Form5.Show()
                Me.Close()
            Else
                MsgBox("Wrong Username and/or Password combination", MsgBoxStyle.Critical, "Error")
            End If
        End If
    End Sub

    Private Sub textBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles textBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If textBox1.Text = "admin1" And textBox2.Text = "admin1" Then
                MsgBox("Logged into System Mode", MsgBoxStyle.Information, "Login")
                Form5.Show()
                Me.Close()
            Else
                MsgBox("Wrong Username and/or Password combination", MsgBoxStyle.Critical, "Error")
            End If
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class