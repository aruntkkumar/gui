<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form4
    Inherits MetroFramework.Forms.MetroForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form4))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FullScreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MyDevicesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PINGPIOToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SCOUTSC4410ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SierraWirelessToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DisableToolbarsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommPortSelectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VIOSelectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.EnableAdminModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ComboBox1 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox2 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox3 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox4 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox5 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox6 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox7 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox8 = New MetroFramework.Controls.MetroComboBox()
        Me.ComboBox9 = New MetroFramework.Controls.MetroComboBox()
        Me.Label15 = New MetroFramework.Controls.MetroLabel()
        Me.Label4 = New MetroFramework.Controls.MetroLabel()
        Me.Label11 = New MetroFramework.Controls.MetroLabel()
        Me.Label10 = New MetroFramework.Controls.MetroLabel()
        Me.Label9 = New MetroFramework.Controls.MetroLabel()
        Me.Label16 = New MetroFramework.Controls.MetroLabel()
        Me.Label5 = New MetroFramework.Controls.MetroLabel()
        Me.Label6 = New MetroFramework.Controls.MetroLabel()
        Me.Label7 = New MetroFramework.Controls.MetroLabel()
        Me.Label8 = New MetroFramework.Controls.MetroLabel()
        Me.Button1 = New MetroFramework.Controls.MetroButton()
        Me.Button2 = New MetroFramework.Controls.MetroButton()
        Me.Button3 = New MetroFramework.Controls.MetroButton()
        Me.Button4 = New MetroFramework.Controls.MetroButton()
        Me.Button5 = New MetroFramework.Controls.MetroButton()
        Me.Button6 = New MetroFramework.Controls.MetroButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label12 = New MetroFramework.Controls.MetroLabel()
        Me.Label13 = New MetroFramework.Controls.MetroLabel()
        Me.RadioButton1 = New MetroFramework.Controls.MetroRadioButton()
        Me.RadioButton2 = New MetroFramework.Controls.MetroRadioButton()
        Me.Label14 = New MetroFramework.Controls.MetroLabel()
        Me.RadioButton3 = New MetroFramework.Controls.MetroRadioButton()
        Me.Label18 = New MetroFramework.Controls.MetroLabel()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.Window
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(20, 60)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(936, 24)
        Me.MenuStrip1.TabIndex = 37
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.ToolStripMenuItem1, Me.SaveToolStripMenuItem, Me.ToolStripMenuItem2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(100, 6)
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(100, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FullScreenToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'FullScreenToolStripMenuItem
        '
        Me.FullScreenToolStripMenuItem.Name = "FullScreenToolStripMenuItem"
        Me.FullScreenToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.FullScreenToolStripMenuItem.Text = "Full Screen"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem, Me.ToolStripMenuItem3, Me.EnableAdminModeToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MyDevicesToolStripMenuItem, Me.DisableToolbarsToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'MyDevicesToolStripMenuItem
        '
        Me.MyDevicesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PINGPIOToolStripMenuItem, Me.SCOUTSC4410ToolStripMenuItem, Me.SierraWirelessToolStripMenuItem})
        Me.MyDevicesToolStripMenuItem.Name = "MyDevicesToolStripMenuItem"
        Me.MyDevicesToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.MyDevicesToolStripMenuItem.Text = "My Devices"
        '
        'PINGPIOToolStripMenuItem
        '
        Me.PINGPIOToolStripMenuItem.Name = "PINGPIOToolStripMenuItem"
        Me.PINGPIOToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.PINGPIOToolStripMenuItem.Text = "NUMATO USB GPIO 8"
        '
        'SCOUTSC4410ToolStripMenuItem
        '
        Me.SCOUTSC4410ToolStripMenuItem.Name = "SCOUTSC4410ToolStripMenuItem"
        Me.SCOUTSC4410ToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.SCOUTSC4410ToolStripMenuItem.Text = "SCOUT SC4410"
        '
        'SierraWirelessToolStripMenuItem
        '
        Me.SierraWirelessToolStripMenuItem.Name = "SierraWirelessToolStripMenuItem"
        Me.SierraWirelessToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.SierraWirelessToolStripMenuItem.Text = "Sierra Wireless"
        '
        'DisableToolbarsToolStripMenuItem
        '
        Me.DisableToolbarsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CommPortSelectToolStripMenuItem, Me.VIOSelectToolStripMenuItem})
        Me.DisableToolbarsToolStripMenuItem.Name = "DisableToolbarsToolStripMenuItem"
        Me.DisableToolbarsToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.DisableToolbarsToolStripMenuItem.Text = "Disable Toolbars"
        '
        'CommPortSelectToolStripMenuItem
        '
        Me.CommPortSelectToolStripMenuItem.Name = "CommPortSelectToolStripMenuItem"
        Me.CommPortSelectToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.CommPortSelectToolStripMenuItem.Text = "Comm Port Select"
        '
        'VIOSelectToolStripMenuItem
        '
        Me.VIOSelectToolStripMenuItem.Name = "VIOSelectToolStripMenuItem"
        Me.VIOSelectToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.VIOSelectToolStripMenuItem.Text = "VIO Select"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(179, 6)
        '
        'EnableAdminModeToolStripMenuItem
        '
        Me.EnableAdminModeToolStripMenuItem.Name = "EnableAdminModeToolStripMenuItem"
        Me.EnableAdminModeToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.EnableAdminModeToolStripMenuItem.Text = "Enable Admin mode"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.AboutToolStripMenuItem.Text = "About SmarTenna SDA"
        '
        'SerialPort1
        '
        '
        'Timer1
        '
        '
        'ComboBox1
        '
        Me.ComboBox1.FontSize = MetroFramework.MetroComboBoxSize.Small
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.ItemHeight = 19
        Me.ComboBox1.Location = New System.Drawing.Point(273, 102)
        Me.ComboBox1.MaxDropDownItems = 20
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(265, 25)
        Me.ComboBox1.TabIndex = 38
        Me.ComboBox1.UseSelectable = True
        '
        'ComboBox2
        '
        Me.ComboBox2.FontSize = MetroFramework.MetroComboBoxSize.Small
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.ItemHeight = 19
        Me.ComboBox2.Items.AddRange(New Object() {"256000", "128000", "115200", "57600", "38400", "28800", "19200", "14400", "9600", "4800", "2400", "1200", "600"})
        Me.ComboBox2.Location = New System.Drawing.Point(273, 155)
        Me.ComboBox2.MaxDropDownItems = 20
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(265, 25)
        Me.ComboBox2.TabIndex = 39
        Me.ComboBox2.UseSelectable = True
        '
        'ComboBox3
        '
        Me.ComboBox3.FontSize = MetroFramework.MetroComboBoxSize.Small
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.ItemHeight = 19
        Me.ComboBox3.Items.AddRange(New Object() {"1 (ADR = Low)", "9 (ADR = High)"})
        Me.ComboBox3.Location = New System.Drawing.Point(273, 261)
        Me.ComboBox3.MaxDropDownItems = 20
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(265, 25)
        Me.ComboBox3.TabIndex = 40
        Me.ComboBox3.UseSelectable = True
        '
        'ComboBox4
        '
        Me.ComboBox4.FontSize = MetroFramework.MetroComboBoxSize.Small
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.ItemHeight = 19
        Me.ComboBox4.Items.AddRange(New Object() {"OFF/External", "1.2", "1.8"})
        Me.ComboBox4.Location = New System.Drawing.Point(273, 314)
        Me.ComboBox4.MaxDropDownItems = 20
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(265, 25)
        Me.ComboBox4.TabIndex = 41
        Me.ComboBox4.UseSelectable = True
        '
        'ComboBox5
        '
        Me.ComboBox5.FontSize = MetroFramework.MetroComboBoxSize.Small
        Me.ComboBox5.FormattingEnabled = True
        Me.ComboBox5.ItemHeight = 19
        Me.ComboBox5.Items.AddRange(New Object() {"MCM Sleep Mode", "Main Antenna", "Aux Antenna", "Both Antennas"})
        Me.ComboBox5.Location = New System.Drawing.Point(273, 367)
        Me.ComboBox5.MaxDropDownItems = 20
        Me.ComboBox5.Name = "ComboBox5"
        Me.ComboBox5.Size = New System.Drawing.Size(265, 25)
        Me.ComboBox5.TabIndex = 42
        Me.ComboBox5.UseSelectable = True
        '
        'ComboBox6
        '
        Me.ComboBox6.FontSize = MetroFramework.MetroComboBoxSize.Small
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.ItemHeight = 19
        Me.ComboBox6.Items.AddRange(New Object() {"IDLE mode", "TX ON", "RX ON", "TX and RX ON"})
        Me.ComboBox6.Location = New System.Drawing.Point(273, 420)
        Me.ComboBox6.MaxDropDownItems = 20
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(265, 25)
        Me.ComboBox6.TabIndex = 43
        Me.ComboBox6.UseSelectable = True
        '
        'ComboBox7
        '
        Me.ComboBox7.FontSize = MetroFramework.MetroComboBoxSize.Small
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.ItemHeight = 19
        Me.ComboBox7.Items.AddRange(New Object() {"Band 1: 1920-1980 MHz TX, 2110-2170 MHz RX", "Band 2: 1850-1910 MHz TX, 1930-1990 MHz RX", "Band 3: 1710-1785 MHz TX, 1805-1880 MHz RX", "Band 4: 1710-1785 MHz TX, 2110-2155 MHz RX", "Band 5: 824-849 MHz TX, 869-894 MHz RX", "Band 7: 2500-2570 MHz TX, 2620-2690 MHz RX", "Band 8: 880-915 MHz TX, 925-960 MHz RX", "Band 12: 699-716 MHz TX, 729-746 MHz RX", "Band 13: 777-787 MHz TX, 746-756 MHz RX", "Band 20: 832-862 MHz TX, 791-821 MHz RX", "Band 25: 1850-1915 MHz TX, 1930-1995 MHz RX", "Band 26: 814-849 MHz TX, 859-894 MHz RX", "Band 29: 717-728 MHz RX", "Band 30: 2305-2315 MHz TX, 2350-2360 MHz RX", "Band 41: 2496-2690 MHz TDD"})
        Me.ComboBox7.Location = New System.Drawing.Point(273, 473)
        Me.ComboBox7.MaxDropDownItems = 20
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(265, 25)
        Me.ComboBox7.TabIndex = 44
        Me.ComboBox7.UseSelectable = True
        '
        'ComboBox8
        '
        Me.ComboBox8.FontSize = MetroFramework.MetroComboBoxSize.Small
        Me.ComboBox8.FormattingEnabled = True
        Me.ComboBox8.ItemHeight = 19
        Me.ComboBox8.Items.AddRange(New Object() {"None selected", "Band 1+8: 2110-2170 MHz RX, 925-960 MHz RX", "Band 2+5: 1930-1990 MHz RX, 869-894 MHz RX", "Band 2+12: 1930-1990 MHz RX, 729-746 MHz RX", "Band 2+13: 1930-1990 MHz RX, 746-756 MHz RX", "Band 2+29: 1930-1990 MHz RX, 717-728 MHz RX", "Band 3+7: 1805-1880 MHz RX, 2620-2690 MHz RX", "Band 3+20: 1805-1880 MHz RX, 791-821 MHz RX", "Band 4+5: 2110-2155 MHz RX, 869-894 MHz RX", "Band 4+12: 2110-2155 MHz RX, 729-746 MHz RX", "Band 4+13: 2110-2155 MHz RX, 746-756 MHz RX", "Band 4+29: 2110-2155 MHz RX, 717-728 MHz RX", "Band 5+30: 869-894 MHz RX, 2350-2360 MHz RX", "Band 7+20: 2620-2690 MHz RX, 791-821 MHz RX", "Band 12+30: 729-746 MHz RX, 2350-2360 MHz RX", "Band 41+41: 2496-2690 MHz RX"})
        Me.ComboBox8.Location = New System.Drawing.Point(273, 526)
        Me.ComboBox8.MaxDropDownItems = 20
        Me.ComboBox8.Name = "ComboBox8"
        Me.ComboBox8.Size = New System.Drawing.Size(265, 25)
        Me.ComboBox8.TabIndex = 45
        Me.ComboBox8.UseSelectable = True
        '
        'ComboBox9
        '
        Me.ComboBox9.FontSize = MetroFramework.MetroComboBoxSize.Small
        Me.ComboBox9.FormattingEnabled = True
        Me.ComboBox9.ItemHeight = 19
        Me.ComboBox9.Items.AddRange(New Object() {"Low power (below 5 dBm)", "Low to medium power (5 to 15 dBm)", "Medium to high power (15 to 20 dBm)", "High power (20 to 30 dBm)"})
        Me.ComboBox9.Location = New System.Drawing.Point(273, 579)
        Me.ComboBox9.MaxDropDownItems = 20
        Me.ComboBox9.Name = "ComboBox9"
        Me.ComboBox9.Size = New System.Drawing.Size(265, 25)
        Me.ComboBox9.TabIndex = 46
        Me.ComboBox9.UseSelectable = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(95, 102)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(82, 19)
        Me.Label15.TabIndex = 47
        Me.Label15.Text = "Comm Port:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(95, 155)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 19)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Baud Rate:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(95, 208)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(85, 19)
        Me.Label11.TabIndex = 49
        Me.Label11.Text = "Comm Type:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(95, 261)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(107, 19)
        Me.Label10.TabIndex = 50
        Me.Label10.Text = "LM8335 address:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(95, 314)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(81, 19)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "VIO voltage:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(95, 367)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 19)
        Me.Label16.TabIndex = 52
        Me.Label16.Text = "Antenna select:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(95, 420)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 19)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "TRX select:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(95, 473)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(163, 19)
        Me.Label6.TabIndex = 54
        Me.Label6.Text = "Supported E-UTRA bands:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(95, 526)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(178, 19)
        Me.Label7.TabIndex = 55
        Me.Label7.Text = "Supported CA combinations:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(95, 579)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 19)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "TX power level:"
        '
        'Button1
        '
        Me.Button1.DisplayFocus = True
        Me.Button1.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.Button1.Location = New System.Drawing.Point(95, 632)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(119, 38)
        Me.Button1.TabIndex = 57
        Me.Button1.Text = "Reset to Default"
        Me.Button1.UseSelectable = True
        '
        'Button2
        '
        Me.Button2.DisplayFocus = True
        Me.Button2.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.Button2.Location = New System.Drawing.Point(273, 632)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(119, 38)
        Me.Button2.TabIndex = 58
        Me.Button2.Text = "Send"
        Me.Button2.UseSelectable = True
        '
        'Button3
        '
        Me.Button3.DisplayFocus = True
        Me.Button3.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.Button3.Location = New System.Drawing.Point(591, 102)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(119, 38)
        Me.Button3.TabIndex = 59
        Me.Button3.Text = "Open"
        Me.Button3.UseSelectable = True
        '
        'Button4
        '
        Me.Button4.DisplayFocus = True
        Me.Button4.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.Button4.Location = New System.Drawing.Point(745, 102)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(119, 38)
        Me.Button4.TabIndex = 60
        Me.Button4.Text = "Close"
        Me.Button4.UseSelectable = True
        '
        'Button5
        '
        Me.Button5.DisplayFocus = True
        Me.Button5.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.Button5.Location = New System.Drawing.Point(591, 199)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(119, 38)
        Me.Button5.TabIndex = 61
        Me.Button5.Text = "Set Comm"
        Me.Button5.UseSelectable = True
        '
        'Button6
        '
        Me.Button6.DisplayFocus = True
        Me.Button6.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.Button6.Location = New System.Drawing.Point(591, 632)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(119, 38)
        Me.Button6.TabIndex = 62
        Me.Button6.Text = "Clear Screen"
        Me.Button6.UseSelectable = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(891, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 14)
        Me.Label3.TabIndex = 66
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(858, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 14)
        Me.Label2.TabIndex = 65
        Me.Label2.Text = "Date:"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(858, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(80, 14)
        Me.Label1.TabIndex = 64
        Me.Label1.Text = "Build VER: 1.0"
        '
        'Label17
        '
        Me.Label17.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(497, 693)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(459, 14)
        Me.Label17.TabIndex = 68
        Me.Label17.Text = "Copyright © 2013-2016 SMART ANTENNA TECHNOLOGIES LIMITED. All Rights Reserved."
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(862, 641)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(94, 49)
        Me.PictureBox1.TabIndex = 67
        Me.PictureBox1.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(273, 208)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(47, 19)
        Me.Label12.TabIndex = 69
        Me.Label12.Text = "GPIO8"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(376, 208)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(38, 19)
        Me.Label13.TabIndex = 70
        Me.Label13.Text = "RFFE"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(326, 211)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(26, 15)
        Me.RadioButton1.TabIndex = 71
        Me.RadioButton1.Text = " "
        Me.RadioButton1.UseSelectable = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(420, 211)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(26, 15)
        Me.RadioButton2.TabIndex = 72
        Me.RadioButton2.Text = " "
        Me.RadioButton2.UseSelectable = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(591, 261)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 19)
        Me.Label14.TabIndex = 73
        Me.Label14.Text = "Command Line"
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(523, 211)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(26, 15)
        Me.RadioButton3.TabIndex = 75
        Me.RadioButton3.Text = " "
        Me.RadioButton3.UseSelectable = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(470, 208)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(47, 19)
        Me.Label18.TabIndex = 74
        Me.Label18.Text = "GPIO4"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.RichTextBox1.BackColor = System.Drawing.SystemColors.InfoText
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RichTextBox1.ForeColor = System.Drawing.SystemColors.Window
        Me.RichTextBox1.Location = New System.Drawing.Point(591, 296)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(273, 310)
        Me.RichTextBox1.TabIndex = 76
        Me.RichTextBox1.Text = ""
        '
        'Form4
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(976, 727)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.RadioButton3)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.ComboBox9)
        Me.Controls.Add(Me.ComboBox8)
        Me.Controls.Add(Me.ComboBox7)
        Me.Controls.Add(Me.ComboBox6)
        Me.Controls.Add(Me.ComboBox5)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form4"
        Me.Resizable = False
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow
        Me.Text = "SmarTenna (TM) SDA: Demo Mode"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FullScreenToolStripMenuItem As ToolStripMenuItem
    Public WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MyDevicesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SCOUTSC4410ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SierraWirelessToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PINGPIOToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DisableToolbarsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CommPortSelectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VIOSelectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripSeparator
    Friend WithEvents EnableAdminModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ComboBox1 As MetroFramework.Controls.MetroComboBox
    Friend WithEvents ComboBox2 As MetroFramework.Controls.MetroComboBox
    Friend WithEvents ComboBox3 As MetroFramework.Controls.MetroComboBox
    Friend WithEvents ComboBox4 As MetroFramework.Controls.MetroComboBox
    Friend WithEvents ComboBox5 As MetroFramework.Controls.MetroComboBox
    Friend WithEvents ComboBox6 As MetroFramework.Controls.MetroComboBox
    Friend WithEvents ComboBox7 As MetroFramework.Controls.MetroComboBox
    Friend WithEvents ComboBox8 As MetroFramework.Controls.MetroComboBox
    Friend WithEvents ComboBox9 As MetroFramework.Controls.MetroComboBox
    Friend WithEvents Label15 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label4 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label11 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label10 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label9 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label16 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label5 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label6 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label7 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label8 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Button1 As MetroFramework.Controls.MetroButton
    Friend WithEvents Button2 As MetroFramework.Controls.MetroButton
    Friend WithEvents Button3 As MetroFramework.Controls.MetroButton
    Friend WithEvents Button4 As MetroFramework.Controls.MetroButton
    Friend WithEvents Button5 As MetroFramework.Controls.MetroButton
    Friend WithEvents Button6 As MetroFramework.Controls.MetroButton
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label12 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label13 As MetroFramework.Controls.MetroLabel
    Friend WithEvents RadioButton1 As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents RadioButton2 As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents Label14 As MetroFramework.Controls.MetroLabel
    Friend WithEvents RadioButton3 As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents Label18 As MetroFramework.Controls.MetroLabel
    Friend WithEvents RichTextBox1 As RichTextBox
End Class
