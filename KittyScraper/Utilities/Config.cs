using Microsoft.Extensions.Configuration;

namespace KittyScraper.Utilities
{
    public static class Config
    {
        private static readonly ConfigurationBuilder configBuilder = new ConfigurationBuilder();
        public static IConfiguration AppSettings = configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
    }
}
