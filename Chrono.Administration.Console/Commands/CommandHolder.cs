using System.Collections.Generic;

namespace Chrono.Administration.Console.Commands
{
    public class CommandHolder
    {
        public static IDictionary<string, ICommand> Commands = new Dictionary<string, ICommand>()
        {
            { "help", new HelpCommand() },
            { "exit", new ExitCommand() },
            { "createsession", new CreateSessionCommand() },
            { "closesession", new CloseSessionCommand() }
        };
    }
}