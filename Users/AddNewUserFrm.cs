using DVDL.UesrsControls;
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

namespace DVDL.Users
{
    public partial class AddNewUserFrm : Form
    {
        clsUser _User;
        int _UserID; 
        public AddNewUserFrm()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

        }
        private clsPeople _Person;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        public AddNewUserFrm(clsPeople person)
        {
            InitializeComponent();
            _Person = person;
            _Mode = enMode.Update;

        }
        public AddNewUserFrm(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _User = clsUser.FindByUserID(_UserID);
            _Person =  clsPeople.Find(_User.PersonID);
            _Mode = enMode.Update; 

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void userControlFltier1_Load(object sender, EventArgs e)
        {

        }

        private void AddNewUserFrm_Load(object sender, EventArgs e)
        {
            _User = new clsUser();
            userControlFltier1.OnPersonSelected += userControlFltier1_OnPersonSelected_1;
            if (_Person != null)
            {
                if (_Mode == enMode.Update)
                {
                    userControlFltier1.Visible = false;
                }
                 personaLInfoUserControl11.LoadPerson(_Person);
                _User.PersonID = _Person.PersonID;
            }

        }

        private void personaLInfoUserControl11_Load(object sender, EventArgs e)
        {
          
        }
        private clsPeople _SelectedPerson;

        

        private void userControlFltier1_OnPersonSelected_1(clsPeople person)
        {
            _SelectedPerson = person;
            personaLInfoUserControl11.LoadPerson(person);
            _User.PersonID = person.PersonID;
        }


        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (!clsUser.isUserExistForPersonID(_User.PersonID))
            {
            tabControl1.SelectedTab = tabPage2;

            }
            else
            {
                MessageBox.Show(" The Selected Person have alrady User ");
                return; 
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button_Save_Click(object sender, EventArgs e)
        {
          
            _User.UserName = UserNameTb.Text.ToString();
            _User.Password = textBoxPassword.Text.ToString();
            _User.IsActive = (checkBox1.Checked ? true : false);

            if (textBoxPassword.Text != textBoxPassword.Text)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            if (_User.Save())
            {
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

        
    }
}
