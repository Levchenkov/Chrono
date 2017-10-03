namespace Chrono.Administration
{
    public interface IChronoAdministrationContext
    {
        IChronoAdministrationService AdministrationService
        {
            get;
            set;
        }
    }
}
