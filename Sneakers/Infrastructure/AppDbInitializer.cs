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
           
            }
        }
    }
}

