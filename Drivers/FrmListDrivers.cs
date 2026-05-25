using DVDLBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVDL.Drivers
{
    public partial class FrmListDrivers : Form
    {
        private DataTable _dtAllDrivers = clsDriver.GetAll();
        private DataTable _dtDrivers;
        private void _LoadData()
        {
            _dtAllDrivers = clsDriver.GetAll();


 

            dgvDrivers.DataSource = _dtAllDrivers;

            dgvDrivers.Columns[0].HeaderText = "DriverID";
            dgvDrivers.Columns[0].Width = 150;

            dgvDrivers.Columns[1].HeaderText = "PersonID";
            dgvDrivers.Columns[1].Width = 150;

            dgvDrivers.Columns[2].HeaderText = "NationalNo";
            dgvDrivers.Columns[2].Width = 150;

            dgvDrivers.Columns[3].HeaderText = "FullName";
            dgvDrivers.Columns[3].Width = 
               390;

            label4.Text = dgvDrivers.Rows.Count.ToString();
        }
        private void _RefreshPeopleList()
        {
            _LoadData();
        }
        private string GetColumnName(string selectedText)
        {
            switch (selectedText)
            {


                case "Person ID": return "PersonID";
                case "National No.": return "NationalNo";
                case "Driver ID": return "DriverID";
                case "Full Name": return "FullName";

                default: return "None";
            }
        }
        private void ApplyFilter(string columnName, string value)
        {
            if (string.IsNullOrWhiteSpace(value) || columnName == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
            }
            else if (columnName == "PersonID" || columnName == "DriverID")
            {
                if (int.TryParse(value, out int id))
                    _dtAllDrivers.DefaultView.RowFilter = $"[{columnName}] = {id}";
                else
                    _dtAllDrivers.DefaultView.RowFilter = "";
            }
            else
            {
                _dtAllDrivers.DefaultView.RowFilter =
      $"[{columnName}] LIKE '{value}%'";
            }

            label4.Text = dgvDrivers.Rows.Count.ToString();


        }

        public FrmListDrivers()
        {
            InitializeComponent();
        }

        private void FrmListDrivers_Load(object sender, EventArgs e)
        {
            dgvDrivers.ContextMenuStrip = cmsDrivers;
           cbFilterBy.SelectedIndex = 0 ; 
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";

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

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ShowDetailsForm((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string column = GetColumnName(cbFilterBy.Text);
            ApplyFilter(column, txtFilterValue.Text);
        }
    }
}
