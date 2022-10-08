using PhoneBook.Interfaces;
using PhoneBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    internal class UserInput : IUserInput
    {
        PhoneBookService phoneBookService = new PhoneBookService();

        public string GetEmailInput()
        {
            Console.Write("Type your email or type m to return to return to main menu:\n  ");
            string email = Console.ReadLine();
            if (email == "m") phoneBookService.ShowMenu();

            return email;
        }

        public string GetNumberInput(string message)
        {
            Console.WriteLine(message);
            string? input = Console.ReadLine();
            if (input == "m") phoneBookService.ShowMenu();
            // ta bort getnameinput metoden och använd bara getnumberinput
            return input;
        }

        public string GetNameInput()
        {
            Console.Write("Type your name or type m to return to return to main menu:\n  ");
            string name = Console.ReadLine();
            if (name == "m") phoneBookService.ShowMenu();
            return name;
        }
    }
}
