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
        TextBox5.Text = GlobalVariables.xaxisint
        TextBox6.Text = GlobalVariables.yaxisint
        GlobalVariables.button = "cancel"
        If (GlobalVariables.ports = 0) Or (GlobalVariables.ports = vbNull) Then
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
        Else
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            test = 0
            For a As Integer = 1 To GlobalVariables.ports
                For b As Integer = 1 To GlobalVariables.ports
                    If GlobalVariables.matrix = "lower" Then
                        If a < b Then
                            Exit For
                        End If
                    ElseIf GlobalVariables.matrix = "upper" Then
                        While a > b
                            b += 1
                        End While
                    End If
                    If GlobalVariables.series(test) = 1 Then
                        CheckedListBox1.Items.Add("S(" & a & "," & b & ")", isChecked:=True)
                    Else
                        CheckedListBox1.Items.Add("S(" & a & "," & b & ")", isChecked:=False)
                    End If
                    test += 1
                Next
            Next
        End If
        'CheckedListBox1.Items.Add("S(1,1)", isChecked:=True)
        'CheckedListBox1.Items.Add("S(1,2)", isChecked:=False)
        'CheckedListBox1.Items.Add("S(2,1)", isChecked:=True)
        'CheckedListBox1.Items.Add("S(2,2)", isChecked:=False)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then     'Check if TextBox1 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As X-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox1.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As X-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < 0) Or (test > 100) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And +100 As X-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox2.Text = "" Then     'Check if TextBox2 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As X-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox2.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As X-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < 0) Or (test > 100) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And +100 As X-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox3.Text = "" Then     'Check if TextBox3 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox3.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < -1000) Or (test > 1000) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between -1000 And +1000 As Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox4.Text = "" Then     'Check if TextBox4 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox4.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < -1000) Or (test > 1000) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between -1000 And +1000 As Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox5.Text = "" Then     'Check if TextBox5 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As X-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox5.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As X-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test > (TextBox1.Text - TextBox2.Text)) Or (test <= 0) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And " & TextBox1.Text - TextBox2.Text & " As X-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox6.Text = "" Then     'Check if TextBox6 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As Y-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox6.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value As Y-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test > (TextBox3.Text - TextBox4.Text)) Or (test <= 0) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And " & TextBox3.Text - TextBox4.Text & " As Y-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        GlobalVariables.xaxisint = TextBox5.Text
        GlobalVariables.yaxisint = TextBox6.Text
        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemChecked(i) = True Then
                GlobalVariables.series(i) = 1
            Else
                GlobalVariables.series(i) = 0
            End If
        Next
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

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            test = 0
            For a As Integer = 1 To GlobalVariables.ports
                For b As Integer = 1 To GlobalVariables.ports
                    If GlobalVariables.matrix = "lower" Then
                        If a < b Then
                            Exit For
                        End If
                    End If
                    If GlobalVariables.matrix = "upper" Then
                        While a > b
                            b += 1
                        End While
                    End If
                    CheckedListBox1.SetItemCheckState(test, CheckState.Checked)
                    test += 1
                    'CheckedListBox1.Items.Add("S(" & a & "," & b & ")", isChecked:=True)
                Next
            Next
            CheckBox2.Checked = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            test = 0
            For a As Integer = 1 To GlobalVariables.ports
                For b As Integer = 1 To GlobalVariables.ports
                    If GlobalVariables.matrix = "lower" Then
                        If a < b Then
                            Exit For
                        End If
                    End If
                    If GlobalVariables.matrix = "upper" Then
                        While a > b
                            b += 1
                        End While
                    End If
                    CheckedListBox1.SetItemCheckState(test, CheckState.Unchecked)
                    test += 1
                    'CheckedListBox1.Items.Add("S(" & a & "," & b & ")", isChecked:=False)
                Next
            Next
            CheckBox1.Checked = False
        End If
    End Sub

    'Private Sub CheckedListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles CheckedListBox1.MouseClick
    '    CheckBox1.Checked = False
    '    CheckBox2.Checked = False
    'End Sub

    'Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
    '    CheckBox1.Checked = False
    '    CheckBox2.Checked = False
    'End Sub
End Class