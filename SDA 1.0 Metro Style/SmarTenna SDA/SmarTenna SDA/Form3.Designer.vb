<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3))
        Me.textBox2 = New MetroFramework.Controls.MetroTextBox()
        Me.textBox1 = New MetroFramework.Controls.MetroTextBox()
        Me.button2 = New MetroFramework.Controls.MetroButton()
        Me.button1 = New MetroFramework.Controls.MetroButton()
        Me.SuspendLayout()
        '
        'textBox2
        '
        '
        '
        '
        Me.textBox2.CustomButton.Image = Nothing
        Me.textBox2.CustomButton.Location = New System.Drawing.Point(289, 1)
        Me.textBox2.CustomButton.Name = ""
        Me.textBox2.CustomButton.Size = New System.Drawing.Size(29, 29)
        Me.textBox2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.textBox2.CustomButton.TabIndex = 1
        Me.textBox2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.textBox2.CustomButton.UseSelectable = True
        Me.textBox2.CustomButton.Visible = False
        Me.textBox2.DisplayIcon = True
        Me.textBox2.Icon = CType(resources.GetObject("textBox2.Icon"), System.Drawing.Image)
        Me.textBox2.Lines = New String(-1) {}
        Me.textBox2.Location = New System.Drawing.Point(42, 102)
        Me.textBox2.MaxLength = 32767
        Me.textBox2.Name = "textBox2"
        Me.textBox2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.textBox2.SelectedText = ""
        Me.textBox2.SelectionLength = 0
        Me.textBox2.SelectionStart = 0
        Me.textBox2.Size = New System.Drawing.Size(319, 31)
        Me.textBox2.TabIndex = 15
        Me.textBox2.UseSelectable = True
        Me.textBox2.WaterMark = "Password"
        Me.textBox2.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.textBox2.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World)
        '
        'textBox1
        '
        '
        '
        '
        Me.textBox1.CustomButton.Image = Nothing
        Me.textBox1.CustomButton.Location = New System.Drawing.Point(289, 1)
        Me.textBox1.CustomButton.Name = ""
        Me.textBox1.CustomButton.Size = New System.Drawing.Size(29, 29)
        Me.textBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.textBox1.CustomButton.TabIndex = 1
        Me.textBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.textBox1.CustomButton.UseSelectable = True
        Me.textBox1.CustomButton.Visible = False
        Me.textBox1.DisplayIcon = True
        Me.textBox1.Icon = CType(resources.GetObject("textBox1.Icon"), System.Drawing.Image)
        Me.textBox1.Lines = New String(-1) {}
        Me.textBox1.Location = New System.Drawing.Point(42, 65)
        Me.textBox1.MaxLength = 32767
        Me.textBox1.Name = "textBox1"
        Me.textBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.textBox1.SelectedText = ""
        Me.textBox1.SelectionLength = 0
        Me.textBox1.SelectionStart = 0
        Me.textBox1.Size = New System.Drawing.Size(319, 31)
        Me.textBox1.TabIndex = 14
        Me.textBox1.UseSelectable = True
        Me.textBox1.WaterMark = "Username"
        Me.textBox1.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.textBox1.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World)
        '
        'button2
        '
        Me.button2.DisplayFocus = True
        Me.button2.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.button2.Location = New System.Drawing.Point(242, 144)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(119, 38)
        Me.button2.TabIndex = 13
        Me.button2.Text = "Back"
        Me.button2.UseSelectable = True
        '
        'button1
        '
        Me.button1.DisplayFocus = True
        Me.button1.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.button1.Location = New System.Drawing.Point(42, 144)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(119, 38)
        Me.button1.TabIndex = 12
        Me.button1.Text = "Login"
        Me.button1.UseSelectable = True
        '
        'Form3
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(406, 205)
        Me.Controls.Add(Me.textBox2)
        Me.Controls.Add(Me.textBox1)
        Me.Controls.Add(Me.button2)
        Me.Controls.Add(Me.button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form3"
        Me.Resizable = False
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow
        Me.Text = "Engineering Login Form"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents textBox2 As MetroFramework.Controls.MetroTextBox
    Friend WithEvents textBox1 As MetroFramework.Controls.MetroTextBox
    Friend WithEvents button2 As MetroFramework.Controls.MetroButton
    Friend WithEvents button1 As MetroFramework.Controls.MetroButton
End Class
