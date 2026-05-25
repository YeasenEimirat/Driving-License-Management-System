using DVDL.Licenses.Controlls;
using DVDLBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVDL.Applications.Re_New_License_Applications
{
    public partial class RenewLicenseApplications : Form
    {
        clsLicenses _Licenses = new clsLicenses();

        public RenewLicenseApplications()
        {
            InitializeComponent();
        }

        private void RenewLicenseApplications_Load(object sender, EventArgs e)
        {
            btnRenewLicense.Enabled = false;
            driverLicenseInfo1.Enabled = true;
 
            driverLicenseInfo1.Enabled = false; // المفروض معطل في البداي
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(clsLicenses licenses)
        {
            _Licenses = licenses;
            _LoadLicenseInfo();
        }

        private void _LoadLicenseInfo()
        {
            clsApplications app = clsApplications.Find(_Licenses.ApplicationID);
            if (app == null) return;

            float licenseFees = clsApplicationTypes
                .FindByApplicationTypeID((int)clsApplications.enApplicationType.RenewDrivingLicense)
                .ApplicationFees;

            lblApplicationDate.Text = app.ApplicationDate.ToShortDateString();
            lblIssueDate.Text = _Licenses.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _Licenses.ExpirationDate.ToShortDateString();
            lblOldLicenseID.Text = _Licenses.LicenseID.ToString();
            lblApplicationFees.Text = app.PaidFees.ToString();
            lblLicenseFees.Text = licenseFees.ToString();
            lblTotalFees.Text = (licenseFees + app.PaidFees).ToString();
            lblCreatedByUser.Text = app.CreatedByUserID.ToString();

            // ✅ الزر يتفعل بس لو الرخصة منتهية
            btnRenewLicense.Enabled = (DateTime.Now > _Licenses.ExpirationDate);
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            clsApplications oldApp = clsApplications.Find(_Licenses.ApplicationID);

            if (oldApp == null)
            {
                MessageBox.Show("Original application not found.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // ✅ Application جديد بتواريخ جديدة
            clsApplications newApp = new clsApplications
            {
                ApplicantPersonID = oldApp.ApplicantPersonID,
                ApplicationTypeID = oldApp.ApplicationTypeID,
                ApplicationStatus = clsApplications.enApplicationStatus.Completed,
                ApplicationDate = DateTime.Now,
                LastStatusDate = DateTime.Now,
                PaidFees = oldApp.PaidFees,
                CreatedByUserID = oldApp.CreatedByUserID
            };

            if (!newApp.Save())
            {
                MessageBox.Show("Failed to create application.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // ✅ License جديد بتواريخ جديدة
            clsLicenses newLicense = new clsLicenses
            {
                ApplicationID = newApp.ApplicationID,
                DriverID = _Licenses.DriverID,
                LicenseClassID = _Licenses.LicenseClassID,
                IssueDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddYears(10),
                PaidFees = _Licenses.PaidFees,
                Notes = txtNotes.Text,
                IssueReason = true,
                IsActive = true,
                CreatedByUser = _Licenses.CreatedByUser
            };

            if (!newLicense.Save())
            {
                MessageBox.Show("Failed to create new license.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // ✅ تعطيل الرخصة القديمة وحفظها
            _Licenses.IsActive = false;
            if (!_Licenses.Save())
            {
                MessageBox.Show("Failed to deactivate old license.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // ✅ تحديث الـ UI
            lblApplicationID.Text = newApp.ApplicationID.ToString();
            lblRenewedLicenseID.Text = newLicense.LicenseID.ToString();

            showLicenseHistory1.Enabled = true;
            showLicenseHistory1.PersonID = newApp.ApplicantPersonID;

            driverLicenseInfo1.GetLicenseID(newLicense.LicenseID);

            btnRenewLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.Enabled = false;

            MessageBox.Show("License renewed successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
    }
}