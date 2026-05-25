using DVDL.Drivers;
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
    public partial class ShowLicenseHistory : UserControl
    {
        public ShowLicenseHistory()
        {
            InitializeComponent();
            llShowLicenseHistory.Enabled = false;
        }
        // في ShowLicenseHistory UserControl
        private int _personID;
        public int PersonID
        {
            get { return _personID; }
            set
            {
                _personID = value;
                llShowLicenseHistory.Enabled = (_personID > 0); // ✅ تحكم مباشر
            }
        }
        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(PersonID <= 0) return;
            Form frm = new PersonLicenseHistoryForm(PersonID);
            frm.ShowDialog();
        }
    }
}
