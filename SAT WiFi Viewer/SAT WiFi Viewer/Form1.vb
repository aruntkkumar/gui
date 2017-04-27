Imports System
Imports System.Text
Imports NativeWifi
Imports System.Collections.ObjectModel
Imports System.Net.NetworkInformation
Imports System.Threading

Public Class Form1

    Dim tMac As String
    Dim frequency As Double
    Dim channelnumber As Integer
    'Dim signalstrength As Integer
    Dim ssid As String
    Dim test As String
    Dim datarate As Integer
    Dim counter As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler System.Windows.Forms.Application.Idle, AddressOf Application_Idle
        Try
            'Dim adapters() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            'Dim wirelessfound As Boolean = False
            'For Each adapter As NetworkInterface In adapters
            '    If adapter.NetworkInterfaceType = NetworkInterfaceType.Wireless80211 Then
            '        'MessageBox.Show("Name " & adapter.Name)
            '        'MessageBox.Show("Status:" & adapter.OperationalStatus.ToString)
            '        'MessageBox.Show("Speed:" & adapter.Speed.ToString())
            '        'MessageBox.Show(adapter.GetIPProperties.GetIPv4Properties.ToString)
            '        'MessageBox.Show(adapter.GetIPProperties.GetIPv4Properties.IsDhcpEnabled.ToString)
            '        'If adapter.GetIPProperties.GetIPv4Properties.IsDhcpEnabled Then
            '        '    MsgBox("Dynamic IP")
            '        'Else
            '        '    MsgBox("Static IP")
            '        'End If
            '        wirelessfound = True
            '    End If
            'Next
            'If Not wirelessfound Then
            '    MetroFramework.MetroMessageBox.Show(Me, "No wireless adaptor detected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If

            'Dim wlan = New WlanClient()
            'Dim connectedSsids As Collection(Of [String]) = New Collection(Of String)()

            'Dim proc As New Process
            'proc.StartInfo.CreateNoWindow = True
            'proc.StartInfo.FileName = "netsh"
            'proc.StartInfo.Arguments = "interface set interface name=""""Wireless Network Connection 2"""" admin=disabled"
            'proc.StartInfo.RedirectStandardOutput = True
            'proc.StartInfo.UseShellExecute = False
            'proc.Start()
            'proc.WaitForExit()

            'Dim profname As String
            'Dim xml As String
            'For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
            '    ' Retrieves XML configurations of existing profiles.
            '    ' This can assist you in constructing your own XML configuration
            '    ' (that is, it will give you an example to follow).
            '    For Each profileInfo As Wlan.WlanProfileInfo In wlanIface.GetProfiles()
            '        profname = profileInfo.profileName     ' this is typically the network's SSID
            '        Xml = wlanIface.GetProfileXml(profileInfo.profileName)
            '        MsgBox(profname)
            '        MsgBox(Xml)
            '        wlanIface.DeleteProfile(profname)
            '        MsgBox(profname & " is deleted.")
            '    Next
            'Next

            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                wlanIface.Scan()

                'Dim proc As New Process
                'proc.StartInfo.CreateNoWindow = True
                'proc.StartInfo.FileName = "ipconfig"
                'proc.StartInfo.Arguments = " /release"
                'proc.StartInfo.RedirectStandardOutput = True
                'proc.StartInfo.UseShellExecute = False
                'proc.Start()
                'proc.WaitForExit()

                '                AddHandler wlanIface.WlanNotification, AddressOf wlanIface_WlanNotification
                'checkagain1:
                '                If Wlan.WlanNotificationCodeAcm.ScanComplete Then
                '                    'MsgBox("Scan Complete")
                '                Else
                '                    GoTo checkagain1
                '                End If
                ' Lists all networks with WEP security
                'Dim networks As Wlan.WlanAvailableNetwork() = wlanIface.GetAvailableNetworkList(0)
                'For Each network As Wlan.WlanAvailableNetwork In networks
                '    Dim ssid As Wlan.Dot11Ssid = network.dot11Ssid
                '    Dim networkName As String = Encoding.ASCII.GetString(ssid.SSID, 0, CInt(ssid.SSIDLength))

                '    'testname = networkName & network.dot11DefaultCipherAlgorithm.ToString() & network.wlanSignalQuality & "%"
                '    If DataGridView1.RowCount <= 0 Then
                '        DataGridView1.Rows.Add(networkName, network.dot11DefaultCipherAlgorithm.ToString(), network.wlanSignalQuality & "%")
                '    End If
                '    test = 1
                '    For Each row As DataGridViewRow In DataGridView1.Rows
                '        If row.Cells(0).Value.ToString().Contains(networkName) Then
                '            test = 0
                '            Exit For
                '        End If
                '    Next
                '    If test = 1 Then
                '        DataGridView1.Rows.Add(networkName, network.dot11DefaultCipherAlgorithm.ToString(), network.wlanSignalQuality & "%")
                '    End If
                'Next

                'RetrieveNetworks()
                Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()

                For Each network As Wlan.WlanBssEntry In wlanBssEntries
                    Dim rss As Integer = network.rssi
                    Dim macAddr As Byte() = network.dot11Bssid
                    tMac = ""
                    frequency = network.chCenterFrequency / 1000000
                    For i As Integer = 0 To macAddr.Length - 1
                        If tMac = "" Then
                            tMac += macAddr(i).ToString("x2").PadLeft(2, "0"c).ToUpper()
                        Else
                            tMac += ":" & macAddr(i).ToString("x2").PadLeft(2, "0"c).ToUpper()
                        End If
                    Next
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

                    ssid = Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength))
                    'ssid = System.Text.Encoding.ASCII.GetString(network.dot11Ssid.SSID).ToString()
                    'ssid = ssid.Replace(vbNullChar, "")

                    'Dim available As Integer = 0
                    'For Each row As DataGridViewRow In DataGridView1.Rows
                    '    If row.Cells(0).Value.ToString().Equals(ssid) AndAlso row.Cells(1).Value.ToString().Equals(tMac) Then
                    '        available = 1
                    '        Exit For
                    '    End If
                    'Next
                    'If available = 0 Then
                    'signalstrength = 0
                    'Dim networks As Wlan.WlanAvailableNetwork() = wlanIface.GetAvailableNetworkList(0)
                    'For Each network2 As Wlan.WlanAvailableNetwork In networks
                    '    If Encoding.ASCII.GetString(network2.dot11Ssid.SSID, 0, CInt(network2.dot11Ssid.SSIDLength)) = ssid Then
                    '        signalstrength = network2.wlanSignalQuality
                    '        MsgBox(network2.ToString())
                    '    End If
                    'Next
                    datarate = 0
                    For i As Integer = 0 To network.wlanRateSet.Rates.Length + 10
                        'datarate += network.wlanRateSet.GetRateInMbps(i) & " " 'If accessed as a string of values
                        If network.wlanRateSet.GetRateInMbps(i) > datarate Then
                            datarate = network.wlanRateSet.GetRateInMbps(i)
                        End If
                    Next
                    DataGridView1.Rows.Add(ssid, tMac, network.dot11BssPhyType, rss, network.linkQuality, frequency, channelnumber, datarate, network.dot11BssType)
                    'End If
                Next
            Next
            DataGridView1.ClearSelection()
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Shared Sub wlanIface_WlanNotification(notifyData As Wlan.WlanNotificationData)
        If notifyData.NotificationCode = CInt(Wlan.WlanNotificationCodeAcm.ScanComplete) Then
            'MsgBox("Scan Complete!")
            'CreateObject("WScript.Shell").Popup("Welcome", 0.5, "Title")
        End If
    End Sub

    Private Sub Application_Idle(ByVal sender As Object, ByVal e As EventArgs)
