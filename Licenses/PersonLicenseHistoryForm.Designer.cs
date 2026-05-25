namespace DVDL.Drivers
{
    partial class PersonLicenseHistoryForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonLicenseHistoryForm));
            this.personaLInfoUserControl11 = new DVDL.PersonaLInfoUserControl1();
            this.userControlFltier1 = new DVDL.UesrsControls.UserControlFltier();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ctrlDriverLicenses1 = new DVDL.Licenses.Controlls.ctrlDriverLicenses();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // personaLInfoUserControl11
            // 
            this.personaLInfoUserControl11.Location = new System.Drawing.Point(326, 65);
            this.personaLInfoUserControl11.Name = "personaLInfoUserControl11";
            this.personaLInfoUserControl11.Size = new System.Drawing.Size(727, 334);
            this.personaLInfoUserControl11.TabIndex = 1;
            // 
            // userControlFltier1
            // 
            this.userControlFltier1.Location = new System.Drawing.Point(326, 2);
            this.userControlFltier1.Name = "userControlFltier1";
            this.userControlFltier1.Size = new System.Drawing.Size(676, 57);
            this.userControlFltier1.TabIndex = 0;
            this.userControlFltier1.OnPersonSelected += new System.Action<DVDLBusinessLayer.clsPeople>(this.userControlFltier1_OnPersonSelected);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(44, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(196, 209);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // ctrlDriverLicenses1
            // 
            this.ctrlDriverLicenses1.Location = new System.Drawing.Point(-4, 392);
            this.ctrlDriverLicenses1.Name = "ctrlDriverLicenses1";
            this.ctrlDriverLicenses1.Size = new System.Drawing.Size(1057, 336);
            this.ctrlDriverLicenses1.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.AutoEllipsis = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(918, 737);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(135, 36);
            this.btnClose.TabIndex = 128;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PersonLicenseHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 785);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlDriverLicenses1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.personaLInfoUserControl11);
            this.Controls.Add(this.userControlFltier1);
            this.Name = "PersonLicenseHistoryForm";
            this.Text = "PersonLicenseHistoryForm";
            this.Load += new System.EventHandler(this.PersonLicenseHistoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UesrsControls.UserControlFltier userControlFltier1;
        private PersonaLInfoUserControl1 personaLInfoUserControl11;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Licenses.Controlls.ctrlDriverLicenses ctrlDriverLicenses1;
        private System.Windows.Forms.Button btnClose;
    }
}