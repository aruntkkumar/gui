<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits MetroFramework.Forms.MetroForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddMarkerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ComparisonModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ClearSelectedMarkerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearAllMarkersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearChartAreaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ChartTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogMagToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LinearMagToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PhaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnwrappedPhaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PolarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SmithChartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InverseSmithChartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupDelayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SWRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RealToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImaginaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConnectivityToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScanMyDevicesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FirstDevice = New System.Windows.Forms.ToolStripMenuItem()
        Me.SecondDevice = New System.Windows.Forms.ToolStripMenuItem()
        Me.ThirdDevice = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeviceOptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New MetroFramework.Controls.MetroTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TextBox1 = New System.Windows.Forms.RichTextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.Toggle1 = New MetroFramework.Controls.MetroToggle()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pictureBox1
        '
        Me.pictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
        Me.pictureBox1.Location = New System.Drawing.Point(747, 508)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(86, 45)
        Me.pictureBox1.TabIndex = 15
        Me.pictureBox1.TabStop = False
        '
        'label3
        '
        Me.label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label3.Font = New System.Drawing.Font("Calibri", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.label3.Location = New System.Drawing.Point(692, 538)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(46, 15)
        Me.label3.TabIndex = 16
        Me.label3.Text = "Ver. 3.0"
        '
        'Chart1
        '
        Me.Chart1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea1.AxisX.IsLabelAutoFit = False
        ChartArea1.AxisX.LabelStyle.Angle = -90
        ChartArea1.AxisX.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(0, Byte))
        ChartArea1.AxisX.TitleFont = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        ChartArea1.AxisX2.IsLabelAutoFit = False
        ChartArea1.AxisX2.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(0, Byte))
        ChartArea1.AxisX2.TitleFont = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        ChartArea1.AxisY.IsLabelAutoFit = False
        ChartArea1.AxisY.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        ChartArea1.AxisY.TitleFont = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        ChartArea1.AxisY2.IsLabelAutoFit = False
        ChartArea1.AxisY2.LabelStyle.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        ChartArea1.AxisY2.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270
        ChartArea1.AxisY2.TitleFont = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        ChartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        Legend1.IsTextAutoFit = False
        Legend1.Name = "Legend1"
        Legend1.TitleFont = New System.Drawing.Font("Microsoft Sans Serif", 10.66667!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World)
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(0, 88)
        Me.Chart1.Name = "Chart1"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine
        Series1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Size = New System.Drawing.Size(856, 351)
        Me.Chart1.TabIndex = 17
        Me.Chart1.Text = "Chart1"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.Window
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.ConnectivityToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(20, 60)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(816, 24)
        Me.MenuStrip1.TabIndex = 21
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.AddToolStripMenuItem, Me.ToolStripMenuItem1, Me.SaveToolStripMenuItem, Me.ToolStripMenuItem2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.NewToolStripMenuItem.Text = "New Touchstone File"
        '
        'AddToolStripMenuItem
        '
        Me.AddToolStripMenuItem.Name = "AddToolStripMenuItem"
        Me.AddToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.AddToolStripMenuItem.Text = "Add/New Data File"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(182, 6)
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.SaveToolStripMenuItem.Text = "Save As"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(182, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddMarkerToolStripMenuItem, Me.ComparisonModeToolStripMenuItem, Me.ToolStripMenuItem4, Me.ClearSelectedMarkerToolStripMenuItem, Me.ClearAllMarkersToolStripMenuItem, Me.ClearChartAreaToolStripMenuItem, Me.ToolStripMenuItem3, Me.OptionsToolStripMenuItem, Me.ToolStripMenuItem5, Me.ChartTypeToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'AddMarkerToolStripMenuItem
        '
        Me.AddMarkerToolStripMenuItem.Name = "AddMarkerToolStripMenuItem"
        Me.AddMarkerToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.AddMarkerToolStripMenuItem.Text = "Add Marker"
        '
        'ComparisonModeToolStripMenuItem
        '
        Me.ComparisonModeToolStripMenuItem.Name = "ComparisonModeToolStripMenuItem"
        Me.ComparisonModeToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ComparisonModeToolStripMenuItem.Text = "Comparison Mode"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(185, 6)
        '
        'ClearSelectedMarkerToolStripMenuItem
        '
        Me.ClearSelectedMarkerToolStripMenuItem.Name = "ClearSelectedMarkerToolStripMenuItem"
        Me.ClearSelectedMarkerToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ClearSelectedMarkerToolStripMenuItem.Text = "Clear Selected Marker"
        '
        'ClearAllMarkersToolStripMenuItem
        '
        Me.ClearAllMarkersToolStripMenuItem.Name = "ClearAllMarkersToolStripMenuItem"
        Me.ClearAllMarkersToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ClearAllMarkersToolStripMenuItem.Text = "Clear All Markers"
        '
        'ClearChartAreaToolStripMenuItem
        '
        Me.ClearChartAreaToolStripMenuItem.Name = "ClearChartAreaToolStripMenuItem"
        Me.ClearChartAreaToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ClearChartAreaToolStripMenuItem.Text = "Clear Chart Area"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(185, 6)
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(185, 6)
        '
        'ChartTypeToolStripMenuItem
        '
        Me.ChartTypeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogMagToolStripMenuItem, Me.LinearMagToolStripMenuItem, Me.PhaseToolStripMenuItem, Me.UnwrappedPhaseToolStripMenuItem, Me.PolarToolStripMenuItem, Me.SmithChartToolStripMenuItem, Me.InverseSmithChartToolStripMenuItem, Me.GroupDelayToolStripMenuItem, Me.SWRToolStripMenuItem, Me.RealToolStripMenuItem, Me.ImaginaryToolStripMenuItem})
        Me.ChartTypeToolStripMenuItem.Name = "ChartTypeToolStripMenuItem"
        Me.ChartTypeToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.ChartTypeToolStripMenuItem.Text = "Chart Format"
        '
        'LogMagToolStripMenuItem
        '
        Me.LogMagToolStripMenuItem.Checked = True
        Me.LogMagToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LogMagToolStripMenuItem.Name = "LogMagToolStripMenuItem"
        Me.LogMagToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.LogMagToolStripMenuItem.Text = "Log Mag"
        '
        'LinearMagToolStripMenuItem
        '
        Me.LinearMagToolStripMenuItem.Name = "LinearMagToolStripMenuItem"
        Me.LinearMagToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.LinearMagToolStripMenuItem.Text = "Linear Mag"
        '
        'PhaseToolStripMenuItem
        '
        Me.PhaseToolStripMenuItem.Name = "PhaseToolStripMenuItem"
        Me.PhaseToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.PhaseToolStripMenuItem.Text = "Phase"
        '
        'UnwrappedPhaseToolStripMenuItem
        '
        Me.UnwrappedPhaseToolStripMenuItem.Name = "UnwrappedPhaseToolStripMenuItem"
        Me.UnwrappedPhaseToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.UnwrappedPhaseToolStripMenuItem.Text = "Unwrapped Phase"
        '
        'PolarToolStripMenuItem
        '
        Me.PolarToolStripMenuItem.Name = "PolarToolStripMenuItem"
        Me.PolarToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.PolarToolStripMenuItem.Text = "Polar"
        '
        'SmithChartToolStripMenuItem
        '
        Me.SmithChartToolStripMenuItem.Name = "SmithChartToolStripMenuItem"
        Me.SmithChartToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.SmithChartToolStripMenuItem.Text = "Smith Chart"
        '
        'InverseSmithChartToolStripMenuItem
        '
        Me.InverseSmithChartToolStripMenuItem.Name = "InverseSmithChartToolStripMenuItem"
        Me.InverseSmithChartToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.InverseSmithChartToolStripMenuItem.Text = "Inverse Smith Chart"
        '
        'GroupDelayToolStripMenuItem
        '
        Me.GroupDelayToolStripMenuItem.Name = "GroupDelayToolStripMenuItem"
        Me.GroupDelayToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.GroupDelayToolStripMenuItem.Text = "Group Delay"
        '
        'SWRToolStripMenuItem
        '
        Me.SWRToolStripMenuItem.Name = "SWRToolStripMenuItem"
        Me.SWRToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.SWRToolStripMenuItem.Text = "SWR"
        '
        'RealToolStripMenuItem
        '
        Me.RealToolStripMenuItem.Name = "RealToolStripMenuItem"
        Me.RealToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.RealToolStripMenuItem.Text = "Real"
        '
        'ImaginaryToolStripMenuItem
        '
        Me.ImaginaryToolStripMenuItem.Name = "ImaginaryToolStripMenuItem"
        Me.ImaginaryToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ImaginaryToolStripMenuItem.Text = "Imaginary"
        '
        'ConnectivityToolStripMenuItem
        '
        Me.ConnectivityToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ScanMyDevicesToolStripMenuItem, Me.DeviceOptionsToolStripMenuItem})
        Me.ConnectivityToolStripMenuItem.Name = "ConnectivityToolStripMenuItem"
        Me.ConnectivityToolStripMenuItem.Size = New System.Drawing.Size(86, 20)
        Me.ConnectivityToolStripMenuItem.Text = "Connectivity"
        '
        'ScanMyDevicesToolStripMenuItem
        '
        Me.ScanMyDevicesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FirstDevice, Me.SecondDevice, Me.ThirdDevice})
        Me.ScanMyDevicesToolStripMenuItem.Name = "ScanMyDevicesToolStripMenuItem"
        Me.ScanMyDevicesToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.ScanMyDevicesToolStripMenuItem.Text = "My Devices"
        '
        'FirstDevice
        '
        Me.FirstDevice.Name = "FirstDevice"
        Me.FirstDevice.Size = New System.Drawing.Size(326, 22)
        Me.FirstDevice.Text = "Agilent Technologies N5230A Network Analyzer"
        '
        'SecondDevice
        '
        Me.SecondDevice.Name = "SecondDevice"
        Me.SecondDevice.Size = New System.Drawing.Size(326, 22)
        Me.SecondDevice.Text = "Rohde && Schwarz ZVL6"
        '
        'ThirdDevice
        '
        Me.ThirdDevice.Name = "ThirdDevice"
        Me.ThirdDevice.Size = New System.Drawing.Size(326, 22)
        Me.ThirdDevice.Text = "Keysight E5071C ENA Vector Network Analyzer"
        '
        'DeviceOptionsToolStripMenuItem
        '
        Me.DeviceOptionsToolStripMenuItem.Name = "DeviceOptionsToolStripMenuItem"
        Me.DeviceOptionsToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.DeviceOptionsToolStripMenuItem.Text = "Device Options"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Regular
        Me.TabControl1.Location = New System.Drawing.Point(50, 426)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(636, 136)
        Me.TabControl1.TabIndex = 23
        Me.TabControl1.UseSelectable = True
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.Transparent
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 38)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(628, 94)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "File Location"
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(11, 9)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(609, 77)
        Me.TextBox1.TabIndex = 25
        Me.TextBox1.Text = ""
        Me.TextBox1.WordWrap = False
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.Transparent
        Me.TabPage2.Controls.Add(Me.CheckedListBox1)
        Me.TabPage2.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 35)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(628, 97)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Marker List"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.CheckedListBox1.ColumnWidth = 150
        Me.CheckedListBox1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(0, Byte))
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Location = New System.Drawing.Point(10, 6)
        Me.CheckedListBox1.MultiColumn = True
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(608, 90)
        Me.CheckedListBox1.TabIndex = 23
        '
        'Toggle1
        '
        Me.Toggle1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Toggle1.AutoSize = True
        Me.Toggle1.Location = New System.Drawing.Point(753, 463)
        Me.Toggle1.Name = "Toggle1"
        Me.Toggle1.Size = New System.Drawing.Size(80, 17)
        Me.Toggle1.TabIndex = 125
        Me.Toggle1.Text = "Off"
        Me.Toggle1.UseSelectable = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Calibri", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(692, 464)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 15)
        Me.Label1.TabIndex = 126
        Me.Label1.Text = "Live Feed"
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackMaxSize = 30
        Me.ClientSize = New System.Drawing.Size(856, 573)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Toggle1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(856, 573)
        Me.Name = "Form1"
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow
        Me.Text = "SAT Test Suite"
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents pictureBox1 As PictureBox
    Private WithEvents label3 As Label
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents Timer1 As Timer
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearAllMarkersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As ToolStripSeparator
    Friend WithEvents ConnectivityToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScanMyDevicesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FirstDevice As ToolStripMenuItem
    Friend WithEvents DeviceOptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SecondDevice As ToolStripMenuItem
    Friend WithEvents ClearSelectedMarkerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabControl1 As MetroFramework.Controls.MetroTabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents TextBox1 As RichTextBox
    Friend WithEvents ToolStripMenuItem3 As ToolStripSeparator
    Friend WithEvents ComparisonModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearChartAreaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddMarkerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As ToolStripSeparator
    Friend WithEvents ChartTypeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LogMagToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LinearMagToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PhaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UnwrappedPhaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PolarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SmithChartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InverseSmithChartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupDelayToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SWRToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RealToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImaginaryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ThirdDevice As ToolStripMenuItem
    Friend WithEvents Toggle1 As MetroFramework.Controls.MetroToggle
    Private WithEvents Label1 As Label
End Class
