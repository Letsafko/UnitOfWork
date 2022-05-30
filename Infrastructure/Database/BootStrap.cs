using System;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace Infrastructure.Database
{
    public class BootStrap
    {
        private readonly DatabaseConfiguration _databaseConfiguration;
        public BootStrap(IOptions<DatabaseConfiguration> options)
        {
            _databaseConfiguration = options.Value ??
                throw new InvalidOperationException("unable to retrieve database configuration.");
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(_databaseConfiguration.ConnectionString);
            if (_databaseConfiguration.InitDatabaseOnStartup)
            {
                    
            }
        }
    }
}