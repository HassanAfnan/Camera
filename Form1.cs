using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Data.SqlClient;


namespace imag1
{
    public partial class Form1 : Form
    {
        VideoCapture capture;
        public Form1()
        {
            InitializeComponent();
            Run();
        }
        private void Run()
        {
            try
            {
                capture = new VideoCapture();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return;
            }
            Application.Idle += ProcessFrame;
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            imageBox1.Image = capture.QuerySmallFrame();
            imageBox2.Image = capture.QuerySmallFrame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            panel1.Enabled = false ;
            imageBox1.Visible = false;
            imageBox2.Visible = false;
            imageBox3.Visible = false;
            imageBox4.Visible = false;


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                panel1.Enabled = true;
                imageBox2.Enabled = true;
               
            }
        }
        string imagelocation = "";
        private void button5_Click(object sender, EventArgs e)
        {
            imageBox3.Image = imageBox1.Image;
            imageBox3.Image.Save(@"C:\Users\User\Desktop\New folder\" + textBox1.Text + ".png");
            imagelocation = @"C:\Users\User\Desktop\New folder\" + textBox1.Text + ".png";
            imageBox3.Visible = true;
            imageBox1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            imageBox4.Image = imageBox2.Image;
            imageBox4.Visible = true;
            imageBox2.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imageBox1.Visible = true;
            imageBox3.Visible = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            imageBox2.Visible = true;
            imageBox4.Visible = false;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
            string query = "insert into itb values(@f,@m,@n,@p,@ni,@r,@i)";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@f", textBox1.Text);
            com.Parameters.AddWithValue("@m", textBox2.Text);
            com.Parameters.AddWithValue("@n", textBox3.Text);
            com.Parameters.AddWithValue("@p", textBox4.Text);
            com.Parameters.AddWithValue("@ni", textBox5.Text);
            com.Parameters.AddWithValue("@r", textBox6.Text);
            com.Parameters.AddWithValue("@i", imagelocation);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            MessageBox.Show("Successfully inserted");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = "";
            SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select img from itb ",con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                path = sdr["img"].ToString();
                
            }
            con.Close();
            imageBox3.Visible = true;
            imageBox3.ImageLocation = path;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=test;Integrated Security=True");
            string query = "update itb set mem=@m,name=@n,ph=@p,nic=@ni,room=@r,img=@i";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@m", textBox2.Text);
            com.Parameters.AddWithValue("@n", textBox3.Text);
            com.Parameters.AddWithValue("@p", textBox4.Text);
            com.Parameters.AddWithValue("@ni", textBox5.Text);
            com.Parameters.AddWithValue("@r", textBox6.Text);
            com.Parameters.AddWithValue("@i", imagelocation);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            MessageBox.Show("Successfully updated");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
 