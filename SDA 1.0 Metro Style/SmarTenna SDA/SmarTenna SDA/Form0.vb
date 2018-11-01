Public Class Form0

    Dim i As Integer = 1

    Private Sub Form0_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Select()
    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        If TextBox1.Text = "!2L+/@Ti02" Then
            MetroFramework.MetroMessageBox.Show(Me, "Logging into SmarTenna ® SDA", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Form1.Show()
            Me.Close()
            Exit Sub
        Else
            MetroFramework.MetroMessageBox.Show(Me, "Wrong Password. Please try again", "Attempt # " & i, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            i = i + 1
        End If
        If i = 4 Then
            MetroFramework.MetroMessageBox.Show(Me, "Please contact Smart Antenna Technologies Ltd.", "Unauthorised access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
            Dim Info As ProcessStartInfo = New ProcessStartInfo()
            Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del """ & Application.ExecutablePath & """"
            Info.WindowStyle = ProcessWindowStyle.Hidden
            Info.CreateNoWindow = True
            Info.FileName = "cmd.exe"
            Process.Start(Info)
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "!2L+/@Ti02" Then
                MetroFramework.MetroMessageBox.Show(Me, "Logging into SmarTenna ® SDA", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Form1.Show()
                Me.Close()
                Exit Sub
            Else
                MetroFramework.MetroMessageBox.Show(Me, "Wrong Password. Please try again", "Attempt # " & i, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                i = i + 1
            End If
            If i = 4 Then
                MetroFramework.MetroMessageBox.Show(Me, "Please contact Smart Antenna Technologies Ltd.", "Unauthorised access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Close()
                Dim Info As ProcessStartInfo = New ProcessStartInfo()
                Info.Arguments = "/C choice /C Y /N /D Y /T 3 & Del """ & Application.ExecutablePath & """"
                Info.WindowStyle = ProcessWindowStyle.Hidden
                Info.CreateNoWindow = True
                Info.FileName = "cmd.exe"
                Process.Start(Info)
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox1.PasswordChar = ""
        Else
            TextBox1.PasswordChar = "*"
        End If
    End Sub
End Class