using DVDLBusinessLayer;
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
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        public event Action<clsLicenses> OnLicenseSelected;
        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtLicenseID.Text != "")
            {

                int LicenseID = Convert.ToInt32(txtLicenseID.Text);
                clsLicenses licenses = clsLicenses.Find(LicenseID);
            if (licenses!= null)
            {
                driverLicenseInfo1.GetLicenseID(LicenseID);
                OnLicenseSelected?.Invoke(licenses);
            }
            else
            {
                MessageBox.Show("Please Enter a Valid LicenseID  ");
            }

            }
            else
            {
                MessageBox.Show("Please Enter a Valid LicenseID  ");
                return; 
            }



        }
    }
}
