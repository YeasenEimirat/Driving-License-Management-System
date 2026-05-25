using DVDLBusinessLayer;
using System;
using System.Windows.Forms;

namespace DVDL.Applications.Controlls
{
    public partial class ApplicationBasicInfoControl : UserControl
    {
        public ApplicationBasicInfoControl()
        {
            InitializeComponent();
        }

        private clsApplications _Application;

        // Property بدل static
         

        // تحميل البيانات
        public void LoadApplicationData(int _ApplicationID )
        {
            _Application = clsApplications.Find(_ApplicationID);

            if (_Application == null)
            {
                MessageBox.Show("Application Not Found!");
                return;
            }
            else
            {
  lblApplicationID.Text = _Application.ApplicationID.ToString();
            lblApplicant.Text = _Application.ApplicantPersonID.ToString();
            lblCreatedByUser.Text = _Application.CreatedByUser.UserName;
            lblDate.Text = _Application.ApplicationDate.ToString();
            lblStatusDate.Text = _Application.LastStatusDate.ToString();
            lblType.Text = _Application.ApplicationTypes.ApplicationTypeTitle.ToString();
            lblFees.Text = _Application.PaidFees.ToString();
            }
          
        }

        // فتح تفاصيل الشخص
        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_Application == null) return;

            Form frm = new ShowDetailsForm(_Application.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void lblFees_Click(object sender, EventArgs e)
        {

        }
    }
}