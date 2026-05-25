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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVDL.Users
{
    public partial class ManageUsersForm : Form
    {
        private DataTable _dtAllUsers = clsUser.GetAllUsers();

        public ManageUsersForm()
        {
            InitializeComponent();
        }
    
     

        private void ManageUsersFrom_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();
            DvgUsers.DataSource = _dtAllUsers;
            comboBox_Flitering.SelectedIndex = 0;
            lblCountRecords.Text = DvgUsers.Rows.Count.ToString();

            DvgUsers.Columns[0].HeaderText = "User ID";
            DvgUsers.Columns[0].Width = 110;

            DvgUsers.Columns[1].HeaderText = "Person ID";
            DvgUsers.Columns[1].Width = 120;

            DvgUsers.Columns[2].HeaderText = "Full Name";
            DvgUsers.Columns[2].Width = 350;

            DvgUsers.Columns[3].HeaderText = "UserName";
            DvgUsers.Columns[3].Width = 120;

            DvgUsers.Columns[4].HeaderText = "Is Active";
            DvgUsers.Columns[4].Width = 120;
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {

            AddNewUserFrm Frm1 = new AddNewUserFrm();
            Frm1.ShowDialog();
            ManageUsersFrom_Load(null, null);
        }


        private void textSerarch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (comboBox_Flitering.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }
            if (textSerarch.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblCountRecords.Text = DvgUsers.Rows.Count.ToString();
                return;
            }
            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, textSerarch.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, textSerarch.Text.Trim());

            lblCountRecords.Text = _dtAllUsers.Rows.Count.ToString();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DvgUsers.CurrentRow == null ||
DvgUsers.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a valid row first.");
                return;
            }

            int UserID = Convert.ToInt32(
                DvgUsers.CurrentRow.Cells["UserID"].Value
            );

            AddNewUserFrm frm = new AddNewUserFrm(UserID);
            frm.ShowDialog();
        }

        private void showDetalisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(DvgUsers.CurrentRow == null ||
DvgUsers.CurrentRow.IsNewRow)

            {
                MessageBox.Show("Please select a valid row first.");
                return;
            }

            int UserID = Convert.ToInt32(
                DvgUsers.CurrentRow.Cells["UserID"].Value
            );
            clsUser user = clsUser.FindByUserID(UserID);
            FrmUserInfo frm = new FrmUserInfo(user);
            frm.ShowDialog();
            ManageUsersFrom_Load(null, null);

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete contact [" + DvgUsers.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsUser.DeleteUser((int)DvgUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                    ManageUsersFrom_Load(null, null);

                }

                else
                    MessageBox.Show("Contact is not deleted.");

            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void comboBox_Flitering_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Flitering.Text == "Is Active")
            {
                textSerarch.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                textSerarch.Visible = (comboBox_Flitering.Text != "None");
                cbIsActive.Visible = false;
                if (comboBox_Flitering.Text == "None")
                {
                    textSerarch.Enabled = false;
                }
                else
                    textSerarch.Enabled = true;
                textSerarch.Text = "";
                textSerarch.Focus();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;
            switch (FilterValue)
            {
                case "All":

                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;


            }

            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblCountRecords.Text = _dtAllUsers.Rows.Count.ToString();

        }
    }
}
