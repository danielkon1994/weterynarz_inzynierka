using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weterynarz.Basic.Const;
using Weterynarz.Domain.EntitiesDb;

namespace Weterynarz.Web.Extensions
{
    public class CreateSuperUser
    {
        public static async Task Create(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Create roles
            string[] roles = UserRoles.GetUserRolesList();
            IdentityResult identityResult;

            foreach(string role in roles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);
                if(!roleExists)
                {
                    identityResult = await roleManager.CreateAsync(new IdentityRole() { Id = role, Name = role });
                }
            }

            // Create user
            var user = new ApplicationUser()
            {
                Name = "admin",
                UserName = "admin",
                Email = "admin@admin.pl",
                EmailConfirmed = true,
            };
            string userPass = "Admin123!";

            var checkUser = await userManager.FindByNameAsync(user.Name);
            if(checkUser == null)
            {
                var createUser = await userManager.CreateAsync(user, userPass);
                if(createUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                }
            }
        }
    }
}
