using DVDLBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVDL.International_License
{
    public partial class NewInternationalLicense : Form
    {
        clsLicenses _Licenses = null;

        public NewInternationalLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =========================
        // Load License Info
        // =========================
        private void _LoadLicenseInfo()
        {
            if (_Licenses == null || _Licenses.LicenseID <= 0)
                return;

            lblIssueDate.Text = _Licenses.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _Licenses.ExpirationDate.ToShortDateString();
            lblLocalLicenseID.Text = _Licenses.LicenseID.ToString();
            lblFees.Text = _Licenses.PaidFees.ToString();
            lblCreatedByUser.Text = _Licenses.CreatedByUser.ToString();
        }

        // =========================
        // When License Selected
        // =========================
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(clsLicenses obj)
        {
            _Licenses = obj;

            if (_Licenses == null || _Licenses.LicenseID <= 0)
            {
                btnIssueLicense.Enabled = false;
                return;
            }

            _LoadLicenseInfo();

            // لازم تكون Active
            if (!_Licenses.IsActive)
            {
                MessageBox.Show("License is not active.",
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnIssueLicense.Enabled = false;
                return;
            }

            // check active international license
            int activeLicenseId =
                clsInternationalLicense.GetActiveInternationalLicenseID(_Licenses.DriverID);

            if (activeLicenseId != -1)
            {
                MessageBox.Show(
                    "This person already has an active International License ID = " + activeLicenseId,
                    "Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                btnIssueLicense.Enabled = false;
                return;
            }

            // إذا كل شيء OK
            btnIssueLicense.Enabled = true;
            crlShowLicense1.Enabled = false;
        }

        // =========================
        // Issue License Button
        // =========================
        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (_Licenses == null || _Licenses.LicenseID <= 0)
                return;

            if (!_Licenses.IsActive)
            {
                MessageBox.Show("License is not active.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // استدعاء الدالة (Business Layer)
            int internationalLicenseId =
                clsInternationalLicense.IssueInternationalLicense(
                    _Licenses.DriverID,
                    _Licenses.LicenseID,
                    clsGlopal.CurrentUser.UserID
                );

            // =========================
            // Result Handling
            // =========================
            if (internationalLicenseId == -1)
            {
                MessageBox.Show("License not found.");
                return;
            }
            else if (internationalLicenseId == -2)
            {
                MessageBox.Show("License must be Class 3.");
                return;
            }
            else if (internationalLicenseId == -3)
            {
                MessageBox.Show("Driver already has active International License.");
                return;
            }
            else if (internationalLicenseId == -4)
            {
                MessageBox.Show("Failed to create application.");
                return;
            }
            else if (internationalLicenseId == -5)
            {
                MessageBox.Show("Failed to issue International License.");
                return;
            }

            // =========================
            // Success
            // =========================
            lblInternationalLicenseID.Text = internationalLicenseId.ToString();

            crlShowLicense1.licenseID = _Licenses.LicenseID;
            crlShowLicense1.Enabled = true;

            ctrlDriverLicenseInfoWithFilter1.Enabled = false;

            MessageBox.Show("International License issued successfully!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}