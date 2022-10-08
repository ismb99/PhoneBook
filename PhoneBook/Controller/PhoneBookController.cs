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
        //private readonly IContactRepository _contactRepository;
        private readonly IPhoneBookService _IPhoneBookService;


        //public PhoneBookController(IContactRepository contactRepository)
        //{
        //    _contactRepository = contactRepository;
        //}

        public void Post(Contacts contact)
        {
            //_contactRepository.AddContact(contact);
        }

        public void Put(Contacts contact)
        {
            _contactRepository.Update(contact);
        }

        public void GetAll()
        {
            // Var contacts = _contactService.GetAllContact();
            //var phoneBookList = _contactRepository.GetAllContact().ToList();

            ContactVisualizer.ShowContacts(phoneBookList);
        }
        public void Get(int id)
        {
            //_contactRepository.GetContactById(id);
            
            var resulat = _IPhoneBookService.Processget(id);
            // Tablevisualation (resulat)
            

        }
        public void Delete(int id)
        {
            //_contactRepository.Remove(id);
        }

       

        
    }
}


