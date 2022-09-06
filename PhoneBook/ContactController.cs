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
        //public static PhoneBookContext dbContext = new PhoneBookContext();

        public static void AddContact(string name, string number)
        {
            var contact = new Contacts() { Name = name, PhoneNumber = number };
            using (var dbContext = new PhoneBookContext())
            {
                dbContext.Add(contact);
                Console.WriteLine("\n\nContact added!");
                dbContext.SaveChanges();
            }
        }

        public static void Update(Contacts contact)
        {
            using (var dbContext = new PhoneBookContext())
            {
                dbContext.Update(contact);
                dbContext.SaveChanges();
            }
        }

        public static void Delete(Contacts contact)
        {
            using (var dbContext = new PhoneBookContext())
            {
                dbContext.Remove(contact);
                dbContext.SaveChanges();
            }
        }

        public static List<Contacts> GetAllContact()
        {
            using (var dbContext = new PhoneBookContext())
            {
                var allContacts = dbContext.Contacts.ToList();
                ContactVisualizer.ShowContacts(allContacts);
                return allContacts;
            }
        }

        public static void GetContact(int id)
        {
            using (var dbContext = new PhoneBookContext())
            {
                var contact = dbContext.Contacts.FirstOrDefault(x => x.Id == id);
                Console.WriteLine(contact.Name);
            }

        }
       
    }

}

