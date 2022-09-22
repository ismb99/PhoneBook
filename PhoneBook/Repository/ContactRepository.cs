using PhoneBook.Models;
using PhoneBook.Models.Data;
using PhoneBook.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly PhoneBookContext context;

        public ContactRepository(PhoneBookContext context)
        {
            this.context = context;
        }

        public Contacts AddContact(Contacts contacts)
        {
            context.Contacts.Add(contacts);
            context.SaveChanges();
            return contacts;
        }

        public Contacts Delete(Contacts contacts)
        {
            context.Contacts.Remove(contacts);
            context.SaveChanges();
            return contacts;
        }

        public IEnumerable<Contacts> GetAllContact()
        {
            return context.Contacts;
        }

        public Contacts Update(Contacts contacts)
        {
            context.Contacts.Update(contacts);
            context.SaveChanges();
            return contacts;
        }
    }
}
