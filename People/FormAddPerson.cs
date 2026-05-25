using DVDL.Properties;
using DVDLBusinessLayer;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace DVDL
{
    public partial class FormAddPerson : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
         public delegate void OnPersonSave(object sender, clsPeople Person);

        // Declare an event using the delegate
        public event OnPersonSave OnPersonSaved;
        int _PersonID;
        clsPeople _Person;

        public FormAddPerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

        }

        public FormAddPerson(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
            _Mode =   enMode.Update;
        }

        // ======================= COUNTRIES =======================
        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            comboBoxCountry.DataSource = dtCountries;
            comboBoxCountry.DisplayMember = "ClassName";
            comboBoxCountry.ValueMember = "CountryID";
        }

        // ========================================================
        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FillCountriesInComoboBox();

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPeople();
            }
            else
            {
                lblMode.Text = "Update Person";
            }

            //set default image for the person.
            if (RDBMale.Checked)
                pictureBox3.ImageLocation = pictureBoxMeal.ImageLocation;
            else
            {
                pictureBox3.ImageLocation = pictureBoxFemale.ImageLocation;
            }

            //hide/show the remove linke incase there is no image for the person.
            linkLabelReomv.Visible = (pictureBox3.ImageLocation != null);

            //we set the max date to 18 years from today, and set the default value the same.
          //  dateTimePicker2.MaxDate = DateTime.Now.AddYears(-18);
            //dateTimePicker2.Value = dateTimePicker2.MaxDate;

            //should not allow adding age more than 100 years
      //dateTimePicker2.MinDate = DateTime.Now.AddYears(-100);

            //this will set default country to jordan.
            comboBoxCountry.SelectedIndex = comboBoxCountry.FindString("Jordan");

            txFirst.Text = "";
            txSecond.Text = "";
            txThird.Text = "";
            txLast.Text = "";
            txNationalNo.Text = "";
            RDBMale.Checked = true;
            txPhone.Text = "";
            txEmail.Text = "";
            tcAddress.Text = "";


        }
        private void _LoadData()
        {
            _FillCountriesInComoboBox();

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                lbPersonID.Text = "N/A";
                _Person = new clsPeople();
                 return;
            }

            _Person = clsPeople.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID);
                this.Close();
                return;
            }

            lblMode.Text = "Edit Person";
            lbPersonID.Text = _Person.PersonID.ToString();

            txFirst.Text = _Person.FirstName;
            txSecond.Text = _Person.SecondName;
            txThird.Text = _Person.ThirdName;
            txLast.Text = _Person.LastName;
            txNationalNo.Text = _Person.NationalNo;
            txEmail.Text = _Person.Email;
            txPhone.Text = _Person.Phone;
            tcAddress.Text = _Person.Address;
            dateTimePicker2.Value = _Person.DateOfBirth;


            if (_Person != null &&
                _Person.NationalityCountryID > 0)
            {
                comboBoxCountry.SelectedValue = _Person.NationalityCountryID;
            }


            if (_Person.Gendor == 0)
                RDBMale.Checked = true;
            else
                RDBFemale.Checked = true;

            if (!string.IsNullOrEmpty(_Person.ImagePath) &&
       File.Exists(_Person.ImagePath))
            {
                pictureBox3.Load(_Person.ImagePath);
            }
            else
            {
                pictureBox3.Image = null;
                linkLabelReomv.Visible = false;
            }
        }

        private void FormAddPerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        // ======================= SAVE =======================
        private void button_Save_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!_HandlePersonImage())
                return;
            _Person.FirstName = txFirst.Text.Trim();
            _Person.SecondName = txSecond.Text.Trim();
            _Person.ThirdName = txThird.Text.Trim();
            _Person.LastName = txLast.Text.Trim();
            _Person.NationalNo = txNationalNo.Text.Trim();
            _Person.Email = txEmail.Text.Trim();
            _Person.Phone = txPhone.Text.Trim();
            _Person.Address = tcAddress.Text.Trim();
            _Person.DateOfBirth = dateTimePicker2.Value;
            _Person.Gendor = (short)(RDBMale.Checked ? 0 : 1);

            _Person.NationalityCountryID =
      Convert.ToInt32(comboBoxCountry.SelectedValue);



            if (pictureBox3.ImageLocation != null)
                _Person.ImagePath = pictureBox3.ImageLocation;
            else
                _Person.ImagePath = "";
            if (_Person.Save())
            {
                OnPersonSaved?.Invoke(this, _Person);
                MessageBox.Show("Data Saved Successfully ✅");
                _Mode = enMode.Update;
                lblMode.Text = "Edit Person";
                lbPersonID.Text = _Person.PersonID.ToString();
            }
            else
            {
                MessageBox.Show("Error: Data Is Not Saved ❌");
            }
        }
        // ====================================================

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);
                pictureBox3.ImageLocation = selectedFilePath;

                pictureBox3.Load(selectedFilePath);
                // ...
                linkLabelReomv.Visible = true;
            }
        }
        private bool _HandlePersonImage()
        {

            //this procedure will handle the person image,
            //it will take care of deleting the old image from the folder
            //in case the image changed. and it will rename the new image with guid and 
            // place it in the images folder.


            //Person.ImagePath contains the old Image, we check if it changed then we copy the new image
            if (_Person.ImagePath != pictureBox3.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        // We could not delete the file.
                        //log it later   
                    }
                }

                if (pictureBox3.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = pictureBox3.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pictureBox3.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabelReomv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox3.ImageLocation = null;
            linkLabelReomv.Visible = false; 
        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (txEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidatoin.ValidateEmail(txEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txEmail, null);
            };

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (txNationalNo.Text.Trim() != _Person.NationalNo && clsPeople.isPersonExist(txNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txNationalNo, null);
            }
        }

        private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
