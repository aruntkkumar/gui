Public Class Form6
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim myPort As Array
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox1.Enabled = False
        CheckBox2.Enabled = False
        CheckBox3.Enabled = False
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        TextBox3.ReadOnly = True
        Dim readValue As String = ""
        myPort = IO.Ports.SerialPort.GetPortNames()
        If Form4.device = 1 Then
            CheckBox1.Checked = True
            CheckBox1.Enabled = True
        Else
            If Form4.device = 2 Then
                CheckBox2.Checked = True
                CheckBox2.Enabled = True
            End If
        End If
        'Reg1 = My.Computer.Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Enum\\USB\\VID_413C&PID_81B6&MI_03", False)
        'Reg2 = Reg1.OpenSubKey("PortName", False)
        'readValue = Reg2.GetValue("PortName")
        readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1831ed82&0&0003\Device Parameters", "PortName", Nothing)
        For Each Str As String In myPort
            If Str.Contains(readValue) Then
                TextBox2.Text = "Port Name: " & readValue & "; Baud Rate: 115200"
                'Form4.device = 2
                CheckBox2.Enabled = True
            End If
        Next
        readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1e9f55e2&0&0003\Device Parameters", "PortName", Nothing)
        For Each Str As String In myPort
            If Str.Contains(readValue) Then
                TextBox2.Text = "Port Name: " & readValue & "; Baud Rate: 115200"
                'Form4.device = 2
                CheckBox2.Enabled = True
            End If
        Next

        readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\5&1c8c57d1&0&1\Device Parameters", "PortName", Nothing)
        For Each Str As String In myPort
            If Str.Contains(readValue) Then
                TextBox1.Text = "Port Name: " & readValue & "; Baud Rate: 115200"
                'Form4.device = 1
                CheckBox1.Enabled = True
            End If
        Next
        readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\5&1c8c57d1&0&2\Device Parameters", "PortName", Nothing)
        For Each Str As String In myPort
            If Str.Contains(readValue) Then
                TextBox1.Text = "Port Name: " & readValue & "; Baud Rate: 115200"
                'Form4.device = 1
                CheckBox1.Enabled = True
            End If
        Next
        readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\6&2a9e2b2e&0&3\Device Parameters", "PortName", Nothing)
        For Each Str As String In myPort
            If Str.Contains(readValue) Then
                TextBox1.Text = "Port Name: " & readValue & "; Baud Rate: 115200"
                'Form4.device = 1
                CheckBox1.Enabled = True
            End If
        Next

        'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1e9f55e2&0&0003\Device Parameters", "PortName", Nothing)
        'MsgBox("The value is " & readValue)
    End Sub

    Private Sub CheckBox1_Click(sender As Object, e As EventArgs) Handles CheckBox1.Click
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            Form4.device = 1
        Else
            If (CheckBox1.Checked = False) And (CheckBox2.Checked = False) And (CheckBox3.Checked = False) Then
                Form4.device = 0
            End If
        End If
    End Sub

    Private Sub CheckBox2_CheckStateChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckStateChanged
        'Private Sub CheckBox2_Click(sender As Object, e As EventArgs) Handles CheckBox2.Click
        If CheckBox2.Checked = True Then
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            Form4.device = 2
        Else
            If (CheckBox1.Checked = False) And (CheckBox2.Checked = False) And CheckBox3.Checked = False Then
                Form4.device = 0
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            Form4.device = 1
        Else
            If (CheckBox1.Checked = False) And (CheckBox2.Checked = False) And (CheckBox3.Checked = False) Then
                Form4.device = 0
            End If
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            Form4.device = 2
        Else
            If (CheckBox1.Checked = False) And (CheckBox2.Checked = False) And CheckBox3.Checked = False Then
                Form4.device = 0
            End If
        End If
    End Sub
End Class