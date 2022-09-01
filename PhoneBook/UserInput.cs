using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class UserInput
    {
        public int GetNumInput(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine();
            while (!int.TryParse(input, out _) || int.Parse(input) < 0)
            {
                Console.WriteLine("Invalid input, try again");
                input = Console.ReadLine();
            }
            int finalInput = int.Parse(input);
            return finalInput;
        }

        public string GetStringInput(string message)
        {
            Console.Write(message);
            string name = Console.ReadLine();
            while (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("Invalid input,try again");
                name = Console.ReadLine();
            }
            return name;
        }
    }
}
