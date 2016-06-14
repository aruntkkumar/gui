<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel1 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel2 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
        Me.MetroLabel4 = New MetroFramework.Controls.MetroLabel()
        Me.TextBox1 = New MetroFramework.Controls.MetroTextBox()
        Me.TextBox2 = New MetroFramework.Controls.MetroTextBox()
        Me.TextBox4 = New MetroFramework.Controls.MetroTextBox()
        Me.TextBox3 = New MetroFramework.Controls.MetroTextBox()
        Me.Button1 = New MetroFramework.Controls.MetroButton()
        Me.Button2 = New MetroFramework.Controls.MetroButton()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.TextBox4)
        Me.Panel1.Controls.Add(Me.TextBox3)
        Me.Panel1.Controls.Add(Me.TextBox2)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.MetroLabel4)
        Me.Panel1.Controls.Add(Me.MetroLabel3)
        Me.Panel1.Controls.Add(Me.MetroLabel2)
        Me.Panel1.Controls.Add(Me.MetroLabel1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(30, 87)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(494, 139)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "X-axis maximum (GHz):"
        '
        'MetroLabel1
        '
        Me.MetroLabel1.AutoSize = True
        Me.MetroLabel1.Location = New System.Drawing.Point(18, 86)
        Me.MetroLabel1.Name = "MetroLabel1"
        Me.MetroLabel1.Size = New System.Drawing.Size(143, 19)
        Me.MetroLabel1.TabIndex = 1
        Me.MetroLabel1.Text = "X-axis minimum (GHz):"
        '
        'MetroLabel2
        '
        Me.MetroLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MetroLabel2.AutoSize = True
        Me.MetroLabel2.Location = New System.Drawing.Point(268, 86)
        Me.MetroLabel2.Name = "MetroLabel2"
        Me.MetroLabel2.Size = New System.Drawing.Size(135, 19)
        Me.MetroLabel2.TabIndex = 2
        Me.MetroLabel2.Text = "Y-axis minimum (dB):"
        '
        'MetroLabel3
        '
        Me.MetroLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MetroLabel3.AutoSize = True
        Me.MetroLabel3.Location = New System.Drawing.Point(268, 53)
        Me.MetroLabel3.Name = "MetroLabel3"
        Me.MetroLabel3.Size = New System.Drawing.Size(138, 19)
        Me.MetroLabel3.TabIndex = 3
        Me.MetroLabel3.Text = "Y-axis maximum (dB):"
        '
        'MetroLabel4
        '
        Me.MetroLabel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.MetroLabel4.AutoSize = True
        Me.MetroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Bold
        Me.MetroLabel4.Location = New System.Drawing.Point(181, 18)
        Me.MetroLabel4.Name = "MetroLabel4"
        Me.MetroLabel4.Size = New System.Drawing.Size(134, 19)
        Me.MetroLabel4.TabIndex = 4
        Me.MetroLabel4.Text = "AXIS PARAMETERS"
        '
        'TextBox1
        '
        '
        '
        '
        Me.TextBox1.CustomButton.Image = Nothing
        Me.TextBox1.CustomButton.Location = New System.Drawing.Point(42, 2)
        Me.TextBox1.CustomButton.Name = ""
        Me.TextBox1.CustomButton.Size = New System.Drawing.Size(15, 15)
        Me.TextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.TextBox1.CustomButton.TabIndex = 1
        Me.TextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TextBox1.CustomButton.UseSelectable = True
        Me.TextBox1.CustomButton.Visible = False
        Me.TextBox1.Lines = New String(-1) {}
        Me.TextBox1.Location = New System.Drawing.Point(166, 53)
        Me.TextBox1.MaxLength = 32767
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TextBox1.SelectedText = ""
        Me.TextBox1.SelectionLength = 0
        Me.TextBox1.SelectionStart = 0
        Me.TextBox1.Size = New System.Drawing.Size(60, 20)
        Me.TextBox1.TabIndex = 6
        Me.TextBox1.UseSelectable = True
        Me.TextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TextBox1.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World)
        '
        'TextBox2
        '
        '
        '
        '
        Me.TextBox2.CustomButton.Image = Nothing
        Me.TextBox2.CustomButton.Location = New System.Drawing.Point(42, 2)
        Me.TextBox2.CustomButton.Name = ""
        Me.TextBox2.CustomButton.Size = New System.Drawing.Size(15, 15)
        Me.TextBox2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.TextBox2.CustomButton.TabIndex = 1
        Me.TextBox2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TextBox2.CustomButton.UseSelectable = True
        Me.TextBox2.CustomButton.Visible = False
        Me.TextBox2.Lines = New String(-1) {}
        Me.TextBox2.Location = New System.Drawing.Point(166, 87)
        Me.TextBox2.MaxLength = 32767
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TextBox2.SelectedText = ""
        Me.TextBox2.SelectionLength = 0
        Me.TextBox2.SelectionStart = 0
        Me.TextBox2.Size = New System.Drawing.Size(60, 20)
        Me.TextBox2.TabIndex = 7
        Me.TextBox2.UseSelectable = True
        Me.TextBox2.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TextBox2.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World)
        '
        'TextBox4
        '
        Me.TextBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.TextBox4.CustomButton.Image = Nothing
        Me.TextBox4.CustomButton.Location = New System.Drawing.Point(42, 2)
        Me.TextBox4.CustomButton.Name = ""
        Me.TextBox4.CustomButton.Size = New System.Drawing.Size(15, 15)
        Me.TextBox4.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.TextBox4.CustomButton.TabIndex = 1
        Me.TextBox4.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TextBox4.CustomButton.UseSelectable = True
        Me.TextBox4.CustomButton.Visible = False
        Me.TextBox4.Lines = New String(-1) {}
        Me.TextBox4.Location = New System.Drawing.Point(408, 87)
        Me.TextBox4.MaxLength = 32767
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextBox4.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TextBox4.SelectedText = ""
        Me.TextBox4.SelectionLength = 0
        Me.TextBox4.SelectionStart = 0
        Me.TextBox4.Size = New System.Drawing.Size(60, 20)
        Me.TextBox4.TabIndex = 9
        Me.TextBox4.UseSelectable = True
        Me.TextBox4.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TextBox4.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World)
        '
        'TextBox3
        '
        Me.TextBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.TextBox3.CustomButton.Image = Nothing
        Me.TextBox3.CustomButton.Location = New System.Drawing.Point(42, 2)
        Me.TextBox3.CustomButton.Name = ""
        Me.TextBox3.CustomButton.Size = New System.Drawing.Size(15, 15)
        Me.TextBox3.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.TextBox3.CustomButton.TabIndex = 1
        Me.TextBox3.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TextBox3.CustomButton.UseSelectable = True
        Me.TextBox3.CustomButton.Visible = False
        Me.TextBox3.Lines = New String(-1) {}
        Me.TextBox3.Location = New System.Drawing.Point(408, 53)
        Me.TextBox3.MaxLength = 32767
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextBox3.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TextBox3.SelectedText = ""
        Me.TextBox3.SelectionLength = 0
        Me.TextBox3.SelectionStart = 0
        Me.TextBox3.Size = New System.Drawing.Size(60, 20)
        Me.TextBox3.TabIndex = 8
        Me.TextBox3.UseSelectable = True
        Me.TextBox3.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TextBox3.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World)
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.FontSize = MetroFramework.MetroButtonSize.Medium
        Me.Button1.Location = New System.Drawing.Point(287, 298)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 38)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "OK"
        Me.Button1.UseSelectable = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.FontSize = MetroFramework.MetroButtonSize.Medium
        Me.Button2.Location = New System.Drawing.Point(409, 298)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 38)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Cancel"
        Me.Button2.UseSelectable = True
        '
        'Form2
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(554, 355)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(554, 355)
        Me.Name = "Form2"
        Me.Text = "Options"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents MetroLabel4 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel3 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel2 As MetroFramework.Controls.MetroLabel
    Friend WithEvents MetroLabel1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents TextBox4 As MetroFramework.Controls.MetroTextBox
    Friend WithEvents TextBox3 As MetroFramework.Controls.MetroTextBox
    Friend WithEvents TextBox2 As MetroFramework.Controls.MetroTextBox
    Friend WithEvents TextBox1 As MetroFramework.Controls.MetroTextBox
    Friend WithEvents Button1 As MetroFramework.Controls.MetroButton
    Friend WithEvents Button2 As MetroFramework.Controls.MetroButton
End Class
