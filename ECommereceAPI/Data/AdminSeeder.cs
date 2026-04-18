// Helpers/AdminSeeder.cs
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class AdminSeeder
{
    public static async Task SeedAdminAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // تأكد أن قاعدة البيانات تم إنشاؤها
        await context.Database.MigrateAsync();

        // تأكد إذا كان يوجد أدمن مسبقًا
        if (!context.Users.Any(u => u.Role == "Admin"))
        {
            var admin = new User
            {
                Username = "admin",
                Password = "123456", // يفضل لاحقًا تعملها هاش
                Role = "Admin"
            };

            context.Users.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}
