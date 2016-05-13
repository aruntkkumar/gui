Imports System.Threading
Imports System.Windows.Forms

Public Class Form8

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Process.Start("http://www.smartantennatech.com/")
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Process.Start("https://twitter.com/SmartAntennaTec")
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        Process.Start("https://www.linkedin.com/company/smart-antenna-technologies")
    End Sub

    Private Sub Form8_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.IsDisposed() Then
            Try
                Me.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub Form8_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(770, 273)
    End Sub

    'Private Sub PictureBox4_Click(sender As Object, e As EventArgs)
    '    Process.Start("https://plus.google.com/103011616596456540984/posts")

    'End Sub

    'Private Sub PictureBox5_Click(sender As Object, e As EventArgs)
    '    Process.Start("https://www.facebook.com/Smart-Antenna-Tech-Ltd-1050346681650577/")
    'End Sub

End Class