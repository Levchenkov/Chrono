using System.Collections.Generic;

namespace Chrono.Administration.Console.Commands
{
    public interface ICommand
    {
        string Name { get; }

        string HelpText { get; }

        string Execute(List<string> args);
    }
}