using LegalEazeRewrite.Models.DataModels;
using Microsoft.AspNetCore.Identity;

namespace LegalEazeRewrite.Services
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<User> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "lawyer", "manager", "admin" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Check if the admin user exists, and create it if not
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                var admin = new User
                {
                    UserName = "michaelkiffer@gmail.com",
                    Email = "michaelkiffer@gmail.com",
                    EmailConfirmed = true
                };

                var createAdminResult = await userManager.CreateAsync(admin, "Admin@123");
                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
