using Domain.Abstracts;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    [ExcludeFromCodeCoverage]
    public sealed class ConnectionFactory : IConnectionFactory
    {
        #region fields & ctor

        /// <summary>
        ///     create an instance of <see cref="ConnectionFactory"/>
        /// </summary>
        /// <param name="provider">service provider</param>
        public ConnectionFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        private readonly IServiceProvider _provider;

        #endregion

        /// <summary>
        ///     create an instance of <see cref="IDbConnection"/> according to specific <see cref="T"/> configuration
        /// </summary>
        /// <typeparam name="T">database configuration</typeparam>
        /// <returns><see cref="IDbConnection"/></returns>
        public IDbConnection CreateConnection<T>() where T : class, IDatabaseConfiguration, new()
        {
            SqlConnection connection = default;
            try
            {
                var configuration = _provider.GetRequiredService<IOptions<T>>();
                if (string.IsNullOrWhiteSpace(configuration?.Value?.ConnectionString))
                    throw new InvalidOperationException($"missing configuration for {typeof(T).Name}");

                connection = new SqlConnection(configuration.Value.ConnectionString);
                connection.Open();
                return connection;
            }
            catch
            {
                connection?.Close();
                connection?.Dispose();
                throw;
            }
        }
    }
}