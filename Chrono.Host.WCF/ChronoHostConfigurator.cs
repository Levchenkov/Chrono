namespace Chrono.Host.WCF
{
    public static class ChronoHostConfigurator
    {
        public static void ConfigureHost()
        {
            if (ChronoHostContext.Current == null)
            {
                ChronoHostContext.Current = new ChronoHostBuilder().WithInMemoryStorage().Build();
            }
        }
    }
}