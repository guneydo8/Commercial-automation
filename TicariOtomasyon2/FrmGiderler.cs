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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TblGiderler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            listele();


        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtid.Text = gridView1.GetFocusedRowCellValue("Id").ToString();
            txtdoglgaz.Text = gridView1.GetFocusedRowCellValue("Dogalgaz").ToString();
            txtekstra.Text = gridView1.GetFocusedRowCellValue("Ekstra").ToString();
            txtelktrik.Text = gridView1.GetFocusedRowCellValue("Elektrik").ToString();
            txtinternet.Text = gridView1.GetFocusedRowCellValue("Internet").ToString();
            txtmaas.Text = gridView1.GetFocusedRowCellValue("Maaşlar").ToString();
            txtsu.Text = gridView1.GetFocusedRowCellValue("Su").ToString();
            cmbay.Text = gridView1.GetFocusedRowCellValue("Ay").ToString();
            cmbyıl.Text = gridView1.GetFocusedRowCellValue("Yıl").ToString();
            rchnot.Text = gridView1.GetFocusedRowCellValue("Notlar").ToString();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblGiderler(Ay,Yıl,Elektrik,Su,Dogalgaz,Internet,Maaşlar,Ekstra,Notlar) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbay.Text);
            komut.Parameters.AddWithValue("@p2", cmbyıl.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtelktrik.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtdoglgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtinternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtmaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtekstra.Text));
            komut.Parameters.AddWithValue("@p9", rchnot.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtsu.Text));
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni Gider Eklemesi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            listele();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            txtdoglgaz.Text = "";
            txtekstra.Text = "";
            txtelktrik.Text = "";
            txtid.Text = "";
            txtinternet.Text = "";
            txtmaas.Text = "";
            txtsu.Text = "";
            cmbay.Text = "";
            cmbyıl.Text = "";
            rchnot.Text = "";
            cmbay.Focus();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TblGiderler where Id=" + txtid.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Silme İşlemi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TblGiderler set Ay=@p1,Yıl=@p2,Elektrik=@p3,Su=@p4,Dogalgaz=@p5,Internet=@p6,Maaşlar=@p7,Ekstra=@p8,Notlar=@p9 where Id=@p10", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbay.Text);
            komut.Parameters.AddWithValue("@p2", cmbyıl.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(txtelktrik.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(txtdoglgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtinternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtmaas.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(txtekstra.Text));
            komut.Parameters.AddWithValue("@p9", rchnot.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtsu.Text));
            komut.Parameters.AddWithValue("@p10", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Güncellemesi Yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }
    }
}
