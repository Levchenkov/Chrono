using System;

namespace Chrono.Administration
{
    public class ChronoAdministrationBuilder
    {
        private IChronoAdministrationContext context;

        public ChronoAdministrationBuilder()
        {
            context = new ChronoAdministrationContext();
        }

        internal Func<IChronoAdministrationService> AdministrationServiceProvider
        {
            get;
            set;
        }

        public ChronoAdministrationBuilder With(IChronoAdministrationService administrationService)
        {
            AdministrationServiceProvider = () => administrationService;

            return this;
        }

        public ChronoAdministrationContext Build()
        {
            context.AdministrationService = AdministrationServiceProvider();

            return context;
        }
    }
}
