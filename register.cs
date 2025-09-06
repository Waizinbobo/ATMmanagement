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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\HP\\source\\repos\\ATMmanagement\\ATM.mdf;Integrated Security=True");

        private int GenerateAccountNo()
        {
            Random rnd = new Random();
            int accountNo = rnd.Next(10000000, 99999999); // 8-digit number
            return accountNo;
        }

        



        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int accno = Convert.ToInt32(txtAccNo.Text);
                long mobno = Convert.ToInt64(txtMobileNo.Text);


                string query = @"INSERT INTO Users (AccountNo, Pin, Name, MobileNo, Address, Birthdate) 
                                VALUES (@AccountNo, @Pin, @Name, @MobileNo, @Address, @Birthdate)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@AccountNo", accno);
                cmd.Parameters.AddWithValue("@Pin", txtPin.Text);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@MobileNo", mobno);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Birthdate", dtpBirthdate.Value.Date);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("User registered successfully!\nAccount No: " + accno, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                txtPin.Clear();
                txtName.Clear();
                txtMobileNo.Clear();
                txtAddress.Clear();
                dtpBirthdate.Value = DateTime.Now;

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Register_Load(object sender, EventArgs e)
        {
            int accno = GenerateAccountNo();
            txtAccNo.Text = accno.ToString();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide(); 
            Login loginForm = new Login(); 
            loginForm.Show();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
