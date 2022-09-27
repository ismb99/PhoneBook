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

        public PhoneBookController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void Post(Contacts contact)
        {
            _contactRepository.AddContact(contact);
        }

        public void Put(Contacts contact)
        {
            _contactRepository.Update(contact);
        }

        public void GetAll()
        {
            var phoneBookList = _contactRepository.GetAllContact().ToList();

            ContactVisualizer.ShowContacts(phoneBookList);
        }
        public void Get(int id)
        {
            _contactRepository.GetContact(id);
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

                    case "5":
                        ProcessGet();
                        break;

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

        private void ProcessGet()
        {
            string contactId = UserInput.GetuserInput("Choose contact id you want to view or press 0 for manin menu: ");

            int id = int.Parse(contactId);
            List<Contacts> contact = new();


            var foundContact = _contactRepository.GetContact(id);


            contact.Add(foundContact);

            ContactVisualizer.ShowContacts(contact);

            Get(foundContact.Id);

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
        private void ProcessUpdate()
        {
            GetAll();
            string input = UserInput.GetuserInput("Choose the id you want to update or press 0 for main menu: ");
            if (input == "0") ShowMenu();
            int id = int.Parse(input);

            var contact = _contactRepository.GetAllContact().FirstOrDefault(contact => contact.Id == id);

            if (contact != null)
            {
                string name = UserInput.GetuserInput("Update name: ");
                string phoneNumber = UserInput.GetuserInput("Update number: ");
                string email = UserInput.GetuserInput("Update email: ");

                contact.Name = name;
                contact.PhoneNumber = phoneNumber;
                contact.Emaill = email;
                Put(contact);

            }
            else
            {
                Console.Write("Id not found, try again");
                ProcessUpdate();
            }
        }

        // Delete contact
        private void ProcessDelete()
        {
            GetAll();

            string input = UserInput.GetuserInput("\nType the id you want to delete or press 0 to return to main menu:");
            if (input == "0") ShowMenu();
            
            while (!Validator.IsOnlyDigits(input)) // kolla även om id finns
            {
                input = UserInput.GetuserInput("\nType the id you want to delete or press 0 to return to main menu:");
                if (input == "0") ShowMenu();
            }
            int id = int.Parse(input);
            var alltContacts = _contactRepository.GetAllContact();

            var contact = alltContacts.Where(x => x.Id == id).FirstOrDefault();

            if (contact != null)
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
            var name = UserInput.GetuserInput("Enter name: ");
            while (!Validator.IsStringValid(name))
            {
                name = UserInput.GetuserInput("Enter name: ");
            }
            var number = UserInput.GetuserInput("Enter phonenumber: ");
            while (!Validator.IsOnlyDigits(number))
            {
                number = UserInput.GetuserInput("Enter phonenumber: ");
            }
            var email = UserInput.GetuserInput("Enter Email: ");
            while (!Validator.IsValidEmail(email))
            {
                 email = UserInput.GetuserInput("Enter Email: ");
            }

            Contacts newContact = new Contacts
            {
                Name = name,
                PhoneNumber = number,
                Emaill = email
            };

            Console.Write("Do you want to get a email with your contact information?, press y to send email, press any key to return to main menu: ");
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


