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

namespace DVDL.Licenses.Controlls
{
    public partial class DriverLicenseInfo : UserControl
    {
        int _LicenseID = -1;  
        public DriverLicenseInfo()
        {
            InitializeComponent();
        }
       public  void GetLicenseID(int LicenseID)
        {
            _LicenseID = LicenseID;
            _LoadLicenseInfo();
        }
        private void _LoadLicenseInfo()
        {
            if (_LicenseID == -1) return;

            clsLicenses licenses = clsLicenses.Find(_LicenseID);
            if (licenses == null) return;

            clsLicenseClass LicenseClass = clsLicenseClass.Find(licenses.LicenseClassID);
            clsPeople people = clsPeople.Find(clsApplications.Find(licenses.ApplicationID).ApplicantPersonID);

            lblClass.Text = LicenseClass.ClassName;
            lblFullName.Text = people.FullName;
            lblLicenseID.Text = _LicenseID.ToString();
            lblNationalNo.Text = people.NationalNo;
            lblGendor.Text = (people.Gendor == 1 ? "Male" : "Female");
            lblIssueDate.Text = licenses.IssueDate.ToShortDateString();
            lblIssueReason.Text = licenses.IssueReason.ToString();
            lblNotes.Text = licenses.Notes;
            lblIsActive.Text = (licenses.IsActive ? "Active" : "Not Active");
           clsDriver driver =  clsDriver.FindByPersonID(people.PersonID);
            lblDriverID.Text= driver.DriverID.ToString();
            lblDateOfBirth.Text = people.DateOfBirth.ToShortDateString();
            lblExpirationDate.Text = licenses.ExpirationDate.ToShortDateString();
            lblIsDetained.Text = clsDetainLicense.IsDetained(licenses.LicenseID) ? "Yes" : "No";
        }
        private void DriverLicenseInfo_Load(object sender, EventArgs e)
        {
            _LoadLicenseInfo();



        }
    }
}
