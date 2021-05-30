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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select TblMüşteriler.Id, Ad,Soyad,Telefon,Telefon2,TC,Mail,TblSehirler.Sehir,Tblilceler.Ilce,Adres,VergiDaire from TblMüşteriler inner join TblSehirler on TblMüşteriler.Sehir=TblSehirler.Id inner join Tblilceler on TblMüşteriler.Ilce=Tblilceler.Id", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();

           

            SqlDataAdapter da = new SqlDataAdapter("select * from TblSehirler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbsehir.ValueMember = "Id";
            cmbsehir.DisplayMember = "Sehir";
            cmbsehir.DataSource = dt;


        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblMüşteriler(Ad,Soyad,Telefon,Telefon2,TC,Mail,Sehir,Ilce,Adres,VergiDaire) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", txttelefon.Text);
            komut.Parameters.AddWithValue("@p4", txtelefon2.Text);
            komut.Parameters.AddWithValue("@p5", txttc.Text);
            komut.Parameters.AddWithValue("@p6", txtmail.Text);
            komut.Parameters.AddWithValue("@p7", cmbsehir.SelectedValue);
            komut.Parameters.AddWithValue("@p8", cmbilce.SelectedValue);
            komut.Parameters.AddWithValue("@p9", rchadres.Text);
            komut.Parameters.AddWithValue("@p10", txtvergi.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Ekleme Işlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void cmbsehir_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlDataAdapter da = new SqlDataAdapter("select * from Tblilceler where Sehir =" + cmbsehir.SelectedValue, bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbilce.ValueMember = "Id";
            cmbilce.DisplayMember = "Ilce";
            cmbilce.DataSource = dt;
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtid.Text = gridView1.GetFocusedRowCellValue("Id").ToString();
            txtad.Text = gridView1.GetFocusedRowCellValue("Ad").ToString();
            txtsoyad.Text = gridView1.GetFocusedRowCellValue("Soyad").ToString();
            txttelefon.Text = gridView1.GetFocusedRowCellValue("Telefon").ToString();
            txtelefon2.Text = gridView1.GetFocusedRowCellValue("Telefon2").ToString();
            txttc.Text = gridView1.GetFocusedRowCellValue("TC").ToString();
            txtmail.Text = gridView1.GetFocusedRowCellValue("Mail").ToString();
            cmbsehir.Text = gridView1.GetFocusedRowCellValue("Sehir").ToString();
            cmbilce.Text = gridView1.GetFocusedRowCellValue("Ilce").ToString();
            rchadres.Text = gridView1.GetFocusedRowCellValue("Adres").ToString();
            txtvergi.Text = gridView1.GetFocusedRowCellValue("VergiDaire").ToString();

        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TblMüşteriler where Id=" + txtid.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Silme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TblMüşteriler set Ad=@p2,Soyad=@p3,Telefon=@p4,Telefon2=@p5,TC=@p6,Mail=@p7,Sehir=@p8,Ilce=@p9,Adres=@p10,VergiDaire=@p11 where Id=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.Parameters.AddWithValue("@p2", txtad.Text);
            komut.Parameters.AddWithValue("@p3", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p4", txttelefon.Text);
            komut.Parameters.AddWithValue("@p5", txtelefon2.Text);
            komut.Parameters.AddWithValue("@p6", txttc.Text);
            komut.Parameters.AddWithValue("@p7", txtmail.Text);
            komut.Parameters.AddWithValue("@p8", cmbsehir.SelectedValue);
            komut.Parameters.AddWithValue("@p9", cmbilce.SelectedValue);
            komut.Parameters.AddWithValue("@p10", rchadres.Text);
            komut.Parameters.AddWithValue("@p11", txtvergi.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Güncelleme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            txtad.Text = "";
            txtelefon2.Text = "";
            txtid.Text = "";
            txtmail.Text = "";
            txtsoyad.Text = "";
            txttc.Text = "";
            txttelefon.Text = "";
            txtvergi.Text = "";
            cmbilce.Text = "";
            cmbsehir.Text = "";
            rchadres.Text = "";
            txtad.Focus();
        }

      
    }
}
