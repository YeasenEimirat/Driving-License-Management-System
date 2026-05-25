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

namespace DVDL.Licenses.Detain_License
{
    public partial class frmDetainLicenseApplication : Form
    {
        clsLicenses _Licenses = new clsLicenses();

        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(DVDLBusinessLayer.clsLicenses obj)
        {
            _Licenses = obj;
            crlShowLicense1.Enabled = false;
            btnDetain.Enabled = true;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
           
            if (MessageBox.Show("Are you sure to Detain?",
                             "Confirm",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            if (clsDetainLicense.IsDetained(_Licenses.LicenseID))
            {
                 MessageBox.Show("This License is Detain Please Select other License .",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            return;
            }
           


            clsDetainLicense Detain = new clsDetainLicense();
            Detain.ReleaseApplicationID = null;

            Detain.CreatedByUserID = clsGlopal.CurrentUser.UserID;
 
            Detain.DetainDate= DateTime.Now;
            Detain.IsReleased = false;
            Detain.ReleaseDate = null;
            if (!decimal.TryParse(txtFineFees.Text, out decimal FineFees))
            {
                MessageBox.Show("Invalid Fine Fees");
                return;
            }

            Detain.FineFees = FineFees;
            Detain.LicenseID = _Licenses.LicenseID;
            if (!Detain.Save())
            {
                MessageBox.Show("Failed to create Detain.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            lblDetainID.Text = Detain.DetainID.ToString();
            lblCreatedByUser.Text = Detain.CreatedByUserID.ToString();
            lblDetainDate.Text = Detain.DetainDate.ToString();
            lblLicenseID.Text = Detain.LicenseID.ToString();
            clsApplications Application =
        clsApplications.Find(_Licenses.ApplicationID);

            if (Application != null)
            {
                showLicenseHistory1.PersonID = Application.ApplicantPersonID;
            }
            showLicenseHistory1.Enabled = true;
            crlShowLicense1.Enabled = true;
            crlShowLicense1.licenseID = _Licenses.LicenseID;
            btnDetain.Enabled = false;
        }
    }
}
