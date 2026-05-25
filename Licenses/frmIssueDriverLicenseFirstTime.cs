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
using static DVDL.FormAddPerson;

namespace DVDL.Licenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private int _LocalDrivingID;
        public frmIssueDriverLicenseFirstTime(int localDrivingID)
        {
            InitializeComponent();
            _LocalDrivingID = localDrivingID;
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            applicationBasicInfoControl1.LoadApplicationData(clsLocalDrivingLicenseApplications.Find(_LocalDrivingID).ApplicationID);
            clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.Find(_LocalDrivingID);
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

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
        clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications = clsLocalDrivingLicenseApplications.Find(_LocalDrivingID);

            if (LocalDrivingLicenseApplications.IssueLicenseFirstTime(txtNotes.Text,   clsGlopal.CurrentUser.UserID) )
            {
                
                MessageBox.Show("Data Saved Successfully ✅");
                
            }
            else
            {
                MessageBox.Show("Error: Data Is Not Saved ❌");
            }
        }
    }
}
