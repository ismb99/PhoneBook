
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook;
using PhoneBook.Controller;
using PhoneBook.Models.Data;
using PhoneBook.Repository;
using PhoneBook.Repository.IRepository;


//EmailService.SendMail();

class Program
{


    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        host.Services.GetService<IContactRepository>();

        var contactRepository = host.Services.GetService<IContactRepository>();

        var phoneBookController = new PhoneBookController(contactRepository);

        Menu.ShowMenu();

    }


    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<IContactRepository, ContactRepository>();
                services.AddDbContext<PhoneBookContext>(options => options.UseSqlServer("Server=.;Database=PhoneBook;Trusted_Connection=True"));
            });
        return hostBuilder;
    }

}

