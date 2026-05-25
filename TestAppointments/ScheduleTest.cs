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

namespace DVDL.TestAppointments
{
    public partial class ScheduleTest : Form
    {
        private int _LocalDrivingLicesnseApplicationID = -1;
        clsLocalDrivingLicenseApplications _LocalDrivingLicesnseApplication = new clsLocalDrivingLicenseApplications(); clsTestAppointments testAppointments = new clsTestAppointments();
        clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;
        public ScheduleTest(int LocalDrivingLicesnseApplicationID, clsTestType.enTestType TestType)
        {
            _LocalDrivingLicesnseApplicationID = LocalDrivingLicesnseApplicationID;
            _TestType = TestType;
            InitializeComponent();
        }
        private void LodeHader(clsTestType.enTestType TestType)
        {
            switch (TestType)
            {
                case clsTestType.enTestType.VisionTest:
                    groupBox1.Text = "Vision Test  ";
                    pictureBox2.Image = Resources.Vision_512;
                     break;
                case clsTestType.enTestType.WrittenTest:
                    groupBox1.Text = "Written Test  ";
                    pictureBox2.Image = Resources.Written_Test_512;
                     break;
                case clsTestType.enTestType.StreetTest:
                    groupBox1.Text = "Street Test";
                    pictureBox2.Image = Resources.Street_Test_32;
                     break;
            }

        }
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ScheduleTest_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicesnseApplication = clsLocalDrivingLicenseApplications.Find(_LocalDrivingLicesnseApplicationID);
            if (_LocalDrivingLicesnseApplication == null)
            {
                return;
            }
            DLAppID.Text = _LocalDrivingLicesnseApplicationID.ToString();
            DClass.Text = _LocalDrivingLicesnseApplication.LicenseClass.ClassName;
            lblName.Text = _LocalDrivingLicesnseApplication.PersonInfo.FullName;
            lblTrial.Text = clsLocalDrivingLicenseApplications.Trial(_LocalDrivingLicesnseApplicationID).ToString();
            lblFees.Text =  _LocalDrivingLicesnseApplication.PaidFees.ToString();
            
            LodeHader(_TestType);
        }
        private void _HandleRetakeApplication()
        {
            //this will decide to create a seperate application for retake test or not.
            // and will create it if needed , then it will linkit to the appoinment.
           
                //then we linke it with the appointment.

                //First Create Applicaiton 
                clsApplications Application = new clsApplications();

                Application.ApplicantPersonID = _LocalDrivingLicesnseApplication.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)clsApplications.enApplicationType.RetakeTest;
                Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationTypes.FindByApplicationTypeID((int)clsApplications.enApplicationType.RetakeTest).ApplicationFees;
                Application.CreatedByUserID = clsGlopal.CurrentUser.UserID;

                if (!Application.Save())
                {
                   testAppointments .RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
                 }

            testAppointments.RetakeTestApplicationID = Application.ApplicationID;

           
         }
        private void button_Save_Click(object sender, EventArgs e)
        {
            _LocalDrivingLicesnseApplication = clsLocalDrivingLicenseApplications.Find(_LocalDrivingLicesnseApplicationID);
            if (_LocalDrivingLicesnseApplication == null)
            {
                return;
            }
            _HandleRetakeApplication();
            testAppointments.PaidFees = Convert.ToSingle(lblFees.Text);
            testAppointments.AppointmentDate = dateTimePicker1.Value;
             testAppointments.CreatedByUserID = clsGlopal.CurrentUser.UserID;
             testAppointments.IsLocked = false;
            testAppointments.TestTypeID =(int) _TestType;
            testAppointments.LocalDrivingLicenseApplicationID = _LocalDrivingLicesnseApplicationID;
            if (testAppointments.Save())
            {
                MessageBox.Show("Data Saved Successfully ✅");
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Error: Data Is Not Saved ❌");

            }
        }
    }
}
