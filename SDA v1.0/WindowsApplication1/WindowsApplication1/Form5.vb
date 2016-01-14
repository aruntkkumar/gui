Imports System
Imports System.Threading
Imports System.IO.Ports
Imports System.ComponentModel

Public Class Form5

    Dim Line As String
    Dim myPort As Array
    Dim vio As Integer
    Dim byte1 As Integer
    Dim bandsel As Integer
    Dim cabandsel As Integer
    Delegate Sub SetTextCallBack(ByVal [text] As String)

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Date.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox1.Items.AddRange(myPort)
        Button2.Enabled = False
        Button4.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If ComboBox1.Text = "" Then
                MsgBox("Please select a Comm Port", MsgBoxStyle.Information, "Error")
            Else
                If ComboBox2.Text = "" Then
                    MsgBox("Please select a Baud Rate", MsgBoxStyle.Information, "Error")
                Else
                    SerialPort1.PortName = ComboBox1.Text
                    SerialPort1.BaudRate = ComboBox2.Text
                    SerialPort1.Parity = Parity.None
                    SerialPort1.DataBits = 8
                    SerialPort1.StopBits = StopBits.One
                    SerialPort1.Open()
                    'Button2.Enabled = True
                    Button3.Enabled = False
                    Button4.Enabled = True
                    Button5.Enabled = True
                    ComboBox1.Enabled = False
                    ComboBox2.Enabled = False
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ComboBox1.Items.Clear()
            myPort = IO.Ports.SerialPort.GetPortNames()
            ComboBox1.Items.AddRange(myPort)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If ComboBox5.Text = "" Then
                MsgBox("Please select the Antenna", MsgBoxStyle.Information, "Error")
            Else
                If ComboBox6.Text = "" Then
                    MsgBox("Please select the TRX", MsgBoxStyle.Information, "Error")
                Else
                    If ComboBox7.Text = "" Then
                        MsgBox("Please select a supported E-UTRA band", MsgBoxStyle.Information, "Error")
                    Else
                        If ComboBox8.Text = "" Then
                            MsgBox("Please select a supported Carrier Aggregation band", MsgBoxStyle.Information, "Error")
                        Else
                            If ComboBox9.Text = "" Then
                                MsgBox("Please select the TX power level", MsgBoxStyle.Information, "Error")
                            Else
                                If ComboBox7.SelectedIndex = 0 Then
                                    bandsel = &H41
                                Else
                                    If ComboBox7.SelectedIndex = 1 Then
                                        bandsel = &H42
                                    Else
                                        If ComboBox7.SelectedIndex = 2 Then
                                            bandsel = &H43
                                        Else
                                            If ComboBox7.SelectedIndex = 3 Then
                                                bandsel = &H44
                                            Else
                                                If ComboBox7.SelectedIndex = 4 Then
                                                    bandsel = &H45
                                                Else
                                                    If ComboBox7.SelectedIndex = 5 Then
                                                        bandsel = &H47
                                                    Else
                                                        If ComboBox7.SelectedIndex = 6 Then
                                                            bandsel = &H48
                                                        Else
                                                            If ComboBox7.SelectedIndex = 7 Then
                                                                bandsel = &H4C
                                                            Else
                                                                If ComboBox7.SelectedIndex = 8 Then
                                                                    bandsel = &H4D
                                                                Else
                                                                    If ComboBox7.SelectedIndex = 9 Then
                                                                        bandsel = &H54
                                                                    Else
                                                                        If ComboBox7.SelectedIndex = 10 Then
                                                                            bandsel = &H59
                                                                        Else
                                                                            If ComboBox7.SelectedIndex = 11 Then
                                                                                bandsel = &H5A
                                                                            Else
                                                                                If ComboBox7.SelectedIndex = 12 Then
                                                                                    bandsel = &H5D
                                                                                Else
                                                                                    If ComboBox7.SelectedIndex = 13 Then
                                                                                        bandsel = &H5E
                                                                                    Else
                                                                                        bandsel = &H69
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
                                If ComboBox8.SelectedIndex = 0 Then
                                    cabandsel = &H80
                                Else
                                    If ComboBox8.SelectedIndex = 1 Then
                                        cabandsel = &H81
                                    Else
                                        If ComboBox8.SelectedIndex = 2 Then
                                            cabandsel = &H82
                                        Else
                                            If ComboBox8.SelectedIndex = 3 Then
                                                cabandsel = &H83
                                            Else
                                                If ComboBox8.SelectedIndex = 4 Then
                                                    cabandsel = &H84
                                                Else
                                                    If ComboBox8.SelectedIndex = 5 Then
                                                        cabandsel = &H85
                                                    Else
                                                        If ComboBox8.SelectedIndex = 6 Then
                                                            cabandsel = &H87
                                                        Else
                                                            If ComboBox8.SelectedIndex = 7 Then
                                                                cabandsel = &H88
                                                            Else
                                                                If ComboBox8.SelectedIndex = 8 Then
                                                                    cabandsel = &H8C
                                                                Else
                                                                    If ComboBox8.SelectedIndex = 9 Then
                                                                        cabandsel = &H8D
                                                                    Else
                                                                        If ComboBox8.SelectedIndex = 10 Then
                                                                            cabandsel = &H94
                                                                        Else
                                                                            If ComboBox8.SelectedIndex = 11 Then
                                                                                cabandsel = &H99
                                                                            Else
                                                                                If ComboBox8.SelectedIndex = 12 Then
                                                                                    cabandsel = &H9A
                                                                                Else
                                                                                    If ComboBox8.SelectedIndex = 13 Then
                                                                                        cabandsel = &H9D
                                                                                    Else
                                                                                        If ComboBox8.SelectedIndex = 14 Then
                                                                                            cabandsel = &H9E
                                                                                        Else
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
                                'SerialPort1.WriteLine("rw 1 0x05 0xD7" & vbCrLf & "rw 1 0x05 0x58" & vbCrLf & "rw 1 0x05 0x97" & vbCrLf)
                                SerialPort1.WriteLine("rw 1 0x05 0x" & byte1.ToString("X") & vbCrLf & "rw 1 0x05 0x" & bandsel.ToString("X") & vbCrLf & "rw 1 0x05 0x" & cabandsel.ToString("X") & vbCrLf)
                                RichTextBox1.Text &= SerialPort1.ReadExisting()
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ComboBox1.Items.Clear()
            myPort = IO.Ports.SerialPort.GetPortNames()
            ComboBox1.Items.AddRange(myPort)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SerialPort1.Close()
        Button2.Enabled = False
        Button3.Enabled = True
        Button4.Enabled = False
        Button5.Enabled = True
        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
        ComboBox1.Items.Clear()
        myPort = IO.Ports.SerialPort.GetPortNames()
        ComboBox1.Items.AddRange(myPort)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button5.Enabled = True
        Button2.Enabled = False
        ComboBox5.SelectedIndex = -1
        ComboBox6.SelectedIndex = -1
        ComboBox7.SelectedIndex = -1
        ComboBox8.SelectedIndex = -1
        ComboBox9.SelectedIndex = -1
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        ReceivedText(SerialPort1.ReadExisting())
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
        Try
            If Button3.Enabled = True Then
                MsgBox("Please open a Comm Port", MsgBoxStyle.Information, "Error")
            Else
                Button5.Enabled = False
                Button2.Enabled = True
                SerialPort1.WriteLine("vio " & vio & vbCrLf & "clk 0" & vbCrLf)
                RichTextBox1.Text &= SerialPort1.ReadExisting()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ComboBox1.Items.Clear()
            myPort = IO.Ports.SerialPort.GetPortNames()
            ComboBox1.Items.AddRange(myPort)
        End Try
    End Sub

End Class