using Npgsql;
using System;
using System.Threading.Tasks;
using SimpleMigrations;
using SimpleMigrations.DatabaseProvider;

namespace Ascalon.ClientService.Migrations
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
                throw new NullReferenceException(nameof(connectionString));

            if (int.TryParse(Environment.GetEnvironmentVariable("DELAY"), out var delay))
                await Task.Delay(delay);

            using (var connection = new NpgsqlConnection(connectionString))
            {
                var databaseProvider = new PostgresqlDatabaseProvider(connection);
                var migrator = new SimpleMigrator(typeof(Program).Assembly, databaseProvider);

                migrator.Load();

                if (long.TryParse(Environment.GetEnvironmentVariable("MIGRATE_TO"), out var migrateTo))
                    migrator.MigrateTo(migrateTo);
                else
                    migrator.MigrateToLatest();
            }
        }
    }
}
