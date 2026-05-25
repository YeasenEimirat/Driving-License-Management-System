namespace DVDL.Users
{
    partial class FrmUserInfo
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
            this.loginInFo1 = new DVDL.UesrsControls.LoginInFo();
            this.SuspendLayout();
            // 
            // loginInFo1
            // 
            this.loginInFo1.Location = new System.Drawing.Point(1, 8);
            this.loginInFo1.Name = "loginInFo1";
            this.loginInFo1.Size = new System.Drawing.Size(715, 486);
            this.loginInFo1.TabIndex = 0;
            // 
            // FrmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 496);
            this.Controls.Add(this.loginInFo1);
            this.Name = "FrmUserInfo";
            this.Text = "FrmUserInfo";
            this.Load += new System.EventHandler(this.FrmUserInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UesrsControls.LoginInFo loginInFo1;
    }
}