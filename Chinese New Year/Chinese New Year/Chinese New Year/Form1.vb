Public Class Form1

    Dim rand As New Random
    Dim nextperson As Integer = 1
    Dim n() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    Dim i As Integer = 0
    Dim flag As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MetroLabel1.Text = "Please select the next contestant..."
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        If i = 16 Then
            MetroLabel1.Text = "All the contestants have been selected..."
            Exit Sub
        End If
        flag = False
        While (flag = False)
            nextperson = rand.Next(1, 17)
            If Not (n.Contains(nextperson)) Then
                n(i) = nextperson
                Select Case nextperson
                    Case 1
                        MetroLabel1.Text = "Next contestant: Colin Tucker"
                        i = i + 1
                    Case 2
                        MetroLabel1.Text = "Next contestant: Dr. Sampson Hu"
                        i = i + 1
                    Case 3
                        MetroLabel1.Text = "Next contestant: Paul Moon"
                        i = i + 1
                    'Case 4
                    '    MetroLabel1.Text = "Next contestant: Richard Cohen"
                    '    i = i + 1
                    Case 4
                        MetroLabel1.Text = "Next contestant: David Tanner"
                        i = i + 1
                    Case 5
                        MetroLabel1.Text = "Next contestant: David Beet"
                        i = i + 1
                    Case 6
                        MetroLabel1.Text = "Next contestant: Ian Ormerod"
                        i = i + 1
                    Case 7
                        MetroLabel1.Text = "Next contestant: Marzena Braminska"
                        i = i + 1
                    Case 8
                        MetroLabel1.Text = "Next contestant: Jiaping Lu"
                        i = i + 1
                    Case 9
                        MetroLabel1.Text = "Next contestant: Liang Wan"
                        i = i + 1
                    Case 10
                        MetroLabel1.Text = "Next contestant: Qing Liu"
                        i = i + 1
                    Case 11
                        MetroLabel1.Text = "Next contestant: Jiechen Chen"
                        i = i + 1
                    Case 12
                        MetroLabel1.Text = "Next contestant: Thanh Nga Mai"
                        i = i + 1
                    Case 13
                        MetroLabel1.Text = "Next contestant: Jinsong Song"
                        i = i + 1
                    Case 14
                        MetroLabel1.Text = "Next contestant: Arun Kumar"
                        i = i + 1
                    Case 15
                        MetroLabel1.Text = "Next contestant: Joanna Zhou"
                        i = i + 1
                    'Case 16
                    '    MetroLabel1.Text = "Next contestant: Edwin Candy"
                    '    i = i + 1
                    Case 16
                        MetroLabel1.Text = "Next contestant: Peter Hall"
                        i = i + 1
                        'Case 19
                        '    MetroLabel1.Text = "Next contestant: Peter Gardner"
                        '    i = i + 1
                    Case Else
                        MetroLabel1.Text = "Try again please..."
                End Select
                flag = True
            End If
        End While
    End Sub

End Class
