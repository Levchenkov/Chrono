using System.Collections.Generic;
using System.Text;

namespace Chrono.Administration.Console.Commands
{
    public class HelpCommand : ICommand
    {
        public string HelpText
        {
            get
            {
                return "Returns help information for any command.";
            }
        }

        public string Name
        {
            get
            {
                return "Help";
            }
        }

        public string Execute(List<string> args)
        {
            var result = new StringBuilder();

            if(args.Count == 0)
            {
                foreach (var command in CommandHolder.Commands.Values)
                {
                    result.Append($"{command.Name}: {command.HelpText}\n");
                }

                return result.ToString();
            }
            else
            {
                var commandName = args[0].ToLower();
                if (CommandHolder.Commands.ContainsKey(commandName))
                {
                    var command = CommandHolder.Commands[commandName];
                    return $"{command.Name}: {command.HelpText}";
                }

                return $"Command {commandName} does not exist";
            }
        }
    }
}