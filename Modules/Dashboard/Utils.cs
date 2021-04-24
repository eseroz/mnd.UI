using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions; // mail gönderme işlemleri için gereklidir.

// regex komutunu kullanabilmemiz için gereklidir.

namespace mnd.UI.Modules.Dashboard
{
    public static class SendMail
    {
        public static bool Send(string MailHesabi, string MailHesapSifresi, string MailUnvan, string MailAdresi,
            string MailKonu, string MailIcerik, string MailEkleri)
        {

            var smtpUri = "smtp.office365.com";
            var port = 587;

            try
            {
                NetworkCredential cred = new NetworkCredential(MailHesabi, MailHesapSifresi);

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.From = new System.Net.Mail.MailAddress(MailHesabi, MailUnvan);

                foreach (var address in MailAdresi.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    mail.To.Add(address);
                }

                mail.Subject = MailKonu;
                mail.IsBodyHtml = true;
                mail.Body = MailIcerik;
                mail.Attachments.Clear();

                string[] sonuc1 = Regex.Split(MailEkleri, "/");

                foreach (string items in sonuc1)
                {
                    if (items != "")
                    {
                        mail.Attachments.Add(new Attachment("\\Mail_Eklerinin_Yolu\\" + items));
                    }
                }

                SmtpClient smtp = new SmtpClient(smtpUri, port);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = cred;
                smtp.Send(mail);


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}