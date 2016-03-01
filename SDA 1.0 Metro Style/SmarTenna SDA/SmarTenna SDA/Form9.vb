Imports System.Threading
Imports System.IO.Ports
Imports Microsoft.Win32
Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class Form9

    Public Shared device As Integer = 0
    Public Shared pword As Integer = 0
    Dim Line As String
    Dim myPort As Array
    Dim vio As Integer
    Dim byte1 As Integer
    Dim byte2 As Integer
    Dim byte3 As Integer
    Dim Reg1 As RegistryKey
    Dim Reg2 As RegistryKey
    Dim portname As String
    Dim readValue As String
    Dim fullscreen As Boolean = False
    Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
    Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
    Dim myserialPort2 As New ExSerialPort
    Dim test As Integer
    Dim i As Integer
    Delegate Sub SetTextCallBack(ByVal [text] As String)

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Date.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        OptionsToolStripMenuItem.Enabled = True
        NewToolStripMenuItem.Enabled = True
        OpenToolStripMenuItem.Enabled = True
        SaveToolStripMenuItem.Enabled = True
        RichTextBox1.ReadOnly = True
        'SerialReset()
        Button2.Enabled = False
        Button3.Enabled = True
        Button4.Enabled = False
        Button5.Enabled = True
        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        'RadioButton1.Enabled = True
        'RadioButton2.Enabled = True
        RadioButton1.Enabled = False
        RadioButton2.Enabled = False
        RadioButton3.Enabled = False
        'ComboBox1.Items.Clear()
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox1.Items.AddRange(myPort)
        SCOUTSC4410ToolStripMenuItem.Checked = False
        SCOUTSC4410ToolStripMenuItem.Enabled = False
        PINGPIOToolStripMenuItem.Checked = False
        PINGPIOToolStripMenuItem.Enabled = False
        Label18.Visible = False
        RadioButton3.Visible = False

        'Dim pattern As String
        'pattern = String.Format("^VID_{0}.PID_{1}", "173C", "0002")
        'Dim _rx As New Regex(pattern, RegexOptions.IgnoreCase)
        'Dim comports As New List(Of String)()
        'Dim rk1 As RegistryKey = Registry.LocalMachine
        'Dim rk2 As RegistryKey = rk1.OpenSubKey("SYSTEM\CurrentControlSet\Enum")
        'For Each s3 As String In rk2.GetSubKeyNames()
        '    Dim rk3 As RegistryKey = rk2.OpenSubKey(s3)
        '    For Each s As String In rk3.GetSubKeyNames()
        '        If _rx.Match(s).Success Then
        '            Dim rk4 As RegistryKey = rk3.OpenSubKey(s)
        '            For Each s2 As String In rk4.GetSubKeyNames()
        '                Dim rk5 As RegistryKey = rk4.OpenSubKey(s2)
        '                Dim rk6 As RegistryKey = rk5.OpenSubKey("Device Parameters")
        '                comports.Add(DirectCast(rk6.GetValue("PortName"), String))
        '            Next
        '        End If
        '    Next
        'Next

        Dim x As New ComPortFinder
        Dim list As List(Of String)
        'Try
        list = x.ComPortNames("173C", "0002")
        For Each item As String In list
            For Each Str As String In myPort
                If Str.Contains(item) Then
                    SCOUTSC4410ToolStripMenuItem.Enabled = True
                End If
            Next
        Next
        'Catch ex As Exception
        'MessageBox.Show(ex.Message)
        'End Try
        list = x.ComPortNames("2A19", "0800")
        For Each item As String In list
            For Each Str As String In myPort
                If Str.Contains(item) Then
                    PINGPIOToolStripMenuItem.Enabled = True
                End If
            Next
        Next
        'list = x.ComPortNames("413C", "81B6", "03")
        'For Each item As String In list
        '    For Each Str As String In myPort
        '        If Str.Contains(item) Then
        '            SierraWirelessToolStripMenuItem.Enabled = True
        '        End If
        '    Next
        'Next

        'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1831ed82&0&0003\Device Parameters", "PortName", Nothing)
        'For Each Str As String In myPort
        '    If Str.Contains(readValue) Then
        '        SierraWirelessToolStripMenuItem.Enabled = Enabled
        '    End If
        'Next
        'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1e9f55e2&0&0003\Device Parameters", "PortName", Nothing)
        'For Each Str As String In myPort
        '    If Str.Contains(readValue) Then
        '        SierraWirelessToolStripMenuItem.Enabled = Enabled
        '    End If
        'Next

        'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\5&1c8c57d1&0&1\Device Parameters", "PortName", Nothing)
        'For Each Str As String In myPort
        '    If Str.Contains(readValue) Then
        '        SCOUTSC4410ToolStripMenuItem.Enabled = True
        '    End If
        'Next
        'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\5&1c8c57d1&0&2\Device Parameters", "PortName", Nothing)
        'For Each Str As String In myPort
        '    If Str.Contains(readValue) Then
        '        SCOUTSC4410ToolStripMenuItem.Enabled = True
        '    End If
        'Next
        'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\6&2a9e2b2e&0&3\Device Parameters", "PortName", Nothing)
        'For Each Str As String In myPort
        '    If Str.Contains(readValue) Then
        '        SCOUTSC4410ToolStripMenuItem.Enabled = True
        '    End If
        'Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If ComboBox1.Text = "" Then
                MetroFramework.MetroMessageBox.Show(Me, "Please select a Comm Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If ComboBox2.Text = "" Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please select a Baud Rate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    'SerialPort1.PortName = ComboBox1.Text
                    'SerialPort1.BaudRate = ComboBox2.Text
                    'SerialPort1.Parity = Parity.None
                    'SerialPort1.DataBits = 8
                    'SerialPort1.StopBits = StopBits.One
                    'SerialPort1.Open()
                    myserialPort2.PortName = ComboBox1.Text
                    myserialPort2.BaudRate = ComboBox2.Text
                    myserialPort2.Parity = Parity.None
                    myserialPort2.DataBits = 8
                    myserialPort2.StopBits = StopBits.One
                    myserialPort2.Open()
                    'Button2.Enabled = True
                    Button3.Enabled = False
                    Button4.Enabled = True
                    Button5.Enabled = True
                    ComboBox1.Enabled = False
                    ComboBox2.Enabled = False
                    ComboBox3.Enabled = True
                    ComboBox4.Enabled = True
                    'RadioButton1.Enabled = True
                    'RadioButton2.Enabled = True
                    RichTextBox1.Text &= "Port Name: " & ComboBox1.Text & "; Baud Rate: " & ComboBox2.Text & vbCrLf & vbCrLf
                    Dim x As New ComPortFinder
                    Dim list As List(Of String)
                    list = x.ComPortNames("173C", "0002")
                    For Each item As String In list
                        If (item = ComboBox1.Text) Then
                            RadioButton1.Checked = False
                            RadioButton2.Checked = True
                            device = 1
                            SCOUTSC4410ToolStripMenuItem.Checked = True
                            'SierraWirelessToolStripMenuItem.Checked = False
                            PINGPIOToolStripMenuItem.Checked = False
                        End If
                    Next
                    list = x.ComPortNames("2A19", "0800")
                    For Each item As String In list
                        If (item = ComboBox1.Text) Then
                            RadioButton1.Checked = True
                            RadioButton2.Checked = False
                            Button5.Enabled = False
                            ComboBox3.Enabled = False
                            ComboBox4.Enabled = False
                            Button2.Enabled = True
                            device = 3
                            SCOUTSC4410ToolStripMenuItem.Checked = False
                            'SierraWirelessToolStripMenuItem.Checked = False
                            PINGPIOToolStripMenuItem.Checked = True
                            myserialPort2.Write("gpio iodir 00" & vbCr)
                            RichTextBox1.Text &= myserialPort2.ReadExisting()
                            RichTextBox1.Text &= myserialPort2.ReadExisting()
                            'RichTextBox1.Text &= myserialPort2.ReadExisting()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'MsgBox(ex.Message, MsgBoxStyle.Information, "Error")
            SerialReset()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Do
            If ComboBox5.Text = "" Then
                MetroFramework.MetroMessageBox.Show(Me, "Please select the status for RF Switch 1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If ComboBox6.Text = "" Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please select the status for RF Switch 2", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If ComboBox7.Text = "" Then
                        MetroFramework.MetroMessageBox.Show(Me, "Please select the status for RF Switch 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        If ComboBox8.Text = "" Then
                            MetroFramework.MetroMessageBox.Show(Me, "Please select the status for RF Switch 4", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            If ComboBox9.Text = "" Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please select the state of the DAC Output", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                If ListBox1.Items.Count = 0 Then
                                    MetroFramework.MetroMessageBox.Show(Me, "Please enter the SSC states using integers between 0 to 64, starting from SSC1. (The SSC states ranges from State0 to State64)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Else
                                    byte1 = &HC0
                                    byte2 = &H40
                                    byte3 = &H80
                                    If ComboBox5.Text = "Isolation" Then
                                        byte1 = byte1 Or &H08
                                    Else
                                        If ComboBox5.Text = "ANT-RF1" Then
                                            byte1 = byte1 Or &H00
                                        Else
                                            If ComboBox5.Text = "ANT-RF2" Then
                                                byte1 = byte1 Or &H20
                                            Else
                                                If ComboBox5.Text = "ANT-RF3" Then
                                                    byte1 = byte1 Or &H10
                                                Else
                                                    If ComboBox5.Text = "ANT-RF4" Then
                                                        byte1 = byte1 Or &H30
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                    If ComboBox6.Text = "Isolation" Then
                                        byte1 = byte1 Or &H01
                                    Else
                                        If ComboBox6.Text = "ANT-RF1" Then
                                            byte1 = byte1 Or &H00
                                        Else
                                            If ComboBox6.Text = "ANT-RF2" Then
                                                byte1 = byte1 Or &H04
                                            Else
                                                If ComboBox6.Text = "ANT-RF3" Then
                                                    byte1 = byte1 Or &H02
                                                Else
                                                    If ComboBox6.Text = "ANT-RF4" Then
                                                        byte1 = byte1 Or &H06
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                    If ComboBox7.Text = "Isolation" Then
                                        byte2 = byte2 Or &H08
                                    Else
                                        If ComboBox7.Text = "ANT-RF1" Then
                                            byte2 = byte2 Or &H00
                                        Else
                                            If ComboBox7.Text = "ANT-RF2" Then
                                                byte2 = byte2 Or &H20
                                            Else
                                                If ComboBox7.Text = "ANT-RF3" Then
                                                    byte2 = byte2 Or &H10
                                                Else
                                                    If ComboBox7.Text = "ANT-RF4" Then
                                                        byte2 = byte2 Or &H30
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                    If ComboBox8.Text = "Isolation" Then
                                        byte2 = byte2 Or &H01
                                    Else
                                        If ComboBox8.Text = "ANT-RF1" Then
                                            byte2 = byte2 Or &H00
                                        Else
                                            If ComboBox8.Text = "ANT-RF2" Then
                                                byte2 = byte2 Or &H04
                                            Else
                                                If ComboBox8.Text = "ANT-RF3" Then
                                                    byte2 = byte2 Or &H02
                                                Else
                                                    If ComboBox8.Text = "ANT-RF4" Then
                                                        byte2 = byte2 Or &H06
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                    If ComboBox9.Text = "OFF" Then
                                        byte3 = byte3 Or &H08
                                    Else
                                        If ComboBox9.Text = "0.3V" Then
                                            byte3 = byte3 Or &H00
                                        Else
                                            If ComboBox9.Text = "0.5V" Then
                                                byte3 = byte3 Or &H20
                                            Else
                                                If ComboBox9.Text = "0.6V" Then
                                                    byte3 = byte3 Or &H10
                                                Else
                                                    If ComboBox9.Text = "0.7V" Then
                                                        byte3 = byte3 Or &H30
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                    If Toggle1.Checked = True Then
                                        byte3 = byte3 Or &H02
                                    End If
                                    If Toggle2.Checked = True Then
                                        byte3 = byte3 Or &H01
                                    End If
                                    Try
                                        If device = 3 Then
                                            myserialPort2.Write("gpio writeall " & byte1.ToString("X") & vbCr)
                                            RichTextBox1.Text &= myserialPort2.ReadExisting()
                                            RichTextBox1.Text &= myserialPort2.ReadExisting()
                                            Thread.Sleep(25)
                                            myserialPort2.Write("gpio writeall " & byte2.ToString("X") & vbCr)
                                            RichTextBox1.Text &= myserialPort2.ReadExisting()
                                            RichTextBox1.Text &= myserialPort2.ReadExisting()
                                            Thread.Sleep(25)
                                            myserialPort2.Write("gpio writeall " & byte3.ToString("X") & vbCr)
                                            RichTextBox1.Text &= myserialPort2.ReadExisting()
                                            RichTextBox1.Text &= myserialPort2.ReadExisting()
                                            RichTextBox1.Text &= myserialPort2.ReadExisting()
                                            Thread.Sleep(25)
                                            For i = 0 To ListBox1.Items.Count - 1
                                                If i Mod 2 = 0 Then
                                                    test = &H40
                                                Else
                                                    test = &H80
                                                End If
                                                test = test Or ListBox1.Items.Item(i)
                                                myserialPort2.Write("gpio writeall " & test.ToString("X") & vbCr)
                                                RichTextBox1.Text &= myserialPort2.ReadExisting()
                                                RichTextBox1.Text &= myserialPort2.ReadExisting()
                                                Thread.Sleep(25)
                                            Next
                                            RichTextBox1.Text &= myserialPort2.ReadExisting()
                                        Else
                                            myserialPort2.WriteLine("rw 1 0x05 0x" & byte1.ToString("X") & vbCrLf & "rw 1 0x05 0x" & byte2.ToString("X") & vbCrLf & "rw 1 0x05 0x" & byte3.ToString("X") & vbCrLf)
                                            RichTextBox1.Text &= myserialPort2.ReadLine()
                                            RichTextBox1.Text &= myserialPort2.ReadLine()
                                            RichTextBox1.Text &= myserialPort2.ReadLine()
                                            RichTextBox1.Text &= myserialPort2.ReadLine()
                                            RichTextBox1.Text &= myserialPort2.ReadLine()
                                            RichTextBox1.Text &= myserialPort2.ReadLine()
                                            RichTextBox1.Text &= myserialPort2.ReadLine()
                                            For i = 0 To ListBox1.Items.Count - 1
                                                If i Mod 2 = 0 Then
                                                    test = &H40
                                                Else
                                                    test = &H80
                                                End If
                                                test = test Or ListBox1.Items.Item(i)
                                                myserialPort2.Write("rw 1 0x05 0x" & test.ToString("X") & vbCrLf)
                                                RichTextBox1.Text &= myserialPort2.ReadExisting()
                                                RichTextBox1.Text &= myserialPort2.ReadExisting()
                                                RichTextBox1.Text &= myserialPort2.ReadExisting()
                                            Next
                                        End If
                                    Catch ex As Exception
                                        MetroFramework.MetroMessageBox.Show(Me, ComboBox1.Text & " does not exist. Please open a valid COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        'MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
                                        SerialReset()
                                        Exit Sub
                                    End Try
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Loop Until Toggle3.Checked = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SerialReset()
        RichTextBox1.Text &= "The active device has been disconnected" & vbCrLf & vbCrLf
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If device = 3 Then
            Button5.Enabled = False
            ComboBox3.Enabled = False
            ComboBox4.Enabled = False
            Button2.Enabled = True
        Else
            Button5.Enabled = True
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            Button2.Enabled = False
        End If
        'RadioButton1.Enabled = True
        'RadioButton2.Enabled = True
        ComboBox3.SelectedIndex = -1
        ComboBox4.SelectedIndex = -1
        ComboBox5.SelectedIndex = -1
        ComboBox6.SelectedIndex = -1
        ComboBox7.SelectedIndex = -1
        ComboBox8.SelectedIndex = -1
        ComboBox9.SelectedIndex = -1
        TextBox1.Text = ""
        ListBox1.Items.Clear()
        Toggle1.Checked = False
        Toggle2.Checked = False
    End Sub

    Private Sub myserialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        ReceivedText(myserialPort2.ReadExisting())
        'ReceivedText(SerialPort1.ReadExisting())
    End Sub

    Private Sub ReceivedText(ByVal [text] As String)
        If Me.RichTextBox1.InvokeRequired Then
            Dim x As New SetTextCallBack(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else
            Me.RichTextBox1.Text &= [text]
        End If
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        RichTextBox1.SelectionStart = RichTextBox1.Text.Length
        RichTextBox1.ScrollToCaret()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Button3.Enabled = True Then
            MetroFramework.MetroMessageBox.Show(Me, "Please open a Comm Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If RadioButton2.Checked = False And RadioButton1.Checked = False Then
                MetroFramework.MetroMessageBox.Show(Me, "The selected Comm Port is not suited for Engineering Mode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                'If RadioButton1.Checked = True Then
                '    MetroFramework.MetroMessageBox.Show(Me, "GPIO Mode is currently under development", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                If ComboBox3.Text = "" Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please select the LM8335 Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If ComboBox4.Text = "" Then
                        MetroFramework.MetroMessageBox.Show(Me, "Please select VIO voltage", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        If ComboBox4.Text = "OFF/External" Then
                            vio = 0
                        Else
                            If ComboBox4.Text = "1.2" Then
                                vio = 1
                            Else
                                vio = 2
                            End If
                        End If
                        Try
                            myserialPort2.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
                            RichTextBox1.Text &= myserialPort2.ReadLine()
                            RichTextBox1.Text &= myserialPort2.ReadLine()
                            RichTextBox1.Text &= myserialPort2.ReadLine()
                            RichTextBox1.Text &= myserialPort2.ReadLine()
                            RichTextBox1.Text &= myserialPort2.ReadLine()
                            'RichTextBox1.Text &= myserialPort.ReadExisting()
                            'RichTextBox1.Text &= myserialPort.ReadExisting()
                            'SerialPort1.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
                            'RichTextBox1.Text &= SerialPort1.ReadExisting()
                        Catch ex As Exception
                            MetroFramework.MetroMessageBox.Show(Me, ComboBox1.Text & " does not exist. Please open a valid COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            'MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
                            SerialReset()
                            Exit Sub
                        End Try
                        Button5.Enabled = False
                        'RadioButton1.Enabled = False
                        'RadioButton2.Enabled = False
                        ComboBox3.Enabled = False
                        ComboBox4.Enabled = False
                        Button2.Enabled = True
                    End If
                End If
                'End If
            End If
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        RichTextBox1.Clear()
    End Sub

    Private Sub FullScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullScreenToolStripMenuItem.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
            FullScreenToolStripMenuItem.Checked = True
            fullscreen = True
        Else
            Me.WindowState = FormWindowState.Normal
            FullScreenToolStripMenuItem.Checked = False
            fullscreen = False
        End If
        SizeAdjust()
    End Sub

    Private Sub Form9_SizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.SizeChanged
        If Me.WindowState = FormWindowState.Normal Then
            FullScreenToolStripMenuItem.Checked = False
            fullscreen = False
        Else
            FullScreenToolStripMenuItem.Checked = True
            fullscreen = True
        End If
        SizeAdjust()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        'If SerialPort1.IsOpen() Then
        If myserialPort2.IsOpen() Then
            Try
                'SerialPort1.Close()
                myserialPort2.Close()
            Catch ex As Exception
            End Try
            Application.DoEvents()  ' Give port time to close down
            Thread.Sleep(200)
        End If
        Me.Close()
    End Sub

    'Private Sub MyDevicesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MyDevicesToolStripMenuItem.Click
    'Form6.ShowDialog()
    'If device = 1 Then
    'RichTextBox1.Text &= "Active device selected as SCOUT SC4410" & vbCrLf
    'Else
    'If device = 2 Then
    'RichTextBox1.Text &= "Active device selected as SIERRA WIRELESS" & vbCrLf
    'End If
    'End If
    'Dim readValue As String = ""
    'readValue = Reg2.GetValue("PortName")
    'If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1e9f55e2&0&0003\Device Parameters", "PortName", Nothing) Is Nothing Then
    'MsgBox("Value does not exist.")
    'Else
    'If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1831ed82&0&0003\Device Parameters", "PortName", Nothing) Is Nothing Then
    'MsgBox("Value does not exist.")
    'Else
    'End If
    'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1e9f55e2&0&0003\Device Parameters", "PortName", Nothing)
    'MsgBox("The value is " & readValue)
    'End If
    'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1e9f55e2&0&0003\Device Parameters", "PortName", Nothing)
    'MsgBox("The value is " & readValue)
    'End Sub

    Private Sub SCOUTSC4410ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SCOUTSC4410ToolStripMenuItem.Click
        If SCOUTSC4410ToolStripMenuItem.Checked = True Then
            SerialReset()
            RichTextBox1.Text &= "SCOUT SC4410 has been disconnected" & vbCrLf & vbCrLf
        Else
            'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\5&1c8c57d1&0&1\Device Parameters", "PortName", Nothing)
            'For Each Str As String In myPort
            '    If Str.Contains(readValue) Then
            '        ScoutON(readValue)
            '    End If
            'Next
            'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\5&1c8c57d1&0&2\Device Parameters", "PortName", Nothing)
            'For Each Str As String In myPort
            '    If Str.Contains(readValue) Then
            '        ScoutON(readValue)
            '    End If
            'Next
            'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\6&2a9e2b2e&0&3\Device Parameters", "PortName", Nothing)
            'For Each Str As String In myPort
            '    If Str.Contains(readValue) Then
            '        ScoutON(readValue)
            '    End If
            'Next
            Dim x As New ComPortFinder
            Try
                Dim list = x.ComPortNames("173C", "0002")
                For Each item As String In list
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            ScoutON(item)
                        End If
                    Next
                Next
            Catch ex As Exception
                SerialReset()
                RichTextBox1.Text &= "SCOUT SC4410 has been disconnected" & vbCrLf & vbCrLf
            End Try
        End If
    End Sub

    Function ScoutON(ByVal readvalue1 As String)
        Try
            myserialPort2.Close()
            'SerialPort1.Close()
        Catch ex As Exception
        End Try
        Application.DoEvents()  ' Give port time to close down
        Thread.Sleep(200)
        SCOUTSC4410ToolStripMenuItem.Checked = True
        'SierraWirelessToolStripMenuItem.Checked = False
        PINGPIOToolStripMenuItem.Checked = False
        'SerialPort1.PortName = readvalue1
        'SerialPort1.BaudRate = 115200
        'SerialPort1.Parity = Parity.None
        'SerialPort1.DataBits = 8
        'SerialPort1.StopBits = StopBits.One
        'SerialPort1.Open()
        myserialPort2.PortName = readvalue1
        myserialPort2.BaudRate = 115200
        myserialPort2.Parity = Parity.None
        myserialPort2.DataBits = 8
        myserialPort2.StopBits = StopBits.One
        myserialPort2.Open()
        Button3.Enabled = False
        Button4.Enabled = True
        Button5.Enabled = True
        ComboBox1.Enabled = False
        ComboBox2.Enabled = False
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        RadioButton1.Checked = False
        RadioButton2.Checked = True
        'RadioButton3.Checked = False
        device = 1
        RichTextBox1.Text &= "Active device selected as SCOUT SC4410" & vbCrLf & "Port Name: " & readvalue1 & "; Baud Rate: 115200" & vbCrLf & vbCrLf
        Return 0
    End Function

    Private Sub PINGPIOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PINGPIOToolStripMenuItem.Click
        If PINGPIOToolStripMenuItem.Checked = True Then
            SerialReset()
            RichTextBox1.Text &= "GPIO has been disconnected" & vbCrLf & vbCrLf
        Else
            Dim x As New ComPortFinder
            Try
                Dim list = x.ComPortNames("2A19", "0800")
                For Each item As String In list
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            GPIOON(item)
                        End If
                    Next
                Next
            Catch ex As Exception
                SerialReset()
                RichTextBox1.Text &= "GPIO has been disconnected" & vbCrLf & vbCrLf
            End Try
        End If
    End Sub

    Function GPIOON(ByVal readvalue1 As String)
        Try
            myserialPort2.Close()
            'SerialPort1.Close()
        Catch ex As Exception
        End Try
        Application.DoEvents()  ' Give port time to close down
        Thread.Sleep(200)
        SCOUTSC4410ToolStripMenuItem.Checked = False
        'SierraWirelessToolStripMenuItem.Checked = False
        PINGPIOToolStripMenuItem.Checked = True
        'SerialPort1.PortName = readvalue1
        'SerialPort1.BaudRate = 115200
        'SerialPort1.Parity = Parity.None
        'SerialPort1.DataBits = 8
        'SerialPort1.StopBits = StopBits.One
        'SerialPort1.Open()
        myserialPort2.PortName = readvalue1
        myserialPort2.BaudRate = 9600
        myserialPort2.Parity = Parity.None
        myserialPort2.DataBits = 8
        myserialPort2.StopBits = StopBits.One
        myserialPort2.Open()
        Button2.Enabled = True
        Button3.Enabled = False
        Button4.Enabled = True
        Button5.Enabled = False
        ComboBox1.Enabled = False
        ComboBox2.Enabled = False
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.Enabled = False
        ComboBox4.Enabled = False
        RadioButton1.Checked = True
        RadioButton2.Checked = False
        'RadioButton3.Checked = False
        device = 3
        RichTextBox1.Text &= "Active device selected as GPIO" & vbCrLf & "Port Name: " & readvalue1 & "; Baud Rate: 9600" & vbCrLf & vbCrLf
        myserialPort2.Write("gpio iodir 00" & vbCr)
        RichTextBox1.Text &= myserialPort2.ReadExisting()
        RichTextBox1.Text &= myserialPort2.ReadExisting()
        Return 0
    End Function

    Private Sub Form9_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'If SerialPort1.IsOpen() Then
        If myserialPort2.IsOpen() Then
            Try
                'SerialPort1.Close()
                myserialPort2.Close()
            Catch ex As Exception
            End Try
            Application.DoEvents()  ' Give port time to close down
            Thread.Sleep(200)
        End If
    End Sub

    Function SizeAdjust()
        If Me.WindowState = FormWindowState.Maximized Then
            Label15.Location = New Point(x:=(intX / 8), y:=(intY * 1.5 / 13))
            Label4.Location = New Point(x:=(intX / 8), y:=(intY * 2.1577 / 13))
            Label11.Location = New Point(x:=(intX / 8), y:=(intY * 2.8154 / 13))
            Label10.Location = New Point(x:=(intX / 8), y:=(intY * 3.4731 / 13))
            Label9.Location = New Point(x:=(intX / 8), y:=(intY * 4.1308 / 13))
            Label16.Location = New Point(x:=(intX / 8), y:=(intY * 4.7885 / 13))
            Label5.Location = New Point(x:=(intX / 8), y:=(intY * 5.4462 / 13))
            Label6.Location = New Point(x:=(intX / 8), y:=(intY * 6.1038 / 13))
            Label7.Location = New Point(x:=(intX / 8), y:=(intY * 6.7615 / 13))
            Label19.Location = New Point(x:=(intX / 8), y:=(intY * 7.4192 / 13))
            Label20.Location = New Point(x:=(intX / 8), y:=(intY * 8.7346 / 13))
            Label21.Location = New Point(x:=(intX / 8), y:=(intY * 9.3923 / 13))
            Label8.Location = New Point(x:=(intX / 8), y:=(intY * 10.05 / 13))
            Button1.Location = New Point(x:=(intX / 8), y:=(intY * 11 / 13))
            Button2.Location = New Point(x:=(intX / 3.5), y:=(intY * 11 / 13))
            ComboBox1.Location = New Point(x:=(intX / 3.5), y:=(intY * 1.5 / 13))
            ComboBox2.Location = New Point(x:=(intX / 3.5), y:=(intY * 2.1577 / 13))
            Label12.Location = New Point(x:=((intX / 3.5)), y:=(intY * 2.8154 / 13))
            Label13.Location = New Point(x:=((intX / 3.5) + 103), y:=(intY * 2.8154 / 13))
            Label18.Location = New Point(x:=((intX / 3.5) + 197), y:=(intY * 2.8154 / 13))
            RadioButton1.Location = New Point(x:=((intX / 3.5) + 53), y:=((intY * 2.8154 / 13) + 3))
            RadioButton2.Location = New Point(x:=((intX / 3.5) + 147), y:=((intY * 2.8154 / 13) + 3))
            RadioButton3.Location = New Point(x:=((intX / 3.5) + 250), y:=((intY * 2.8154 / 13) + 3))
            ComboBox3.Location = New Point(x:=(intX / 3.5), y:=(intY * 3.4731 / 13))
            ComboBox4.Location = New Point(x:=(intX / 3.5), y:=(intY * 4.1308 / 13))
            ComboBox5.Location = New Point(x:=(intX / 3.5), y:=(intY * 4.7885 / 13))
            ComboBox6.Location = New Point(x:=(intX / 3.5), y:=(intY * 5.4462 / 13))
            ComboBox7.Location = New Point(x:=(intX / 3.5), y:=(intY * 6.1038 / 13))
            ComboBox8.Location = New Point(x:=(intX / 3.5), y:=(intY * 6.7615 / 13))
            TextBox1.Location = New Point(x:=(intX / 3.5), y:=(intY * 7.4192 / 13))
            Button7.Location = New Point(x:=((intX / 3.5) + 61), y:=(intY * 7.4192 / 13))
            Button8.Location = New Point(x:=((intX / 3.5) + 61), y:=(intY * 8.0769 / 13))
            ListBox1.Location = New Point(x:=((intX / 3.5) + 146), y:=(intY * 7.4192 / 13))
            ListBox1.Size = New Size(width:=119, height:=((intY * (8.0769 - 7.4192) / 13) + 30))
            Toggle1.Location = New Point(x:=(intX / 3.5), y:=(intY * 8.7346 / 13))
            Toggle2.Location = New Point(x:=(intX / 3.5), y:=(intY * 9.3923 / 13))
            ComboBox9.Location = New Point(x:=(intX / 3.5), y:=(intY * 10.05 / 13))
            Label22.Location = New Point(x:=((intX / 3.5) + 130), y:=((intY * 11 / 13) + 9))
            Toggle3.Location = New Point(x:=((intX / 3.5) + 185), y:=((intY * 11 / 13) + 11))
            Button3.Location = New Point(x:=(intX / 1.9), y:=(intY * 1.5 / 13))
            Button4.Location = New Point(x:=(intX / 1.29), y:=(intY * 1.5 / 13))
            Button5.Location = New Point(x:=(intX / 1.9), y:=(intY * 2.8154 / 13))
            Label14.Location = New Point(x:=(intX / 1.9), y:=(intY * 4.35 / 13))
            RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=(intY * 4.85 / 13))
            RichTextBox1.Size = New Size(width:=((intX / 1.29) - (intX / 1.9) + 119), height:=((intY * 10.05 / 13) - (intY * 4.85 / 13) + 25))
            Button6.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 13))
        Else
            Label15.Location = New Point(x:=95, y:=102)
            Label4.Location = New Point(x:=95, y:=139)
            Label11.Location = New Point(x:=95, y:=176)
            Label10.Location = New Point(x:=95, y:=213)
            Label9.Location = New Point(x:=95, y:=250)
            Label16.Location = New Point(x:=95, y:=287)
            Label5.Location = New Point(x:=95, y:=324)
            Label6.Location = New Point(x:=95, y:=361)
            Label7.Location = New Point(x:=95, y:=398)
            Label19.Location = New Point(x:=95, y:=435)
            Label20.Location = New Point(x:=95, y:=509)
            Label21.Location = New Point(x:=95, y:=546)
            Label8.Location = New Point(x:=95, y:=583)
            Button1.Location = New Point(x:=95, y:=632)
            Button2.Location = New Point(x:=273, y:=632)
            ComboBox1.Location = New Point(x:=273, y:=102)
            ComboBox2.Location = New Point(x:=273, y:=139)
            Label12.Location = New Point(x:=273, y:=176)
            Label13.Location = New Point(x:=376, y:=176)
            Label18.Location = New Point(x:=470, y:=176)
            RadioButton1.Location = New Point(x:=326, y:=179)
            RadioButton2.Location = New Point(x:=420, y:=179)
            RadioButton3.Location = New Point(x:=523, y:=179)
            ComboBox3.Location = New Point(x:=273, y:=213)
            ComboBox4.Location = New Point(x:=273, y:=250)
            ComboBox5.Location = New Point(x:=273, y:=287)
            ComboBox6.Location = New Point(x:=273, y:=324)
            ComboBox7.Location = New Point(x:=273, y:=361)
            ComboBox8.Location = New Point(x:=273, y:=398)
            TextBox1.Location = New Point(x:=273, y:=435)
            Button7.Location = New Point(x:=334, y:=435)
            Button8.Location = New Point(x:=334, y:=466)
            ListBox1.Location = New Point(x:=419, y:=435)
            ListBox1.Size = New Size(width:=119, height:=56)
            Toggle1.Location = New Point(x:=273, y:=511)
            Toggle2.Location = New Point(x:=273, y:=548)
            ComboBox9.Location = New Point(x:=273, y:=583)
            Label22.Location = New Point(x:=403, y:=641)
            Toggle3.Location = New Point(x:=458, y:=643)
            Button3.Location = New Point(x:=591, y:=102)
            Button4.Location = New Point(x:=745, y:=102)
            Button5.Location = New Point(x:=591, y:=176)
            Label14.Location = New Point(x:=591, y:=261)
            RichTextBox1.Location = New Point(x:=591, y:=296)
            RichTextBox1.Size = New Size(width:=273, height:=312)
            Button6.Location = New Point(x:=591, y:=632)
        End If
        Return 0
    End Function

    Function SerialReset()
        Try
            myserialPort2.Close()
            'SerialPort1.Close()
        Catch ex As Exception
        End Try
        Application.DoEvents()  ' Give port time to close down
        Thread.Sleep(200)
        Button2.Enabled = False
        Button3.Enabled = True
        Button4.Enabled = False
        Button5.Enabled = True
        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        'RadioButton1.Enabled = True
        'RadioButton2.Enabled = True
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        ComboBox1.Items.Clear()
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox1.Items.AddRange(myPort)
        SCOUTSC4410ToolStripMenuItem.Checked = False
        'SierraWirelessToolStripMenuItem.Checked = False
        PINGPIOToolStripMenuItem.Checked = False
        SCOUTSC4410ToolStripMenuItem.Enabled = False
        'SierraWirelessToolStripMenuItem.Enabled = False
        PINGPIOToolStripMenuItem.Enabled = False
        device = 0
        Dim x As New ComPortFinder
        Dim list As List(Of String)
        'Try
        list = x.ComPortNames("173C", "0002")
        For Each item As String In list
            For Each Str As String In myPort
                If Str.Contains(item) Then
                    SCOUTSC4410ToolStripMenuItem.Enabled = True
                End If
            Next
        Next
        'Catch ex As Exception
        'End Try
        list = x.ComPortNames("2A19", "0800")
        For Each item As String In list
            For Each Str As String In myPort
                If Str.Contains(item) Then
                    PINGPIOToolStripMenuItem.Enabled = True
                End If
            Next
        Next
        '    list = x.ComPortNames("413C", "81B6", "03")
        '    For Each item As String In list
        '        For Each Str As String In myPort
        '            If Str.Contains(item) Then
        '                SierraWirelessToolStripMenuItem.Enabled = True
        '            End If
        '        Next
        '    Next
        Return 0
    End Function

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Form8.ShowDialog()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Process.Start("http://www.smartantennatech.com/")
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            test = CInt(TextBox1.Text)
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-64 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End Try
        test = CInt(TextBox1.Text)
        If (test >= 0) Then
            If (test > 64) Then
                MetroFramework.MetroMessageBox.Show(Me, "Invalid state. Please enter an integer between 0-64 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If ListBox1.Text = "" Then
                    ListBox1.Items.Add(TextBox1.Text)
                    ListBox1.TopIndex = ListBox1.Items.Count - 1
                Else
                    test = ListBox1.SelectedIndex
                    ListBox1.Items.Remove(ListBox1.SelectedItem.ToString)
                    ListBox1.Items.Insert(test, TextBox1.Text)
                    ListBox1.TopIndex = ListBox1.Items.Count - 1
                End If

                TextBox1.Text = ""
                TextBox1.SelectionStart = 0
                TextBox1.SelectionLength = 0
                TextBox1.Focus()
            End If
        Else
            MetroFramework.MetroMessageBox.Show(Me, "Invalid state. Please enter an integer between 0-64 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If ListBox1.Text = "" Then
            If (ListBox1.Items.Count - 1) = -1 Then
            Else
                ListBox1.Items.RemoveAt(ListBox1.Items.Count - 1)
                ListBox1.TopIndex = ListBox1.Items.Count - 1
            End If
        Else
            ListBox1.Items.Remove(ListBox1.SelectedItem.ToString)
        End If
        TextBox1.SelectionStart = 0
        TextBox1.SelectionLength = 0
        TextBox1.Focus()
    End Sub

End Class