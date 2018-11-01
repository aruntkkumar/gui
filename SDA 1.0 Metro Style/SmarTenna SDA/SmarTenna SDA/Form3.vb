Public Class Form3
    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        If textBox1.Text = "admin2" And textBox2.Text = "admin2" Then
            MetroFramework.MetroMessageBox.Show(Me, "Logged into Engineering Mode", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Form9.Show()
            Me.Close()
        Else
            MetroFramework.MetroMessageBox.Show(Me, "Wrong Username and/or Password combination", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub button2_Click(sender As Object, e As EventArgs) Handles button2.Click
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub textBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles textBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If textBox1.Text = "admin2" And textBox2.Text = "admin2" Then
                MetroFramework.MetroMessageBox.Show(Me, "Logged into Engineering Mode", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Form9.Show()
                Me.Close()
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Wrong Username and/or Password combination", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub textBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles textBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If textBox1.Text = "admin2" And textBox2.Text = "admin2" Then
                MetroFramework.MetroMessageBox.Show(Me, "Logged into Engineering Mode", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Form9.Show()
                Me.Close()
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Wrong Username and/or Password combination", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(406, 205)
        textBox1.Select()
    End Sub
End Class