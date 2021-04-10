using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAppIdIdentifier
{
    public partial class SteamAppId : Form
    {
        protected DataTableGeneration dataTableGeneration;

        public SteamAppId()
        {
            dataTableGeneration = new DataTableGeneration();
            Task.Run(async() => await dataTableGeneration.GetDataTableAsync(dataTableGeneration)).Wait();
            InitializeComponent();
        }

        private void SteamAppId_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dataTableGeneration.DataTableToGenerate;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = string.Empty;
            ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Name like '%{0}%'", searchTextBox.Text);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("Name like '%{0}%'", searchTextBox.Text.Replace("'", "''"));
            }
            catch (Exception ex) { }
        }
    }
}
