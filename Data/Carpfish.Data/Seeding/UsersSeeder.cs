namespace Carpfish.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Carpfish.Common;
    using Carpfish.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any(u => u.UserName == GlobalConstants.AdminName))
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser
            {
                UserName = GlobalConstants.AdminName,
                Email = GlobalConstants.AdminEmail,
            };

            await userManager.CreateAsync(user, GlobalConstants.AdminPass);
            await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
        }
    }
}
