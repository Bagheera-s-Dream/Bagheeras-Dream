namespace Bagheeras.Dream.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Bagheeras.Dream.Common;
    using Bagheeras.Dream.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class AdministratorSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var administrationRole = await roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);

            if (administrationRole == null)
            {
               var a = new RolesSeeder();
               await a.SeedAsync(dbContext, serviceProvider);
               administrationRole = await roleManager.FindByNameAsync(GlobalConstants.AdministratorRoleName);
            }

            var adminProfile = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == "nina.manova@abv.bg");
            if (adminProfile == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "nina.manova@abv.bg",
                    UserName = "Nina",
                    PasswordHash = "AQAAAAEAACcQAAAAEPcq9ukr7W8o+2J2LVcXzuPM0FrICNGHS+PkuXC+yt93H25kkqLbMAH9uMrwwb67oQ==",
                };

                await dbContext.Users.AddAsync(user);

                await dbContext.UserRoles.AddAsync(new Microsoft.AspNetCore.Identity.IdentityUserRole<string>()
                {
                    RoleId = administrationRole.Id,
                    UserId = user.Id,
                });
            }
        }
    }
}