Testagain:
        If Not Me.IsDisposed Then
            Try
                'Using wlan1 = New WlanClient()
                'Dim wlan As New WlanClient()
                'For Each wlanIface As WlanClient.WlanInterface In wlan1.Interfaces
                For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                    If counter = 10 Then
                        wlanIface.Scan()
                        '                        AddHandler wlanIface.WlanNotification, AddressOf wlanIface_WlanNotification
                        'checkagain2:
                        '                        If Wlan.WlanNotificationCodeAcm.ScanComplete Then
                        '                            'MsgBox("Scan Complete")
                        '                        Else
                        '                            GoTo checkagain2
                        '                        End If
                        counter = 0
                    End If

                    'Dim proc As New Process
                    'proc.StartInfo.CreateNoWindow = True
                    'proc.StartInfo.FileName = "netsh"
                    'proc.StartInfo.Arguments = "lan reconnect"
                    'proc.StartInfo.RedirectStandardOutput = True
                    'proc.StartInfo.UseShellExecute = False
                    'proc.Start()
                    'proc.WaitForExit()


                    Dim wlanBssEntries As Wlan.WlanBssEntry() = wlanIface.GetNetworkBssList()

                    For Each network As Wlan.WlanBssEntry In wlanBssEntries
                        Dim rss As Integer = network.rssi
                        Dim macAddr As Byte() = network.dot11Bssid
                        tMac = ""
                        frequency = network.chCenterFrequency / 1000000
                        For i As Integer = 0 To macAddr.Length - 1
                            If tMac = "" Then
                                tMac += macAddr(i).ToString("x2").PadLeft(2, "0"c).ToUpper()
                            Else
                                tMac += ":" & macAddr(i).ToString("x2").PadLeft(2, "0"c).ToUpper()
                            End If
                        Next
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

                        ssid = Encoding.ASCII.GetString(network.dot11Ssid.SSID, 0, CInt(network.dot11Ssid.SSIDLength))

                        Dim available As Integer = 0
                        Dim rowindex As Integer = -1
                        For Each row As DataGridViewRow In DataGridView1.Rows
                            If row.Cells(0).Value.ToString().Equals(ssid) AndAlso row.Cells(1).Value.ToString().Equals(tMac) Then
                                available = 1
                                rowindex = row.Index
                                Exit For
                            End If
                        Next
                        'signalstrength = 0
                        'Dim networks As Wlan.WlanAvailableNetwork() = wlanIface.GetAvailableNetworkList(0)
                        'For Each network2 As Wlan.WlanAvailableNetwork In networks
                        '    If Encoding.ASCII.GetString(network2.dot11Ssid.SSID, 0, CInt(network2.dot11Ssid.SSIDLength)) = ssid Then
                        '        signalstrength = network2.wlanSignalQuality
                        '    End If
                        'Next
                        datarate = 0
                        For i As Integer = 0 To network.wlanRateSet.Rates.Length + 10
                            If network.wlanRateSet.GetRateInMbps(i) > datarate Then
                                datarate = network.wlanRateSet.GetRateInMbps(i)
                            End If
                        Next
                        If available = 0 Then
                            If Me.IsDisposed = True Then
                                Application.DoEvents()
                                Exit Sub
                            End If
                            DataGridView1.Rows.Add(ssid, tMac, network.dot11BssPhyType, rss, network.linkQuality, frequency, channelnumber, datarate, network.dot11BssType)
                        Else
                            'If signalstrength <> 999 Then
                            'DataGridView1.Rows(rowindex).Cells(1).Value = network.linkQuality
                            'End If
                            'MsgBox(ssid & ", " & rss & ", " & network.linkQuality)
                            If DataGridView1.Rows(rowindex).Cells(3).Value <> rss Or DataGridView1.Rows(rowindex).Cells(4).Value <> network.linkQuality Then
                                DataGridView1.Rows(rowindex).Cells(3).Value = rss
                                DataGridView1.Rows(rowindex).Cells(4).Value = network.linkQuality
                            End If

                        End If

                    Next
                Next
                'GC.SuppressFinalize(wlan1)
                'Close()
                'End Using
                counter += 1
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
            Application.DoEvents()
            Thread.Sleep(100)
        Else
            Exit Sub
        End If
        GoTo Testagain
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex = -1 Then
            Exit Sub
        End If
        GlobalVariables.ssidname = DataGridView1.SelectedRows(0).Cells(0).Value.ToString
        GlobalVariables.macadd = DataGridView1.SelectedRows(0).Cells(1).Value.ToString.Replace(":", "")
        'Dim client = New WlanClient()
        Try
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                'wlanIface.Scan()
                For Each profileInfo As Wlan.WlanProfileInfo In wlanIface.GetProfiles()
                    Dim name As String = profileInfo.profileName
                    Dim xml As String = wlanIface.GetProfileXml(profileInfo.profileName)
                    If name = GlobalVariables.ssidname Then 'Need to check the MAC address of the profileName
                        wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, xml, True)
                        wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, name)
                        'End If
                        Application.DoEvents()
                        Thread.Sleep(1000)
                        If DebugToolStripMenuItem.Checked = True Then
                            Form4.Show()
                        ElseIf DemoToolStripMenuItem.Checked = True Then
                            Form6.Show()
                        Else
                            Form3.Show()
                        End If
                        Me.Close()
                        Me.Dispose()
                        Exit Sub
                        'Catch ex As Exception
                        '    MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'End Try
                    End If
                Next
            Next
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End Try
        Form2.Show()
        Me.Close()
    End Sub

    Private Sub DataGridView1_Sorted(sender As Object, e As EventArgs) Handles DataGridView1.Sorted
        DataGridView1.ClearSelection()
    End Sub

    'Dim selectionsMade As Boolean = False

    'Private Sub DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDown
    '    If (e.Button = MouseButtons.Left) AndAlso (e.RowIndex = -1) Then
    '        selectionsMade = (DirectCast(sender, DataGridView).SelectedRows.Count > 0)
    '    End If
    'End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        DataGridView1.ClearSelection()
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseDoubleClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseDoubleClick
        DataGridView1.ClearSelection()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex = -1 Then
            DataGridView1.ClearSelection()
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub DebugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DebugToolStripMenuItem.Click
        If DebugToolStripMenuItem.Checked = False Then
            DebugToolStripMenuItem.Checked = True
            GlobalVariables.debug = True
            DemoToolStripMenuItem.Checked = False
            GlobalVariables.demo = False
        Else
            DebugToolStripMenuItem.Checked = False
            GlobalVariables.debug = False
        End If
    End Sub

    Private Sub DemoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DemoToolStripMenuItem.Click
        If DemoToolStripMenuItem.Checked = False Then
            DemoToolStripMenuItem.Checked = True
            GlobalVariables.demo = True
            DebugToolStripMenuItem.Checked = False
            GlobalVariables.debug = False
        Else
            DemoToolStripMenuItem.Checked = False
            GlobalVariables.demo = False
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            GlobalVariables.ssidname = DataGridView1.SelectedRows(0).Cells(0).Value.ToString
            GlobalVariables.macadd = DataGridView1.SelectedRows(0).Cells(1).Value.ToString.Replace(":", "")
            'Dim client = New WlanClient()
            Try
                For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                    'wlanIface.Scan()
                    For Each profileInfo As Wlan.WlanProfileInfo In wlanIface.GetProfiles()
                        Dim name As String = profileInfo.profileName
                        Dim xml As String = wlanIface.GetProfileXml(profileInfo.profileName)
                        If name = GlobalVariables.ssidname Then 'Need to check the MAC address of the profileName
                            'Try
                            'If wlanIface.InterfaceState.ToString <> "Connected" Then)
                            'checkagain:
                            '                        If Wlan.WlanNotificationCodeAcm.ScanComplete Then
                            '                            MsgBox("Scan Complete")
                            '                        Else
                            '                            GoTo checkagain
                            '                        End If
                            wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, xml, True)
                            wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, name)
                            'End If
                            Application.DoEvents()
                            Thread.Sleep(1000)
                            If DebugToolStripMenuItem.Checked = True Then
                                Form4.Show()
                            ElseIf DemoToolStripMenuItem.Checked = True Then
                                Form6.Show()
                            Else
                                Form3.Show()
                            End If
                            Me.Close()
                            Me.Dispose()
                            Exit Sub
                            'Catch ex As Exception
                            '    MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            'End Try
                        End If
                    Next
                Next
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
                If DataGridView1.CurrentRow.Index < 0 Then
                    DataGridView1.Rows(DataGridView1.CurrentRow.Index - 1).Selected = True
                End If
            End Try
            Form2.Show()
            Me.Close()
        End If
    End Sub

    'Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    '    'Me.Dispose()
    '    'GC.SuppressFinalize(Me)
    '    For Each d As [Delegate] In FindClicked.GetInvocationList()
    '        FindClicked -= DirectCast(d, FindClickedHandler)
    '    Next
    'End Sub

    'Private Function RetrieveNetworks()
    '    Dim proc As New Process
    '    proc.StartInfo.CreateNoWindow = True
    '    proc.StartInfo.FileName = "netsh"
    '    proc.StartInfo.Arguments = "wlan show networks mode=ssid"
    '    proc.StartInfo.RedirectStandardOutput = True
    '    proc.StartInfo.UseShellExecute = False
    '    proc.Start()
    '    MsgBox(proc.StandardOutput.ReadToEnd())
    '    Return (proc.StandardOutput.ReadToEnd())
    '    proc.WaitForExit()
    'End Function

    'Private Shared Sub wlanIface_WlanNotification(notifyData As Wlan.WlanNotificationData)
    '    If notifyData.NotificationCode = CInt(Wlan.WlanNotificationCodeAcm.ScanComplete) Then
    '        MsgBox("Scan Complete!")
    '    End If
    'End Sub

End Class

Public Class GlobalVariables
    Public Shared ssidname As String = ""
    Public Shared macadd As String = ""
    Public Shared debug As Boolean = False
    Public Shared demo As Boolean = False
    Public Shared dfolder As String = ""
    Public Shared ufolder As String = ""
    Public Shared period As String = ""
    Public Shared size As String = ""
    Public Shared detailed As Boolean = False
    Public Shared okbutton As String = "cancel"
End Class

Public Class WiFi
    Public Shared client As New WlanClient()
End Class