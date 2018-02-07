using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BarcodeScanAppForms
{
    public partial class BarcodeSearchList : UserControl
    {
        private string ServerDBConnectionString;
        private Form pf;

        public BarcodeSearchList()
        {
            InitializeComponent();
        }

        public BarcodeSearchList(DataTable BarcodeSearchList, string DBConnectionString, Form f)
            : this()
        {
            pf = f;
            ServerDBConnectionString = DBConnectionString;
            this.BarcodeSearchDataGridView.DataSource = BarcodeSearchList;
            this.BarcodeSearchDataGridView.Visible = true;
            this.BarcodeSearchDataGridView.Enabled = true;
            this.BarcodeSearchDataGridView.Columns[0].HeaderCell.Value = "ID";
            this.BarcodeSearchDataGridView.Columns[1].HeaderCell.Value = "Module Serial Number";
            this.BarcodeSearchDataGridView.Columns[2].HeaderCell.Value = "Reader Serial Number";
            this.BarcodeSearchDataGridView.Columns[3].HeaderCell.Value = "Laminator Number";
            this.BarcodeSearchDataGridView.Columns[4].HeaderCell.Value = "Creation Time";
            this.BarcodeSearchDataGridView.Columns[5].Visible = false;
            this.BarcodeSearchDataGridView.Columns[6].Visible = false;
            this.BarcodeSearchDataGridView.Columns[7].Visible = false;
            this.BarcodeSearchDataGridView.Columns[0].Visible = false;

            DataGridViewCellStyle HeaderStyle = new DataGridViewCellStyle();
            HeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            HeaderStyle.BackColor = System.Drawing.Color.Gray;
            HeaderStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            DataGridViewCellStyle ContentStyle = new DataGridViewCellStyle();
            ContentStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ContentStyle.Font = new Font("Microsoft Sans Serif", 8);

            foreach (DataGridViewColumn col in BarcodeSearchDataGridView.Columns)
            {
                col.HeaderCell.Style = HeaderStyle;
            }

            foreach (DataGridViewRow row in BarcodeSearchDataGridView.Rows)
            {
                row.DefaultCellStyle = ContentStyle;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            //var a = this.Parent.Parent;
            UserControl1 uc1 = new UserControl1(ServerDBConnectionString, pf);
            this.Parent.Controls.Add(uc1);
            this.Dispose();
        }
    }
}
