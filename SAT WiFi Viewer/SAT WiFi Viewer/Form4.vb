Imports System.Collections.ObjectModel
Imports System.IO
Imports System.IO.Ports
Imports System.Net
Imports System.Text
Imports System.Threading
Imports NativeWifi

Public Class Form4

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
    'Dim url As Uri
    'Dim tmp As String
    Dim WithEvents wc As New WebClient
    Dim infoReader As System.IO.FileInfo
    Dim state As String
    Dim values() As String

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
                GlobalVariables.period = "Unlimited"
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
                'If (Not Directory.Exists("\\192.168.31.1\tddownload\TDTEMP\Download_Files\")) Or (Not Directory.Exists("\\192.168.31.1\tddownload\TDTEMP\Upload_Files\")) Then
                '    MetroFramework.MetroMessageBox.Show(Me, "Unable to access \\192.168.31.1\tddownload\TDTEMP\. Kindly verify if the network is available and try again.", "Network Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If (Not Directory.Exists(GlobalVariables.dfolder)) Or (Not Directory.Exists(GlobalVariables.ufolder)) Then
                    MetroFramework.MetroMessageBox.Show(Me, "Unable to access " & GlobalVariables.ssidname & " Or " & GlobalVariables.macadd & ". Kindly verify if the network is available and try again.", "Network Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Button1.Enabled = True
                    MenuStrip1.Enabled = True
                    Exit Sub
                End If
            End If
            timestamp = DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")    'Hebrew colon (׃) is from right to left
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                wlanIface.Scan()
                Thread.Sleep(1000)
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

            DataGridView1.Rows.Clear()
            If NoneSelectedToolStripMenuItem.Checked = False Then
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
                    If STATE1ToolStripMenuItem.Checked = True Then
                        state = 1
                        myserialPort.Write("SET STATE1")
                    ElseIf STATE2ToolStripMenuItem.Checked = True Then
                        state = 2
                        myserialPort.Write("SET STATE2")
                    ElseIf STATE3ToolStripMenuItem.Checked = True Then
                        state = 3
                        myserialPort.Write("SET STATE3")
                    ElseIf STATE4ToolStripMenuItem.Checked = True Then
                        state = 4
                        myserialPort.Write("SET STATE4")
                    ElseIf STATE5ToolStripMenuItem.Checked = True Then
                        state = 5
                        myserialPort.Write("SET STATE5")
                    ElseIf STATE6ToolStripMenuItem.Checked = True Then
                        state = 6
                        myserialPort.Write("SET STATE6")
                    ElseIf STATE7ToolStripMenuItem.Checked = True Then
                        state = 7
                        myserialPort.Write("SET STATE7")
                    ElseIf STATE8ToolStripMenuItem.Checked = True Then
                        state = 8
                        myserialPort.Write("SET STATE8")
                    ElseIf STATE9ToolStripMenuItem.Checked = True Then
                        state = 9
                        myserialPort.Write("SET STATE9")
                    End If
                    Thread.Sleep(25)
                    myserialPort.ReadLine()
                    Thread.Sleep(25)
                End If
            Else
                state = "None"
            End If
            TextBox9.Text = state
            If state = "None" Then
                fullstring += "No State selected" & vbNewLine
            Else
                fullstring += "STATE " & state & " selected" & vbNewLine
            End If
            count = 0
            quality = New Double(count) {}
            rssi = New Double(count) {}
            If MonitorSSIDToolStripMenuItem.Checked = True Then
                fullstring += "Date & Time,RSSI,Signal Quality,Avg Signal Quality" & vbNewLine
                'First 10 times are compulsory
                foundit = 0
                While foundit < 10
                    'For j As Integer = 1 To 10
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
                                    fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & network.rssi & "," & network.linkQuality & "," & avgquality & vbNewLine
                                    DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), network.rssi, network.linkQuality, avgquality) ', download / 8000000, Math.Round((download / (8000000 * totaltime)), 2))
                                    DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
                                    System.Array.Resize(Of Double)(quality, count + 1)
                                    System.Array.Resize(Of Double)(rssi, count + 1)
                                    Application.DoEvents()
                                    Thread.Sleep(200)
                                    foundit += 1
                                End If
                            End If
                        Next
                    Next
                    'Next
                End While
                Do
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
                                    fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & "," & network.rssi & "," & network.linkQuality & "," & avgquality & vbNewLine
                                    DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), network.rssi, network.linkQuality, avgquality) ', download / 8000000, Math.Round((download / (8000000 * totaltime)), 2))
                                    DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
                                    System.Array.Resize(Of Double)(quality, count + 1)
                                    System.Array.Resize(Of Double)(rssi, count + 1)
                                    Application.DoEvents()
                                    Thread.Sleep(200)
                                End If
                            End If
                        Next
                    Next
                Loop Until Toggle1.Checked = False
            End If

            If SpeedTestStatusToolStripMenuItem.Checked = True Then
                wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)")
                wc.UseDefaultCredentials = True
                wc.Credentials = New NetworkCredential("admin", "admin")
                foundit = 0
                fullstring += "Download Started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Download Speed (Mbps)" & vbNewLine
                elapsedStartTime = DateTime.Now
                'If MBToolStripMenuItem.Checked = True Then
                '    wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/1mb.test"), tmp1)
                'ElseIf MBToolStripMenuItem1.Checked = True Then
                '    wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/10mb.test"), tmp2)
                'ElseIf MBToolStripMenuItem2.Checked = True Then
                '    wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/100mb.test"), tmp3)
                'Else
                '    wc.DownloadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Download_Files/1gb.test"), tmp4)
                'End If
                If GlobalVariables.size = "1 MB" Then
                    wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1mb.test"), tmp1)
                ElseIf GlobalVariables.size = "10 MB" Then
                    wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "10mb.test"), tmp2)
                ElseIf GlobalVariables.size = "100 MB" Then
                    wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "100mb.test"), tmp3)
                Else
                    wc.DownloadFileAsync(New Uri("file:" & GlobalVariables.dfolder.Replace("\", "/") & "1gb.test"), tmp4)
                End If
            Else
                Button1.Enabled = True
                MenuStrip1.Enabled = True
                timestamp = timestamp & " to " & DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")
                If (Not System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")) Then
                    System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")
                End If
                Dim sw As New StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\" & timestamp & ".csv")
                sw.Write(fullstring)
                sw.Close()
                myserialPort.Close()
                Application.DoEvents()  ' Give port time to close down
                Thread.Sleep(200)
                MetroFramework.MetroMessageBox.Show(Me, "Test results are saved under """ & Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\"" as " & """" & timestamp & ".csv""", "TEST COMPLETE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
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
        TextBox10.Text = Math.Round((download * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
        'ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        If foundit = 1 Then
            fullstring += "Download aborted due to the set termination period of " & GlobalVariables.period & vbNewLine
            TextBox10.Text = "Aborted due to the set time limit of " & GlobalVariables.period
        End If

        foundit = 0
        fullstring += "Upload Started..." & vbNewLine & "Total Bytes (MB),Time taken (s), Avg. Upload Speed (Mbps)" & vbNewLine
        elapsedStartTime = DateTime.Now
        'If MBToolStripMenuItem.Checked = True Then
        '    wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/1mb.test"), tmp1)
        'ElseIf MBToolStripMenuItem1.Checked = True Then
        '    wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/10mb.test"), tmp2)
        'ElseIf MBToolStripMenuItem2.Checked = True Then
        '    wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/100mb.test"), tmp3)
        'Else
        '    wc.UploadFileAsync(New Uri("file://192.168.31.1/tddownload/TDTEMP/Upload_Files/1gb.test"), tmp4)
        'End If
        If GlobalVariables.size = "1 MB" Then
            wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "1mb.test"), tmp1)
        ElseIf GlobalVariables.size = "10 MB" Then
            wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "10mb.test"), tmp2)
        ElseIf GlobalVariables.size = "100 MB" Then
            wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "100mb.test"), tmp3)
        Else
            wc.UploadFileAsync(New Uri("file:" & GlobalVariables.ufolder.Replace("\", "/") & "1gb.test"), tmp4)
        End If
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
        TextBox11.Text = Math.Round((upload * 8 / (elapsedtime.TotalSeconds * 1000000)), 2)
        'ProgressBar1.Visible = False
        ProgressBar1.Value = 0
        If foundit = 1 Then
            fullstring += "Upload aborted due to the set termination period of " & GlobalVariables.period
            TextBox11.Text = "Aborted due to the set time limit of " & GlobalVariables.period
        End If

        Button1.Enabled = True
        MenuStrip1.Enabled = True
        timestamp = timestamp & " to " & DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")
        If (Not System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")) Then
            System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")
        End If
        Dim sw As New StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\" & timestamp & ".csv")
        sw.Write(fullstring)
        sw.Close()
        myserialPort.Close()
        Application.DoEvents()  ' Give port time to close down
        Thread.Sleep(200)
        MetroFramework.MetroMessageBox.Show(Me, "Test results are saved under """ & Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\"" as " & """" & timestamp & ".csv""", "TEST COMPLETE", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        End If
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
    End Sub

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
            Application.DoEvents()  ' Give port time to close down
            Thread.Sleep(200)
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class