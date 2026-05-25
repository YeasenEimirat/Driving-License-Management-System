using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVDL.Licenses
{
    public partial class ShowLicenseDetailes : Form
    {
        int _licenseID = 0; 
        public ShowLicenseDetailes(int licenseID)
        {
            InitializeComponent();
            _licenseID = licenseID;
        }

        private void driverLicenseInfo1_Load(object sender, EventArgs e)
        {
            driverLicenseInfo1.GetLicenseID(_licenseID);
        }
    }
}
