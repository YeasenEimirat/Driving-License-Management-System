using DVDL.ApplicationType;
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
    public partial class ListTestType : Form
    {
        private DataTable _dtAllTestTypes  = clsTestType.GetAllTestTypes();
        public ListTestType()
        {
            InitializeComponent();
        }
        private void _LoadData()
        {
            _dtAllTestTypes = clsTestType.GetAllTestTypes();

            dvgTestTypes.DataSource = _dtAllTestTypes;
            lblCountRecords.Text = dvgTestTypes.Rows.Count.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new UpdateTestType((clsTestType.enTestType)dvgTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _LoadData();
        }

        private void ListTestType_Load(object sender, EventArgs e)
        {
        //    dvgTestTypes.Columns[0].HeaderText = "TestType ID";
        //    dvgTestTypes.Columns[0].Width = 110;
         
        //    dvgTestTypes.Columns[1].HeaderText = "Test Type Title";
        //    dvgTestTypes.Columns[1].Width = 250;

        //    dvgTestTypes.Columns[2].HeaderText = "Test Type Description";
        //    dvgTestTypes.Columns[2].Width = 400;

        //    dvgTestTypes.Columns[3].HeaderText = "ApplicationFees";
        //    dvgTestTypes.Columns[3].Width = 100;
            _LoadData();
        }
    }
}
