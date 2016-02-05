Public Class Form7
    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        If textBox1.Text = "admin3" And textBox2.Text = "admin3" Then
            If Form4.pword = 1 Then
                MsgBox("Administrator mode deactivated", MsgBoxStyle.Information, "Login")
                Me.Close()
                Form4.pword = 0
            Else
                Form4.pword = 1
                MsgBox("Administrator mode activated", MsgBoxStyle.Information, "Login")
                Me.Close()
            End If
        Else
            MsgBox("Wrong Username and/or Password combination", MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    Private Sub button2_Click(sender As Object, e As EventArgs) Handles button2.Click
        Me.Close()
    End Sub

    Private Sub textBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles textBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If textBox1.Text = "admin3" And textBox2.Text = "admin3" Then
                If Form4.pword = 1 Then
                    MsgBox("Administrator mode deactivated", MsgBoxStyle.Information, "Login")
                    Me.Close()
                    Form4.pword = 0
                Else
                    Form4.pword = 1
                    MsgBox("Administrator mode activated", MsgBoxStyle.Information, "Login")
                    Me.Close()
                End If
            Else
                MsgBox("Wrong Username and/or Password combination", MsgBoxStyle.Critical, "Error")
            End If
        End If
    End Sub

    Private Sub textBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles textBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If textBox1.Text = "admin3" And textBox2.Text = "admin3" Then
                If Form4.pword = 1 Then
                    MsgBox("Administrator mode deactivated", MsgBoxStyle.Information, "Login")
                    Me.Close()
                    Form4.pword = 0
                Else
                    Form4.pword = 1
                    MsgBox("Administrator mode activated", MsgBoxStyle.Information, "Login")
                    Me.Close()
                End If
            Else
                MsgBox("Wrong Username and/or Password combination", MsgBoxStyle.Critical, "Error")
            End If
        End If
    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        textBox1.Text = ""
        textBox2.Text = ""
    End Sub

End Class