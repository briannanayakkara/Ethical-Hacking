using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace EthicalHacking
{
    public partial class Form1 : Form
    {
        public string q,c;
        
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }


        void ShowList(String Tbl)
        {

            dataGridView1.DataSource = GetDT(Tbl);
        }
        public  DataTable GetDT(string sp) // attach to savedata
        {

                try
                {
                    using (var cnn = new SqlConnection(ConectionString.cnn(c)))
                    {

                        cnn.Open();
                        sp = @"select * from [source-1-server\dynamics].solotest.[dbo]." + sp;

                        q = sp;

                        SqlCommand cmd = new SqlCommand(q, cnn);
                        cmd.CommandTimeout = 60; // 1 min 
                        SqlDataReader sdr = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        dt.Load(sdr);
                        //wait.Close();

                        return dt;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text == "Monitoring")
            {
                ShowList(textBox1.Text);
                label1.Text = q;

            }
            else
                MessageBox.Show("Access denied");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                c = textBox2.Text;

                using (SqlConnection connection = new SqlConnection(c))
                {
                    try
                    {
                        connection.Open();
                        button1.Enabled = true;
                        textBox2.Clear();
                    }
                    catch (SqlException)
                    {
                        button1.Enabled = false;
                        textBox2.Clear();
                    }
                }
               
            }
        }
    }
}
