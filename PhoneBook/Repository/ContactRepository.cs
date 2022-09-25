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
        private readonly PhoneBookContext _context;

        public ContactRepository(PhoneBookContext context)
        {
            _context = context;
        }

        public Contacts AddContact(Contacts contacts)
        {
            _context.Contacts.Add(contacts);
            _context.SaveChanges();
            return contacts;
        }


        public Contacts Delete(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(i => i.Id == id);

            if(contact != null)
            {
                _context.Remove(contact);
                _context.SaveChanges();
                return contact;
            }

            return contact;
        }

        public List<Contacts> GetAllContact()
        {
            return _context.Contacts.ToList();
        }

        public Contacts Update(Contacts contacts)
        {
            _context.Contacts.Update(contacts);
            _context.SaveChanges();
            return contacts;
        }
    }
}
