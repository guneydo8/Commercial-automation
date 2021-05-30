using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon2
{
    public partial class FrmAnaModul : Form
    {
        public FrmAnaModul()
        {
            InitializeComponent();
        }

        FrmÜrünler frürün;
        private void BTNURUNLER_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frürün == null)
            {
                frürün = new FrmÜrünler();
                frürün.MdiParent = this;
                frürün.Show();
            }

        }

        FrmMusteriler frmüsteriler;
        private void BTNMUSTERİLER_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (frmüsteriler == null)
            {
                frmüsteriler = new FrmMusteriler();
                frmüsteriler.MdiParent = this;
                frmüsteriler.Show();

            }
        }


        FrmFirmalar fr;
        private void BTNFIRMLAR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fr == null)
            {
                fr = new FrmFirmalar();
                fr.MdiParent = this;
                fr.Show();
            }

        }

        FrmPersoneller frpersonel;
        private void BTNPERSONLLER_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frpersonel == null)
            {
                frpersonel = new FrmPersoneller();
                frpersonel.MdiParent = this;
                frpersonel.Show();

            }

        }

        FrmRehber frrehber;
        private void BTNREHBER_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frrehber == null)
            {
                frrehber = new FrmRehber();
                frrehber.MdiParent = this;
                frrehber.Show();
            }

        }
        FrmGiderler frgider;
        private void BTNGIDERLER_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frgider == null)
            {
                frgider = new FrmGiderler();
                frgider.MdiParent = this;
                frgider.Show();
            }

        }

        FrmBankalar frbanka;
        private void BTNBANKA_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frbanka == null)
            {
                frbanka = new FrmBankalar();
                frbanka.MdiParent = this;
                frbanka.Show();
            }

        }

        FrmFaturalar frfatura;
        private void BTNFATURALAR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frfatura == null)
            {
                frfatura = new FrmFaturalar();
                frfatura.MdiParent = this;
                frfatura.Show();
            }

        }

        FrmNotlar frnot;
        private void BTNNOTLAR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frnot == null)
            {
                frnot = new FrmNotlar();
                frnot.MdiParent = this;
                frnot.Show();
            }

        }

        FrmSatıslar frsatıs;
        private void BTNSATISLAR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frsatıs == null)
            {
                frsatıs = new FrmSatıslar();
                frsatıs.MdiParent = this;
                frsatıs.Show();
            }

        }

        //FrmRaporlar frrapor;
        private void BTNRAPORLAR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (frrapor == null)
            //{
            //    frrapor = new FrmRaporlar();
            //    frrapor.MdiParent = this;
            //    frrapor.Show();

            //}

        }

        FrmStoklar frstok;
        private void BTNSTOKLAR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frstok == null)
            {
                frstok = new FrmStoklar();
                frstok.MdiParent = this;
                frstok.Show();
            }

        }

        FrmAyarlar frayar;
        private void BTNAYARLAR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frayar == null)
            {
                frayar = new FrmAyarlar();
                frayar.Show();
            }

        }
        FrmKasa frkasa;
        private void BTNKASA_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (frkasa == null)
            {
                frkasa = new FrmKasa();
                frkasa.MdiParent = this;
                frkasa.Show();
            }

        }

        FrmAnaSayfa FRANAS;
        private void BTNANASAYFA_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (FRANAS == null)
            {
                FRANAS = new FrmAnaSayfa();
                FRANAS.MdiParent = this;
                FRANAS.Show();
            }
        }

        private void FrmAnaModul_Load(object sender, EventArgs e)
        {
            if (FRANAS == null)
            {
                FRANAS = new FrmAnaSayfa();
                FRANAS.MdiParent = this;
                FRANAS.Show();
            }
        }
    }
}
