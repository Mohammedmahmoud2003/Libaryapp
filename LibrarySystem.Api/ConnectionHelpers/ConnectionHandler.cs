using Npgsql;
using Microsoft.Extensions.Configuration;

namespace LibrarySystem.Api.ConnectionHelpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            // Attempt to get the connection string from configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // If not found, try to get it from the environment variable
            if (string.IsNullOrEmpty(connectionString))
            {
                var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                if (!string.IsNullOrEmpty(databaseUrl))
                {
                    connectionString = BuildConnectionString(databaseUrl);
                }
            }

            // If still not found, throw an exception
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            return connectionString;
        }

        // Build the connection string from the environment variable (e.g., Heroku)
        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer // Use SSL by default
                // TrustServerCertificate = true // Uncomment if needed
            };

            return builder.ToString();
        }
    }
}
