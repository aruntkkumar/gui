Imports NativeWifi
Imports System
Imports System.Collections.ObjectModel
Imports System.Net.NetworkInformation
Imports System.IO
Imports System.Threading
Imports System.Text
Imports System.Net
Imports System.ComponentModel
Imports System.IO.Ports

Public Class Form3

    Dim fullstring As String
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
    'Dim totaltime As Double
    Dim WithEvents wc As New WebClient
    'Dim WithEvents myWebClient As New WebClient

    'Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    'AddHandler System.Windows.Forms.Application.Idle, AddressOf Application_Idle
    '    Try
    '        myPort = IO.Ports.SerialPort.GetPortNames()
    '        Dim x As New ComPortFinder
    '        Dim list = x.ComPortNames("16C0", "0483")
    '        For Each item As String In list
    '            For Each Str As String In myPort
    '                If Str.Contains(item) Then
    '                    myserialPort.PortName = item
    '                    myserialPort.BaudRate = 115200
    '                    myserialPort.Parity = Parity.None
    '                    myserialPort.DataBits = 8
    '                    myserialPort.StopBits = StopBits.One
    '                    myserialPort.Open()
    '                End If
    '            Next
    '        Next
    '        'MsgBox(myserialPort.PortName)
    '        'MsgBox(myserialPort.IsOpen)
    '        If myserialPort.IsOpen Then
    '            myserialPort.Write("INIT")
    '            myserialPort.Write("INIT")
    '            Thread.Sleep(25)
    '            myserialPort.ReadLine()
    '            Thread.Sleep(25)
    '            myserialPort.Write("BLINK")
    '            Thread.Sleep(25)
    '            MsgBox(myserialPort.ReadLine())
    '            Thread.Sleep(25)
    '            myserialPort.Close()
    '            Application.DoEvents()  ' Give port time to close down
    '            Thread.Sleep(200)
    '            MsgBox(myserialPort.IsOpen)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex = -1 Then
            MetroFramework.MetroMessageBox.Show(Me, "Please select a file size for speed test.", "File size", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        ComboBox1.Enabled = False
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        'If Button1.Text = "Start" Then
        Button1.Enabled = False
        'foundit = 0
        Try
            Dim connectedSsids As Collection(Of [String]) = New Collection(Of String)()
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces        'Gets a list of all the connected SSIDs
                wlanIface.Scan()
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
                    ComboBox1.Enabled = True
                    Exit Sub
                End Try
            Next
            If Not connectedSsids.Contains(GlobalVariables.ssidname) Then
                MetroFramework.MetroMessageBox.Show(Me, "WiFi Connection to """ & GlobalVariables.ssidname & """ has been lost. Please establish a connection and try again.", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Button1.Enabled = True
                ComboBox1.Enabled = True
                Exit Sub
            End If
            'If (Not Directory.Exists("\\192.168.0.1\volume(sda1)\Download_Files\")) Or (Not Directory.Exists("\\192.168.0.1\volume(sda1)\Upload_Files\")) Then
            If (Not Directory.Exists("\\192.168.31.1\tddownload\TDTEMP\Download_Files\")) Or (Not Directory.Exists("\\192.168.31.1\tddownload\TDTEMP\Upload_Files\")) Then
                'MetroFramework.MetroMessageBox.Show(Me, "Unable to access \\192.168.0.1\volume(sda1)\. Kindly verify if the network is available and try again.", "Network Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                MetroFramework.MetroMessageBox.Show(Me, "Unable to access \\192.168.31.1\tddownload\TDTEMP\. Kindly verify if the network is available and try again.", "Network Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Button1.Enabled = True
                ComboBox1.Enabled = True
                Exit Sub
            End If
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                wlanIface.Scan()
                'If (wlanIface.InterfaceState.ToString() = "Disconnecting" Or wlanIface.InterfaceState.ToString() = "Disconnected") Then
                '    MetroFramework.MetroMessageBox.Show(Me, "WiFi Connection lost. Please establish a connection and try again.", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    Button1.Enabled = True
                '    ComboBox1.Enabled = True
                '    Exit Sub
                'End If
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
                            'foundit = 1
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
                            'If DataGridView2.RowCount > 0 Then
                            '    DataGridView2.Rows.Clear()
                            'End If
                            bandwidth = 0
                            Try
                                bandwidth = CInt(InputBox("Please enter the channel bandwidth in MHz.", "Bandwidth Information", 20))
                            Catch ex As Exception
                                MetroFramework.MetroMessageBox.Show(Me, "No relevant data provided. Kindly try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Button1.Enabled = True
                                ComboBox1.Enabled = True
                                Exit Sub
                            End Try
                            If ((bandwidth = 20) Or (bandwidth = 22) Or (bandwidth = 40) Or (bandwidth = 80) Or (bandwidth = 160) Or (bandwidth = 2160) Or (bandwidth = 8000)) Then
                                'DataGridView2.Rows.Add(GlobalVariables.ssidname, tMac, network.dot11BssPhyType, frequency, wlanIface.Channel, bandwidth, datarate, network.dot11BssType)
                                TextBox1.Text = GlobalVariables.ssidname
                                TextBox2.Text = tMac
                                TextBox3.Text = network.dot11BssPhyType
                                TextBox4.Text = frequency
                                TextBox5.Text = wlanIface.Channel
                                TextBox6.Text = bandwidth
                                TextBox7.Text = datarate
                                TextBox8.Text = network.dot11BssType
                                fullstring = "SSID = " & GlobalVariables.ssidname & vbNewLine & "MAC Address = " & tMac & vbNewLine & "PHY Type = " & network.dot11BssPhyType & vbNewLine & "Frequency = " & frequency & vbNewLine & "Channel = " & wlanIface.Channel & vbNewLine & "Bandwidth = " & bandwidth & vbNewLine & "Maximum Data Rate = " & datarate & vbNewLine & "BSS Type = " & network.dot11BssType & vbNewLine
                                'fullstring += GlobalVariables.ssidname & " | " & tMac & " | " & network.dot11BssPhyType & " | " & frequency & " | " & wlanIface.Channel & " | " & bandwidth & " | " & datarate & " | " & network.dot11BssType & vbNewLine
                                fullstring += "Date & Time,State,RSSI,Signal Quality,Avg Signal Quality" & vbNewLine
                                Exit For
                            Else
                                MetroFramework.MetroMessageBox.Show(Me, "No relevant data provided. Kindly try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Button1.Enabled = True
                                ComboBox1.Enabled = True
                                Exit Sub
                            End If
                        End If
                    End If
                Next
            Next
            'If foundit = 0 Then
            '    MetroFramework.MetroMessageBox.Show(Me, "WiFi Connection to """ & GlobalVariables.ssidname & """ has been lost. Please establish a connection and try again.", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Button1.Enabled = True
            '    ComboBox1.Enabled = True
            '    Exit Sub
            'End If
            DataGridView1.Rows.Clear()
            'fullstring = ""
            myPort = IO.Ports.SerialPort.GetPortNames()
            Dim x As New ComPortFinder
            Dim list = x.ComPortNames("16C0", "0483")
            For Each item As String In list
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
            Next

            If myserialPort.IsOpen Then
                timestamp = DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")    'Hebrew colon (׃) is from right to left
                For i As Integer = 1 To 9
                    myserialPort.Write("SET STATE" & i)
                    Thread.Sleep(25)
                    myserialPort.ReadLine()
                    Thread.Sleep(25)
                    count = 0
                    quality = New Double(count) {}
                    rssi = New Double(count) {}
                    For j As Integer = 1 To 10
                        For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                            wlanIface.Scan()
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
                                        fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & "STATE" & i & "," & network.rssi & "," & network.linkQuality & "," & avgquality & vbNewLine
                                        DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), "STATE" & i, network.rssi, network.linkQuality, avgquality) ', download / 8000000, Math.Round((download / (8000000 * totaltime)), 2))
                                        DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
                                        System.Array.Resize(Of Double)(quality, count + 1)
                                        System.Array.Resize(Of Double)(rssi, count + 1)
                                        Application.DoEvents()
                                        Thread.Sleep(200)
                                    End If
                                End If
                            Next
                        Next
                    Next
                    avgqualityarray(i - 1) = avgquality
                    avgrssiarray(i - 1) = avgrssi
                Next
            Else
                MetroFramework.MetroMessageBox.Show(Me, "No supported COM Ports available. Please check if Teensy 3.2 is connected and try again.", "COM Port Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Button1.Enabled = True
                ComboBox1.Enabled = True
                Exit Sub
            End If
            'avgqualityarray(avgqualityarray.Length - 1) = 100
            'MsgBox(avgqualityarray(avgqualityarray.Length - 1))
            'MsgBox(Array.IndexOf(avgqualityarray, avgqualityarray.Max()))
            'myserialPort.Write("SET STATE" & (Array.IndexOf(avgqualityarray, avgqualityarray.Max()) + 1))
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
            Dim z As Integer = 0
            While z <= rssiindex.Length - 1
                For i As Integer = 0 To qualityindex.Length - 1
                    If rssiindex(z) = qualityindex(i) Then
                        'myserialPort.Write("SET STATE" & (Array.IndexOf(avgrssiarray, avgrssiarray.Max()) + 1))
                        myserialPort.Write("SET STATE" & rssiindex(z))
                        Thread.Sleep(25)
                        fullstring += myserialPort.ReadLine()
                        Thread.Sleep(25)
                        TextBox9.Text = (rssiindex(z))
                        Exit While
                    End If
                Next
                z += 1
            End While

            wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)")
            wc.UseDefaultCredentials = True
            wc.Credentials = New NetworkCredential("admin", "admin")
            'elapsedStartTime = DateTime.Now
            foundit = 0
            fullstring += "Download Started..." & vbNewLine
            'If ComboBox1.SelectedIndex = 0 Then
            '    wc.DownloadFileAsync(New Uri("file://192.168.0.1/volume(sda1)/Download_Files/1mb.test"), tmp1)
            'ElseIf ComboBox1.SelectedIndex = 1 Then
            '    wc.DownloadFileAsync(New Uri("file://192.168.0.1/volume(sda1)/Download_Files/10mb.test"), tmp2)
            'ElseIf ComboBox1.SelectedIndex = 2 Then
            '    wc.DownloadFileAsync(New Uri("file://192.168.0.1/volume(sda1)/Download_Files/100mb.test"), tmp3)
            'Else
            '    wc.DownloadFileAsync(New Uri("file://192.168.0.1/volume(sda1)/Download_Files/1gb.test"), tmp4)
            'End If
            If ComboBox1.SelectedIndex = 0 Then
                wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/1mb.test"), tmp1)
            ElseIf ComboBox1.SelectedIndex = 1 Then
                wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/10mb.test"), tmp2)
            ElseIf ComboBox1.SelectedIndex = 2 Then
                wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/100mb.test"), tmp3)
            Else
                wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/1gb.test"), tmp4)
            End If

            myserialPort.Close()
        Catch ex As Exception
            'If ex.Message.Contains("The group or resource is not in the correct state to perform the requested operation") Then
            'MetroFramework.MetroMessageBox.Show(Me, "WiFi Connection to """ & GlobalVariables.ssidname & """ has been lost. Please establish a connection and try again.", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Else
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If
            myserialPort.Close()
            Button1.Enabled = True
            ComboBox1.Enabled = True
            'Exit Sub
        End Try

        'Button1.Text = "Stop"

        'totaltime = 0.0
        'Dim wc As New WebClient
        'AddHandler wc.DownloadProgressChanged, AddressOf wc_DownloadProgressChanged
        'AddHandler wc.DownloadFileCompleted, AddressOf wc_DownloadFileCompleted
        'download = 0
        'wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)")
        'elapsedStartTime = DateTime.Now
        'wc.DownloadFileAsync(New Uri("http://cachefly.cachefly.net/100mb.test"), "100mb.test")
        'wc.DownloadFileAsync(New Uri("http://cachefly.cachefly.net/100mb.test"), tmp, Stopwatch.StartNew)
        'wc.DownloadFile(New Uri("http://cachefly.cachefly.net/10mb.test"), tmp)
        'wc.DownloadFileAsync(New Uri("file://192.168.0.1/volume(sda1)/10mb.test"), tmp)
        'While (wc.IsBusy)
        'End While

        'Dim elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
        'MsgBox("Download Completed")
        'MsgBox("Total Bytes received = " & (10485760 / 1000000) & " MB. Seconds taken = " & Math.Round(elapsedtime.TotalSeconds, 2) & ". Download Speed = " & Math.Round((10485760 * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & "Mbps")
        'MetroButton1_Click(sender, e)
        'elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
        'MsgBox("Upload completed.")
        'MsgBox("Total Bytes sent = " & (10485760 / 1000000) & " MB. Seconds taken = " & Math.Round(elapsedtime.TotalSeconds, 2) & ". Upload Speed = " & Math.Round((10485760 * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & "Mbps")

        'Else
        'timestamp = timestamp & " to " & DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")
        'If (Not System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")) Then
        '    System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")
        'End If
        'File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\" & timestamp & ".txt", fullstring)
        ''Button1.Text = "Start"

        ''End If
        'Button1.Enabled = True
    End Sub

    Private tmp1 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "1mb.test")
    Private tmp2 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "10mb.test")
    Private tmp3 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "100mb.test")
    Private tmp4 = IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "1gb.test")
    'Private tmp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\"
    'Private Downloading As Boolean = False

    Private Sub wc_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.DownloadFileCompleted
        Dim elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
        'MsgBox("Downloading completed.")
        'MsgBox("Total Bytes received = " & (download / 1000000) & " MB. Seconds taken = " & Math.Round(elapsedtime.TotalSeconds, 2) & ". Download Speed = " & Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & "Mbps")
        'fullstring += "Total Bytes received = " & (download / 1000000) & " MB. Seconds taken = " & Math.Round(elapsedtime.TotalSeconds, 2) & ". Download Speed = " & Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & "Mbps" & vbNewLine
        fullstring += (download / 1000000) & "," & Math.Round(elapsedtime.TotalSeconds, 2) & "," & Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & vbNewLine
        TextBox10.Text = Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
        'ProgressBar1.Visible = False
        ProgressBar1.Value = 0

        'elapsedStartTime = DateTime.Now
        foundit = 0
        fullstring += "Upload Started..." & vbNewLine
        'If ComboBox1.SelectedIndex = 0 Then
        '    wc.UploadFileAsync(New Uri("file://192.168.0.1/volume(sda1)/Upload_Files/1mb.test"), tmp1)
        'ElseIf ComboBox1.SelectedIndex = 1 Then
        '    wc.UploadFileAsync(New Uri("file://192.168.0.1/volume(sda1)/Upload_Files/10mb.test"), tmp2)
        'ElseIf ComboBox1.SelectedIndex = 2 Then
        '    wc.UploadFileAsync(New Uri("file://192.168.0.1/volume(sda1)/Upload_Files/100mb.test"), tmp3)
        'Else
        '    wc.UploadFileAsync(New Uri("file://192.168.0.1/volume(sda1)/Upload_Files/1gb.test"), tmp4)
        'End If
        If ComboBox1.SelectedIndex = 0 Then
            wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/1mb.test"), tmp1)
        ElseIf ComboBox1.SelectedIndex = 1 Then
            wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/10mb.test"), tmp2)
        ElseIf ComboBox1.SelectedIndex = 2 Then
            wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/100mb.test"), tmp3)
        Else
            wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/1gb.test"), tmp4)
        End If

    End Sub

    Private Sub wc_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles wc.DownloadProgressChanged
        If foundit = 0 Then
            elapsedStartTime = DateTime.Now
            foundit = 1
            fullstring += "Total Bytes (MB),Time taken (s), Avg. Download Speed (Mbps)" & vbNewLine
        End If
        download = CDbl(e.BytesReceived)
        ProgressBar1.Visible = True
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub wc_UploadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles wc.UploadFileCompleted
        Dim elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
        'MsgBox("Upload completed.")
        'MsgBox("Total Bytes sent = " & (upload / 1000000) & " MB. Seconds taken = " & Math.Round(elapsedtime.TotalSeconds, 2) & ". Upload Speed = " & Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & "Mbps")
        'fullstring += "Total Bytes sent = " & (upload / 1000000) & " MB. Seconds taken = " & Math.Round(elapsedtime.TotalSeconds, 2) & ". Upload Speed = " & Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & "Mbps"
        fullstring += (upload / 1000000) & "," & Math.Round(elapsedtime.TotalSeconds, 2) & "," & Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
        TextBox11.Text = Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
        'ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        timestamp = timestamp & " to " & DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")
        If (Not System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")) Then
            System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")
        End If
        'File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\" & timestamp & ".txt", fullstring)
        Dim sw As New StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\" & timestamp & ".csv")
        sw.Write(fullstring)
        sw.Close()
        Button1.Enabled = True
        ComboBox1.Enabled = True
        myserialPort.Close()
        Application.DoEvents()  ' Give port time to close down
        Thread.Sleep(200)
        MetroFramework.MetroMessageBox.Show(Me, "Test results are saved under """ & Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\"" as " & """" & timestamp & ".csv""", "TEST COMPLETE", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub wc_UploadProgressChanged(sender As Object, e As UploadProgressChangedEventArgs) Handles wc.UploadProgressChanged
        If foundit = 0 Then
            elapsedStartTime = DateTime.Now
            foundit = 1
            fullstring += "Total Bytes (MB),Time taken (s), Avg. Upload Speed (Mbps)" & vbNewLine
        End If
        upload = CDbl(e.BytesSent)
        ProgressBar1.Visible = True
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    'Private Sub myWebClient_UploadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles myWebClient.UploadFileCompleted
    '    Dim elapsedtime = DateTime.Now.Subtract(elapsedStartTime)
    '    MsgBox("Upload completed.")
    '    MsgBox("Total Bytes sent = " & (upload / 1000000) & " MB. Seconds taken = " & Math.Round(elapsedtime.TotalSeconds, 2) & ". Upload Speed = " & Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2) & "Mbps")
    '    ProgressBar1.Visible = False
    '    ProgressBar1.Value = 0
    'End Sub

    'Private Sub myWebClient_UploadProgressChanged(sender As Object, e As UploadProgressChangedEventArgs) Handles myWebClient.UploadProgressChanged
    '    upload = CDbl(e.BytesSent)
    '    ProgressBar1.Visible = True
    '    ProgressBar1.Value = e.ProgressPercentage
    'End Sub

    'Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
    '    MetroButton1.Enabled = False
    '    Try
    '        myWebClient.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)")
    '        myWebClient.UseDefaultCredentials = True
    '        myWebClient.Credentials = New NetworkCredential("admin", "admin")
    '        elapsedStartTime = DateTime.Now
    '        'Dim responseArray As Byte() = myWebClient.UploadFile("http://www.keepandshare.com/addr4/show.php?i=2704040", tmp)
    '        myWebClient.UploadFileAsync(New Uri("file://192.168.0.1/volume(sda1)/10mb.test"), tmp)
    '        While (myWebClient.IsBusy)

    '        End While
    '    Catch ex As Exception
    '        MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try
    '    MetroButton1.Enabled = True
    'End Sub

    'Private Sub Application_Idle(ByVal sender As Object, ByVal e As EventArgs)
    '    If Button1.Text = "Stop" Then
    '        'Try
    '        For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
    '            Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()
    '            For Each network As Wlan.WlanBssEntry In wlanBssEntries
    '                If (Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength)) = GlobalVariables.ssidname) Then 'AndAlso (getMACaddress(network.dot11Bssid) = GlobalVariables.macadd) Then
    '                    Dim macAddr As Byte() = network.dot11Bssid
    '                    Dim tMac As String = ""
    '                    For i As Integer = 0 To macAddr.Length - 1
    '                        If tMac = "" Then
    '                            tMac += macAddr(i).ToString("x2").PadLeft(2, "0"c).ToUpper()
    '                        Else
    '                            tMac += ":" & macAddr(i).ToString("x2").PadLeft(2, "0"c).ToUpper()
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
    '                        avgquality = Math.Round(avgquality, 1)
    '                        avgrssi /= count
    '                        avgrssi = Math.Round(avgrssi, 1)

    '                        'Dim wc As New WebClient
    '                        'AddHandler wc.DownloadProgressChanged, AddressOf wc_ProgressChanged
    '                        'AddHandler wc.DownloadFileCompleted, AddressOf wc_DownloadDone
    '                        'wc.DownloadFileAsync(New Uri("http://cachefly.cachefly.net/100mb.test"), tmp, Stopwatch.StartNew)
    '                        'elapsedStartTime = DateTime.Now
    '                        'While Downloading

    '                        'End While

    '                        fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & " " & network.rssi & " " & network.linkQuality & " " & avgquality & vbNewLine
    '                        DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), network.rssi, network.linkQuality, avgquality) ', download / 8000000, Math.Round((download / (8000000 * totaltime)), 2))
    '                        DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
    '                        System.Array.Resize(Of Double)(quality, count + 1)
    '                        System.Array.Resize(Of Double)(rssi, count + 1)
    '                    End If
    '                End If
    '            Next

    '        Next
    '        'Catch ex As Exception
    '        '    MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        'End Try
    '    End If
    '    'For i As Integer = 0 To 10
    '    'Application.DoEvents()
    '    Thread.Sleep(200)
    '    'Next
    'End Sub

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
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

    'Dim SW As Stopwatch
    'Private Sub client_ProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
    '    download = CDbl((e.BytesReceived / (DirectCast(e.UserState, Stopwatch).ElapsedMilliseconds / 1000.0#)).ToString("#"))

    'End Sub
    'Private Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
    '    'MessageBox.Show("Download Complete")
    'End Sub

    'Function downloadspeed() As Long
    '    Dim req As WebRequest = WebRequest.Create("http://cachefly.cachefly.net/100mb.test") 'Make a request for the url of the file to be downloaded
    '    Dim resp As WebResponse = req.GetResponse 'Ask for the response
    '    If (Not System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")) Then
    '        System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")
    '    End If
    '    Dim fs As New FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\Data.txt", FileMode.CreateNew) 'Create a new FileStream that writes to the desired download path

    '    Dim buffer(8192) As Byte 'Make a buffer
    '    Dim downloadedsize As Long = 0
    '    Dim downloadedTime As Long = 0

    '    Dim dlSpeed As Long = 0
    '    Dim currentSize As Long = 0 'Size that has been downloaded
    '    Dim totalSize As Long = req.ContentLength 'Total size of the file that has to be downloaded

    '    While currentSize < totalSize
    '        Dim read As Integer = resp.GetResponseStream.Read(buffer, 0, 8192) 'Read the buffer from the response the WebRequest gave you

    '        fs.Write(buffer, 0, read) 'Write to filestream that you declared at the beginning of the DoWork sub

    '        currentSize += read

    '        downloadedsize += read
    '        downloadedTime += 1 'Add 1 millisecond for every cycle the While field makes

    '        If downloadedTime = 1000 Then 'Then, if downloadedTime reaches 1000 then it will call this part
    '            dlSpeed = (downloadedsize / TimeSpan.FromMilliseconds(downloadedTime).TotalSeconds) 'Calculate the download speed by dividing the downloadedSize by the total formatted seconds of the downloadedTime

    '            downloadedTime = 0 'Reset downloadedTime and downloadedSize
    '            downloadedsize = 0
    '        End If
    '    End While

    '    fs.Close() 'Close the FileStream first, or the FileStream will crash.
    '    resp.Close() 'Close the response
    '    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\Data.txt")
    '    Return (dlSpeed)
    'End Function

End Class

'Public Class Wireless
'    Public Shared main As Wlan()
'End Class