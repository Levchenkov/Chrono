namespace Chrono.Administration.Console
{
    public class ChronoAdministrationConfigurator
    {
        public static void Configure()
        {
            if(ChronoAdministrationContext.Current == null)
            {
                ChronoAdministrationContext.Current = new ChronoAdministrationBuilder().With(null).Build();
            }            
        }
    }
}
