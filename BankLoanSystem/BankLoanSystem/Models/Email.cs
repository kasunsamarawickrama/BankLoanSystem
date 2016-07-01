using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace BankLoanSystem.Models
{
    public class Email
    {
        private string email;


        

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        /// Sending mail
        /// 
        /// to send a mail:
        /// // email address : string
        /// Email email = new Email(reciever email address);
        /// // body : string
        /// //message:string
        /// email.send(body , message)
        /// </summary>
        /// <returns></returns>
        /// 

        public Email(string email)
        {
            this.email = email;
            
        }

        public int SendMail(string body,string subject)
        {
            int success = 0;

            try
            {

                MailAddress toAddress = new MailAddress(email, this.email);
                
                SmtpClient smtp = new SmtpClient();
                MailAddress fromAddress = new MailAddress(((NetworkCredential)smtp.Credentials).UserName, "TFN");
                using (MailMessage message = new MailMessage()
                {
                    Subject = subject,
                    Body = body,
                    
                })

                {
                    message.IsBodyHtml = true;
                    message.From = fromAddress;
                    message.To.Add(new MailAddress(toAddress.ToString()));
                    smtp.Send(message);
                    

                    success = 1;
                }

            }
            catch
            {
                Console.WriteLine("error in sending mail");
                success = 0;
            }

            return success;
        }

        public void SendMailWithAttachment(string subject, string body, byte[] bytePdf)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient();
                mail.From = new MailAddress(((NetworkCredential)smtpServer.Credentials).UserName, "TFN");
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;

                MemoryStream ms = new MemoryStream(bytePdf);
                var attachment = new Attachment(ms, "Report", "application/pdf");
                mail.Attachments.Add(attachment);

                //if (System.IO.File.Exists(fullPath))
                //{
                //    var attachment = new Attachment(fullPath);
                //    mail.Attachments.Add(attachment);
                //}

                smtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}