using System.Collections.Generic;
using Chrono.Host.Console;

namespace Chrono.Administration.Console.Commands
{
    public class CreateSessionCommand : ICommand
    {
        public string HelpText
        {
            get
            {
                return "Create a new session.";
            }
        }

        public string Name
        {
            get
            {
                return "CreateSession";
            }
        }

        public string Execute(List<string> args)
        {
            var session = ChronoAdministrationContext<IChronoAdministrationService>.Current.AdministrationService.CreateSession();

            return $"Session {session.Id} was created.";
        }
    }
}