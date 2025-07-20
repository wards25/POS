using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ite5_finals
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from stock", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        MySqlConnection con = new MySqlConnection("server = localhost; database = project_db; uid=root; pwd=root");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        private void Form9_Load(object sender, EventArgs e)
        {
            dataLoad();
        }
        void dataLoad()
        {
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from stock", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Form7.instance.prID.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Form7.instance.prName.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            Form7.instance.prQty.Text = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
            Form7.instance.prPrice.Text = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string valsearch = txtSearch.Text.ToString();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from stock where itemName LIKE '%" + txtSearch.Text + "%' ", con);
            MySqlDataAdapter dt = new MySqlDataAdapter(cmd);
            DataTable dtItem = new DataTable();
            dt.Fill(dtItem);
            dataGridView1.DataSource = dtItem;
            con.Close();
        }
    }
}
