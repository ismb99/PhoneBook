using PhoneBook.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Controller
{
    internal class PhoneBookController
    {
        private readonly IContactRepository _contactRepository;

        public PhoneBookController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
    }
}
