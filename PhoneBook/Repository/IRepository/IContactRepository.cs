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
        IEnumerable<Contacts> GetAllContact();
        Contacts AddContact(Contacts contacts);
        Contacts Delete(Contacts contacts);
        Contacts Update(Contacts contacts);

    }
}
