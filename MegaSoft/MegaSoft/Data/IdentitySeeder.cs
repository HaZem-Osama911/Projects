using MegaSoft.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace MegaSoft.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider services, IConfiguration config, ILogger logger)
        {
            var enabled = config.GetValue<bool>("Seed:Enabled");
            if (!enabled)
            {
                return;
            }

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var roles = new[] { "ADMIN", "TECHNICAL_HEAD", "MANAGER", "EMPLOYEE" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var createRoleResult = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!createRoleResult.Succeeded)
                    {
                        logger.LogWarning("Failed to create role {Role}: {Errors}", role,
                            string.Join("; ", createRoleResult.Errors.Select(e => e.Description)));
                    }
                }
            }

            var adminEmail = config["Seed:AdminEmail"];
            var adminPassword = config["Seed:AdminPassword"];
            if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
            {
                await EnsureUserAsync(userManager, adminEmail.Trim(), adminPassword, "ADMIN", logger);
            }
            else
            {
                logger.LogInformation("Admin seed skipped (Seed:AdminEmail/AdminPassword not set).");
            }

            var employeePassword = config["Seed:EmployeePassword"];
            var employeePrefix = config["Seed:EmployeeEmailPrefix"] ?? "test";
            var employeeDomain = config["Seed:EmployeeEmailDomain"] ?? "gmail.com";
            var employeeCount = config.GetValue<int?>("Seed:EmployeeCount") ?? 0;

            if (!string.IsNullOrWhiteSpace(employeePassword) && employeeCount > 0)
            {
                for (var i = 1; i <= employeeCount; i++)
                {
                    var email = $"{employeePrefix}{i}@{employeeDomain}";
                    await EnsureUserAsync(userManager, email, employeePassword, "EMPLOYEE", logger);
                }
            }
            else
            {
                logger.LogInformation("Employee seed skipped (Seed:EmployeePassword/Seed:EmployeeCount not set).");
            }
        }

        private static async Task EnsureUserAsync(
            UserManager<ApplicationUser> userManager,
            string email,
            string password,
            string role,
            ILogger logger)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    IsActive = true
                };

                var createUserResult = await userManager.CreateAsync(user, password);
                if (!createUserResult.Succeeded)
                {
                    logger.LogWarning("Failed to create user {Email}: {Errors}", email,
                        string.Join("; ", createUserResult.Errors.Select(e => e.Description)));
                    return;
                }
            }

            if (!await userManager.IsInRoleAsync(user, role))
            {
                var addRoleResult = await userManager.AddToRoleAsync(user, role);
                if (!addRoleResult.Succeeded)
                {
                    logger.LogWarning("Failed to add role {Role} to user {Email}: {Errors}", role, email,
                        string.Join("; ", addRoleResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}

