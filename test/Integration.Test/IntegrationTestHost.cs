using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation;
namespace Integration.Test
{
    public sealed class IntegrationTestHost : IDisposable
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;
        public IntegrationTestHost(IntegrationTestFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory
                .WithWebHostBuilder(builder => ConfigureServices(builder));
        }

        private bool _disposed;
        public T GetRequiredService<T>() where T : class
        {
            return _webApplicationFactory.Services.GetRequiredService<T>();
        }

        public HttpClient CreateClient()
        {
            return _webApplicationFactory.CreateClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        #region private helpers
        
        private static IServiceCollection ConfigureServices(IWebHostBuilder builder)
        {
            return null;
        }
        
        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _webApplicationFactory?.Dispose();

            _disposed = true;
        }
        
        #endregion
    }
    
    public abstract class IntegrationTestFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, cb) =>
            {
                cb.AddJsonFile("appSettings.json", optional: false);
            });
            base.ConfigureWebHost(builder);
        }
    }
}