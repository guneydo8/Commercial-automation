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
using DevExpress.Charts;

namespace TicariOtomasyon2
{
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select ÜrünAd,sum(adet) as 'Toplam Adet' from TblÜrünler group by ÜrünAd ", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            SqlCommand komut = new SqlCommand("select ÜrünAd,sum(adet) as 'Toplam Adet' from TblÜrünler group by ÜrünAd", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
               chartControl3.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));


            }
            bgl.baglanti();


            SqlCommand komut2 = new SqlCommand("select Marka,Count(*) from TblÜrünler group by MArka", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));


            }
            bgl.baglanti();


            
            SqlCommand komut3 = new SqlCommand("select (Ad+' '+Soyad) ,sum(Toplam) from TblFirmaSatış inner join TblPersoneller on TblFirmaSatış.Personel=TblPersoneller.Id group by (Ad+' '+Soyad)", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                chartControl1.Series["Series 1"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr3[0], dr3[1]));


            }
            bgl.baglanti();


            SqlCommand komut6 = new SqlCommand("select (Ad+' '+Soyad) ,sum(Toplam) from TblMüşteriSatış inner join TblPersoneller on TblMüşteriSatış.Personel=TblPersoneller.Id group by (Ad+' '+Soyad)", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                chartControl6.Series["Series 1"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr6[0], dr6[1]));

            }
            bgl.baglanti().Close();

            SqlCommand komut4 = new SqlCommand("select ad,Sum(Toplam)from TblFirmaSatış inner join TblFirmalar on TblFirmaSatış.AlıcıFirma=TblFirmalar.Id group by Ad", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                chartControl4.Series["Series 1"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr4[0], dr4[1]));


            }
            bgl.baglanti();


            SqlCommand komut5 = new SqlCommand("select ad+' '+soyad,Sum(Toplam)from TblMüşteriSatış inner join TblMüşteriler on TblMüşteriSatış.Müşteri=TblMüşteriler.Id group by ad+' '+soyad", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                chartControl5.Series["Series 1"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr5[0], dr5[1]));


            }
            bgl.baglanti();


            int ıd;
            int ıd;
            



        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay fr = new FrmStokDetay();
            fr.ıd = gridView1.GetFocusedRowCellValue("ÜrünAd").ToString();
            fr.Show();
        }
    }
}
