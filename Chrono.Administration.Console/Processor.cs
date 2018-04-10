using System;
using System.Linq;
using Chrono.Administration.Console.Commands;

namespace Chrono.Administration.Console
{
    public class Processor
    {
        const string ReadPrompt = "chrono> ";

        public void Run()
        {
            var isRunning = true;
            while (isRunning)
            {
                var consoleInput = ReadFromConsole();
                if (string.IsNullOrWhiteSpace(consoleInput))
                {
                    continue;
                }

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