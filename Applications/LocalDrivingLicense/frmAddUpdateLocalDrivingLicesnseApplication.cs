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

namespace DVDL.LocalDrivingLicense
{
    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _SelectedPersonID = -1;
        private clsPeople _SelectedPerson;

        clsLocalDrivingLicenseApplications _LocalDrivingLicenseApplication;
        int _LocalDrivingLicenseApplicationsID =-1;
        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

        }

        private void _ResetDefualtValues()
        {

            if (_Mode == enMode.AddNew)
            {

                label1.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplications();
                if (_SelectedPerson == null)
                {
                    tabPage2.Enabled = false;

                }
                else
                {
                    tabPage2.Enabled = true;

                }
                lblFees.Text = clsApplicationTypes.FindByApplicationTypeID((int)clsApplications.enApplicationType.NewDrivingLicense).ApplicationFees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = clsGlopal.CurrentUser.UserName;
            }
            else
            {
                label1.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tabPage2.Enabled = true;
                button_Save.Enabled = true;


            }
        }

        public frmAddUpdateLocalDrivingLicesnseApplication(int LocalDrivingLicenseApplicationsID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationsID = LocalDrivingLicenseApplicationsID; 
            _Mode = enMode.Update;

        }

     
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        private void _FillLicenseClassInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetAll();

            foreach (DataRow row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }


        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
            {
                _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplications.Find(_LocalDrivingLicenseApplicationsID);
                userControlFltier1.Visible = false;
                personaLInfoUserControl11.LoadPerson(_LocalDrivingLicenseApplication.ApplicantPersonID);
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplicationsID.ToString(); 
            } 
            _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplications();
            userControlFltier1.OnPersonSelected += userControlFltier1_OnPersonSelected_1;
            _FillLicenseClassInComoboBox();
             lblCreatedByUser.Text = clsGlopal.CurrentUser.UserName;
            lblApplicationDate.Text = DateTime.Now.ToString();
        }

        private void userControlFltier1_OnPersonSelected_1(clsPeople person)
        {
            //if (_SelectedPerson == null)
            //{
            //    MessageBox.Show("Please select a person first!");
            //    return;
            //}
            _SelectedPerson = person;
            personaLInfoUserControl11.LoadPerson(person);
        }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            if (_SelectedPerson != null)
            {
                tabPage2.Enabled = true;

            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (_SelectedPerson == null)
            {
                MessageBox.Show("Please select a person first!");
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                clsApplications app = new clsApplications();
               _LocalDrivingLicenseApplication.ApplicantPersonID = _SelectedPerson.PersonID;
               _LocalDrivingLicenseApplication .ApplicationDate = DateTime.Now;
               _LocalDrivingLicenseApplication .ApplicationTypeID = 1;
               _LocalDrivingLicenseApplication.ApplicationStatus = clsApplications.enApplicationStatus.New;
               _LocalDrivingLicenseApplication .LastStatusDate = DateTime.Now;
               _LocalDrivingLicenseApplication .PaidFees = 15;
                _LocalDrivingLicenseApplication.CreatedByUserID = clsGlopal.CurrentUser.UserID;
                _LocalDrivingLicenseApplication.LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;


                _LocalDrivingLicenseApplication.ApplicationID = _LocalDrivingLicenseApplication.ApplicationID;
             }
           if (_LocalDrivingLicenseApplication.LicenseClassID !=-1)
            {
                _LocalDrivingLicenseApplication.LicenseClassID = clsLicenseClass.Find(cbLicenseClass.Text).LicenseClassID;

            }
            else
            {
                MessageBox.Show("Please Select the  License Classes");

            }
 
            if (_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                MessageBox.Show("Data Saved Successfully ✅");
            }
            else
            {
                MessageBox.Show("Error: Data Is Not Saved ❌");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void lblLocalDrivingLicebseApplicationID_Click(object sender, EventArgs e)
        {

        }
    }
}
