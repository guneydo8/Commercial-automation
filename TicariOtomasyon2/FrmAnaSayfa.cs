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
using System.Xml;

namespace TicariOtomasyon2
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void stokdurum()
        {
            SqlDataAdapter da = new SqlDataAdapter("exec stokdurum",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        
        void notlar()
        {
            SqlDataAdapter da = new SqlDataAdapter("select top 5 NotTarih,NotSaat,Konu from TblNotlar order by NotTarih asc", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl4.DataSource = dt;

        }

        void firmasatıslar()
        {
            SqlDataAdapter da = new SqlDataAdapter("select top 5 FirmaSatışId,ÜrünAd,TblFirmalar.Ad as 'Firma Adı',tblFirmaSatış.Adet from Tblfirmasatış inner join TblÜrünler on TblFirmaSatış.Ürün=TblÜrünler.Id  inner join TblFirmalar on TblFirmaSatış.AlıcıFirma=TblFirmalar.Id order by FirmaSatışId desc", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        void müşterisatışlar()
        {
            SqlDataAdapter da = new SqlDataAdapter("select top 5 TblMüşteriSatış.Id,ÜrünAd,TblMüşteriler.Ad+' '+TblMüşteriler.Soyad as'Müşteriler',tblMüşteriSatış.Adet from TblMüşteriSatış inner join TblÜrünler on TblmüşteriSatış.Ürün=TblÜrünler.Id  inner join TblMüşteriler on TblMüşteriSatış.Müşteri=TblMüşteriler.Id  order by ıd desc", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }
        void haberler()
        {
            XmlTextReader oku = new XmlTextReader("https://www.haberturk.com/rss/kategori/teknoloji.xml");
            while (oku.Read())
            {
                if (oku.Name == "title")
                {
                    listBox1.Items.Add(oku.ReadString());
                }
            }

        }
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stokdurum();
            notlar();
            firmasatıslar();
            müşterisatışlar();
            haberler();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
            webBrowser2.Navigate("http://www.youtube.com");

        }

       
    }
}
