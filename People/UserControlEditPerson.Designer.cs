namespace DVDL
{
    partial class UserControlEditPerson
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
            this.linkLabelEditPorson = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // linkLabelEditPorson
            // 
            this.linkLabelEditPorson.AutoSize = true;
            this.linkLabelEditPorson.Location = new System.Drawing.Point(3, 0);
            this.linkLabelEditPorson.Name = "linkLabelEditPorson";
            this.linkLabelEditPorson.Size = new System.Drawing.Size(77, 17);
            this.linkLabelEditPorson.TabIndex = 1;
            this.linkLabelEditPorson.TabStop = true;
            this.linkLabelEditPorson.Text = "Edit Person";
            this.linkLabelEditPorson.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelEditPorson_LinkClicked);
            // 
            // UserControlٍُEditPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabelEditPorson);
            this.Name = "UserControlٍُEditPerson";
            this.Size = new System.Drawing.Size(90, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelEditPorson;
    }
}
