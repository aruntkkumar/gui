<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form0
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form0))
        Me.TextBox1 = New MetroFramework.Controls.MetroTextBox()
        Me.button1 = New MetroFramework.Controls.MetroButton()
        Me.CheckBox1 = New MetroFramework.Controls.MetroCheckBox()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        '
        '
        '
        Me.TextBox1.CustomButton.Image = Nothing
        Me.TextBox1.CustomButton.Location = New System.Drawing.Point(289, 1)
        Me.TextBox1.CustomButton.Name = ""
        Me.TextBox1.CustomButton.Size = New System.Drawing.Size(29, 29)
        Me.TextBox1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.TextBox1.CustomButton.TabIndex = 1
        Me.TextBox1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.TextBox1.CustomButton.UseSelectable = True
        Me.TextBox1.CustomButton.Visible = False
        Me.TextBox1.DisplayIcon = True
        Me.TextBox1.Icon = CType(resources.GetObject("TextBox1.Icon"), System.Drawing.Image)
        Me.TextBox1.Lines = New String(-1) {}
        Me.TextBox1.Location = New System.Drawing.Point(23, 63)
        Me.TextBox1.MaxLength = 32767
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TextBox1.SelectedText = ""
        Me.TextBox1.SelectionLength = 0
        Me.TextBox1.SelectionStart = 0
        Me.TextBox1.Size = New System.Drawing.Size(319, 31)
        Me.TextBox1.TabIndex = 13
        Me.TextBox1.UseSelectable = True
        Me.TextBox1.WaterMark = "Password"
        Me.TextBox1.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.TextBox1.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.World)
        '
        'button1
        '
        Me.button1.DisplayFocus = True
        Me.button1.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.button1.Location = New System.Drawing.Point(123, 127)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(119, 38)
        Me.button1.TabIndex = 12
        Me.button1.Text = "Login"
        Me.button1.UseSelectable = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(23, 102)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(105, 15)
        Me.CheckBox1.TabIndex = 14
        Me.CheckBox1.Text = "Show Password"
        Me.CheckBox1.UseSelectable = True
        '
        'Form0
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(363, 187)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form0"
        Me.Resizable = False
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow
        Me.Text = "Master Password"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBox1 As MetroFramework.Controls.MetroTextBox
    Friend WithEvents button1 As MetroFramework.Controls.MetroButton
    Friend WithEvents CheckBox1 As MetroFramework.Controls.MetroCheckBox
End Class
