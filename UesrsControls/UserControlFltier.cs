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
    public partial class UserControlFltier : UserControl
    {
 
        public UserControlFltier()
        {
            InitializeComponent();
        }
        //   static public int PersonId;
        public   clsPeople Person;
        public event Action<clsPeople> OnPersonSelected;

        // static public string  NationalNo;




        private void UserControlFltier_Load(object sender, EventArgs e)
        {
   
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
                return;

            //  textBox1.Visible = comboBox1.SelectedItem.ToString() != "None";

            switch (comboBox1.SelectedItem.ToString())
            {


                case "PersonID":
                    Person = clsPeople.Find(Convert.ToInt32(textBox1.Text));
                    if (Person != null)
                        OnPersonSelected?.Invoke(Person);
                    break;

                case "NationalNo":
                    Person =  clsPeople.Find(textBox1.Text.ToString());
                    if (Person != null)
                        OnPersonSelected?.Invoke(Person);
                    break;


            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FormAddPerson frm = new FormAddPerson();
            frm.OnPersonSaved += Form_OnPersonSaved; // Subscribe to the event
            frm.ShowDialog();   // ✅ الحل هنا

        }
        private void Form_OnPersonSaved(object sender, clsPeople Person)
        {
            // Handle the data received from Form2
            OnPersonSelected?.Invoke(Person);

        }

    }
}
