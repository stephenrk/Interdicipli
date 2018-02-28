using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClientNorthwind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var svc = new NorthwindServices.CustomerServiceClient();
            var customer = svc.GetById(textBox1.Text);
            textBox2.Text = customer.CompanyName;
            textBox3.Text = customer.ContactName;
            MessageBox.Show("OK");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var svc = new NorthwindServices.CustomerServiceClient();
            var customer = new NorthwindServices.Customer
            {
                CustomerID = textBox1.Text,
                CompanyName = textBox2.Text,
                ContactName = textBox3.Text

            };
            customer = svc.Save(customer);

            MessageBox.Show("OK");
        }
    }
}
