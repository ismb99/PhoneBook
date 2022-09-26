using System;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;

namespace PhoneBook
{
    internal class EmailService
    {
        public static void SendMail()
        {


            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bingis khan", "im_077@hotmail.com"));
            message.To.Add(MailboxAddress.Parse("mr_marocci_87@hotmail.com"));

            message.Subject = "Hej";

            message.Body = new TextPart("plain")
            {
                Text = @"Hej,
                  Snälla Sluta Slössa Tid!."
            };

            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(email, password);
                client.Send(message);

                Console.WriteLine("Email sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }



        }

    }
}
