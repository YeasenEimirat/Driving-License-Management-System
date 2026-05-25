using DVDL.Licenses.Detain_License;
using DVDLBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVDL.Applications.Relase_Detain_Licenses_Application
{
    public partial class frmListDetainedLicenses : Form
    {
        private DataTable _dtAllDetainedLicenses = new DataTable();

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void _UpdateRecordsCount()
        {
            lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private string GetColumnName(string selectedText)
        {
            switch (selectedText)
            {
                case "Detain ID": return "DetainID";
                case "Is Released": return "IsReleased";
                case "National No.": return "NationalNo";
                case "Full Name": return "FullName";
                case "Release Application ID": return "ReleaseApplicationID";
                default: return null;
            }
        }

        private void ApplyFilter(string columnName, string value)
        {
            if (_dtAllDetainedLicenses == null) return;

            if (string.IsNullOrWhiteSpace(value) || columnName == null)
            {
                _dtAllDetainedLicenses.DefaultView.RowFilter = "";
                _UpdateRecordsCount();
                return;
            }

            switch (columnName)
            {
                case "DetainID":
                case "ReleaseApplicationID":
                case "IsReleased":
                    if (int.TryParse(value, out int id))
                        _dtAllDetainedLicenses.DefaultView.RowFilter = $"[{columnName}] = {id}";
                    else
                        _dtAllDetainedLicenses.DefaultView.RowFilter = "";
                    break;

                default:
                    _dtAllDetainedLicenses.DefaultView.RowFilter = $"[{columnName}] LIKE '{value}%'";
                    break;
            }

            _UpdateRecordsCount();
        }

        private void _LoadData()
        {
            try
            {
                _dtAllDetainedLicenses = clsDetainLicense.GetAllDetainedLicenses();

                if (_dtAllDetainedLicenses == null)
                {
                    MessageBox.Show("Failed to load data.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dgvDetainedLicenses.DataSource = _dtAllDetainedLicenses;

                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 90;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 90;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 110;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "National No.";
                dgvDetainedLicenses.Columns[6].Width = 90;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 330;

                dgvDetainedLicenses.Columns[8].HeaderText = "Release Application ID";
                dgvDetainedLicenses.Columns[8].Width = 150;

                _UpdateRecordsCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbIsReleased.Visible = false;
            cbFilterBy.SelectedIndex = 0;
            cbIsReleased.SelectedIndex = 0;
            _LoadData();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string columnName = GetColumnName(cbFilterBy.Text);
            bool isReleasedSelected = columnName == "IsReleased";

            txtFilterValue.Visible = !isReleasedSelected && columnName != null;
            cbIsReleased.Visible = isReleasedSelected;

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            else
            {
                ApplyFilter(null, "");
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string column = GetColumnName(cbFilterBy.Text);
            ApplyFilter(column, txtFilterValue.Text);
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GetColumnName(cbFilterBy.Text) != "IsReleased") return;

            switch (cbIsReleased.SelectedIndex)
            {
                case 0: ApplyFilter(null, ""); break;
                case 1: ApplyFilter("IsReleased", "1"); break;
                case 2: ApplyFilter("IsReleased", "0"); break;
            }
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            using (Form frm = new frmDetainLicenseApplication())
            {
                frm.ShowDialog();
            }
            _LoadData();
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            using (Form frm = new frmReleaseDetainedLicenseApplication())
            {
                frm.ShowDialog();
            }
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}