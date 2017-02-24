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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler System.Windows.Forms.Application.Idle, AddressOf Application_Idle
        Try
            Dim adapters() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            Dim wirelessfound As Boolean = False
            For Each adapter As NetworkInterface In adapters
                If adapter.NetworkInterfaceType = NetworkInterfaceType.Wireless80211 Then
                    'MessageBox.Show("Name " & adapter.Name)
                    'MessageBox.Show("Status:" & adapter.OperationalStatus.ToString)
                    'MessageBox.Show("Speed:" & adapter.Speed.ToString())
                    'MessageBox.Show(adapter.GetIPProperties.GetIPv4Properties.ToString)
                    'MessageBox.Show(adapter.GetIPProperties.GetIPv4Properties.IsDhcpEnabled.ToString)
                    'If adapter.GetIPProperties.GetIPv4Properties.IsDhcpEnabled Then
                    '    MsgBox("Dynamic IP")
                    'Else
                    '    MsgBox("Static IP")
                    'End If
                    wirelessfound = True
                End If
            Next
            If Not wirelessfound Then
                MetroFramework.MetroMessageBox.Show(Me, "No wireless adaptor detected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            'Dim wlan = New WlanClient()
            'Dim connectedSsids As Collection(Of [String]) = New Collection(Of String)()
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
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

    Private Sub Application_Idle(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'Dim wlan = New WlanClient()
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
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
                            Exit Sub
                        End If
                        DataGridView1.Rows.Add(ssid, tMac, network.dot11BssPhyType, rss, network.linkQuality, frequency, channelnumber, datarate, network.dot11BssType)
                    Else
                        'If signalstrength <> 999 Then
                        'DataGridView1.Rows(rowindex).Cells(1).Value = network.linkQuality
                        'End If
                        DataGridView1.Rows(rowindex).Cells(3).Value = rss
                        DataGridView1.Rows(rowindex).Cells(4).Value = network.linkQuality
                    End If
                Next
            Next

        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        'Application.DoEvents()
        Thread.Sleep(100)
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
                For Each profileInfo As Wlan.WlanProfileInfo In wlanIface.GetProfiles()
                    Dim name As String = profileInfo.profileName
                    Dim xml As String = wlanIface.GetProfileXml(profileInfo.profileName)
                    If name = GlobalVariables.ssidname Then 'Need to check the MAC address of the profileName
                        Try
                            wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, xml, True)
                            wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, name)
                            Form3.Show()
                            Me.Close()
                            Exit Sub
                        Catch ex As Exception
                            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End Try
                    End If
                Next
            Next
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

End Class

Public Class GlobalVariables
    Public Shared ssidname As String = ""
    Public Shared macadd As String = ""
End Class

Public Class WiFi
    Public Shared client As New WlanClient()
End Class