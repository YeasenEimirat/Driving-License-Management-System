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

namespace DVDL.TestTypes
{
    public partial class UpdateTestType : Form
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        clsTestType _TestType; 
        public UpdateTestType()
        {
            InitializeComponent();
        }
        public UpdateTestType(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            _TestType.TestTypeID = _TestTypeID;
            _TestType.TestTypeFees = Convert.ToSingle(txtFees.Text.Trim()); ;
            _TestType.TestTypeDescription = txtDescription.Text;
            _TestType.TestTypeTitle = txtTitel.Text;

            if (_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {


            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitel, "Title cannot be Description!");
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

        private void UpdateTestType_Load(object sender, EventArgs e)
        {
            _TestType = clsTestType.FindByTestTypeID(_TestTypeID);
            lblID.Text = _TestTypeID.ToString();
            
            if (_TestType == null)
            {
                MessageBox.Show("Test Type not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtFees.Text = _TestType.TestTypeFees.ToString();
            txtTitel.Text = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
        }
    }
}
