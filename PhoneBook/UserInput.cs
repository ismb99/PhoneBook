using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    internal class UserInput
    {
        public static string GetuserInput(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid input, try again");
                input = Console.ReadLine();
            }
            return input;   
        }
    }
}
