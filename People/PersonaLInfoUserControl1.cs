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
using System.IO;
using DVDL.Properties;

namespace DVDL
{
    public partial class PersonaLInfoUserControl1 : UserControl
    {
        public PersonaLInfoUserControl1()
        {
            InitializeComponent();
        }
        private int _personID = -1;
         
        public int personID
        {
            get { return _personID; }
        }

        private clsPeople person; 
        public void LoadPerson(int personID)
        {

            person = clsPeople.Find(personID);
            if (person == null)
            {
              //  ResetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + personID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPerson(clsPeople person)
        {
            if (person == null) return;
            this.person = person;
            _FillPersonInfo();
        }
        
        private void _LoadPersonImage()
        {
            if (person.Gendor == 0)
            {
                pbGendor.Image = Properties.Resources.Man_32;
            }
            else
            {
                pbGendor.Image = Properties.Resources.Woman_32;

            }
            string ImagePath = person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pictureBoximfo.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _FillPersonInfo()
        {
            lbPersonID.Text = person.PersonID.ToString();
            lblFullName.Text = $"{person.FirstName} {person.SecondName} {person.LastName}";
            lblAddress.Text = person.Address;
            lblDateOfBith.Text = person.DateOfBirth.ToShortDateString();
            lblEmail.Text = person.Email;
            lblGendor.Text = person.Gendor == 0 ? "Male" : "Female";
            lblNationalNo.Text = person.NationalNo;
            lblPhone.Text = person.Phone;

            lblCountry.Text = person.CountryInfo != null
                ? person.CountryInfo.CountryName
                : "Unknown";
            _LoadPersonImage();

            userControlEditPerson1._personID = person.PersonID; 

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void userControlEditPerson1_Load(object sender, EventArgs e)
        {
            userControlEditPerson1._personID = personID; 
        }
    }
}
