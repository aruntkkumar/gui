Public Class Form7
    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        If textBox1.Text = "admin3" And textBox2.Text = "admin3" Then
            If Form4.pword = 1 Then
                MetroFramework.MetroMessageBox.Show(Me, "Administrator mode deactivated", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
                Form4.pword = 0
            Else
                Form4.pword = 1
                MetroFramework.MetroMessageBox.Show(Me, "Administrator mode activated", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Else
            MetroFramework.MetroMessageBox.Show(Me, "Wrong Username and/or Password combination", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub button2_Click(sender As Object, e As EventArgs) Handles button2.Click
        Me.Close()
    End Sub

    Private Sub textBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles textBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If textBox1.Text = "admin3" And textBox2.Text = "admin3" Then
                If Form4.pword = 1 Then
                    MetroFramework.MetroMessageBox.Show(Me, "Administrator mode deactivated", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                    Form4.pword = 0
                Else
                    Form4.pword = 1
                    MetroFramework.MetroMessageBox.Show(Me, "Administrator mode activated", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Wrong Username and/or Password combination", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub textBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles textBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If textBox1.Text = "admin3" And textBox2.Text = "admin3" Then
                If Form4.pword = 1 Then
                    MetroFramework.MetroMessageBox.Show(Me, "Administrator mode deactivated", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                    Form4.pword = 0
                Else
                    Form4.pword = 1
                    MetroFramework.MetroMessageBox.Show(Me, "Administrator mode activated", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End If
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Wrong Username and/or Password combination", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        textBox1.Text = ""
        textBox2.Text = ""
    End Sub

    Private Sub Form7_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

End Class