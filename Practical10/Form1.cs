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


namespace Practical10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string gender="";
            string[] language = new string[10];
            int i = 0;

            if (radioButton1.Checked == true)
            {
                gender = radioButton1.Text;
            }

            if (radioButton2.Checked == true)
            {
                gender = radioButton2.Text;
            }

            if (checkBox1.CheckState == CheckState.Checked)
            {
                language[i] = checkBox1.Text;
                i = i + 1;
            }

            if (checkBox2.CheckState == CheckState.Checked)
            {
                language[i] = checkBox2.Text;
                i = i + 1;
            }

            if (checkBox3.CheckState == CheckState.Checked)
            {
                language[i] = checkBox3.Text;
                i = i + 1;
            }

            string s = "";
            for (int j = 0; j < i; j++)
            {
                if (j == i - 1)
                {
                    s = s + language[j];
                }
                else
                {
                    s = s + language[j]+",";
                }
            }

            SqlConnection c = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Riya\OneDrive\Documents\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            c.Open();
            string a = "insert into Data(Name,Enrollment_No,Email,Branch,Semester,Gender,Language,Contact_No) values ('" + tname.Text + "','" + tEnroll.Text + "','" + tEmail.Text + "','" + combo_branch.Text + "','" + combo_semester.Text + "','" + gender + "','" + s + "','" + contact.Text + "')";
            SqlCommand cmd = new SqlCommand(a, c);
           if (cmd.ExecuteNonQuery() != 0)
            {
                MessageBox.Show("Data successfully inserted");
            }

            else
            {
                MessageBox.Show("Please Enter the Data....");
            }
            c.Close();
            tname.Clear();
            tEnroll.Clear();
            tEmail.Clear();
            contact.Clear();
            combo_branch.Text = "";
            combo_semester.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection cn1 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Riya\OneDrive\Documents\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            cn1.Open();
            if (txt_Enroll.Text != "")
            {
                string a1 = "select * from Data where Enrollment_No='" + txt_Enroll.Text + "'";
                SqlCommand cmd = new SqlCommand(a1, cn1);
                SqlDataReader d = cmd.ExecuteReader();

                while (d.Read())
                {
                    tname.Text = d.GetValue(0).ToString();
                    tEnroll.Text = d.GetValue(1).ToString();
                    tEmail.Text = d.GetValue(7).ToString();
                    combo_branch.Text = d.GetValue(2).ToString();
                    combo_semester.Text = d.GetValue(3).ToString();
                    contact.Text = d.GetValue(6).ToString();
                    string gender1 = d.GetValue(4).ToString();

                    if (gender1.Equals("Male"))
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                }
            }
            else 
            {
                MessageBox.Show("Please Enter valid value so system can search it!");
            }
            cn1.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Riya\OneDrive\Documents\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            cn.Open();
            string a = "update Data SET Semester='" + combo_semester.Text + "',Contact_No='"+contact.Text+"' where Enrollment_No='"+txt_Enroll.Text+"'";
            SqlCommand cmd = new SqlCommand(a, cn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Updated Successfully!");
            cn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Riya\OneDrive\Documents\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            cn.Open();
            string a = "delete from Data where Enrollment_No='" + txt_Enroll.Text + "'";
            SqlCommand cmd = new SqlCommand(a, cn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Deleted Successfully!");
            cn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Riya\OneDrive\Documents\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            string a1 = "select * from Data where Enrollment_No='"+showdata.Text+"'";
            SqlDataAdapter SDA = new SqlDataAdapter(a1, cn);
            DataTable T = new DataTable();
            SDA.Fill(T);
            dataGridView1.DataSource = T;
            SDA.Update(T);
        }

       /* private void text_change(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Riya\OneDrive\Documents\Database1.mdf;Integrated Security=True;Connect Timeout=30");
            string a1 = "select * from Data where Enrollment_No='" + showdata.Text + "'";
            SqlDataAdapter SDA = new SqlDataAdapter(a1, cn);
            DataTable T = new DataTable();
            SDA.Fill(T);
            dataGridView1.DataSource = T;
            SDA.Update(T);
        }
        */
    }
}
