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

namespace DVDL.People
{
    public partial class PersonCardControl : UserControl
    {
        public PersonCardControl()
        {
            InitializeComponent();
        }
        private clsUser _User;
        private clsPeople _Person;

        private void userControlFltier1_OnPersonSelected(DVDLBusinessLayer.clsPeople person)
        {
            personaLInfoUserControl11.LoadPerson(person);
        }

        private void PersonCardControl_Load(object sender, EventArgs e)
        {
            _User = new clsUser();
            userControlFltier1.OnPersonSelected += userControlFltier1_OnPersonSelected;

            if (_Person != null)
            {
                personaLInfoUserControl11.LoadPerson(_Person);
                _User.PersonID = _Person.PersonID;
            }
        }
    }
}
