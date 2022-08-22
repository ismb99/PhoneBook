using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models.Data
{
    public class PhoneBookContext : DbContext
    {
        public DbSet<Contacts> Contacts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\\Local;Initial Catalog=PhoneBook; Integrated Security=True");
            optionsBuilder.UseSqlServer(@"Server=(localdb)\\Local;Database=PhoneBook;Trusted_Connection=True;");
        }
    }
}
