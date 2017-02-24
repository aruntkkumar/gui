Imports NativeWifi
Imports System
Imports System.Collections.ObjectModel
Imports System.Net.NetworkInformation
Imports System.IO
Imports System.Threading
Imports System.Text

Public Class Form3

    Dim fullstring As String
    Dim timestamp As String
    Dim frequency As Double
    Dim channelnumber As Integer
    Dim datarate As Integer
    Dim count As Integer
    Dim quality() As Integer
    Dim avgquality As Double
    Dim bandwidth As Integer
    Dim foundit As Integer

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler System.Windows.Forms.Application.Idle, AddressOf Application_Idle
        Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Start" Then
            foundit = 0
            Try
                For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                    If (wlanIface.InterfaceState.ToString() = "Disconnecting" Or wlanIface.InterfaceState.ToString() = "Disconnected") Then
                        MetroFramework.MetroMessageBox.Show(Me, "Connection lost. Please establish a connection and try again.", "Connectivity Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
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
                                foundit = 1
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
                                If DataGridView2.RowCount > 0 Then
                                    DataGridView2.Rows.Clear()
                                End If
                                bandwidth = 0
                                bandwidth = InputBox("Please enter the channel bandwidth in MHz.", "Bandwidth Information", 20)
                                If bandwidth = 0 Then
                                    MetroFramework.MetroMessageBox.Show(Me, "No relevant data provided. Kindly try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                                DataGridView2.Rows.Add(GlobalVariables.ssidname, tMac, network.dot11BssPhyType, frequency, wlanIface.Channel, bandwidth, datarate, network.dot11BssType)
                                Exit For
                            End If
                        End If
                    Next
                Next
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
            End Try
            If foundit = 0 Then
                Exit Sub
            End If
            count = 0
            DataGridView1.Rows.Clear()
            quality = New Integer(count) {}
            fullstring = ""
            timestamp = DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")    'Hebrew colon (׃) is from right to left
            Button1.Text = "Stop"
        Else
            timestamp = timestamp & " to " & DateTime.Now.ToString("dd.MM.yyyy_ss׃mm׃HH")
            If (Not System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")) Then
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\")
            End If
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\SAT WiFi Data Logger\" & timestamp & ".txt", fullstring)
            Button1.Text = "Start"
        End If
    End Sub

    Private Sub Application_Idle(ByVal sender As Object, ByVal e As EventArgs)
        If Button1.Text = "Stop" Then
            Try
                For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
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
                                count += 1
                                quality(count - 1) = network.linkQuality
                                avgquality = 0.0
                                For Each n In quality
                                    avgquality += n
                                Next
                                avgquality /= count
                                avgquality = Math.Round(avgquality, 1)
                                fullstring += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff") & " " & network.rssi & " " & network.linkQuality & " " & avgquality & vbNewLine
                                DataGridView1.Rows.Add(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff"), network.rssi, network.linkQuality, avgquality)
                                DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.RowCount - 1
                                System.Array.Resize(Of Integer)(quality, count + 1)
                            End If
                        End If
                    Next

                Next
            Catch ex As Exception
                MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End If
        'For i As Integer = 0 To 10
        'Application.DoEvents()
        Thread.Sleep(100)
        'Next
    End Sub
End Class

Public Class Wireless
    Public Shared main As Wlan()
End Class