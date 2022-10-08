using PhoneBook.Interfaces;
using PhoneBook.Models;
using PhoneBook.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services
{
    internal class PhoneBookService : IPhoneBookService
    {
        private readonly IContactRepository _contactRepository;

        private readonly IUserInput _iUserInput;

        public PhoneBookService(IContactRepository contactRepository, IUserInput iUserInput)
        {
            _contactRepository = contactRepository;
            _iUserInput = iUserInput;
        }

        public PhoneBookService()
        {
        }

        private Contacts ProcessGet(int id)
        {
            string contactId = _iUserInput.GetNumberInput("Enter id you want to show: ");

            int inputId = int.Parse(contactId);
            List<Contacts> contact = new();


            var foundContact = _contactRepository.GetContactById(id);


            contact.Add(foundContact);

            ContactVisualizer.ShowContacts(contact);


            return foundContact;
            //Get(foundContact.Id);

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

            string inputId = GetNumberInput("\nType the id you want to update or press 0 to return to main menu: ");

            int id;

            id = int.Parse(inputId);

            var contact = _contactRepository.GetAllContact().FirstOrDefault(contact => contact.Id == id);

            if (contact != null)
            {
                int updateNumber;
                string updateName = GetNameInput();
                string updateEmail = GetEmailInput();
                string numberAsString = GetNumberInput("Enter your number: ");

                updateNumber = int.Parse(numberAsString);

                contact.Name = updateName;
                contact.PhoneNumber = updateNumber;
                contact.Emaill = updateEmail;
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
            Console.WriteLine("\n");

            string inputId = GetNumberInput("\nType the id you want to delete or press 0 to return to main menu: ");
            int id;
            while (!Validator.IsOnlyDigits(inputId))
            {
                Console.WriteLine("Invalid input try again");
                inputId = GetNumberInput("\nType the id you want to delete or press 0 to return to main menu: ");
            }

            id = int.Parse(inputId);


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
                Console.WriteLine("Id not found try again");
                ProcessDelete();
            }
        }

        // Create contact
        private void ProcessAdd()
        {
            var name = GetNameInput();
            var number = GetNumberInput("Enter you number: ");
            var email = GetEmailInput();

            int phoneNumber;

            while (!Validator.IsStringValid(name))
            {
                Console.WriteLine("invalid input try again");
                name = GetNameInput();
            }
            while (!Validator.IsOnlyDigits(number))
            {
                Console.WriteLine("invalid input try again");
                number = GetNumberInput("Enter you number: ");
            }

            phoneNumber = int.Parse(number);

            Contacts newContact = new Contacts
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Emaill = email
            };

            Console.Write("Do you want to get a email with your contact information?, press y to send email or return to main menu: ");
            string sendEmail = Console.ReadLine();

            if (sendEmail == "y")
            {
                SendMail(email, newContact);
                Post(newContact);
            }
            else
            {
                Post(newContact);
            }
        }



        private static void SendMail(string email, Contacts newContact)
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
        }

        public static string GetuserInput(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Empty input try again");
                input = Console.ReadLine();
            }
            return input;
        }

        public static int GetNumInput(string message)
        {
            Console.Write(message);
            var numberAsString = Console.ReadLine();

            int num;
            while (!int.TryParse(numberAsString, out num))
            {
                numberAsString = Console.ReadLine();
            }
            return num;
        }


        public void ShowMenu()
        {
            bool closeApp = false;

            while (closeApp == false)
            {
                Console.WriteLine("\nMain Menu");
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

                while (string.IsNullOrEmpty(userChoice))
                {
                    Console.WriteLine("\nInvalid input. Please type a number from 1 to 7\n");
                    userChoice = Console.ReadLine();
                }

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

     
    }

}
