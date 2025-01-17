﻿Imports NativeWifi
Imports System.Collections.ObjectModel
Imports System.Net.NetworkInformation
Imports System.IO
Imports System.Threading
Imports System.Text

Public Class Form2

    Dim profname As String
    Dim xml As String

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            TextBox1.PasswordChar = "*"
        Else
            TextBox1.PasswordChar = ""
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            'Dim client = New WlanClient()
            For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                ' Retrieves XML configurations of existing profiles.
                ' This can assist you in constructing your own XML configuration
                ' (that is, it will give you an example to follow).
                'For Each profileInfo As Wlan.WlanProfileInfo In wlanIface.GetProfiles()
                '    profname = profileInfo.profileName     ' this is typically the network's SSID
                '    xml = wlanIface.GetProfileXml(profileInfo.profileName)
                '    MsgBox(profname)
                '    MsgBox(xml)
                '    wlanIface.DeleteProfile(profname)
                '    MsgBox(profname & " is deleted.")
                'Next

                'Dim profileName As String = "SATWireless"           ' Connects to a known network with WEP security
                'Dim hexval As String = "534154576972656C657373"    '  this is also the SSID
                'Dim hexval As String = StringToHex(profileName)
                'Dim key As String = "Sm4RtAnt3nnA"
                Dim profileName As String = GlobalVariables.ssidname
                Dim hexval As String = StringToHex(GlobalVariables.ssidname)

                'For Each nic As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
                '    MsgBox(nic.Speed & """" & nic.GetPhysicalAddress().ToString() & """")
                'Next

                'Dim profileXml As String = String.Format("<?xml version=""1.0""?><WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1""><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><MSM><security><authEncryption><authentication>open</authentication><encryption>WEP</encryption><useOneX>false</useOneX></authEncryption><sharedKey><keyType>networkKey</keyType><protected>false</protected><keyMaterial>{2}</keyMaterial></sharedKey><keyIndex>0</keyIndex></security></MSM></WLANProfile>", profileName, hexval, key) 'GlobalVariables.ssidname, GlobalVariables.macadd, TextBox1.Text)
                Dim profileXml As String = String.Format("<?xml version=""1.0""?><WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1""><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><connectionMode>auto</connectionMode><MSM><security><authEncryption><authentication>WPA2PSK</authentication><encryption>AES</encryption><useOneX>false</useOneX></authEncryption><sharedKey><keyType>passPhrase</keyType><protected>false</protected><keyMaterial>{2}</keyMaterial></sharedKey></security></MSM></WLANProfile>", GlobalVariables.ssidname, hexval, TextBox1.Text)
                'If wlanIface.InterfaceState.ToString <> "Connected" Then
                wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, True)
                wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName)
                'MsgBox(wlanIface.InterfaceState.ToString())
                Thread.Sleep(1000) 'Total 10 seconds maximum, including the for loop
                For i As Integer = 1 To 9
                    If (wlanIface.InterfaceState.ToString() = "Authenticating" Or wlanIface.InterfaceState.ToString() = "Associating") Then
                        Thread.Sleep(1000)
                    ElseIf (wlanIface.InterfaceState.ToString() = "Disassociating" Or wlanIface.InterfaceState.ToString() = "Disconnected") Then
                        Exit For
                    ElseIf (wlanIface.InterfaceState.ToString() = "Connected") Then
                        'If GlobalVariables.debug = True Then
                        Form4.Show()
                        'ElseIf GlobalVariables.demo = True Then
                        '    Form6.Show()
                        'Else
                        '    Form3.Show()
                        'End If
                        Me.Close()
                        Exit Sub
                    End If
                Next
                wlanIface.DeleteProfile(profileName)
                'Dim proc As New Process
                'proc.StartInfo.CreateNoWindow = True
                'proc.StartInfo.FileName = "netsh"
                'proc.StartInfo.Arguments = "wlan disconnect interface=""" & wlanIface.InterfaceName & """"
                'proc.StartInfo.RedirectStandardOutput = True
                'proc.StartInfo.UseShellExecute = False
                'proc.Start()
                'proc.WaitForExit()

                'While (wlanIface.InterfaceState.ToString() = "Authenticating" Or wlanIface.InterfaceState.ToString() = "Associating")
                '    MsgBox(wlanIface.InterfaceState.ToString())
                'End While
                'If (wlanIface.InterfaceState.ToString() = "Disconnecting" Or wlanIface.InterfaceState.ToString() = "Disconnected") Then
                '    wlanIface.DeleteProfile(profileName)
                '    'Exit Sub
                'Else

                'End If
                'End If
                'MetroFramework.MetroMessageBox.Show(Me, "Attempting to connect...", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'While (wlanIface.InterfaceState.ToString() = "Authenticating" Or wlanIface.InterfaceState.ToString() = "Associating")

                'End While
                'Dispose(MetroFramework.MetroMessageBox.Show(Me, "Attempting to connect...", "Connection Status", MessageBoxButtons.OK, MessageBoxIcon.Information))
                'If (wlanIface.InterfaceState.ToString() = "Disconnecting" Or wlanIface.InterfaceState.ToString() = "Disconnected") Then
                '    MetroFramework.MetroMessageBox.Show(Me, "Unable to establish a connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Exit Sub
                'End If


                'Dim proc As New Process
                'proc.StartInfo.CreateNoWindow = True
                'proc.StartInfo.FileName = "netsh"
                'proc.StartInfo.Arguments = "wlan connect=" & profileName & " key=" & key
                'proc.StartInfo.RedirectStandardOutput = True
                'proc.StartInfo.UseShellExecute = False
                'proc.Start()
                'proc.WaitForExit()
            Next
            MetroFramework.MetroMessageBox.Show(Me, "Wrong Password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            If ex.Message.Contains("The network connection profile is corrupted") Then
                MetroFramework.MetroMessageBox.Show(Me, "Wrong Password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Show()
        Me.Close()
    End Sub

    Function StringToHex(ByVal text As String) As String
        Dim hex As String = ""
        For i As Integer = 0 To text.Length - 1
            hex &= Asc(text.Substring(i, 1)).ToString("x").ToUpper
        Next
        Return hex
    End Function

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                For Each wlanIface As WlanClient.WlanInterface In WiFi.client.Interfaces
                    Dim profileName As String = GlobalVariables.ssidname
                    Dim hexval As String = StringToHex(GlobalVariables.ssidname)

                    Dim profileXml As String = String.Format("<?xml version=""1.0""?><WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1""><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><connectionMode>auto</connectionMode><MSM><security><authEncryption><authentication>WPA2PSK</authentication><encryption>AES</encryption><useOneX>false</useOneX></authEncryption><sharedKey><keyType>passPhrase</keyType><protected>false</protected><keyMaterial>{2}</keyMaterial></sharedKey></security></MSM></WLANProfile>", GlobalVariables.ssidname, hexval, TextBox1.Text)
                    wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, True)
                    wlanIface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName)
                    'If GlobalVariables.debug = True Then
                    Form4.Show()
                    'ElseIf GlobalVariables.demo = True Then
                    '    Form6.Show()
                    'Else
                    '    Form3.Show()
                    'End If
                    Me.Close()
                    Exit Sub
                Next
            Catch ex As Exception
                If ex.Message.Contains("The network connection profile is corrupted") Then
                    MetroFramework.MetroMessageBox.Show(Me, "Wrong Password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Try
        End If
    End Sub
End Class