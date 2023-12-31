﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Bankamatik_Simulasyon14
{
    public partial class Form3 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"");
        public string hesapno;
        public Form3()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Gonderilen();
            Gelen();
        }

        void Gonderilen()
        {
            SqlCommand cmd = new SqlCommand("execute GONDERILEN @DEGER=@PR1", baglanti);
            cmd.Parameters.AddWithValue("@PR1", hesapno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        void Gelen()
        {
            SqlCommand cmd = new SqlCommand("execute GELEN @DEGER=@PR2", baglanti);
            cmd.Parameters.AddWithValue("@PR2", hesapno);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }
    }
}
