namespace DVDL
{
    partial class ShowDetailsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowDetailsForm));
            this.btnClose = new System.Windows.Forms.Button();
            this.lblMode = new System.Windows.Forms.Label();
            this.personaLInfoUserControl11 = new DVDL.PersonaLInfoUserControl1();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(642, 469);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 39);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.Location = new System.Drawing.Point(247, 23);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(208, 36);
            this.lblMode.TabIndex = 24;
            this.lblMode.Text = "Person Detalis";
            // 
            // personaLInfoUserControl11
            // 
            this.personaLInfoUserControl11.Location = new System.Drawing.Point(2, 76);
            this.personaLInfoUserControl11.Name = "personaLInfoUserControl11";
            this.personaLInfoUserControl11.Size = new System.Drawing.Size(727, 386);
            this.personaLInfoUserControl11.TabIndex = 25;
            // 
            // ShowDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 511);
            this.Controls.Add(this.personaLInfoUserControl11);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.btnClose);
            this.Name = "ShowDetailsForm";
            this.Text = "ShowDetailsForm";
            this.Load += new System.EventHandler(this.ShowDetailsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblMode;
        private PersonaLInfoUserControl1 personaLInfoUserControl11;
    }
}