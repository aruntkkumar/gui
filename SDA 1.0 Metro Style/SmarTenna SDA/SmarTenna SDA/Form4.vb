Imports System.Threading
Imports System.IO.Ports
Imports Microsoft.Win32
Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class Form4

    Public Shared device As Integer = 0
    Public Shared pword As Integer = 0
    Dim Line As String
    Dim myPort As Array
    Dim testPort As Array
    Dim test As Integer
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
        AddHandler System.Windows.Forms.Application.Idle, AddressOf Application_Idle
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
        RadioButton3.Enabled = False
        'ComboBox1.Items.Clear()
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox1.Items.AddRange(myPort)
        If ComboBox1.Items.Count = 0 Then
            ComboBox1.Items.Add("                   No ComPorts detected")
            ComboBox1.SelectedIndex = 0
        Else
            ComboBox1.SelectedIndex = -1
        End If
        SCOUTSC4410ToolStripMenuItem.Checked = False
        SCOUTSC4410ToolStripMenuItem.Enabled = False
        SierraWirelessToolStripMenuItem.Checked = False
        SierraWirelessToolStripMenuItem.Enabled = False
        SierraWirelessToolStripMenuItem.Visible = False
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
                If ComboBox1.Text = "                   No ComPorts detected" Then
                    MetroFramework.MetroMessageBox.Show(Me, "No active devices detected. Please connect a supported device", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                                myserialPort.Write("gpio iodir 00" & vbCrLf)
                                RichTextBox1.Text &= myserialPort.ReadLine()
                                RichTextBox1.Text &= myserialPort.ReadExisting()
                            End If
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'MsgBox(ex.Message, MsgBoxStyle.Information, "Error")
            SerialReset()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox5.Text = "" Then
            MetroFramework.MetroMessageBox.Show(Me, "Please select the Antenna", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If ComboBox6.Text = "" Then
                MetroFramework.MetroMessageBox.Show(Me, "Please select the TRX", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If (ComboBox7.Text = "" Or ComboBox8.Text = "" Or (ComboBox7.SelectedIndex = 0 And ComboBox8.SelectedIndex = 0)) Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please select a supported E-UTRA band or Carrier Aggregation Band", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    If (ComboBox7.SelectedIndex = 2 Or ComboBox7.SelectedIndex = 8 Or ComboBox7.SelectedIndex = 9 Or ComboBox7.SelectedIndex = 10 Or ComboBox7.SelectedIndex = 13 Or ComboBox8.SelectedIndex = 3 Or ComboBox8.SelectedIndex = 9 Or ComboBox8.SelectedIndex = 10) Then
                        If ComboBox9.Text = "" Then
                            MetroFramework.MetroMessageBox.Show(Me, "Please select the TX power level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            If ComboBox9.SelectedIndex = 0 Then
                                If ComboBox7.SelectedIndex = 1 Then
                                    bandsel = &H41
                                    cabandsel = &H80
                                Else
                                    If ComboBox7.SelectedIndex = 2 Then
                                        bandsel = &H42
                                        cabandsel = &H80
                                    Else
                                        If ComboBox7.SelectedIndex = 3 Then
                                            bandsel = &H43
                                            cabandsel = &H80
                                        Else
                                            If ComboBox7.SelectedIndex = 4 Then
                                                bandsel = &H44
                                                cabandsel = &H80
                                            Else
                                                If ComboBox7.SelectedIndex = 5 Then
                                                    bandsel = &H45
                                                    cabandsel = &H80
                                                Else
                                                    If ComboBox7.SelectedIndex = 6 Then
                                                        bandsel = &H47
                                                        cabandsel = &H80
                                                    Else
                                                        If ComboBox7.SelectedIndex = 7 Then
                                                            bandsel = &H48
                                                            cabandsel = &H80
                                                        Else
                                                            If ComboBox7.SelectedIndex = 8 Then
                                                                bandsel = &H4C
                                                                cabandsel = &H80
                                                            Else
                                                                If ComboBox7.SelectedIndex = 9 Then
                                                                    bandsel = &H4D
                                                                    cabandsel = &H80
                                                                Else
                                                                    If ComboBox7.SelectedIndex = 10 Then
                                                                        bandsel = &H54
                                                                        cabandsel = &H80
                                                                    Else
                                                                        If ComboBox7.SelectedIndex = 11 Then
                                                                            bandsel = &H59
                                                                            cabandsel = &H80
                                                                        Else
                                                                            If ComboBox7.SelectedIndex = 12 Then
                                                                                bandsel = &H5A
                                                                                cabandsel = &H80
                                                                            Else
                                                                                If ComboBox7.SelectedIndex = 13 Then
                                                                                    bandsel = &H5D
                                                                                    cabandsel = &H80
                                                                                Else
                                                                                    If ComboBox7.SelectedIndex = 14 Then
                                                                                        bandsel = &H5E
                                                                                        cabandsel = &H80
                                                                                    Else
                                                                                        If ComboBox7.SelectedIndex = 15 Then
                                                                                            bandsel = &H69
                                                                                            cabandsel = &H80
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
                                If ComboBox8.SelectedIndex = 1 Then
                                    bandsel = &H41
                                    cabandsel = &H88
                                Else
                                    If ComboBox8.SelectedIndex = 2 Then
                                        bandsel = &H42
                                        cabandsel = &H85
                                    Else
                                        If ComboBox8.SelectedIndex = 3 Then
                                            bandsel = &H42
                                            cabandsel = &H8C
                                        Else
                                            If ComboBox8.SelectedIndex = 4 Then
                                                bandsel = &H42
                                                cabandsel = &H8D
                                            Else
                                                If ComboBox8.SelectedIndex = 5 Then
                                                    bandsel = &H42
                                                    cabandsel = &H9D
                                                Else
                                                    If ComboBox8.SelectedIndex = 6 Then
                                                        bandsel = &H43
                                                        cabandsel = &H87
                                                    Else
                                                        If ComboBox8.SelectedIndex = 7 Then
                                                            bandsel = &H43
                                                            cabandsel = &H94
                                                        Else
                                                            If ComboBox8.SelectedIndex = 8 Then
                                                                bandsel = &H44
                                                                cabandsel = &H85
                                                            Else
                                                                If ComboBox8.SelectedIndex = 9 Then
                                                                    bandsel = &H44
                                                                    cabandsel = &H8C
                                                                Else
                                                                    If ComboBox8.SelectedIndex = 10 Then
                                                                        bandsel = &H44
                                                                        cabandsel = &H8D
                                                                    Else
                                                                        If ComboBox8.SelectedIndex = 11 Then
                                                                            bandsel = &H44
                                                                            cabandsel = &H9D
                                                                        Else
                                                                            If ComboBox8.SelectedIndex = 12 Then
                                                                                bandsel = &H45
                                                                                cabandsel = &H9E
                                                                            Else
                                                                                If ComboBox8.SelectedIndex = 13 Then
                                                                                    bandsel = &H47
                                                                                    cabandsel = &H94
                                                                                Else
                                                                                    If ComboBox8.SelectedIndex = 14 Then
                                                                                        bandsel = &H4C
                                                                                        cabandsel = &H9E
                                                                                    Else
                                                                                        If ComboBox8.SelectedIndex = 15 Then
                                                                                            bandsel = &H69
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
                                        RichTextBox1.Text &= myserialPort.ReadLine()
                                        RichTextBox1.Text &= myserialPort.ReadExisting()
                                        Thread.Sleep(25)
                                        myserialPort.Write("gpio writeall " & bandsel.ToString("X") & vbCr)
                                        RichTextBox1.Text &= myserialPort.ReadLine()
                                        RichTextBox1.Text &= myserialPort.ReadExisting()
                                        Thread.Sleep(25)
                                        myserialPort.Write("gpio writeall " & cabandsel.ToString("X") & vbCrLf)
                                        RichTextBox1.Text &= myserialPort.ReadLine()
                                        RichTextBox1.Text &= myserialPort.ReadExisting()
                                    Else
                                        myserialPort.WriteLine("rw 1 0x05 0x" & byte1.ToString("X") & vbCrLf & "rw 1 0x05 0x" & bandsel.ToString("X") & vbCrLf & "rw 1 0x05 0x" & cabandsel.ToString("X") & vbCrLf)
                                        RichTextBox1.Text &= myserialPort.ReadLine()
                                        RichTextBox1.Text &= myserialPort.ReadExisting()
                                    End If
                                Catch ex As Exception
                                    MetroFramework.MetroMessageBox.Show(Me, myserialPort.PortName & " does not exist. Please open a valid COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    'MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
                                    SerialReset()
                                    Exit Sub
                                End Try
                            Else
                                MetroFramework.MetroMessageBox.Show(Me, "TX power level selection is not available in Demo Mode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If
                    Else
                        MetroFramework.MetroMessageBox.Show(Me, "The selected E-UTRA band or Carrier Aggregation Band is not available in Demo Mode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
            MetroFramework.MetroMessageBox.Show(Me, "Please open a Comm Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            If RadioButton2.Checked = False And RadioButton1.Checked = False Then
                MetroFramework.MetroMessageBox.Show(Me, "The selected Comm Port is not suited for DEMO Mode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                            myserialPort.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
                            RichTextBox1.Text &= myserialPort.ReadLine()
                            RichTextBox1.Text &= myserialPort.ReadExisting()
                            'SerialPort1.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
                            'RichTextBox1.Text &= SerialPort1.ReadExisting()
                        Catch ex As Exception
                            MetroFramework.MetroMessageBox.Show(Me, myserialPort.PortName & " does not exist. Please open a valid COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub Form4_SizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.SizeChanged
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
        'RadioButton3.Checked = False
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
        'RadioButton3.Checked = False
        device = 3
        RichTextBox1.Text &= "Active device selected as GPIO" & vbCrLf & "Port Name: " & readvalue1 & "; Baud Rate: 9600" & vbCrLf & vbCrLf
        myserialPort.Write("gpio iodir 00" & vbCrLf)
        RichTextBox1.Text &= myserialPort.ReadLine()
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
                    Label16.Location = New Point(x:=(intX / 8), y:=(intY * 1.5 / 13))
                    Label5.Location = New Point(x:=(intX / 8), y:=(intY * 3.4 / 13))
                    Label6.Location = New Point(x:=(intX / 8), y:=(intY * 5.3 / 13))
                    Label7.Location = New Point(x:=(intX / 8), y:=(intY * 7.2 / 13))
                    Label8.Location = New Point(x:=(intX / 8), y:=(intY * 9.1 / 13))
                    Button2.Location = New Point(x:=(intX / 3.5), y:=(intY * 11 / 13))
                    ComboBox5.Location = New Point(x:=(intX / 3.5), y:=(intY * 1.5 / 13))
                    ComboBox6.Location = New Point(x:=(intX / 3.5), y:=(intY * 3.4 / 13))
                    ComboBox7.Location = New Point(x:=(intX / 3.5), y:=(intY * 5.3 / 13))
                    ComboBox8.Location = New Point(x:=(intX / 3.5), y:=(intY * 7.2 / 13))
                    ComboBox9.Location = New Point(x:=(intX / 3.5), y:=(intY * 9.1 / 13))
                    Label14.Location = New Point(x:=(intX / 1.9), y:=(intY * 1.5 / 13))
                    RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=(intY * 2 / 13))
                    RichTextBox1.Size = New Size(width:=((intX / 1.29) - (intX / 1.9) + 119), height:=((intY * 9.1 / 13) - (intY * 2 / 13) + 25))
                    Button6.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 13))
                Else
                    Label11.Location = New Point(x:=(intX / 8), y:=(intY * 1.5 / 13))
                    Label10.Location = New Point(x:=(intX / 8), y:=(intY * 2.6875 / 13))
                    Label9.Location = New Point(x:=(intX / 8), y:=(intY * 3.875 / 13))
                    Label16.Location = New Point(x:=(intX / 8), y:=(intY * 5.0625 / 13))
                    Label5.Location = New Point(x:=(intX / 8), y:=(intY * 6.25 / 13))
                    Label6.Location = New Point(x:=(intX / 8), y:=(intY * 7.4375 / 13))
                    Label7.Location = New Point(x:=(intX / 8), y:=(intY * 8.625 / 13))
                    Label8.Location = New Point(x:=(intX / 8), y:=(intY * 9.8125 / 13))
                    Button1.Location = New Point(x:=(intX / 8), y:=(intY * 11 / 13))
                    Button2.Location = New Point(x:=(intX / 3.5), y:=(intY * 11 / 13))
                    Label12.Location = New Point(x:=((intX / 3.5)), y:=(intY * 1.5 / 13))
                    Label13.Location = New Point(x:=((intX / 3.5) + 103), y:=(intY * 1.5 / 13))
                    Label18.Location = New Point(x:=((intX / 3.5) + 197), y:=(intY * 1.5 / 13))
                    RadioButton1.Location = New Point(x:=((intX / 3.5) + 53), y:=((intY * 1.5 / 13) + 3))
                    RadioButton2.Location = New Point(x:=((intX / 3.5) + 147), y:=((intY * 1.5 / 13) + 3))
                    RadioButton3.Location = New Point(x:=((intX / 3.5) + 250), y:=((intY * 1.5 / 13) + 3))
                    ComboBox3.Location = New Point(x:=(intX / 3.5), y:=(intY * 2.6875 / 13))
                    ComboBox4.Location = New Point(x:=(intX / 3.5), y:=(intY * 3.875 / 13))
                    ComboBox5.Location = New Point(x:=(intX / 3.5), y:=(intY * 5.0625 / 13))
                    ComboBox6.Location = New Point(x:=(intX / 3.5), y:=(intY * 6.25 / 13))
                    ComboBox7.Location = New Point(x:=(intX / 3.5), y:=(intY * 7.4375 / 13))
                    ComboBox8.Location = New Point(x:=(intX / 3.5), y:=(intY * 8.625 / 13))
                    ComboBox9.Location = New Point(x:=(intX / 3.5), y:=(intY * 9.8125 / 13))
                    Button5.Location = New Point(x:=(intX / 1.9), y:=(intY * 1.5 / 13))
                    Label14.Location = New Point(x:=(intX / 1.9), y:=(intY * 2.6878 / 13))
                    RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=(intY * 3.1875 / 13))
                    RichTextBox1.Size = New Size(width:=((intX / 1.29) - (intX / 1.9) + 119), height:=((intY * 9.8125 / 13) - (intY * 3.1875 / 13) + 25))
                    Button6.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 13))
                End If
            Else
                If VIOSelectToolStripMenuItem.Checked = True Then
                    Label15.Location = New Point(x:=(intX / 8), y:=(intY * 1.5 / 13))
                    Label4.Location = New Point(x:=(intX / 8), y:=(intY * 2.86 / 13))
                    Label16.Location = New Point(x:=(intX / 8), y:=(intY * 4.21 / 13))
                    Label5.Location = New Point(x:=(intX / 8), y:=(intY * 5.57 / 13))
                    Label6.Location = New Point(x:=(intX / 8), y:=(intY * 6.93 / 13))
                    Label7.Location = New Point(x:=(intX / 8), y:=(intY * 8.29 / 13))
                    Label8.Location = New Point(x:=(intX / 8), y:=(intY * 9.64 / 13))
                    Button2.Location = New Point(x:=(intX / 3.5), y:=(intY * 11 / 13))
                    ComboBox1.Location = New Point(x:=(intX / 3.5), y:=(intY * 1.5 / 13))
                    ComboBox2.Location = New Point(x:=(intX / 3.5), y:=(intY * 2.86 / 13))
                    ComboBox5.Location = New Point(x:=(intX / 3.5), y:=(intY * 4.21 / 13))
                    ComboBox6.Location = New Point(x:=(intX / 3.5), y:=(intY * 5.57 / 13))
                    ComboBox7.Location = New Point(x:=(intX / 3.5), y:=(intY * 6.93 / 13))
                    ComboBox8.Location = New Point(x:=(intX / 3.5), y:=(intY * 8.29 / 13))
                    ComboBox9.Location = New Point(x:=(intX / 3.5), y:=(intY * 9.64 / 13))
                    Button3.Location = New Point(x:=(intX / 1.9), y:=(intY * 1.5 / 13))
                    Button4.Location = New Point(x:=(intX / 1.29), y:=(intY * 1.5 / 13))
                    Label14.Location = New Point(x:=(intX / 1.9), y:=(intY * 2.86 / 13))
                    RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=(intY * 3.36 / 13))
                    RichTextBox1.Size = New Size(width:=((intX / 1.29) - (intX / 1.9) + 119), height:=((intY * 9.64 / 13) - (intY * 3.36 / 13) + 25))
                    Button6.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 13))
                Else
                    Label15.Location = New Point(x:=(intX / 8), y:=(intY * 1.5 / 13))
                    Label4.Location = New Point(x:=(intX / 8), y:=(intY * 2.45 / 13))
                    Label11.Location = New Point(x:=(intX / 8), y:=(intY * 3.4 / 13))
                    Label10.Location = New Point(x:=(intX / 8), y:=(intY * 4.35 / 13))
                    Label9.Location = New Point(x:=(intX / 8), y:=(intY * 5.3 / 13))
                    Label16.Location = New Point(x:=(intX / 8), y:=(intY * 6.25 / 13))
                    Label5.Location = New Point(x:=(intX / 8), y:=(intY * 7.2 / 13))
                    Label6.Location = New Point(x:=(intX / 8), y:=(intY * 8.15 / 13))
                    Label7.Location = New Point(x:=(intX / 8), y:=(intY * 9.1 / 13))
                    Label8.Location = New Point(x:=(intX / 8), y:=(intY * 10.05 / 13))
                    Button1.Location = New Point(x:=(intX / 8), y:=(intY * 11 / 13))
                    Button2.Location = New Point(x:=(intX / 3.5), y:=(intY * 11 / 13))
                    ComboBox1.Location = New Point(x:=(intX / 3.5), y:=(intY * 1.5 / 13))
                    ComboBox2.Location = New Point(x:=(intX / 3.5), y:=(intY * 2.45 / 13))
                    Label12.Location = New Point(x:=((intX / 3.5)), y:=(intY * 3.4 / 13))
                    Label13.Location = New Point(x:=((intX / 3.5) + 103), y:=(intY * 3.4 / 13))
                    Label18.Location = New Point(x:=((intX / 3.5) + 197), y:=(intY * 3.4 / 13))
                    RadioButton1.Location = New Point(x:=((intX / 3.5) + 53), y:=((intY * 3.4 / 13) + 3))
                    RadioButton2.Location = New Point(x:=((intX / 3.5) + 147), y:=((intY * 3.4 / 13) + 3))
                    RadioButton3.Location = New Point(x:=((intX / 3.5) + 250), y:=((intY * 3.4 / 13) + 3))
                    ComboBox3.Location = New Point(x:=(intX / 3.5), y:=(intY * 4.35 / 13))
                    ComboBox4.Location = New Point(x:=(intX / 3.5), y:=(intY * 5.3 / 13))
                    ComboBox5.Location = New Point(x:=(intX / 3.5), y:=(intY * 6.25 / 13))
                    ComboBox6.Location = New Point(x:=(intX / 3.5), y:=(intY * 7.2 / 13))
                    ComboBox7.Location = New Point(x:=(intX / 3.5), y:=(intY * 8.15 / 13))
                    ComboBox8.Location = New Point(x:=(intX / 3.5), y:=(intY * 9.1 / 13))
                    ComboBox9.Location = New Point(x:=(intX / 3.5), y:=(intY * 10.05 / 13))
                    Button3.Location = New Point(x:=(intX / 1.9), y:=(intY * 1.5 / 13))
                    Button4.Location = New Point(x:=(intX / 1.29), y:=(intY * 1.5 / 13))
                    Button5.Location = New Point(x:=(intX / 1.9), y:=(intY * 3.4 / 13))
                    Label14.Location = New Point(x:=(intX / 1.9), y:=(intY * 4.35 / 13))
                    RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=(intY * 4.85 / 13))
                    RichTextBox1.Size = New Size(width:=((intX / 1.29) - (intX / 1.9) + 119), height:=((intY * 10.05 / 13) - (intY * 4.85 / 13) + 25))
                    Button6.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 13))
                End If
            End If
        Else
            If CommPortSelectToolStripMenuItem.Checked = True Then
                If VIOSelectToolStripMenuItem.Checked = True Then
                    Label16.Location = New Point(x:=95, y:=102)
                    Label5.Location = New Point(x:=95, y:=208)
                    Label6.Location = New Point(x:=95, y:=314)
                    Label7.Location = New Point(x:=95, y:=420)
                    Label8.Location = New Point(x:=95, y:=526)
                    Button2.Location = New Point(x:=273, y:=632)
                    ComboBox5.Location = New Point(x:=273, y:=102)
                    ComboBox6.Location = New Point(x:=273, y:=208)
                    ComboBox7.Location = New Point(x:=273, y:=314)
                    ComboBox8.Location = New Point(x:=273, y:=420)
                    ComboBox9.Location = New Point(x:=273, y:=526)
                    Label14.Location = New Point(x:=591, y:=102)
                    RichTextBox1.Location = New Point(x:=591, y:=137)
                    RichTextBox1.Size = New Size(width:=287, height:=414)
                    Button6.Location = New Point(x:=591, y:=632)
                Else
                    Label11.Location = New Point(x:=95, y:=102)
                    Label10.Location = New Point(x:=95, y:=168.25)
                    Label9.Location = New Point(x:=95, y:=234.5)
                    Label16.Location = New Point(x:=95, y:=300.75)
                    Label5.Location = New Point(x:=95, y:=367)
                    Label6.Location = New Point(x:=95, y:=433.25)
                    Label7.Location = New Point(x:=95, y:=499.5)
                    Label8.Location = New Point(x:=95, y:=565.75)
                    Button1.Location = New Point(x:=95, y:=632)
                    Button2.Location = New Point(x:=273, y:=632)
                    Label12.Location = New Point(x:=273, y:=102)
                    Label13.Location = New Point(x:=376, y:=102)
                    Label18.Location = New Point(x:=470, y:=102)
                    RadioButton1.Location = New Point(x:=326, y:=105)
                    RadioButton2.Location = New Point(x:=420, y:=105)
                    RadioButton3.Location = New Point(x:=523, y:=105)
                    ComboBox3.Location = New Point(x:=273, y:=168.25)
                    ComboBox4.Location = New Point(x:=273, y:=234.5)
                    ComboBox5.Location = New Point(x:=273, y:=300.75)
                    ComboBox6.Location = New Point(x:=273, y:=367)
                    ComboBox7.Location = New Point(x:=273, y:=433.25)
                    ComboBox8.Location = New Point(x:=273, y:=499.5)
                    ComboBox9.Location = New Point(x:=273, y:=565.75)
                    Button5.Location = New Point(x:=591, y:=102)
                    Label14.Location = New Point(x:=591, y:=168.25)
                    RichTextBox1.Location = New Point(x:=591, y:=203.25)
                    RichTextBox1.Size = New Size(width:=273, height:=387.5)
                    Button6.Location = New Point(x:=591, y:=632)
                End If
            Else
                If VIOSelectToolStripMenuItem.Checked = True Then
                    Label15.Location = New Point(x:=95, y:=102)
                    Label4.Location = New Point(x:=95, y:=177.71)
                    Label16.Location = New Point(x:=95, y:=253.43)
                    Label5.Location = New Point(x:=95, y:=329.14)
                    Label6.Location = New Point(x:=95, y:=404.86)
                    Label7.Location = New Point(x:=95, y:=480.57)
                    Label8.Location = New Point(x:=95, y:=556.29)
                    Button2.Location = New Point(x:=273, y:=632)
                    ComboBox1.Location = New Point(x:=273, y:=102)
                    ComboBox2.Location = New Point(x:=273, y:=177.71)
                    ComboBox5.Location = New Point(x:=273, y:=253.43)
                    ComboBox6.Location = New Point(x:=273, y:=329.14)
                    ComboBox7.Location = New Point(x:=273, y:=404.86)
                    ComboBox8.Location = New Point(x:=273, y:=480.57)
                    ComboBox9.Location = New Point(x:=273, y:=556.29)
                    Button3.Location = New Point(x:=591, y:=102)
                    Button4.Location = New Point(x:=745, y:=102)
                    Label14.Location = New Point(x:=591, y:=177.71)
                    RichTextBox1.Location = New Point(x:=591, y:=212.71)
                    RichTextBox1.Size = New Size(width:=273, height:=368.58)
                    Button6.Location = New Point(x:=591, y:=632)
                Else
                    Label15.Location = New Point(x:=95, y:=102)
                    Label4.Location = New Point(x:=95, y:=155)
                    Label11.Location = New Point(x:=95, y:=208)
                    Label10.Location = New Point(x:=95, y:=261)
                    Label9.Location = New Point(x:=95, y:=314)
                    Label16.Location = New Point(x:=95, y:=367)
                    Label5.Location = New Point(x:=95, y:=420)
                    Label6.Location = New Point(x:=95, y:=473)
                    Label7.Location = New Point(x:=95, y:=526)
                    Label8.Location = New Point(x:=95, y:=579)
                    Button1.Location = New Point(x:=95, y:=632)
                    Button2.Location = New Point(x:=273, y:=632)
                    ComboBox1.Location = New Point(x:=273, y:=102)
                    ComboBox2.Location = New Point(x:=273, y:=155)
                    Label12.Location = New Point(x:=273, y:=208)
                    Label13.Location = New Point(x:=376, y:=208)
                    Label18.Location = New Point(x:=470, y:=208)
                    RadioButton1.Location = New Point(x:=326, y:=211)
                    RadioButton2.Location = New Point(x:=420, y:=211)
                    RadioButton3.Location = New Point(x:=523, y:=211)
                    ComboBox3.Location = New Point(x:=273, y:=261)
                    ComboBox4.Location = New Point(x:=273, y:=314)
                    ComboBox5.Location = New Point(x:=273, y:=367)
                    ComboBox6.Location = New Point(x:=273, y:=420)
                    ComboBox7.Location = New Point(x:=273, y:=473)
                    ComboBox8.Location = New Point(x:=273, y:=526)
                    ComboBox9.Location = New Point(x:=273, y:=579)
                    Button3.Location = New Point(x:=591, y:=102)
                    Button4.Location = New Point(x:=745, y:=102)
                    Button5.Location = New Point(x:=591, y:=199)
                    Label14.Location = New Point(x:=591, y:=261)
                    RichTextBox1.Location = New Point(x:=591, y:=296)
                    RichTextBox1.Size = New Size(width:=273, height:=310)
                    Button6.Location = New Point(x:=591, y:=632)
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
        If ComboBox1.Items.Count = 0 Then
            ComboBox1.Items.Add("                   No ComPorts detected")
            ComboBox1.SelectedIndex = 0
        Else
            ComboBox1.SelectedIndex = -1
        End If
        ComboBox1.DroppedDown = False
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

    Private DisabledIndices As New List(Of Integer)(New Integer() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}) 'Disable items 1 to 15

    Private Sub ComboBox8_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs) Handles ComboBox8.DrawItem
        If e.Index = -1 Then Exit Sub
        If DisabledIndices.Contains(e.Index) Then
            e.Graphics.FillRectangle(New SolidBrush(SystemColors.Control), e.Bounds)
        End If
        e.Graphics.DrawString(ComboBox8.Items(e.Index).ToString(), ComboBox8.Font, New SolidBrush(ComboBox8.ForeColor), e.Bounds)
        'If DisabledIndices.Contains(e.Index) Then
        '    e.Graphics.DrawString(ComboBox8.SelectedItem, ComboBox8.Font, New SolidBrush(Color.Gray), e.Bounds.X, e.Bounds.Y)
        'Else
        '    e.Graphics.DrawString(ComboBox8.SelectedItem, ComboBox8.Font, Brushes.Black, e.Bounds.X, e.Bounds.Y)
        'End If
    End Sub

    Dim lastIndex As Integer = -1
    Private Sub ComboBox8_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        'If DisabledIndices.Contains(ComboBox8.SelectedIndex) Then
        '    ComboBox8.SelectedIndex = lastIndex
        'Else
        '    lastIndex = ComboBox8.SelectedIndex
        'End If
        If ComboBox8.SelectedIndex = -1 Then
            Exit Sub
        End If
        If ComboBox8.SelectedIndex = 0 Then
            Exit Sub
        Else
            ComboBox7.SelectedIndex = 0
        End If
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        If ComboBox7.SelectedIndex = -1 Then
            Exit Sub
        End If
        If ComboBox7.SelectedIndex = 0 Then
            Exit Sub
        Else
            ComboBox8.SelectedIndex = 0
        End If
    End Sub

    Private Sub Application_Idle(ByVal sender As Object, ByVal e As EventArgs)
        If Button3.Enabled = True Then
            testPort = IO.Ports.SerialPort.GetPortNames()
            If Not testPort.Length = myPort.Length Then
                SerialReset()
                Exit Sub
            End If
            test = 0
            For Each item1 As String In testPort
                For Each item2 As String In myPort
                    Dim res As Int16 = String.Compare(item1, item2) ' compare items
                    If res = 0 Then 'the items are equal
                        test += 1
                    End If
                Next
            Next
            If Not test = myPort.Length Then
                SerialReset()
            End If
        End If
    End Sub

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