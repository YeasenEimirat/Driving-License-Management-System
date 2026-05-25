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

namespace DVDL.Users
{
    public partial class ChangePassword : Form
    {

        private int _UserID;
        private clsUser _User;

        public ChangePassword( int UserID)
        {
            InitializeComponent();
            _UserID = UserID; 
        }
        private void _ResetDefualtValues()
        {
            ComfirmPassTxt.Text = "";
            CurrentPassTxt.Text = "";
            NewPasswordTxt.Text = "";
            CurrentPassTxt.Focus();


        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string NewPassword = NewPasswordTxt.Text;

            if (clsUser.ChangePassword(_UserID, NewPassword))
            {
                MessageBox.Show(" Update Password Successfully  ");
            }
            else
            {
                MessageBox.Show("Error: the Password Not Update");
            }
            _User.Password = NewPassword; 
            //if (_User.Save())
            //{
            //    MessageBox.Show("Password Changed Successfully.",
            //       "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    _ResetDefualtValues();
            //}
            //else
            //{
            //    MessageBox.Show("An Erro Occured, Password did not change.",
            //       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(CurrentPassTxt.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(CurrentPassTxt, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(CurrentPassTxt, null);
            };

            if (clsGlopal.CurrentUser.Password != CurrentPassTxt.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(CurrentPassTxt, "Current password is wrong!");
                return;
            }
            else
            {
                errorProvider1.SetError(CurrentPassTxt, null);
            };
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(NewPasswordTxt.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(NewPasswordTxt, "New Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(NewPasswordTxt, null);
            };
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (ComfirmPassTxt.Text.Trim() != NewPasswordTxt.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(ComfirmPassTxt, "Password Confirmation does not match New Password!");
            }
            else
            {
                errorProvider1.SetError(ComfirmPassTxt, null);
            };
        }
        private void loginInFo1_Load(object sender, EventArgs e)
        {

        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            _User = clsUser.FindByUserID(_UserID);

            if (_User == null )
            {
                MessageBox.Show("Could not Find User with id = " + _UserID,
        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;
            }
            loginInFo1.LoadUserInfo(_UserID);
        }
    }
}
