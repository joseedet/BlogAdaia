
using BlogAdaia.Data;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Linq;

namespace BlogAdaia
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host= CreateWebHostBuilder(args).Build();
            try
            {

              var scope = host.Services.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated();

                var adminRole = new IdentityRole("Admin");

                if (!context.Roles.Any())
                {
                    // Crea rol
                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!context.Users.Any(u => u.UserName == "admin"))
                {
                    // Crea un admin 
                    var adminUser = new IdentityUser

                    {

                        UserName = "admin",

                        Email = "admin@tet.com"

                    };

                    var result = userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }

            }
            catch(Exception e)
            {

                Console.WriteLine(e.Message);
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
