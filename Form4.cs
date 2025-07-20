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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            txtId.Enabled = false;
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

        private void Form4_Load(object sender, EventArgs e)
        {
            dataLoad();
        }
        void dataLoad()
        {
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from client", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var r = dataGridView1.CurrentRow;
            txtName.Text = r.Cells[1].Value.ToString();
            txtAddress.Text = r.Cells[2].Value.ToString();
            txtContact.Text = r.Cells[3].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            if (txtName.Text == "" || txtAddress.Text == "" || txtContact.Text == "")
            {
                MessageBox.Show("Fill in all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            if (btnSave.Text == "Save")
            {
                if (txtName.Text != null)
                {
                    cmd.Connection = con;
                    cmd.CommandText = "Select * from client where name = '" + txtName.Text + "' ";
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
                cmd.CommandText = "Insert into client (name, address, contact) values (@name,@address,@contact)";
                var c = cmd.Parameters;
                c.AddWithValue("name", txtName.Text.Trim());
                c.AddWithValue("address", txtAddress.Text.Trim());
                c.AddWithValue("contact", txtContact.Text.Trim());
                cmd.ExecuteNonQuery();
                c.Clear();
                con.Close();
                MessageBox.Show("Record successfully added", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Text = txtAddress.Text = txtContact.Text = "";
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
                cmd.CommandText = "Delete from client where name =@name";
                var c = cmd.Parameters;
                c.Clear();
                c.AddWithValue("name", txtName.Text.Trim());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();
                MessageBox.Show("Deleted", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Text = txtAddress.Text = txtContact.Text = "";
                dataLoad();
                txtName.Focus();

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = txtAddress.Text = txtContact.Text = string.Empty;
            dataGridView1.SelectedRows[0].Selected = false;
            txtName.Focus();
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
