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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("exec FirmaBilgi", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void kodacıklama()
        {
            SqlCommand komut = new SqlCommand("select * from TblKodlar", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                rchkod1.Text = dr[0].ToString();
                rchkod2.Text = dr[1].ToString();
                rchkod3.Text = dr[2].ToString();
            }
            bgl.baglanti().Close();
        }
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            listele();
            kodacıklama();
            SqlDataAdapter da = new SqlDataAdapter("select * from TblSehirler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbsehir.DisplayMember = "Sehir";
            cmbsehir.ValueMember = "Id";
            cmbsehir.DataSource = dt;

        }

        private void cmbsehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from Tblilceler where sehir=" + cmbsehir.SelectedValue, bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbilce.DisplayMember = "Ilce";
            cmbilce.ValueMember = "Id";
            cmbilce.DataSource = dt;

        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tblfirmalar(Ad,Sektör,YetkiliDepartman,YetkiliAdSoyad,YetkiliTC,Telefon1,Telefon2,Telefon3,Mail,Fax,Sehir,Ilce,Adres,VergiDaire,Kod1,Kod2,Kod3) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtfirma.Text);
            komut.Parameters.AddWithValue("@p2", txtsektör.Text);
            komut.Parameters.AddWithValue("@p3", txtdepartman.Text);
            komut.Parameters.AddWithValue("@p4", txtadsoyad.Text);
            komut.Parameters.AddWithValue("@p5", txttc.Text);
            komut.Parameters.AddWithValue("@p6", txttelefon1.Text);
            komut.Parameters.AddWithValue("@p7", txttelefon2.Text);
            komut.Parameters.AddWithValue("@p8", txttelefon3.Text);
            komut.Parameters.AddWithValue("@p9", txtmail.Text);
            komut.Parameters.AddWithValue("@p10", txtfax.Text);
            komut.Parameters.AddWithValue("@p11", cmbsehir.SelectedValue);
            komut.Parameters.AddWithValue("@p12", cmbilce.SelectedValue);
            komut.Parameters.AddWithValue("@p13", rchadres.Text);
            komut.Parameters.AddWithValue("@p14", txtvergi.Text);
            komut.Parameters.AddWithValue("@p15", txtkod1.Text);
            komut.Parameters.AddWithValue("@p16", txtkod2.Text);
            komut.Parameters.AddWithValue("@p17", txtkod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni Firma Ekleme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtıd.Text = gridView1.GetFocusedRowCellValue("Id").ToString();
            txtadsoyad.Text = gridView1.GetFocusedRowCellValue("YetkiliAdSoyad").ToString();
            txtdepartman.Text = gridView1.GetFocusedRowCellValue("YetkiliDepartman").ToString();
            txtfax.Text = gridView1.GetFocusedRowCellValue("Fax").ToString();
            txtfirma.Text = gridView1.GetFocusedRowCellValue("Ad").ToString();
            txtkod1.Text = gridView1.GetFocusedRowCellValue("Kod1").ToString();
            txtkod2.Text = gridView1.GetFocusedRowCellValue("Kod2").ToString();
            txtkod3.Text = gridView1.GetFocusedRowCellValue("Kod3").ToString();
            txtmail.Text = gridView1.GetFocusedRowCellValue("Mail").ToString();
            txtsektör.Text = gridView1.GetFocusedRowCellValue("Sektör").ToString();
            txttc.Text = gridView1.GetFocusedRowCellValue("YetkiliTC").ToString();
            txttelefon1.Text = gridView1.GetFocusedRowCellValue("Telefon1").ToString();
            txttelefon2.Text = gridView1.GetFocusedRowCellValue("Telefon2").ToString();
            txttelefon3.Text = gridView1.GetFocusedRowCellValue("Telefon3").ToString();
            txtvergi.Text = gridView1.GetFocusedRowCellValue("VergiDaire").ToString();
            cmbsehir.Text = gridView1.GetFocusedRowCellValue("Sehir").ToString();
            cmbilce.Text = gridView1.GetFocusedRowCellValue("Ilce").ToString();
            rchadres.Text = gridView1.GetFocusedRowCellValue("Adres").ToString();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {

            txtıd.Text = "";
            txtadsoyad.Text = "";
            txtdepartman.Text = "";
            txtfax.Text = "";
            txtfirma.Text = "";
            txtkod1.Text = "";
            txtkod2.Text = "";
            txtkod3.Text = "";
            txtmail.Text = "";
            txtsektör.Text = "";
            txttc.Text = "";
            txttelefon1.Text = "";
            txttelefon2.Text = "";
            txttelefon3.Text = "";
            txtvergi.Text = "";
            cmbsehir.Text = "";
            cmbilce.Text = "";
            rchadres.Text = "";
            txtıd.Text = "";
            txtfirma.Focus();

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TblFirmalar where Id="+txtıd.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Silme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TblFirmalar set Ad=@p1,Sektör=@p2,YetkiliDepartman=@p3,YetkiliAdSoyad=@p4,YetkiliTC=@p5,Telefon1=@p6,Telefon2=@p7,Telefon3=@p8,Mail=@p9,Fax=@p10,Sehir=@p11,Ilce=@p12,Adres=@p13,VergiDaire=@p14,Kod1=@p15,Kod2=@p16,Kod3=@p17 where Id=@p18", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtfirma.Text);
            komut.Parameters.AddWithValue("@p2", txtsektör.Text);
            komut.Parameters.AddWithValue("@p3", txtdepartman.Text);
            komut.Parameters.AddWithValue("@p4", txtadsoyad.Text);
            komut.Parameters.AddWithValue("@p5", txttc.Text);
            komut.Parameters.AddWithValue("@p6", txttelefon1.Text);
            komut.Parameters.AddWithValue("@p7", txttelefon2.Text);
            komut.Parameters.AddWithValue("@p8", txttelefon3.Text);
            komut.Parameters.AddWithValue("@p9", txtmail.Text);
            komut.Parameters.AddWithValue("@p10", txtfax.Text);
            komut.Parameters.AddWithValue("@p11", cmbsehir.SelectedValue);
            komut.Parameters.AddWithValue("@p12", cmbilce.SelectedValue);
            komut.Parameters.AddWithValue("@p13", rchadres.Text);
            komut.Parameters.AddWithValue("@p14", txtvergi.Text);
            komut.Parameters.AddWithValue("@p15", txtkod1.Text);
            komut.Parameters.AddWithValue("@p16", txtkod2.Text);
            komut.Parameters.AddWithValue("@p17", txtkod3.Text);
            komut.Parameters.AddWithValue("@p18", txtıd.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Güncelleme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
    }
}
