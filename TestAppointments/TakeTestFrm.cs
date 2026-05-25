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
    public partial class TakeTestFrm : Form
    {
        private int _TestAppointmentID = -1;
        clsLocalDrivingLicenseApplications _LocalDrivingLicesnseApplication = new clsLocalDrivingLicenseApplications();

        clsTestAppointments TestAppointments = new clsTestAppointments();
         clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;
        public TakeTestFrm(int TestAppointmentID ,  clsTestType.enTestType TestType)
        {
            _TestAppointmentID = TestAppointmentID;
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TakeTestFrm_Load(object sender, EventArgs e)
        {
            TestAppointments = clsTestAppointments.Find(_TestAppointmentID);
            _LocalDrivingLicesnseApplication = clsLocalDrivingLicenseApplications.Find(TestAppointments.LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicesnseApplication == null || TestAppointments == null)
            {
                return;
            }
            DLAppID.Text = _LocalDrivingLicesnseApplication.LocalDrivingLicenseApplicationID.ToString();
            DClass.Text = _LocalDrivingLicesnseApplication.LicenseClass.ClassName;
            lblName.Text = _LocalDrivingLicesnseApplication.PersonInfo.FullName;
            lblTrial.Text = clsLocalDrivingLicenseApplications.Trial(_LocalDrivingLicesnseApplication.LocalDrivingLicenseApplicationID).ToString();
            lblFees.Text = _LocalDrivingLicesnseApplication.PaidFees.ToString();
            rbPass.Checked = true;
            lblDate.Text = _LocalDrivingLicesnseApplication.ApplicationDate.ToString();
            LodeHader(_TestType);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            TestAppointments = clsTestAppointments.Find(_TestAppointmentID);
            _LocalDrivingLicesnseApplication = clsLocalDrivingLicenseApplications.Find(TestAppointments.LocalDrivingLicenseApplicationID);

            clsTests Test = new clsTests();
            
            Test.TestResult = (rbPass.Checked? true : false);

            Test.TestAppointmentID = _TestAppointmentID;
            Test.CreatedByUserID = clsGlopal.CurrentUser.UserID;
            Test.Notes = tbNotes.Text;

            if (Test.Save())
            {
                if (rbPass.Checked)
                {

                    if ((_LocalDrivingLicesnseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest)
                        && _LocalDrivingLicesnseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest)
                        && _LocalDrivingLicesnseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest)) == true)
                    {
                        _LocalDrivingLicesnseApplication.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
                        _LocalDrivingLicesnseApplication.Save();
                    }

                    if (_LocalDrivingLicesnseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        TestAppointments.Lock();


                    }
                    else if (_LocalDrivingLicesnseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {

                        TestAppointments.Lock();


                    }
                    else if (_LocalDrivingLicesnseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest))
                    {
                        TestAppointments.Lock();
                    }
                    TestAppointments.Save();
                }
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
