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
    public partial class user2 : Form
    {
        SqlConnection sqlCon;
        public user2()
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

        private void user2_Load(object sender, EventArgs e)
        {

            try
            {
                SqlCommand cmd = new SqlCommand("select PID from product", sqlCon);
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
                        PID.Text = "P00" + CTR;
                    }
                    else if (CTR >= 10 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        PID.Text = "P0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        PID.Text = "P" + CTR;
                    }

                }
                else
                {
                    PID.Text = "P001";
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in generating  data" + ex, "Customer Form", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand cmdinsert = new SqlCommand("Insert into product values( '" + PID.Text.Trim() + "','" + Pname.Text.Trim() + "','" + Pdescription.Text.Trim() + "','" + Quantity.Text.Trim() + "' ,'" + Pprice.Text.Trim() + "','" + catagory1.Text.Trim() + "' )", sqlCon);

                cmdinsert.CommandType = CommandType.Text;
                cmdinsert.ExecuteNonQuery();
                MessageBox.Show("Item Added", "User form", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        private void Clear7_Click(object sender, EventArgs e)
        {
            PID.Text = "";
            Pname.Text = "";
            Pdescription.Text = "";
            Quantity.Text = "";
            Pprice.Text = "";
            catagory1.Text = "";

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("delete from product where PID = '" + PID.Text.ToString() + "'", sqlCon);

                int recordnum = cmd.ExecuteNonQuery();
                if (recordnum > 0)
                {
                    MessageBox.Show("Item deleted", "User form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid Item", "admin form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting data " + ex);

            }

        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update product set Name ='" + Pname.Text.Trim() + "',Description='" + Pdescription.Text.Trim() + "',Quantity='" + Quantity.Text.Trim() + "', Price ='" + Pprice.Text.Trim() + "',Catagory='" + catagory1.Text.Trim() + "'where PID= '" + PID.Text.Trim() + "'", sqlCon);

                int recordnum = cmd.ExecuteNonQuery();
                if (recordnum > 0)
                {
                    MessageBox.Show("Item updated", "admin form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid Item", "User form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting data " + ex);

            }


        }

        private void cmbProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                SqlCommand SearchCustomerNAmeCommand = new SqlCommand
                    ("select * from product where Catagory = @P_Name", sqlCon);

                SearchCustomerNAmeCommand.Parameters.Add("@P_Name", cmbProductCategory.Text.ToString());


                SqlDataAdapter da = new SqlDataAdapter(SearchCustomerNAmeCommand);
                DataSet ds = new DataSet();
                da.Fill(ds, "product");

                dgProduct.DataSource = ds;
                dgProduct.DataMember = "product";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error loading the product " + ex);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            user2 obj = new user2();
            obj.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            try
            {

                SqlCommand SelectCustomerNameCommand = new SqlCommand
                    ("Select distinct Catagory from product", sqlCon);

                SqlDataAdapter da = new SqlDataAdapter(SelectCustomerNameCommand);

                DataSet ds = new DataSet();

                da.Fill(ds, "product");

                cmbProductCategory.DataSource = ds;
                cmbProductCategory.DisplayMember = "product.Catagory";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading the category " + ex);

            }
        }
        private void dgProduct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PID.Text = dgProduct.SelectedRows[0].Cells[0].Value.ToString();
            Pname.Text = dgProduct.SelectedRows[0].Cells[1].Value.ToString();
            Pdescription.Text = dgProduct.SelectedRows[0].Cells[2].Value.ToString();
            Quantity.Text = dgProduct.SelectedRows[0].Cells[3].Value.ToString();
            Pprice.Text = dgProduct.SelectedRows[0].Cells[4].Value.ToString();
            catagory1.Text = dgProduct.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlCommand SearchCustomerNAmeCommand = new SqlCommand
                    ("select * from product where Catagory = @P_Name", sqlCon);

                SearchCustomerNAmeCommand.Parameters.Add("@P_Name", comboBox1.Text.ToString());


                SqlDataAdapter da = new SqlDataAdapter(SearchCustomerNAmeCommand);
                DataSet ds = new DataSet();
                da.Fill(ds, "product");

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "product";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error loading the product " + ex);

            }
        }

    

        private void update1_Click(object sender, EventArgs e)
        {
            try
            {
                if (updatetext.Text == "")
                {
                    MessageBox.Show("Enter Employee Id To Update");
                }
                else
                {

                    SqlCommand cmd = new SqlCommand("update product set Catagory='" + updatetext.Text.Trim() + "' where Catagory='" + comboBox1.Text.Trim() + "'", sqlCon);


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                   
                    MessageBox.Show("Category Updated", "User Form", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        private void del_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBox2.Text == "")
                {
                    MessageBox.Show("Enter Employee Id To Update");
                }

                else
                {

                    SqlCommand cmd = new SqlCommand("delete from product where Catagory='" + textBox2.Text.Trim() + "'", sqlCon);


                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    
                    MessageBox.Show("Category deleted", "Admin Form", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    sqlCon.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        private void clear4_Click(object sender, EventArgs e)
        {
            
            updatetext.Text = "";
            textBox2.Text = "";

        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            user2 obj = new user2();
            obj.Show();
            this.Hide();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
           
        }

        private void sid_Click(object sender, EventArgs e)
        {
            try
            {

           
                SqlCommand cmd = new SqlCommand("select Name,Quantity,Price,Catagory from product where PID = '" + PID2.Text.Trim() + "' ", sqlCon);


                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        pname2.Text = dr["Name"].ToString().Trim();
                        quan1.Text = dr["Quantity"].ToString().Trim();
                        price1.Text = dr["Price"].ToString().Trim();
                        cat2.Text = dr["Catagory"].ToString().Trim();
                        MessageBox.Show("Product Sucessfully Detected", "Admin Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        eq.ReadOnly = false;
                        total9.ReadOnly = false;




                    }
                    else
                    {

                        MessageBox.Show("Invalid Product ID", "User Form", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    }
                   
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in adding data" + ex.Message, "D Form", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }

        }

        private void sname1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("select PID,Quantity,Price,Catagory from product where Name = '" + pname2.Text.Trim() + "' ", sqlCon);


                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        PID2.Text = dr["PID"].ToString().Trim();
                        quan1.Text = dr["Quantity"].ToString().Trim();
                        price1.Text = dr["Price"].ToString().Trim();
                        cat2.Text = dr["Catagory"].ToString().Trim();
                        MessageBox.Show("Product Sucessfully Detected", "User Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        eq.ReadOnly = false;
                        total9.ReadOnly = false;
                        dr.Close();

                    }

                }

                else
                {
                    MessageBox.Show("Invalid Product Name", "User Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dr.Close();
                }



            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in adding data" + ex.Message, "Details Form", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }

        }

        private void total_Click(object sender, EventArgs e)
        {

            try
            {
                String val1 = quan1.Text.ToString();
                String val2 = eq.Text.ToString();
                int v1 = int.Parse(val1);
                int v2 = int.Parse(val2);

                if (v2 > v1)
                {
                    MessageBox.Show("Limited Stock Please Try Again", "Admin Form", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    eq.ReadOnly = false;
                }
                else
                {

                    String x = price1.Text.ToString();
                    String y = eq.Text.ToString();
                    int mul = int.Parse(x) * int.Parse(y);
                    total9.Text = mul.ToString();

                    String a = quan1.Text.ToString();
                    String b = eq.Text.ToString();
                    int min = int.Parse(a) - int.Parse(b);
                    astock.Text = min.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in adding data" + ex.Message, "Details Form", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
        }

        private void inovice_Click(object sender, EventArgs e)
        {
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("Insert into sells values( ' " + PID2.Text.Trim() + " ','" + pname2.Text.Trim() + "','" + eq.Text.Trim() + "','" + total9.Text.Trim() + "' ,'" + cat2.Text.Trim() + "','" + cname.Text.Trim() + "' )", sqlCon);


                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
              
                MessageBox.Show("Item Added", "admin form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        private void order_Click(object sender, EventArgs e)
        {
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("update product set Quantity='" + astock.Text.Trim() + "' where PID='" + PID2.Text.Trim() + "'", sqlCon);


                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Order Has been Placed");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        private void clear3_Click(object sender, EventArgs e)
        {
            cname.Text = "";
            PID2.Text = "";
            pname2.Text = "";
            cat2.Text = "";
            quan1.Text = "";
            price1.Text = "";
            eq.Text = "";
            total9.Text = "";
            astock.Text = "";
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                SqlCommand SearchCustomerNAmeCommand = new SqlCommand
                    ("select * from sells", sqlCon);



                SqlDataAdapter da = new SqlDataAdapter(SearchCustomerNAmeCommand);
                DataSet ds = new DataSet();
                da.Fill(ds, "sells");

                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "sells";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error loading the product " + ex);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            user2 obj = new user2();
            obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }

        private void PID_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            try
            {

                SqlCommand SelectCustomerNameCommand = new SqlCommand
                    ("Select distinct Catagory from product", sqlCon);

                SqlDataAdapter da = new SqlDataAdapter(SelectCustomerNameCommand);

                DataSet ds = new DataSet();

                da.Fill(ds, "product");

                comboBox1.DataSource = ds;
                comboBox1.DisplayMember = "product.Catagory";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading the category " + ex);

            }
        }
    }
}
