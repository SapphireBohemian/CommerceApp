using Microsoft.AspNetCore.Identity;

namespace CommerceApp.Services
{
    // Services/DatabaseSeeder.cs
    public static class DatabaseSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            // Add roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Patient.ToString()));

            // Create admin user
            var admin = new IdentityUser
            {
                UserName = "admin@healthcare.com",
                Email = "admin@healthcare.com",
                EmailConfirmed = true
            };

            if (await userManager.FindByEmailAsync(admin.Email) == null)
            {
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }

    public enum Roles
    {
        Admin,
        Patient
    }
}
