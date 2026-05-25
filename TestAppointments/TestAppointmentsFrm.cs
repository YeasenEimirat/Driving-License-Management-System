using DVDL.Properties;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVDL.TestAppointments
{
    public partial class TestAppointmentsFrm : Form
    {
      private  int _LocalDrivingLicesnseApplicationID = -1;
        clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;
        private DataTable _dtAllTestAppointment ;

        clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications = new clsLocalDrivingLicenseApplications();
        public TestAppointmentsFrm(int LocalDrivingLicesnseApplicationID , clsTestType.enTestType TestType )
        {
            _LocalDrivingLicesnseApplicationID = LocalDrivingLicesnseApplicationID;
            _TestType = TestType; 
            InitializeComponent();
        }

        private void lblTestTitel_Click(object sender, EventArgs e)
        {

        }
      private  void LodeHader(clsTestType.enTestType TestType)
        {
            switch (TestType)
            {
                case clsTestType.enTestType.VisionTest:
                    lblTestTitel.Text = "Vision Test Appointment";
                    pictureBox4.Image= Resources.Vision_512;
                    this.Text = "Vision Test Appointment";
                    break;
                case clsTestType.enTestType.WrittenTest:
                    lblTestTitel.Text = "Written Test Appointment";
                    pictureBox4.Image = Resources.Written_Test_512;
                    this.Text = "Written Test Appointment";
                    break;
                case clsTestType.enTestType.StreetTest:
                    lblTestTitel.Text = "Street Test Appointment";
                    pictureBox4.Image = Resources.Street_Test_32;
                    this.Text = "Street Test Appointment";
                    break;
            }

        }
        private void LodeDvgTest()
        {
         _dtAllTestAppointment = clsTestAppointments.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicesnseApplicationID, _TestType);
            dvgTest.DataSource = _dtAllTestAppointment;

            if (dvgTest.Rows.Count > 0)
            {
                dvgTest.Columns[0].HeaderText = "Appointment ID";
                dvgTest.Columns[0].Width = 150;

               dvgTest.Columns[1].HeaderText = "Appointment Date";
               dvgTest.Columns[1].Width = 200;
                dvgTest.Columns[2].HeaderText = "Paid Fees";
               dvgTest.Columns[2].Width = 150;
                dvgTest.Columns[3].HeaderText = "Is Locked";
                dvgTest.Columns[3].Width = 100;
            }

             lblCountRecords.Text = dvgTest.Rows.Count.ToString();
        }
        private void TestAppointmentsFrm_Load(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.Find(_LocalDrivingLicesnseApplicationID);
            if (LocalDrivingLicenseApplications == null)
            {
                MessageBox.Show("Application not found");
                this.Close();
                return;
            }
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicesnseApplicationID.ToString();
            lblAppliedFor.Text = clsLicenseClass.Find(LocalDrivingLicenseApplications.LicenseClassID).ClassName;
            clsApplications app = clsApplications.Find(LocalDrivingLicenseApplications.ApplicationID);
            LodeHader(_TestType);
            int passed = 0;

            if (LocalDrivingLicenseApplications.DoesPassTestType(clsTestType.enTestType.VisionTest))
                passed++;

            if (LocalDrivingLicenseApplications.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                passed++;

            if (LocalDrivingLicenseApplications.DoesPassTestType(clsTestType.enTestType.StreetTest))
                passed++;

            lblPassedTests.Text = $"{passed}/3";
            applicationBasicInfoControl1.LoadApplicationData(clsLocalDrivingLicenseApplications.Find(_LocalDrivingLicesnseApplicationID).ApplicationID);
            LodeDvgTest();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new ScheduleTest(_LocalDrivingLicesnseApplicationID, _TestType);
            frm.ShowDialog();
            TestAppointmentsFrm_Load(null , null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editTestTypesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void tackTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new TakeTestFrm((int)dvgTest.CurrentRow.Cells[0].Value, _TestType);
            frm.ShowDialog();
            TestAppointmentsFrm_Load(null, null);
        }
    }
}
