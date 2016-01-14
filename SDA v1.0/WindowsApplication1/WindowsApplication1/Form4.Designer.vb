<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form4
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form4))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox6 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBox7 = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ComboBox8 = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ComboBox9 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.ComboBox5 = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(842, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Build VER: 1.0"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(842, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Date:"
        '
        'Timer1
        '
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(872, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 2
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"256000", "128000", "115200", "57600", "38400", "28800", "19200", "14400", "9600", "4800", "2400", "1200", "600"})
        Me.ComboBox2.Location = New System.Drawing.Point(238, 104)
        Me.ComboBox2.MaxDropDownItems = 16
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(259, 21)
        Me.ComboBox2.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(91, 109)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Baud Rate:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(91, 339)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "TRX select:"
        '
        'ComboBox6
        '
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Items.AddRange(New Object() {"IDLE mode", "TX ON", "RX ON", "TX and RX ON"})
        Me.ComboBox6.Location = New System.Drawing.Point(238, 335)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(259, 21)
        Me.ComboBox6.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(91, 385)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(134, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Supported E-UTRA bands:"
        '
        'ComboBox7
        '
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Items.AddRange(New Object() {"Band 1: 1920-1980 MHz TX, 2110-2170 MHz RX", "Band 2: 1850-1910 MHz TX, 1930-1990 MHz RX", "Band 3: 1710-1785 MHz TX, 1805-1880 MHz RX", "Band 4: 1710-1785 MHz TX, 2110-2155 MHz RX", "Band 5: 824-849 MHz TX, 869-894 MHz RX", "Band 7: 2500-2570 MHz TX, 2620-2690 MHz RX", "Band 8: 880-915 MHz TX, 925-960 MHz RX", "Band 12: 699-716 MHz TX, 729-746 MHz RX", "Band 13: 777-787 MHz TX, 746-756 MHz RX", "Band 20: 832-862 MHz TX, 791-821 MHz RX", "Band 25: 1850-1915 MHz TX, 1930-1995 MHz RX", "Band 26: 814-849 MHz TX, 859-894 MHz RX", "Band 29: 717-728 MHz RX", "Band 30: 2305-2315 MHz TX, 2350-2360 MHz RX", "Band 41: 2496-2690 MHz TDD"})
        Me.ComboBox7.Location = New System.Drawing.Point(238, 382)
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(259, 21)
        Me.ComboBox7.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(91, 431)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(141, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Supported CA combinations:"
        '
        'ComboBox8
        '
        Me.ComboBox8.FormattingEnabled = True
        Me.ComboBox8.Items.AddRange(New Object() {"None selected", "Band 1+8: 2110-2170 MHz RX, 925-960 MHz RX", "Band 2+5: 1930-1990 MHz RX, 869-894 MHz RX", "Band 2+12: 1930-1990 MHz RX, 729-746 MHz RX", "Band 2+13: 1930-1990 MHz RX, 746-756 MHz RX", "Band 2+29: 1930-1990 MHz RX, 717-728 MHz RX", "Band 3+7: 1805-1880 MHz RX, 2620-2690 MHz RX", "Band 3+20: 1805-1880 MHz RX, 791-821 MHz RX", "Band 4+5: 2110-2155 MHz RX, 869-894 MHz RX", "Band 4+12: 2110-2155 MHz RX, 729-746 MHz RX", "Band 4+13: 2110-2155 MHz RX, 746-756 MHz RX", "Band 4+29: 2110-2155 MHz RX, 717-728 MHz RX", "Band 5+30: 869-894 MHz RX, 2350-2360 MHz RX", "Band 7+20: 2620-2690 MHz RX, 791-821 MHz RX", "Band 12+30: 729-746 MHz RX, 2350-2360 MHz RX", "Band 41+41: 2496-2690 MHz RX"})
        Me.ComboBox8.Location = New System.Drawing.Point(238, 429)
        Me.ComboBox8.Name = "ComboBox8"
        Me.ComboBox8.Size = New System.Drawing.Size(259, 21)
        Me.ComboBox8.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(91, 477)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "TX power level:"
        '
        'ComboBox9
        '
        Me.ComboBox9.FormattingEnabled = True
        Me.ComboBox9.Items.AddRange(New Object() {"Low power (below 5 dBm)", "Low to medium power (5 to 15 dBm)", "Medium to high power (15 to 20 dBm)", "High power (20 to 30 dBm)"})
        Me.ComboBox9.Location = New System.Drawing.Point(238, 476)
        Me.ComboBox9.Name = "ComboBox9"
        Me.ComboBox9.Size = New System.Drawing.Size(259, 21)
        Me.ComboBox9.TabIndex = 12
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(91, 247)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "VIO voltage:"
        '
        'ComboBox4
        '
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Items.AddRange(New Object() {"OFF/External", "1.2", "1.8"})
        Me.ComboBox4.Location = New System.Drawing.Point(238, 241)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(259, 21)
        Me.ComboBox4.TabIndex = 14
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(91, 201)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(89, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "LM8335 address:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(91, 155)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(66, 13)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Comm Type:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(249, 155)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(33, 13)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "GPIO"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(308, 155)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 13)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "RFFE"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(288, 155)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(14, 13)
        Me.RadioButton1.TabIndex = 20
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(348, 155)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(14, 13)
        Me.RadioButton2.TabIndex = 21
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(89, 547)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(119, 38)
        Me.Button1.TabIndex = 22
        Me.Button1.Text = "Reset"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(259, 547)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(119, 38)
        Me.Button2.TabIndex = 23
        Me.Button2.Text = "Send"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(537, 253)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(287, 250)
        Me.RichTextBox1.TabIndex = 24
        Me.RichTextBox1.Text = ""
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(535, 214)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(77, 13)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "Command Line"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(91, 63)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(61, 13)
        Me.Label15.TabIndex = 26
        Me.Label15.Text = "Comm Port:"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(238, 59)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(259, 21)
        Me.ComboBox1.TabIndex = 27
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(537, 46)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(119, 38)
        Me.Button3.TabIndex = 28
        Me.Button3.Text = "Open"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(704, 46)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(119, 38)
        Me.Button4.TabIndex = 29
        Me.Button4.Text = "Close"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'SerialPort1
        '
        '
        'ComboBox5
        '
        Me.ComboBox5.FormattingEnabled = True
        Me.ComboBox5.Items.AddRange(New Object() {"MCM Sleep Mode", "Main Antenna", "Aux Antenna", "Both Antennas"})
        Me.ComboBox5.Location = New System.Drawing.Point(238, 288)
        Me.ComboBox5.Name = "ComboBox5"
        Me.ComboBox5.Size = New System.Drawing.Size(259, 21)
        Me.ComboBox5.TabIndex = 31
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(91, 293)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(81, 13)
        Me.Label16.TabIndex = 30
        Me.Label16.Text = "Antenna select:"
        '
        'ComboBox3
        '
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {"1 (ADR = Low)", "9 (ADR = High)"})
        Me.ComboBox3.Location = New System.Drawing.Point(238, 193)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(259, 21)
        Me.ComboBox3.TabIndex = 32
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(537, 142)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(119, 38)
        Me.Button5.TabIndex = 35
        Me.Button5.Text = "Set Comm"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Form4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(956, 632)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.ComboBox5)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ComboBox9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.ComboBox8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ComboBox7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBox6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form4"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SmarTenna SDA: Demo Mode"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label3 As Label
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ComboBox6 As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ComboBox7 As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents ComboBox8 As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents ComboBox9 As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents ComboBox4 As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents RadioButton1 As RadioButton
    Friend WithEvents RadioButton2 As RadioButton
    Private WithEvents Button1 As Button
    Private WithEvents Button2 As Button
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Private WithEvents Button3 As Button
    Private WithEvents Button4 As Button
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents ComboBox5 As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents ComboBox3 As ComboBox
    Private WithEvents Button5 As Button
End Class
