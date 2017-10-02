using Chrono.FileSystem.Storages;
using Chrono.Host;

namespace Chrono.FileSystem.Extensions
{
    public static class HostBuilderExtensions
    {
        public static ChronoHostBuilder WithFileStorage(this ChronoHostBuilder hostBuilder)
        {
            hostBuilder.HostContext.Storage = new FileStorage();

            return hostBuilder;
        }
    }
}
