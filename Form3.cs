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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from accounts", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        MySqlConnection con = new MySqlConnection("server = localhost; database = project_db; uid=root; pwd=root");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        private void Form3_Load(object sender, EventArgs e)
        {
            dataLoad();
        }
        void dataLoad()
        {
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from accounts", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var r = dataGridView1.CurrentRow;
            txtUsername.Text = r.Cells[1].Value.ToString();
            txtPass.Text = r.Cells[2].Value.ToString();
            cboUserType.Text = r.Cells[3].Value.ToString();
            btnSave.Text = "Update";
            txtUsername.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            if (txtUsername.Text == "" || txtPass.Text == "" || cboUserType.Text == "")
            {
                MessageBox.Show("Fill in all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            if (btnSave.Text == "Save")
            {
                if (txtUsername.Text != null)
                {
                    cmd.Connection = con;
                    cmd.CommandText = "Select * from accounts where user = '" + txtUsername.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Username already exists", "Error in Saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Text = "";
                        txtUsername.Focus();
                        con.Close();
                        return;
                    } con.Close();
                }
                con.Open();
                cmd.CommandText = "Insert into accounts (user, pass, type) values (@un,@pass,@type)";
                var c = cmd.Parameters;
                c.AddWithValue("un", txtUsername.Text.Trim());
                c.AddWithValue("pass", txtPass.Text.Trim());
                c.AddWithValue("type", cboUserType.Text.Trim());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();
                MessageBox.Show("Record successfully added", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Text = txtPass.Text = cboUserType.Text = "";
                dataLoad();
                txtUsername.Focus();
            }
            if (btnSave.Text == "Update")
            {
                cmd.Connection = con;
                cmd.CommandText = "Update accounts set user= @un, pass=@pass, type=@usertype where user= @un";
                var c = cmd.Parameters;
                c.Clear();
                c.AddWithValue("un", txtUsername.Text.Trim());
                c.AddWithValue("pass", txtPass.Text.Trim());
                c.AddWithValue("usertype", cboUserType.Text.Trim());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();
                MessageBox.Show("Record successfully updated", "updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Text = txtPass.Text = cboUserType.Text = "";
                dataLoad();
                txtUsername.Focus();
                btnSave.Text = "Save";
                txtUsername.Enabled = true;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("username must not be null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus(); return;

            }
            if (MessageBox.Show("Do you really want to delete this Record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Delete from accounts where user =@un";
                var c = cmd.Parameters;
                c.Clear();
                c.AddWithValue("un", txtUsername.Text.Trim());
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                con.Close();
                MessageBox.Show("Deleted", "Successfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Text = txtPass.Text = cboUserType.Text = "";
                dataLoad();
                txtUsername.Focus();
                btnSave.Text = "Save";
                txtUsername.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUsername.Text = txtPass.Text = cboUserType.Text = string.Empty;
            dataGridView1.SelectedCells[0].Selected = false;
            txtUsername.Focus();
            txtUsername.Enabled = true;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                e.Value = new String('*', e.Value.ToString().Length);
            }
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            string valsearch = txtSearch.Text.ToString();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from accounts where user LIKE '%" + txtSearch.Text + "%' ", con);
            MySqlDataAdapter dt = new MySqlDataAdapter(cmd);
            DataTable duser = new DataTable();
            dt.Fill(duser);
            dataGridView1.DataSource = duser;
            con.Close();
        }
    }
}
