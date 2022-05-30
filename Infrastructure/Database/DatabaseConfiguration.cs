using System.Diagnostics.CodeAnalysis;
using Domain.Abstracts.Database;
namespace Infrastructure.Database
{
    [ExcludeFromCodeCoverage]
    public sealed class DatabaseConfiguration : IDatabaseConfiguration
    {
        public bool InitDatabaseOnStartup { get; set; }
        public string ConnectionString { get; set; }
    }
}