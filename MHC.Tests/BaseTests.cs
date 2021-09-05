using Microsoft.Extensions.Configuration;

namespace MHC.Tests
{
    public class BaseTests
    {
        public IConfiguration Configuration { get; }

        public BaseTests()
        {
            Configuration = InitConfiguration();
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }
    }
}