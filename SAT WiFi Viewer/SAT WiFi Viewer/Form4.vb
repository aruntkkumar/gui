Imports System.Collections.ObjectModel
Imports System.IO
Imports System.IO.Ports
Imports System.Net
Imports System.Text
Imports System.Threading
Imports NativeWifi

Public Class Form4

    Dim fullstring As String = ""
    Dim timestamp As String
    Dim frequency As Double
    Dim channelnumber As Integer
    Dim datarate As Integer
    Dim count As Integer
    Dim quality() As Double
    Dim avgquality As Double
    Dim avgqualityarray(8) As Double
    Dim qualitymax As Double
    Dim qualityindex(-1) As Integer
    Dim rssi() As Double
    Dim avgrssi As Double
    Dim avgrssiarray(8) As Double
    Dim rssimax As Double
    Dim rssiindex(-1) As Integer
    Dim bandwidth As Integer
    Dim foundit As Integer
    Dim download As Double
    Dim upload As Double
    Dim elapsedStartTime As DateTime
    Dim myPort As Array
    Dim myserialPort As New SerialPort
    'Dim url As Uri
    'Dim tmp As String
    Dim WithEvents wc As New WebClient
    Dim infoReader As System.IO.FileInfo
    Dim state As String
    Dim values() As String
    Dim dialog1 As SaveFileDialog = New SaveFileDialog()
    Dim qualityapprox As Integer
    Dim rssiapprox As Integer
    Dim qualityvalue As Double
    Dim rssivalue As Double
    Dim states(-1) As String
    Dim rssis(-1) As Double
    Dim links(-1) As Double
    Dim downloads(-1) As Double
    Dim uploads(-1) As Double
    Dim URI As String
    Dim oRequest As FileWebRequest
    Dim oResponse As WebResponse
    Dim responseStream As Stream
    Dim fs As FileStream
    Dim buffer(65536) As Byte
    Dim read As Integer
    Dim elapsedtime As TimeSpan
    Dim tmp As Object
    Dim sizeselect As String
    Dim newone As Integer

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            myPort = IO.Ports.SerialPort.GetPortNames()
            Dim x As New ComPortFinder
            Dim list As List(Of String)
            list = x.ComPortNames("16C0", "0483") 'VID, PID for Teensy 3.2
            For Each item As String In list
                If item <> Nothing Then
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            TeensyToolStripMenuItem.Enabled = True
                        End If
                    Next
                End If
            Next
            list = x.ComPortNames("0403", "6001") 'VID, PID for FT232RQ
            For Each item As String In list
                If item <> Nothing Then
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            WLANBSRev02ToolStripMenuItem.Enabled = True
                        End If
                    Next
                End If
            Next
            list = x.ComPortNames("10C4", "EA60") 'VID, PID for CP2104
            For Each item As String In list
                If item <> Nothing Then
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            WLANBSRev02ToolStripMenuItem.Enabled = True
                        End If
                    Next
                End If
            Next
            AddHandler System.Windows.Forms.Application.Idle, AddressOf Application_Idle
            Control.CheckForIllegalCrossThreadCalls = False
            SaveAsToolStripMenuItem.Enabled = False
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End Try
    End Sub

    Private Sub MonitorSSIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MonitorSSIDToolStripMenuItem.Click
        If MonitorSSIDToolStripMenuItem.Checked = False Then
            MonitorSSIDToolStripMenuItem.Checked = True
        Else
            MonitorSSIDToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Button1.Enabled = False
            MenuStrip1.Enabled = False
            Application.DoEvents()
            TextBox9.Text = ""
            TextBox10.Text = ""
            TextBox11.Text = ""
            Dim connectedSsids As Collection(Of [String]) = New Collection(Of String)()
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces        'Gets a list of all the connected SSIDs
                wlanIface.Scan()
                Thread.Sleep(100)
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
                'If (Not Directory.Exists("\\192.168.31.1\tddownload\TDTEMP\Download_Files\")) Or (Not Directory.Exists("\\192.168.31.1\tddownload\TDTEMP\Upload_Files\")) Then
                '    MetroFramework.MetroMessageBox.Show(Me, "Unable to access \\192.168.31.1\tddownload\TDTEMP\. Kindly verify if the network is available and try again.", "Network Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If (Not Directory.Exists(GlobalVariables.dfolder)) Or (Not Directory.Exists(GlobalVariables.ufolder)) Then
                    MetroFramework.MetroMessageBox.Show(Me, "Unable to access the network storage of " & GlobalVariables.ssidname & ". Kindly verify if the network is available and try again.", "Network Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Button1.Enabled = True
                    MenuStrip1.Enabled = True
                    Exit Sub
                End If
            End If
            If NoneSelectedToolStripMenuItem.Checked = False Then
                If TeensyToolStripMenuItem.Enabled = False AndAlso WLANBSRev02ToolStripMenuItem.Enabled = False Then
                    MetroFramework.MetroMessageBox.Show(Me, "No active devices found. Please connect a supported device for testing the selected states.", "Device Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Button1.Enabled = True
                    MenuStrip1.Enabled = True
                    Exit Sub
                End If
                If TeensyToolStripMenuItem.Checked = False AndAlso WLANBSRev02ToolStripMenuItem.Checked = False Then
                    MetroFramework.MetroMessageBox.Show(Me, "Please select an active device from the Devices list for testing the selected states.", "Device Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Button1.Enabled = True
                    MenuStrip1.Enabled = True
                    Exit Sub
                End If
            End If
            timestamp = DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")    'Hebrew colon (׃) is from right to left
            foundit = 0
            While foundit < 1
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                If Me.IsDisposed Then
                    Exit Sub
                Else
                    wlanIface.Scan()
                    'Thread.Sleep(100)
                    Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
                    For Each network As Wlan.WlanBssEntry In wlanBssEntries
                        If (Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength)) = GlobalVariables.ssidname) Then 'AndAlso (getMACaddress(network.dot11Bssid) = GlobalVariables.macadd) Then
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
                                datarate = 0
                                For i As Integer = 0 To network.wlanRateSet.Rates.Length + 10
                                    If network.wlanRateSet.GetRateInMbps(i) > datarate Then
                                        datarate = network.wlanRateSet.GetRateInMbps(i)
                                    End If
                                Next
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
                                    TextBox3.Text = network.dot11BssPhyType
                                    TextBox4.Text = frequency
                                    TextBox5.Text = wlanIface.Channel
                                    TextBox6.Text = bandwidth
                                    TextBox7.Text = datarate
                                    TextBox8.Text = network.dot11BssType
                                    fullstring = "SSID = " & GlobalVariables.ssidname & vbNewLine & "MAC Address = " & tMac & vbNewLine & "PHY Type = " & network.dot11BssPhyType & vbNewLine & "Frequency = " & frequency & vbNewLine & "Channel = " & wlanIface.Channel & vbNewLine & "Bandwidth = " & bandwidth & vbNewLine & "Maximum Data Rate = " & datarate & vbNewLine & "BSS Type = " & network.dot11BssType & vbNewLine
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
                End If
            Next
        End While
            DataGridView1.Rows.Clear()
            If NoneSelectedToolStripMenuItem.Checked = False Then
                myPort = IO.Ports.SerialPort.GetPortNames()
                Dim x As New ComPortFinder
                Dim list As List(Of String)
                If TeensyToolStripMenuItem.Checked = True Then
                    list = x.ComPortNames("16C0", "0483")
                    For Each item As String In list
                        If item <> Nothing Then
                            For Each Str As String In myPort
                                If Str.Contains(item) Then
                                    myserialPort.PortName = item
                                    myserialPort.BaudRate = 115200
                                    myserialPort.Parity = Parity.None
                                    myserialPort.DataBits = 8
                                    myserialPort.StopBits = StopBits.One
                                    myserialPort.Open()
                                End If
                            Next
                        End If
                    Next
                Else
                    list = x.ComPortNames("0403", "6001") 'VID, PID for FT232RQ
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
                        list = x.ComPortNames("10C4", "EA60") 'VID, PID for CP2104
                        For Each item As String In list
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
                End If
            End If
            states = New String(-1) {}
            rssis = New Double(-1) {}
            links = New Double(-1) {}
            downloads = New Double(-1) {}
            uploads = New Double(-1) {}
            newone = 0
        Do
            If Me.IsDisposed Then
                Exit Sub
            Else
                Application.DoEvents()
            End If
            TextBox9.Text = ""
            TextBox10.Text = ""
            TextBox11.Text = ""
            If NoneSelectedToolStripMenuItem.Checked = True Then
                state = "None"
                TextBox9.Text = "None"
                System.Array.Resize(states, states.Length + 1)
                states(states.Length - 1) = "None"
                fullstring += "No State selected" & vbNewLine
                MonitorSSID()
                SpeedTest()
            Else
                If myserialPort.IsOpen Then
                    If TeensyToolStripMenuItem.Checked = True Then
                        If NormalOperationToolStripMenuItem.Checked = True Then
                            rssiindex = New Integer(-1) {}
                            qualityindex = New Integer(-1) {}
                            For i As Integer = 1 To 9
                                myserialPort.Write("SET STATE" & i)
                                Thread.Sleep(25)
                                myserialPort.ReadLine()
                                Thread.Sleep(25)
                                count = 0
                                quality = New Double(count) {}
                                rssi = New Double(count) {}
                                foundit = 0
                                While foundit < 10
                                    For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                                        If Me.IsDisposed Then
                                            Exit Sub
                                        Else
                                            wlanIface.Scan()
                                            Application.DoEvents()
                                            Thread.Sleep(1)
                                            Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
                                            For Each network As Wlan.WlanBssEntry In wlanBssEntries
                                                Application.DoEvents()
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
                                                        count += 1
                                                        quality(count - 1) = network.linkQuality
                                                        rssi(count - 1) = network.rssi
                                                        avgquality = 0.0
                                                        avgrssi = 0.0
                                                        For Each n In quality
                                                            avgquality += n
                                                        Next
                                                        For Each n In rssi
                                                            avgrssi += n
                                                        Next
                                                        avgquality /= count
                                                        avgrssi /= count
                                                        avgquality = Math.Round(avgquality, 1)
                                                        avgrssi = Math.Round(avgrssi, 1)
                                                        fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & "STATE" & i & "," & network.rssi & "," & avgrssi & "," & network.linkQuality & "," & avgquality & vbNewLine
                                                        DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), "STATE" & i, network.rssi, avgrssi, network.linkQuality, avgquality)
                                                        DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
                                                        System.Array.Resize(Of Double)(quality, count + 1)
                                                        System.Array.Resize(Of Double)(rssi, count + 1)
                                                        Application.DoEvents()
                                                        'Thread.Sleep(200)
                                                        foundit += 1
                                                    End If
                                                End If
                                            Next
                                        End If
                                    Next
                                End While
                                avgqualityarray(i - 1) = avgquality
                                avgrssiarray(i - 1) = avgrssi
                            Next
                            rssimax = avgrssiarray(0)
                            For i As Integer = 0 To avgrssiarray.Length - 1
                                If avgrssiarray(i) > rssimax Then
                                    rssiindex = New Integer(0) {}
                                    rssiindex(0) = i + 1
                                    rssimax = avgrssiarray(i)
                                ElseIf avgrssiarray(i) = rssimax Then
                                    System.Array.Resize(Of Integer)(rssiindex, rssiindex.Length + 1)
                                    rssiindex(rssiindex.Length - 1) = i + 1
                                End If
                            Next
                            qualitymax = avgqualityarray(0)
                            For i As Integer = 0 To avgqualityarray.Length - 1
                                If avgqualityarray(i) > qualitymax Then
                                    qualityindex = New Integer(0) {}
                                    qualityindex(0) = i + 1
                                    qualitymax = avgqualityarray(i)
                                ElseIf avgqualityarray(i) = qualitymax Then
                                    System.Array.Resize(Of Integer)(qualityindex, qualityindex.Length + 1)
                                    qualityindex(qualityindex.Length - 1) = i + 1
                                End If
                            Next
                            foundit = 0
                            Dim z As Integer = 0
                            While z <= rssiindex.Length - 1
                                For i As Integer = 0 To qualityindex.Length - 1
                                    If rssiindex(z) = qualityindex(i) Then
                                        myserialPort.Write("SET STATE" & rssiindex(z))
                                        Thread.Sleep(25)
                                        fullstring += myserialPort.ReadLine()
                                        Thread.Sleep(25)
                                        TextBox9.Text = (rssiindex(z))
                                        state = (rssiindex(z))
                                        foundit = 1
                                        Exit While
                                    End If
                                Next
                                z += 1
                            End While
                            If foundit = 0 Then                                     'Condition when RSSI and Link Quality does not match.
                                rssimax = avgrssiarray(qualityindex(0) - 1)
                                state = qualityindex(0)
                                For i As Integer = 0 To qualityindex.Length - 1
                                    If avgrssiarray(qualityindex(i) - 1) > rssimax Then
                                        rssimax = avgrssiarray(qualityindex(i) - 1)
                                        state = qualityindex(i)
                                    End If
                                Next
                                myserialPort.Write("SET STATE" & state)   'Created Version 3.0 with option to check the best link quality than best RSSI.
                                Thread.Sleep(25)
                                fullstring += myserialPort.ReadLine()
                                Thread.Sleep(25)
                                TextBox9.Text = (state)
                            End If
                            System.Array.Resize(states, states.Length + 1)  'Summary Details Size Reallocation
                            System.Array.Resize(rssis, rssis.Length + 1)
                            System.Array.Resize(links, links.Length + 1)
                            states(states.Length - 1) = "STATE" & state       'Summary Details
                            rssis(rssis.Length - 1) = avgrssiarray(state - 1)
                            links(links.Length - 1) = avgqualityarray(state - 1)
                            Thread.Sleep(200)
                            SpeedTest()
                        Else
                            If STATE1ToolStripMenuItem.Checked = True Then
                                state = 1
                                TeensyStateTest()
                            End If
                            If STATE2ToolStripMenuItem.Checked = True Then
                                state = 2
                                TeensyStateTest()
                            End If
                            If STATE3ToolStripMenuItem.Checked = True Then
                                state = 3
                                TeensyStateTest()
                            End If
                            If STATE4ToolStripMenuItem.Checked = True Then
                                state = 4
                                TeensyStateTest()
                            End If
                            If STATE5ToolStripMenuItem.Checked = True Then
                                state = 5
                                TeensyStateTest()
                            End If
                            If STATE6ToolStripMenuItem.Checked = True Then
                                state = 6
                                TeensyStateTest()
                            End If
                            If STATE7ToolStripMenuItem.Checked = True Then
                                state = 7
                                TeensyStateTest()
                            End If
                            If STATE8ToolStripMenuItem.Checked = True Then
                                state = 8
                                TeensyStateTest()
                            End If
                            If STATE9ToolStripMenuItem.Checked = True Then
                                state = 9
                                TeensyStateTest()
                            End If
                        End If
                    Else
                        myserialPort.Write(Convert.ToChar(&HFF))
                        'myserialPort.ReadByte()   'Disabled in the firmware. Only enable when any data is being sent back.
                        Thread.Sleep(25)
                        If NormalOperationToolStripMenuItem.Checked = True Then
                            avgrssiarray = New Double(8) {}
                            avgqualityarray = New Double(8) {}
                            myserialPort.Write(Convert.ToChar(&H4E))       'Hex value for char 'N'
                            'myserialPort.ReadByte()
                            Thread.Sleep(25)
                            fullstring += "Date & Time,RSSI,Signal Quality" & vbNewLine
                            foundit = 0
                            While foundit < 6
                                For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                                    If Me.IsDisposed Then
                                        Exit Sub
                                    Else
                                        wlanIface.Scan()
                                        Application.DoEvents()
                                        Thread.Sleep(1)
                                        Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
                                        For Each network As Wlan.WlanBssEntry In wlanBssEntries
                                            Application.DoEvents()
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
                                                    avgrssiarray(foundit) = (network.rssi)
                                                    avgqualityarray(foundit) = network.linkQuality
                                                    qualityvalue = network.linkQuality
                                                    rssivalue = Math.Abs(network.rssi)         'Absolute value of RSSI
                                                    fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & network.rssi & "," & network.linkQuality & vbNewLine
                                                    DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), "Unknown", network.rssi, network.rssi, network.linkQuality, network.linkQuality)
                                                    DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
                                                    Application.DoEvents()
                                                    'Thread.Sleep(200)
                                                    foundit += 1
                                                End If
                                            End If
                                        Next
                                    End If
                                Next

                                rssiapprox = CInt(rssivalue)
                                qualityapprox = CInt(qualityvalue)
                                myserialPort.Write(Convert.ToChar(&H52))    'Hex value for char 'R'
                                Thread.Sleep(10)
                                myserialPort.Write(Convert.ToChar(rssiapprox))
                                Thread.Sleep(10)
                                myserialPort.Write(Convert.ToChar(&H4C))   'Hex value for char 'L'
                                Thread.Sleep(10)
                                myserialPort.Write(Convert.ToChar(qualityapprox))
                                Thread.Sleep(10)
                            End While

                            While (myserialPort.ReadByte() <> &H53)
                                'myserialPort.ReadByte()
                            End While
                            Thread.Sleep(10)
                            System.Array.Resize(rssis, rssis.Length + 1)
                            System.Array.Resize(links, links.Length + 1)
                            foundit = myserialPort.ReadByte()
                            If foundit = &H1 Then
                                TextBox9.Text = "1"
                            ElseIf foundit = &H2 Then
                                TextBox9.Text = "2"
                                rssis(rssis.Length - 1) = avgrssiarray(3)
                                links(links.Length - 1) = avgqualityarray(3)
                            ElseIf foundit = &H3 Then
                                TextBox9.Text = "3"
                                rssis(rssis.Length - 1) = avgrssiarray(5)
                                links(links.Length - 1) = avgqualityarray(5)
                            ElseIf foundit = &H4 Then
                                TextBox9.Text = "4"
                                rssis(rssis.Length - 1) = avgrssiarray(1)
                                links(links.Length - 1) = avgqualityarray(1)
                            ElseIf foundit = &H5 Then
                                TextBox9.Text = "5"
                                rssis(rssis.Length - 1) = avgrssiarray(0)
                                links(links.Length - 1) = avgqualityarray(0)
                            ElseIf foundit = &H6 Then
                                TextBox9.Text = "6"
                                rssis(rssis.Length - 1) = avgrssiarray(2)
                                links(links.Length - 1) = avgqualityarray(2)
                            ElseIf foundit = &H7 Then
                                TextBox9.Text = "7"
                            ElseIf foundit = &H8 Then
                                TextBox9.Text = "8"
                                rssis(rssis.Length - 1) = avgrssiarray(4)
                                links(links.Length - 1) = avgqualityarray(4)
                            Else
                                TextBox9.Text = "9"
                            End If
                            fullstring += "State " & TextBox9.Text & " selected." & vbNewLine
                            state = TextBox9.Text
                            System.Array.Resize(states, states.Length + 1)
                            states(states.Length - 1) = "STATE" & state
                            SpeedTest()
                        Else
                            If STATE1ToolStripMenuItem.Checked = True Then
                                state = 1
                                WLANBSStateTest()
                            End If
                            If STATE2ToolStripMenuItem.Checked = True Then
                                state = 2
                                WLANBSStateTest()
                            End If
                            If STATE3ToolStripMenuItem.Checked = True Then
                                state = 3
                                WLANBSStateTest()
                            End If
                            If STATE4ToolStripMenuItem.Checked = True Then
                                state = 4
                                WLANBSStateTest()
                            End If
                            If STATE5ToolStripMenuItem.Checked = True Then
                                state = 5
                                WLANBSStateTest()
                            End If
                            If STATE6ToolStripMenuItem.Checked = True Then
                                state = 6
                                WLANBSStateTest()
                            End If
                            If STATE7ToolStripMenuItem.Checked = True Then
                                state = 7
                                WLANBSStateTest()
                            End If
                            If STATE8ToolStripMenuItem.Checked = True Then
                                state = 8
                                WLANBSStateTest()
                            End If
                            If STATE9ToolStripMenuItem.Checked = True Then
                                state = 9
                                WLANBSStateTest()
                            End If
                        End If
                    End If
                Else
                    MetroFramework.MetroMessageBox.Show(Me, "No supported COM Ports available. Please check if Teensy 3.2 is connected and try again.", "COM Port Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Button1.Enabled = True
                    MenuStrip1.Enabled = True
                    Exit Sub
                End If
            End If
        Loop Until Toggle1.Checked = False
        Button1.Enabled = True
            MenuStrip1.Enabled = True
            'dialog1.Filter = "CSV (Comma delimited) (*.csv)|*.csv"
            'dialog1.FileName = ""
            'If dialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    File.WriteAllText(dialog1.FileName, fullstring)
            'End If
            myserialPort.Close()
            Application.DoEvents()  ' Give port time to close down
            Thread.Sleep(200)
            fullstring += vbNewLine & "TEST RESULT SUMMARY" & vbNewLine & "STATE,AVG. RSSI,AVG. LINK QUALITY,AVG. DOWNLOAD,AVG. UPLOAD" & vbNewLine
            For i As Integer = 0 To states.Length - 1
                fullstring += states(i) & ","
                If i <= rssis.Length - 1 Then
                    fullstring += rssis(i) & "," & links(i) & ","
                Else
                    fullstring += ",,"
                End If
                If i <= downloads.Length - 1 Then
                    fullstring += downloads(i) & ","
                Else
                    fullstring += ","
                End If
                If i <= uploads.Length - 1 Then
                    fullstring += uploads(i) & ","
                Else
                    fullstring += ","
                End If
                fullstring += vbNewLine
            Next

            'If NoneSelectedToolStripMenuItem.Checked = False Then
            '    myPort = IO.Ports.SerialPort.GetPortNames()
            '    Dim x As New ComPortFinder
            '    Dim list = x.ComPortNames("16C0", "0483")
            '    For Each item As String In list
            '        For Each Str As String In myPort
            '            If Str.Contains(item) Then
            '                myserialPort.PortName = item
            '                myserialPort.BaudRate = 115200
            '                myserialPort.Parity = Parity.None
            '                myserialPort.DataBits = 8
            '                myserialPort.StopBits = StopBits.One
            '                myserialPort.Open()
            '            End If
            '        Next
            '    Next

            '    If myserialPort.IsOpen Then
            '        If STATE1ToolStripMenuItem.Checked = True Then
            '            state = 1
            '            myserialPort.Write("SET STATE1")
            '        ElseIf STATE2ToolStripMenuItem.Checked = True Then
            '            state = 2
            '            myserialPort.Write("SET STATE2")
            '        ElseIf STATE3ToolStripMenuItem.Checked = True Then
            '            state = 3
            '            myserialPort.Write("SET STATE3")
            '        ElseIf STATE4ToolStripMenuItem.Checked = True Then
            '            state = 4
            '            myserialPort.Write("SET STATE4")
            '        ElseIf STATE5ToolStripMenuItem.Checked = True Then
            '            state = 5
            '            myserialPort.Write("SET STATE5")
            '        ElseIf STATE6ToolStripMenuItem.Checked = True Then
            '            state = 6
            '            myserialPort.Write("SET STATE6")
            '        ElseIf STATE7ToolStripMenuItem.Checked = True Then
            '            state = 7
            '            myserialPort.Write("SET STATE7")
            '        ElseIf STATE8ToolStripMenuItem.Checked = True Then
            '            state = 8
            '            myserialPort.Write("SET STATE8")
            '        ElseIf STATE9ToolStripMenuItem.Checked = True Then
            '            state = 9
            '            myserialPort.Write("SET STATE9")
            '        End If
            '        Thread.Sleep(25)
            '        myserialPort.ReadLine()
            '        Thread.Sleep(25)
            '    End If
            'Else
            '    state = "None"
            'End If
            'TextBox9.Text = state
            'If state = "None" Then
            '    fullstring += "No State selected" & vbNewLine
            'Else
            '    fullstring += "STATE " & state & " selected" & vbNewLine
            'End If
            'count = 0
            'quality = New Double(count) {}
            'rssi = New Double(count) {}
            'If MonitorSSIDToolStripMenuItem.Checked = True Then
            '    fullstring += "Date & Time,RSSI,Signal Quality,Avg Signal Quality" & vbNewLine
            '    'First 10 times are compulsory (9 plus 1 from Do While Loop)
            '    foundit = 0
            '    While foundit < 9
            '        'For j As Integer = 1 To 10
            '        For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
            '            wlanIface.Scan()
            '            Thread.Sleep(1000)
            '            Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
            '            For Each network As Wlan.WlanBssEntry In wlanBssEntries
            '                If (Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength)) = GlobalVariables.ssidname) Then 'AndAlso (getMACaddress(network.dot11Bssid) = GlobalVariables.macadd) Then
            '                    Dim macAddr As Byte() = network.dot11Bssid
            '                    Dim tMac As String = ""
            '                    For k As Integer = 0 To macAddr.Length - 1
            '                        If tMac = "" Then
            '                            tMac += macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
            '                        Else
            '                            tMac += ":" & macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
            '                        End If
            '                    Next
            '                    If tMac.Replace(":", "") = GlobalVariables.macadd Then
            '                        count += 1
            '                        quality(count - 1) = network.linkQuality
            '                        rssi(count - 1) = network.rssi
            '                        avgquality = 0.0
            '                        avgrssi = 0.0
            '                        For Each n In quality
            '                            avgquality += n
            '                        Next
            '                        For Each n In rssi
            '                            avgrssi += n
            '                        Next
            '                        avgquality /= count
            '                        avgrssi /= count
            '                        avgquality = Math.Round(avgquality, 1)
            '                        avgrssi = Math.Round(avgrssi, 1)
            '                        fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & network.rssi & "," & network.linkQuality & "," & avgquality & vbNewLine
            '                        DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), network.rssi, network.linkQuality, avgquality) ', download / 8000000, Math.Round((download / (8000000 * totaltime)), 2))
            '                        DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
            '                        System.Array.Resize(Of Double)(quality, count + 1)
            '                        System.Array.Resize(Of Double)(rssi, count + 1)
            '                        Application.DoEvents()
            '                        Thread.Sleep(200)
            '                        foundit += 1
            '                    End If
            '                End If
            '            Next
            '        Next
            '        'Next
            '    End While
            '    Do
            '        For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
            '            wlanIface.Scan()
            '            Thread.Sleep(1000)
            '            Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
            '            For Each network As Wlan.WlanBssEntry In wlanBssEntries
            '                If (Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength)) = GlobalVariables.ssidname) Then 'AndAlso (getMACaddress(network.dot11Bssid) = GlobalVariables.macadd) Then
            '                    Dim macAddr As Byte() = network.dot11Bssid
            '                    Dim tMac As String = ""
            '                    For k As Integer = 0 To macAddr.Length - 1
            '                        If tMac = "" Then
            '                            tMac += macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
            '                        Else
            '                            tMac += ":" & macAddr(k).ToString("x2").PadLeft(2, "0"c).ToUpper()
            '                        End If
            '                    Next
            '                    If tMac.Replace(":", "") = GlobalVariables.macadd Then
            '                        count += 1
            '                        quality(count - 1) = network.linkQuality
            '                        rssi(count - 1) = network.rssi
            '                        avgquality = 0.0
            '                        avgrssi = 0.0
            '                        For Each n In quality
            '                            avgquality += n
            '                        Next
            '                        For Each n In rssi
            '                            avgrssi += n
            '                        Next
            '                        avgquality /= count
            '                        avgrssi /= count
            '                        avgquality = Math.Round(avgquality, 1)
            '                        avgrssi = Math.Round(avgrssi, 1)
            '                        fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & network.rssi & "," & network.linkQuality & "," & avgquality & vbNewLine
            '                        DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), network.rssi, network.linkQuality, avgquality) ', download / 8000000, Math.Round((download / (8000000 * totaltime)), 2))
            '                        DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
            '                        System.Array.Resize(Of Double)(quality, count + 1)
            '                        System.Array.Resize(Of Double)(rssi, count + 1)
            '                        Application.DoEvents()
            '                        Thread.Sleep(200)
            '                    End If
            '                End If
            '            Next
            '        Next
            '    Loop Until Toggle1.Checked = False
            'End If

            'If SpeedTestStatusToolStripMenuItem.Checked = True Then
            '    wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)")
            '    wc.UseDefaultCredentials = True
            '    wc.Credentials = New NetworkCredential("admin", "admin")
            '    foundit = 0
            '    fullstring += "Download Started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Download Speed (Mbps)" & vbNewLine
            '    elapsedStartTime = DateTime.Now
            '    'If MBToolStripMenuItem.Checked = True Then
            '    '    wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/1mb.test"), tmp1)
            '    'ElseIf MBToolStripMenuItem1.Checked = True Then
            '    '    wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/10mb.test"), tmp2)
            '    'ElseIf MBToolStripMenuItem2.Checked = True Then
            '    '    wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/100mb.test"), tmp3)
            '    'Else
            '    '    wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/1gb.test"), tmp4)
            '    'End If
            '    If GlobalVariables.size = "1 MB" Then
            '        wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1mb.test"), tmp1)
            '    ElseIf GlobalVariables.size = "10 MB" Then
            '        wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "10mb.test"), tmp2)
            '    ElseIf GlobalVariables.size = "100 MB" Then
            '        wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "100mb.test"), tmp3)
            '    Else
            '        wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1gb.test"), tmp4)
            '    End If
            'Else
            '    Button1.Enabled = True
            '    MenuStrip1.Enabled = True
            '    timestamp = timestamp & " to " & DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")
            '    If (Not System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")) Then
            '        System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")
            '    End If
            '    Dim sw As New StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\" & timestamp & ".csv")
            '    sw.Write(fullstring)
            '    sw.Close()
            '    myserialPort.Close()
            '    Application.DoEvents()  ' Give port time to close down
            '    Thread.Sleep(200)
            '    MetroFramework.MetroMessageBox.Show(Me, "Test results are saved under """ & Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\"" as " & """" & timestamp & ".csv""", "TEST COMPLETE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If

            'Application.DoEvents()
            'Thread.Sleep(100)

        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Button1.Enabled = True
            MenuStrip1.Enabled = True
        End Try
    End Sub

    Private tmp1 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "1mb.test")
    Private tmp2 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "10mb.test")
    Private tmp3 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "100mb.test")
    Private tmp4 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "1gb.test")

    Private Sub wc_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.DownloadFileCompleted
        Dim elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
        'fullstring += "Total Bytes received = " & (download / 1000000) & " MB. Seconds taken = " & Math.Round(elapsedtime.TotalSeconds, 2) & ". Download Speed = " & Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & "Mbps" & vbNewLine
        fullstring += (download / 1000000) & "," & Math.Round(elapsedtime.TotalSeconds, 2) & "," & Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & vbNewLine
        'ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        If foundit = 1 Then
            fullstring += "Download aborted due to the set termination period of " & GlobalVariables.period & vbNewLine
            TextBox10.Text = Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & " (Aborted due to the set time limit of " & GlobalVariables.period & ")"
        Else
            TextBox10.Text = Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
        End If
        System.Array.Resize(downloads, downloads.Length + 1)
        downloads(downloads.Length - 1) = Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
        'foundit = 0
        'fullstring += "State " & state & " upload Started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Upload Speed (Mbps)" & vbNewLine
        'elapsedStartTime = DateTime.Now
        'If MBToolStripMenuItem.Checked = True Then
        '    wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/1mb.test"), tmp1)
        'ElseIf MBToolStripMenuItem1.Checked = True Then
        '    wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/10mb.test"), tmp2)
        'ElseIf MBToolStripMenuItem2.Checked = True Then
        '    wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/100mb.test"), tmp3)
        'Else
        '    wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/1gb.test"), tmp4)
        'End If

        'If GlobalVariables.size = "1 MB" Then
        '    wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "1mb.test"), tmp1)
        'ElseIf GlobalVariables.size = "10 MB" Then
        '    wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "10mb.test"), tmp2)
        'ElseIf GlobalVariables.size = "100 MB" Then
        '    wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "100mb.test"), tmp3)
        'Else
        '    wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "1gb.test"), tmp4)
        'End If
    End Sub

    Private Sub wc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        'If foundit = 0 Then
        '    elapsedStartTime = DateTime.Now
        '    foundit = 1
        'End If
        If (GlobalVariables.period = "1 min" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 60)) Or (GlobalVariables.period = "2.5 mins" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 150)) Or (GlobalVariables.period = "5 mins" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 300)) Then
            wc.CancelAsync()
            foundit = 1
            Exit Sub
        End If
        download = CDbl(e.BytesReceived)
        If GlobalVariables.detailed = True Then
            'fullstring += download & " Bytes received. Download speed = " & Math.Round((download * 8 / (DateTime.Now.Subtract(elapsedStartTime).TotalSeconds * 1000000)), 2) & "Mbps" & vbNewLine
            fullstring += download / 1000000 & "," & Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) & "," & Math.Round((download * 8 / (DateTime.Now.Subtract(elapsedStartTime).TotalSeconds * 1000000)), 2) & vbNewLine
        End If
        ProgressBar1.Visible = True
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub wc_UploadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.UploadFileCompleted
        Dim elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
        'fullstring += "Total Bytes sent = " & (upload / 1000000) & " MB. Seconds taken = " & Math.Round(elapsedtime.TotalSeconds, 2) & ". Upload Speed = " & Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & "Mbps"
        fullstring += (upload / 1000000) & "," & Math.Round(elapsedtime.TotalSeconds, 2) & "," & Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & vbNewLine
        'ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        If foundit = 1 Then
            fullstring += "Upload aborted due to the set termination period of " & GlobalVariables.period & vbNewLine
            TextBox11.Text = Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & " (Aborted due to the set time limit of " & GlobalVariables.period & ")"
        Else
            TextBox11.Text = Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
        End If
        System.Array.Resize(uploads, uploads.Length + 1)
        uploads(uploads.Length - 1) = Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
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
        'If foundit = 0 Then
        '    elapsedStartTime = DateTime.Now
        '    foundit = 1
        'End If
        If (GlobalVariables.period = "1 min" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 60)) Or (GlobalVariables.period = "2.5 mins" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 150)) Or (GlobalVariables.period = "5 mins" AndAlso (Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) > 300)) Then
            wc.CancelAsync()
            foundit = 1
            Exit Sub
        End If
        upload = CDbl(e.BytesSent)
        If GlobalVariables.detailed = True Then
            'fullstring += upload & " Bytes sent. Upload speed = " & Math.Round((upload * 8 / (DateTime.Now.Subtract(elapsedStartTime).TotalSeconds * 1000000)), 2) & "Mbps" & vbNewLine
            fullstring += upload / 1000000 & "," & Math.Round((DateTime.Now.Subtract(elapsedStartTime).TotalSeconds), 2) & "," & Math.Round((upload * 8 / (DateTime.Now.Subtract(elapsedStartTime).TotalSeconds * 1000000)), 2) & vbNewLine
        End If
        ProgressBar1.Visible = True
        ProgressBar1.Value = e.ProgressPercentage
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
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE1ToolStripMenuItem.Checked = False
        End If
        NoneSelectCheck()
    End Sub

    Private Sub STATE2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE2ToolStripMenuItem.Click
        If STATE2ToolStripMenuItem.Checked = False Then
            STATE2ToolStripMenuItem.Checked = True
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE2ToolStripMenuItem.Checked = False
        End If
        NoneSelectCheck()
    End Sub

    Private Sub STATE3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE3ToolStripMenuItem.Click
        If STATE3ToolStripMenuItem.Checked = False Then
            STATE3ToolStripMenuItem.Checked = True
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE3ToolStripMenuItem.Checked = False
        End If
        NoneSelectCheck()
    End Sub

    Private Sub STATE4ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE4ToolStripMenuItem.Click
        If STATE4ToolStripMenuItem.Checked = False Then
            STATE4ToolStripMenuItem.Checked = True
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE4ToolStripMenuItem.Checked = False
        End If
        NoneSelectCheck()
    End Sub

    Private Sub STATE5ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE5ToolStripMenuItem.Click
        If STATE5ToolStripMenuItem.Checked = False Then
            STATE5ToolStripMenuItem.Checked = True
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE5ToolStripMenuItem.Checked = False
        End If
        NoneSelectCheck()
    End Sub

    Private Sub STATE6ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE6ToolStripMenuItem.Click
        If STATE6ToolStripMenuItem.Checked = False Then
            STATE6ToolStripMenuItem.Checked = True
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE6ToolStripMenuItem.Checked = False
        End If
        NoneSelectCheck()
    End Sub

    Private Sub STATE7ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE7ToolStripMenuItem.Click
        If STATE7ToolStripMenuItem.Checked = False Then
            STATE7ToolStripMenuItem.Checked = True
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE7ToolStripMenuItem.Checked = False
        End If
        NoneSelectCheck()
    End Sub

    Private Sub STATE8ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE8ToolStripMenuItem.Click
        If STATE8ToolStripMenuItem.Checked = False Then
            STATE8ToolStripMenuItem.Checked = True
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE8ToolStripMenuItem.Checked = False
        End If
        NoneSelectCheck()
    End Sub

    Private Sub STATE9ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles STATE9ToolStripMenuItem.Click
        If STATE9ToolStripMenuItem.Checked = False Then
            STATE9ToolStripMenuItem.Checked = True
            NoneSelectedToolStripMenuItem.Checked = False
            NormalOperationToolStripMenuItem.Checked = False
        Else
            STATE9ToolStripMenuItem.Checked = False
        End If
        NoneSelectCheck()
    End Sub

    Private Sub NoneSelectedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoneSelectedToolStripMenuItem.Click
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
    End Sub

    Function NoneSelectCheck()
        If STATE1ToolStripMenuItem.Checked = False AndAlso STATE2ToolStripMenuItem.Checked = False AndAlso STATE3ToolStripMenuItem.Checked = False AndAlso STATE4ToolStripMenuItem.Checked = False AndAlso STATE5ToolStripMenuItem.Checked = False AndAlso STATE6ToolStripMenuItem.Checked = False AndAlso STATE7ToolStripMenuItem.Checked = False AndAlso STATE8ToolStripMenuItem.Checked = False AndAlso STATE9ToolStripMenuItem.Checked = False Then
            NoneSelectedToolStripMenuItem.Checked = True
        End If
        Return 0
    End Function

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Form5.ShowDialog()
    End Sub

    Private Sub Form4_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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
            wc.CancelAsync()
            Application.DoEvents()  ' Give port time to close down
            Thread.Sleep(200)
            BackgroundWorker1.Dispose()
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Sub MonitorSSID()
        If MonitorSSIDToolStripMenuItem.Checked = True Then
            If WLANBSRev02ToolStripMenuItem.Checked = True Then
                fullstring += "Date & Time,State,RSSI,Signal Quality" & vbNewLine
                foundit = 0
                While foundit < 1
                    For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                        If Me.IsDisposed Then
                            Exit Sub
                        Else
                            wlanIface.Scan()
                            Application.DoEvents()
                            Thread.Sleep(1)
                            Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
                            For Each network As Wlan.WlanBssEntry In wlanBssEntries
                                Application.DoEvents()
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
                                        System.Array.Resize(rssis, rssis.Length + 1)
                                        System.Array.Resize(links, links.Length + 1)
                                        rssis(rssis.Length - 1) = network.rssi
                                        links(links.Length - 1) = network.linkQuality
                                        DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), state, network.rssi, network.rssi, network.linkQuality, network.linkQuality)
                                        DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
                                        Application.DoEvents()
                                        'Thread.Sleep(200)
                                        foundit += 1
                                    End If
                                End If
                            Next
                        End If
                    Next
                End While
            Else
                count = 0
                quality = New Double(count) {}
                rssi = New Double(count) {}
                fullstring += "Date & Time,State,RSSI,Avg RSSI,Signal Quality,Avg Signal Quality" & vbNewLine
                'First 10 times are compulsory. Changed to 10 and 0 from Do While Loop.(Previously, 9 plus 1 from Do While Loop). 
                foundit = 0
                While foundit < 10
                    For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                        If Me.IsDisposed Then
                            Exit Sub
                        Else
                            wlanIface.Scan()
                            Application.DoEvents()
                            Thread.Sleep(1)
                            Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
                            For Each network As Wlan.WlanBssEntry In wlanBssEntries
                                Application.DoEvents()
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
                                        count += 1
                                        quality(count - 1) = network.linkQuality
                                        rssi(count - 1) = network.rssi
                                        avgquality = 0.0
                                        avgrssi = 0.0
                                        For Each n In quality
                                            avgquality += n
                                        Next
                                        For Each n In rssi
                                            avgrssi += n
                                        Next
                                        avgquality /= count
                                        avgrssi /= count
                                        avgquality = Math.Round(avgquality, 1)
                                        avgrssi = Math.Round(avgrssi, 1)
                                        If state = "None" Then
                                            fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & state & "," & network.rssi & "," & avgrssi & "," & network.linkQuality & "," & avgquality & vbNewLine
                                            DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), state, network.rssi, avgrssi, network.linkQuality, avgquality) ', download / 8000000, Math.Round((download / (8000000 * totaltime)), 2))
                                        Else
                                            fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & "STATE" & state & "," & network.rssi & "," & avgrssi & "," & network.linkQuality & "," & avgquality & vbNewLine
                                            DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), "STATE" & state, network.rssi, avgrssi, network.linkQuality, avgquality) ', download / 8000000, Math.Round((download / (8000000 * totaltime)), 2))
                                        End If
                                        DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
                                        System.Array.Resize(Of Double)(quality, count + 1)
                                        System.Array.Resize(Of Double)(rssi, count + 1)
                                        Application.DoEvents()
                                        'Thread.Sleep(200)
                                        foundit += 1
                                    End If
                                End If
                            Next
                        End If
                    Next
                End While
                System.Array.Resize(rssis, rssis.Length + 1)
                System.Array.Resize(links, links.Length + 1)
                rssis(rssis.Length - 1) = avgrssi
                links(links.Length - 1) = avgquality
                'Do
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
                '                    count += 1
                '                    quality(count - 1) = network.linkQuality
                '                    rssi(count - 1) = network.rssi
                '                    avgquality = 0.0
                '                    avgrssi = 0.0
                '                    For Each n In quality
                '                        avgquality += n
                '                    Next
                '                    For Each n In rssi
                '                        avgrssi += n
                '                    Next
                '                    avgquality /= count
                '                    avgrssi /= count
                '                    avgquality = Math.Round(avgquality, 1)
                '                    avgrssi = Math.Round(avgrssi, 1)
                '                    If state = "None" Then
                '                        fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & state & "," & network.rssi & "," & avgrssi & "," & network.linkQuality & "," & avgquality & vbNewLine
                '                        DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), state, network.rssi, avgrssi, network.linkQuality, avgquality) ', download / 8000000, Math.Round((download / (8000000 * totaltime)), 2))
                '                    Else
                '                        fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & "STATE" & state & "," & network.rssi & "," & avgrssi & "," & network.linkQuality & "," & avgquality & vbNewLine
                '                        DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), "STATE" & state, network.rssi, avgrssi, network.linkQuality, avgquality) ', download / 8000000, Math.Round((download / (8000000 * totaltime)), 2))
                '                    End If
                '                    DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
                '                    System.Array.Resize(Of Double)(quality, count + 1)
                '                    System.Array.Resize(Of Double)(rssi, count + 1)
                '                    Application.DoEvents()
                '                    Thread.Sleep(200)
                '                End If
                '            End If
                '        Next
                '    Next
                'Loop Until Toggle1.Checked = False
                'End If
            End If
        End If
    End Sub

    Sub SpeedTest()
        If SpeedTestStatusToolStripMenuItem.Checked = True Then

            tmp = tmp1                                                  ' To Remove Latency
            sizeselect = "1mb.test"
            URI = "file:" & GlobalVariables.dfolder.Replace("\", "/") & sizeselect
            oRequest = CType(FileWebRequest.Create(URI), FileWebRequest)
            oRequest.Credentials = New NetworkCredential("admin", "admin")
            oRequest.Timeout = 10000
            oResponse = CType(oRequest.GetResponse, WebResponse)
            responseStream = oResponse.GetResponseStream()
            buffer = New Byte(FileLen(tmp) / 100) {}
            If Me.IsDisposed Then
                Exit Sub
            Else
                fs = New FileStream(tmp, FileMode.Create, FileAccess.Write)
            End If
            Do
                If Me.IsDisposed Then
                    Exit Sub
                End If
                read = responseStream.Read(buffer, 0, buffer.Length)
                fs.Write(buffer, 0, read)
                Application.DoEvents()
            Loop Until read = 0
            responseStream.Close()
            fs.Flush()
            fs.Close()
            responseStream.Close()
            oResponse.Close()
            buffer = Nothing

            If GlobalVariables.size = "1 MB" Then                       ' Download Loop
                tmp = tmp1
                sizeselect = "1mb.test"
            ElseIf GlobalVariables.size = "10 MB" Then
                tmp = tmp2
                sizeselect = "10mb.test"
            ElseIf GlobalVariables.size = "100 MB" Then
                tmp = tmp3
                sizeselect = "100mb.test"
            Else
                tmp = tmp4
                sizeselect = "1gb.test"
            End If

            ProgressBar1.Value = 0.0
            Label13.Visible = True
            Label13.Text = "Download Progress:"
            foundit = 0
            fullstring += "State " & state & " download started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Download Speed (Mbps)" & vbNewLine
            URI = "file:" & GlobalVariables.dfolder.Replace("\", "/") & sizeselect
            oRequest = CType(FileWebRequest.Create(URI), FileWebRequest)
            oRequest.Credentials = New NetworkCredential("admin", "admin")
            oRequest.Timeout = 10000
            oResponse = CType(oRequest.GetResponse, WebResponse)
            responseStream = oResponse.GetResponseStream()
            If newone = 0 AndAlso GlobalVariables.size <> "1 MB" Then
                buffer = New Byte(FileLen(tmp) / 100) {}
            Else
                buffer = New Byte(FileLen(tmp) / 1000) {}
            End If
            newone = 1
            If Me.IsDisposed Then
                Exit Sub
            Else
                fs = New FileStream(tmp, FileMode.Create, FileAccess.Write)
            End If
            elapsedStartTime = DateTime.Now
            Do
                If Me.IsDisposed Then
                    Exit Sub
                End If
                read = responseStream.Read(buffer, 0, buffer.Length)
                fs.Write(buffer, 0, read)
                ProgressBar1.Value = (FileLen(tmp) / FileLen(GlobalVariables.dfolder & sizeselect)) * 100.0
                Application.DoEvents()
                download = FileLen(tmp)
                elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
                If (GlobalVariables.period = "1 min" AndAlso (Math.Round((elapsedtime.TotalSeconds), 2) > 60)) Or (GlobalVariables.period = "2.5 mins" AndAlso (Math.Round((elapsedtime.TotalSeconds), 2) > 150)) Or (GlobalVariables.period = "5 mins" AndAlso (Math.Round((elapsedtime.TotalSeconds), 2) > 300)) Then
                    foundit = 1
                    Exit Do
                End If
                If GlobalVariables.detailed = True Then
                    fullstring += download / 1000000 & "," & Math.Round((elapsedtime.TotalSeconds), 2) & "," & Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & vbNewLine
                End If
            Loop Until read = 0
            fullstring += download / 1000000 & "," & Math.Round((elapsedtime.TotalSeconds), 2) & "," & Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & vbNewLine
            If foundit = 1 Then
                fullstring += "Download aborted due to the set termination period of " & GlobalVariables.period & vbNewLine
                TextBox10.Text = Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & " (Aborted due to the set time limit of " & GlobalVariables.period & ")"
            Else
                TextBox10.Text = Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
            End If
            System.Array.Resize(downloads, downloads.Length + 1)
            downloads(downloads.Length - 1) = Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
            ProgressBar1.Value = 0.0
            responseStream.Close()
            fs.Flush()
            fs.Close()
            responseStream.Close()
            oResponse.Close()
            buffer = Nothing
            Label13.Visible = False
            For i As Integer = 1 To 50
                Thread.Sleep(10)
                Application.DoEvents()
            Next

            If GlobalVariables.downloadonly = False Then                        ' Upload Loop
                Label13.Visible = True
                Label13.Text = "Upload Progress:"
                foundit = 0
                fullstring += "State " & state & " upload started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Upload Speed (Mbps)" & vbNewLine
                URI = GlobalVariables.ufolder & sizeselect
                oRequest = CType(FileWebRequest.Create(tmp), FileWebRequest)
                oRequest.Credentials = New NetworkCredential("admin", "admin")
                oRequest.Timeout = 10000
                oResponse = CType(oRequest.GetResponse, WebResponse)
                responseStream = oResponse.GetResponseStream()
                buffer = New Byte(FileLen(tmp) / 100) {}
                If Me.IsDisposed Then
                    Exit Sub
                Else
                    fs = New FileStream(URI, FileMode.Create, FileAccess.Write)
                End If
                elapsedStartTime = DateTime.Now
                Do
                    If Me.IsDisposed Then
                        Exit Sub
                    End If
                    read = responseStream.Read(buffer, 0, buffer.Length)
                    fs.Write(buffer, 0, read)
                    ProgressBar1.Value = (FileLen(URI) / FileLen(GlobalVariables.dfolder & sizeselect)) * 100.0
                    Application.DoEvents()
                    upload = FileLen(URI)
                    elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
                    If (GlobalVariables.period = "1 min" AndAlso (Math.Round((elapsedtime.TotalSeconds), 2) > 60)) Or (GlobalVariables.period = "2.5 mins" AndAlso (Math.Round((elapsedtime.TotalSeconds), 2) > 150)) Or (GlobalVariables.period = "5 mins" AndAlso (Math.Round((elapsedtime.TotalSeconds), 2) > 300)) Then
                        foundit = 1
                        Exit Do
                    End If
                    If GlobalVariables.detailed = True Then
                        fullstring += upload / 1000000 & "," & Math.Round((elapsedtime.TotalSeconds), 2) & "," & Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & vbNewLine
                    End If
                Loop Until read = 0
                fullstring += upload / 1000000 & "," & Math.Round((elapsedtime.TotalSeconds), 2) & "," & Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & vbNewLine
                If foundit = 1 Then
                    fullstring += "Upload aborted due to the set termination period of " & GlobalVariables.period & vbNewLine
                    TextBox11.Text = Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & " (Aborted due to the set time limit of " & GlobalVariables.period & ")"
                Else
                    TextBox11.Text = Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
                End If
                System.Array.Resize(uploads, uploads.Length + 1)
                uploads(uploads.Length - 1) = Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
                ProgressBar1.Value = 0.0
                responseStream.Close()
                fs.Flush()
                fs.Close()
                responseStream.Close()
                oResponse.Close()
                buffer = Nothing
                Label13.Visible = False
                For i As Integer = 1 To 50
                    Thread.Sleep(10)
                    Application.DoEvents()
                Next
            End If

            'Dim URI As String = "file:" & GlobalVariables.dfolder.Replace("\", "/") & "10mb.test"
            'Dim oRequest As FileWebRequest = CType(FileWebRequest.Create(URI), FileWebRequest)
            'oRequest.Credentials = New NetworkCredential("admin", "admin")
            'oRequest.Timeout = 10000
            'Using oResponse As WebResponse = CType(oRequest.GetResponse, WebResponse)
            '    Using responseStream As IO.Stream = oResponse.GetResponseStream
            '        Using fs As New FileStream(tmp2, FileMode.Create, FileAccess.Write)
            '            Dim buffer(10240) As Byte
            '            Dim read As Integer
            '            ProgressBar1.Value = 0.0
            '            Do
            '                If Me.IsDisposed Then
            '                    Exit Sub
            '                End If
            '                read = responseStream.Read(buffer, 0, buffer.Length)
            '                fs.Write(buffer, 0, read)
            '                ProgressBar1.Value = (FileLen(tmp2) / FileLen(GlobalVariables.dfolder & "10mb.test")) * 100.0
            '                Application.DoEvents()
            '            Loop Until read = 0
            '            'MsgBox("Download Completed")
            '            ProgressBar1.Value = 0.0
            '            responseStream.Close()
            '            fs.Flush()
            '            fs.Close()
            '        End Using
            '        responseStream.Close()
            '    End Using
            '    oResponse.Close()
            'End Using

            'wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)")
            'wc.UseDefaultCredentials = True
            'wc.Credentials = New NetworkCredential("admin", "admin")
            'wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore)

            'wc.DownloadFile(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1mb.test"), tmp1) ' To Remove Latency

            'For i As Integer = 1 To 100
            '    Thread.Sleep(10)
            '    Application.DoEvents()
            'Next
            'File.Delete(tmp1)
            'Label13.Visible = True
            'Label13.Text = "Download Progress:"
            'foundit = 0
            'fullstring += "State " & state & " download started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Download Speed (Mbps)" & vbNewLine
            'elapsedStartTime = DateTime.Now
            'If GlobalVariables.size = "1 MB" Then
            '    wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1mb.test"), tmp1)
            'ElseIf GlobalVariables.size = "10 MB" Then
            '    wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "10mb.test"), tmp2)
            'ElseIf GlobalVariables.size = "100 MB" Then
            '    wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "100mb.test"), tmp3)
            'Else
            '    wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1gb.test"), tmp4)
            'End If
            'While wc.IsBusy
            '    Application.DoEvents()
            'End While
            'Label13.Visible = False
            'For i As Integer = 1 To 100
            '    Thread.Sleep(10)
            '    Application.DoEvents()
            'Next

            'If GlobalVariables.downloadonly = False Then
            '    Label13.Visible = True
            '    Label13.Text = "Upload Progress:"
            '    foundit = 0
            '    fullstring += "State " & state & " upload started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Upload Speed (Mbps)" & vbNewLine
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
            '    Label13.Visible = False
            '    For i As Integer = 1 To 100
            '        Thread.Sleep(10)
            '        Application.DoEvents()
            '    Next

            'End If
        End If
    End Sub

    Sub TeensyStateTest()
        TextBox9.Text = state
        TextBox10.Text = ""
        TextBox11.Text = ""
        myserialPort.Write("SET STATE" & state)
        Thread.Sleep(25)
        myserialPort.ReadLine()
        Thread.Sleep(25)
        fullstring += "STATE " & state & " selected" & vbNewLine
        System.Array.Resize(states, states.Length + 1)
        states(states.Length - 1) = "STATE" & state
        MonitorSSID()
        SpeedTest()
    End Sub

    Sub WLANBSStateTest()
        myserialPort.Write(Convert.ToChar(&H73))        'Hex value for char 's'
        Thread.Sleep(25)
        'myserialPort.Write(Convert.ToChar(&H1))        'Hex value for char '1'
        myserialPort.Write(Convert.ToChar(state))
        Thread.Sleep(25)
        TextBox9.Text = state
        TextBox10.Text = ""
        TextBox11.Text = ""
        Thread.Sleep(25)
        fullstring += "STATE " & state & " selected" & vbNewLine
        System.Array.Resize(states, states.Length + 1)
        states(states.Length - 1) = "STATE" & state
        MonitorSSID()
        SpeedTest()
    End Sub

    Private Sub TeensyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TeensyToolStripMenuItem.Click
        If TeensyToolStripMenuItem.Checked = False Then
            TeensyToolStripMenuItem.Checked = True
            WLANBSRev02ToolStripMenuItem.Checked = False
        Else
            TeensyToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub WLANBSRev02ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WLANBSRev02ToolStripMenuItem.Click
        If WLANBSRev02ToolStripMenuItem.Checked = False Then
            WLANBSRev02ToolStripMenuItem.Checked = True
            TeensyToolStripMenuItem.Checked = False
        Else
            WLANBSRev02ToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork  ' Do some time-consuming work on this thread.
        'MsgBox("Hello")
        myPort = IO.Ports.SerialPort.GetPortNames()
        Dim x As New ComPortFinder
        Dim list As List(Of String)
        list = x.ComPortNames("16C0", "0483") 'VID, PID for Teensy 3.2
        foundit = 0
        For Each item As String In list
            If item <> Nothing Then
                For Each Str As String In myPort
                    If Str.Contains(item) Then
                        TeensyToolStripMenuItem.Enabled = True
                        foundit = 1
                    End If
                Next
            End If
        Next
        If foundit = 0 Then
            TeensyToolStripMenuItem.Enabled = False
            TeensyToolStripMenuItem.Checked = False
        End If
        list = x.ComPortNames("0403", "6001") 'VID, PID for FT232RQ
        foundit = 0
        For Each item As String In list
            If item <> Nothing Then
                For Each Str As String In myPort
                    If Str.Contains(item) Then
                        WLANBSRev02ToolStripMenuItem.Enabled = True
                        foundit = 1
                    End If
                Next
            End If
        Next
        If foundit = 0 Then
            list = x.ComPortNames("10C4", "EA60") 'VID, PID for CP2104
            foundit = 0
            For Each item As String In list
                If item <> Nothing Then
                    For Each Str As String In myPort
                        If Str.Contains(item) Then
                            WLANBSRev02ToolStripMenuItem.Enabled = True
                            foundit = 1
                        End If
                    Next
                End If
            Next
            If foundit = 0 Then
                WLANBSRev02ToolStripMenuItem.Enabled = False
                WLANBSRev02ToolStripMenuItem.Checked = False
            End If
            If fullstring = "" Then
                SaveAsToolStripMenuItem.Enabled = False
            Else
                SaveAsToolStripMenuItem.Enabled = True
            End If
        End If
    End Sub

    Private Sub Application_Idle(ByVal sender As Object, ByVal e As EventArgs)
        If Not BackgroundWorker1.IsBusy Then
            BackgroundWorker1.RunWorkerAsync()
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        Try
            dialog1.Filter = "CSV (Comma delimited) (*.csv)|*.csv"
            dialog1.FileName = ""
            If dialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                File.WriteAllText(dialog1.FileName, fullstring)
            End If
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub ChangeSSIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeSSIDToolStripMenuItem.Click
        Form1.Show()
        Me.Close()
        Me.Dispose()
    End Sub
End Class