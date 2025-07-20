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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("server = localhost; database = project_db; uid=root; pwd=root");

        private void btnLogin_Click(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from accounts where user = @un and pass=@pw", con);
            var c = cmd.Parameters;
            c.Clear();
            c.AddWithValue("un", txtUser.Text.Trim());
            c.AddWithValue("pw", txtPass.Text.Trim());
            cmd.ExecuteNonQuery();
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr.GetString(3) == "admin")
                    {
                        MessageBox.Show("Succesfully LogIn", "System LogIn",MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Form2 frmMenu = new Form2();
                        frmMenu.Show();
                        this.Hide();
                        Form2.instance.lbl1.Text = " Welcome " + dr.GetString(1);
                    }
                    if (dr.GetString(3) == "user")
                    {
                        MessageBox.Show("Succesfully LogIn", "System LogIn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Form2 frmMenu = new Form2();
                        frmMenu.Show();
                        this.Hide();
                        Form2.instance.lbl1.Text = " Welcome " + dr.GetString(1);
                    }
                }
            }
            else 
            {
                MessageBox.Show("username and password mismatched", "System LogIn");
                txtUser.Text = txtPass.Text = "";
            }
            dr.Close();
            cmd.Dispose();
            con.Close();
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPass.UseSystemPasswordChar = true;
            }
            else 
            {
                txtPass.UseSystemPasswordChar = false;
            }
        }
    }
}
