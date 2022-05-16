using System.Diagnostics.CodeAnalysis;
using Domain.Abstracts;

namespace Infrastructure
{
    [ExcludeFromCodeCoverage]
    public sealed class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string ConnectionString { get; set; }
    }
}