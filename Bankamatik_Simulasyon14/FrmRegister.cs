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
    public partial class FrmRegister : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source=LAPTOP-ONATSOFT\ONATSOFT;Initial Catalog=BankamatikDB;Integrated Security=True");
        Random random = new Random();

        public FrmRegister()
        {
            InitializeComponent();
        }

        private void LnkGiris_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }
        bool durum;
        
        void mukerrer()
        {
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from TBL_KİSİLER where SİFRE=@P1 and HESAPNO=@P2", baglanti);
            komut.Parameters.AddWithValue("@P1", txtSifreTekrar.Text);
            komut.Parameters.AddWithValue("@P2", txtHesapno.Text);
            SqlDataReader oku = komut.ExecuteReader();
            if (oku.Read())
            {
                durum = false;
            }
            else
            {
                durum = true;
            }
            baglanti.Close();
        }

        private void btnKaydol_Click(object sender, EventArgs e)
        {
            mukerrer();

            baglanti.Close();
            baglanti.Open();

            if (durum == true)
            {
                if (txtAdSoyad.Text != "" && msktxtTCKimlik.Text != "" && txtEmail.Text != "" && msktxtTelefon.Text != "" && txtSifre.Text != "" && txtSifre.Text == txtSifreTekrar.Text && txtBakiye.Text != "" && label3.Text == txtCaptcha.Text)
                {
                    SqlCommand cmd = new SqlCommand("insert into TBL_KİSİLER (ADSOYAD,TCKİMLİK,TELEFON,EMAİL,HESAPNO,SİFRE) values (@PR1, @PR2, @PR3, @PR4, @PR5, @PR6)", baglanti);
                    cmd.Parameters.AddWithValue("@PR1", txtAdSoyad.Text);
                    cmd.Parameters.AddWithValue("@PR2", msktxtTCKimlik.Text);
                    cmd.Parameters.AddWithValue("@PR3", msktxtTelefon.Text);
                    cmd.Parameters.AddWithValue("@PR4", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@PR5", txtHesapno.Text);
                    cmd.Parameters.AddWithValue("@PR6", txtSifre.Text);
                    cmd.ExecuteNonQuery();

                    SqlCommand komut2 = new SqlCommand("insert into TBL_HESAP (HESAPNUMARA,BAKİYE) values (@k1,@k2)", baglanti);
                    komut2.Parameters.AddWithValue("@k1", txtHesapno.Text);
                    komut2.Parameters.AddWithValue("@k2", decimal.Parse(txtBakiye.Text));
                    komut2.ExecuteNonQuery();
                    MessageBox.Show("Başarıyla sisteme kayıt yapıldı. Giriş sayfasından T.C. Kimlik numaranızla ve Şifrenizle birlikte giriş yapabilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kayıt yapılamadı! Lütfen tüm alanları eksiksiz ve doğru girdiğinizden emin olunuz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seçtiğiniz hesap numarası veya şifre daha önce kayıtlara eklenmiş. Lütfen yeni bir hesap numarası ve yeni şifre seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            baglanti.Close();
        }

        Random rast = new Random();
        int[] sayi1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] sayi2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string hesapno = new string(Enumerable.Repeat("0123456789", 6).Select(r => r[random.Next(r.Length)]).ToArray());
            txtHesapno.Text = hesapno;
        }

        private void FrmRegister_Load(object sender, EventArgs e)
        {
            string hesapno = new string(Enumerable.Repeat("0123456789", 6).Select(r => r[random.Next(r.Length)]).ToArray());
            txtHesapno.Text = hesapno;

            int karakter1;
            int sembol1 = rast.Next(0, sayi1.Length);
            karakter1 = (sayi1[sembol1]);

            int karakter2;
            int sembol2 = rast.Next(0, sayi2.Length);
            karakter2 = (sayi2[sembol2]);
            int toplam = karakter1 + karakter2;
            lblSayiToplami.Text = karakter1 + " + " + karakter2 + " =";
            label11.Text = toplam.ToString();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtSifre.UseSystemPasswordChar == true)
            {
                txtSifre.UseSystemPasswordChar = false;
                txtSifreTekrar.UseSystemPasswordChar = false;
                linkLabel2.Text = "Gizle";
            }
            else
            {
                txtSifre.UseSystemPasswordChar = true;
                txtSifreTekrar.UseSystemPasswordChar = true;
                linkLabel2.Text = "Göster";
            }
        }
    }
}
