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
    public partial class FrmSatıslar : Form
    {
        public FrmSatıslar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void ürünler()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Id,ÜrünAd from  TblÜrünler where marka='"+comboBox1.Text+"'", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbfürün.ValueMember = "Id";
            cmbfürün.DisplayMember = "ÜrünAd";
            cmbfürün.DataSource = dt;
           
        }
        void ürünlermüşteri()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Id,ÜrünAd from  TblÜrünler where marka='" + comboBox2.Text + "'", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
           
            cmburun.ValueMember = "Id";
            cmburun.DisplayMember = "ÜrünAd";
            cmburun.DataSource = dt;
        }

        void marka()
        {
            SqlDataAdapter da = new SqlDataAdapter("select distinct(marka) from TblÜrünler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "marka";
            comboBox1.DataSource = dt;
            comboBox2.DisplayMember = "marka";
            comboBox2.DataSource = dt;
        }

        void personeller()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Id,Ad+' '+Soyad as 'Personeller'from TblPersoneller", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbpersonel.ValueMember = "Id";
            cmbpersonel.DisplayMember = "Personeller";
            cmbpersonel.DataSource = dt;
            cmbfpersonel.ValueMember = "Id";
            cmbfpersonel.DisplayMember = "Personeller";
            cmbfpersonel.DataSource = dt;
        }

        void Firmalar()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Id,Ad from Tblfirmalar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbfirmalar.ValueMember = "Id";
            cmbfirmalar.DisplayMember = "Ad";
            cmbfirmalar.DataSource = dt;
        }

        void müsteriler()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Id,Ad+' '+Soyad as 'Müşteriler'from TblMüşteriler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbmüşteri.ValueMember = "Id";
            cmbmüşteri.DisplayMember = "Müşteriler";
            cmbmüşteri.DataSource = dt;
        }

        void Firmafiyat()
        {
            SqlCommand komut = new SqlCommand("select SatışFiyat from TblÜrünler where Id=" + cmbfürün.SelectedValue, bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtfiyat.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void MüşteriFiyat()
        {
            SqlCommand komut = new SqlCommand("select SatışFiyat from TblÜrünler where Id=" + cmburun.SelectedValue, bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtfıyat.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void firmaliste()
        {
            SqlDataAdapter da = new SqlDataAdapter("exec FirmaSatış", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void Müsteriliste()
        {
            SqlDataAdapter da = new SqlDataAdapter("exec MüşteriSatış", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }
        private void FrmSatıslar_Load(object sender, EventArgs e)
        {
            
            personeller();
            Firmalar();
            müsteriler();
            firmaliste();
            Müsteriliste();
            marka();

        }

        private void cmbfürün_SelectedIndexChanged(object sender, EventArgs e)
        {
            Firmafiyat();
        }

        private void cmburun_SelectedIndexChanged(object sender, EventArgs e)
        {
            MüşteriFiyat();
        }

        double fiyat, miktar, toplam;

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblFirmaSatış(Ürün,Personel,AlıcıFirma,Adet,Fiyat,Toplam,Tarih,Notlar) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbfürün.SelectedValue);
            komut.Parameters.AddWithValue("@p2", cmbfpersonel.SelectedValue);
            komut.Parameters.AddWithValue("@p3", cmbfirmalar.SelectedValue);
            komut.Parameters.AddWithValue("@p4", int.Parse(nudadet.Value.ToString()));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtfiyat.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txttoplamfiyat.Text));
            komut.Parameters.AddWithValue("@p7", txttarihl.Text);
            komut.Parameters.AddWithValue("@p8", richTextBox1.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            SqlCommand komut2 = new SqlCommand("update TblÜrünler set adet=adet-@p1 where ÜrünAd=@p2", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", nudadet.Value);
            komut2.Parameters.AddWithValue("@p2", cmbfürün.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Satış İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmaliste();


            

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtıd.Text = gridView1.GetFocusedRowCellValue("FirmaSatışId").ToString();
            cmbfpersonel.Text = gridView1.GetFocusedRowCellValue("Personeller").ToString();
            cmbfirmalar.Text = gridView1.GetFocusedRowCellValue("Firma Adı").ToString();
            cmbfürün.Text = gridView1.GetFocusedRowCellValue("ÜrünAd").ToString();
            txtfiyat.Text = gridView1.GetFocusedRowCellValue("Fiyat").ToString();
            txttoplamfiyat.Text = gridView1.GetFocusedRowCellValue("Toplam").ToString();
            nudadet.Value = int.Parse(gridView1.GetFocusedRowCellValue("Adet").ToString());
            txttarihl.Text = gridView1.GetFocusedRowCellValue("Tarih").ToString();
            richTextBox1.Text = gridView1.GetFocusedRowCellValue("Notlar").ToString();



        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TblFirmaSatış where FirmaSatışId=" + txtıd.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            SqlCommand komut2 = new SqlCommand("update TblÜrünler set adet=adet+@p1 where ÜrünAd=@p2", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", nudadet.Value);
            komut2.Parameters.AddWithValue("@p2", cmbfürün.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Satış Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmaliste();

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TblFirmaSatış set Ürün=@p1,Personel=@p2,AlıcıFirma=@p3,Adet=@p4,Fiyat=@p5,Toplam=@p6,Tarih=@p7,Notlar=@p8 where FirmaSatışId=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbfürün.SelectedValue);
            komut.Parameters.AddWithValue("@p2", cmbfpersonel.SelectedValue);
            komut.Parameters.AddWithValue("@p3", cmbfirmalar.SelectedValue);
            komut.Parameters.AddWithValue("@p4", int.Parse(nudadet.Value.ToString()));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtfiyat.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txttoplamfiyat.Text));
            komut.Parameters.AddWithValue("@p7", txttarihl.Text);
            komut.Parameters.AddWithValue("@p8", richTextBox1.Text);
            komut.Parameters.AddWithValue("@p9", txtıd.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Satış Güncelleme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmaliste();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            cmbfürün.Text= "";
            cmbfpersonel.Text = "";
            cmbfirmalar.Text = "";
            nudadet.Value = 0;
            txtfiyat.Text = "0";
            txttoplamfiyat.Text = "";
            txttarihl.Text = "";
            richTextBox1.Text = "";
            txtıd.Text = "";
            cmbfürün.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblMüşteriSatış(Ürün,Personel,Müşteri,Adet,Fiyat,Toplam,Tarih,Notlar) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmburun.SelectedValue);
            komut.Parameters.AddWithValue("@p2", cmbpersonel.SelectedValue);
            komut.Parameters.AddWithValue("@p3", cmbmüşteri.SelectedValue);
            komut.Parameters.AddWithValue("@p4", int.Parse(numadet.Value.ToString()));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtfıyat.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txttoplam.Text));
            komut.Parameters.AddWithValue("@p7", txttarih.Text);
            komut.Parameters.AddWithValue("@p8", rchnot.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            SqlCommand komut2 = new SqlCommand("update TblÜrünler set adet=adet-@p1 where ÜrünAd=@p2", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", numadet.Value);
            komut2.Parameters.AddWithValue("@p2", cmburun.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Satış İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Müsteriliste();

        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            textEdit8.Text = gridView2.GetFocusedRowCellValue("Id").ToString();
            cmbpersonel.Text = gridView2.GetFocusedRowCellValue("Personeller").ToString();
            cmbmüşteri.Text = gridView2.GetFocusedRowCellValue("Müşteriler").ToString();
            cmburun.Text = gridView2.GetFocusedRowCellValue("ÜrünAd").ToString();
            txtfıyat.Text = gridView2.GetFocusedRowCellValue("Fiyat").ToString();
            txttoplam.Text = gridView2.GetFocusedRowCellValue("Toplam").ToString();
            numadet.Value = int.Parse(gridView2.GetFocusedRowCellValue("Adet").ToString());
            txttarih.Text = gridView2.GetFocusedRowCellValue("Tarih").ToString();
            rchnot.Text = gridView2.GetFocusedRowCellValue("Notlar").ToString();

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TblMüşteriSatış set Ürün=@p1,Personel=@p2,Müşteri=@p3,Adet=@p4,Fiyat=@p5,Toplam=@p6,Tarih=@p7,Notlar=@p8 where Id=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmburun.SelectedValue);
            komut.Parameters.AddWithValue("@p2", cmbpersonel.SelectedValue);
            komut.Parameters.AddWithValue("@p3", cmbmüşteri.SelectedValue);
            komut.Parameters.AddWithValue("@p4", int.Parse(numadet.Value.ToString()));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtfıyat.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txttoplam.Text));
            komut.Parameters.AddWithValue("@p7", txttarih.Text);
            komut.Parameters.AddWithValue("@p8", rchnot.Text);
            komut.Parameters.AddWithValue("@p9", textEdit8.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Satış Güncelleme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Müsteriliste();

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete From TblMüşteriSatış where Id=" + textEdit8.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            SqlCommand komut2 = new SqlCommand("update TblÜrünler set adet=adet+@p1 where ÜrünAd=@p2", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",numadet.Value);
            komut2.Parameters.AddWithValue("@p2", cmburun.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Satış Silme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Müsteriliste();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            cmburun.Text = "";
            cmbpersonel.Text = "";
            cmbmüşteri.Text = "";
            numadet.Value = 0;
            txtfıyat.Text = "0";
            txttoplam.Text = "";
            txttarih.Text = "";
            rchnot.Text = "";
            textEdit8.Text = "";
            cmburun.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ürünler();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ürünlermüşteri();
        }

        private void nudadet_ValueChanged(object sender, EventArgs e)
        {
            fiyat = Convert.ToDouble(txtfiyat.Text);
            miktar = Convert.ToDouble(nudadet.Value);
            toplam = fiyat * miktar;
            txttoplamfiyat.Text = toplam.ToString();
        }

        private void numadet_ValueChanged(object sender, EventArgs e)
        {
            fiyat = Convert.ToDouble(txtfıyat.Text);
            miktar = Convert.ToDouble(numadet.Value);
            toplam = fiyat * miktar;
            txttoplam.Text = toplam.ToString();

        }
    }
}
