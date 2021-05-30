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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();


        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select tblnotlar.Id,NotTarih,NotSaat,Konu,Detay,Ad+' '+soyad as'Oluşturan',Hitap from TblNotlar inner join TblPersoneller on TblNotlar.Oluşturan=TblPersoneller.Id", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void personel()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Id,Ad+' '+soyad as 'Oluşturan' from TblPersoneller", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbolusturan.DisplayMember = "Oluşturan";
            cmbolusturan.ValueMember = "Id";
            cmbolusturan.DataSource = dt;
            
        }
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();
            personel();

        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TblNotlar(NotTarih,NotSaat,Konu,detay,oluşturan,hitap) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txttarih.Text);
            komut.Parameters.AddWithValue("@p2", txtsaat.Text);
            komut.Parameters.AddWithValue("@p3", txtkonu.Text);
            komut.Parameters.AddWithValue("@p4", rchdeaty.Text);
            komut.Parameters.AddWithValue("@p5", cmbolusturan.SelectedValue);
            komut.Parameters.AddWithValue("@p6", txthitap.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            txtid.Text = gridView1.GetFocusedRowCellValue("Id").ToString();
            txthitap.Text = gridView1.GetFocusedRowCellValue("Hitap").ToString();
            txtkonu.Text = gridView1.GetFocusedRowCellValue("Konu").ToString();
            txtsaat.Text = gridView1.GetFocusedRowCellValue("NotSaat").ToString();
            txttarih.Text = gridView1.GetFocusedRowCellValue("NotTarih").ToString();
            rchdeaty.Text = gridView1.GetFocusedRowCellValue("Detay").ToString();
            cmbolusturan.Text = gridView1.GetFocusedRowCellValue("Oluşturan").ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from TblNotlar where Id=" + txtid.Text, bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TblNotlar set NotTarih=@p1,NotSaat=@p2,Konu=@p3,Detay=@p4,Oluşturan=@p5,Hitap=@p6 where Id=@p7", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txttarih.Text);
            komut.Parameters.AddWithValue("@p2", txtsaat.Text);
            komut.Parameters.AddWithValue("@p3", txtkonu.Text);
            komut.Parameters.AddWithValue("@p4", rchdeaty.Text);
            komut.Parameters.AddWithValue("@p5", cmbolusturan.SelectedValue);
            komut.Parameters.AddWithValue("@p6", txthitap.Text);
            komut.Parameters.AddWithValue("@p7", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            txthitap.Text = "";
            txtid.Text = "";
            txtkonu.Text = "";
            txtsaat.Text = "";
            txttarih.Text = "";
            cmbolusturan.Text = "";
            rchdeaty.Text = "";
            txttarih.Focus();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();
            fr.not = txtid.Text;
            fr.Show();

        }
    }
}
