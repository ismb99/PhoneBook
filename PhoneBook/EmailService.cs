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
            //var from = "im_077@hotmail.com";
            //var to = "ismail@shamsen.se";
            //var subject = "Test mail";
            //var body = "Test body";

            //var host = "smtp.mailtrap.io";
            //var port = 2525;

            //var username = "02d35c9298efff"; // get from Mailtrap
            //var password = "935d7bd650f154"; // get from Mailtrap

            //var client = new SmtpClient(host, port)
            //{
            //    Credentials = new NetworkCredential(username, password),
            //    EnableSsl = true
            //};

            //client.Send(from, to, subject, body);

            //Console.WriteLine("Email sent");





            // from mailtrap dok
            //var client = new SmtpClient("smtp.mailtrap.io", 2525)
            //{
            //    Credentials = new NetworkCredential("02d35c9298efff", "935d7bd650f154"),
            //    EnableSsl = true
            //};
            //client.Send("im_077@hotmail.com", "im_077@hotmail.com", "Hello world", "testbody");
            //Console.WriteLine("Sent");
            //Console.ReadLine();








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
