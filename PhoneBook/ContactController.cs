using PhoneBook.Models;
using PhoneBook.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class ContactController
    {
        public static PhoneBookContext dbContext = new PhoneBookContext();

        public static void AddContact(string name, string number)
        {
            var contact = new Contacts() { Name = name, PhoneNumber = number };

            dbContext.Add(contact);
            Console.WriteLine("Contact added!");

            dbContext.SaveChanges();

        }

        public static void Update(int id)
        {
            dbContext.Update(id);
            dbContext.SaveChanges();
        }

        public static void Delete()
        {

            Console.Write("Type the id you want to delete: ");
            string line = Console.ReadLine();
            int id = int.Parse(line);

            var context = new PhoneBookContext();
            var contactId = context.Contacts.Where(x => id == x.Id).ToList();

            foreach (var item in contactId)
            {
               if(item.Id == id)
                {
                    dbContext.Remove(item);
                    dbContext.SaveChanges(); 
                }
            }
        }


        public static void GetAllContact()
        {
            //var firstContact = dbContext.Contacts.First();

            Console.Clear();

            Console.WriteLine("All the contacts in the phonebook");
            Console.WriteLine("----------------------------------");
            var contacts = dbContext.Contacts;

            foreach (var person in contacts)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Name: {person.Name} Number: {person.PhoneNumber}");
                Console.WriteLine("\n");

            }

        }

        public static void GetContact(int id)
        {
            var contact = dbContext.Contacts.FirstOrDefault(x => x.Id == id);

            Console.WriteLine(contact.Name);

           
        }


        //while(!int.TryParse(line, out _) || int.Parse(line) < 0)
        //{
        //    Console.WriteLine("Invalid input, try again");
        //    line = Console.ReadLine();
        //}

        //int finalInput = int.Parse(line);
        //return finalInput;
    }

}

