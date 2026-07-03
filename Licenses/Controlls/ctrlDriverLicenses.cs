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

namespace DVDL.Licenses.Controlls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }
        private int _personID;
        public int PersonID
        {
            get { return _personID; }
            set
            {
                _personID = value;
                LodeDvgLicenseHistory(); // يحمل تلقائي لما يتغير
            }
        }
        DataTable _dtLocalLicensesHistory;
        DataTable _dtInternationalLicensesHistory;

        private void dgvLocalLicensesHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LodeDvgLicenseHistory()
        {
            _dtLocalLicensesHistory = clsLicenses.ListOfLicensesHistory(PersonID);
            dgvLocalLicensesHistory.DataSource = _dtLocalLicensesHistory;


            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {

                dgvLocalLicensesHistory.Columns[0].Width = 120;


                dgvLocalLicensesHistory.Columns[1].Width = 150;

                dgvLocalLicensesHistory.Columns[2].Width = 120;

                dgvLocalLicensesHistory.Columns[3].Width = 120;
                dgvLocalLicensesHistory.Columns[4].Width = 120;

                dgvLocalLicensesHistory.Columns[5].Width = 110;
            }

            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();
        }
        private void LodeDvgInternationalLicenseHistory()
        {
            _dtInternationalLicensesHistory = clsInternationalLicense.GetAllInternationalLicensesHistory(PersonID);
            dgvInternationalLicensesHistory.DataSource = _dtInternationalLicensesHistory;

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {

                dgvInternationalLicensesHistory.Columns[0].Width = 120;


                dgvInternationalLicensesHistory.Columns[1].Width = 150;

                dgvInternationalLicensesHistory.Columns[2].Width = 120;

                dgvInternationalLicensesHistory.Columns[3].Width = 120;
                dgvInternationalLicensesHistory.Columns[4].Width = 120;

                
            }

            lblInternationalLicensesRecords.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();
        }
       
        private void tcDriverLicenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcDriverLicenses.SelectedIndex == 0)
            {
                LodeDvgLicenseHistory();
            }
            else if (tcDriverLicenses.SelectedIndex == 1)
            {
                LodeDvgInternationalLicenseHistory();
            }
        }
    }
}
