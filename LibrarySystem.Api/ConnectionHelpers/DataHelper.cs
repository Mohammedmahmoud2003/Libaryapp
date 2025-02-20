
using LibrarySystem.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LibrarySystem.Api.ConnectionHelpers
{
    public static class DataHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            using var scope = svcProvider.CreateScope();
            var dbContextSvc = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

            try
            {
                // Apply pending migrations
                await dbContextSvc.Database.MigrateAsync();
                Console.WriteLine("Database migration applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration failed: {ex.Message}");
            }
        }
    }
}

