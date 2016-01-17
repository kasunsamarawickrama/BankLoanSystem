using System;
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

                MailAddress toAddress = new MailAddress(email, "To Name");
                
                SmtpClient smtp = new SmtpClient();
                MailAddress fromAddress = new MailAddress(((NetworkCredential)smtp.Credentials).UserName, "From Name");
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
            catch (Exception ex)
            {
                Console.WriteLine("error in sending mail");
                success = 0;
            }

            return success;
        }
    }
}