using DVDL.Licenses;
using DVDLBusinessLayer;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVDL.Applications.Replacement_for_Damaged_or_Lost_Licenses
{
    public partial class ReplacementForDamagedOrLostLicenses : Form
    {
        clsLicenses _Licenses = new clsLicenses();

        public ReplacementForDamagedOrLostLicenses()
        {
            InitializeComponent();
        }

        private void _SetIssueButtonState()
        {
            btnIssueReplacement.Enabled =
                _Licenses != null &&
                _Licenses.LicenseID > 0 &&
                _Licenses.IsActive &&
                (rbDamagedLicense.Checked || rbLostLicense.Checked);
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

            float damagedFees = clsApplicationTypes
                .FindByApplicationTypeID(
                (int)clsApplications.enApplicationType.ReplaceDamagedDrivingLicense)
                .ApplicationFees;

            float lostFees = clsApplicationTypes
                .FindByApplicationTypeID(
                (int)clsApplications.enApplicationType.ReplaceLostDrivingLicense)
                .ApplicationFees;

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();

            lblApplicationFees.Text = rbDamagedLicense.Checked
                ? damagedFees.ToString()
                : lostFees.ToString();

            lblCreatedByUser.Text = app.CreatedByUserID.ToString();

            lblOldLicenseID.Text = _Licenses.LicenseID.ToString();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (_Licenses == null || _Licenses.LicenseID <= 0)
            {
                MessageBox.Show("Please select a license first.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            if (!_Licenses.IsActive)
            {
                MessageBox.Show("Selected license is not active.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure to issue License Replacement?",
                                "Confirm",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            bool isDamaged = rbDamagedLicense.Checked;

            int newApplicationTypeID = isDamaged
                ? (int)clsApplications.enApplicationType.ReplaceDamagedDrivingLicense
                : (int)clsApplications.enApplicationType.ReplaceLostDrivingLicense;

            float newFees = clsApplicationTypes
                .FindByApplicationTypeID(newApplicationTypeID)
                .ApplicationFees;

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

            newApplication.ApplicantPersonID = oldApplication.ApplicantPersonID;
            newApplication.ApplicationDate = DateTime.Now;
            newApplication.ApplicationTypeID = newApplicationTypeID;
            newApplication.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            newApplication.LastStatusDate = DateTime.Now;
            newApplication.PaidFees = newFees;
            newApplication.CreatedByUserID = oldApplication.CreatedByUserID;

            if (!newApplication.Save())
            {
                MessageBox.Show("Failed to create application.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            clsLicenses newLicense = new clsLicenses();

            newLicense.ApplicationID = newApplication.ApplicationID;
            newLicense.DriverID = _Licenses.DriverID;
            newLicense.LicenseClassID = _Licenses.LicenseClassID;
            newLicense.IssueDate = DateTime.Now;
            newLicense.ExpirationDate = _Licenses.ExpirationDate;
            newLicense.Notes = _Licenses.Notes;
            newLicense.IssueReason = isDamaged;
            newLicense.PaidFees = newFees;
            newLicense.IsActive = true;
            newLicense.CreatedByUser = _Licenses.CreatedByUser;

            if (!newLicense.Save())
            {
                MessageBox.Show("Failed to create replacement license.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            _Licenses.IsActive = false;

            if (!_Licenses.Save())
            {
                MessageBox.Show("Failed to deactivate old license.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = newApplication.ApplicationID.ToString();
            lblRreplacedLicenseID.Text = newLicense.LicenseID.ToString();

            crlShowLicense1.licenseID = newLicense.LicenseID;

            btnIssueReplacement.Enabled = false;
            gbReplacementFor.Enabled = false;
            crlShowLicense1.Enabled = true; 
            ctrlDriverLicenseInfoWithFilter1.Enabled = false;
            showLicenseHistory1.Enabled = true;
            showLicenseHistory1.PersonID = newApplication.ApplicantPersonID;
            MessageBox.Show("License Replacement issued successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _LoadLicenseInfo();
            _SetIssueButtonState(); // ✅
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            _LoadLicenseInfo();
            _SetIssueButtonState(); // ✅
        }

        private void ReplacementForDamagedOrLostLicenses_Load(object sender, EventArgs e)
        {
            btnIssueReplacement.Enabled = false; // ✅ معطل من البداية
            crlShowLicense1.Enabled = false;
            
        }

        private void ctrlDriverLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {
            btnIssueReplacement.Enabled = false; // ✅ معطل من البداية
        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected_1(clsLicenses obj)
        {
            _Licenses = obj;
            _LoadLicenseInfo();
            _SetIssueButtonState(); // ✅
        }

        private void crlShowLicense1_Load(object sender, EventArgs e)
        {

        }
    }
}