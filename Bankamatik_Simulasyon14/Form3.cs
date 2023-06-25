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

namespace Bankamatik_Simulasyon14
{
    public partial class Form3 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-ONATSOFT\ONATSOFT;Initial Catalog=BankamatikDB;Integrated Security=True");
        public string hesapno;
        public Form3()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
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
