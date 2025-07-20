using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ite5_finals
{
    public partial class Form2 : Form
    {
        public static Form2 instance;
        public Label lbl1;

        public Form2()
        {
            InitializeComponent(); 
            instance = this;
            lbl1 = label1;
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frmUserAcc = new Form3();
            frmUserAcc.Show();
        }

        private void manageClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 frmClient = new Form4();
            frmClient.Show();
        }

        private void addStocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frmAddStocks = new Form5();
            frmAddStocks.Show();
        }

        private void productsManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frmProductManage = new Form6();
            frmProductManage.Show();
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 frmSales = new Form7();
            frmSales.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form10 frmRead = new Form10();
            frmRead.Show(); 
        }
    }
}
