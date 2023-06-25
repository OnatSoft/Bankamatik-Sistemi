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

        private void btnKaydol_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            if (txtAdSoyad.Text != "" && msktxtTCKimlik.Text != "" && txtEmail.Text != "" && msktxtTelefon.Text != "" && txtSifre.Text != "")
            {
                SqlCommand cmd = new SqlCommand("insert into TBL_KİSİLER (ADSOYAD,TCKİMLİK,TELEFON,EMAİL,HESAPNO,SİFRE) values (@PR1, @PR2, @PR3, @PR4, @PR5, @PR6)", baglanti);
                cmd.Parameters.AddWithValue("@PR1", txtAdSoyad.Text);
                cmd.Parameters.AddWithValue("@PR2", msktxtTCKimlik.Text);
                cmd.Parameters.AddWithValue("@PR3", msktxtTelefon.Text);
                cmd.Parameters.AddWithValue("@PR4", txtEmail.Text);
                cmd.Parameters.AddWithValue("@PR5", txtHesapno.Text);
                cmd.Parameters.AddWithValue("@PR6", txtSifre.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Başarıyla sisteme kayıt yapıldı. Giriş sayfasından T.C. Kimlik numaranızla ve Şifrenizle birlikte giriş yapabilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SqlCommand komut2 = new SqlCommand("insert into TBL_HESAP (HESAPNO,BAKİYE) values (@k1,@k2)", baglanti);
                komut2.Parameters.AddWithValue("@k1", txtHesapno.Text);
                komut2.Parameters.AddWithValue("@k2", decimal.Parse(txtBakiye.Text));
                komut2.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Kayıt yapılamadı. Lütfen gerekli tüm alanları doldurduğunuzdan emin olunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            baglanti.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string hesapno = new string(Enumerable.Repeat("0123456789", 6).Select(r => r[random.Next(r.Length)]).ToArray());
            txtHesapno.Text = hesapno;
        }

        private void FrmRegister_Load(object sender, EventArgs e)
        {
            string hesapno = new string(Enumerable.Repeat("0123456789", 6).Select(r => r[random.Next(r.Length)]).ToArray());
            txtHesapno.Text = hesapno;
        }
    }
}
