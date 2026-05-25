using DVDL.Drivers;
using DVDL.Licenses;
using DVDL.TestAppointments;
using DVDLBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVDL.LocalDrivingLicense
{
    public partial class MangeLocalDrivingLicenseForm : Form
    {
        public MangeLocalDrivingLicenseForm()
        {
            InitializeComponent();
        }
        private DataTable _dtAllLocal = clsLocalDrivingLicenseApplications.ListOfLocalDrivingLicenseApplications();

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicesnseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
 
            clsLocalDrivingLicenseApplications localDrivingLicenseApplications = clsLocalDrivingLicenseApplications.Find(LocalDrivingLicesnseApplicationID);
            int TotalPassedTests = Convert.ToInt32(
                dgvLocalDrivingLicenseApplications.CurrentRow.Cells["PassedTestCount"].Value
            ); editToolStripMenuItem.Enabled =    (localDrivingLicenseApplications.ApplicationStatus == clsApplications.enApplicationStatus.New);
            CancelApplicaitonToolStripMenuItem.Enabled = (localDrivingLicenseApplications.ApplicationStatus == clsApplications.enApplicationStatus.New);
            DeleteApplicationToolStripMenuItem.Enabled =
              (localDrivingLicenseApplications.ApplicationStatus == clsApplications.enApplicationStatus.New);
            bool PassedVisionTest = localDrivingLicenseApplications.DoesPassTestType(clsTestType.enTestType.VisionTest); ;
            bool PassedWrittenTest = localDrivingLicenseApplications.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = localDrivingLicenseApplications.DoesPassTestType(clsTestType.enTestType.StreetTest);
            ScheduleTestsMenue.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (localDrivingLicenseApplications.ApplicationStatus == clsApplications.enApplicationStatus.New);
            if (ScheduleTestsMenue.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;
                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }
            
            else
            {
                scheduleVisionTestToolStripMenuItem.Enabled = false;
                scheduleWrittenTestToolStripMenuItem.Enabled = false;
                scheduleStreetTestToolStripMenuItem.Enabled = false;
            }
        
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
            MangeLocalDrivingLicenseForm_Load(null, null);

        }

        private void MangeLocalDrivingLicenseForm_Load(object sender, EventArgs e)
        {
            _dtAllLocal = clsLocalDrivingLicenseApplications.ListOfLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocal;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
 
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;


                case "Status":
                    FilterColumn = "ApplicationStatus";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocal.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with numbers not string.
                _dtAllLocal.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllLocal.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateLocalDrivingLicesnseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            MangeLocalDrivingLicenseForm_Load(null, null);
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplications.DeleteLocalDrivingLicense((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsApplications Application = clsApplications.Find(clsLocalDrivingLicenseApplications.Find((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value).ApplicationID);
            Application.Cancel();
            MangeLocalDrivingLicenseForm_Load(null, null);

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new Driving_License_Application_Info((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new TestAppointmentsFrm((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value  , clsTestType.enTestType.VisionTest);
            frm.ShowDialog();
            MangeLocalDrivingLicenseForm_Load(null, null);

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new TestAppointmentsFrm((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.WrittenTest);
            frm.ShowDialog();
            MangeLocalDrivingLicenseForm_Load(null, null);

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new TestAppointmentsFrm((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value, clsTestType.enTestType.StreetTest);
            frm.ShowDialog();
            MangeLocalDrivingLicenseForm_Load(null, null);

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmIssueDriverLicenseFirstTime((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            MangeLocalDrivingLicenseForm_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new PersonLicenseHistoryForm(clsLocalDrivingLicenseApplications.Find( (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value).ApplicantPersonID);
            frm.ShowDialog();
            MangeLocalDrivingLicenseForm_Load(null, null);
        }
    }
}
