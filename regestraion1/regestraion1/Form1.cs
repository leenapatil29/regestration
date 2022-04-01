using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace regestraion1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection  con = new SqlConnection(@"Data Source=DESKTOP-7CN45UN\TEW_SQLEXPRESS;Initial Catalog=demo;Integrated Security=True");
        string g = String.Empty;
        public int id;
        void fillcombo()
        {

        }
        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Getrecord();

        }
        public void Getrecord()
        {
            
           SqlCommand cmd = new SqlCommand("select * from regtb",con);
            DataTable dt=new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt;
        }

        /*private void button5_Click(object sender, EventArgs e)
        {
            string imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter="jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    pictureBox1.ImageLocation =imageLocation;


                }
                        
            }
            catch (Exception)
            {
                MessageBox.Show("An error occur","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }*/

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select distinct(city) from city1", con);
            SqlDataReader dr = cmd1.ExecuteReader();
        while(dr.Read())
            {
                comboBox1.Items.Add(dr.GetValue(0).ToString());
            }
        dr.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)

        {
            if (Valid()) 
            {
                SqlCommand cmd = new SqlCommand("insert into regtb values(@name,@lname,@email,@contact,@gender,@dob,@city)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@lname", txtlname.Text);
                cmd.Parameters.AddWithValue("@email", txtemail.Text);
                cmd.Parameters.AddWithValue("@contact", txtcontact.Text);
                cmd.Parameters.AddWithValue("@gender",g);
                cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@city", comboBox1.Text);
               
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("new data is sucessfully saved ind database", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Getrecord();
                resetcontrol();
                txtlname.Focus();
            }

            
        }

        private bool Valid()
        {
            if (txtname.Text==string.Empty)
            {
                MessageBox.Show("Name is required", "faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            return true;

            
            



        }



        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked)
            {
                g="male";

            }
            else
                g="female";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(id>0)
            {
                SqlCommand cmd2 = new SqlCommand("update regtb SET name=@name,lname=@lname,email=@email,contact=@contact,gender=@gender,dob=@dob,city=@city where id=@id", con);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@name", txtname.Text);
                cmd2.Parameters.AddWithValue("@lname", txtlname.Text);
                cmd2.Parameters.AddWithValue("@email", txtemail.Text);
                cmd2.Parameters.AddWithValue("@contact", txtcontact.Text);
                cmd2.Parameters.AddWithValue("@gender", g);
                cmd2.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                cmd2.Parameters.AddWithValue("@city", comboBox1.Text);
                cmd2.Parameters.AddWithValue("@id", this.id);



                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("UPDATE", "update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Getrecord();
                resetcontrol();

            }
            else
            {
                MessageBox.Show("please select", "select", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetcontrol();
        }

        private void resetcontrol()
        {
            id=0;
            txtname.Clear();
            txtlname.Clear();
            txtemail.Clear();
            txtcontact.Clear();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id=Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            txtname.Text=dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtlname.Text=dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtemail.Text=dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtcontact.Text=dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            g=dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            dateTimePicker1.Text=dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

            comboBox1.Text=dataGridView1.SelectedRows[0].Cells[7].Value.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(id>0)
            {
                SqlCommand cmd2 = new SqlCommand("delete from regtb  where id=@id", con);
                cmd2.CommandType = CommandType.Text;
                
                cmd2.Parameters.AddWithValue("@id", this.id);



                con.Open();
                cmd2.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("delete data", "delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Getrecord();
                resetcontrol();

            }
            else
            {
                MessageBox.Show("please select", "DElete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

       

        private void txtcontact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled=true;
                MessageBox.Show("Error,a phono nuber can not contain letter");
            }
        }

        

      

        

        private void button5_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("select * from regtb where name='"+txtname.Text+"'", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 f2=new Form2();
            f2.Show();
            Visible=false;
        }
    }
    

   
}