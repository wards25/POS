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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from stock", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            txtID.Enabled = false;
        }

        MySqlConnection con = new MySqlConnection("server = localhost; database = project_db; uid=root; pwd=root");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        private void Form6_Load(object sender, EventArgs e)
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
            txtName.Text = r.Cells[1].Value.ToString();
            txtDesc.Text = r.Cells[2].Value.ToString();
            txtQty.Text = r.Cells[3].Value.ToString();
            txtPrice.Text = r.Cells[4].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            if (txtName.Text == "" || txtDesc.Text == "" || txtPrice.Text == "" || txtQty.Text == "")
            {
                MessageBox.Show("Fill in all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            if (btnSave.Text == "Save")
            {
                if (txtName.Text != null)
                {
                    cmd.Connection = con;
                    cmd.CommandText = "Select * from stock where itemName = '" + txtName.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Username already exists", "Error in Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtName.Text = "";
                        txtName.Focus();
                        con.Close();
                        return;
                    } con.Close();
                }
                con.Open();
                cmd.CommandText = "Insert into stock (itemName, des , qty, price) values (@in,@des,@qty,@price)";
                var c = cmd.Parameters;
                c.AddWithValue("in", txtName.Text.Trim());
                c.AddWithValue("des", txtDesc.Text.Trim());
                c.AddWithValue("qty", txtQty.Text.Trim());
                c.AddWithValue("price", txtPrice.Text.Trim());
                cmd.ExecuteNonQuery();
                c.Clear();
                con.Close();
                MessageBox.Show("Record successfully added", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Text = txtDesc.Text = txtPrice.Text = txtQty.Text = "";
                dataLoad();
                txtName.Focus();
            } 
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("Name must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus(); return;

            }
            if (MessageBox.Show("Do you really want to delete this Record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Delete from stock where itemName =@in";
                var c = cmd.Parameters;
                c.Clear();
                c.AddWithValue("in", txtName.Text.Trim());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();
                MessageBox.Show("Deleted", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Text = txtDesc.Text = txtPrice.Text = txtQty.Text = "";
                dataLoad();
                txtName.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = txtDesc.Text = txtPrice.Text = txtQty.Text = string.Empty;
            dataGridView1.SelectedCells[0].Selected = false;
            txtName.Focus();
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
