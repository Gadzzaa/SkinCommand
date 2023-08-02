using System.IO;
using Microsoft.Extensions.Configuration;

namespace SkinCommand
{
    public class Startup
    {
        public Startup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);

            IConfiguration config = builder.Build();

            ApiSettings = config.GetSection("ApiSettings").Get<ApiSettings>();

        }

        public static ApiSettings ApiSettings { get; private set; }
    }
}