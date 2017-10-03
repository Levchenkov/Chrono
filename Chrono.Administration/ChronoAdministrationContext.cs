namespace Chrono.Administration
{
    public class ChronoAdministrationContext : IChronoAdministrationContext
    {
        public static ChronoAdministrationContext Current
        {
            get;
            set;
        }

        public IChronoAdministrationService AdministrationService
        {
            get;
            set;
        }
    }
}
