using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MimeKit;
using MailKit.Net.Smtp;

namespace TicariOtomasyon2
{
    public partial class Frmİletisim : Form
    {
        public Frmİletisim()
        {
            InitializeComponent();
        }

        public string iletisim;
        private void Frmİletisim_Load(object sender, EventArgs e)
        {
            txtmail.Text = iletisim;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
          
            SendMail("Test","sado.doan@yandex.com",iletisim,iletisim,txtkonu.Text,txtmail.Text, "smtp.yandex.com.tr",true,465,"Password2");

        }
        public static bool SendMail(string fromName, string fromEmail, string toName, string toMail, string subject, string mail, string smtp, bool ssl, int port, string password)
        {
            try
            {
                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress(fromName, fromEmail);
                message.From.Add(from);
                MailboxAddress to = new MailboxAddress(toName, toMail);
                message.To.Add(to);
                message.Subject = subject;
                BodyBuilder bodyBuilder = new BodyBuilder
                {
                    HtmlBody = mail
                    //TextBody = mail
                };

                message.Body = bodyBuilder.ToMessageBody();
                SmtpClient client = new SmtpClient();
                client.Connect(smtp, port, ssl);
                client.Authenticate(fromEmail, password);
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
