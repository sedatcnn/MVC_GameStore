using Microsoft.AspNetCore.Identity;
using MVCProje.Models;

namespace MVCProje.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider serviceProvider)
        {
            var userMgr = serviceProvider.GetService<UserManager<IdentityUser>>();
            var roleMgr = serviceProvider.GetService<RoleManager<IdentityRole>>();

            await roleMgr.CreateAsync(new IdentityRole(Roles .Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.Kullanici.ToString()));

            var admin = new IdentityUser
            {
                 UserName = "sedat@gmail.com",
                 Email= "sedat@gmail.com",  
                 EmailConfirmed = true,

            };
            var isUserExists = await userMgr.FindByEmailAsync(admin.Email);
            

            if (isUserExists is  null)
            {
                await userMgr.CreateAsync(admin,"Sedat123_");
                await userMgr.AddToRoleAsync(admin,Roles.Admin.ToString());
            }

        }
    }
}
