namespace Chrono.Administration
{
    public interface IChronoAdministrationContext
    {
        IAdministrationService AdministrationService
        {
            get;
            set;
        }
    }

    public interface IChronoAdministrationContext<TAdminService>
    {
        TAdminService AdministrationService
        {
            get;
            set;
        }
    }
}
