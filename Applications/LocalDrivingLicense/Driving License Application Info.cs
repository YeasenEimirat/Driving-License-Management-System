using DVDLBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVDL.LocalDrivingLicense
{
    public partial class Driving_License_Application_Info : Form
    {
        private  int _LocalDrivingID; 
        public Driving_License_Application_Info()
        {
            InitializeComponent();
        }
        public Driving_License_Application_Info(int LocalDrivingID)
        {
            InitializeComponent();
            _LocalDrivingID = LocalDrivingID; 
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void applicationBasicInfoControl1_Load(object sender, EventArgs e)
        {
            applicationBasicInfoControl1.LoadApplicationData( clsLocalDrivingLicenseApplications.Find(_LocalDrivingID).ApplicationID);
        }

        private void Driving_License_Application_Info_Load(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications =  clsLocalDrivingLicenseApplications.Find(_LocalDrivingID);
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingID.ToString();
            lblAppliedFor.Text = clsLicenseClass.Find(LocalDrivingLicenseApplications.LicenseClassID).ClassName;
            clsApplications app = clsApplications.Find(LocalDrivingLicenseApplications.ApplicationID);
            int passed = 0;

            if (LocalDrivingLicenseApplications.DoesPassTestType(clsTestType.enTestType.VisionTest))
                passed++;

            if (LocalDrivingLicenseApplications.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                passed++;

            if (LocalDrivingLicenseApplications.DoesPassTestType(clsTestType.enTestType.StreetTest))
                passed++;

            lblPassedTests.Text = $"{passed}/3";

        }
    }
}
