using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    internal class ContactVisualizer
    {
        internal static void ShowContacts<T>(List<T> tableData) where T : class
        {
            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle("CONTACTS", ConsoleColor.Yellow, ConsoleColor.DarkGray)
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}
