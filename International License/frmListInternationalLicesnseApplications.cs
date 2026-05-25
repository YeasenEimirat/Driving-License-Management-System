using DVDL.Drivers;
using DVDL.Licenses;
using DVDLBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVDL.International_License
{
    public partial class frmListInternationalLicesnseApplications : Form
    {
        private DataTable _dtAllInternationalLicense = clsInternationalLicense.GetAll();

        private void _LoadData()
        {
            _dtAllInternationalLicense = clsInternationalLicense.GetAll();




            dgvInternationalLicenses.DataSource = _dtAllInternationalLicense;

            dgvInternationalLicenses.Columns[0].HeaderText = "InternationalLicenseID";
            dgvInternationalLicenses.Columns[0].Width = 150;

            dgvInternationalLicenses.Columns[1].HeaderText = "ApplicationID";
            dgvInternationalLicenses.Columns[1].Width = 130;

            dgvInternationalLicenses.Columns[2].HeaderText = "DriverID";
            dgvInternationalLicenses.Columns[2].Width = 100;

            dgvInternationalLicenses.Columns[3].HeaderText = "LocalLicenseID";
            dgvInternationalLicenses.Columns[3].Width =
            120;
            dgvInternationalLicenses.Columns[4].HeaderText = "IssueDate";
            dgvInternationalLicenses.Columns[4].Width = 100;

            dgvInternationalLicenses.Columns[5].HeaderText = "ExpirationDate";
            dgvInternationalLicenses.Columns[5].Width = 100;

            dgvInternationalLicenses.Columns[6].HeaderText = "IsActive";
            dgvInternationalLicenses.Columns[6].Width =
            70;
            dgvInternationalLicenses.Columns[7].HeaderText = "CreatedByUserID";
            dgvInternationalLicenses.Columns[7].Width =
            110;

          
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }
        private string GetColumnName(string selectedText)
        {
            switch (selectedText)
            {



                case "International License ID": return "InternationalLicenseID";
                case "Application ID": return "ApplicationID";
                case "Driver ID": return "DriverID";
                case "Local License ID": return "LocalLicenseID";
                case "Is Active": return "IsActive";
                default: return "None";
            }
        }
        private void ApplyFilter(string columnName, string value)
        {
            if (string.IsNullOrWhiteSpace(value) || columnName == "None")
            {
                _dtAllInternationalLicense.DefaultView.RowFilter = "";
            }
          
                if (int.TryParse(value, out int id))
                    _dtAllInternationalLicense.DefaultView.RowFilter = $"[{columnName}] = {id}";
                else
                    _dtAllInternationalLicense.DefaultView.RowFilter = "";
          
          

            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();



        }
        private void _RefreshPeopleList()
        {
            _LoadData();
        }
        public frmListInternationalLicesnseApplications()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicesnseApplications_Load(object sender, EventArgs e)
        {
            cbIsReleased.Visible = false;
            dgvInternationalLicenses.ContextMenuStrip = cmsApplications;
            cbFilterBy.SelectedIndex = 0;
            cbIsReleased.SelectedIndex = 0;
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string column = GetColumnName(cbFilterBy.Text);
            ApplyFilter(column, txtFilterValue.Text);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";
                if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false; 
                cbIsReleased.Visible = true;    
            }
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            else
            {
                ApplyFilter("None", "");
            }
            _RefreshPeopleList();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
  

            if (GetColumnName(cbFilterBy.Text) == "IsActive")
            {
                txtFilterValue.Visible = false;

                cbIsReleased.Visible = true;
                switch (cbIsReleased.SelectedIndex)
                {
                    case 0:      _RefreshPeopleList();
                        break;
                    case 1:
                        ApplyFilter("IsActive", "1");
                        break;
                        case 2:
                        ApplyFilter("IsActive", "0");
                        break;

                }
            }
            
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new ShowLicenseDetailes((int)dgvInternationalLicenses.CurrentRow.Cells[3].Value);
            form.ShowDialog();
            frmListInternationalLicesnseApplications_Load(null, null);

        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new ShowDetailsForm(clsApplications.Find((int)dgvInternationalLicenses.CurrentRow.Cells[1].Value).ApplicantPersonID);
            form.ShowDialog();
            frmListInternationalLicesnseApplications_Load(null, null);

        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            Form form = new NewInternationalLicense();
            form.ShowDialog();
            frmListInternationalLicesnseApplications_Load(null, null);
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new PersonLicenseHistoryForm(clsApplications.Find((int)dgvInternationalLicenses.CurrentRow.Cells[1].Value).ApplicantPersonID);
            frm.ShowDialog();
            frmListInternationalLicesnseApplications_Load(null, null);

        }
    }
}
