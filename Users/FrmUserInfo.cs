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
    public partial class FrmUserInfo : Form
    {
        private clsUser _User;

        public FrmUserInfo(clsUser User)
        {
            InitializeComponent();
            _User = User;

        }

        private void FrmUserInfo_Load(object sender, EventArgs e)
        {
            loginInFo1.LoadUserInfo(_User.UserID);
        }
    }
}
