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
    public partial class PesonCard : UserControl
    {
         private  static clsUser _user = clsGlopal.CurrentUser; 
        public PesonCard()
        { 
            InitializeComponent();
        }
        

        private void personaLInfoUserControl11_Load(object sender, EventArgs e)
        {

        }

        private void LoginInformations_Load(object sender, EventArgs e)
        {
            personaLInfoUserControl11.LoadPerson(_user.PersonID);

            lblUserID.Text = _user.UserID.ToString();
            lblUserName.Text = _user.UserName;
            lblIsActive.Text = _user.IsActive.ToString();
        }
    }
}
