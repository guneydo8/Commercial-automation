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

namespace TicariOtomasyon2
{
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select TblFaturaBilgi.Id,SeriNo,SıraNo,Tarih,Saat,TblFaturaBilgi.VergiDaire,TblFirmalar.Ad as'Firma Adı',TblPersoneller.Ad+ ' ' +Soyad as 'Teslim Düşen',YetkiliAdSoyad as'Firma Yetkili' from TblFaturaBilgi inner join TblPersoneller on TblFaturaBilgi.TeslimEden=TblPersoneller.Id inner join TblFirmalar on TblFaturaBilgi.Alıcıfirma=TblFirmalar.Id ", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void personel()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TblPersoneller", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbpersonel.ValueMember = "Id";
            cmbpersonel.DisplayMember = "Ad";
            cmbpersonel.DataSource = dt;

        }

        void Firma()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TblFirmalar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbalıcı.ValueMember = "Id";
            cmbalıcı.DisplayMember = "Ad";
            cmbalıcı.DataSource = dt;
        }

        void ürün()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Id,ÜrünAd from TblÜrünler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbürün.DisplayMember = "ÜrünAd";
            cmbürün.ValueMember = "Id";
            cmbürün.DataSource = dt;
        }

        void fatura()
        {
            SqlCommand komut = new SqlCommand("select Id from TblFaturaBilgi", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbfatura.Items.Add(dr[0].ToString());
            }
            bgl.baglanti().Close();
        }
        void ürünfiyat()
        {
            SqlCommand komut = new SqlCommand("select SatışFiyat from TblÜrünler where ÜrünAd=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbürün.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtfiyat.Text = dr[0].ToString();

            }
            bgl.baglanti().Close();



        }

        void faturadetay()
        {
            SqlDataAdapter da = new SqlDataAdapter("select TblFaturaDetay.Id,ÜrünAd,Miktar,Fiyat,ToplamFiyat,Fatura from TblFaturaDetay inner join TblÜrünler on TblFaturaDetay.Ürün=TblÜrünler.Id where Fatura=" + txtid.Text, bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;

        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            personel();
            Firma();
            ürün();
            fatura();
            //faturadetay();

        }

        private void cmbalıcı_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Tblfirmalar where Id=" + cmbalıcı.SelectedValue, bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbyetkili.DisplayMember = "YetkiliAdSoyad";
            cmbyetkili.ValueMember = "Id";
            cmbyetkili.DataSource = dt;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblFaturaBilgi(SeriNo,SıraNo,Tarih,Saat,VergiDaire,AlıcıFirma,TeslimEden,TeslimAlan) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtseri.Text);
            komut.Parameters.AddWithValue("@p2", txtsıra.Text);
            komut.Parameters.AddWithValue("@p3", txttarih.Text);
            komut.Parameters.AddWithValue("@p4", txtsaat.Text);
            komut.Parameters.AddWithValue("@p5", txtvergi.Text);
            komut.Parameters.AddWithValue("@p6", cmbalıcı.SelectedValue);
            komut.Parameters.AddWithValue("@p7", cmbpersonel.SelectedValue);
            komut.Parameters.AddWithValue("@p8", cmbyetkili.SelectedValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Fatura Bilgisi Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtid.Text = gridView1.GetFocusedRowCellValue("Id").ToString();

            txtseri.Text = gridView1.GetFocusedRowCellValue("SeriNo").ToString();
            txtsıra.Text = gridView1.GetFocusedRowCellValue("SıraNo").ToString();
            txttarih.Text = gridView1.GetFocusedRowCellValue("Tarih").ToString();
            txtsaat.Text = gridView1.GetFocusedRowCellValue("Saat").ToString();
            txtvergi.Text = gridView1.GetFocusedRowCellValue("VergiDaire").ToString();
            cmbalıcı.Text = gridView1.GetFocusedRowCellValue("Firma Adı").ToString();
            cmbpersonel.Text = gridView1.GetFocusedRowCellValue("Teslim Düşen").ToString();
            cmbyetkili.Text = gridView1.GetFocusedRowCellValue("Firma Yetkili").ToString();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TblFaturaBilgi where Id=" + txtid.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            SqlCommand komut2 = new SqlCommand("update TblÜrünler set adet=adet+@p1 where ÜrünAd=@p2", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", numericUpDown1.Value);
            komut2.Parameters.AddWithValue("@p2", cmbürün.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Fatura Bilgisi Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TblFaturaBilgi set SeriNo=@p1,SıraNo=@p2,tarih=@p3,saat=@p4,vergiDaire=@p5,Alıcıfirma=@p6,teslimeden=@p7,teslimalan=@p8 where ıd=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtseri.Text);
            komut.Parameters.AddWithValue("@p2", txtsıra.Text);
            komut.Parameters.AddWithValue("@p3", txttarih.Text);
            komut.Parameters.AddWithValue("@p4", txtsaat.Text);
            komut.Parameters.AddWithValue("@p5", txtvergi.Text);
            komut.Parameters.AddWithValue("@p6", cmbalıcı.SelectedValue);
            komut.Parameters.AddWithValue("@p7", cmbpersonel.SelectedValue);
            komut.Parameters.AddWithValue("@p8", cmbyetkili.SelectedValue);
            komut.Parameters.AddWithValue("@p9", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtid.Text = "";
            txtsaat.Text = "";
            txttarih.Text = "";
            txtseri.Text = "";
            txtvergi.Text = "";
            txtsıra.Text = "";
            cmbalıcı.Text = "";
            cmbpersonel.Text = "";
            cmbyetkili.Text = "";
            txtseri.Focus();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {



            SqlCommand komut = new SqlCommand("insert into TblFaturaDetay(Ürün,Miktar,Fiyat,Toplamfiyat,Fatura) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbürün.SelectedValue);
            komut.Parameters.AddWithValue("@p2", int.Parse(numericUpDown1.Value.ToString()));
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtfiyat.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(toplamfiyat.ToString()));
            komut.Parameters.AddWithValue("@p5", cmbfatura.Text);
            komut.ExecuteNonQuery();
           
            bgl.baglanti().Close();
            SqlCommand komut3 = new SqlCommand("update TblÜrünler set adet=adet-@p1 where ÜrünAd=@p2", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", numericUpDown1.Value);
            komut3.Parameters.AddWithValue("@p2", cmbürün.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();




            SqlCommand komut2 = new SqlCommand("insert into TblFirmaSatış(Ürün,Personel,AlıcıFirma,Adet,Fiyat,Toplam,Tarih,Notlar) values (@q1,@q2,@q3,@q4,@q5,@q6,@q7,@q8)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@q1", cmbürün.SelectedValue);
            komut2.Parameters.AddWithValue("@q2", cmbpersonel.SelectedValue);
            komut2.Parameters.AddWithValue("@q3", cmbalıcı.SelectedValue);
            komut2.Parameters.AddWithValue("@q4", int.Parse(numericUpDown1.Value.ToString()));
            komut2.Parameters.AddWithValue("@q5", decimal.Parse(txtfiyat.Text));
            komut2.Parameters.AddWithValue("@q6", decimal.Parse(txttoplamfiyat.Text));
            komut2.Parameters.AddWithValue("@q7", txttarih.Text);
            komut2.Parameters.AddWithValue("@q8", "Faturalı Satış");
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Faturaya Ürün Eklemesi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            faturadetay();





        }
        double toplamfiyat = 0, fiyat;
        int miktar;


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            faturadetay();
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtfaturadetayıd.Text = gridView2.GetFocusedRowCellValue("Id").ToString();
            cmbfatura.Text = gridView2.GetFocusedRowCellValue("Fatura").ToString();
            cmbürün.Text = gridView2.GetFocusedRowCellValue("ÜrünAd").ToString();
            txtfiyat.Text = gridView2.GetFocusedRowCellValue("Fiyat").ToString();
            numericUpDown1.Value = int.Parse(gridView2.GetFocusedRowCellValue("Miktar").ToString());
            txttoplamfiyat.Text = gridView2.GetFocusedRowCellValue("ToplamFiyat").ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tblFaturaDetay set Ürün=@p1,Miktar=@p2,Fiyat=@p3,ToplamFiyat=@p4,Fatura=@p5 where Id=@p6", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", cmbürün.SelectedValue);
            komut.Parameters.AddWithValue("@p2", int.Parse(numericUpDown1.Value.ToString()));
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtfiyat.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(toplamfiyat.ToString()));
            komut.Parameters.AddWithValue("@p5", cmbfatura.Text);
            komut.Parameters.AddWithValue("@p6", txtfaturadetayıd.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Faturaya Ürün Güncellemesi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            faturadetay();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TblFaturaDetay where Id=" + txtfaturadetayıd.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Faturadan Ürün Silinme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            faturadetay();

        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            txtfaturadetayıd.Text = "";
            cmbfatura.Text = "";
            cmbürün.Text = "";
            txtfiyat.Text = "0";
            txttoplamfiyat.Text = "0";
            numericUpDown1.Value = 0;
            cmbfatura.Focus();
        }

        private void cmbürün_SelectedIndexChanged(object sender, EventArgs e)
        {
            ürünfiyat();

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

            fiyat = Convert.ToDouble(txtfiyat.Text);
            miktar = Convert.ToInt32(numericUpDown1.Value);

            toplamfiyat = fiyat * miktar;
            txttoplamfiyat.Text = toplamfiyat.ToString();
        }
    }
}
