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

namespace DVDL.Applications.Relase_Detain_Licenses_Application
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        clsLicenses _Licenses = new clsLicenses();

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }
        private void _LoadLicenseInfo()
        {
            if (_Licenses == null || _Licenses.LicenseID <= 0)
            {

                return;
            }

            clsApplications app = clsApplications.Find(_Licenses.ApplicationID);

            if (app == null)
                return;
            clsDetainLicense Detain = clsDetainLicense.FindByLicenseID(_Licenses.LicenseID);
            if (Detain == null)
                return;
            float ApplicationFees = clsApplicationTypes
                .FindByApplicationTypeID(
                (int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense)
                .ApplicationFees;

            float fees = clsApplicationTypes
                .FindByApplicationTypeID(
                (int)clsApplications.enApplicationType.ReplaceLostDrivingLicense)
                .ApplicationFees;

            lblTotalFees.Text = (fees + ApplicationFees).ToString();
            lblApplicationFees.Text = fees.ToString();
            lblFineFees.Text = Detain.FineFees.ToString();
            lblDetainDate.Text = Detain.DetainDate.ToString();
            lblDetainID.Text = Detain.DetainID.ToString();
            lblLicenseID.Text = Detain.LicenseID.ToString();
            lblCreatedByUser.Text = Detain.CreatedByUserID.ToString();


            btnRelease.Enabled = clsDetainLicense.IsDetained(_Licenses.LicenseID);
        }

        private void ctrlDriverLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(DVDLBusinessLayer.clsLicenses obj)
        {
            _Licenses = obj;
            crlShowLicense1.Enabled = false;
            showLicenseHistory1.Enabled = false;
            btnRelease.Enabled = true;
            _LoadLicenseInfo();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to Release. ?",
                             "Confirm",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }
            if (!clsDetainLicense.IsDetained(_Licenses.LicenseID))
            {
                MessageBox.Show("the Selected License is Not Detain You cant Release it .",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            clsApplications oldApplication =
                clsApplications.Find(_Licenses.ApplicationID);

            if (oldApplication == null)
            {
                MessageBox.Show("Original application not found.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // ✅ local variable مش field
            clsApplications newApplication = new clsApplications();
            float ApplicationFees = clsApplicationTypes
               .FindByApplicationTypeID(
               (int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense)
               .ApplicationFees;
            newApplication.ApplicantPersonID = oldApplication.ApplicantPersonID;
            newApplication.ApplicationDate = DateTime.Now;
            newApplication.ApplicationTypeID =  ((int)clsApplications.enApplicationType.ReleaseDetainedDrivingLicsense) ;
            newApplication.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            newApplication.LastStatusDate = DateTime.Now;
            newApplication.PaidFees = ApplicationFees;
            newApplication.CreatedByUserID = oldApplication.CreatedByUserID;

            if (!newApplication.Save())
            {
                MessageBox.Show("Failed to create application.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            lblApplicationID.Text = newApplication.ApplicationID.ToString();
            clsDetainLicense Detain = clsDetainLicense.FindByLicenseID(_Licenses.LicenseID);
            if (Detain == null)
            {
                return;
            }
            Detain.ReleaseDate = DateTime.Now;
            Detain.IsReleased = true;
            Detain.ReleaseApplicationID = newApplication.ApplicationID;
            if (!Detain.Save())
            {
                MessageBox.Show("Failed to release Detain License.",
                       "Error",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
            }
            showLicenseHistory1.Enabled = true;
            crlShowLicense1.Enabled = true;
            crlShowLicense1.licenseID = _Licenses.LicenseID;
            btnRelease.Enabled = false;
            showLicenseHistory1.PersonID = newApplication.ApplicantPersonID;
            showLicenseHistory1.Enabled = true;
        }
    }
}
