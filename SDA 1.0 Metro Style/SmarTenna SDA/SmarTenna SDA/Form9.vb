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
    Dim testPort As Array
    Dim vio As Integer
    Dim byte1 As Integer
    Dim byte2 As Integer
    Dim byte3 As Integer
    Dim byte4 As Integer
    Dim Reg1 As RegistryKey
    Dim Reg2 As RegistryKey
    Dim portname As String
    Dim readValue As String
    Dim fullscreen As Boolean = False
    Dim myserialPort2 As New ExSerialPort
    Dim test As Integer
    Dim test1 As Double
    Dim test2 As Integer
    Dim i As Integer
    Delegate Sub SetTextCallBack(ByVal [text] As String)
    Dim foundit As Integer
    Dim output As String

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Date.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.Size = New Size(976, 727)
        Timer1.Enabled = True
        OptionsToolStripMenuItem.Enabled = True
        NewToolStripMenuItem.Enabled = True
        OpenToolStripMenuItem.Enabled = True
        SaveToolStripMenuItem.Enabled = True
        RichTextBox1.ReadOnly = True
        'SerialReset()
        'Button2.Enabled = False
        'Button3.Enabled = True
        'Button4.Enabled = False
        'Button5.Enabled = True
        'ComboBox1.Enabled = True
        'ComboBox2.Enabled = True
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        ComboBox11.Enabled = False
        ComboBox12.Enabled = False
        ComboBox7.Enabled = False
        ComboBox8.Enabled = False
        'RadioButton1.Enabled = True
        'RadioButton2.Enabled = True
        RadioButton1.Enabled = False
        RadioButton2.Enabled = False
        RadioButton3.Enabled = False
        'ComboBox1.Items.Clear()
        myPort = IO.Ports.SerialPort.GetPortNames()
        'ComboBox1.Items.AddRange(myPort)
        'If ComboBox1.Items.Count = 0 Then
        '    ComboBox1.Items.Add("                   No ComPorts detected")
        '    ComboBox1.SelectedIndex = 0
        'Else
        '    ComboBox1.SelectedIndex = -1
        'End If
        SCOUTSC4410ToolStripMenuItem.Checked = False
        SCOUTSC4410ToolStripMenuItem.Enabled = False
        PINGPIOToolStripMenuItem.Checked = False
        PINGPIOToolStripMenuItem.Enabled = False
        SCOUTSC4415ToolStripMenuItem.Checked = False
        SCOUTSC4415ToolStripMenuItem.Enabled = False
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
            If item <> Nothing Then
                For Each Str As String In myPort
                    If Str.Contains(item) Then
                        SCOUTSC4410ToolStripMenuItem.Enabled = True
                    End If
                Next
            End If
        Next
        'Catch ex As Exception
        'MessageBox.Show(ex.Message)
        'End Try
        list = x.ComPortNames("2A19", "0800")
        For Each item As String In list
            If item <> Nothing Then
                For Each Str As String In myPort
                    If Str.Contains(item) Then
                        PINGPIOToolStripMenuItem.Enabled = True
                    End If
                Next
            End If
        Next
        list = x.ComPortNames("173C", "0003")
        For Each item As String In list
            If item <> Nothing Then
                For Each Str As String In myPort
                    If Str.Contains(item) Then
                        SCOUTSC4415ToolStripMenuItem.Enabled = True
                    End If
                Next
            End If
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
        ConfigurationToolStripMenuItem.Enabled = False
        TypeASSCCXM3664XRToolStripMenuItem.Checked = False
        TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
        TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
        TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
        TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        'TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Visible = False
        'TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Visible = False
        'TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Visible = False
        AddHandler System.Windows.Forms.Application.Idle, AddressOf Application_Idle
        Control.CheckForIllegalCrossThreadCalls = False
    End Sub

    'Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
    '    Try
    '        If ComboBox1.Text = "" Then
    '            MetroFramework.MetroMessageBox.Show(Me, "Please select a Comm Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Else
    '            If ComboBox1.Text = "                   No ComPorts detected" Then
    '                MetroFramework.MetroMessageBox.Show(Me, "No active devices detected. Please connect a supported device", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Else
    '                If ComboBox2.Text = "" Then
    '                    MetroFramework.MetroMessageBox.Show(Me, "Please select a Baud Rate", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Else
    '                    'SerialPort1.PortName = ComboBox1.Text
    '                    'SerialPort1.BaudRate = ComboBox2.Text
    '                    'SerialPort1.Parity = Parity.None
    '                    'SerialPort1.DataBits = 8
    '                    'SerialPort1.StopBits = StopBits.One
    '                    'SerialPort1.Open()
    '                    myserialPort2.PortName = ComboBox1.Text
    '                    myserialPort2.BaudRate = ComboBox2.Text
    '                    myserialPort2.Parity = Parity.None
    '                    myserialPort2.DataBits = 8
    '                    myserialPort2.StopBits = StopBits.One
    '                    myserialPort2.Open()
    '                    'Button2.Enabled = True
    '                    Button3.Enabled = False
    '                    Button4.Enabled = True
    '                    Button5.Enabled = True
    '                    ComboBox1.Enabled = False
    '                    ComboBox2.Enabled = False
    '                    ComboBox3.Enabled = True
    '                    ComboBox4.Enabled = True
    '                    'RadioButton1.Enabled = True
    '                    'RadioButton2.Enabled = True
    '                    RichTextBox1.Text &= "Port Name: " & ComboBox1.Text & "; Baud Rate: " & ComboBox2.Text & vbCrLf & vbCrLf
    '                    Dim x As New ComPortFinder
    '                    Dim list As List(Of String)
    '                    list = x.ComPortNames("173C", "0002")
    '                    For Each item As String In list
    '                        If (item = ComboBox1.Text) Then
    '                            RadioButton1.Checked = False
    '                            RadioButton2.Checked = True
    '                            device = 1
    '                            SCOUTSC4410ToolStripMenuItem.Checked = True
    '                            'SierraWirelessToolStripMenuItem.Checked = False
    '                            PINGPIOToolStripMenuItem.Checked = False
    '                        End If
    '                    Next
    '                    list = x.ComPortNames("2A19", "0800")
    '                    For Each item As String In list
    '                        If (item = ComboBox1.Text) Then
    '                            RadioButton1.Checked = True
    '                            RadioButton2.Checked = False
    '                            Button5.Enabled = False
    '                            ComboBox3.Enabled = False
    '                            ComboBox4.Enabled = False
    '                            Button2.Enabled = True
    '                            device = 3
    '                            SCOUTSC4410ToolStripMenuItem.Checked = False
    '                            'SierraWirelessToolStripMenuItem.Checked = False
    '                            PINGPIOToolStripMenuItem.Checked = True
    '                            myserialPort2.Write("gpio iodir 00" & vbCrLf)
    '                            RichTextBox1.Text &= myserialPort2.ReadLine()
    '                            RichTextBox1.Text &= myserialPort2.ReadExisting()
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        'MsgBox(ex.Message, MsgBoxStyle.Information, "Error")
    '        SerialReset()
    '    End Try
    'End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Do
            If PINGPIOToolStripMenuItem.Checked = False AndAlso SCOUTSC4410ToolStripMenuItem.Checked = False AndAlso SCOUTSC4415ToolStripMenuItem.Checked = False Then
                MetroFramework.MetroMessageBox.Show(Me, "Please select an available device under ""My Devices"" and try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If SCOUTSC4410ToolStripMenuItem.Checked = True Or SCOUTSC4415ToolStripMenuItem.Checked = True Then
                If ComboBox3.Text = "" Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please select the Device Slave address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If ComboBox4.Text = "" Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please select VIO voltage", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
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
                End If
            End If
            If (ComboBox3.Text = "6 (UID = Low); SONY CXM3664XR" Or ComboBox3.Text = "7 (UID - High); SONY CXM3664XR") AndAlso ((SCOUTSC4410ToolStripMenuItem.Checked = True) Or (SCOUTSC4415ToolStripMenuItem.Checked = True)) Then
                test2 = CInt(TextBox3.Text)
                If (test2 <> ListBox1.Items.Count) Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter all the RFFE MIPI values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                Try
                    If SCOUTSC4410ToolStripMenuItem.Checked = True Then
                        SCOUTSC4410_Start()
                        myserialPort2.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
                    ElseIf SCOUTSC4415ToolStripMenuItem.Checked = True Then
                        SCOUTSC4415_Start()
                        myserialPort2.WriteLine("mode 1" & vbCrLf & "vio " & vio & vbCrLf & "clock 52000" & vbCrLf)
                    End If
                    RichTextBox1.Text &= myserialPort2.ReadLine().Replace("->", "")
                    RichTextBox1.Text &= myserialPort2.ReadExisting().Replace("->", "")
                    'SerialPort1.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
                    'RichTextBox1.Text &= SerialPort1.ReadExisting()
                    test = &H0
                    test = test Or ListBox1.Items.Item(0)
                    If ComboBox3.Text = "6 (UID = Low); SONY CXM3664XR" Then
                        myserialPort2.WriteLine("rw 6 0x01 0x" & test.ToString("X") & vbCrLf)
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace("->", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace("->", "")
                        RichTextBox1.Text &= "State selected: " & test & vbCrLf
                        myserialPort2.WriteLine("rr 6 0x01" & vbCrLf)
                        RichTextBox1.Text &= "SSC current state: "
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace("->", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace("->", "")
                    Else
                        myserialPort2.WriteLine("rw 7 0x01 0x" & test.ToString("X") & vbCrLf)
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace("->", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace("->", "")
                        RichTextBox1.Text &= "State selected: " & test & vbCrLf
                        myserialPort2.WriteLine("rr 7 0x01" & vbCrLf)
                        RichTextBox1.Text &= "SSC current state: "
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace("->", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace("->", "")
                    End If
                Catch ex As Exception
                    'MetroFramework.MetroMessageBox.Show(Me, myserialPort2.PortName & " does not exist. Please open a valid COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
                    SerialReset()
                    Exit Sub
                End Try
            Else
                If TypeBSP4TCXA4447GToolStripMenuItem.Checked = True Or TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = True Or TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = True Or TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = True Then
                    If ComboBox9.Text = "" AndAlso TypeASSCCXM3664XRToolStripMenuItem.Checked = False Then
                        MetroFramework.MetroMessageBox.Show(Me, "Please select the RF Switch 1 type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    If ComboBox5.Text = "" AndAlso TypeASSCCXM3664XRToolStripMenuItem.Checked = False Then
                        MetroFramework.MetroMessageBox.Show(Me, "Please select the status for RF Switch 1", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
                'If ComboBox10.Text = "" Then
                '    MetroFramework.MetroMessageBox.Show(Me, "Please select the RF Switch 2 type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Exit Sub
                'Else
                '    If ComboBox6.Text = "" Then
                '        MetroFramework.MetroMessageBox.Show(Me, "Please select the status for RF Switch 2", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '        Exit Sub
                '    Else
                'If ComboBox11.Text = "" Then
                'MetroFramework.MetroMessageBox.Show(Me, "Please select the RF Switch 3 type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                'If ComboBox7.Text = "" Then
                'MetroFramework.MetroMessageBox.Show(Me, "Please select the status for RF Switch 3", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                'If ComboBox12.Text = "" Then
                'MetroFramework.MetroMessageBox.Show(Me, "Please select the RF Switch 4 type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                'If ComboBox8.Text = "" Then
                'MetroFramework.MetroMessageBox.Show(Me, "Please select the status for RF Switch 4", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                If (TextBox3.Text = "") Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter the number of RFFE MIPI values to be programmed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                Try
                    test = CInt(TextBox3.Text)
                Catch ex As Exception
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter a number to denote the number of RFFE MIPI values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End Try
                If (test < 0) Then
                    MetroFramework.MetroMessageBox.Show(Me, "Invalid value. Please enter a number to denote the number of RFFE MIPI values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                ElseIf ComboBox13.SelectedIndex = -1 AndAlso TypeBSP4TCXA4447GToolStripMenuItem.Checked = False Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please select the MIPI device that needs to be addressed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                test2 = CInt(TextBox3.Text)
                If (test2 <> ListBox1.Items.Count) Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter all the RFFE MIPI values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                If TextBox2.Text = "" Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter a voltage value between 0 to 1.8V", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                Try
                    test1 = CDbl(TextBox2.Text)
                    test = CInt(TextBox2.Text)
                Catch ex As Exception
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter a voltage value between 0 to 1.8V", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End Try
                'test1 = CInt(TextBox2.Text)
                'test = CInt(TextBox2.Text)
                If (test1 < 0.0 Or test1 > 1.8) Then
                    MetroFramework.MetroMessageBox.Show(Me, "Invalid value. Please enter a voltage value between 0 to 1.8V", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                'If ListBox1.Items.Count = 0 Then
                'MetroFramework.MetroMessageBox.Show(Me, "Please enter the RFFE MIPI values states using integers between 0 to 64, starting from RFFE MIPI values1. (The RFFE MIPI values states ranges from State0 to State64)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                byte1 = &HC0
                byte2 = &H40
                byte3 = &H80
                byte4 = &H40
                'If ComboBox5.Text = "Isolation" Then
                '    byte1 = byte1 Or &H08
                'Else
                '    If ComboBox5.Text = "ANT-RF1" Then
                '        byte1 = byte1 Or &H00
                '    Else
                '        If ComboBox5.Text = "ANT-RF2" Then
                '            byte1 = byte1 Or &H20
                '        Else
                '            If ComboBox5.Text = "ANT-RF3" Then
                '                byte1 = byte1 Or &H10
                '            Else
                '                If ComboBox5.Text = "ANT-RF4" Then
                '                    byte1 = byte1 Or &H30
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
                Select Case ComboBox5.Text
                    Case "RF8", "Isolation", "ALL OFF"
                        byte1 = byte1 Or &H0
                    Case "RF1"
                        byte1 = byte1 Or &H8
                    Case "RF2"
                        byte1 = byte1 Or &H10
                    Case "RF3"
                        byte1 = byte1 Or &H18
                    Case "RF4"
                        byte1 = byte1 Or &H20
                    Case "RF5", "RF1/2"
                        byte1 = byte1 Or &H28
                    Case "RF6", "RF3/4"
                        byte1 = byte1 Or &H30
                    Case "RF7", "ALL ON"
                        byte1 = byte1 Or &H38
                End Select
                'If ComboBox6.Text = "Isolation" Then
                '    byte1 = byte1 Or &H01
                'Else
                '    If ComboBox6.Text = "ANT-RF1" Then
                '        byte1 = byte1 Or &H00
                '    Else
                '        If ComboBox6.Text = "ANT-RF2" Then
                '            byte1 = byte1 Or &H04
                '        Else
                '            If ComboBox6.Text = "ANT-RF3" Then
                '                byte1 = byte1 Or &H02
                '            Else
                '                If ComboBox6.Text = "ANT-RF4" Then
                '                    byte1 = byte1 Or &H06
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
                Select Case ComboBox6.Text
                    Case "RF8", "Isolation", "ALL OFF"
                        byte1 = byte1 Or &H0
                    Case "RF1"
                        byte1 = byte1 Or &H1
                    Case "RF2"
                        byte1 = byte1 Or &H2
                    Case "RF3"
                        byte1 = byte1 Or &H3
                    Case "RF4"
                        byte1 = byte1 Or &H4
                    Case "RF5", "RF1/2"
                        byte1 = byte1 Or &H5
                    Case "RF6", "RF3/4"
                        byte1 = byte1 Or &H6
                    Case "RF7", "ALL ON"
                        byte1 = byte1 Or &H7
                End Select
                'If ComboBox7.Text = "Isolation" Then
                '    byte2 = byte2 Or &H08
                'Else
                '    If ComboBox7.Text = "ANT-RF1" Then
                '        byte2 = byte2 Or &H00
                '    Else
                '        If ComboBox7.Text = "ANT-RF2" Then
                '            byte2 = byte2 Or &H20
                '        Else
                '            If ComboBox7.Text = "ANT-RF3" Then
                '                byte2 = byte2 Or &H10
                '            Else
                '                If ComboBox7.Text = "ANT-RF4" Then
                '                    byte2 = byte2 Or &H30
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
                Select Case ComboBox7.Text
                    Case "RF8", "Isolation", "ALL OFF"
                        byte2 = byte2 Or &H0
                    Case "RF1"
                        byte2 = byte2 Or &H8
                    Case "RF2"
                        byte2 = byte2 Or &H10
                    Case "RF3"
                        byte2 = byte2 Or &H18
                    Case "RF4"
                        byte2 = byte2 Or &H20
                    Case "RF5", "RF1/2"
                        byte2 = byte2 Or &H28
                    Case "RF6", "RF3/4"
                        byte2 = byte2 Or &H30
                    Case "RF7", "ALL ON"
                        byte2 = byte2 Or &H38
                End Select
                'If ComboBox8.Text = "Isolation" Then
                '    byte2 = byte2 Or &H01
                'Else
                '    If ComboBox8.Text = "ANT-RF1" Then
                '        byte2 = byte2 Or &H00
                '    Else
                '        If ComboBox8.Text = "ANT-RF2" Then
                '            byte2 = byte2 Or &H04
                '        Else
                '            If ComboBox8.Text = "ANT-RF3" Then
                '                byte2 = byte2 Or &H02
                '            Else
                '                If ComboBox8.Text = "ANT-RF4" Then
                '                    byte2 = byte2 Or &H06
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
                Select Case ComboBox8.Text
                    Case "RF8", "Isolation", "ALL OFF"
                        byte2 = byte2 Or &H0
                    Case "RF1"
                        byte2 = byte2 Or &H1
                    Case "RF2"
                        byte2 = byte2 Or &H2
                    Case "RF3"
                        byte2 = byte2 Or &H3
                    Case "RF4"
                        byte2 = byte2 Or &H4
                    Case "RF5", "RF1/2"
                        byte2 = byte2 Or &H5
                    Case "RF6", "RF3/4"
                        byte2 = byte2 Or &H6
                    Case "RF7"
                        byte2 = byte2 Or &H7
                End Select
                test1 = CDbl(TextBox2.Text)
                test = CInt((test1 * 255.0) / 1.8)  'Equivalent DAC step value

                test2 = test >> 4
                test2 = test2 And &HC
                byte4 = byte4 Or test2                      'MSB (First 2 bits)

                test = test And &H3F
                byte3 = byte3 Or test                       'LSB (Last 6 bits)

                If ComboBox13.SelectedIndex = 0 Then
                    byte4 = byte4 Or &H10
                ElseIf ComboBox13.SelectedIndex = 1 Then
                    byte4 = byte4 Or &H20
                ElseIf ComboBox13.SelectedIndex = 2 Then
                    byte4 = byte4 Or &H30
                End If

                'If ComboBox9.Text = "OFF" Then
                '    byte3 = byte3 Or &H08
                'Else
                '    If ComboBox9.Text = "0.3V" Then
                '        byte3 = byte3 Or &H00
                '    Else
                '        If ComboBox9.Text = "0.5V" Then
                '            byte3 = byte3 Or &H20
                '        Else
                '            If ComboBox9.Text = "0.6V" Then
                '                byte3 = byte3 Or &H10
                '            Else
                '                If ComboBox9.Text = "0.7V" Then
                '                    byte3 = byte3 Or &H30
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
                If Toggle1.Checked = True Then
                    byte4 = byte4 Or &H2
                End If
                If Toggle2.Checked = True Then
                    byte4 = byte4 Or &H1
                End If
                Try
                    If PINGPIOToolStripMenuItem.Checked = True Then
                        NUMATO()
                        Thread.Sleep(5)
                        myserialPort2.Write("gpio writeall 00" & vbCr)  'BUG FIX FOR SKIPPING FIRST DATA
                        myserialPort2.ReadLine()
                        myserialPort2.ReadExisting()
                        Thread.Sleep(5)
                        myserialPort2.Write("gpio writeall " & byte1.ToString("X") & vbCr)
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace(">", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace(">", "")
                        Thread.Sleep(5)
                        myserialPort2.Write("gpio writeall " & byte2.ToString("X") & vbCr)
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace(">", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace(">", "")
                        Thread.Sleep(5)
                        myserialPort2.Write("gpio writeall " & byte3.ToString("X") & vbCr)
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace(">", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace(">", "")
                        Thread.Sleep(5)
                        myserialPort2.Write("gpio writeall " & byte4.ToString("X") & vbCr)
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace(">", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace(">", "")
                        Thread.Sleep(5)
                        'output = "gpio writeall " & byte1.ToString("X") & vbCr & "gpio writeall " & byte2.ToString("X") & vbCr & "gpio writeall " & byte3.ToString("X") & vbCr & "gpio writeall " & byte4.ToString("X") & vbCr
                        If Not ListBox1.Items.Count = 0 Then
                            For i = 0 To ListBox1.Items.Count - 1
                                If i Mod 2 = 0 Then
                                    test = &H80
                                Else
                                    test = &H0
                                End If
                                test = test Or ListBox1.Items.Item(i)
                                'output = output & "gpio writeall " & test.ToString("X") & vbCr
                                myserialPort2.Write("gpio writeall " & test.ToString("X") & vbCr)
                                RichTextBox1.Text &= myserialPort2.ReadLine().Replace(">", "")
                                RichTextBox1.Text &= myserialPort2.ReadExisting().Replace(">", "")
                                Thread.Sleep(5)
                            Next
                        End If
                        myserialPort2.Write("gpio writeall 00" & vbCr)  'CLEARING THE DATA LINES
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace(">", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace(">", "")
                        Thread.Sleep(5)
                        myserialPort2.Write(vbCr)
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace(">", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace(">", "")
                        'myserialPort2.Write(output & vbCr)
                        'RichTextBox1.Text &= myserialPort2.ReadLine().Replace(">", "")
                        'RichTextBox1.Text &= myserialPort2.ReadExisting().Replace(">", "")
                    Else
                        If SCOUTSC4410ToolStripMenuItem.Checked = True Then
                            SCOUTSC4410_Start()
                            myserialPort2.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
                        ElseIf SCOUTSC4415ToolStripMenuItem.Checked = True Then
                            SCOUTSC4415_Start()
                            myserialPort2.WriteLine("mode 1" & vbCrLf & "vio " & vio & vbCrLf & "clock 52000" & vbCrLf)
                        End If
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace("->", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace("->", "")
                        output = "rw 1 0x05 0x" & byte1.ToString("X") & vbCrLf & "rw 1 0x05 0x" & byte2.ToString("X") & vbCrLf & "rw 1 0x05 0x" & byte3.ToString("X") & vbCrLf & "rw 1 0x05 0x" & byte4.ToString("X") & vbCrLf
                        'myserialPort2.WriteLine("rw 1 0x05 0x" & byte1.ToString("X") & vbCrLf & "rw 1 0x05 0x" & byte2.ToString("X") & vbCrLf & "rw 1 0x05 0x" & byte3.ToString("X") & vbCrLf & "rw 1 0x05 0x" & byte4.ToString("X") & vbCrLf)
                        'RichTextBox1.Text &= myserialPort2.ReadLine().Replace("->", "")
                        'RichTextBox1.Text &= myserialPort2.ReadExisting().Replace("->", "")
                        'RichTextBox1.Text.Replace("->", "")
                        If Not ListBox1.Items.Count = 0 Then
                            For i = 0 To ListBox1.Items.Count - 1
                                If i Mod 2 = 0 Then
                                    test = &H80
                                Else
                                    test = &H0
                                End If
                                test = test Or ListBox1.Items.Item(i)
                                output = output & "rw 1 0x05 0x" & test.ToString("X") & vbCrLf
                                'myserialPort2.WriteLine("rw 1 0x05 0x" & test.ToString("X") & vbCrLf)
                                'RichTextBox1.Text &= myserialPort2.ReadLine().Replace("->", "")
                                'RichTextBox1.Text &= myserialPort2.ReadExisting().Replace("->", "")
                                'RichTextBox1.Text.Replace("->", "")
                            Next
                        End If
                        myserialPort2.WriteLine(output & "rw 1 0x05 0x00" & vbCrLf)
                        RichTextBox1.Text &= myserialPort2.ReadLine().Replace("->", "")
                        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace("->", "")
                    End If
                Catch ex As Exception
                    MetroFramework.MetroMessageBox.Show(Me, myserialPort2.PortName & " does not exist. Please open a valid COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
                    SerialReset()
                    Exit Sub
                End Try
                'End If
                'End If
                'End If
                'End If
                'End If
                'End If
                'End If
                'End If
                'End If
                'End If
                'End If
                'End If
            End If
            Try
                myserialPort2.Close()
            Catch ex As Exception
            End Try
            Application.DoEvents()  ' Give port time to close down
            Thread.Sleep(20)
        Loop Until Toggle3.Checked = False
    End Sub

    'Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
    '    SerialReset()
    '    RichTextBox1.Text &= "The active device has been disconnected" & vbCrLf & vbCrLf
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ResettoDefault()
    End Sub

    Sub ResettoDefault()
        'If device = 3 Then
        '    Button5.Enabled = False
        '    ComboBox3.Enabled = False
        '    ComboBox4.Enabled = False
        '    Button2.Enabled = True
        'Else
        '    Button5.Enabled = True
        '    ComboBox3.Enabled = True
        '    ComboBox4.Enabled = True
        '    Button2.Enabled = False
        'End If
        'RadioButton1.Enabled = True
        'RadioButton2.Enabled = True
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        ComboBox3.SelectedIndex = -1
        ComboBox4.SelectedIndex = -1
        ComboBox5.SelectedIndex = -1
        ComboBox6.SelectedIndex = -1
        'ComboBox7.SelectedIndex = -1
        'ComboBox8.SelectedIndex = -1
        ComboBox9.SelectedIndex = -1
        ComboBox10.SelectedIndex = -1
        'ComboBox11.SelectedIndex = -1
        'ComboBox12.SelectedIndex = -1
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        ComboBox5.Enabled = True
        ComboBox6.Enabled = True
        ComboBox9.Enabled = True
        ComboBox10.Enabled = True
        ComboBox13.SelectedIndex = -1
        ComboBox13.Enabled = True
        'ComboBox9.SelectedIndex = -1
        TextBox1.Text = ""
        TextBox1.WaterMark = "0 to 64, 96"
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        ListBox1.Items.Clear()
        Toggle1.Checked = False
        Toggle2.Checked = False
        PINGPIOToolStripMenuItem.Checked = False
        SCOUTSC4410ToolStripMenuItem.Checked = False
        SCOUTSC4415ToolStripMenuItem.Checked = False
        ConfigurationToolStripMenuItem.Enabled = False
        TypeASSCCXM3664XRToolStripMenuItem.Checked = False
        TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
        TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
        TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
        TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
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

    'Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
    '    If Button3.Enabled = True Then
    '        MetroFramework.MetroMessageBox.Show(Me, "Please open a Comm Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Else
    '        If RadioButton2.Checked = False And RadioButton1.Checked = False Then
    '            MetroFramework.MetroMessageBox.Show(Me, "The selected Comm Port is not suited for Engineering Mode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Else
    '            'If RadioButton1.Checked = True Then
    '            '    MetroFramework.MetroMessageBox.Show(Me, "GPIO Mode is currently under development", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            'Else
    '            If ComboBox3.Text = "" Then
    '                MetroFramework.MetroMessageBox.Show(Me, "Please select the LM8335 Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Else
    '                If ComboBox4.Text = "" Then
    '                    MetroFramework.MetroMessageBox.Show(Me, "Please select VIO voltage", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                Else
    '                    If ComboBox4.Text = "OFF/External" Then
    '                        vio = 0
    '                    Else
    '                        If ComboBox4.Text = "1.2" Then
    '                            vio = 1
    '                        Else
    '                            vio = 2
    '                        End If
    '                    End If
    '                    Try
    '                        myserialPort2.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
    '                        RichTextBox1.Text &= myserialPort2.ReadLine()
    '                        RichTextBox1.Text &= myserialPort2.ReadExisting()
    '                        'SerialPort1.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
    '                        'RichTextBox1.Text &= SerialPort1.ReadExisting()
    '                    Catch ex As Exception
    '                        MetroFramework.MetroMessageBox.Show(Me, myserialPort2.PortName & " does not exist. Please open a valid COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                        'MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
    '                        SerialReset()
    '                        Exit Sub
    '                    End Try
    '                    'Button5.Enabled = False
    '                    'RadioButton1.Enabled = False
    '                    'RadioButton2.Enabled = False
    '                    'ComboBox3.Enabled = False
    '                    'ComboBox4.Enabled = False
    '                    Button2.Enabled = True
    '                End If
    '            End If
    '            'End If
    '        End If
    '    End If
    'End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        RichTextBox1.Clear()
    End Sub

    'Private Sub FullScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullScreenToolStripMenuItem.Click
    '    If Me.WindowState = FormWindowState.Normal Then
    '        Me.WindowState = FormWindowState.Maximized
    '        FullScreenToolStripMenuItem.Checked = True
    '        fullscreen = True
    '    Else
    '        Me.WindowState = FormWindowState.Normal
    '        FullScreenToolStripMenuItem.Checked = False
    '        fullscreen = False
    '    End If
    '    SizeAdjust()
    'End Sub

    'Private Sub Form9_SizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.SizeChanged
    '    If Me.WindowState = FormWindowState.Normal Then
    '        FullScreenToolStripMenuItem.Checked = False
    '        fullscreen = False
    '    Else
    '        FullScreenToolStripMenuItem.Checked = True
    '        fullscreen = True
    '    End If
    '    SizeAdjust()
    'End Sub

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
            SCOUTSC4410ToolStripMenuItem.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1
            ComboBox6.SelectedIndex = -1
            'ComboBox7.SelectedIndex = -1
            'ComboBox8.SelectedIndex = -1
            ComboBox9.SelectedIndex = -1
            ComboBox10.SelectedIndex = -1
            'ComboBox11.SelectedIndex = -1
            'ComboBox12.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            'ComboBox9.SelectedIndex = -1
            TextBox1.Text = ""
            TextBox1.WaterMark = "0 to 64, 96"
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            ListBox1.Items.Clear()
            Toggle1.Checked = False
            Toggle2.Checked = False
            ConfigurationToolStripMenuItem.Enabled = False
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        Else
            SCOUTSC4410ToolStripMenuItem.Checked = True
            PINGPIOToolStripMenuItem.Checked = False
            SCOUTSC4415ToolStripMenuItem.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = True
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1
            ComboBox6.SelectedIndex = -1
            'ComboBox7.SelectedIndex = -1
            'ComboBox8.SelectedIndex = -1
            ComboBox9.SelectedIndex = -1
            ComboBox10.SelectedIndex = -1
            'ComboBox11.SelectedIndex = -1
            'ComboBox12.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            'ComboBox9.SelectedIndex = -1
            TextBox1.Text = ""
            TextBox1.WaterMark = "0 to 64, 96"
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            ListBox1.Items.Clear()
            Toggle1.Checked = False
            Toggle2.Checked = False
            ConfigurationToolStripMenuItem.Enabled = True
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        End If
        'If SCOUTSC4410ToolStripMenuItem.Checked = True Then
        '    SerialReset()
        '    RichTextBox1.Text &= "SCOUT SC4410 has been disconnected" & vbCrLf & vbCrLf
        'Else
        '    'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\5&1c8c57d1&0&1\Device Parameters", "PortName", Nothing)
        '    'For Each Str As String In myPort
        '    '    If Str.Contains(readValue) Then
        '    '        ScoutON(readValue)
        '    '    End If
        '    'Next
        '    'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\5&1c8c57d1&0&2\Device Parameters", "PortName", Nothing)
        '    'For Each Str As String In myPort
        '    '    If Str.Contains(readValue) Then
        '    '        ScoutON(readValue)
        '    '    End If
        '    'Next
        '    'readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Enum\USB\VID_173C&PID_0002\6&2a9e2b2e&0&3\Device Parameters", "PortName", Nothing)
        '    'For Each Str As String In myPort
        '    '    If Str.Contains(readValue) Then
        '    '        ScoutON(readValue)
        '    '    End If
        '    'Next
        '    Dim x As New ComPortFinder
        '    Try
        '        Dim list = x.ComPortNames("173C", "0002")
        '        For Each item As String In list
        '            For Each Str As String In myPort
        '                If Str.Contains(item) Then
        '                    ScoutON(item)
        '                End If
        '            Next
        '        Next
        '    Catch ex As Exception
        '        SerialReset()
        '        RichTextBox1.Text &= "SCOUT SC4410 has been disconnected" & vbCrLf & vbCrLf
        '    End Try
        'End If
    End Sub

    Function SCOUTSC4410_Start()
        myPort = IO.Ports.SerialPort.GetPortNames()
        Dim x As New ComPortFinder
        Try
            Dim list = x.ComPortNames("173C", "0002")
            For Each item As String In list
                If item <> Nothing Then
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            ScoutON(item)
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            SerialReset()
            RichTextBox1.Text &= "SCOUT SC4410 has been disconnected" & vbCrLf & vbCrLf
        End Try
        Return 0
    End Function

    Function SCOUTSC4415_Start()
        myPort = IO.Ports.SerialPort.GetPortNames()
        Dim x As New ComPortFinder
        Try
            Dim list = x.ComPortNames("173C", "0003")
            For Each item As String In list
                If item <> Nothing Then
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            ScoutON2(item)
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            SerialReset()
            RichTextBox1.Text &= "SCOUT SC4415 has been disconnected" & vbCrLf & vbCrLf
        End Try
        Return 0
    End Function

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
        SCOUTSC4415ToolStripMenuItem.Checked = False
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
        'Button3.Enabled = False
        'Button4.Enabled = True
        'Button5.Enabled = True
        'ComboBox1.Enabled = False
        'ComboBox2.Enabled = False
        'ComboBox1.SelectedIndex = -1
        'ComboBox2.SelectedIndex = -1
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        RadioButton1.Checked = False
        RadioButton2.Checked = True
        'RadioButton3.Checked = False
        device = 1
        RichTextBox1.Text &= "Active device selected as SCOUT SC4410" & vbCrLf & "Port Name: " & readvalue1 & "; Baud Rate: 115200" & vbCrLf & vbCrLf
        Return 0
    End Function

    Function ScoutON2(ByVal readvalue1 As String)
        Try
            myserialPort2.Close()
            'SerialPort1.Close()
        Catch ex As Exception
        End Try
        Application.DoEvents()  ' Give port time to close down
        Thread.Sleep(200)
        SCOUTSC4410ToolStripMenuItem.Checked = False
        'SierraWirelessToolStripMenuItem.Checked = False
        PINGPIOToolStripMenuItem.Checked = False
        SCOUTSC4415ToolStripMenuItem.Checked = True
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
        'Button3.Enabled = False
        'Button4.Enabled = True
        'Button5.Enabled = True
        'ComboBox1.Enabled = False
        'ComboBox2.Enabled = False
        'ComboBox1.SelectedIndex = -1
        'ComboBox2.SelectedIndex = -1
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        RadioButton1.Checked = False
        RadioButton2.Checked = True
        'RadioButton3.Checked = False
        device = 1
        RichTextBox1.Text &= "Active device selected as SCOUT SC4415" & vbCrLf & "Port Name: " & readvalue1 & "; Baud Rate: 115200" & vbCrLf & vbCrLf
        Return 0
    End Function

    Private Sub PINGPIOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PINGPIOToolStripMenuItem.Click
        If PINGPIOToolStripMenuItem.Checked = True Then
            PINGPIOToolStripMenuItem.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1
            ComboBox6.SelectedIndex = -1
            'ComboBox7.SelectedIndex = -1
            'ComboBox8.SelectedIndex = -1
            ComboBox9.SelectedIndex = -1
            ComboBox10.SelectedIndex = -1
            'ComboBox11.SelectedIndex = -1
            'ComboBox12.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            'ComboBox9.SelectedIndex = -1
            TextBox1.Text = ""
            TextBox1.WaterMark = "0 to 64, 96"
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            ListBox1.Items.Clear()
            Toggle1.Checked = False
            Toggle2.Checked = False
            ConfigurationToolStripMenuItem.Enabled = False
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        Else
            PINGPIOToolStripMenuItem.Checked = True
            SCOUTSC4410ToolStripMenuItem.Checked = False
            SCOUTSC4415ToolStripMenuItem.Checked = False
            RadioButton1.Checked = True
            RadioButton2.Checked = False
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1
            ComboBox6.SelectedIndex = -1
            'ComboBox7.SelectedIndex = -1
            'ComboBox8.SelectedIndex = -1
            ComboBox9.SelectedIndex = -1
            ComboBox10.SelectedIndex = -1
            'ComboBox11.SelectedIndex = -1
            'ComboBox12.SelectedIndex = -1
            ComboBox3.Enabled = False
            ComboBox4.Enabled = False
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            'ComboBox9.SelectedIndex = -1
            TextBox1.Text = ""
            TextBox1.WaterMark = "0 to 64, 96"
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            ListBox1.Items.Clear()
            Toggle1.Checked = False
            Toggle2.Checked = False
            ConfigurationToolStripMenuItem.Enabled = True
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        End If
        'If PINGPIOToolStripMenuItem.Checked = True Then
        '    SerialReset()
        '    RichTextBox1.Text &= "GPIO has been disconnected" & vbCrLf & vbCrLf
        'Else
        '    Dim x As New ComPortFinder
        '    Try
        '        Dim list = x.ComPortNames("2A19", "0800")
        '        For Each item As String In list
        '            For Each Str As String In myPort
        '                If Str.Contains(item) Then
        '                    GPIOON(item)
        '                End If
        '            Next
        '        Next
        '    Catch ex As Exception
        '        SerialReset()
        '        RichTextBox1.Text &= "GPIO has been disconnected" & vbCrLf & vbCrLf
        '    End Try
        'End If
    End Sub

    Function NUMATO()
        'If PINGPIOToolStripMenuItem.Checked = True Then
        '    SerialReset()
        '    RichTextBox1.Text &= "GPIO has been disconnected" & vbCrLf & vbCrLf
        'Else
        myPort = IO.Ports.SerialPort.GetPortNames()
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
        Return 0
        'End If
    End Function

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
        SCOUTSC4415ToolStripMenuItem.Checked = False
        'SerialPort1.PortName = readvalue1
        'SerialPort1.BaudRate = 115200
        'SerialPort1.Parity = Parity.None
        'SerialPort1.DataBits = 8
        'SerialPort1.StopBits = StopBits.One
        'SerialPort1.Open()
        myserialPort2.PortName = readvalue1
        myserialPort2.BaudRate = 115200 '9600
        myserialPort2.Parity = Parity.None
        myserialPort2.DataBits = 8
        myserialPort2.StopBits = StopBits.One
        myserialPort2.Open()
        'Button2.Enabled = True
        'Button3.Enabled = False
        'Button4.Enabled = True
        'Button5.Enabled = False
        'ComboBox1.Enabled = False
        'ComboBox2.Enabled = False
        'ComboBox1.SelectedIndex = -1
        'ComboBox2.SelectedIndex = -1
        ComboBox3.Enabled = False
        ComboBox4.Enabled = False
        RadioButton1.Checked = True
        RadioButton2.Checked = False
        'RadioButton3.Checked = False
        device = 3
        RichTextBox1.Text &= "Active device selected as GPIO" & vbCrLf & "Port Name: " & readvalue1 & "; Baud Rate: 9600" & vbCrLf & vbCrLf
        myserialPort2.Write("gpio iodir 00" & vbCrLf)
        RichTextBox1.Text &= myserialPort2.ReadLine().Replace(">", "")
        RichTextBox1.Text &= myserialPort2.ReadExisting().Replace(">", "")
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
            BackgroundWorker1.Dispose()
        End If
    End Sub

    Function SizeAdjust()
        Dim screen As Screen = Screen.FromControl(Me)
        Dim intX As Integer = screen.WorkingArea.Width
        Dim intY As Integer = screen.WorkingArea.Height
        If Me.WindowState = FormWindowState.Maximized Then
            Label15.Location = New Point(x:=(intX / 8), y:=(intY * 1.6 / 13))
            Label4.Location = New Point(x:=(intX / 8), y:=(intY * 2.25 / 13))
            Label11.Location = New Point(x:=(intX / 8), y:=(intY * 2.9 / 13))
            Label10.Location = New Point(x:=(intX / 8), y:=(intY * 3.55 / 13))
            Label9.Location = New Point(x:=(intX / 8), y:=(intY * 4.2 / 13))
            Label16.Location = New Point(x:=(intX / 8), y:=(intY * 4.85 / 13))
            Label5.Location = New Point(x:=(intX / 8), y:=(intY * 5.5 / 13))
            Label6.Location = New Point(x:=(intX / 8), y:=(intY * 6.15 / 13))
            Label7.Location = New Point(x:=(intX / 8), y:=(intY * 6.8 / 13))
            Label19.Location = New Point(x:=(intX / 8), y:=(intY * 7.45 / 13))
            Label20.Location = New Point(x:=(intX / 8), y:=(intY * 8.75 / 13))
            Label21.Location = New Point(x:=(intX / 8), y:=(intY * 9.4 / 13))
            Label8.Location = New Point(x:=(intX / 8), y:=(intY * 10.05 / 13))
            Button1.Location = New Point(x:=(intX / 8), y:=(intY * 11 / 13))
            Button2.Location = New Point(x:=(intX / 3.5), y:=(intY * 11 / 13))
            ComboBox1.Location = New Point(x:=(intX / 3.5), y:=(intY * 1.6 / 13))
            ComboBox2.Location = New Point(x:=(intX / 3.5), y:=(intY * 2.25 / 13))
            Label12.Location = New Point(x:=((intX / 3.5)), y:=(intY * 2.9 / 13))
            Label13.Location = New Point(x:=((intX / 3.5) + 103), y:=(intY * 2.9 / 13))
            Label18.Location = New Point(x:=((intX / 3.5) + 197), y:=(intY * 2.9 / 13))
            RadioButton1.Location = New Point(x:=((intX / 3.5) + 53), y:=((intY * 2.9 / 13) + 3))
            RadioButton2.Location = New Point(x:=((intX / 3.5) + 147), y:=((intY * 2.9 / 13) + 3))
            RadioButton3.Location = New Point(x:=((intX / 3.5) + 250), y:=((intY * 2.9 / 13) + 3))
            ComboBox3.Location = New Point(x:=(intX / 3.5), y:=(intY * 3.55 / 13))
            ComboBox4.Location = New Point(x:=(intX / 3.5), y:=(intY * 4.2 / 13))
            ComboBox9.Location = New Point(x:=(intX / 3.5), y:=(intY * 4.85 / 13))
            ComboBox10.Location = New Point(x:=(intX / 3.5), y:=(intY * 5.5 / 13))
            ComboBox11.Location = New Point(x:=(intX / 3.5), y:=(intY * 6.15 / 13))
            ComboBox12.Location = New Point(x:=(intX / 3.5), y:=(intY * 6.8 / 13))
            ComboBox5.Location = New Point(x:=((intX / 3.5) + 136), y:=(intY * 4.85 / 13))
            ComboBox6.Location = New Point(x:=((intX / 3.5) + 136), y:=(intY * 5.5 / 13))
            ComboBox7.Location = New Point(x:=((intX / 3.5) + 136), y:=(intY * 6.15 / 13))
            ComboBox8.Location = New Point(x:=((intX / 3.5) + 136), y:=(intY * 6.8 / 13))
            TextBox1.Location = New Point(x:=(intX / 3.5), y:=(intY * 7.45 / 13))
            TextBox3.Location = New Point(x:=(intX / 3.5), y:=(intY * 8.1 / 13))
            'Button7.Location = New Point(x:=((intX / 3.5) + 61), y:=(intY * 7.45 / 13))
            'Button8.Location = New Point(x:=((intX / 3.5) + 61), y:=(intY * 8.1 / 13))
            'ListBox1.Location = New Point(x:=((intX / 3.5) + 146), y:=(intY * 7.45 / 13))
            'ListBox1.Size = New Size(width:=119, height:=((intY * (8.0769 - 7.45) / 13) + 30))

            Button7.Location = New Point(x:=((intX / 3.5) + 80), y:=(intY * 7.45 / 13))
            Button8.Location = New Point(x:=((intX / 3.5) + 80), y:=(intY * 8.1 / 13))
            ListBox1.Location = New Point(x:=((intX / 3.5) + 165), y:=(intY * 7.45 / 13))
            ListBox1.Size = New Size(width:=100, height:=((intY * (8.0769 - 7.45) / 13) + 31))

            Toggle1.Location = New Point(x:=(intX / 3.5), y:=(intY * 8.75 / 13))
            Toggle2.Location = New Point(x:=(intX / 3.5), y:=(intY * 9.4 / 13))
            'ComboBox9.Location = New Point(x:=(intX / 3.5), y:=(intY * 10.05 / 13))
            TextBox2.Location = New Point(x:=(intX / 3.5), y:=(intY * 10.05 / 13))
            Label22.Location = New Point(x:=((intX / 3.5) + 130), y:=((intY * 11 / 13) + 9))
            Toggle3.Location = New Point(x:=((intX / 3.5) + 185), y:=((intY * 11 / 13) + 11))
            Button3.Location = New Point(x:=(intX / 1.9), y:=(intY * 1.6 / 13))
            Button4.Location = New Point(x:=(intX / 1.29), y:=(intY * 1.6 / 13))
            Button5.Location = New Point(x:=(intX / 1.9), y:=(intY * 2.9 / 13))
            Label14.Location = New Point(x:=(intX / 1.9), y:=((intY * 4.2 / 13) - 12))
            RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=((intY * 4.85 / 13) - 14))
            RichTextBox1.Size = New Size(width:=((intX / 1.29) - (intX / 1.9) + 119), height:=((intY * 10.05 / 13) - (intY * 4.85 / 13) + 25 + 14))
            Button6.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 13))
        Else
            Me.Size = New Size(976, 727)
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
            ComboBox9.Location = New Point(x:=273, y:=287)
            ComboBox10.Location = New Point(x:=273, y:=324)
            ComboBox11.Location = New Point(x:=273, y:=361)
            ComboBox12.Location = New Point(x:=273, y:=398)
            ComboBox5.Location = New Point(x:=409, y:=287)
            ComboBox6.Location = New Point(x:=409, y:=324)
            ComboBox7.Location = New Point(x:=409, y:=361)
            ComboBox8.Location = New Point(x:=409, y:=398)
            TextBox1.Location = New Point(x:=273, y:=435)
            TextBox3.Location = New Point(x:=273, y:=466)
            Button7.Location = New Point(x:=360, y:=435)
            Button8.Location = New Point(x:=360, y:=466)
            ListBox1.Location = New Point(x:=452, y:=435)
            ListBox1.Size = New Size(width:=87, height:=56)
            Toggle1.Location = New Point(x:=273, y:=511)
            Toggle2.Location = New Point(x:=273, y:=548)
            'ComboBox9.Location = New Point(x:=273, y:=583)
            TextBox2.Location = New Point(x:=273, y:=583)
            Label22.Location = New Point(x:=403, y:=641)
            Toggle3.Location = New Point(x:=458, y:=643)
            Button3.Location = New Point(x:=591, y:=102)
            Button4.Location = New Point(x:=745, y:=102)
            Button5.Location = New Point(x:=591, y:=176)
            Label14.Location = New Point(x:=591, y:=238)
            RichTextBox1.Location = New Point(x:=591, y:=273)
            RichTextBox1.Size = New Size(width:=273, height:=335)
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
        'Button2.Enabled = False
        'Button3.Enabled = True
        'Button4.Enabled = False
        'Button5.Enabled = True
        'ComboBox1.Enabled = True
        'ComboBox2.Enabled = True
        ComboBox3.Enabled = True
        ComboBox4.Enabled = True
        'RadioButton1.Enabled = True
        'RadioButton2.Enabled = True
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        ComboBox5.Enabled = True
        ComboBox6.Enabled = True
        'ComboBox7.Enabled = True
        'ComboBox8.Enabled = True
        ComboBox9.Enabled = True
        ComboBox10.Enabled = True
        'ComboBox11.Enabled = True
        'ComboBox12.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox3.Text = ""
        Toggle1.Enabled = True
        Toggle2.Enabled = True
        'ComboBox1.Items.Clear()
        myPort = IO.Ports.SerialPort.GetPortNames()
        'ComboBox1.Items.AddRange(myPort)
        'If ComboBox1.Items.Count = 0 Then
        '    ComboBox1.Items.Add("                   No ComPorts detected")
        '    ComboBox1.SelectedIndex = 0
        'Else
        '    ComboBox1.SelectedIndex = -1
        'End If
        'ComboBox1.DroppedDown = False
        SCOUTSC4410ToolStripMenuItem.Checked = False
        'SierraWirelessToolStripMenuItem.Checked = False
        PINGPIOToolStripMenuItem.Checked = False
        SCOUTSC4415ToolStripMenuItem.Checked = False
        SCOUTSC4410ToolStripMenuItem.Enabled = False
        'SierraWirelessToolStripMenuItem.Enabled = False
        PINGPIOToolStripMenuItem.Enabled = False
        SCOUTSC4415ToolStripMenuItem.Enabled = False
        device = 0
        Dim x As New ComPortFinder
        Dim list As List(Of String)
        'Try
        list = x.ComPortNames("173C", "0002")
        For Each item As String In list
            If item <> Nothing Then
                For Each Str As String In myPort
                    If Str.Contains(item) Then
                        SCOUTSC4410ToolStripMenuItem.Enabled = True
                    End If
                Next
            End If
        Next
        'Catch ex As Exception
        'End Try
        list = x.ComPortNames("2A19", "0800")
        For Each item As String In list
            If item <> Nothing Then
                For Each Str As String In myPort
                    If Str.Contains(item) Then
                        PINGPIOToolStripMenuItem.Enabled = True
                    End If
                Next
            End If
        Next
        list = x.ComPortNames("173C", "0003")
        For Each item As String In list
            If item <> Nothing Then
                For Each Str As String In myPort
                    If Str.Contains(item) Then
                        SCOUTSC4415ToolStripMenuItem.Enabled = True
                    End If
                Next
            End If
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
            If TextBox1.WaterMark = "0 to 64, 96" Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-64 or 96 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBox1.WaterMark = "0 to 16, 24" Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-16 or 24 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBox1.WaterMark = "0 to 15" Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-15 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Exit Sub
        End Try
        test = CInt(TextBox1.Text)
        If (test >= 0) Then
            If ((TextBox1.WaterMark = "0 to 64, 96") AndAlso (test > 64) AndAlso (test <> 96)) Then
                MetroFramework.MetroMessageBox.Show(Me, "Invalid state. Please enter an integer between 0-64 or 96 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf ((TextBox1.WaterMark = "0 to 16, 24") AndAlso (test > 16) AndAlso (test <> 24)) Then
                MetroFramework.MetroMessageBox.Show(Me, "Invalid state. Please enter an integer between 0-16 or 24 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf ((TextBox1.WaterMark = "0 to 15") AndAlso (test > 15)) Then
                MetroFramework.MetroMessageBox.Show(Me, "Invalid state. Please enter an integer between 0-15 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If (TextBox3.Text = "") Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter the number of RFFE MIPI values to be programmed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                Try
                    test2 = CInt(TextBox3.Text)
                Catch ex As Exception
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter a number to denote the number of RFFE MIPI values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End Try
                If (test2 <= ListBox1.Items.Count) Then
                    MetroFramework.MetroMessageBox.Show(Me, "The number of RFFE MIPI values have exceeded the quantity provided. Please reconfirm the number of RFFE MIPI values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TextBox1.Text = ""
                    TextBox1.SelectionStart = 0
                    TextBox1.SelectionLength = 0
                    TextBox1.Focus()
                    Exit Sub
                End If
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
            If TextBox1.WaterMark = "0 to 64, 96" Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-64 or 96 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBox1.WaterMark = "0 to 16, 24" Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-16 or 24 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ElseIf TextBox1.WaterMark = "0 to 15" Then
                MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-15 for the corresponding state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
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

    Private Sub Application_Idle(ByVal sender As Object, ByVal e As EventArgs)
        If Not BackgroundWorker1.IsBusy Then
            BackgroundWorker1.RunWorkerAsync()
        End If
        'If Button3.Enabled = True Then
        '    testPort = IO.Ports.SerialPort.GetPortNames()
        '    If Not testPort.Length = myPort.Length Then
        '        SerialReset()
        '        Exit Sub
        '    End If
        '    test = 0
        '    For Each item1 As String In testPort
        '        For Each item2 As String In myPort
        '            Dim res As Int16 = String.Compare(item1, item2) ' compare items
        '            If res = 0 Then 'the items are equal
        '                test += 1
        '            End If
        '        Next
        '    Next
        '    If Not test = myPort.Length Then
        '        SerialReset()
        '    End If
        'End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork  ' Do some time-consuming work on this thread.
        Try
            myPort = IO.Ports.SerialPort.GetPortNames()
            Dim x As New ComPortFinder
            Dim list As List(Of String)

            list = x.ComPortNames("173C", "0002")
            foundit = 0
            For Each item As String In list
                If item <> Nothing Then
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            SCOUTSC4410ToolStripMenuItem.Enabled = True
                            foundit = 1
                        End If
                    Next
                End If
            Next
            If foundit = 0 Then
                If SCOUTSC4410ToolStripMenuItem.Checked = True Then
                    ResettoDefault()
                End If
                SCOUTSC4410ToolStripMenuItem.Enabled = False
                SCOUTSC4410ToolStripMenuItem.Checked = False
            End If
            list = x.ComPortNames("2A19", "0800")
            foundit = 0
            For Each item As String In list
                If item <> Nothing Then
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            PINGPIOToolStripMenuItem.Enabled = True
                            foundit = 1
                        End If
                    Next
                End If
            Next
            If foundit = 0 Then
                If PINGPIOToolStripMenuItem.Checked = True Then
                    ResettoDefault()
                End If
                PINGPIOToolStripMenuItem.Enabled = False
                PINGPIOToolStripMenuItem.Checked = False
            End If
            list = x.ComPortNames("173C", "0003")
            foundit = 0
            For Each item As String In list
                If item <> Nothing Then
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            SCOUTSC4415ToolStripMenuItem.Enabled = True
                            foundit = 1
                        End If
                    Next
                End If
            Next
            If foundit = 0 Then
                If SCOUTSC4415ToolStripMenuItem.Checked = True Then
                    ResettoDefault()
                End If
                SCOUTSC4415ToolStripMenuItem.Enabled = False
                SCOUTSC4415ToolStripMenuItem.Checked = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ComboBox9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox9.SelectedIndexChanged
        If ComboBox9.Text = "SP2T" Then
            ComboBox5.Items.Clear()
            ComboBox5.Items.AddRange(New String() {"RF1", "RF2"})
        Else
            If ComboBox9.Text = "SP3T" Then
                ComboBox5.Items.Clear()
                ComboBox5.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3"})
            Else
                If ComboBox9.Text = "SP4T" Then
                    ComboBox5.Items.Clear()
                    ComboBox5.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4"})
                Else
                    If ComboBox9.Text = "SP5T" Then
                        ComboBox5.Items.Clear()
                        ComboBox5.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5"})
                    Else
                        If ComboBox9.Text = "SP6T" Then
                            ComboBox5.Items.Clear()
                            ComboBox5.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5", "RF6"})
                        Else
                            If ComboBox9.Text = "SP7T" Then
                                ComboBox5.Items.Clear()
                                ComboBox5.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5", "RF6", "RF7"})
                            Else
                                ComboBox5.Items.Clear()
                                ComboBox5.Items.AddRange(New String() {"RF1", "RF2", "RF3", "RF4", "RF5", "RF6", "RF7", "RF8"})
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ComboBox10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox10.SelectedIndexChanged
        If ComboBox10.Text = "SP2T" Then
            ComboBox6.Items.Clear()
            ComboBox6.Items.AddRange(New String() {"RF1", "RF2"})
        Else
            If ComboBox10.Text = "SP3T" Then
                ComboBox6.Items.Clear()
                ComboBox6.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3"})
            Else
                If ComboBox10.Text = "SP4T" Then
                    ComboBox6.Items.Clear()
                    ComboBox6.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4"})
                Else
                    If ComboBox10.Text = "SP5T" Then
                        ComboBox6.Items.Clear()
                        ComboBox6.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5"})
                    Else
                        If ComboBox10.Text = "SP6T" Then
                            ComboBox6.Items.Clear()
                            ComboBox6.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5", "RF6"})
                        Else
                            If ComboBox10.Text = "SP7T" Then
                                ComboBox6.Items.Clear()
                                ComboBox6.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5", "RF6", "RF7"})
                            Else
                                ComboBox6.Items.Clear()
                                ComboBox6.Items.AddRange(New String() {"RF1", "RF2", "RF3", "RF4", "RF5", "RF6", "RF7", "RF8"})
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ComboBox11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox11.SelectedIndexChanged
        If ComboBox11.Text = "SP2T" Then
            ComboBox7.Items.Clear()
            ComboBox7.Items.AddRange(New String() {"RF1", "RF2"})
        Else
            If ComboBox11.Text = "SP3T" Then
                ComboBox7.Items.Clear()
                ComboBox7.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3"})
            Else
                If ComboBox11.Text = "SP4T" Then
                    ComboBox7.Items.Clear()
                    ComboBox7.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4"})
                Else
                    If ComboBox11.Text = "SP5T" Then
                        ComboBox7.Items.Clear()
                        ComboBox7.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5"})
                    Else
                        If ComboBox11.Text = "SP6T" Then
                            ComboBox7.Items.Clear()
                            ComboBox7.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5", "RF6"})
                        Else
                            If ComboBox11.Text = "SP7T" Then
                                ComboBox7.Items.Clear()
                                ComboBox7.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5", "RF6", "RF7"})
                            Else
                                ComboBox7.Items.Clear()
                                ComboBox7.Items.AddRange(New String() {"RF1", "RF2", "RF3", "RF4", "RF5", "RF6", "RF7", "RF8"})
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ComboBox12_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox12.SelectedIndexChanged
        If ComboBox12.Text = "SP2T" Then
            ComboBox8.Items.Clear()
            ComboBox8.Items.AddRange(New String() {"RF1", "RF2"})
        Else
            If ComboBox12.Text = "SP3T" Then
                ComboBox8.Items.Clear()
                ComboBox8.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3"})
            Else
                If ComboBox12.Text = "SP4T" Then
                    ComboBox8.Items.Clear()
                    ComboBox8.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4"})
                Else
                    If ComboBox12.Text = "SP5T" Then
                        ComboBox8.Items.Clear()
                        ComboBox8.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5"})
                    Else
                        If ComboBox12.Text = "SP6T" Then
                            ComboBox8.Items.Clear()
                            ComboBox8.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5", "RF6"})
                        Else
                            If ComboBox12.Text = "SP7T" Then
                                ComboBox8.Items.Clear()
                                ComboBox8.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4", "RF5", "RF6", "RF7"})
                            Else
                                ComboBox8.Items.Clear()
                                ComboBox8.Items.AddRange(New String() {"RF1", "RF2", "RF3", "RF4", "RF5", "RF6", "RF7", "RF8"})
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "6 (UID = Low); SONY CXM3664XR" Or ComboBox3.Text = "7 (UID - High); SONY CXM3664XR" Then
            ComboBox5.Enabled = False
            ComboBox6.Enabled = False
            'ComboBox7.Enabled = False
            'ComboBox8.Enabled = False
            ComboBox9.Enabled = False
            ComboBox10.Enabled = False
            'ComboBox11.Enabled = False
            'ComboBox12.Enabled = False
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox3.Text = "1"
            Toggle1.Enabled = False
            Toggle2.Enabled = False
        Else
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            'ComboBox7.Enabled = True
            'ComboBox8.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            'ComboBox11.Enabled = True
            'ComboBox12.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox3.Text = ""
            Toggle1.Enabled = True
            Toggle2.Enabled = True
        End If
    End Sub

    Private Sub TypeASSCCXM3664XRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TypeASSCCXM3664XRToolStripMenuItem.Click
        If TypeASSCCXM3664XRToolStripMenuItem.Checked = False Then

            If PINGPIOToolStripMenuItem.Checked = True Then
                ComboBox3.SelectedIndex = -1
                ComboBox3.Enabled = False
                ComboBox4.SelectedIndex = -1
                ComboBox4.Enabled = False
            Else
                ComboBox3.SelectedIndex = 2
                ComboBox3.Enabled = True
                ComboBox4.SelectedIndex = 2
                ComboBox4.Enabled = True
            End If

            ComboBox5.Enabled = False
            ComboBox6.Enabled = False
            'ComboBox7.Enabled = False
            'ComboBox8.Enabled = False
            ComboBox9.Enabled = False
            ComboBox10.Enabled = False
            'ComboBox11.Enabled = False
            'ComboBox12.Enabled = False
            ComboBox13.SelectedIndex = 0
            ComboBox13.Enabled = False
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox2.Text = "0"
            TextBox3.Text = "1"
            Toggle1.Enabled = False
            Toggle2.Enabled = False
            ListBox1.Items.Clear()
            TypeASSCCXM3664XRToolStripMenuItem.Checked = True
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        Else
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            'ComboBox7.Enabled = True
            'ComboBox8.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            'ComboBox11.Enabled = True
            'ComboBox12.Enabled = True
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            TextBox2.Text = ""
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox3.Text = ""
            ListBox1.Items.Clear()
            Toggle1.Enabled = True
            Toggle2.Enabled = True
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub TypeBSP4TCXA4447GToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TypeBSP4TCXA4447GToolStripMenuItem.Click
        If TypeBSP4TCXA4447GToolStripMenuItem.Checked = False Then

            If PINGPIOToolStripMenuItem.Checked = True Then
                ComboBox3.SelectedIndex = -1
                ComboBox3.Enabled = False
                ComboBox4.SelectedIndex = -1
                ComboBox4.Enabled = False
            Else
                ComboBox3.SelectedIndex = 0 'EVB02 LM8335 ADR is grounded
                ComboBox3.Enabled = True
                ComboBox4.SelectedIndex = 2
                ComboBox4.Enabled = True
            End If

            ComboBox5.Enabled = True
            ComboBox6.Enabled = False
            'ComboBox7.Enabled = False
            'ComboBox8.Enabled = False
            ComboBox9.Enabled = False
            ComboBox9.SelectedIndex = 2
            ComboBox5.Items.Clear()
            ComboBox5.Items.AddRange(New String() {"RF1", "RF2", "RF3", "RF4", "RF1/2", "RF3/4", "ALL ON", "ALL OFF"})
            ComboBox10.Enabled = False
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = False
            'ComboBox11.Enabled = False
            'ComboBox12.Enabled = False
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox2.Text = "0"
            TextBox3.Text = "1"
            ListBox1.Items.Clear()
            ListBox1.Items.Add("0")
            Toggle1.Enabled = False
            Toggle2.Enabled = False
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = True
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        Else
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            'ComboBox7.Enabled = True
            'ComboBox8.Enabled = True
            ComboBox9.SelectedIndex = -1
            ComboBox9.Enabled = True
            ComboBox5.Items.Clear()
            ComboBox5.Items.AddRange(New String() {"Isolation", "RF1", "RF2", "RF3", "RF4"})
            ComboBox5.SelectedIndex = -1
            ComboBox10.Enabled = True
            'ComboBox11.Enabled = True
            'ComboBox12.Enabled = True
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox2.Text = ""
            TextBox3.Text = ""
            ListBox1.Items.Clear()
            Toggle1.Enabled = True
            Toggle2.Enabled = True
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Click
        If TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False Then

            If PINGPIOToolStripMenuItem.Checked = True Then
                ComboBox3.SelectedIndex = -1
                ComboBox3.Enabled = False
                ComboBox4.SelectedIndex = -1
                ComboBox4.Enabled = False
            Else
                ComboBox3.SelectedIndex = 0 'EVB02 LM8335 ADR is grounded
                ComboBox3.Enabled = True
                ComboBox4.SelectedIndex = 2
                ComboBox4.Enabled = True
            End If

            ComboBox5.Enabled = True
            ComboBox6.Enabled = False
            'ComboBox7.Enabled = False
            'ComboBox8.Enabled = False
            ComboBox9.Enabled = False
            ComboBox10.Enabled = False
            'ComboBox11.Enabled = False
            'ComboBox12.Enabled = False
            ComboBox9.SelectedIndex = 0
            ComboBox5.SelectedIndex = 0
            ComboBox13.SelectedIndex = 2
            ComboBox13.Enabled = False
            TextBox1.WaterMark = "0 to 16, 24"
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox2.Text = "0"
            TextBox3.Text = "1"
            ListBox1.Items.Clear()
            Toggle1.Enabled = False
            Toggle2.Enabled = False
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = True
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        Else
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1
            ComboBox9.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            'ComboBox7.Enabled = True
            'ComboBox8.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            'ComboBox11.Enabled = True
            'ComboBox12.Enabled = True
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            TextBox1.WaterMark = "0 to 64, 96"
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox2.Text = ""
            TextBox3.Text = ""
            ListBox1.Items.Clear()
            Toggle1.Enabled = True
            Toggle2.Enabled = True
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Click
        If TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False Then

            If PINGPIOToolStripMenuItem.Checked = True Then
                ComboBox3.SelectedIndex = -1
                ComboBox3.Enabled = False
                ComboBox4.SelectedIndex = -1
                ComboBox4.Enabled = False
            Else
                ComboBox3.SelectedIndex = 0 'EVB02 LM8335 ADR is grounded
                ComboBox3.Enabled = True
                ComboBox4.SelectedIndex = 2
                ComboBox4.Enabled = True
            End If

            ComboBox5.Enabled = True
            ComboBox6.Enabled = False
            'ComboBox7.Enabled = False
            'ComboBox8.Enabled = False
            ComboBox9.Enabled = False
            ComboBox10.Enabled = False
            'ComboBox11.Enabled = False
            'ComboBox12.Enabled = False
            ComboBox9.SelectedIndex = 0
            ComboBox5.SelectedIndex = 0
            ComboBox13.SelectedIndex = 1
            ComboBox13.Enabled = False
            TextBox1.WaterMark = "0 to 15"
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox2.Text = "0"
            TextBox3.Text = "1"
            Toggle1.Enabled = False
            Toggle2.Enabled = False
            ListBox1.Items.Clear()
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = True
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        Else
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            'ComboBox7.Enabled = True
            'ComboBox8.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            'ComboBox11.Enabled = True
            'ComboBox12.Enabled = True
            ComboBox5.SelectedIndex = -1
            ComboBox9.SelectedIndex = -1
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            TextBox1.WaterMark = "0 to 64, 96"
            TextBox2.Text = ""
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox3.Text = ""
            ListBox1.Items.Clear()
            Toggle1.Enabled = True
            Toggle2.Enabled = True
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Click
        If TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False Then

            If PINGPIOToolStripMenuItem.Checked = True Then
                ComboBox3.SelectedIndex = -1
                ComboBox3.Enabled = False
                ComboBox4.SelectedIndex = -1
                ComboBox4.Enabled = False
            Else
                ComboBox3.SelectedIndex = 0 'EVB02 LM8335 ADR is grounded
                ComboBox3.Enabled = True
                ComboBox4.SelectedIndex = 2
                ComboBox4.Enabled = True
            End If

            ComboBox5.Enabled = True
            ComboBox6.Enabled = False
            'ComboBox7.Enabled = False
            'ComboBox8.Enabled = False
            ComboBox9.Enabled = False
            ComboBox10.Enabled = False
            'ComboBox11.Enabled = False
            'ComboBox12.Enabled = False
            ComboBox9.SelectedIndex = 0
            ComboBox5.SelectedIndex = 0
            ComboBox13.SelectedIndex = 1
            ComboBox13.Enabled = False
            TextBox1.WaterMark = "0 to 15"
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox2.Text = "0"
            TextBox3.Text = "1"
            Toggle1.Enabled = False
            Toggle2.Enabled = False
            ListBox1.Items.Clear()
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = True
        Else
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            'ComboBox7.Enabled = True
            'ComboBox8.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            'ComboBox11.Enabled = True
            'ComboBox12.Enabled = True
            ComboBox5.SelectedIndex = -1
            ComboBox9.SelectedIndex = -1
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            TextBox1.WaterMark = "0 to 64, 96"
            TextBox2.Text = ""
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox3.Text = ""
            ListBox1.Items.Clear()
            Toggle1.Enabled = True
            Toggle2.Enabled = True
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub SCOUTSC4415ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SCOUTSC4415ToolStripMenuItem.Click
        If SCOUTSC4415ToolStripMenuItem.Checked = True Then
            SCOUTSC4415ToolStripMenuItem.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = False
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1
            ComboBox6.SelectedIndex = -1
            'ComboBox7.SelectedIndex = -1
            'ComboBox8.SelectedIndex = -1
            ComboBox9.SelectedIndex = -1
            ComboBox10.SelectedIndex = -1
            'ComboBox11.SelectedIndex = -1
            'ComboBox12.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            'ComboBox9.SelectedIndex = -1
            TextBox1.Text = ""
            TextBox1.WaterMark = "0 to 64, 96"
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            ListBox1.Items.Clear()
            Toggle1.Checked = False
            Toggle2.Checked = False
            ConfigurationToolStripMenuItem.Enabled = False
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        Else
            SCOUTSC4415ToolStripMenuItem.Checked = True
            PINGPIOToolStripMenuItem.Checked = False
            SCOUTSC4410ToolStripMenuItem.Checked = False
            RadioButton1.Checked = False
            RadioButton2.Checked = True
            ComboBox3.SelectedIndex = -1
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1
            ComboBox6.SelectedIndex = -1
            'ComboBox7.SelectedIndex = -1
            'ComboBox8.SelectedIndex = -1
            ComboBox9.SelectedIndex = -1
            ComboBox10.SelectedIndex = -1
            'ComboBox11.SelectedIndex = -1
            'ComboBox12.SelectedIndex = -1
            ComboBox3.Enabled = True
            ComboBox4.Enabled = True
            ComboBox5.Enabled = True
            ComboBox6.Enabled = True
            ComboBox9.Enabled = True
            ComboBox10.Enabled = True
            ComboBox13.SelectedIndex = -1
            ComboBox13.Enabled = True
            'ComboBox9.SelectedIndex = -1
            TextBox1.Text = ""
            TextBox1.WaterMark = "0 to 64, 96"
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            ListBox1.Items.Clear()
            Toggle1.Checked = False
            Toggle2.Checked = False
            ConfigurationToolStripMenuItem.Enabled = True
            TypeASSCCXM3664XRToolStripMenuItem.Checked = False
            TypeBSP4TCXA4447GToolStripMenuItem.Checked = False
            TypeC4bitSSCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeDSP4TCXA4484GCSPDTCXA4405GCToolStripMenuItem.Checked = False
            TypeESP4TCXA4484XRSPDTCXA4405GCToolStripMenuItem.Checked = False
        End If
    End Sub
End Class