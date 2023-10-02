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
        SqlConnection baglanti = new SqlConnection(@"");

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
            string tckimlik = msktxtTCKimlik.Text;
            string sifre = txtSifre.Text;
            int count = 0;

            baglanti.Close();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("Select * From TBL_KİSİLER Where TCKİMLİK=@PR1 AND SİFRE=@PR2", baglanti);
            cmd.Parameters.AddWithValue("@PR1", msktxtTCKimlik.Text);
            cmd.Parameters.AddWithValue("@PR2", txtSifre.Text);
            SqlDataReader dr = cmd.ExecuteReader();

            if (label3.Text == txtCaptcha.Text)
            {
                if (dr.Read())
                {
                    Form2 frm2 = new Form2();
                    frm2.hesap = dr["HESAPNO"].ToString();
                    frm2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Giriş bilgilerinizin doğru olduğundan emin olunuz.\nTekrar Deneyiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSifre.Focus();
                }
            }
            else
            {
                MessageBox.Show("Sayıların toplamı yanlış olduğundan giriş başarısız!\nLütfen Tekrar Deneyiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCaptcha.Clear();
                txtCaptcha.Focus();
            }
            dr.Close();
            baglanti.Close();
        }

        Random rast = new Random();
        int[] sayi1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] sayi2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        private void Form1_Load(object sender, EventArgs e)
        {
            int karakter1;
            int sembol1 = rast.Next(0, sayi1.Length);
            karakter1 = (sayi1[sembol1]);

            int karakter2;
            int sembol2 = rast.Next(0, sayi2.Length);
            karakter2 = (sayi2[sembol2]);
            int toplam = karakter1 + karakter2;
            lblSayiToplami.Text = karakter1 + " + " + karakter2 + " =";
            label3.Text = toplam.ToString();
        }
    }
}
