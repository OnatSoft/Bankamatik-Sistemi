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
    public partial class Form2 : Form
    {
        SqlConnection baglanti = new SqlConnection(@"");
        public string hesap;
        DialogResult secenek = new DialogResult();

        public Form2()
        {
            InitializeComponent();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            secenek = MessageBox.Show("Güvenli olarak sistemden çıkış yapılacaktır.\nOnaylıyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                Form1 frm1 = new Form1();
                frm1.Show();
                this.Close();
            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            //Gönderilen Hesabın Para Artışı
            baglanti.Close();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("update TBL_HESAP set BAKİYE=BAKİYE+@p1 where HESAPNUMARA=@p2", baglanti);
            cmd.Parameters.AddWithValue("@p1", decimal.Parse(txtTutar1.Text));
            cmd.Parameters.AddWithValue("@p2", msktxtHesapNo.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();

            //Gönderen Hesabın Para Azalışı
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("update TBL_HESAP set BAKİYE=BAKİYE-@k1 where HESAPNUMARA=@k2", baglanti);
            cmd2.Parameters.AddWithValue("@k1", decimal.Parse(txtTutar1.Text));
            cmd2.Parameters.AddWithValue("@k2", hesap);
            cmd2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Para Transferi Güvenli Bir Şekilde Başarıyla Gerçekleşmiştir.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HavaleGonder();
            BakiyeGetir();

            groupBox2.Visible = false;
            msktxtHesapNo.Clear();
            txtTutar1.Clear();
            txtAciklama.Clear();
        }

        void HavaleGonder()
        {
            baglanti.Close();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into TBL_HAREKET (GONDEREN,ALİCİ,TUTAR,TARİH,ACİKLAMA) values (@A1, @A2, @A3, @A4, @A5)", baglanti);
            cmd.Parameters.AddWithValue("@A1", hesap);
            cmd.Parameters.AddWithValue("@A2", msktxtHesapNo.Text);
            cmd.Parameters.AddWithValue("@A3", decimal.Parse(txtTutar1.Text));
            cmd.Parameters.AddWithValue("@A4", Convert.ToDateTime(lblTarihSaat.Text));
            cmd.Parameters.AddWithValue("@A5", txtAciklama.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }

        void BakiyeGetir()
        {
            //HesapNo'ya Göre Müşterinin Güncel Bakiyesini Getirme
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select BAKİYE from TBL_HESAP where HESAPNUMARA=@H1", baglanti);
            komut2.Parameters.AddWithValue("@H1", lblHesapNo1.Text);
            SqlDataReader dataReader = komut2.ExecuteReader();
            while (dataReader.Read())
            {
                lblBakiye.Text = dataReader[0] + " TL";
                lblBakiye2.Text = dataReader[0] + " TL";
            }
            baglanti.Close();
        }

        void Tarih()
        {
            DateTime dt = DateTime.Now;
            lblTarihSaat.Text = dt.ToString();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Tarih();
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            secenek = MessageBox.Show("Geçerli işlemi iptal etmek istiyor musunuz?", "Bilgi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (secenek == DialogResult.OK)
            {
                groupBox2.Visible = false;
            }
        }

        private void btnHareketler_Click(object sender, EventArgs e)
        {
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select HESAPNO=@hesapno From TBL_KİSİLER", baglanti);
            komut.Parameters.AddWithValue("@hesapno", hesap);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form3 frm3 = new Form3();
                frm3.hesapno = dr["HESAPNO"].ToString();
                frm3.Show();
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e) //DEVAM BUTONU
        {
            if (msktxtHesapNo.Text != "" && txtTutar1.Text != "" && txtAciklama.Text != "")
            {
                groupBox2.Visible = true;
                lblHesapReceiver.Text = msktxtHesapNo.Text;
                lblHesapSend.Text = lblHesapNo1.Text;
                lblTutar.Text = txtTutar1.Text;
                lblAciklama.Text = txtAciklama.Text;
            }
            else
            {
                MessageBox.Show("Havale işlemi yapmak için lütfen tüm alanları doldurunuz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            lblHesapNo1.Text = hesap;
            BakiyeGetir();
            Tarih();

            //HesapNo'ya Göre Giriş Yapan Müşterinin Bilgilerini Getirme
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBL_KİSİLER Where HESAPNO=@hesapno", baglanti);
            komut.Parameters.AddWithValue("@hesapno", lblHesapNo1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[1].ToString();
                lblTCKimlik.Text = dr[2].ToString();
                lblHesapNo1.Text = dr[5].ToString();
            }
            baglanti.Close();
        }

        private void btnPassDegistir_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.Show();
        }

        private void btnHakkinda_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu program EchoSoft tarafından MilletBank Bankamatik Sistemi için geliştirilmiştir.\nİletişim ve Destek için: https://osomerr.cf veya onat.somer2018@gmail.com\n----------------------------------------\nSürüm: 1.3.0\nCopyRight 2023 - 26.06.2023 - EchoSoft", "SİSTEM HAKKINDA");
        }
    }
}
