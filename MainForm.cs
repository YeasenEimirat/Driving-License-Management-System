using DVDL.Applications.Re_New_License_Applications;
using DVDL.Applications.Relase_Detain_Licenses_Application;
using DVDL.Applications.Replacement_for_Damaged_or_Lost_Licenses;
using DVDL.ApplicationType;
using DVDL.Drivers;
using DVDL.International_License;
using DVDL.Licenses.Detain_License;
using DVDL.LocalDrivingLicense;
using DVDL.TestTypes;
using DVDL.Users;
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

namespace DVDL
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        frmLogin _frmLogin; 
        public MainForm(frmLogin frm)
        {
            InitializeComponent();
            _frmLogin = frm;
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

      
        
        private void useresToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
 Form frm = new ManageUsersForm();
            frm.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
Form frm = new ManagePepole();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUserInfo frm = new FrmUserInfo(clsGlopal.CurrentUser);
            frm.ShowDialog();
        }

        private void singInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            _frmLogin.ShowDialog();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ManagePepole();
            frm.ShowDialog();
        }

        private void accountSeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ChangePassword(clsGlopal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void mangeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mangeeApplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Mange_Application_Types();
            frm.ShowDialog();
        }

        private void mangeTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ListTestType();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form frm = new MangeLocalDrivingLicenseForm();
            frm.ShowDialog();
        }

        private void reNewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form frm = new RenewLicenseApplications();
            frm.ShowDialog();
        }

        private void replacmentForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ReplacementForDamagedOrLostLicenses();
            frm.ShowDialog();
        }

        private void driverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new FrmListDrivers();
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }
    }
}
