using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVDL.Licenses.Controlls
{
    public partial class crlShowLicense : UserControl
    {
        public crlShowLicense()
        {
            InitializeComponent();
        }
        public int licenseID { set; get;  }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ShowLicenseDetailes(licenseID);
            frm.ShowDialog();
        }
    }
}
