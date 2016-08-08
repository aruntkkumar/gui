Public Class Form2

    Dim test As Double
    Dim checked As Double
    Dim unchecked As Double

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
        If GlobalVariables.y2axis = True Then
            TextBox7.Enabled = True
            TextBox8.Enabled = True
            TextBox9.Enabled = True
            TextBox7.Text = GlobalVariables.y2axismax
            TextBox8.Text = GlobalVariables.y2axismin
            TextBox9.Text = GlobalVariables.y2axisint
        Else
            TextBox7.Enabled = False
            TextBox8.Enabled = False
            TextBox9.Enabled = False
        End If
        GlobalVariables.okbutton = "cancel"
        If (GlobalVariables.ports = 0) Then
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
        Else
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            RemoveHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
            For i As Integer = 0 To GlobalVariables.seriesnames.Length - 2
                'If GlobalVariables.seriesnames(i) <> "" Then
                If GlobalVariables.series(i) = 1 Then
                    CheckedListBox1.Items.Add(GlobalVariables.seriesnames(i), isChecked:=True)
                Else
                    CheckedListBox1.Items.Add(GlobalVariables.seriesnames(i), isChecked:=False)
                End If
                'End If
            Next
            AddHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
        End If
        If GlobalVariables.autobutton = True Then
            CheckBox3.Checked = True
        Else
            CheckBox3.Checked = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then     'Check if TextBox1 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as X-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox1.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as X-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < 0) Or (test > 100) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And +100 as X-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox2.Text = "" Then     'Check if TextBox2 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as X-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox2.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as X-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < 0) Or (test > 100) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And +100 as X-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If (CDbl(TextBox1.Text) - CDbl(TextBox2.Text)) < 0.5 Then
            MetroFramework.MetroMessageBox.Show(Me, "The X-axis maximum value should be atleast 0.5 more than the X-axis minimum value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If TextBox3.Text = "" Then     'Check if TextBox3 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as primary Y-Axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox3.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as primary Y-Axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < -1000) Or (test > 1000) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between -1000 And +1000 as primary Y-Axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox4.Text = "" Then     'Check if TextBox4 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as primary Y-Axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox4.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as primary Y-Axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test < -1000) Or (test > 1000) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between -1000 And +1000 as primary Y-Axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If (CDbl(TextBox3.Text) - CDbl(TextBox4.Text)) < 5 Then
            MetroFramework.MetroMessageBox.Show(Me, "The primary Y-Axis maximum value should be atleast 5 more than the primary Y-Axis minimum value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        If TextBox5.Text = "" Then     'Check if TextBox5 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as X-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox5.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as X-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test > (TextBox1.Text - TextBox2.Text)) Or (test <= 0) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And " & TextBox1.Text - TextBox2.Text & " as X-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox6.Text = "" Then     'Check if TextBox6 is empty
            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as primary Y-Axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Try
                test = CDbl(TextBox6.Text)
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as primary Y-Axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End Try
            If (test > (TextBox3.Text - TextBox4.Text)) Or (test <= 0) Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And " & TextBox3.Text - TextBox4.Text & " as primary Y-Axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
        End If
        If TextBox7.Enabled = True Then
            'If GlobalVariables.plot = "efficiency" Then
            If TextBox7.Text = "" Then     'Check if TextBox7 is empty
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                Try
                    test = CDbl(TextBox7.Text)
                Catch ex As Exception
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End Try
                If GlobalVariables.plot = "efficiency" Then
                    If (test < 0) Or (test > 100) Then
                        MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And +100 As secondary Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            End If
            If TextBox8.Text = "" Then     'Check if TextBox8 is empty
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                Try
                    test = CDbl(TextBox8.Text)
                Catch ex As Exception
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End Try
                If GlobalVariables.plot = "efficiency" Then
                    If (test < 0) Or (test > 100) Then
                        MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And +100 as secondary Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            End If
            If GlobalVariables.plot = "efficiency" Then
                If (CDbl(TextBox7.Text) - CDbl(TextBox8.Text)) < 5 Then
                    MetroFramework.MetroMessageBox.Show(Me, "The secondary Y-Axis maximum value should be atleast 5 more than the secondary Y-Axis minimum value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If
            If TextBox9.Text = "" Then     'Check if TextBox9 is empty
                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                Try
                    test = CDbl(TextBox9.Text)
                Catch ex As Exception
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End Try
                If GlobalVariables.plot = "efficiency" Then
                    If (test > (TextBox7.Text - TextBox8.Text)) Or (test <= 0) Then
                        MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value between 0 And " & TextBox7.Text - TextBox8.Text & " as secondary Y-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            End If
            If GlobalVariables.plot = "generic" Then
                If CDbl(TextBox7.Text) < CDbl(TextBox8.Text) Then
                    MetroFramework.MetroMessageBox.Show(Me, "The secondary Y-Axis maximum value should be more than the secondary Y-Axis minimum value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If CDbl(TextBox9.Text) <= 0 Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If CDbl(TextBox9.Text) > Math.Abs(CDbl(TextBox7.Text) - CDbl(TextBox8.Text)) Then
                    MetroFramework.MetroMessageBox.Show(Me, "The secondary Y-Axis interval value is too small compared to its maximum and minimum values.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End If
            'Else
            '    If TextBox7.Text = "" Then     'Check if TextBox7 is empty
            '        MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Exit Sub
            '    Else
            '        Try
            '            test = CDbl(TextBox7.Text)
            '        Catch ex As Exception
            '            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis maximum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            Exit Sub
            '        End Try
            '    End If
            '    If TextBox8.Text = "" Then     'Check if TextBox8 is empty
            '        MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Exit Sub
            '    Else
            '        Try
            '            test = CDbl(TextBox8.Text)
            '        Catch ex As Exception
            '            MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid value as secondary Y-axis minimum.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            Exit Sub
            '        End Try
            '    End If
            GlobalVariables.y2axismax = TextBox7.Text
            GlobalVariables.y2axismin = TextBox8.Text
            GlobalVariables.y2axisint = TextBox9.Text
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
        GlobalVariables.okbutton = "ok"
        If CheckBox3.Checked = True Then
            GlobalVariables.autobutton = True
        Else
            GlobalVariables.autobutton = False
        End If
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

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            RemoveHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
            CheckedListBox1.SetItemCheckState(i, CheckState.Checked)
            AddHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
        Next
        RemoveHandler CheckBox1.CheckedChanged, AddressOf CheckBox1_CheckedChanged
        CheckBox1.Checked = True
        AddHandler CheckBox1.CheckedChanged, AddressOf CheckBox1_CheckedChanged

        RemoveHandler CheckBox2.CheckedChanged, AddressOf CheckBox2_CheckedChanged
        CheckBox2.Checked = False
        AddHandler CheckBox2.CheckedChanged, AddressOf CheckBox2_CheckedChanged
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            RemoveHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
            CheckedListBox1.SetItemCheckState(i, CheckState.Unchecked)
            AddHandler CheckedListBox1.ItemCheck, AddressOf CheckedListBox1_ItemCheck
        Next
        RemoveHandler CheckBox2.CheckedChanged, AddressOf CheckBox2_CheckedChanged
        CheckBox2.Checked = True
        AddHandler CheckBox2.CheckedChanged, AddressOf CheckBox2_CheckedChanged

        RemoveHandler CheckBox1.CheckedChanged, AddressOf CheckBox1_CheckedChanged
        CheckBox1.Checked = False
        AddHandler CheckBox1.CheckedChanged, AddressOf CheckBox1_CheckedChanged
    End Sub

    Private Sub CheckedListBox1_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles CheckedListBox1.ItemCheck
        checked = 0
        unchecked = 0
        For i As Integer = 0 To CheckedListBox1.Items.Count - 1
            If CheckedListBox1.GetItemChecked(test) Then
                checked += 1
            Else
                unchecked += 1
            End If
        Next
        If checked <> CheckedListBox1.Items.Count - 1 Then
            RemoveHandler CheckBox1.CheckedChanged, AddressOf CheckBox1_CheckedChanged
            CheckBox1.Checked = False
            AddHandler CheckBox1.CheckedChanged, AddressOf CheckBox1_CheckedChanged
        End If
        If unchecked <> CheckedListBox1.Items.Count - 1 Then
            RemoveHandler CheckBox2.CheckedChanged, AddressOf CheckBox2_CheckedChanged
            CheckBox2.Checked = False
            AddHandler CheckBox2.CheckedChanged, AddressOf CheckBox2_CheckedChanged
        End If
    End Sub

End Class