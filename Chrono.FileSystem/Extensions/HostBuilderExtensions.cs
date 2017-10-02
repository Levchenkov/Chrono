using Chrono.FileSystem.Storages;
using Chrono.Host;

namespace Chrono.FileSystem.Extensions
{
    public static class HostBuilderExtensions
    {
        public static ChronoHostBuilder WithFileStorage(this ChronoHostBuilder hostBuilder)
        {
            hostBuilder.StorageProvider = settings => new FileStorage(settings);

            return hostBuilder;
        }
    }
}
