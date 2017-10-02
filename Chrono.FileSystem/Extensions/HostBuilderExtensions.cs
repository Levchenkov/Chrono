using Chrono.FileSystem.Storages;
using Chrono.Host;

namespace Chrono.FileSystem.Extensions
{
    public static class HostBuilderExtensions
    {
        public static HostBuilder WithFileStorage(this HostBuilder hostBuilder)
        {
            hostBuilder.Host.Storage = new FileStorage();

            return hostBuilder;
        }
    }
}
