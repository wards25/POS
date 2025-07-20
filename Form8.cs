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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from client", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        MySqlConnection con = new MySqlConnection("server = localhost; database = project_db; uid=root; pwd=root");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        private void Form8_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Form7.instance.tbID.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Form7.instance.tbName.Text = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string valsearch = txtSearch.Text.ToString();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from client where name LIKE '%" + txtSearch.Text + "%' ", con);
            MySqlDataAdapter dt = new MySqlDataAdapter(cmd);
            DataTable dname = new DataTable();
            dt.Fill(dname);
            dataGridView1.DataSource = dname;
            con.Close();
        }
    }
}
