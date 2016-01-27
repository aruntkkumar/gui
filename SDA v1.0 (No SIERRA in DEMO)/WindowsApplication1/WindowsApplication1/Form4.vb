Imports System.Threading
Imports System.IO.Ports
Imports Microsoft.Win32
Imports System.Text.RegularExpressions

Public Class Form4

    Public Shared device As Integer = 0
    Public Shared pword As Integer = 0
    Dim Line As String
    Dim myPort As Array
    Dim vio As Integer
    Dim byte1 As Integer
    Dim bandsel As Integer
    Dim cabandsel As Integer
    Dim Reg1 As RegistryKey
    Dim Reg2 As RegistryKey
    Dim portname As String
    Dim readValue As String
    Dim fullscreen As Boolean = False
    Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
    Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
    Dim myserialPort As New ExSerialPort
    Delegate Sub SetTextCallBack(ByVal [text] As String)

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Date.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        OptionsToolStripMenuItem.Enabled = False
        NewToolStripMenuItem.Enabled = False
        OpenToolStripMenuItem.Enabled = False
        SaveToolStripMenuItem.Enabled = False
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
        'ComboBox1.Items.Clear()
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox1.Items.AddRange(myPort)
        SCOUTSC4410ToolStripMenuItem.Checked = False
        SCOUTSC4410ToolStripMenuItem.Enabled = False
        SierraWirelessToolStripMenuItem.Checked = False
        SierraWirelessToolStripMenuItem.Enabled = False
        SierraWirelessToolStripMenuItem.Visible = False
        PINGPIOToolStripMenuItem.Checked = False
        PINGPIOToolStripMenuItem.Enabled = False

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
                MsgBox("Please select a Comm Port", MsgBoxStyle.Information, "Error")
            Else
                If ComboBox2.Text = "" Then
                    MsgBox("Please select a Baud Rate", MsgBoxStyle.Information, "Error")
                Else
                    'SerialPort1.PortName = ComboBox1.Text
                    'SerialPort1.BaudRate = ComboBox2.Text
                    'SerialPort1.Parity = Parity.None
                    'SerialPort1.DataBits = 8
                    'SerialPort1.StopBits = StopBits.One
                    'SerialPort1.Open()
                    myserialPort.PortName = ComboBox1.Text
                    myserialPort.BaudRate = ComboBox2.Text
                    myserialPort.Parity = Parity.None
                    myserialPort.DataBits = 8
                    myserialPort.StopBits = StopBits.One
                    myserialPort.Open()
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
                            myserialPort.Write("gpio iodir 00" & vbCr)
                            RichTextBox1.Text &= myserialPort.ReadExisting()
                            RichTextBox1.Text &= myserialPort.ReadExisting()
                            'RichTextBox1.Text &= myserialPort.ReadExisting()
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Error")
            SerialReset()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox5.Text = "" Then
            MsgBox("Please select the Antenna", MsgBoxStyle.Information, "Error")
        Else
            If ComboBox6.Text = "" Then
                MsgBox("Please select the TRX", MsgBoxStyle.Information, "Error")
            Else
                If ComboBox7.Text = "" Then
                    MsgBox("Please select a supported E-UTRA band", MsgBoxStyle.Information, "Error")
                Else
                    If ComboBox8.Text = "" Then
                        MsgBox("Please select a supported Carrier Aggregation band", MsgBoxStyle.Information, "Error")
                    Else
                        If ComboBox9.Text = "" Then
                            MsgBox("Please select the TX power level", MsgBoxStyle.Information, "Error")
                        Else
                            If ComboBox7.SelectedIndex = 0 Then
                                bandsel = &H41
                            Else
                                If ComboBox7.SelectedIndex = 1 Then
                                    bandsel = &H42
                                Else
                                    If ComboBox7.SelectedIndex = 2 Then
                                        bandsel = &H43
                                    Else
                                        If ComboBox7.SelectedIndex = 3 Then
                                            bandsel = &H44
                                        Else
                                            If ComboBox7.SelectedIndex = 4 Then
                                                bandsel = &H45
                                            Else
                                                If ComboBox7.SelectedIndex = 5 Then
                                                    bandsel = &H47
                                                Else
                                                    If ComboBox7.SelectedIndex = 6 Then
                                                        bandsel = &H48
                                                    Else
                                                        If ComboBox7.SelectedIndex = 7 Then
                                                            bandsel = &H4C
                                                        Else
                                                            If ComboBox7.SelectedIndex = 8 Then
                                                                bandsel = &H4D
                                                            Else
                                                                If ComboBox7.SelectedIndex = 9 Then
                                                                    bandsel = &H54
                                                                Else
                                                                    If ComboBox7.SelectedIndex = 10 Then
                                                                        bandsel = &H59
                                                                    Else
                                                                        If ComboBox7.SelectedIndex = 11 Then
                                                                            bandsel = &H5A
                                                                        Else
                                                                            If ComboBox7.SelectedIndex = 12 Then
                                                                                bandsel = &H5D
                                                                            Else
                                                                                If ComboBox7.SelectedIndex = 13 Then
                                                                                    bandsel = &H5E
                                                                                Else
                                                                                    bandsel = &H69
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            If ComboBox8.SelectedIndex = 0 Then
                                cabandsel = &H80
                            Else
                                If ComboBox8.SelectedIndex = 1 Then
                                    cabandsel = &H81
                                Else
                                    If ComboBox8.SelectedIndex = 2 Then
                                        cabandsel = &H82
                                    Else
                                        If ComboBox8.SelectedIndex = 3 Then
                                            cabandsel = &H83
                                        Else
                                            If ComboBox8.SelectedIndex = 4 Then
                                                cabandsel = &H84
                                            Else
                                                If ComboBox8.SelectedIndex = 5 Then
                                                    cabandsel = &H85
                                                Else
                                                    If ComboBox8.SelectedIndex = 6 Then
                                                        cabandsel = &H87
                                                    Else
                                                        If ComboBox8.SelectedIndex = 7 Then
                                                            cabandsel = &H88
                                                        Else
                                                            If ComboBox8.SelectedIndex = 8 Then
                                                                cabandsel = &H8C
                                                            Else
                                                                If ComboBox8.SelectedIndex = 9 Then
                                                                    cabandsel = &H8D
                                                                Else
                                                                    If ComboBox8.SelectedIndex = 10 Then
                                                                        cabandsel = &H94
                                                                    Else
                                                                        If ComboBox8.SelectedIndex = 11 Then
                                                                            cabandsel = &H99
                                                                        Else
                                                                            If ComboBox8.SelectedIndex = 12 Then
                                                                                cabandsel = &H9A
                                                                            Else
                                                                                If ComboBox8.SelectedIndex = 13 Then
                                                                                    cabandsel = &H9D
                                                                                Else
                                                                                    If ComboBox8.SelectedIndex = 14 Then
                                                                                        cabandsel = &H9E
                                                                                    Else
                                                                                        cabandsel = &HA9
                                                                                    End If
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    End If
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                            'bandsel = CInt(ComboBox7.Text)
                            'bandsel = bandsel Or &H40
                            'cabandsel = CInt(ComboBox8.Text)
                            'cabandsel = cabandsel Or &H80
                            byte1 = &HC0
                            If ComboBox5.Text = "Both Antennas" Then
                                byte1 = byte1 Or &H30
                            Else
                                If ComboBox5.Text = "Aux Antenna" Then
                                    byte1 = byte1 Or &H20
                                Else
                                    If ComboBox5.Text = "Main Antenna" Then
                                        byte1 = byte1 Or &H10
                                    End If
                                End If
                            End If
                            If ComboBox6.Text = "TX and RX ON" Then
                                byte1 = byte1 Or &H0C
                            Else
                                If ComboBox6.Text = "RX ON" Then
                                    byte1 = byte1 Or &H08
                                Else
                                    If ComboBox6.Text = "TX ON" Then
                                        byte1 = byte1 Or &H04
                                    End If
                                End If
                            End If
                            If ComboBox9.Text = "High power (20 to 30 dBm)" Then
                                byte1 = byte1 Or &H03
                            Else
                                If ComboBox9.Text = "Medium to high power (15 to 20 dBm)" Then
                                    byte1 = byte1 Or &H02
                                Else
                                    If ComboBox9.Text = "Low to medium power (5 to 15 dBm)" Then
                                        byte1 = byte1 Or &H01
                                    End If
                                End If
                            End If
                            'SerialPort1.WriteLine("rw 1 0x05 0xD7" & vbCrLf & "rw 1 0x05 0x58" & vbCrLf & "rw 1 0x05 0x97" & vbCrLf)
                            Try
                                If device = 3 Then
                                    myserialPort.Write("gpio writeall " & byte1.ToString("X") & vbCr)
                                    RichTextBox1.Text &= myserialPort.ReadExisting()
                                    RichTextBox1.Text &= myserialPort.ReadExisting()
                                    Thread.Sleep(25)
                                    myserialPort.Write("gpio writeall " & bandsel.ToString("X") & vbCr)
                                    RichTextBox1.Text &= myserialPort.ReadExisting()
                                    RichTextBox1.Text &= myserialPort.ReadExisting()
                                    Thread.Sleep(25)
                                    myserialPort.Write("gpio writeall " & cabandsel.ToString("X") & vbCr)
                                    RichTextBox1.Text &= myserialPort.ReadExisting()
                                    RichTextBox1.Text &= myserialPort.ReadExisting()
                                Else
                                    myserialPort.WriteLine("rw 1 0x05 0x" & byte1.ToString("X") & vbCrLf & "rw 1 0x05 0x" & bandsel.ToString("X") & vbCrLf & "rw 1 0x05 0x" & cabandsel.ToString("X") & vbCrLf)
                                    RichTextBox1.Text &= myserialPort.ReadExisting()
                                    RichTextBox1.Text &= myserialPort.ReadExisting()
                                    'SerialPort1.WriteLine("rw 1 0x05 0x" & byte1.ToString("X") & vbCrLf & "rw 1 0x05 0x" & bandsel.ToString("X") & vbCrLf & "rw 1 0x05 0x" & cabandsel.ToString("X") & vbCrLf)
                                    'RichTextBox1.Text &= SerialPort1.ReadExisting()
                                End If
                            Catch ex As Exception
                                MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
                                SerialReset()
                                Exit Sub
                            End Try
                        End If
                    End If
                End If
            End If
        End If
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
    End Sub

    Private Sub myserialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        ReceivedText(myserialPort.ReadExisting())
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
            MsgBox("Please open a Comm Port", MsgBoxStyle.Information, "Error")
        Else
            If RadioButton2.Checked = False And RadioButton1.Checked = False Then
                MsgBox("The selected Comm Port is not suited for DEMO Mode", MsgBoxStyle.Information, "Error")
            Else
                If RadioButton1.Checked = True Then
                    MsgBox("GPIO Mode is currently under development", MsgBoxStyle.Information, "Error")
                Else
                    If ComboBox3.Text = "" Then
                        MsgBox("Please select the LM8335 Address", MsgBoxStyle.Information, "Error")
                    Else
                        If ComboBox4.Text = "" Then
                            MsgBox("Please select VIO voltage", MsgBoxStyle.Information, "Error")
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
                                myserialPort.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
                                RichTextBox1.Text &= myserialPort.ReadExisting()
                                RichTextBox1.Text &= myserialPort.ReadExisting()
                                'SerialPort1.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
                                'RichTextBox1.Text &= SerialPort1.ReadExisting()
                            Catch ex As Exception
                                MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
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
                End If
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

    Private Sub Form4_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        If (fullscreen) Then
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
        If myserialPort.IsOpen() Then
            Try
                'SerialPort1.Close()
                myserialPort.Close()
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
            myserialPort.Close()
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
        myserialPort.PortName = readvalue1
        myserialPort.BaudRate = 115200
        myserialPort.Parity = Parity.None
        myserialPort.DataBits = 8
        myserialPort.StopBits = StopBits.One
        myserialPort.Open()
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
        device = 1
        RichTextBox1.Text &= "Active device selected as SCOUT SC4410" & vbCrLf & "Port Name: " & readvalue1 & "; Baud Rate: 115200" & vbCrLf & vbCrLf
        Return 0
    End Function

    'Private Sub SierraWirelessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SierraWirelessToolStripMenuItem.Click
    '    If SierraWirelessToolStripMenuItem.Checked = True Then
    '        SerialReset()
    '        RichTextBox1.Text &= "SIERRA WIRELESS has been disconnected" & vbCrLf & vbCrLf
    '    Else
    '        'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1831ed82&0&0003\Device Parameters", "PortName", Nothing)
    '        'For Each Str As String In myPort
    '        '    If Str.Contains(readValue) Then
    '        '        SierraON(readValue)
    '        '    End If
    '        'Next
    '        'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_413C&PID_81B6&MI_03\6&1e9f55e2&0&0003\Device Parameters", "PortName", Nothing)
    '        'For Each Str As String In myPort
    '        '    If Str.Contains(readValue) Then
    '        '        SierraON(readValue)
    '        '    End If
    '        'Next
    '        Dim x As New ComPortFinder
    '        Try
    '            Dim List = x.ComPortNames("413C", "81B6", "03")
    '            For Each item As String In List
    '                For Each Str As String In myPort
    '                    If Str.Contains(item) Then
    '                        SierraON(item)
    '                    End If
    '                Next
    '            Next
    '        Catch ex As Exception
    '            SerialReset()
    '            RichTextBox1.Text &= "SIERRA WIRELESS has been disconnected" & vbCrLf & vbCrLf
    '        End Try
    '    End If
    'End Sub

    'Function SierraON(ByVal readvalue1 As String)
    '    Try
    '        myserialPort.Close()
    '        'SerialPort1.Close()
    '    Catch ex As Exception
    '    End Try
    '    Application.DoEvents()  ' Give port time to close down
    '    Thread.Sleep(200)
    '    SCOUTSC4410ToolStripMenuItem.Checked = False
    '    SierraWirelessToolStripMenuItem.Checked = True
    '    PINGPIOToolStripMenuItem.Checked = False
    '    SerialPort1.PortName = readvalue1
    '    SerialPort1.BaudRate = 115200
    '    SerialPort1.Parity = Parity.None
    '    SerialPort1.DataBits = 8
    '    SerialPort1.StopBits = StopBits.One
    '    SerialPort1.Open()
    '    'myserialPort.PortName = readvalue1
    '    'myserialPort.BaudRate = 115200
    '    'myserialPort.Parity = Parity.None
    '    'myserialPort.DataBits = 8
    '    'myserialPort.StopBits = StopBits.One
    '    'myserialPort.Open()
    '    Button3.Enabled = False
    '    Button4.Enabled = True
    '    Button5.Enabled = True
    '    ComboBox1.Enabled = False
    '    ComboBox2.Enabled = False
    '    ComboBox1.SelectedIndex = -1
    '    ComboBox2.SelectedIndex = -1
    '    ComboBox3.Enabled = True
    '    ComboBox4.Enabled = True
    '    device = 2
    '    RichTextBox1.Text &= "Active device selected as SIERRA WIRELESS" & vbCrLf & "Port Name: " & readvalue1 & "; Baud Rate: 115200" & vbCrLf & vbCrLf
    '    Return 0
    'End Function

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
            myserialPort.Close()
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
        myserialPort.PortName = readvalue1
        myserialPort.BaudRate = 9600
        myserialPort.Parity = Parity.None
        myserialPort.DataBits = 8
        myserialPort.StopBits = StopBits.One
        myserialPort.Open()
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
        device = 3
        RichTextBox1.Text &= "Active device selected as GPIO" & vbCrLf & "Port Name: " & readvalue1 & "; Baud Rate: 9600" & vbCrLf & vbCrLf
        myserialPort.Write("gpio iodir 00" & vbCr)
        RichTextBox1.Text &= myserialPort.ReadExisting()
        RichTextBox1.Text &= myserialPort.ReadExisting()
        Return 0
    End Function

    Private Sub CommPortSelectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CommPortSelectToolStripMenuItem.Click
        If CommPortSelectToolStripMenuItem.Checked = False Then
            CommPortSelectToolStripMenuItem.Checked = True
            Label15.Visible = False
            Label4.Visible = False
            ComboBox1.Visible = False
            ComboBox2.Visible = False
            Button3.Visible = False
            Button4.Visible = False
        Else
            CommPortSelectToolStripMenuItem.Checked = False
            Label15.Visible = True
            Label4.Visible = True
            ComboBox1.Visible = True
            ComboBox2.Visible = True
            Button3.Visible = True
            Button4.Visible = True
        End If
        SizeAdjust()
    End Sub

    Private Sub VIOSelectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VIOSelectToolStripMenuItem.Click
        If VIOSelectToolStripMenuItem.Checked = False Then
            VIOSelectToolStripMenuItem.Checked = True
            Label9.Visible = False
            Label10.Visible = False
            Label11.Visible = False
            Label12.Visible = False
            Label13.Visible = False
            RadioButton1.Visible = False
            RadioButton2.Visible = False
            Button1.Visible = False
            Button5.Visible = False
            ComboBox3.Visible = False
            ComboBox4.Visible = False
        Else
            VIOSelectToolStripMenuItem.Checked = False
            Label9.Visible = True
            Label10.Visible = True
            Label11.Visible = True
            Label12.Visible = True
            Label13.Visible = True
            RadioButton1.Visible = True
            RadioButton2.Visible = True
            Button1.Visible = True
            Button5.Visible = True
            ComboBox3.Visible = True
            ComboBox4.Visible = True
        End If
        SizeAdjust()
    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'If SerialPort1.IsOpen() Then
        If myserialPort.IsOpen() Then
            Try
                'SerialPort1.Close()
                myserialPort.Close()
            Catch ex As Exception
            End Try
            Application.DoEvents()  ' Give port time to close down
            Thread.Sleep(200)
        End If
    End Sub

    Private Sub EnableAdminModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnableAdminModeToolStripMenuItem.Click
        Form7.ShowDialog()
        If pword = 1 Then
            OptionsToolStripMenuItem.Enabled = True
            NewToolStripMenuItem.Enabled = True
            OpenToolStripMenuItem.Enabled = True
            SaveToolStripMenuItem.Enabled = True
            EnableAdminModeToolStripMenuItem.Text = "Disable Admin mode"
        Else
            OptionsToolStripMenuItem.Enabled = False
            NewToolStripMenuItem.Enabled = False
            OpenToolStripMenuItem.Enabled = False
            SaveToolStripMenuItem.Enabled = False
            EnableAdminModeToolStripMenuItem.Text = "Enable Admin mode"
        End If
    End Sub

    Function SizeAdjust()
        If Me.WindowState = FormWindowState.Maximized Then
            If CommPortSelectToolStripMenuItem.Checked = True Then
                If VIOSelectToolStripMenuItem.Checked = True Then
                    Label16.Location = New Point(x:=(intX / 8), y:=(intY * 1 / 14))
                    Label5.Location = New Point(x:=(intX / 8), y:=(intY * 3 / 14))
                    Label6.Location = New Point(x:=(intX / 8), y:=(intY * 5 / 14))
                    Label7.Location = New Point(x:=(intX / 8), y:=(intY * 7 / 14))
                    Label8.Location = New Point(x:=(intX / 8), y:=(intY * 9 / 14))
                    Button2.Location = New Point(x:=(intX / 4), y:=(intY * 11 / 14))
                    ComboBox5.Location = New Point(x:=(intX / 4), y:=(intY * 1 / 14))
                    ComboBox6.Location = New Point(x:=(intX / 4), y:=(intY * 3 / 14))
                    ComboBox7.Location = New Point(x:=(intX / 4), y:=(intY * 5 / 14))
                    ComboBox8.Location = New Point(x:=(intX / 4), y:=(intY * 7 / 14))
                    ComboBox9.Location = New Point(x:=(intX / 4), y:=(intY * 9 / 14))
                    Label14.Location = New Point(x:=(intX / 1.9), y:=(intY * 1 / 14))
                    RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=(intY * 2 / 14))
                    RichTextBox1.Size = New Size(width:=(intX / 3), height:=(intY / 1.95))
                    Button6.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 14))
                Else
                    Label11.Location = New Point(x:=(intX / 8), y:=(intY * 1 / 14))
                    Label10.Location = New Point(x:=(intX / 8), y:=(intY * 2.25 / 14))
                    Label9.Location = New Point(x:=(intX / 8), y:=(intY * 3.5 / 14))
                    Label16.Location = New Point(x:=(intX / 8), y:=(intY * 4.75 / 14))
                    Label5.Location = New Point(x:=(intX / 8), y:=(intY * 6 / 14))
                    Label6.Location = New Point(x:=(intX / 8), y:=(intY * 7.25 / 14))
                    Label7.Location = New Point(x:=(intX / 8), y:=(intY * 8.5 / 14))
                    Label8.Location = New Point(x:=(intX / 8), y:=(intY * 9.75 / 14))
                    Button1.Location = New Point(x:=(intX / 8), y:=(intY * 11 / 14))
                    Button2.Location = New Point(x:=(intX / 4), y:=(intY * 11 / 14))
                    Label12.Location = New Point(x:=(intX / 3.85), y:=(intY * 1 / 14))
                    Label13.Location = New Point(x:=(intX / 3), y:=(intY * 1 / 14))
                    RadioButton1.Location = New Point(x:=(intX / 3.5), y:=(intY * 1 / 14))
                    RadioButton2.Location = New Point(x:=(intX / 2.8), y:=(intY * 1 / 14))
                    ComboBox3.Location = New Point(x:=(intX / 4), y:=(intY * 2.25 / 14))
                    ComboBox4.Location = New Point(x:=(intX / 4), y:=(intY * 3.5 / 14))
                    ComboBox5.Location = New Point(x:=(intX / 4), y:=(intY * 4.75 / 14))
                    ComboBox6.Location = New Point(x:=(intX / 4), y:=(intY * 6 / 14))
                    ComboBox7.Location = New Point(x:=(intX / 4), y:=(intY * 7.25 / 14))
                    ComboBox8.Location = New Point(x:=(intX / 4), y:=(intY * 8.5 / 14))
                    ComboBox9.Location = New Point(x:=(intX / 4), y:=(intY * 9.75 / 14))
                    Button5.Location = New Point(x:=(intX / 1.9), y:=(intY * 1 / 14))
                    Label14.Location = New Point(x:=(intX / 1.9), y:=(intY * 4 / 14))
                    RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=(intY * 5 / 14))
                    RichTextBox1.Size = New Size(width:=(intX / 3), height:=(intY / 2.9))
                    Button6.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 14))
                End If
            Else
                If VIOSelectToolStripMenuItem.Checked = True Then
                    Label15.Location = New Point(x:=(intX / 8), y:=(intY / 14))
                    Label4.Location = New Point(x:=(intX / 8), y:=(intY * 2.43 / 14))
                    Label16.Location = New Point(x:=(intX / 8), y:=(intY * 3.85 / 14))
                    Label5.Location = New Point(x:=(intX / 8), y:=(intY * 5.28 / 14))
                    Label6.Location = New Point(x:=(intX / 8), y:=(intY * 6.71 / 14))
                    Label7.Location = New Point(x:=(intX / 8), y:=(intY * 8.14 / 14))
                    Label8.Location = New Point(x:=(intX / 8), y:=(intY * 9.57 / 14))
                    Button2.Location = New Point(x:=(intX / 4), y:=(intY * 11 / 14))
                    ComboBox1.Location = New Point(x:=(intX / 4), y:=(intY * 1 / 14))
                    ComboBox2.Location = New Point(x:=(intX / 4), y:=(intY * 2.43 / 14))
                    ComboBox5.Location = New Point(x:=(intX / 4), y:=(intY * 3.85 / 14))
                    ComboBox6.Location = New Point(x:=(intX / 4), y:=(intY * 5.28 / 14))
                    ComboBox7.Location = New Point(x:=(intX / 4), y:=(intY * 6.71 / 14))
                    ComboBox8.Location = New Point(x:=(intX / 4), y:=(intY * 8.14 / 14))
                    ComboBox9.Location = New Point(x:=(intX / 4), y:=(intY * 9.57 / 14))
                    Button3.Location = New Point(x:=(intX / 1.9), y:=(intY * 1 / 14))
                    Button4.Location = New Point(x:=(intX / 1.29), y:=(intY * 1 / 14))
                    Label14.Location = New Point(x:=(intX / 1.9), y:=(intY * 3.3 / 14))
                    RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=(intY * 4 / 14))
                    RichTextBox1.Size = New Size(width:=(intX / 3), height:=(intY / 2.4))
                    Button6.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 14))
                Else
                    Label15.Location = New Point(x:=(intX / 8), y:=(intY / 14))
                    Label4.Location = New Point(x:=(intX / 8), y:=(intY * 2 / 14))
                    Label11.Location = New Point(x:=(intX / 8), y:=(intY * 3 / 14))
                    Label10.Location = New Point(x:=(intX / 8), y:=(intY * 4 / 14))
                    Label9.Location = New Point(x:=(intX / 8), y:=(intY * 5 / 14))
                    Label16.Location = New Point(x:=(intX / 8), y:=(intY * 6 / 14))
                    Label5.Location = New Point(x:=(intX / 8), y:=(intY * 7 / 14))
                    Label6.Location = New Point(x:=(intX / 8), y:=(intY * 8 / 14))
                    Label7.Location = New Point(x:=(intX / 8), y:=(intY * 9 / 14))
                    Label8.Location = New Point(x:=(intX / 8), y:=(intY * 10 / 14))
                    Button1.Location = New Point(x:=(intX / 8), y:=(intY * 11 / 14))
                    Button2.Location = New Point(x:=(intX / 4), y:=(intY * 11 / 14))
                    ComboBox1.Location = New Point(x:=(intX / 4), y:=(intY * 1 / 14))
                    ComboBox2.Location = New Point(x:=(intX / 4), y:=(intY * 2 / 14))
                    Label12.Location = New Point(x:=(intX / 3.85), y:=(intY * 3 / 14))
                    Label13.Location = New Point(x:=(intX / 3), y:=(intY * 3 / 14))
                    RadioButton1.Location = New Point(x:=(intX / 3.5), y:=(intY * 3 / 14))
                    RadioButton2.Location = New Point(x:=(intX / 2.8), y:=(intY * 3 / 14))
                    ComboBox3.Location = New Point(x:=(intX / 4), y:=(intY * 4 / 14))
                    ComboBox4.Location = New Point(x:=(intX / 4), y:=(intY * 5 / 14))
                    ComboBox5.Location = New Point(x:=(intX / 4), y:=(intY * 6 / 14))
                    ComboBox6.Location = New Point(x:=(intX / 4), y:=(intY * 7 / 14))
                    ComboBox7.Location = New Point(x:=(intX / 4), y:=(intY * 8 / 14))
                    ComboBox8.Location = New Point(x:=(intX / 4), y:=(intY * 9 / 14))
                    ComboBox9.Location = New Point(x:=(intX / 4), y:=(intY * 10 / 14))
                    Button3.Location = New Point(x:=(intX / 1.9), y:=(intY * 1 / 14))
                    Button4.Location = New Point(x:=(intX / 1.29), y:=(intY * 1 / 14))
                    Button5.Location = New Point(x:=(intX / 1.9), y:=(intY * 4 / 14))
                    Label14.Location = New Point(x:=(intX / 1.9), y:=(intY * 5.4 / 14))
                    RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=(intY * 6 / 14))
                    RichTextBox1.Size = New Size(width:=(intX / 3), height:=(intY / 3.3))
                    Button6.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 14))
                End If
            End If
        Else
            If CommPortSelectToolStripMenuItem.Checked = True Then
                If VIOSelectToolStripMenuItem.Checked = True Then
                    Label16.Location = New Point(x:=91, y:=63)
                    Label5.Location = New Point(x:=91, y:=166.5)
                    Label6.Location = New Point(x:=91, y:=270)
                    Label7.Location = New Point(x:=91, y:=373.5)
                    Label8.Location = New Point(x:=91, y:=477)
                    Button2.Location = New Point(x:=259, y:=547)
                    ComboBox5.Location = New Point(x:=238, y:=58)
                    ComboBox6.Location = New Point(x:=238, y:=162.5)
                    ComboBox7.Location = New Point(x:=238, y:=267)
                    ComboBox8.Location = New Point(x:=238, y:=370.5)
                    ComboBox9.Location = New Point(x:=238, y:=476)
                    Label14.Location = New Point(x:=535, y:=63)
                    RichTextBox1.Location = New Point(x:=537, y:=117)
                    RichTextBox1.Size = New Size(width:=287, height:=380)
                    Button6.Location = New Point(x:=538, y:=547)
                Else
                    Label11.Location = New Point(x:=91, y:=63)
                    Label10.Location = New Point(x:=91, y:=123.5)
                    Label9.Location = New Point(x:=91, y:=184)
                    Label16.Location = New Point(x:=91, y:=244.5)
                    Label5.Location = New Point(x:=91, y:=305)
                    Label6.Location = New Point(x:=91, y:=365.5)
                    Label7.Location = New Point(x:=91, y:=426)
                    Label8.Location = New Point(x:=91, y:=486.5)
                    Button1.Location = New Point(x:=89, y:=547)
                    Button2.Location = New Point(x:=259, y:=547)
                    Label12.Location = New Point(x:=244, y:=63)
                    Label13.Location = New Point(x:=308, y:=63)
                    RadioButton1.Location = New Point(x:=288, y:=63)
                    RadioButton2.Location = New Point(x:=348, y:=63)
                    ComboBox3.Location = New Point(x:=238, y:=117.5)
                    ComboBox4.Location = New Point(x:=238, y:=179)
                    ComboBox5.Location = New Point(x:=238, y:=240.5)
                    ComboBox6.Location = New Point(x:=238, y:=301)
                    ComboBox7.Location = New Point(x:=238, y:=362.5)
                    ComboBox8.Location = New Point(x:=238, y:=424)
                    ComboBox9.Location = New Point(x:=238, y:=485.5)
                    Button5.Location = New Point(x:=537, y:=50)
                    Label14.Location = New Point(x:=535, y:=136.5)
                    RichTextBox1.Location = New Point(x:=537, y:=190)
                    RichTextBox1.Size = New Size(width:=287, height:=317)
                    Button6.Location = New Point(x:=538, y:=547)
                End If
            Else
                If VIOSelectToolStripMenuItem.Checked = True Then
                    Label15.Location = New Point(x:=91, y:=63)
                    Label4.Location = New Point(x:=91, y:=132.14)
                    Label16.Location = New Point(x:=91, y:=201.28)
                    Label5.Location = New Point(x:=91, y:=270.43)
                    Label6.Location = New Point(x:=91, y:=339.57)
                    Label7.Location = New Point(x:=91, y:=408.71)
                    Label8.Location = New Point(x:=91, y:=477.85)
                    Button2.Location = New Point(x:=259, y:=547)
                    ComboBox1.Location = New Point(x:=238, y:=59)
                    ComboBox2.Location = New Point(x:=238, y:=127.14)
                    ComboBox5.Location = New Point(x:=238, y:=196.28)
                    ComboBox6.Location = New Point(x:=238, y:=266.43)
                    ComboBox7.Location = New Point(x:=238, y:=336.57)
                    ComboBox8.Location = New Point(x:=238, y:=406.71)
                    ComboBox9.Location = New Point(x:=238, y:=476.85)
                    Button3.Location = New Point(x:=537, y:=46)
                    Button4.Location = New Point(x:=704, y:=46)
                    Label14.Location = New Point(x:=535, y:=132.14)
                    RichTextBox1.Location = New Point(x:=537, y:=171.14)
                    RichTextBox1.Size = New Size(width:=287, height:=329)
                    Button6.Location = New Point(x:=538, y:=547)
                Else
                    Label15.Location = New Point(x:=91, y:=63)
                    Label4.Location = New Point(x:=91, y:=109)
                    Label11.Location = New Point(x:=91, y:=155)
                    Label10.Location = New Point(x:=91, y:=201)
                    Label9.Location = New Point(x:=91, y:=247)
                    Label16.Location = New Point(x:=91, y:=293)
                    Label5.Location = New Point(x:=91, y:=339)
                    Label6.Location = New Point(x:=91, y:=385)
                    Label7.Location = New Point(x:=91, y:=431)
                    Label8.Location = New Point(x:=91, y:=477)
                    Button1.Location = New Point(x:=89, y:=547)
                    Button2.Location = New Point(x:=259, y:=547)
                    ComboBox1.Location = New Point(x:=238, y:=59)
                    ComboBox2.Location = New Point(x:=238, y:=104)
                    Label12.Location = New Point(x:=244, y:=155)
                    Label13.Location = New Point(x:=308, y:=155)
                    RadioButton1.Location = New Point(x:=288, y:=155)
                    RadioButton2.Location = New Point(x:=348, y:=155)
                    ComboBox3.Location = New Point(x:=238, y:=193)
                    ComboBox4.Location = New Point(x:=238, y:=241)
                    ComboBox5.Location = New Point(x:=238, y:=288)
                    ComboBox6.Location = New Point(x:=238, y:=335)
                    ComboBox7.Location = New Point(x:=238, y:=382)
                    ComboBox8.Location = New Point(x:=238, y:=429)
                    ComboBox9.Location = New Point(x:=238, y:=476)
                    Button3.Location = New Point(x:=537, y:=46)
                    Button4.Location = New Point(x:=704, y:=46)
                    Button5.Location = New Point(x:=537, y:=142)
                    Label14.Location = New Point(x:=535, y:=214)
                    RichTextBox1.Location = New Point(x:=537, y:=253)
                    RichTextBox1.Size = New Size(width:=287, height:=244)
                    Button6.Location = New Point(x:=538, y:=547)
                End If
            End If
        End If
        Return 0
    End Function

    Function SerialReset()
        Try
            myserialPort.Close()
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

End Class

Public Class ExSerialPort
        Inherits SerialPort

        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Dim mytype As Type = GetType(SerialPort)
            Dim field As Reflection.FieldInfo = mytype.GetField("internalSerialStream", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
            Dim stream As Object = field.GetValue(Me)
            If Not stream Is Nothing Then
                Try
                    stream.Dispose()
                Catch ex As Exception
                End Try
            End If
            MyBase.Dispose(disposing)
        End Sub

End Class