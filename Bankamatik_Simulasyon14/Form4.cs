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
    public partial class Form4 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"");

        public Form4()
        {
            InitializeComponent();
        }

        void PassChange()
        {

            baglanti.Close();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("update TBL_KİSİLER set SİFRE=@sifre where TCKİMLİK=@tckimlik", baglanti);
            cmd.Parameters.AddWithValue("@sifre", txtYeniSifre2.Text);
            cmd.Parameters.AddWithValue("@tckimlik", msktxtTCNo.Text);
            
            if (msktxtTCNo.Text != "" && txtEskiSifre.Text != "" && txtYeniSifre1.Text != "" && txtYeniSifre2.Text != "")
            {
                if (txtYeniSifre1.Text == txtYeniSifre2.Text)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bankamıza ait İnternet Bankacılığı şifreniz başarıyla güncellenmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Yeni şifreler birbirleriyle uyuşmamaktadır!\nLütfen doğru girdiğinizden emin olunuz.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtYeniSifre1.Focus();
                }
            }
            else
            {
                MessageBox.Show("Lütfen T.C. Kimlik numaranız dahil tüm alanları eksiksiz ve doğru şekilde giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                msktxtTCNo.Focus();
            }
            baglanti.Close();


            //baglanti.Close();
            //baglanti.Open();
            //SqlCommand cmd2 = new SqlCommand("select TCKİMLİK=@pr1,SİFRE=@pr2 from TBL_KİSİLER", baglanti);
            //cmd2.Parameters.AddWithValue("@pr1", msktxtTCNo.Text);
            //cmd2.Parameters.AddWithValue("@pr2", txtEskiSifre.Text);
            //baglanti.Close();
        }

        private void linklblGosterGizle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtYeniSifre1.UseSystemPasswordChar == true && txtYeniSifre2.UseSystemPasswordChar == true)
            {
                txtYeniSifre1.UseSystemPasswordChar = false;
                txtYeniSifre2.UseSystemPasswordChar = false;
                linklblGosterGizle.Text = "Gizle";
            }
            else
            {
                txtYeniSifre1.UseSystemPasswordChar = true;
                txtYeniSifre2.UseSystemPasswordChar = true;
                linklblGosterGizle.Text = "Göster";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            PassChange();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }
    }
}
