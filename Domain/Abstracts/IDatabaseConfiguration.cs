namespace Domain.Abstracts
{
    public interface IDatabaseConfiguration
    {
        string ConnectionString { get; }
    }
}