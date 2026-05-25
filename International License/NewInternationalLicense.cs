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

namespace DVDL.International_License
{
    public partial class NewInternationalLicense : Form
    {
        clsLicenses _Licenses = new clsLicenses();
        public NewInternationalLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _LoadLicenseInfo()
        {
            if (_Licenses == null || _Licenses.LicenseID <= 0)
            {

                return;
            }

            clsApplications app = clsApplications.Find(_Licenses.ApplicationID);

            if (app == null)
                return;

             
 

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = _Licenses.IssueDate.ToShortDateString();
            lblExpirationDate.Text = _Licenses.ExpirationDate.ToShortDateString();
            lblLocalLicenseID.Text = _Licenses.LicenseID.ToString();
            lblFees.Text = _Licenses.PaidFees.ToString();

            lblCreatedByUser.Text = _Licenses.CreatedByUser.ToString();

         
        }
        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(DVDLBusinessLayer.clsLicenses obj)
        {
            _Licenses = obj;
            _LoadLicenseInfo();
            btnIssueLicense.Enabled = (_Licenses.IsActive); 
             crlShowLicense1.Enabled = false;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (_Licenses.LicenseClassID !=3 )
            {
                MessageBox.Show("the license Class Should be in class 3 .",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
 
                return;
            }
     
            clsApplications oldApplication =
               clsApplications.Find(_Licenses.ApplicationID);
            clsInternationalLicense newInternationalLicense = new clsInternationalLicense();


            if (oldApplication == null)
            {
                MessageBox.Show("Original application not found.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }
            int InterationalLicense = clsInternationalLicense.GetActiveInternationalLicenseID(oldApplication.ApplicantPersonID);
            if (InterationalLicense != -1)
            {

             
            
                MessageBox.Show("the Person already have a International License with id = ." + InterationalLicense,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                
        

                return;
            }

            // ✅ local variable مش field
            clsApplications newApplication = new clsApplications();
           
            newApplication.ApplicantPersonID = oldApplication.ApplicantPersonID;
            newApplication.ApplicationDate = DateTime.Now;
            newApplication.ApplicationTypeID = oldApplication.ApplicationTypeID;
            newApplication.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            newApplication.LastStatusDate = DateTime.Now;
            newApplication.PaidFees = oldApplication.PaidFees;
            newApplication.CreatedByUserID = oldApplication.CreatedByUserID;

            if (!newApplication.Save())
            {
                MessageBox.Show("Failed to create application.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            newInternationalLicense.ApplicationID = newApplication.ApplicationID;
            newInternationalLicense.DriverID = _Licenses.DriverID;
            newInternationalLicense.IssuedUsingLocalLicenseID = _Licenses.LicenseID;
            newInternationalLicense.IssueDate = DateTime.Now;
            newInternationalLicense.ExpirationDate = _Licenses.ExpirationDate;
            newInternationalLicense.IsActive = true;
            newInternationalLicense.CreatedByUserID = _Licenses.CreatedByUser;

    
            if (!newInternationalLicense.Save())
            {
                MessageBox.Show("Failed to create International  license.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }


          

            lblApplicationID.Text = newApplication.ApplicationID.ToString();
            lblInternationalLicenseID.Text = newInternationalLicense.InternationalLicenseID.ToString();

            crlShowLicense1.licenseID = _Licenses.LicenseID;
            
            crlShowLicense1.Enabled = true;
            ctrlDriverLicenseInfoWithFilter1.Enabled = false;

      
            MessageBox.Show("new International License issued successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
    }
}
