namespace Presentation
{
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    ///  Program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        ///     Application entrance.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateOnBuild = true;
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());
        }
    }
}