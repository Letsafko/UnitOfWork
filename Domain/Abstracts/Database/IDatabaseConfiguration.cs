namespace Domain.Abstracts.Database
{
    public interface IDatabaseConfiguration
    {
        string ConnectionString { get; }
    }
}