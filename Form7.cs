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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ite5_finals
{
    public partial class Form7 : Form
    {
        public static Form7 instance;
        public TextBox tbName, tbID, prID, prName, prQty, prAmt, prPrice;

        public Form7()
        {
            InitializeComponent();
            label10.Text = DateTime.Now.ToString("dddd, MMM dd yyyy, hh:mm:ss");
            instance = this;
            tbName = txtName;
            tbID = txtID;
            prID = txtPrID;
            prName = txtPrName;
            prQty = txtquant;
            prAmt = txtAmt;
            prPrice = txtPrice;
            txtTransac.Enabled = txtID.Enabled = txtName.Enabled = txtPrID.Enabled = txtPrName.Enabled = txtAmt.Enabled = false;
            txtquant.Visible = false;
            txtPrice.Visible = false;
        }
        MySqlConnection con = new MySqlConnection("server = localhost; database = project_db; uid=root; pwd=root");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        private void Form7_Load(object sender, EventArgs e)
        {
           
        }

        private void btnCllient_Click(object sender, EventArgs e)
        {
            Form8 frmClient = new Form8();
            frmClient.Show();
        } 

        private void btnProd_Click(object sender, EventArgs e)
        {
            Form9 frmProd = new Form9();
            frmProd.Show(); 
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            double qty = 0, prQty = 0, price = 0;

            Double.TryParse(txtQty.Text, out qty);
            Double.TryParse(txtquant.Text, out prQty);

            if (qty > prQty)
            {
                MessageBox.Show("No enough stocks", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQty.Clear();
                txtAmt.Clear();
                return;
            }
            else
            {
                price = double.Parse(txtPrice.Text);
                double amt = qty * price;
                txtAmt.Text = amt.ToString("##,##0.00");
            } 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
            {
                MessageBox.Show("Fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQty.Focus();
                return;
            }
            dataGridView1.Rows.Add(txtPrName.Text, txtQty.Text, txtAmt.Text);

            double x = 0, y = 0, totQty = 0, totPch = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                x = Convert.ToDouble(r.Cells[1].Value);
                y = Convert.ToDouble(r.Cells[2].Value);
                totQty = totQty + x;
                totPch = totPch + y;
            }
            txtTotQty.Text = totQty.ToString();
            txtTotPch.Text = totPch.ToString("##,##0.00");
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" || txtID.Text == "" || txtPrID.Text == "" || txtQty.Text == "" || txtPrName.Text == "" || txtAmt.Text == "")
            {
                MessageBox.Show("Fill in all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Insert into sales (clientID, clientName , totPurchased, totQty, date) values (@Cid,@Cname,@tqty,@tpch,@date)";
            var cp = cmd.Parameters;
            cp.AddWithValue("Cid", txtID.Text.Trim());
            cp.AddWithValue("Cname", txtName.Text.Trim());
            cp.AddWithValue("tqty", txtQty.Text.Trim());
            cp.AddWithValue("tpch", txtTotPch.Text.Trim());
            cp.AddWithValue("date", label10.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();

            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < dataGridView1.RowCount; r++)
            {
                for (int c = 0; c < dataGridView1.ColumnCount; c++)
                {
                    if (dataGridView1[c, r].Value != null)
                    {
                        sb.Append(dataGridView1[c, r].Value.ToString());
                        sb.Append("\t");
                    }
                }
                sb.AppendLine();
            }
            richTextBox1.Text = richTextBox1.Text = "*** Transaction Bill ***" + "\nDate: " + label10.Text + "\nName: " + txtName.Text + "\n" + "\n*** Purchased Item ***" + "\n" + sb.ToString() + "\n\n" + "Total Quantity: " + txtTotQty.Text + "\nTotal Purchased: " + txtTotPch.Text;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Document dc = new Document();
            PdfWriter wr = PdfWriter.GetInstance(dc, new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Records\\" + "Receipt.pdf", FileMode.Create));
            dc.Open();
            dc.Add(new iTextSharp.text.Paragraph(richTextBox1.Text));
            dc.Close();
            txtID.Text = txtName.Text = txtPrID.Text = txtPrName.Text = txtQty.Text = txtAmt.Text = txtTotQty.Text = txtTotPch.Text = richTextBox1.Text = "";
            dataGridView1.Rows.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtID.Text = txtName.Text = txtPrID.Text = txtPrName.Text = txtQty.Text = txtAmt.Text = txtTotQty.Text = txtTotPch.Text = richTextBox1.Text = "";
            dataGridView1.Rows.Clear();
            return;
        }
    }
}
