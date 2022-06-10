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
    public partial class register : Form
    {
        SqlConnection sqlCon;
        public register()
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (userID.Text == "" || Confpassword.Text == "" || Password.Text == "")
                {
                   
                    MessageBox.Show("All Fields Are Compulsory", "Register form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
           if (Confpassword.Text != Password.Text)
                {
                    MessageBox.Show("Password or Comfirm Password is not matched", "Register form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
           else
                {
                    SqlCommand cmdinsert = new SqlCommand("Insert into login values( '" +userID.Text.ToString().Trim()+ "','" +Confpassword.Text.ToString().Trim()+ "','" +userType.Text.ToString().Trim()+ "' )", sqlCon);
                    
                    cmdinsert.CommandType = CommandType.Text;
                    cmdinsert.ExecuteNonQuery();
                    MessageBox.Show("User Added", "Register form", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
                    sqlCon.Close();
                
            }

        }

        private void register_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select username from login", sqlCon);
                SqlDataReader dr = cmd.ExecuteReader();

                string ID = "";
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ID = dr[0].ToString().Trim();
                    }
                    dr.Close();

                    string idString = ID.Substring(1);
                    int CTR = Int32.Parse(idString);

                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        userID.Text = "C00" + CTR;
                    }
                    else if (CTR >= 10 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        userID.Text = "C0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        userID.Text = "C" + CTR;
                    }

                }
                else
                {
                    userID.Text = "C001";
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in generating  data" + ex, "Customer Form", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin1 obj = new admin1();
            obj.Show();
            this.Hide();
        }

        private void clear3_Click(object sender, EventArgs e)
        {
            userID.Text = "";
            Password.Text = "";
            Confpassword.Text = "";


        }

        private void userID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
    

