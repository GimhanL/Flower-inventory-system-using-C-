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

namespace order
{
    public partial class Form1 : Form
    {
        SqlConnection sqlCon;
        public Form1()
        {
            try
            {
                sqlCon = new SqlConnection(@"data Source=DESKTOP-K46VGV3\SQLEXPRESS;initial Catalog=order;  Integrated Security=True;");

                sqlCon.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Connecting" + ex, "Admin Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool valid = true;

                if (string.IsNullOrEmpty(password.Text) || string.IsNullOrEmpty(username.Text))
                {
                    MessageBox.Show("Need to fill all the values", "Login Form", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    valid = false;
                }

                if (valid)
                {
                    String UserType = usertype.Text.ToString().Trim();
                    SqlCommand cmd = new SqlCommand("select usertype from login where username = '" + username.Text.Trim() + "' and password = '" + password.Text.Trim() + "' ", sqlCon);


                    SqlDataReader dr = cmd.ExecuteReader();
                     
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            UserType = dr[0].ToString().Trim();
                         

                        }

                        if (UserType.Equals("Admin"))
                        {
                            admin1 obj = new admin1();
                            obj.Show();
                            this.Hide();
                            dr.Close();
                        }


                        else if (UserType.Equals("User"))
                        {
                            user2 obj = new user2();
                            obj.Show();
                            this.Hide();
                            dr.Close();
                        }
                      
                    }
                    else
                    {
                        MessageBox.Show("Invalid User", "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dr.Close();
                    }
                }



            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in adding data" + ex.Message, "Details Form", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

      

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            admin1 obj = new admin1();
            obj.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            user2 obj = new user2();
            obj.Show();
            this.Hide();
        }
    }
}