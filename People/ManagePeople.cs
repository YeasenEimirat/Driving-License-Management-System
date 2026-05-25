using DVDLBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVDL
{
    public partial class ManagePepole : Form
    {
        private DataTable _dtAllPeople = clsPeople.GetAllPeople();
        private DataTable _dtPeople;

        public ManagePepole()
        {
            InitializeComponent();
        }
        // تحميل البيانات
        private void _LoadData()
        {
            _dtAllPeople = clsPeople.GetAllPeople();

            _dtPeople = _dtAllPeople.DefaultView.ToTable(false,
                "PersonID", "NationalNo",
                "FirstName", "SecondName", "ThirdName", "LastName",
                "GendorCaption", "DateOfBirth", "CountryName",
                "Phone", "Email");

            dgvManagePeople.DataSource = _dtPeople;
            label3.Text = dgvManagePeople.Rows.Count.ToString();
        }

        // تحديث البيانات بعد الإضافة/الحذف/التعديل
        private void _RefreshPeopleList()
        {
            _LoadData();
        }

        // تحويل اختيار ComboBox إلى اسم العمود الحقيقي
        private string GetColumnName(string selectedText)
        {
            switch (selectedText)
            {
                case "PersonID": return "PersonID";
                case "NationalNo": return "NationalNo";
                case "FirstName": return "FirstName";
                case "SecondName": return "SecondName";
                case "ThirdName": return "ThirdName";
                case "LastName": return "LastName";
                case "Gendor": return "GendorCaption";
                case "Nationality": return "CountryName";
                case "Phone": return "Phone";
                case "Email": return "Email";
                default: return "None";
            }
        }

        private void ApplyFilter(string columnName , string value)
        {
            if (string.IsNullOrWhiteSpace(value) || columnName == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
            }
            else if (columnName == "PersonID")
            {
                if (int.TryParse(value, out int id))
                    _dtPeople.DefaultView.RowFilter = $"[{columnName}] = {id}";
                else
                    _dtPeople.DefaultView.RowFilter = "";
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = $"[{columnName}] = {value}";
            }

            label3.Text = dgvManagePeople.Rows.Count.ToString();

        }

 


        private void ManagePepole_Load(object sender, EventArgs e)
        {
            dgvManagePeople.ContextMenuStrip = contextMenuStrip1;
            comboBox1.SelectedIndex = 0;
            _LoadData();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _RefreshPeoplList()
        {
            _LoadData();
        }
        private void ptAdd_Click(object sender, EventArgs e)
        {
            FormAddPerson frm = new FormAddPerson();
            frm.ShowDialog();   // ✅ الحل هنا
            _RefreshPeoplList(); // ✅ يحدث بعد الإضافة
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = comboBox1.Text != "None";

            if (textBox1.Visible)
            {
                textBox1.Text = "";
                textBox1.Focus();
            }
            else
            {
                ApplyFilter("None", "");
            }
        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete contact [" + dgvManagePeople.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPeople.DeletePerson((int)dgvManagePeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                    _RefreshPeoplList();
                }

                else
                    MessageBox.Show("Contact is not deleted.");

            }

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvManagePeople.CurrentRow == null ||
       dgvManagePeople.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a valid row first.");
                return;
            }

            int personID = Convert.ToInt32(
                dgvManagePeople.CurrentRow.Cells["PersonID"].Value
            );

            FormAddPerson frm = new FormAddPerson(personID);
            frm.ShowDialog();

            _RefreshPeoplList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string column = GetColumnName(comboBox1.Text);
            ApplyFilter(column, textBox1.Text);
        }

        private void showDetalisToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvManagePeople.CurrentRow == null ||
      dgvManagePeople.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Please select a valid row first.");
                return;
            }

            int personID = Convert.ToInt32(
                dgvManagePeople.CurrentRow.Cells["PersonID"].Value
            );

            ShowDetailsForm frm = new ShowDetailsForm(personID);
            frm.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
