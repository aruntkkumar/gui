Imports System
Imports System.Threading
Imports System.IO.Ports
Imports System.ComponentModel

Public Class Form5

    Dim Line As String
    Dim myPort As Array
    Dim testPort As Array
    Dim vio As Integer
    Dim byte1 As Integer
    Dim byte4 As Integer
    Dim byte5 As Integer
    Dim bandsel As Integer
    Dim cabandsel As Integer
    Dim test As Integer
    Dim uplink As Double
    Dim fullscreen As Boolean = False
    Dim myserialPort1 As New ExSerialPort
    Delegate Sub SetTextCallBack(ByVal [text] As String)

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Date.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(976, 727)
        AddHandler System.Windows.Forms.Application.Idle, AddressOf Application_Idle
        Timer1.Enabled = True
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox1.Items.AddRange(myPort)
        If ComboBox1.Items.Count = 0 Then
            ComboBox1.Items.Add("                   No ComPorts detected")
            ComboBox1.SelectedIndex = 0
        Else
            ComboBox1.SelectedIndex = -1
        End If
        Button2.Enabled = False
        Button4.Enabled = False
        Button6.Enabled = False
        RichTextBox1.ReadOnly = True
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
                        Dim x As New ComPortFinder
                        Dim List As List(Of String)
                        List = x.ComPortNames("413C", "81B6", "03")
                        For Each item As String In List
                            If (item = ComboBox1.Text) Then
                                'SerialPort1.PortName = ComboBox1.Text
                                'SerialPort1.BaudRate = ComboBox2.Text
                                'SerialPort1.Parity = Parity.None
                                'SerialPort1.DataBits = 8
                                'SerialPort1.StopBits = StopBits.One
                                'SerialPort1.Open()
                                myserialPort1.PortName = ComboBox1.Text
                                myserialPort1.BaudRate = ComboBox2.Text
                                myserialPort1.Parity = Parity.None
                                myserialPort1.DataBits = 8
                                myserialPort1.StopBits = StopBits.One
                                myserialPort1.Open()
                                'Button2.Enabled = True
                                Button3.Enabled = False
                                Button4.Enabled = True
                                Button5.Enabled = True
                                Button6.Enabled = True
                                ComboBox1.Enabled = False
                                ComboBox2.Enabled = False
                                RichTextBox1.Text &= "Port Name: " & ComboBox1.Text & "; Baud Rate: " & ComboBox2.Text & vbCrLf & vbCrLf
                            End If
                        Next
                        If Button3.Enabled = True Then
                        MetroFramework.MetroMessageBox.Show(Me, "The selected ComPort is not suitable for System Mode", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
                End If
            End If
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'MsgBox(ex.Message, MsgBoxStyle.Information, "Error")
            SerialReset()
            'ComboBox1.Items.Clear()
            'myPort = IO.Ports.SerialPort.GetPortNames()
            'ComboBox1.Items.AddRange(myPort)
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
                    If ComboBox9.Text = "" Then
                        MetroFramework.MetroMessageBox.Show(Me, "Please select the TX power level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        If (TextBox4.Enabled = True And TextBox4.Text = "") Then
                            MetroFramework.MetroMessageBox.Show(Me, "Please provide the uplink channel number for the selected band coverage", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            byte4 = &H40
                            byte5 = &H80
                            Try
                                uplink = CDbl(TextBox4.Text)
                            Catch ex As Exception
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number for the selected band coverage.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End Try
                            'If uplink < 18000 Or uplink > 27759 Then
                            '    MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number for the selected band coverage.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            '    Exit Sub
                            'End If
                            If (TextBox4.WaterMark = "18000 to 18599" And (uplink < 18000 Or uplink > 18599)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 18000 and 18599", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "18600 to 19199" And (uplink < 18600 Or uplink > 19199)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 18600 and 19199", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "19200 to 19949" And (uplink < 19200 Or uplink > 19949)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 19200 and 19949", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "19950 to 20399" And (uplink < 19950 Or uplink > 20399)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 19950 and 20399", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "20400 to 20649" And (uplink < 20400 Or uplink > 20649)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 20400 and 20649", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "20750 to 21449" And (uplink < 20750 Or uplink > 21449)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 20750 and 21449", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "21450 to 21799" And (uplink < 21450 Or uplink > 21799)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 21450 and 21799", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "23010 to 23179" And (uplink < 23010 Or uplink > 23179)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 23010 and 23179", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "23180 to 23279" And (uplink < 23180 Or uplink > 23279)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 23180 and 23279", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "24150 to 24449" And (uplink < 24150 Or uplink > 24449)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 24150 and 24449", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "26040 to 26689" And (uplink < 26040 Or uplink > 26689)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 26040 and 26689", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "26690 to 27039" And (uplink < 26690 Or uplink > 27039)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 26690 and 27039", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "27660 to 27759" And (uplink < 27660 Or uplink > 27759)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 27660 and 27759", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf (TextBox4.WaterMark = "39650 to 41589" And (uplink < 39650 Or uplink > 41589)) Then
                                MetroFramework.MetroMessageBox.Show(Me, "Please enter a valid uplink channel number between 39650 and 41589", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            End If
                            test = 0
                            If ComboBox7.SelectedIndex = 1 Or ComboBox8.SelectedIndex = 1 Then
                                test = Math.Truncate((uplink - 18000) / 10)
                            ElseIf ComboBox7.SelectedIndex = 2 Or ComboBox8.SelectedIndex = 2 Or ComboBox8.SelectedIndex = 3 Or ComboBox8.SelectedIndex = 4 Or ComboBox8.SelectedIndex = 5 Then
                                test = Math.Truncate((uplink - 18600) / 10)
                            ElseIf ComboBox7.SelectedIndex = 3 Or ComboBox8.SelectedIndex = 6 Or ComboBox8.SelectedIndex = 7 Then
                                test = Math.Truncate((uplink - 19200) / 10)
                            ElseIf ComboBox7.SelectedIndex = 4 Or ComboBox8.SelectedIndex = 8 Or ComboBox8.SelectedIndex = 9 Or ComboBox8.SelectedIndex = 10 Or ComboBox8.SelectedIndex = 11 Then
                                test = Math.Truncate((uplink - 19950) / 10)
                            ElseIf ComboBox7.SelectedIndex = 5 Or ComboBox8.SelectedIndex = 12 Then
                                test = Math.Truncate((uplink - 20400) / 10)
                            ElseIf ComboBox7.SelectedIndex = 6 Or ComboBox8.SelectedIndex = 13 Then
                                test = Math.Truncate((uplink - 20750) / 10)
                            ElseIf ComboBox7.SelectedIndex = 7 Then
                                test = Math.Truncate((uplink - 21450) / 10)
                            ElseIf ComboBox7.SelectedIndex = 8 Or ComboBox8.SelectedIndex = 14 Then
                                test = Math.Truncate((uplink - 23010) / 10)
                            ElseIf ComboBox7.SelectedIndex = 9 Then
                                test = Math.Truncate((uplink - 23180) / 10)
                            ElseIf ComboBox7.SelectedIndex = 10 Then
                                test = Math.Truncate((uplink - 24150) / 10)
                            ElseIf ComboBox7.SelectedIndex = 11 Then
                                test = Math.Truncate((uplink - 26040) / 10)
                            ElseIf ComboBox7.SelectedIndex = 12 Then
                                test = Math.Truncate((uplink - 26690) / 10)
                            ElseIf ComboBox7.SelectedIndex = 14 Then
                                test = Math.Truncate((uplink - 27660) / 10)
                            ElseIf ComboBox7.SelectedIndex = 15 Or ComboBox8.SelectedIndex = 15 Then
                                test = Math.Truncate((uplink - 39650) / 10)
                            End If
                            byte4 = byte4 Or (test >> 6)
                            byte5 = byte5 Or (test And &H3F)
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
                            'SerialPort1.WriteLine("rw 1 0x05 0x" & byte1.ToString("X") & vbCrLf & "rw 1 0x05 0x" & bandsel.ToString("X") & vbCrLf & "rw 1 0x05 0x" & cabandsel.ToString("X") & vbCrLf)
                            Try
                                'SerialPort1.WriteLine("AT!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & byte1.ToString("X") & ";!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & bandsel.ToString("X") & ";!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & cabandsel.ToString("X") & vbCrLf)
                                'RichTextBox1.Text &= SerialPort1.ReadExisting()
                                myserialPort1.WriteLine("AT!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & byte1.ToString("X") & ";!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & bandsel.ToString("X") & ";!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & cabandsel.ToString("X") & ";!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & byte4.ToString("X") & ";!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & byte5.ToString("X") & vbCrLf)
                                RichTextBox1.Text &= myserialPort1.ReadLine()
                                RichTextBox1.Text &= myserialPort1.ReadExisting()
                            Catch ex As Exception
                                MetroFramework.MetroMessageBox.Show(Me, myserialPort1.PortName & " does not exist. Please open a valid COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                'MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
                                SerialReset()
                                Exit Sub
                            End Try
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SerialReset()
        RichTextBox1.Text &= "The active device has been disconnected" & vbCrLf & vbCrLf
    End Sub

    Function SerialReset()
        Try
            'SerialPort1.Close()
            myserialPort1.Close()
        Catch ex As Exception
        End Try
        Application.DoEvents()  ' Give port time to close down
        Thread.Sleep(200)
        Button2.Enabled = False
        Button3.Enabled = True
        Button4.Enabled = False
        Button5.Enabled = True
        Button6.Enabled = False
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
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
        Return 0
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button5.Enabled = True
        Button2.Enabled = False
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox4.Enabled = True
        TextBox4.WaterMark = "Select an appropriate band coverage"
        ComboBox5.SelectedIndex = -1
        ComboBox6.SelectedIndex = -1
        ComboBox7.SelectedIndex = -1
        ComboBox8.SelectedIndex = -1
        ComboBox9.SelectedIndex = -1
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        'ReceivedText(SerialPort1.ReadExisting())
        ReceivedText(myserialPort1.ReadExisting())
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
            Exit Sub
        End If
        Try
            test = CInt(TextBox1.Text)
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-4 as RFFE Bus value. (Use 3 for external tuner)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End Try
        Try
            test = CInt(TextBox2.Text)
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-15 as Slave ID value. (LM8335 slave ID is 1)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End Try
        Try
            test = CInt(TextBox3.Text)
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-31 as MIPI Register value. (Default value is 5)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End Try
        test = CInt(TextBox1.Text)
        If (test >= 0) Then
            If test > 4 Then
                MetroFramework.MetroMessageBox.Show(Me, "RFFE Bus value above 4. Please enter a value between 0-4 (Use 3 for external tuner)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                test = CInt(TextBox2.Text)
                If (test >= 0) Then
                    If test > 15 Then
                        MetroFramework.MetroMessageBox.Show(Me, "Slave ID value above 15. Please enter a value between 0-15 (LM8335 slave ID is 1)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        test = CInt(TextBox3.Text)
                        If (test >= 0) Then
                            If test > 31 Then
                                MetroFramework.MetroMessageBox.Show(Me, "MIPI Register value above 31. Please enter a value between 0-31 (Default value is 5)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                Try
                                    'SerialPort1.WriteLine("AT+CFUN=" & TextBox3.Text & vbCrLf)
                                    'SerialPort1.WriteLine("AT+CFUN=5" & vbCrLf)
                                    'RichTextBox1.Text &= SerialPort1.ReadExisting()
                                    myserialPort1.WriteLine("AT+CFUN=5" & vbCrLf)
                                    RichTextBox1.Text &= myserialPort1.ReadLine()
                                    RichTextBox1.Text &= myserialPort1.ReadExisting()
                                Catch ex As Exception
                                    MetroFramework.MetroMessageBox.Show(Me, myserialPort1.PortName & " does not exist. Please open a valid COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    'MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
                                    SerialReset()
                                    Exit Sub
                                End Try
                                Button5.Enabled = False
                                Button2.Enabled = True
                                TextBox1.Enabled = False
                                TextBox2.Enabled = False
                                TextBox3.Enabled = False
                            End If
                        Else
                            MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-31 as MIPI Register value. (Default value is 5)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Else
                    MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-15 as Slave ID value. (LM8335 slave ID is 1)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
                Else
            MetroFramework.MetroMessageBox.Show(Me, "Please enter an integer between 0-4 as RFFE Bus value. (Use 3 for external tuner)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            'SerialPort1.Write(RichTextBox2.Text & vbCr)
            'RichTextBox1.Text &= SerialPort1.ReadExisting()
            myserialPort1.Write(RichTextBox2.Text & vbCr)
            RichTextBox1.Text &= myserialPort1.ReadLine()
            RichTextBox1.Text &= myserialPort1.ReadExisting()
        Catch ex As Exception
            MetroFramework.MetroMessageBox.Show(Me, myserialPort1.PortName & " does not exist. Please open a valid COM port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
            SerialReset()
            Exit Sub
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        RichTextBox1.Clear()
    End Sub

    Private Sub Form5_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'If SerialPort1.IsOpen() Then
        If myserialPort1.IsOpen() Then
            Try
                'SerialPort1.Close()
                myserialPort1.Close()
            Catch ex As Exception
            End Try
            Application.DoEvents()  ' Give port time to close down
            Thread.Sleep(200)
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Process.Start("http://www.smartantennatech.com/")
    End Sub

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        If ComboBox7.SelectedIndex = -1 Then
            Exit Sub
        End If
        If ComboBox7.SelectedIndex = 0 Then
            Carrier_Aggregation()
            Exit Sub
        Else
            ComboBox8.SelectedIndex = 0
            Single_Band()
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        If ComboBox8.SelectedIndex = -1 Then
            Exit Sub
        End If
        If ComboBox8.SelectedIndex = 0 Then
            Single_Band()
            Exit Sub
        Else
            ComboBox7.SelectedIndex = 0
            Carrier_Aggregation()
        End If
    End Sub

    Private Sub RichTextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox2.KeyDown
        If e.Modifiers = Keys.Control AndAlso e.KeyCode = Keys.V Then
            Using box As New RichTextBox
                box.SelectAll()
                box.SelectedRtf = Clipboard.GetText(TextDataFormat.Rtf)
                box.SelectAll()
                box.SelectionFont = New Font("Consolas", 8, FontStyle.Regular)
                box.SelectionBackColor = Color.Black
                box.SelectionColor = Color.White
                RichTextBox2.SelectedRtf = box.SelectedRtf
            End Using

            e.Handled = True
        End If
    End Sub

    Private Sub Form5_SizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.SizeChanged
        If Me.WindowState = FormWindowState.Normal Then
            'FullScreenToolStripMenuItem.Checked = False
            fullscreen = False
        Else
            'FullScreenToolStripMenuItem.Checked = True
            fullscreen = True
        End If
        SizeAdjust()
    End Sub

    Function SizeAdjust()
        Dim screen As Screen = Screen.FromControl(Me)
        Dim intX As Integer = screen.WorkingArea.Width
        Dim intY As Integer = screen.WorkingArea.Height
        If Me.WindowState = FormWindowState.Maximized Then
            Label15.Location = New Point(x:=(intX / 8), y:=(intY * 1.5 / 13))
            Label4.Location = New Point(x:=(intX / 8), y:=(intY * 2.3636 / 13))
            Label11.Location = New Point(x:=(intX / 8), y:=(intY * 3.227 / 13))
            Label10.Location = New Point(x:=(intX / 8), y:=(intY * 4.0909 / 13))
            Label9.Location = New Point(x:=(intX / 8), y:=(intY * 4.9545 / 13))
            Label16.Location = New Point(x:=(intX / 8), y:=(intY * 5.8181 / 13))
            Label5.Location = New Point(x:=(intX / 8), y:=(intY * 6.6818 / 13))
            Label6.Location = New Point(x:=(intX / 8), y:=(intY * 7.5454 / 13))
            Label7.Location = New Point(x:=(intX / 8), y:=(intY * 8.409 / 13))
            Label8.Location = New Point(x:=(intX / 8), y:=(intY * 9.2727 / 13))
            Label13.Location = New Point(x:=(intX / 8), y:=(intY * 10.1363 / 13))
            Button1.Location = New Point(x:=(intX / 8), y:=(intY * 11 / 13))
            Button2.Location = New Point(x:=(intX / 3.5), y:=(intY * 11 / 13))
            ComboBox1.Location = New Point(x:=(intX / 3.5), y:=(intY * 1.5 / 13))
            ComboBox2.Location = New Point(x:=(intX / 3.5), y:=(intY * 2.3636 / 13))
            TextBox1.Location = New Point(x:=((intX / 3.5)), y:=(intY * 3.227 / 13))
            TextBox2.Location = New Point(x:=(intX / 3.5), y:=(intY * 4.0909 / 13))
            TextBox3.Location = New Point(x:=(intX / 3.5), y:=(intY * 4.9545 / 13))
            Button5.Location = New Point(x:=((intX / 3.5) + 146), y:=((intY * 4.0909 / 13) - 7))
            ComboBox5.Location = New Point(x:=(intX / 3.5), y:=(intY * 5.8181 / 13))
            ComboBox6.Location = New Point(x:=(intX / 3.5), y:=(intY * 6.6818 / 13))
            ComboBox7.Location = New Point(x:=(intX / 3.5), y:=(intY * 7.5454 / 13))
            ComboBox8.Location = New Point(x:=(intX / 3.5), y:=(intY * 8.409 / 13))
            ComboBox9.Location = New Point(x:=(intX / 3.5), y:=(intY * 9.2727 / 13))
            TextBox4.Location = New Point(x:=(intX / 3.5), y:=(intY * 10.1363 / 13))
            Button3.Location = New Point(x:=(intX / 1.9), y:=(intY * 1.5 / 13))
            Button4.Location = New Point(x:=(intX / 1.29), y:=(intY * 1.5 / 13))
            Label12.Location = New Point(x:=(intX / 1.9), y:=(intY * 2.3636 / 13))
            RichTextBox2.Location = New Point(x:=(intX / 1.9), y:=((intY * 2.3636 / 13) + 31))
            RichTextBox2.Size = New Size(width:=((intX / 1.29) - (intX / 1.9) + 119), height:=((intY * 3.227 / 13) - (intY * 2.3636 / 13) + 16 - 31))
            Button6.Location = New Point(x:=(intX / 1.9), y:=((intY * 4.0909 / 13) - 7))
            Label14.Location = New Point(x:=(intX / 1.9), y:=((intY * 4.9545 / 13) - 7))
            RichTextBox1.Location = New Point(x:=(intX / 1.9), y:=((intY * 4.9545 / 13) + 31 - 7))
            RichTextBox1.Size = New Size(width:=((intX / 1.29) - (intX / 1.9) + 119), height:=((intY * 10.1363 / 13) - (intY * 4.9545 / 13) + 25 - (31 - 7)))
            Button7.Location = New Point(x:=(intX / 1.9), y:=(intY * 11 / 13))
        Else
            Me.Size = New Size(976, 727)
            Label15.Location = New Point(x:=89, y:=102)
            Label4.Location = New Point(x:=89, y:=150)
            Label11.Location = New Point(x:=89, y:=198)
            Label10.Location = New Point(x:=89, y:=246)
            Label9.Location = New Point(x:=89, y:=294)
            Label16.Location = New Point(x:=89, y:=342)
            Label5.Location = New Point(x:=89, y:=390)
            Label6.Location = New Point(x:=89, y:=438)
            Label7.Location = New Point(x:=89, y:=486)
            Label8.Location = New Point(x:=89, y:=534)
            Label13.Location = New Point(x:=89, y:=582)
            Button1.Location = New Point(x:=89, y:=632)
            Button2.Location = New Point(x:=273, y:=632)
            ComboBox1.Location = New Point(x:=273, y:=102)
            ComboBox2.Location = New Point(x:=273, y:=150)
            TextBox1.Location = New Point(x:=273, y:=198)
            TextBox2.Location = New Point(x:=273, y:=246)
            TextBox3.Location = New Point(x:=273, y:=294)
            Button5.Location = New Point(x:=419, y:=239)
            ComboBox5.Location = New Point(x:=273, y:=342)
            ComboBox6.Location = New Point(x:=273, y:=390)
            ComboBox7.Location = New Point(x:=273, y:=438)
            ComboBox8.Location = New Point(x:=273, y:=486)
            ComboBox9.Location = New Point(x:=273, y:=534)
            TextBox4.Location = New Point(x:=273, y:=582)
            Button3.Location = New Point(x:=591, y:=102)
            Button4.Location = New Point(x:=745, y:=102)
            Label12.Location = New Point(x:=591, y:=150)
            RichTextBox2.Location = New Point(x:=591, y:=181)
            RichTextBox2.Size = New Size(width:=273, height:=33)
            Button6.Location = New Point(x:=591, y:=239)
            Label14.Location = New Point(x:=591, y:=287)
            RichTextBox1.Location = New Point(x:=591, y:=318)
            RichTextBox1.Size = New Size(width:=273, height:=289)
            Button7.Location = New Point(x:=591, y:=632)
        End If
        Return 0
    End Function

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

    Function Carrier_Aggregation()
        If ((ComboBox7.SelectedIndex = 0 And ComboBox8.SelectedIndex = 0) Or ComboBox7.SelectedIndex = -1 Or ComboBox8.SelectedIndex = -1) Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "Select an appropriate band coverage"
        ElseIf ComboBox8.SelectedIndex = 1 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "18000 to 18599"
        ElseIf ComboBox8.SelectedIndex = 2 Or ComboBox8.SelectedIndex = 3 Or ComboBox8.SelectedIndex = 4 Or ComboBox8.SelectedIndex = 5 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "18600 to 19199"
        ElseIf ComboBox8.SelectedIndex = 6 Or ComboBox8.SelectedIndex = 7 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "19200 to 19949"
        ElseIf ComboBox8.SelectedIndex = 8 Or ComboBox8.SelectedIndex = 9 Or ComboBox8.SelectedIndex = 10 Or ComboBox8.SelectedIndex = 11 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "19950 to 20399"
        ElseIf ComboBox8.SelectedIndex = 12 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "20400 to 20649"
        ElseIf ComboBox8.SelectedIndex = 13 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "20750 to 21449"
        ElseIf ComboBox8.SelectedIndex = 14 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "23010 to 23179"
        ElseIf ComboBox8.SelectedIndex = 15 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "39650 to 41589"
        End If
        TextBox4.Text = ""
        Return 0
    End Function

    Function Single_Band()
        If ((ComboBox7.SelectedIndex = 0 And ComboBox8.SelectedIndex = 0) Or ComboBox7.SelectedIndex = -1 Or ComboBox8.SelectedIndex = -1) Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "Select an appropriate band coverage"
        ElseIf ComboBox7.SelectedIndex = 1 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "18000 to 18599"
        ElseIf ComboBox7.SelectedIndex = 2 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "18600 to 19199"
        ElseIf ComboBox7.SelectedIndex = 3 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "19200 to 19949"
        ElseIf ComboBox7.SelectedIndex = 4 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "19950 to 20399"
        ElseIf ComboBox7.SelectedIndex = 5 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "20400 to 20649"
        ElseIf ComboBox7.SelectedIndex = 6 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "20750 to 21449"
        ElseIf ComboBox7.SelectedIndex = 7 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "21450 to 21799"
        ElseIf ComboBox7.SelectedIndex = 8 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "23010 to 23179"
        ElseIf ComboBox7.SelectedIndex = 9 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "23180 to 23279"
        ElseIf ComboBox7.SelectedIndex = 10 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "24150 to 24449"
        ElseIf ComboBox7.SelectedIndex = 11 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "26040 to 26689"
        ElseIf ComboBox7.SelectedIndex = 12 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "26690 to 27039"
        ElseIf ComboBox7.SelectedIndex = 13 Then
            TextBox4.Enabled = False
            TextBox4.WaterMark = "Channel number not available"
        ElseIf ComboBox7.SelectedIndex = 14 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "27660 to 27759"
        ElseIf ComboBox7.SelectedIndex = 15 Then
            TextBox4.Enabled = True
            TextBox4.WaterMark = "39650 to 41589"
        End If
        TextBox4.Text = ""
        Return 0
    End Function

End Class