using System.Data;

namespace Domain.Abstracts
{
    public interface IConnectionFactory
    {
        /// <summary>
        ///     create an instance of <see cref="IDbConnection"/>
        /// </summary>
        IDbConnection CreateConnection<T>() where T : class, IDatabaseConfiguration, new();
    }
}