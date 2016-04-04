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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.button1 = New MetroFramework.Controls.MetroButton()
        Me.button2 = New MetroFramework.Controls.MetroButton()
        Me.button3 = New MetroFramework.Controls.MetroButton()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.label3 = New System.Windows.Forms.Label()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'button1
        '
        Me.button1.DisplayFocus = True
        Me.button1.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.button1.Location = New System.Drawing.Point(74, 193)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(119, 38)
        Me.button1.TabIndex = 0
        Me.button1.Text = "Demo"
        Me.button1.UseSelectable = True
        '
        'button2
        '
        Me.button2.DisplayFocus = True
        Me.button2.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.button2.Location = New System.Drawing.Point(267, 193)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(119, 38)
        Me.button2.TabIndex = 1
        Me.button2.Text = "System"
        Me.button2.UseSelectable = True
        '
        'button3
        '
        Me.button3.DisplayFocus = True
        Me.button3.FontWeight = MetroFramework.MetroButtonWeight.Regular
        Me.button3.Location = New System.Drawing.Point(460, 193)
        Me.button3.Name = "button3"
        Me.button3.Size = New System.Drawing.Size(119, 38)
        Me.button3.TabIndex = 2
        Me.button3.Text = "Engineering"
        Me.button3.UseSelectable = True
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Font = New System.Drawing.Font("Calibri", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.label2.Location = New System.Drawing.Point(382, 71)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(49, 59)
        Me.label2.TabIndex = 14
        Me.label2.Text = "®"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Calibri", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.label1.Location = New System.Drawing.Point(84, 63)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(468, 78)
        Me.label1.TabIndex = 13
        Me.label1.Text = "SmarTenna   SDA"
        '
        'pictureBox1
        '
        Me.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
        Me.pictureBox1.Location = New System.Drawing.Point(451, 301)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(176, 101)
        Me.pictureBox1.TabIndex = 15
        Me.pictureBox1.TabStop = False
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(37, Byte), Integer), CType(CType(84, Byte), Integer))
        Me.label3.Location = New System.Drawing.Point(23, 390)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(46, 15)
        Me.label3.TabIndex = 16
        Me.label3.Text = "Ver. 1.0"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackMaxSize = 30
        Me.ClientSize = New System.Drawing.Size(650, 425)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.button3)
        Me.Controls.Add(Me.button2)
        Me.Controls.Add(Me.button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Resizable = False
        Me.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents button1 As MetroFramework.Controls.MetroButton
    Friend WithEvents button2 As MetroFramework.Controls.MetroButton
    Friend WithEvents button3 As MetroFramework.Controls.MetroButton
    Private WithEvents label2 As Label
    Private WithEvents label1 As Label
    Private WithEvents pictureBox1 As PictureBox
    Private WithEvents label3 As Label
End Class
