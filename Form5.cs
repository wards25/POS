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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from stock", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            txtID.Enabled = txtQty.Enabled = txtName.Enabled = txtDesc.Enabled = false;
            txtAdd.Focus();
        }
        MySqlConnection con = new MySqlConnection("server = localhost; database = project_db; uid=root; pwd=root");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        private void Form5_Load(object sender, EventArgs e)
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
            var r = dataGridView1.CurrentRow;
            txtID.Text = r.Cells[0].Value.ToString();
            txtName.Text = r.Cells[1].Value.ToString();
            txtDesc.Text = r.Cells[2].Value.ToString();
            txtQty.Text = r.Cells[3].Value.ToString();
            txtAdd.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtAdd.Text == "")
            {
                MessageBox.Show("Fill in all the fields", "Error"); return;
            }
            var c = cmd.Parameters;
            int qty = Convert.ToInt32(txtQty.Text) + Convert.ToInt32(txtAdd.Text);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Update stock set qty=@qty where itemID=@itemid";
            c.AddWithValue("qty", qty);
            c.AddWithValue("itemid", txtID.Text.Trim());
            MessageBox.Show("Stocks added", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd.ExecuteNonQuery();
            c.Clear();
            con.Close();
            dataLoad();
            txtAdd.Focus();
            txtAdd.Clear();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtID.Text = txtQty.Text = txtName.Text = txtDesc.Text = string.Empty;
            dataGridView1.SelectedCells[0].Selected = false;
            txtAdd.Focus();
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
