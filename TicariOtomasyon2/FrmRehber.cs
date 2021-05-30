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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void müsteriliste()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Ad,Soyad,Telefon,Telefon2,Mail from TblMüşteriler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void firmaliste()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Ad as 'Firma Adı',Sektör,YetkiliAdSoyad,Telefon1,Telefon2,Telefon3,Mail,Fax from TblFirmalar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }


        private void FrmRehber_Load(object sender, EventArgs e)
        {
            müsteriliste();
            firmaliste();

        }




        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Frmİletisim fr = new Frmİletisim();

            fr.iletisim = gridView1.GetFocusedRowCellValue("Mail").ToString();
            fr.Show();
        }

       
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {

            Frmİletisim fr = new Frmİletisim();

            fr.iletisim = gridView2.GetFocusedRowCellValue("Mail").ToString();
            fr.Show();

        }
    }


}
