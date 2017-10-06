using System.Collections.Generic;

namespace Chrono.Administration.Console.Commands
{
    public class ExitCommand : ICommand
    {
        public string HelpText
        {
            get
            {
                return "Exit from program.";
            }
        }

        public string Name
        {
            get
            {
                return "Exit";
            }
        }

        public string Execute(List<string> args)
        {
            return "exit";
        }
    }
}