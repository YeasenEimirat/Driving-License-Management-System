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
    public partial class Mange_Application_Types : Form
    {
        private DataTable _dtAllApplicationTypes = clsApplicationTypes.GetAllApplicationTypes();

        public Mange_Application_Types()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _LoadData()
        {
            _dtAllApplicationTypes = clsApplicationTypes.GetAllApplicationTypes();

            dvgApplicationTypes.DataSource = _dtAllApplicationTypes;
            lblCountRecords.Text = dvgApplicationTypes.Rows.Count.ToString();
        }
        private void Mange_Application_Types_Load(object sender, EventArgs e)
        {
            _LoadData(); 

            dvgApplicationTypes.Columns[0].HeaderText = "Application Type ID";
            dvgApplicationTypes.Columns[0].Width = 110;

            dvgApplicationTypes.Columns[1].HeaderText = "Application Type Title";
            dvgApplicationTypes.Columns[1].Width = 250;

            dvgApplicationTypes.Columns[2].HeaderText = "ApplicationFees";
            dvgApplicationTypes.Columns[2].Width = 100;
 
        }

        private void editTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new UpdateAplicationTypes((int)dvgApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _LoadData();
         }
    }
}
