using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public static class Menu
    {
        public static void ShowMenu()
        {
            
            bool closeApp = false;
            while (closeApp == false)
            {



                Console.WriteLine(@"What would you like todo ? Choose from the options below:
            1 - Add Contact
            2 - Delete Contact
            3 - Update Contact
            4 - Show all contacts
            5 - Show Contact by id
            6 - Quit");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("\n\n");

                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        ProccesAdd();
                        break;

                    case "2":
                        ContactController.Delete();
                        break;

                    //case "3":
                    //    ContactController.Update();
                    //    break;


                    case "4":
                        ContactController.GetAllContact();
                        break;

                    case "5":
                        ContactController.GetContact(2);
                        break;

                    case "6":
                        closeApp = true;
                        break;

                    default:
                        Console.WriteLine("Invalid input, try again");
                        break;
                }
            }
        }

        //private static void ProccesUpdate()
        //{
        //    UserInput userInput = new UserInput();

        //    int id = userInput.GetNumInput("Type the id you want to update");

        //    if(id == 0 || id < 0)
        //    {
        //        Console.WriteLine("Id dosent exsist");
        //    }

        //    string name = userInput.GetStringInput("Type the name: :");
        //    string number = userInput.GetStringInput("type the number: ");

        //    ContactController.Update(id);
        //}

        //private static void ProccesDelete()
        //{
        //    throw new NotImplementedException();
        //}

        private static void ProccesAdd()
        {
            // kolla om det är rätt input
            // validera data typer mm
            // anrpota Addmetoden och lägg in input

            Console.Write("Type contacts namne, or 0 to return to main menu: ");
            string inputName = Console.ReadLine();
            if (inputName == "0") Menu.ShowMenu();

            Console.Write("Type contacts number or 0 for main menu: ");
            string inputNumber = Console.ReadLine();
          
            if(inputNumber == "0") Menu.ShowMenu();

           ContactController.AddContact(inputName, inputNumber);

        }
    }
}
