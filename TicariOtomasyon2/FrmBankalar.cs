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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("exec Banka", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            SqlDataAdapter da = new SqlDataAdapter("select * from TblSehirler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbsehir.ValueMember = "Id";
            cmbsehir.DisplayMember = "Sehir";
            cmbsehir.DataSource = dt;

            SqlDataAdapter da2 = new SqlDataAdapter("Select * from TblFirmalar", bgl.baglanti());
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            cmbfirma.DisplayMember = "Ad";
            cmbfirma.ValueMember = "Id";
            cmbfirma.DataSource = dt2;


        }

        private void cmsehir_SelectedIndexChanged(object sender, EventArgs e)
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
            SqlCommand komut = new SqlCommand("insert into TblBankalar(BankaAdı,Sehir,Ilce,Sube,Iban,HesapNo,Yetkili,Telefon,Tarih,HesapTürü,Firma) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtbankaad.Text);
            komut.Parameters.AddWithValue("@p2", cmbsehir.SelectedValue);
            komut.Parameters.AddWithValue("@p3", cmbilce.SelectedValue);
            komut.Parameters.AddWithValue("@p4", txtsube.Text);
            komut.Parameters.AddWithValue("@p5", txtıban.Text);
            komut.Parameters.AddWithValue("@p6", txthesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtyetkiliadsoyad.Text);
            komut.Parameters.AddWithValue("@p8", txttelefon.Text);
            komut.Parameters.AddWithValue("@p9", txttarih.Text);
            komut.Parameters.AddWithValue("@p10", txthasaptur.Text);
            komut.Parameters.AddWithValue("@p11", cmbfirma.SelectedValue);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firmaya Ait Banka Ekleme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TblBankalar where Id=" + txtid.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firmaya Ait Banka Silme İşlemi Yapıldı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TblBankalar set BankaAdı=@p1,Sehir=@p2,Ilce=@p3,Sube=@p4,Iban=@p5,HesapNo=@p6,Yetkili=@p7,Telefon=@p8,Tarih=@p9,HesapTürü=@p10,Firma=@p11 where Id=@p12",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtbankaad.Text);
            komut.Parameters.AddWithValue("@p2", cmbsehir.SelectedValue);
            komut.Parameters.AddWithValue("@p3", cmbilce.SelectedValue);
            komut.Parameters.AddWithValue("@p4", txtsube.Text);
            komut.Parameters.AddWithValue("@p5", txtıban.Text);
            komut.Parameters.AddWithValue("@p6", txthesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtyetkiliadsoyad.Text);
            komut.Parameters.AddWithValue("@p8", txttelefon.Text);
            komut.Parameters.AddWithValue("@p9", txttarih.Text);
            komut.Parameters.AddWithValue("@p10", txthasaptur.Text);
            komut.Parameters.AddWithValue("@p11", cmbfirma.SelectedValue);
            komut.Parameters.AddWithValue("@p12", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firmaya Ait Banka Güncelleme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtbankaad.Text = gridView1.GetFocusedRowCellValue("BankaAdı").ToString();
            txthasaptur.Text = gridView1.GetFocusedRowCellValue("HesapTürü").ToString();
            txthesapno.Text = gridView1.GetFocusedRowCellValue("HesapNo").ToString();
            txtid.Text = gridView1.GetFocusedRowCellValue("Id").ToString();
            txtsube.Text = gridView1.GetFocusedRowCellValue("Sube").ToString();
            txttarih.Text = gridView1.GetFocusedRowCellValue("Tarih").ToString();
            txttelefon.Text = gridView1.GetFocusedRowCellValue("Telefon").ToString();
            txtyetkiliadsoyad.Text = gridView1.GetFocusedRowCellValue("Yetkili").ToString();
            txtıban.Text = gridView1.GetFocusedRowCellValue("Iban").ToString();
            cmbfirma.Text = gridView1.GetFocusedRowCellValue("Firma Adı").ToString();
            cmbilce.Text = gridView1.GetFocusedRowCellValue("Ilce").ToString();
            cmbsehir.Text = gridView1.GetFocusedRowCellValue("Sehir").ToString();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            txtbankaad.Text = "";
            txthasaptur.Text = "";
            txthesapno.Text = "";
            txtid.Text = "";
            txtsube.Text = "";
            txttarih.Text = "";
            txttelefon.Text = "";
            txtyetkiliadsoyad.Text = "";
            txtıban.Text = "";
            cmbfirma.Text = "";
            cmbilce.Text = "";
            cmbsehir.Text = "";
            txtbankaad.Focus();
        }
    }
}
