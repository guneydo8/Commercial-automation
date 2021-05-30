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
    public partial class FrmÜrünler : Form
    {
        public FrmÜrünler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TblÜrünler",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmÜrünler_Load(object sender, EventArgs e)
        {
            listele();


        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblÜrünler(ÜrünAd,Marka,Model,Yıl,Adet,AlışFiyat,Satışfiyat,Detay) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txturunad.Text);
            komut.Parameters.AddWithValue("@p2", txtmarka.Text);
            komut.Parameters.AddWithValue("@p3", txtmodel.Text);
            komut.Parameters.AddWithValue("@p4", txtyıl.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse(numericadet.Value.ToString()));
            komut.Parameters.AddWithValue("@P6", decimal.Parse(txtalısfiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtsatısfiyat.Text));
            komut.Parameters.AddWithValue("@p8", rchdetay.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Ekleme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtıd.Text = gridView1.GetFocusedRowCellValue("Id").ToString();
            txturunad.Text = gridView1.GetFocusedRowCellValue("ÜrünAd").ToString();
            txtmarka.Text = gridView1.GetFocusedRowCellValue("Marka").ToString();
            txtmodel.Text = gridView1.GetFocusedRowCellValue("Model").ToString();
            txtyıl.Text = gridView1.GetFocusedRowCellValue("Yıl").ToString();
            numericadet.Value = int.Parse(gridView1.GetFocusedRowCellValue("Adet").ToString());
            txtalısfiyat.Text = gridView1.GetFocusedRowCellValue("AlışFiyat").ToString();
            txtsatısfiyat.Text = gridView1.GetFocusedRowCellValue("SatışFiyat").ToString();
            rchdetay.Text = gridView1.GetFocusedRowCellValue("Detay").ToString();


        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            txtıd.Text = "";
            txturunad.Text = "";
            txtmodel.Text = "";
            txtmarka.Text = "";
            txtyıl.Text = "";
            txtalısfiyat.Text = "";
            txtsatısfiyat.Text = "";
            numericadet.Value = 0;
            rchdetay.Text = "";
            txturunad.Focus();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TblÜrünler where Id=" + txtıd.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silme İşlemi Yapıldı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TblÜrünler set ÜrünAd=@p1,Marka=@p2,Model=@p3,Yıl=@p4,Adet=@p5,AlışFiyat=@p6,satışFiyat=@p7,Detay=@p8 where Id=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txturunad.Text);
            komut.Parameters.AddWithValue("@p2", txtmarka.Text);
            komut.Parameters.AddWithValue("@p3", txtmodel.Text);
            komut.Parameters.AddWithValue("@p4", txtyıl.Text);
            komut.Parameters.AddWithValue("@p5", numericadet.Value);
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtalısfiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtsatısfiyat.Text));
            komut.Parameters.AddWithValue("@p8", rchdetay.Text);
            komut.Parameters.AddWithValue("@p9", txtıd.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Güncelleme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            listele();
        }
    }
}
