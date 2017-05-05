namespace MetroFramework.Controls
{
    partial class MetroDateTimeControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtDate = new MetroFramework.Controls.MetroTextBox();
            this.btnCalendar = new MetroFramework.Controls.MetroFormButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtDate
            // 
            this.txtDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDate.CustomButton.Image = null;
            this.txtDate.CustomButton.Location = new System.Drawing.Point(92, 1);
            this.txtDate.CustomButton.Name = "";
            this.txtDate.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtDate.CustomButton.Style = MetroFramework.MetroColorStyle.Custom;
            this.txtDate.CustomButton.TabIndex = 1;
            this.txtDate.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDate.CustomButton.UseSelectable = true;
            this.txtDate.CustomButton.Visible = false;
            this.txtDate.Lines = new string[0];
            this.txtDate.Location = new System.Drawing.Point(0, 0);
            this.txtDate.MaxLength = 32767;
            this.txtDate.Name = "txtDate";
            this.txtDate.PasswordChar = '\0';
            this.txtDate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDate.SelectedText = "";
            this.txtDate.SelectionLength = 0;
            this.txtDate.SelectionStart = 0;
            this.txtDate.ShortcutsEnabled = true;
            this.txtDate.Size = new System.Drawing.Size(116, 25);
            this.txtDate.TabIndex = 0;
            this.txtDate.UseSelectable = true;
            this.txtDate.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDate.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnCalendar
            // 
            this.btnCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalendar.Location = new System.Drawing.Point(115, 0);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.Size = new System.Drawing.Size(25, 25);
            this.btnCalendar.TabIndex = 13;
            this.btnCalendar.TabStop = false;
            this.btnCalendar.Tag = "";
            this.btnCalendar.Text = "6";
            this.btnCalendar.UseSelectable = true;
            // 
            // MetroDateTimeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCalendar);
            this.Controls.Add(this.txtDate);
            this.Name = "MetroDateTimeControl";
            this.Size = new System.Drawing.Size(140, 25);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private MetroFormButton btnCalendar;
        private MetroTextBox txtDate;


    }
}