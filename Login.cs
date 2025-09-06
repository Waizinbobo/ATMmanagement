using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ATMmanagement
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\source\\repos\\ATMmanagement\\ATM.mdf;Integrated Security=True");

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            register reg = new register();
            reg.Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                
                int accountNo = Convert.ToInt32(txtAccNo.Text);
                int pin = Convert.ToInt32(txtPin.Text);

                
                string query = "SELECT COUNT(*) FROM Users WHERE AccountNo=@AccountNo AND Pin=@Pin";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AccountNo", accountNo);
                cmd.Parameters.AddWithValue("@Pin", pin);

                conn.Open();
                int count = (int)cmd.ExecuteScalar(); // matching row count
                conn.Close();

                if (count > 0)
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    this.Hide();
                    MainForm mainForm = new MainForm(); 
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Account No or PIN!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
