using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;


namespace Library_Management_System
{
    public partial class Login : Form
    {
        
       
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Register_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
                panel2.Visible = false;
            else
                panel2.Visible = true;

        }

        private void Password_Click(object sender, EventArgs e)
        {

        }

        private void UserName_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Signup_Click(object sender, EventArgs e)
        {


            string name = textBox3.Text;
            string email = textBox4.Text;
            string password = textBox5.Text;
            string confirmpassword = textBox6.Text;

            Console.WriteLine(name + email + password + confirmpassword);




            //----------emailVerification
            if (!email.Contains("@") && !email.Contains(".com"))
            {

                Email.ForeColor = Color.Red;
            }
            else
            {
                Email.ForeColor = Color.Blue;
            }
            //----------UserNameVerification
            if (!System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[A-Z]"))
            {
                UserNameSignUp.ForeColor = Color.Red;
                MessageBox.Show("Please Use Both UpperCase and LowerCase");


            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[a-z]"))
            {
                UserNameSignUp.ForeColor = Color.Red;
                MessageBox.Show("Please Use Both UpperCase and LowerCase");

            }
            else
            {
                UserNameSignUp.ForeColor = Color.Blue;
            }
            //----------PasswordVerification
            if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[^a-zA-Z0-9]") && password != confirmpassword)
            {
                MessageBox.Show("Please Use  numbers,letters and special character and same password at both term ");
                Password_signup.ForeColor = Color.Red;
                confirn_password.ForeColor = Color.Red;


            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[^a-zA-Z0-9]") && password == confirmpassword)
            {
                MessageBox.Show("Please Use  numbers,letters and special character and same password at both term ");
                Password_signup.ForeColor = Color.Red;
                confirn_password.ForeColor = Color.Red;

            }
            else
            {
                Password_signup.ForeColor = Color.Blue;
                confirn_password.ForeColor = Color.Blue;
            }
            //----------DatabaseEntryVerification&Connection&ConfirmationMail
            if (System.Text.RegularExpressions.Regex.IsMatch(password, @"[^a-zA-Z0-9]") && password == confirmpassword && email.Contains("@") && email.Contains(".com") && name != null && System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[A-Z]") && System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, "[a-z]"))
            {
                SqlConnection connection = new SqlConnection("Data Source=DESKTOP-V5HB460\\SQLEXPRESS;Initial Catalog=Library Manager; Integrated Security=true");
                connection.Open();

                SqlCommand insertCommand = new SqlCommand(" insert into Login(username,email,password) values ('" + name + "','" + email + "','" + password + "') ", connection);

                insertCommand.ExecuteNonQuery();

               


            }

            else
            {
                MessageBox.Show("Follow Proper Instruction");
            }
            
           
        }

        private void Signin_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-V5HB460\\SQLEXPRESS;Initial Catalog=Library Manager; Integrated Security=true");
            connection.Open();

            SqlCommand selectCommand = new SqlCommand(" select * from Login where username = '" + username + "' and password ='" + password + "'", connection);


            SqlDataReader dataFromDb = selectCommand.ExecuteReader();
            if (dataFromDb.HasRows)
            {


                this.Hide();
                Home HomeFrom = new Home();
               HomeFrom.Show();

            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
          
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            clear();
        }
        void clear()
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

        }
    }
}
