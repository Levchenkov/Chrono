﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chrono.Administration.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChronoAdministrationConfigurator.Configure();
            //var client = new ChronoAdministrationServiceClient();

            //client.Close();

            var processor = new Processor();
            processor.Run();
        }        
    }

    public interface ICommand
    {
        string Name { get; }

        string HelpText { get; }

        string Execute(List<string> args);
    }

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

    public class CommandHolder
    {
        public static IDictionary<string, ICommand> Commands = new Dictionary<string, ICommand>()
        {
            { "help", new HelpCommand() },
            { "exit", new ExitCommand() }
        };
    }

    public class Processor
    {
        const string ReadPrompt = "chrono> ";

        public void Run()
        {
            var isRunning = true;
            while (isRunning)
            {
                var consoleInput = ReadFromConsole();
                if (string.IsNullOrWhiteSpace(consoleInput)) continue;

                try
                {
                    string result = Execute(consoleInput);

                    if(result == "exit")
                    {
                        isRunning = false;
                    }

                    WriteToConsole(result);
                }
                catch (Exception exception)
                {
                    WriteToConsole(exception.Message);
                }
            }
        }

        public string Execute(string consoleInput)
        {
            var parts = consoleInput.Split(' ').ToList();

            var mainPart = parts[0].ToLower();

            if (CommandHolder.Commands.ContainsKey(mainPart))
            {
                var args = parts.GetRange(1, parts.Count - 1);
                return CommandHolder.Commands[mainPart].Execute(args);
            }


            return $"Command {mainPart} does not exist";
        }

        public void WriteToConsole(string message = "")
        {
            if (message.Length > 0)
            {
                System.Console.WriteLine(message);
            }
        }
        
        public string ReadFromConsole(string promptMessage = "")
        {
            System.Console.Write(ReadPrompt + promptMessage);
            return System.Console.ReadLine();
        }
    }
}