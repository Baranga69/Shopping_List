using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shopping_List
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Data Source=B1T3M3\\SQLEXPRESS;Initial Catalog=Personal_Shopping_List;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(ConnectionString);

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into shopping_List values ('"+(textBox1.Text)+"', '"+(textBox2.Text)+"', '"+int.Parse(textBox3.Text)+ "','" + int.Parse(textBox4.Text) + "', getdate())", conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Successfully Inserted");
            conn.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();
            BindData();
        }

        void BindData()
        {
            SqlCommand cmd = new SqlCommand("select * from shopping_List", conn);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("update shopping_List set ItemName = '" + textBox1.Text + "', ItemQuantity = '" + textBox2.Text + "', ItemPrice = '" + textBox3.Text + "', ItemUnits = '" + textBox4.Text + "' where ItemID = '" + int.Parse(textBox5.Text) + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Successfully Updated");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                if (MessageBox.Show("Are you sure you want to delete?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Delete shopping_List where ItemID= '" + int.Parse(textBox5.Text) + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Successfully deleted!");
                    textBox5.Text = "";
                    BindData();
                }

            }
            else
            {
                MessageBox.Show("Input Item ID");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from shopping_List where ItemID = '" + int.Parse(textBox5.Text) + "'", conn);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
