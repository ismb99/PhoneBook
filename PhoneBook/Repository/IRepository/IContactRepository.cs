using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Repository.IRepository
{
    internal interface IContactRepository
    {
        List<Contacts> GetAllContact();
        Contacts AddContact(Contacts contacts);
        Contacts Remove(int id);
        Contacts Update(Contacts contacts);
        Contacts GetContactById(int id);

    }
}
