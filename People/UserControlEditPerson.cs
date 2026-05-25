using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVDL
{
    public partial class UserControlEditPerson : UserControl
    {
        public UserControlEditPerson()
        {
            InitializeComponent();
        }
        public  int _personID; 

        public  void  GetPersonID(int personID)
        {
            _personID = personID; 
        }
        private void linkLabelEditPorson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormAddPerson frm = new FormAddPerson(_personID);
            frm.ShowDialog();   // ✅ الحل هنا
        }
    }
}
