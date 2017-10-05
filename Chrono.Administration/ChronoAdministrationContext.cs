using System;

namespace Chrono.Administration
{
    //public class ChronoAdministrationContext : IChronoAdministrationContext
    //{
    //    public static IChronoAdministrationContext Current
    //    {
    //        get;
    //        set;
    //    }

    //    public IAdministrationService AdministrationService
    //    {
    //        get;
    //        set;
    //    }
    //}

    public class ChronoAdministrationContext<TAdminService> : IChronoAdministrationContext<TAdminService>
    {
        public static IChronoAdministrationContext<TAdminService> Current
        {
            get;
            set;
        }

        public TAdminService AdministrationService
        {
            get;
            set;
        }
    }
}
