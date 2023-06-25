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
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-ONATSOFT\ONATSOFT;Initial Catalog=BankamatikDB;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void linkGosterGizle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtSifre.UseSystemPasswordChar == true)
            {
                txtSifre.UseSystemPasswordChar = false;
                linkGosterGizle.Text = "Gizle";
            }
            else
            {
                txtSifre.UseSystemPasswordChar = true;
                linkGosterGizle.Text = "Göster";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRegister frm3 = new FrmRegister();
            frm3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * From TBL_KİSİLER Where TCKİMLİK=@PR1 AND SİFRE=@PR2", baglanti);
            cmd.Parameters.AddWithValue("@PR1", msktxtTCKimlik.Text);
            cmd.Parameters.AddWithValue("@PR2", txtSifre.Text);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Form2 frm2 = new Form2();
                frm2.hesap = dr[5].ToString();
                frm2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Giriş bilgilerinizin doğru olduğundan emin olunuz.\nLütfen Tekrar Deneyin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            baglanti.Close();
        }
    }
}
