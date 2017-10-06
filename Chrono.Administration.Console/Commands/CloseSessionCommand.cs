using System.Collections.Generic;
using Chrono.Host.Console;

namespace Chrono.Administration.Console.Commands
{
    public class CloseSessionCommand : ICommand
    {
        public string HelpText
        {
            get
            {
                return "Close the session by identifier.";
            }
        }

        public string Name
        {
            get
            {
                return "CloseSession";
            }
        }

        public string Execute(List<string> args)
        {
            if(args.Count > 0)
            {
                var sessionId = args[0];
                ChronoAdministrationContext<IChronoAdministrationService>.Current.AdministrationService.CloseSession(sessionId);
                return $"Session {sessionId} was closed.";
            }
            return "Please, provide sessionId.";
        }
    }
}