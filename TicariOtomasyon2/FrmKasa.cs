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
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void satıslistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("exec FirmaSatış", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            SqlDataAdapter da2 = new SqlDataAdapter("exec MüşteriSatış", bgl.baglanti());
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            gridControl3.DataSource = dt2;

        }

        void kasa()
        {
            SqlCommand komut = new SqlCommand("exec kasa", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblkasa.Text = dr[0] + " " + "TL";
            }
            bgl.baglanti();
        }

        void giderler()
        {
            SqlCommand komut = new SqlCommand("select (Elektrik+Su+Internet+Dogalgaz+Ekstra),maaşlar from TblGiderler", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblodemeler.Text = dr[0] + " " + "TL";
                lblpersonelmaas.Text = dr[1] + " " + "TL";
            }
            bgl.baglanti();

        }


        void firmasayısı()
        {

            SqlCommand komut = new SqlCommand("select COUNT(*) from TblFirmalar", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblfırma.Text = dr[0].ToString();

            }
            bgl.baglanti();

        }
        void müsterisayısı()
        {
            SqlCommand komut = new SqlCommand("select COUNT(*) from TblMüşteriler", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblmusteri.Text = dr[0].ToString();

            }
            bgl.baglanti();


        }

        void personelsayısı()
        {
            SqlCommand komut = new SqlCommand("select COUNT(*) from TblPersoneller", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblpersonel.Text = dr[0].ToString();

            }
            bgl.baglanti();
        }

        void stoksayısı()
        {
            SqlCommand komut = new SqlCommand("select Sum(Adet)from TblÜrünler", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblstok.Text = dr[0].ToString();

            }
            bgl.baglanti();
        }

        void giderlerliste()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TblGiderler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        int sayac = 0;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            satıslistesi();
            kasa();
            giderler();
            firmasayısı();
            müsterisayısı();
            personelsayısı();
            stoksayısı();
            giderlerliste();


            timer1.Start();







        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (sayac > 0 && sayac <= 5)
            {
                groupControl8.Text = "Elektrik Faturası";
                SqlCommand komut = new SqlCommand("select top 4 * from TblGiderler", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[3]));

                }
                bgl.baglanti().Close();

            }
            if (sayac > 5 && sayac <= 10)
            {
                chartControl1.Series["Aylar"].Points.Clear();
                groupControl8.Text = "Su Faturası";
                SqlCommand komut = new SqlCommand("select top 4 * from TblGiderler", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[4]));

                }
                bgl.baglanti().Close();

            }
            if (sayac > 10 && sayac <= 15)
            {
                chartControl1.Series["Aylar"].Points.Clear();
                groupControl8.Text = "Doğalgaz Faturası";
                SqlCommand komut = new SqlCommand("select top 4 * from TblGiderler", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[5]));

                }
                bgl.baglanti().Close();

            }
            if (sayac > 15 && sayac <= 20)
            {
                chartControl1.Series["Aylar"].Points.Clear();
                groupControl8.Text = "Internet";
                SqlCommand komut = new SqlCommand("select top 4 * from TblGiderler", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[6]));

                }
                bgl.baglanti().Close();

            }
            if (sayac > 20 && sayac <= 25)
            {
                chartControl1.Series["Aylar"].Points.Clear();

                groupControl8.Text = "Eksta Ödemeler";
                SqlCommand komut = new SqlCommand("select top 4 * from TblGiderler", bgl.baglanti());
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[8]));

                }
                bgl.baglanti().Close();

            }
            if (sayac == 26)
            {
                sayac = 0;
            }

        }


    }
}

