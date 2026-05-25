namespace DVDL.People
{
    partial class PersonCardControl
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
            this.userControlFltier1 = new DVDL.UesrsControls.UserControlFltier();
            this.personaLInfoUserControl11 = new DVDL.PersonaLInfoUserControl1();
            this.SuspendLayout();
            // 
            // userControlFltier1
            // 
            this.userControlFltier1.Location = new System.Drawing.Point(3, 3);
            this.userControlFltier1.Name = "userControlFltier1";
            this.userControlFltier1.Size = new System.Drawing.Size(658, 57);
            this.userControlFltier1.TabIndex = 0;
            this.userControlFltier1.OnPersonSelected += new System.Action<DVDLBusinessLayer.clsPeople>(this.userControlFltier1_OnPersonSelected);
            // 
            // personaLInfoUserControl11
            // 
            this.personaLInfoUserControl11.Location = new System.Drawing.Point(3, 56);
            this.personaLInfoUserControl11.Name = "personaLInfoUserControl11";
            this.personaLInfoUserControl11.Size = new System.Drawing.Size(675, 423);
            this.personaLInfoUserControl11.TabIndex = 1;
            // 
            // PersonCardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.personaLInfoUserControl11);
            this.Controls.Add(this.userControlFltier1);
            this.Name = "PersonCardControl";
            this.Size = new System.Drawing.Size(772, 519);
            this.Load += new System.EventHandler(this.PersonCardControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UesrsControls.UserControlFltier userControlFltier1;
        private PersonaLInfoUserControl1 personaLInfoUserControl11;
    }
}
