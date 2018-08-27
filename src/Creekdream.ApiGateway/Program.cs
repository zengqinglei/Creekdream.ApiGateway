using Creekdream.Configuration.Apollo;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Creekdream.ApiGateway
{
    /// <inheritdoc />
    public class Program
    {
        /// <inheritdoc />
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <inheritdoc />
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (hostingContext, builder) =>
                    {
                        var apolloConfig = builder.Build().GetSection("apollo");
                        if (apolloConfig.Get<ApolloOptions>() != null)
                        {
                            builder.AddApollo(apolloConfig);
                        }
                    })
                .UseStartup<Startup>();
    }
}
