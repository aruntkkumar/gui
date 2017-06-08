Imports System.Collections.ObjectModel
Imports System.IO
Imports System.IO.Ports
Imports System.Net
Imports System.Text
Imports System.Threading
Imports NativeWifi

Public Class Form6

    Dim fullstring As String
    Dim timestamp As String
    Dim frequency As Double
    Dim channelnumber As Integer
    'Dim datarate As Integer
    'Dim count As Integer
    'Dim quality() As Double
    'Dim avgquality As Double
    'Dim avgqualityarray(8) As Double
    'Dim qualitymax As Double
    'Dim qualityindex(-1) As Integer
    'Dim rssi() As Double
    'Dim avgrssi As Double
    'Dim avgrssiarray(8) As Double
    'Dim rssimax As Double
    'Dim rssiindex(-1) As Integer
    Dim quality As Double
    Dim rssi As Double
    Dim qualityapprox As Integer
    Dim rssiapprox As Integer
    Dim bandwidth As Integer
    Dim foundit As Integer
    Dim download As Double
    Dim upload As Double
    Dim elapsedStartTime As DateTime
    Dim myPort As Array
    Dim myserialPort As New ExSerialPort
    'Dim url As Uri
    'Dim tmp As String
    Dim WithEvents wc As New WebClient
    Dim infoReader As System.IO.FileInfo
    Dim state As String
    Dim values() As String
    Dim comread As String
    Delegate Sub SetTextCallBack(ByVal [text] As String)
    Dim dialog1 As SaveFileDialog = New SaveFileDialog()

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If System.IO.File.Exists("config.txt") Then
                values = File.ReadAllLines("config.txt")
                For i As Integer = 0 To values.Length - 1
                    values(i) = values(i).Substring(values(i).IndexOf("="c) + 2)
                Next
                GlobalVariables.period = values(0)
                GlobalVariables.size = values(1)
                GlobalVariables.dfolder = values(2)
                GlobalVariables.ufolder = values(3)
                If values(4).ToLower = "false" Then
                    GlobalVariables.detailed = False
                Else
                    GlobalVariables.detailed = True
                End If
            Else
                GlobalVariables.period = "1 min"
                GlobalVariables.size = "1 MB"
                GlobalVariables.dfolder = "\\192.168.31.1\tddownload\TDTEMP\Download_Files\"
                GlobalVariables.ufolder = "\\192.168.31.1\tddownload\TDTEMP\Upload_Files\"
                GlobalVariables.detailed = False
            End If
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End Try
    End Sub

    Private Sub Form6_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim list1 As List(Of String) = New List(Of String)
        list1.Add("Termination Period = " & GlobalVariables.period)
        list1.Add("File Size = " & GlobalVariables.size)
        list1.Add("Download Folder = " & GlobalVariables.dfolder)
        list1.Add("Upload Folder = " & GlobalVariables.ufolder)
        If GlobalVariables.detailed = True Then
            list1.Add("Detailed Report = True")
        Else
            list1.Add("Detailed Report = False")
        End If
        File.WriteAllLines("config.txt", list1)
        If myserialPort.IsOpen() Then
            Try
                myserialPort.Close()
            Catch ex As Exception
            End Try
            'Me.Dispose()
            Application.DoEvents()  ' Give port time to close down
            Thread.Sleep(200)
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Form5.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        Button1.Enabled = False
        MenuStrip1.Enabled = False
        'Try
        Dim connectedSsids As Collection(Of [String]) = New Collection(Of String)()
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces        'Gets a list of all the connected SSIDs
                wlanIface.Scan()
                Thread.Sleep(1000)
                Try
                    Dim ssid As Wlan.Dot11Ssid = wlanIface.CurrentConnection.wlanAssociationAttributes.dot11Ssid
                    connectedSsids.Add(New [String](Encoding.ASCII.GetChars(ssid.SSID, 0, CInt(ssid.SSIDLength))))
                    If (GlobalVariables.ssidname = (Encoding.ASCII.GetChars(ssid.SSID, 0, CInt(ssid.SSIDLength)))) Then
                        Exit For
                    End If
                Catch ex As Exception
                    If ex.Message.Contains("The group or resource is not in the correct state to perform the requested operation") Then
                        MetroFramework.MetroMessageBox.Show(Me, "WiFi Connection to """ & GlobalVariables.ssidname & """ has been lost. Please establish a connection and try again.", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    Button1.Enabled = True
                    MenuStrip1.Enabled = True
                    Exit Sub
                End Try
            Next
            If Not connectedSsids.Contains(GlobalVariables.ssidname) Then
                MetroFramework.MetroMessageBox.Show(Me, "WiFi Connection to """ & GlobalVariables.ssidname & """ has been lost. Please establish a connection and try again.", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Button1.Enabled = True
                MenuStrip1.Enabled = True
                Exit Sub
            End If
            If SpeedTestStatusToolStripMenuItem.Checked = True Then
                If (Not Directory.Exists(GlobalVariables.dfolder)) Or (Not Directory.Exists(GlobalVariables.ufolder)) Then
                MetroFramework.MetroMessageBox.Show(Me, "Unable to access the network storage of " & GlobalVariables.ssidname & ". Kindly verify if the network is available and try again.", "Network Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Button1.Enabled = True
                    MenuStrip1.Enabled = True
                    Exit Sub
                End If
            End If
        timestamp = DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")    'Hebrew colon (׃) is from right to left
        foundit = 0
        While foundit < 1
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                wlanIface.Scan()
                Thread.Sleep(1000)
                Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
                For Each network As Wlan.WlanBssEntry In wlanBssEntries
                    If (Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength)) = GlobalVariables.ssidname) Then
                        Dim macAddr As Byte() = network.dot11Bssid
                        Dim tMac As String = ""
                        For i As Integer = 0 To macAddr.Length - 1
                            If tMac = "" Then
                                tMac += macAddr(i).ToString("x2").PadLeft(2, "0"c).ToUpper()
                            Else
                                tMac += ":" & macAddr(i).ToString("x2").PadLeft(2, "0"c).ToUpper()
                            End If
                        Next
                        If tMac.Replace(":", "") = GlobalVariables.macadd Then
                            frequency = network.chCenterFrequency / 1000000
                            If frequency > 5.0 Then
                                If frequency >= 5.745 Then
                                    channelnumber = 149 + ((frequency - 5.745) * 200)
                                ElseIf frequency >= 5.5 Then
                                    channelnumber = 100 + ((frequency - 5.5) * 200)
                                Else
                                    channelnumber = 36 + ((frequency - 5.18) * 200)
                                End If
                            Else
                                If frequency > 3.0 Then
                                    channelnumber = 0
                                Else
                                    If frequency = 2.484 Then
                                        channelnumber = 14
                                    Else
                                        channelnumber = (frequency - 2.407) / 0.005
                                    End If
                                End If
                            End If
                            'datarate = 0
                            'For i As Integer = 0 To network.wlanRateSet.Rates.Length + 10
                            '    If network.wlanRateSet.GetRateInMbps(i) > datarate Then
                            '        datarate = network.wlanRateSet.GetRateInMbps(i)
                            '    End If
                            'Next
                            bandwidth = 0
                            Try
                                bandwidth = CInt(InputBox("Please enter the channel bandwidth in MHz.", "Bandwidth Information", 20))
                            Catch ex As Exception
                                MetroFramework.MetroMessageBox.Show(Me, "No relevant data provided. Kindly try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Button1.Enabled = True
                                MenuStrip1.Enabled = True
                                Exit Sub
                            End Try
                            If ((bandwidth = 20) Or (bandwidth = 22) Or (bandwidth = 40) Or (bandwidth = 80) Or (bandwidth = 160) Or (bandwidth = 2160) Or (bandwidth = 8000)) Then
                                TextBox1.Text = GlobalVariables.ssidname
                                TextBox2.Text = tMac
                                TextBox3.Text = frequency
                                TextBox4.Text = wlanIface.Channel
                                TextBox5.Text = bandwidth
                                fullstring = "SSID = " & GlobalVariables.ssidname & vbNewLine & "MAC Address = " & tMac & vbNewLine & "Frequency = " & frequency & vbNewLine & "Channel = " & wlanIface.Channel & vbNewLine & "Bandwidth = " & bandwidth & vbNewLine
                                fullstring += "Date & Time,RSSI,Signal Quality" & vbNewLine
                                foundit += 1
                                Exit For
                            Else
                                MetroFramework.MetroMessageBox.Show(Me, "No relevant data provided. Kindly try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Button1.Enabled = True
                                MenuStrip1.Enabled = True
                                Exit Sub
                            End If
                        End If
                    End If
                Next
            Next
        End While
        If NoneSelectedToolStripMenuItem.Checked = True Then
                RichTextBox1.Text = "Date & Time | State | RSSI | Signal Quality" & vbNewLine
                state = "None"
                'foundit = 0
                'While foundit < 1
                '    For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                '        wlanIface.Scan()
                '        Thread.Sleep(1000)
                '        Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
                '        For Each network As Wlan.WlanBssEntry In wlanBssEntries
                '            If (Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength)) = GlobalVariables.ssidname) Then 'AndAlso (getMACaddress(network.dot11Bssid) = GlobalVariables.macadd) Then
                '                Dim macAddr As Byte() = network.dot11Bssid
                '                Dim tMac As String = ""
                '                For k As Integer = 0 To macAddr.Length - 1
                '                    If tMac = "" Then
                '                        tMac += macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
                '                    Else
                '                        tMac += ":" & macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
                '                    End If
                '                Next
                '                If tMac.Replace(":", "") = GlobalVariables.macadd Then
                '                    'quality = network.linkQuality  'Not necessary
                '                    'rssi = network.rssi
                '                    fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & network.rssi & "," & network.linkQuality & vbNewLine
                '                    RichTextBox1.Text &= DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & " | " & network.rssi & " | " & network.linkQuality & vbNewLine
                '                    Application.DoEvents()
                '                    Thread.Sleep(200)
                '                    foundit += 1
                '                End If
                '            End If
                '        Next
                '    Next
                'End While
                MonitorSSID()
                SpeedTest()
            Else
                myPort = IO.Ports.SerialPort.GetPortNames()
                Dim x As New ComPortFinder
                Dim list = x.ComPortNames("0403", "6001") 'VID, PID for FT232RQ
            For Each item As String In list
                If item <> Nothing Then
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            myserialPort.PortName = item
                            myserialPort.BaudRate = 9600
                            myserialPort.Parity = Parity.None
                            myserialPort.DataBits = 8
                            myserialPort.StopBits = StopBits.One
                            myserialPort.Open()
                        End If
                    Next
                End If
            Next

            If Not myserialPort.IsOpen Then
                Dim list1 = x.ComPortNames("10C4", "EA60") 'VID, PID for CP2104
                For Each item As String In list1
                    If item <> Nothing Then
                        For Each Str As String In myPort
                            If Str.Contains(item) Then
                                myserialPort.PortName = item
                                myserialPort.BaudRate = 9600
                                myserialPort.Parity = Parity.None
                                myserialPort.DataBits = 8
                                myserialPort.StopBits = StopBits.One
                                myserialPort.Encoding = System.Text.Encoding.GetEncoding(28605)
                                myserialPort.Open()
                            End If
                        Next
                    End If
                Next
            End If

                If myserialPort.IsOpen Then
                    myserialPort.Write(Convert.ToChar(&HFF))
                    'RichTextBox1.Text &= myserialPort.ReadByte()   'Disabled in the firmware. Only enable when any data is being sent back.
                    Thread.Sleep(25)
                    If NormalOperationToolStripMenuItem.Checked = True Then
                        RichTextBox1.Text = "Starting to test..." & vbNewLine
                        myserialPort.Write(Convert.ToChar(&H4E))       'Hex value for char 'N'
                        'myserialPort.ReadByte()
                        Thread.Sleep(25)
                        RichTextBox1.Text &= "Date & Time | RSSI | Signal Quality" & vbNewLine
                        foundit = 0
                        While foundit < 6
                            'For i As Integer = 0 To 5
                            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                                wlanIface.Scan()
                                Thread.Sleep(1000)
                                Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
                                For Each network As Wlan.WlanBssEntry In wlanBssEntries
                                    If (Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength)) = GlobalVariables.ssidname) Then 'AndAlso (getMACaddress(network.dot11Bssid) = GlobalVariables.macadd) Then
                                        Dim macAddr As Byte() = network.dot11Bssid
                                        Dim tMac As String = ""
                                        For k As Integer = 0 To macAddr.Length - 1
                                            If tMac = "" Then
                                                tMac += macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
                                            Else
                                                tMac += ":" & macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
                                            End If
                                        Next
                                        If tMac.Replace(":", "") = GlobalVariables.macadd Then
                                            quality = network.linkQuality
                                            rssi = Math.Abs(network.rssi)         'Absolute value of RSSI
                                            fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & network.rssi & "," & network.linkQuality & vbNewLine
                                            RichTextBox1.Text &= DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & " | " & network.rssi & " | " & network.linkQuality & vbNewLine
                                            Application.DoEvents()
                                            Thread.Sleep(200)
                                            foundit += 1
                                        End If
                                    End If
                                Next
                            Next

                            rssiapprox = CInt(rssi)
                            qualityapprox = CInt(quality)
                            myserialPort.Write(Convert.ToChar(&H52))    'Hex value for char 'R'
                            Thread.Sleep(5)
                            myserialPort.Write(Convert.ToChar(rssiapprox))
                            Thread.Sleep(5)
                            myserialPort.Write(Convert.ToChar(&H4C))   'Hex value for char 'L'
                            Thread.Sleep(5)
                            myserialPort.Write(Convert.ToChar(qualityapprox))
                            Thread.Sleep(2)
                            'Next
                        End While

                        'While (comread <> &H53)
                        While (myserialPort.ReadByte() <> &H53)
                            'myserialPort.ReadByte()
                        End While
                        Thread.Sleep(5)
                        foundit = myserialPort.ReadByte()
                        If foundit = &H1 Then
                            TextBox6.Text = "1"
                        ElseIf foundit = &H2 Then
                            TextBox6.Text = "2"
                        ElseIf foundit = &H3 Then
                            TextBox6.Text = "3"
                        ElseIf foundit = &H4 Then
                            TextBox6.Text = "4"
                        ElseIf foundit = &H5 Then
                            TextBox6.Text = "5"
                        ElseIf foundit = &H6 Then
                            TextBox6.Text = "6"
                        ElseIf foundit = &H7 Then
                            TextBox6.Text = "7"
                        ElseIf foundit = &H8 Then
                            TextBox6.Text = "8"
                        Else
                            TextBox6.Text = "9"
                        End If
                        RichTextBox1.Text &= "State " & TextBox6.Text & " selected." & vbNewLine
                        SpeedTest()
                    Else
                    RichTextBox1.Text = "Date & Time | State | RSSI | Signal Quality" & vbNewLine
                    If STATE1ToolStripMenuItem.Checked = True Then
                        myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
                        Thread.Sleep(25)
                        state = 1
                        myserialPort.Write(Convert.ToChar(&H1))        'Hex value for char '1'
                        Thread.Sleep(25)
                        TextBox6.Text = state
                        TextBox7.Text = ""
                        TextBox8.Text = ""
                        Thread.Sleep(25)
                        MonitorSSID()
                        SpeedTest()
                    End If
                    If STATE2ToolStripMenuItem.Checked = True Then
                        myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
                        Thread.Sleep(25)
                        state = 2
                        myserialPort.Write(Convert.ToChar(&H2))        'Hex value for char '2'
                        Thread.Sleep(25)
                        TextBox6.Text = state
                        TextBox7.Text = ""
                        TextBox8.Text = ""
                        Thread.Sleep(25)
                        MonitorSSID()
                        SpeedTest()
                    End If
                    If STATE3ToolStripMenuItem.Checked = True Then
                        myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
                        Thread.Sleep(25)
                        state = 3
                        myserialPort.Write(Convert.ToChar(&H3))        'Hex value for char '3'
                        Thread.Sleep(25)
                        TextBox6.Text = state
                        TextBox7.Text = ""
                        TextBox8.Text = ""
                        Thread.Sleep(25)
                        MonitorSSID()
                        SpeedTest()
                    End If
                    If STATE4ToolStripMenuItem.Checked = True Then
                        myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
                        Thread.Sleep(25)
                        state = 4
                        myserialPort.Write(Convert.ToChar(&H4))        'Hex value for char '4'
                        Thread.Sleep(25)
                        TextBox6.Text = state
                        TextBox7.Text = ""
                        TextBox8.Text = ""
                        Thread.Sleep(25)
                        MonitorSSID()
                        SpeedTest()
                    End If
                    If STATE5ToolStripMenuItem.Checked = True Then
                        myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
                        Thread.Sleep(25)
                        state = 5
                        myserialPort.Write(Convert.ToChar(&H5))        'Hex value for char '5'
                        Thread.Sleep(25)
                        TextBox6.Text = state
                        TextBox7.Text = ""
                        TextBox8.Text = ""
                        Thread.Sleep(25)
                        MonitorSSID()
                        SpeedTest()
                    End If
                    If STATE6ToolStripMenuItem.Checked = True Then
                        myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
                        Thread.Sleep(25)
                        state = 6
                        myserialPort.Write(Convert.ToChar(&H6))        'Hex value for char '6'
                        Thread.Sleep(25)
                        TextBox6.Text = state
                        TextBox7.Text = ""
                        TextBox8.Text = ""
                        Thread.Sleep(25)
                        MonitorSSID()
                        SpeedTest()
                    End If
                    If STATE7ToolStripMenuItem.Checked = True Then
                        myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
                        Thread.Sleep(25)
                        state = 7
                        myserialPort.Write(Convert.ToChar(&H7))        'Hex value for char '7'
                        Thread.Sleep(25)
                        TextBox6.Text = state
                        TextBox7.Text = ""
                        TextBox8.Text = ""
                        Thread.Sleep(25)
                        MonitorSSID()
                        SpeedTest()
                    End If
                    If STATE8ToolStripMenuItem.Checked = True Then
                        myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
                        Thread.Sleep(25)
                        state = 8
                        myserialPort.Write(Convert.ToChar(&H8))        'Hex value for char '8'
                        Thread.Sleep(25)
                        TextBox6.Text = state
                        TextBox7.Text = ""
                        TextBox8.Text = ""
                        Thread.Sleep(25)
                        MonitorSSID()
                        SpeedTest()
                    End If
                    If STATE9ToolStripMenuItem.Checked = True Then
                        myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
                        Thread.Sleep(25)
                        state = 9
                        myserialPort.Write(Convert.ToChar(&H9))        'Hex value for char '9'
                        Thread.Sleep(25)
                        TextBox6.Text = state
                        TextBox7.Text = ""
                        TextBox8.Text = ""
                        Thread.Sleep(25)
                        MonitorSSID()
                        SpeedTest()
                    End If
                    'myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
                    ''RichTextBox1.Text &= myserialPort.ReadByte()
                    'Thread.Sleep(25)
                    'If STATE1ToolStripMenuItem.Checked = True Then
                    '    state = 1
                    '    myserialPort.Write(Convert.ToChar(&H1))        'Hex value for char '1'
                    'ElseIf STATE2ToolStripMenuItem.Checked = True Then
                    '    state = 2
                    '    myserialPort.Write(Convert.ToChar(&H2))        'Hex value for char '2'
                    'ElseIf STATE3ToolStripMenuItem.Checked = True Then
                    '    state = 3
                    '    myserialPort.Write(Convert.ToChar(&H3))        'Hex value for char '3'
                    'ElseIf STATE4ToolStripMenuItem.Checked = True Then
                    '    state = 4
                    '    myserialPort.Write(Convert.ToChar(&H4))        'Hex value for char '4
                    'ElseIf STATE5ToolStripMenuItem.Checked = True Then
                    '    state = 5
                    '    myserialPort.Write(Convert.ToChar(&H5))        'Hex value for char '5'
                    'ElseIf STATE6ToolStripMenuItem.Checked = True Then
                    '    state = 6
                    '    myserialPort.Write(Convert.ToChar(&H6))        'Hex value for char '6'
                    'ElseIf STATE7ToolStripMenuItem.Checked = True Then
                    '    state = 7
                    '    myserialPort.Write(Convert.ToChar(&H7))        'Hex value for char '7'
                    'ElseIf STATE8ToolStripMenuItem.Checked = True Then
                    '    state = 8
                    '    myserialPort.Write(Convert.ToChar(&H8))        'Hex value for char '8'
                    'ElseIf STATE9ToolStripMenuItem.Checked = True Then
                    '    state = 9
                    '    myserialPort.Write(Convert.ToChar(&H9))        'Hex value for char '9'
                    'End If
                    ''RichTextBox1.Text &= myserialPort.ReadByte()
                    'Thread.Sleep(25)
                    'RichTextBox1.Text = "State " & state & " selected." & vbNewLine
                    'TextBox6.Text = state
                    'Thread.Sleep(25)
                    'RichTextBox1.Text &= "Date & Time | RSSI | Signal Quality" & vbNewLine
                    'foundit = 0
                    'While foundit < 1
                    '    For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                    '        wlanIface.Scan()
                    '        Thread.Sleep(1000)
                    '        Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
                    '        For Each network As Wlan.WlanBssEntry In wlanBssEntries
                    '            If (Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength)) = GlobalVariables.ssidname) Then 'AndAlso (getMACaddress(network.dot11Bssid) = GlobalVariables.macadd) Then
                    '                Dim macAddr As Byte() = network.dot11Bssid
                    '                Dim tMac As String = ""
                    '                For k As Integer = 0 To macAddr.Length - 1
                    '                    If tMac = "" Then
                    '                        tMac += macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
                    '                    Else
                    '                        tMac += ":" & macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
                    '                    End If
                    '                Next
                    '                If tMac.Replace(":", "") = GlobalVariables.macadd Then
                    '                    'quality = network.linkQuality  'Not necessary
                    '                    'rssi = network.rssi
                    '                    fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & network.rssi & "," & network.linkQuality & vbNewLine
                    '                    RichTextBox1.Text &= DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & " | " & network.rssi & " | " & network.linkQuality & vbNewLine
                    '                    Application.DoEvents()
                    '                    Thread.Sleep(200)
                    '                    foundit += 1
                    '                End If
                    '            End If
                    '        Next
                    '    Next
                    'End While
                End If
                Else
                    MetroFramework.MetroMessageBox.Show(Me, "No supported COM Ports available. Please check if FT232RQ or CP2104 is connected and try again.", "COM Port Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Button1.Enabled = True
                    MenuStrip1.Enabled = True
                    Exit Sub
                End If
            End If
            'rssimax = avgrssiarray(0)
            '    For i As Integer = 0 To avgrssiarray.Length - 1
            '        If avgrssiarray(i) > rssimax Then
            '            rssiindex = New Integer(0) {}
            '            rssiindex(0) = i + 1
            '            rssimax = avgrssiarray(i)
            '        ElseIf avgrssiarray(i) = rssimax Then
            '            System.Array.Resize(Of Integer)(rssiindex, rssiindex.Length + 1)
            '            rssiindex(rssiindex.Length - 1) = i + 1
            '        End If
            '    Next
            '    qualitymax = avgqualityarray(0)
            'For i As Integer = 0 To avgqualityarray.Length - 1
            '    If avgqualityarray(i) > qualitymax Then
            '        qualityindex = New Integer(0) {}
            '        qualityindex(0) = i + 1
            '        qualitymax = avgqualityarray(i)
            '    ElseIf avgqualityarray(i) = qualitymax Then
            '        System.Array.Resize(Of Integer)(qualityindex, qualityindex.Length + 1)
            '        qualityindex(qualityindex.Length - 1) = i + 1
            '    End If
            'Next
            'Dim z As Integer = 0
            'While z <= rssiindex.Length - 1
            '    For i As Integer = 0 To qualityindex.Length - 1
            '        If rssiindex(z) = qualityindex(i) Then
            '            myserialPort.Write("SET STATE" & rssiindex(z))
            '            Thread.Sleep(25)
            '            fullstring += myserialPort.ReadLine()
            '            Thread.Sleep(25)
            '            TextBox6.Text = (rssiindex(z))
            '            Exit While
            '        End If
            '    Next
            '    z += 1
            'End While

            'If SpeedTestStatusToolStripMenuItem.Checked = True Then
            '    wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)")
            '    wc.UseDefaultCredentials = True
            '    wc.Credentials = New NetworkCredential("admin", "admin")

            '    wc.DownloadFile(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1mb.test"), tmp1) ' To Remove Latency

            '    For i As Integer = 1 To 100
            '        Thread.Sleep(10)
            '        Application.DoEvents()
            '    Next

            '    foundit = 0
            '    fullstring += "Download Started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Download Speed (Mbps)" & vbNewLine
            '    elapsedStartTime = DateTime.Now
            '    If GlobalVariables.size = "1 MB" Then
            '        wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1mb.test"), tmp1)
            '    ElseIf GlobalVariables.size = "10 MB" Then
            '        wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "10mb.test"), tmp2)
            '    ElseIf GlobalVariables.size = "100 MB" Then
            '        wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "100mb.test"), tmp3)
            '    Else
            '        wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1gb.test"), tmp4)
            '    End If
            '    While wc.IsBusy
            '        Application.DoEvents()
            '    End While

            '    For i As Integer = 1 To 100
            '        Thread.Sleep(10)
            '        Application.DoEvents()
            '    Next

            '    foundit = 0
            '    fullstring += "Upload Started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Upload Speed (Mbps)" & vbNewLine
            '    elapsedStartTime = DateTime.Now
            '    If GlobalVariables.size = "1 MB" Then
            '        wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "1mb.test"), tmp1)
            '    ElseIf GlobalVariables.size = "10 MB" Then
            '        wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "10mb.test"), tmp2)
            '    ElseIf GlobalVariables.size = "100 MB" Then
            '        wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "100mb.test"), tmp3)
            '    Else
            '        wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "1gb.test"), tmp4)
            '    End If
            '    While wc.IsBusy
            '        Application.DoEvents()
            '    End While
            'End If

            Button1.Enabled = True
            MenuStrip1.Enabled = True
            myserialPort.Close()
            dialog1.Filter = "CSV (Comma delimited) (*.csv)|*.csv"
            dialog1.FileName = ""
            If dialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                File.WriteAllText(dialog1.FileName, fullstring)
            End If
            Application.DoEvents()  ' Give port time to close down
            Thread.Sleep(200)
        'Catch ex As Exception
        '    MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    myserialPort.Close()
        '    Button1.Enabled = True
        '    MenuStrip1.Enabled = True
        'End Try
    End Sub

    Private tmp1 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "1mb.test")
    Private tmp2 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "10mb.test")
    Private tmp3 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "100mb.test")
    Private tmp4 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "1gb.test")

    Private Sub wc_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.DownloadFileCompleted
        Dim elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
        fullstring += (download / 1000000) & "," & Math.Round(elapsedtime.TotalSeconds, 2) & "," & Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & vbNewLine
        ProgressBar1.Value = 0
        If foundit = 1 Then
            fullstring += "Download aborted due to the set termination period of " & GlobalVariables.period & vbNewLine
            TextBox7.Text = "Aborted due to the set time limit of " & GlobalVariables.period
        Else
            TextBox7.Text = Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
        End If
    End Sub

    Private Sub wc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        If (GlobalVariables.period = "1 min" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 60)) Or (GlobalVariables.period = "2.5 mins" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 150)) Or (GlobalVariables.period = "5 mins" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 300)) Then
            wc.CancelAsync()
            foundit = 1
            Exit Sub
        End If
        download = CDbl(e.BytesReceived)
        If GlobalVariables.detailed = True Then
            fullstring += download / 1000000 & "," & Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) & "," & Math.Round((download * 8 / (DateTime.Now.Subtract(elapsedStartTime).TotalSeconds * 1000000)), 2) & vbNewLine
        End If
        ProgressBar1.Visible = True
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub wc_UploadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.UploadFileCompleted
        Dim elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
        fullstring += (upload / 1000000) & "," & Math.Round(elapsedtime.TotalSeconds, 2) & "," & Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & vbNewLine
        ProgressBar1.Value = 0
        If foundit = 1 Then
            fullstring += "Upload aborted due to the set termination period of " & GlobalVariables.period
            TextBox8.Text = "Aborted due to the set time limit of " & GlobalVariables.period
        Else
            TextBox8.Text = Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
        End If

        'Button1.Enabled = True
        'MenuStrip1.Enabled = True
        'timestamp = timestamp & " to " & DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")
        'If (Not System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")) Then
        '    System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")
        'End If
        'Dim sw As New StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\" & timestamp & ".csv")
        'sw.Write(fullstring)
        'sw.Close()
        'myserialPort.Close()
        'Application.DoEvents()  ' Give port time to close down
        'Thread.Sleep(200)
        'MetroFramework.MetroMessageBox.Show(Me, "Test results are saved under """ & Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\"" as " & """" & timestamp & ".csv""", "TEST COMPLETE", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub wc_UploadProgressChanged(sender As Object, e As UploadProgressChangedEventArgs) Handles wc.UploadProgressChanged
        If (GlobalVariables.period = "1 min" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 60)) Or (GlobalVariables.period = "2.5 mins" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 150)) Or (GlobalVariables.period = "5 mins" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 300)) Then
            wc.CancelAsync()
            foundit = 1
            Exit Sub
        End If
        upload = CDbl(e.BytesSent)
        If GlobalVariables.detailed = True Then
            fullstring += upload / 1000000 & "," & Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) & "," & Math.Round((upload * 8 / (DateTime.Now.Subtract(elapsedStartTime).TotalSeconds * 1000000)), 2) & vbNewLine
        End If
        ProgressBar1.Visible = True
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub NormalOperationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NormalOperationToolStripMenuItem.Click
        If NormalOperationToolStripMenuItem.Checked = False Then
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = True
        Else
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = True
            NormalOperationToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub STATE1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE1ToolStripMenuItem.Click
        If STATE1ToolStripMenuItem.Checked = False Then
            STATE1ToolStripMenuItem.Checked = True
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = True
            NormalOperationToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub STATE2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE2ToolStripMenuItem.Click
        If STATE2ToolStripMenuItem.Checked = False Then
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = True
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = True
            NormalOperationToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub STATE3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE3ToolStripMenuItem.Click
        If STATE3ToolStripMenuItem.Checked = False Then
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = True
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = True
            NormalOperationToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub STATE4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE4ToolStripMenuItem.Click
        If STATE4ToolStripMenuItem.Checked = False Then
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = True
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = True
            NormalOperationToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub STATE5ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE5ToolStripMenuItem.Click
        If STATE5ToolStripMenuItem.Checked = False Then
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = True
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = True
            NormalOperationToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub STATE6ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE6ToolStripMenuItem.Click
        If STATE6ToolStripMenuItem.Checked = False Then
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = True
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = True
            NormalOperationToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub STATE7ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE7ToolStripMenuItem.Click
        If STATE7ToolStripMenuItem.Checked = False Then
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = True
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = True
            NormalOperationToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub STATE8ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE8ToolStripMenuItem.Click
        If STATE8ToolStripMenuItem.Checked = False Then
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = True
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = True
            NormalOperationToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub STATE9ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE9ToolStripMenuItem.Click
        If STATE9ToolStripMenuItem.Checked = False Then
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = True
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE1ToolStripMenuItem.Checked = False
            STATE2ToolStripMenuItem.Checked = False
            STATE3ToolStripMenuItem.Checked = False
            STATE4ToolStripMenuItem.Checked = False
            STATE5ToolStripMenuItem.Checked = False
            STATE6ToolStripMenuItem.Checked = False
            STATE7ToolStripMenuItem.Checked = False
            STATE8ToolStripMenuItem.Checked = False
            STATE9ToolStripMenuItem.Checked = False
            NoneSelectedToolStripMenuItem.Checked = True
            NormalOperationToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub NoneSelectedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoneSelectedToolStripMenuItem.Click
        'If NoneSelectedToolStripMenuItem.Checked = False Then
        STATE1ToolStripMenuItem.Checked = False
        STATE2ToolStripMenuItem.Checked = False
        STATE3ToolStripMenuItem.Checked = False
        STATE4ToolStripMenuItem.Checked = False
        STATE5ToolStripMenuItem.Checked = False
        STATE6ToolStripMenuItem.Checked = False
        STATE7ToolStripMenuItem.Checked = False
        STATE8ToolStripMenuItem.Checked = False
        STATE9ToolStripMenuItem.Checked = False
        NoneSelectedToolStripMenuItem.Checked = True
        NormalOperationToolStripMenuItem.Checked = False
        'Else
        '    STATE1ToolStripMenuItem.Checked = False
        '    STATE2ToolStripMenuItem.Checked = False
        '    STATE3ToolStripMenuItem.Checked = False
        '    STATE4ToolStripMenuItem.Checked = False
        '    STATE5ToolStripMenuItem.Checked = False
        '    STATE6ToolStripMenuItem.Checked = False
        '    STATE7ToolStripMenuItem.Checked = False
        '    STATE8ToolStripMenuItem.Checked = False
        '    STATE9ToolStripMenuItem.Checked = False
        '    NoneSelectedToolStripMenuItem.Checked = False
        '    NormalOperationToolStripMenuItem.Checked = True
        'End If
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs)
        ReceivedText(myserialPort.ReadExisting())
    End Sub

    Private Sub ReceivedText(ByVal [text] As String)
        If Me.RichTextBox1.InvokeRequired Then
            Dim x As New SetTextCallBack(AddressOf ReceivedText)
            Me.Invoke(x, New Object() {(text)})
        Else
            Me.RichTextBox1.Text &= [text]
        End If
        comread = [text]
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        RichTextBox1.SelectionStart = RichTextBox1.Text.Length
        RichTextBox1.ScrollToCaret()
    End Sub

    Private Sub SpeedTestStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpeedTestStatusToolStripMenuItem.Click
        If SpeedTestStatusToolStripMenuItem.Checked = False Then
            SpeedTestStatusToolStripMenuItem.Checked = True
            OptionsToolStripMenuItem.Enabled = True
        Else
            SpeedTestStatusToolStripMenuItem.Checked = False
            OptionsToolStripMenuItem.Enabled = False
        End If
    End Sub

    Function MonitorSSID()
        foundit = 0
        While foundit < 1
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                wlanIface.Scan()
                Thread.Sleep(1000)
                Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
                For Each network As Wlan.WlanBssEntry In wlanBssEntries
                    If (Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength)) = GlobalVariables.ssidname) Then 'AndAlso (getMACaddress(network.dot11Bssid) = GlobalVariables.macadd) Then
                        Dim macAddr As Byte() = network.dot11Bssid
                        Dim tMac As String = ""
                        For k As Integer = 0 To macAddr.Length - 1
                            If tMac = "" Then
                                tMac += macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
                            Else
                                tMac += ":" & macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
                            End If
                        Next
                        If tMac.Replace(":", "") = GlobalVariables.macadd Then
                            'quality = network.linkQuality  'Not necessary
                            'rssi = network.rssi
                            fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & state & "," & network.rssi & "," & network.linkQuality & vbNewLine
                            RichTextBox1.Text &= DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & " | " & state & " | " & network.rssi & " | " & network.linkQuality & vbNewLine
                            Application.DoEvents()
                            Thread.Sleep(200)
                            foundit += 1
                        End If
                    End If
                Next
            Next
        End While
        Return 0
    End Function

    Function SpeedTest()
        If SpeedTestStatusToolStripMenuItem.Checked = True Then
            wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)")
            wc.UseDefaultCredentials = True
            wc.Credentials = New NetworkCredential("admin", "admin")

            wc.DownloadFile(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1mb.test"), tmp1) ' To Remove Latency

            For i As Integer = 1 To 100
                Thread.Sleep(10)
                Application.DoEvents()
            Next

            foundit = 0
            fullstring += "State " & state & " download started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Download Speed (Mbps)" & vbNewLine
            elapsedStartTime = DateTime.Now
            If GlobalVariables.size = "1 MB" Then
                wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1mb.test"), tmp1)
            ElseIf GlobalVariables.size = "10 MB" Then
                wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "10mb.test"), tmp2)
            ElseIf GlobalVariables.size = "100 MB" Then
                wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "100mb.test"), tmp3)
            Else
                wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1gb.test"), tmp4)
            End If
            While wc.IsBusy
                Application.DoEvents()
            End While

            For i As Integer = 1 To 100
                Thread.Sleep(10)
                Application.DoEvents()
            Next

            foundit = 0
            fullstring += "State " & state & " upload started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Upload Speed (Mbps)" & vbNewLine
            elapsedStartTime = DateTime.Now
            If GlobalVariables.size = "1 MB" Then
                wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "1mb.test"), tmp1)
            ElseIf GlobalVariables.size = "10 MB" Then
                wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "10mb.test"), tmp2)
            ElseIf GlobalVariables.size = "100 MB" Then
                wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "100mb.test"), tmp3)
            Else
                wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "1gb.test"), tmp4)
            End If
            While wc.IsBusy
                Application.DoEvents()
            End While

            For i As Integer = 1 To 100
                Thread.Sleep(10)
                Application.DoEvents()
            Next

        End If
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