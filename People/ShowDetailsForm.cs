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
    public partial class ShowDetailsForm : Form
    {
        int _PersonID; 
        public ShowDetailsForm(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void ShowDetailsForm_Load(object sender, EventArgs e)
        {
         personaLInfoUserControl11.LoadPerson(_PersonID);
         }
    }
}
