Public Class Form1
    Private Sub button2_Click(sender As Object, e As EventArgs) Handles button2.Click
        Form2.Show()
        Me.Close()
    End Sub

    Private Sub button3_Click(sender As Object, e As EventArgs) Handles button3.Click
        Form3.Show()
        Me.Close()
    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        MsgBox("Logged into Demo Mode", MsgBoxStyle.Information, "Login")
        Form4.Show()
        Me.Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
