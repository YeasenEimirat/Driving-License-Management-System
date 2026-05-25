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

namespace DVDL.UesrsControls
{
    public partial class LoginInFo : UserControl
    {
        private clsUser _user;
        private int _UserID = -1;

        public int UserID
        {
            get { return _UserID; }
        }
        public void LoadUserInfo(int UserID)
        {
            _user = clsUser.FindByUserID(UserID);
            if (_user == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }
        private void _FillUserInfo()
        {

            personaLInfoUserControl11.LoadPerson(_user.PersonID);
            labelUserID.Text = (_user.UserID).ToString();
            labelUserName.Text = (_user.UserName).ToString();
            labelActive.Text = _user.IsActive.ToString();
            if (_user.IsActive)
                labelActive.Text = "Yes";
            else
                labelActive.Text = "No";

        }

        private void _ResetPersonInfo()
        {

            labelUserID.Text = "[???]";
            labelUserName.Text = "[???]";
            labelActive.Text = "[???]";
        }
        
        public LoginInFo()
        {
            InitializeComponent();
        }
         private void label6_Click(object sender, EventArgs e)
        {

        }
 
        private void LoginInFo_Load(object sender, EventArgs e)
        {
        
        }
    }
}
