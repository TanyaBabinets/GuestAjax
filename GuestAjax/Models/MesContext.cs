using Microsoft.EntityFrameworkCore;
namespace GuestAjax.Models
{
    public class MesContext : DbContext
    {
       
            public DbSet<Users> users { get; set; }
            public DbSet<Messages> messages { get; set; }
            public MesContext(DbContextOptions<MesContext> options)
             : base(options)

            {
                if (Database.EnsureCreated())
                {
                    Users u1 = new Users {FirstName="Tanya", LastName="B",  Login = "Tanya", Password="111", Salt=""};
                    Users u2 = new Users { FirstName="Mikhail", LastName="Petrov", Login = "Misha", Password="222", Salt = "" };
                    Users u3 = new Users { FirstName = "Svetlana", LastName = "Svetova", Login = "Svetlana", Password = "333", Salt=""};
                DateTime now = DateTime.Now.Date;          
          
                messages?.Add(new Messages
                    {

                        user = u1,
                 Datetime = now,
                    message = "Все отлично, спасибо большое",
                        mark = 9,
                      
                    }); messages?.Add(new Messages
                    {

                        user = u2,
                        Datetime = now,
                        message = "Хорошая кухня, хотелось бы пиццу потоньше",
                        mark = 10,
                      

                    });
                    messages?.Add(new Messages
                    {

                        user = u3,
                        Datetime = DateTime.Now,
                        message = "В ванной течет труба. Утром слышен шум от стройки",
                        mark = 3,
                       

                    });
                    SaveChanges();
                }
            }
        }
    }






