using PhoneBook.Models;
using PhoneBook.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public static class Menu
    {
        public static void ShowMenu()
        {
            bool closeApp = false;

            while (closeApp == false)
            {
                Console.WriteLine("\n");
                Console.WriteLine(@"What would you like todo ? Choose from the options below:
            1 - Create a Contact
            2 - Delete a Contact
            3 - Update a Contact
            4 - View all contacts
            5 - Show Contact by id
            6 - Quit");
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
                        ContactController.GetAllContact();
                        break;

                    case "5":
                        ContactController.GetContact(2);
                        break;

                    case "6":
                        Console.WriteLine("Godbyee");
                        closeApp = true;
                        break;

                    default:
                        Console.WriteLine("Invalid input, try again");
                        break;
                }
            }
        }

        private static void ProcessUpdate()
        {
            var allContacts = ContactController.GetAllContact();

            using (var dbContext = new PhoneBookContext())
            {
                Console.Write("choose the contact by name you want to update: ");
                string line = Console.ReadLine();
                Contacts contact = allContacts.FirstOrDefault(n => line == n.Name);

                if (contact != null)
                {
                    Console.Write("Press 1 to update the name, press 2 to update number, press 3 to close app or 0 to return to main menu: ");
                    string input = Console.ReadLine();

                  
                    
                        switch (input)
                        {
                            case "0":
                                ShowMenu();
                                break;

                            case "1":
                                Console.Clear();
                                Console.Write("Type the new name: ");
                                string name = Console.ReadLine();
                                while (string.IsNullOrEmpty(name) && name == input)
                                {
                                    Console.WriteLine("Invalid input or name already exist, try again");
                                    name = Console.ReadLine();
                                }
                                Console.WriteLine($"Name updated to {name}");
                                contact.Name = name;
                                break;

                            case "2":
                                Console.Clear();
                                Console.Write("Type the new phonenumber: ");
                                string number = Console.ReadLine();
                                while (string.IsNullOrEmpty(number) && number == input)
                                {
                                    Console.WriteLine("Invalid input or number already exist, try again");
                                    number = Console.ReadLine();
                                }
                                Console.WriteLine($"phone number updated to {number}");

                                contact.PhoneNumber = number;
                                break;

                            case "3":
                                Console.WriteLine("Goodbye");
                                break;

                            default:
                                Console.WriteLine("Invalid input, try again");
                                break;
                        }
                    ContactController.Update(contact);
                }
                else
                {
                    Console.WriteLine($"{contact} not found");
                }
                
            };
        }

        private static void ProcessDelete()
        {
            var allContacts = ContactController.GetAllContact();

            Console.Write("Type the name you want to delete or press 0 to return to main menu: ");
            string line = Console.ReadLine();
            if (line == "0") ShowMenu();

            var name = allContacts.FirstOrDefault(n => line == n.Name);

            if (name != null)
            {
                Console.WriteLine($"found {name.Name}");
            }
            else
            {
                Console.WriteLine($"{name} not found");
            }

            ContactController.Delete(name);
        }

        private static void ProcessAdd()
        {
            Console.Write("Type contacts name, or 0 to return to main menu: ");

            string name = Console.ReadLine();
            if (name == "0") Menu.ShowMenu();

            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name can not be empty, try again");
                name = Console.ReadLine();
            }

            Console.Write("Type contacts number or 0 for main menu: ");
            string number = Console.ReadLine();

            if (number == "0") Menu.ShowMenu();
            while (string.IsNullOrEmpty(number))
            {
                Console.WriteLine("Name can not be empty, try again");
                number = Console.ReadLine();
            }
            ContactController.AddContact(name, number);
        }
    }
}
