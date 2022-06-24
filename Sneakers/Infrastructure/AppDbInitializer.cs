using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sneakers.Models;
using System.Linq;

namespace Sneakers.Infrastructure
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (!context.EMPLOYEE.Any())
                {
                    context.EMPLOYEE.AddRange(new EMPLOYEE()
                    {
                        Name = "Sarkhan",
                        Surname = "Hajibayov",
                        Email = "shacibyov@gmail.com",
                        PhoneNumber = "0553523596",
                        WorkEnter = System.DateTime.Now.AddDays(-110)


                    },
                    new EMPLOYEE()
                    {
                        Name = "Kenan",
                        Surname = "Naghiyev",
                        Email = "knaghiyev@gmail.com",
                        PhoneNumber = "0553523595",
                        WorkEnter = System.DateTime.Now.AddDays(-10)
                    },
                     new EMPLOYEE()
                     {
                         Name = "Xadica",
                         Surname = "Allahverdi",
                         Email = "xadica@gmail.com",
                         PhoneNumber = "0553523595",
                         WorkEnter = System.DateTime.Now.AddDays(-10)
                     });

                    context.SaveChanges();
                }
            }
        }
    }
}

