using DVDL.Licenses.Controlls;
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

namespace DVDL.Drivers
{
    public partial class PersonLicenseHistoryForm : Form
    {
        int _PersonID = -1;
        public PersonLicenseHistoryForm()
        {
            InitializeComponent();
        }
        public PersonLicenseHistoryForm(int personID)
        {
            InitializeComponent();
            _PersonID = personID;

        }

        private void PersonLicenseHistoryForm_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                personaLInfoUserControl11.LoadPerson(_PersonID);
                userControlFltier1.Enabled = false;
                personaLInfoUserControl11.Enabled = false;
                ctrlDriverLicenses1.PersonID=_PersonID;
            }
            else
            {
                userControlFltier1.Enabled = true;
                personaLInfoUserControl11.Enabled = true;
 
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userControlFltier1_OnPersonSelected(DVDLBusinessLayer.clsPeople obj)
        {
            if (obj != null)
            {
                  personaLInfoUserControl11.LoadPerson(obj);
            ctrlDriverLicenses1.PersonID = obj.PersonID;
            }
            else
            {
                MessageBox.Show("the selected person is not Found please, select again ");
            }
          
         
        }
    }
}
