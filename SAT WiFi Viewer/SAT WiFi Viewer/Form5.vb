Imports System.IO

Public Class Form5

    Dim download As String
    Dim upload As String

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = "\\192.168.31.1\tddownload\TDTEMP\Download_Files\"
        TextBox2.Text = "\\192.168.31.1\tddownload\TDTEMP\Upload_Files\"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If download <> TextBox1.Text Or upload <> TextBox2.Text Then
            Try
                If (Not Directory.Exists(TextBox1.Text)) Or (Not Directory.Exists(TextBox2.Text)) Then
                    MetroFramework.MetroMessageBox.Show(Me, "Unable to access " & TextBox1.Text & " Or " & TextBox2.Text & ". Kindly verify if the network is available and try again.", "Network Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TextBox1.Text = download
                    TextBox2.Text = upload
                    Exit Sub
                End If
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox1.Text = download
                TextBox2.Text = upload
                Exit Sub
            End Try
        End If
        GlobalVariables.period = ComboBox1.Text
        GlobalVariables.size = ComboBox2.Text
        GlobalVariables.dfolder = TextBox1.Text
        GlobalVariables.ufolder = TextBox2.Text
        If CheckBox1.CheckState = CheckState.Checked Then
            GlobalVariables.detailed = True
        Else
            GlobalVariables.detailed = False
        End If
        GlobalVariables.okbutton = "ok"
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        GlobalVariables.okbutton = "cancel"
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = GlobalVariables.dfolder
        TextBox2.Text = GlobalVariables.ufolder
        download = GlobalVariables.dfolder
        upload = GlobalVariables.ufolder
        If GlobalVariables.detailed = True Then
            CheckBox1.CheckState = CheckState.Checked
        Else
            CheckBox1.CheckState = CheckState.Unchecked
        End If
        If GlobalVariables.period = "2.5 mins" Then
            ComboBox1.SelectedIndex = 2
        ElseIf GlobalVariables.period = "1 min" Then
            ComboBox1.SelectedIndex = 1
        ElseIf GlobalVariables.period = "5 mins" Then
            ComboBox1.SelectedIndex = 3
        Else
            ComboBox1.SelectedIndex = 0
        End If
        If GlobalVariables.size = "1 MB" Then
            ComboBox2.SelectedIndex = 0
        ElseIf GlobalVariables.size = "10 MB" Then
            ComboBox2.SelectedIndex = 1
        ElseIf GlobalVariables.size = "100 MB" Then
            ComboBox2.SelectedIndex = 2
        Else
            ComboBox2.SelectedIndex = 3
        End If
        'If GlobalVariables.debug = True Then
        '    CheckBox1.Visible = False
        'End If
    End Sub
End Class