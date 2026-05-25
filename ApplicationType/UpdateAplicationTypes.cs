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

namespace DVDL.ApplicationType
{
    public partial class UpdateAplicationTypes : Form
    {
        private int _ApplicationTypeID;
        private clsApplicationTypes _ApplicationTypes;
        public UpdateAplicationTypes()
        {
            InitializeComponent();
        }
        public UpdateAplicationTypes( int  ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID; 
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _ApplicationTypes.ApplicationTypeTitle = txtTitel.Text.Trim();
            _ApplicationTypes.ApplicationFees = Convert.ToSingle(txtFees.Text.Trim());
            if (_ApplicationTypes.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        private void UpdateAplicationTypes_Load(object sender, EventArgs e)
        {
            _ApplicationTypes = clsApplicationTypes.FindByApplicationTypeID(_ApplicationTypeID);
            lblID.Text = _ApplicationTypeID.ToString();

            if (_ApplicationTypes == null)
            {
                MessageBox.Show("Application Type not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtFees.Text = _ApplicationTypes.ApplicationFees.ToString();
            txtTitel.Text = _ApplicationTypes.ApplicationTypeTitle;
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {


            if (string.IsNullOrEmpty(txtTitel.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitel, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitel, null);
            };


        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {



            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);

            };


            if (!clsValidatoin.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };

        }

        private void txtTitel_Validated(object sender, EventArgs e)
        {

        }
    }
}
