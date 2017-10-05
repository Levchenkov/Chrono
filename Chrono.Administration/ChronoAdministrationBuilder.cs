using System;

namespace Chrono.Administration
{
    //public class ChronoAdministrationBuilder
    //{
    //    private IChronoAdministrationContext context;

    //    public ChronoAdministrationBuilder()
    //    {
    //        context = new ChronoAdministrationContext();
    //    }

    //    internal Func<IAdministrationService> AdministrationServiceProvider
    //    {
    //        get;
    //        set;
    //    }

    //    public ChronoAdministrationBuilder With(IAdministrationService administrationService)
    //    {
    //        AdministrationServiceProvider = () => administrationService;

    //        return this;
    //    }

    //    public IChronoAdministrationContext Build()
    //    {
    //        context.AdministrationService = AdministrationServiceProvider();

    //        return context;
    //    }
    //}

    public class ChronoAdministrationBuilder<TAdminService>
    {
        private IChronoAdministrationContext<TAdminService> context;

        public ChronoAdministrationBuilder()
        {
            context = new ChronoAdministrationContext<TAdminService>();
        }

        internal Func<TAdminService> AdministrationServiceProvider
        {
            get;
            set;
        }

        public ChronoAdministrationBuilder<TAdminService> With(TAdminService administrationService)
        {
            AdministrationServiceProvider = () => administrationService;

            return this;
        }

        public IChronoAdministrationContext<TAdminService> Build()
        {
            context.AdministrationService = AdministrationServiceProvider();

            return context;
        }
    }
}
