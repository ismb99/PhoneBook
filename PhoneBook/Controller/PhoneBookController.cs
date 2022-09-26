using PhoneBook.Models;
using PhoneBook.Repository.IRepository;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Newtonsoft.Json;

namespace PhoneBook.Controller
{
    internal class PhoneBookController
    {
        private readonly IContactRepository _contactRepository;
        private readonly UserInput userInput = new UserInput();


        public PhoneBookController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void Post(Contacts contact)
        {
            _contactRepository.AddContact(contact);
        }

        public void GetAll()
        {
            var phoneBookList = _contactRepository.GetAllContact().ToList();

            ContactVisualizer.ShowContacts(phoneBookList);
        }

        public void Delete(int id)
        {
            _contactRepository.Remove(id);
        }

        // Menu

        public void ShowMenu()
        {
            bool closeApp = false;

            while (closeApp == false)
            {
                Console.WriteLine("\n");
                Console.WriteLine(@"What would you like todo ? Choose from the options below:
            1 - Add Contact
            2 - Delete a Contact
            3 - Update a Contact
            4 - View All contacts
            5 - Show Contact
            6 - Send Email
            7 - Quit");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("\n\n");

                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        Console.Clear();
                        ProcessAdd();
                        break;

                    case "2":
                        ProcessDelete();
                        break;

                    case "3":
                        ProcessUpdate();
                        break;

                    case "4":
                        GetAll();
                        break;

                    //case "5":
                    //    ContactController.GetContact(2);
                    //    break;

                    case "6":
                        SendAllContacts();
                        break;

                    case "7":
                        Console.WriteLine("Godbyee");
                        closeApp = true;
                        break;

                    default:
                        Console.WriteLine("Invalid input, try again");
                        break;
                }
            }
        }

        private void SendAllContacts()
        {
            Console.Clear();
            GetAll();

            var allContacts = _contactRepository.GetAllContact();

            Console.Write("Enter your email to get all the contacts: ");
            string email = Console.ReadLine();
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bingis khan", "im_077@hotmail.com"));
            message.To.Add(MailboxAddress.Parse(email));

            message.Subject = "Alla Kontakter";


            Console.Write("Password: ");
            string password = Console.ReadLine();

            SmtpClient client = new SmtpClient();

            try
            {
                client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("im_077@hotmail.com", password);

                foreach (var item in allContacts)
                {
                    message.Body = new TextPart("plain")
                    {
                        Text = $"{item.Name}"
                    };

                     client.Send(message);
                }

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

        // Update contact
        private static void ProcessUpdate()
        {
            //var allContacts = ContactController.GetAllContact();

            //using (var dbContext = new PhoneBookContext())
            //{
            //    Console.Write("choose the contact by name you want to update: ");
            //    string line = Console.ReadLine();
            //    Contacts contact = allContacts.FirstOrDefault(n => line == n.Name);

            //    if (contact != null)
            //    {
            //        Console.Write("Press 1 to update the name, press 2 to update number, press 3 to close app or 0 to return to main menu: ");
            //        string input = Console.ReadLine();

            //            switch (input)
            //            {
            //                case "0":
            //                    ShowMenu();
            //                    break;

            //                case "1":
            //                    Console.Clear();
            //                    Console.Write("Type the new name: ");
            //                    string name = Console.ReadLine();
            //                    while (string.IsNullOrEmpty(name) && name == input)
            //                    {
            //                        Console.WriteLine("Invalid input or name already exist, try again");
            //                        name = Console.ReadLine();
            //                    }
            //                    Console.WriteLine($"Name updated to {name}");
            //                    contact.Name = name;
            //                    break;

            //                case "2":
            //                    Console.Clear();
            //                    Console.Write("Type the new phonenumber: ");
            //                    string number = Console.ReadLine();
            //                    while (string.IsNullOrEmpty(number) && number == input)
            //                    {
            //                        Console.WriteLine("Invalid input or number already exist, try again");
            //                        number = Console.ReadLine();
            //                    }
            //                    Console.WriteLine($"phone number updated to {number}");

            //                    contact.PhoneNumber = number;
            //                    break;

            //                case "3":
            //                    Console.WriteLine("Goodbye");
            //                    break;

            //                default:
            //                    Console.WriteLine("Invalid input, try again");
            //                    break;
            //            }
            //        ContactController.Update(contact);
            //    }
            //    else
            //    {
            //        Console.WriteLine($"{contact} not found");
            //    }

            //};
        }

        // Delete contact
        private void ProcessDelete()
        {
            GetAll();


            string input = UserInput.GetuserInput("\nType the id you want to delete or press 0 to return to main menu:");
            if (input == "0") ShowMenu();
            int id = int.Parse(input);
            var alltContacts = _contactRepository.GetAllContact();

            var contact = alltContacts.Where(x => x.Id == id).FirstOrDefault();
            
            if(contact != null)
            {
                List<Contacts> idContact = new List<Contacts>();
                idContact.Add(contact);
                Console.WriteLine("\nThis contact was deleted");
                ContactVisualizer.ShowContacts(idContact);
                Delete(id);
            }

            else
            {
                string secondInput = UserInput.GetuserInput("\nId was not found!, press any key to tray again or press 0 for main menu: ");
                if (secondInput == "0") ShowMenu();

                else
                {
                    ProcessDelete();
                }
            }
        }

        // Create contact
        private void ProcessAdd()
        {
            //EmailService emailService = new EmailService();

            var name = UserInput.GetuserInput("Enter name: ");
            var number = UserInput.GetuserInput("Enter phonenumber: ");
            var email = UserInput.GetuserInput("Enter Email: ");

            Contacts newContact = new Contacts
            {
                Name = name,
                PhoneNumber = number,
                Emaill = email
            };

            Console.Write("Do you want to get a email with your contact information?, press y to send email, or n to skip: ");
            string sendEmail = Console.ReadLine();

            if (sendEmail == "y")
            {
                Console.Write("Enter admin Email: ");
                string adminEmail = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Bingis khan", $"{adminEmail}"));
                message.To.Add(MailboxAddress.Parse($"{email}"));

                message.Subject = "Telefonbok";
                message.Body = new TextPart("plain")
                {
                    Text = $"Name: {newContact.Name} Number: {newContact.PhoneNumber} Email: {newContact.Emaill}"
                };

                SmtpClient client = new SmtpClient();
                try
                {
                    client.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate(adminEmail, password);
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

                Post(newContact);

            }
            else
            {
                Post(newContact);
            }
        }
    }
}


