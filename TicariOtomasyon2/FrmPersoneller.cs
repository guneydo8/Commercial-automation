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
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TblPersoneller", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            listele();
            SqlDataAdapter da = new SqlDataAdapter("select * from TblSehirler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbsehir.ValueMember = "Id";
            cmbsehir.DisplayMember = "Sehir";
            cmbsehir.DataSource = dt;

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

        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblPersoneller(Ad,Soyad,Departman,Telefon,Tc,Mail,Sehir,Ilce,Adres) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", txtdepartman.Text);
            komut.Parameters.AddWithValue("@p4", txttelefon.Text);
            komut.Parameters.AddWithValue("@p5", txttc.Text);
            komut.Parameters.AddWithValue("@p6", txtmail.Text);
            komut.Parameters.AddWithValue("@p7", cmbsehir.SelectedValue);
            komut.Parameters.AddWithValue("@p8", cmbilce.SelectedValue);
            komut.Parameters.AddWithValue("@p9", rchadres.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Ekleme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TblPersoneller where Id=" + txtid.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Silme İşlemi Yapıldı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TblPersoneller set Ad=@p1,Soyad=@p2,Departman=@p3,Telefon=@p4,Tc=@p5,Mail=@p6,Sehir=@p7,Ilce=@p8,Adres=@p9 where ıd=@p10", bgl.baglanti());

            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", txtdepartman.Text);
            komut.Parameters.AddWithValue("@p4", txttelefon.Text);
            komut.Parameters.AddWithValue("@p5", txttc.Text);
            komut.Parameters.AddWithValue("@p6", txtmail.Text);
            komut.Parameters.AddWithValue("@p7", cmbsehir.SelectedValue);
            komut.Parameters.AddWithValue("@p8", cmbilce.SelectedValue);
            komut.Parameters.AddWithValue("@p9", rchadres.Text);
            komut.Parameters.AddWithValue("@p10", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Güncelleme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtid.Text = gridView1.GetFocusedRowCellValue("Id").ToString();
            txtad.Text = gridView1.GetFocusedRowCellValue("Ad").ToString();
            txtsoyad.Text = gridView1.GetFocusedRowCellValue("Soyad").ToString();
            txtdepartman.Text = gridView1.GetFocusedRowCellValue("Departman").ToString();
            txttelefon.Text = gridView1.GetFocusedRowCellValue("Telefon").ToString();
            txttc.Text = gridView1.GetFocusedRowCellValue("TC").ToString();
            txtmail.Text = gridView1.GetFocusedRowCellValue("Mail").ToString();
            cmbsehir.Text = gridView1.GetFocusedRowCellValue("Sehir").ToString();
            cmbilce.Text = gridView1.GetFocusedRowCellValue("Ilce").ToString();
            rchadres.Text = gridView1.GetFocusedRowCellValue("Adres").ToString();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            txtid.Text = "";
            txtad.Text = "";
            txtdepartman.Text = "";
            txtmail.Text = "";
            txtsoyad.Text = "";
            txttc.Text = "";
            txttelefon.Text = "";
            cmbilce.Text = "";
            cmbsehir.Text = "";
            rchadres.Text = "";
            txtad.Focus();
        }
    }
}
