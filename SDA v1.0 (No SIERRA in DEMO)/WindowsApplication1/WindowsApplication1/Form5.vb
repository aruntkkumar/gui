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
    Dim test As Integer
    Dim myserialPort1 As New ExSerialPort
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
        Button6.Enabled = False
        RichTextBox1.ReadOnly = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If ComboBox1.Text = "" Then
                MsgBox("Please select a Comm Port", MsgBoxStyle.Information, "Error")
            Else
                If ComboBox2.Text = "" Then
                    MsgBox("Please select a Baud Rate", MsgBoxStyle.Information, "Error")
                Else
                    'Dim x As New ComPortFinder
                    'Dim List As List(Of String)
                    'List = x.ComPortNames("413C", "81B6", "03")
                    'For Each item As String In List
                    '    If (item = ComboBox1.Text) Then
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
                    '    End If
                    'Next
                    'If Not SerialPort1.IsOpen Then
                    '    MsgBox("The Selected Port is not valid for System Mode", MsgBoxStyle.Information, "Error")
                    'End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Error")
            SerialReset()
            'ComboBox1.Items.Clear()
            'myPort = IO.Ports.SerialPort.GetPortNames()
            'ComboBox1.Items.AddRange(myPort)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox5.Text = "" Then
            MsgBox("Please select the Antenna", MsgBoxStyle.Information, "Error")
        Else
            If ComboBox6.Text = "" Then
                MsgBox("Please select the TRX", MsgBoxStyle.Information, "Error")
            Else
                If (ComboBox7.Text = "" Or ComboBox8.Text = "" Or (ComboBox7.SelectedIndex = 0 And ComboBox8.SelectedIndex = 0)) Then
                    MsgBox("Please select a supported E-UTRA band or Carrier Aggregation band", MsgBoxStyle.Information, "Error")
                Else
                    If ComboBox9.Text = "" Then
                        MsgBox("Please select the TX power level", MsgBoxStyle.Information, "Error")
                    Else
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
                            myserialPort1.WriteLine("AT!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & byte1.ToString("X") & ";!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & bandsel.ToString("X") & ";!RFMIPI=" & TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text & "," & "0x" & cabandsel.ToString("X") & vbCrLf)
                            RichTextBox1.Text &= myserialPort1.ReadLine()
                            RichTextBox1.Text &= myserialPort1.ReadLine()
                            'RichTextBox1.Text &= myserialPort1.ReadExisting()
                            'RichTextBox1.Text &= myserialPort1.ReadExisting()
                            'RichTextBox1.Text &= myserialPort1.ReadExisting()
                        Catch ex As Exception
                            MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
                            SerialReset()
                            Exit Sub
                        End Try
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
            MsgBox("Please open a Comm Port", MsgBoxStyle.Information, "Error")
            Exit Sub
        End If
        Try
            test = CInt(TextBox1.Text)
        Catch ex As Exception
            MsgBox("Please enter an integer between 0-4 as RFFE Bus value. (Use 3 for external tuner)", MsgBoxStyle.Information, "Error")
            Exit Sub
        End Try
        Try
            test = CInt(TextBox2.Text)
        Catch ex As Exception
            MsgBox("Please enter an integer between 0-15 as Slave ID value. (LM8335 slave ID is 1)", MsgBoxStyle.Information, "Error")
            Exit Sub
        End Try
        Try
            test = CInt(TextBox3.Text)
        Catch ex As Exception
            MsgBox("Please enter an integer between 0-31 as MIPI Register value. (Default value is 5)", MsgBoxStyle.Information, "Error")
            Exit Sub
        End Try
        test = CInt(TextBox1.Text)
        If test > 4 Then
            MsgBox("RFFE Bus value above 4. Please enter a value between 0-4 (Use 3 for external tuner)", MsgBoxStyle.Information, "Error")
        Else
            test = CInt(TextBox2.Text)
            If test > 15 Then
                MsgBox("Slave ID value above 15. Please enter a value between 0-15 (LM8335 slave ID is 1)", MsgBoxStyle.Information, "Error")
            Else
                test = CInt(TextBox3.Text)
                If test > 31 Then
                    MsgBox("MIPI Register value above 31. Please enter a value between 0-31 (Default value is 5)", MsgBoxStyle.Information, "Error")
                Else
                    Try
                        'SerialPort1.WriteLine("AT+CFUN=" & TextBox3.Text & vbCrLf)
                        'SerialPort1.WriteLine("AT+CFUN=5" & vbCrLf)
                        'RichTextBox1.Text &= SerialPort1.ReadExisting()
                        myserialPort1.WriteLine("AT+CFUN=5" & vbCrLf)
                        RichTextBox1.Text &= myserialPort1.ReadLine()
                        RichTextBox1.Text &= myserialPort1.ReadLine()
                        'RichTextBox1.Text &= myserialPort1.ReadExisting()
                        'RichTextBox1.Text &= myserialPort1.ReadExisting()
                        'RichTextBox1.Text &= myserialPort1.ReadExisting()
                    Catch ex As Exception
                        MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
                        SerialReset()
                        Exit Sub
                    End Try
                    Button5.Enabled = False
                    Button2.Enabled = True
                    TextBox1.Enabled = False
                    TextBox2.Enabled = False
                    TextBox3.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            'SerialPort1.Write(RichTextBox2.Text & vbCr)
            'RichTextBox1.Text &= SerialPort1.ReadExisting()
            myserialPort1.Write(RichTextBox2.Text & vbCr)
            RichTextBox1.Text &= myserialPort1.ReadLine()
            RichTextBox1.Text &= myserialPort1.ReadLine()
            'RichTextBox1.Text &= myserialPort1.ReadExisting()
            'RichTextBox1.Text &= myserialPort1.ReadExisting()
            'RichTextBox1.Text &= myserialPort1.ReadExisting()
        Catch ex As Exception
            MsgBox(ComboBox1.Text & " does not exist. Please open a valid COM port", MsgBoxStyle.Information, "Error")
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

    Private Sub ComboBox7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox7.SelectedIndexChanged
        If ComboBox7.SelectedIndex = -1 Then
            Exit Sub
        End If
        If ComboBox7.SelectedIndex = 0 Then
            Exit Sub
        Else
            ComboBox8.SelectedIndex = 0
        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        If ComboBox8.SelectedIndex = -1 Then
            Exit Sub
        End If
        If ComboBox8.SelectedIndex = 0 Then
            Exit Sub
        Else
            ComboBox7.SelectedIndex = 0
        End If
    End Sub
End Class