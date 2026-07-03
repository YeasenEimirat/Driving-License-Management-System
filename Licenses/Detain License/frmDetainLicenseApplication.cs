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
            // تحقق أولاً
            if (clsDetainLicense.IsDetained(_Licenses.LicenseID))
            {
                MessageBox.Show("This License is already detained!");
                btnDetain.Enabled = false;
                return;
            }

            // بعدين فعّل
            btnDetain.Enabled = true;
            txtFineFees.Focus(); // ← هذا السطر الصغير فرق كبير في UX
        }
        private void btnDetain_Click(object sender, EventArgs e)
        {
            // 1. تأكيد من المستخدم
            if (MessageBox.Show("Are you sure to Detain?",
                             "Confirm",
                             MessageBoxButtons.OKCancel,
                             MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            // 2. بناء الأوبجكت وتعبئة البيانات أولاً
            clsDetainLicense Detain = new clsDetainLicense();
            Detain.LicenseID = _Licenses.LicenseID;
            Detain.CreatedByUserID = clsGlopal.CurrentUser.UserID;
            Detain.DetainDate = DateTime.Now;
            Detain.IsReleased = false;
            Detain.ReleaseDate = null;
            Detain.ReleaseApplicationID = null;

            // 3. Validation على txtFineFees
            if (!decimal.TryParse(txtFineFees.Text, out decimal FineFees))
            {
                MessageBox.Show("Invalid Fine Fees");
                return;
            }
            Detain.FineFees = FineFees;

            // 4. التحقق من الحجز — آخر خطوة قبل الحفظ ✅
            if (clsDetainLicense.IsDetained(_Licenses.LicenseID))
            {
                MessageBox.Show("This License is already Detained, Please Select another one.",
                           "Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
                return;
            }

            // 5. الحفظ
            if (!Detain.Save())
            {
                MessageBox.Show("Failed to create Detain.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            // 6. تحديث الشاشة بعد النجاح
            lblDetainID.Text = Detain.DetainID.ToString();
            lblCreatedByUser.Text = Detain.CreatedByUserID.ToString();
            lblDetainDate.Text = Detain.DetainDate.ToString();
            lblLicenseID.Text = Detain.LicenseID.ToString();

            clsApplications Application = clsApplications.Find(_Licenses.ApplicationID);
            if (Application != null)
            {
                showLicenseHistory1.PersonID = Application.ApplicantPersonID;
            }

            // 7. تجميد الفورم بعد النجاح ✅
            btnDetain.Enabled = false;
            txtFineFees.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.Enabled = false;
            showLicenseHistory1.Enabled = true;
            crlShowLicense1.Enabled = true;          // ✅ اللي قلته
            crlShowLicense1.licenseID = _Licenses.LicenseID;
        }
    }
}
