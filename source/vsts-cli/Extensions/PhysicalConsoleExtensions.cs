using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace vsx.Extensions
{
    public static class PhysicalConsoleExtensions
    {
        public static void SpecifyASubcommand(this IConsole console, string command)
        {
            console.ForegroundColor = ConsoleColor.Yellow;
            console.WriteLine($"No command to execute. You must specify a subcommand for '{command}'.");
            console.WriteLine();
            console.ResetColor();
        }

        public static void ErrorMessage(this IConsole console, string message)
        {
            console.ForegroundColor = ConsoleColor.Red;
            console.WriteLine(message);
            console.ResetColor();
        }

        public static void WriteResults(this IConsole console, JObject jObject)
        {
            foreach (var property in jObject)
            {
                console.WriteLine("{0}: {1}", property.Key, property.Value);
            }
        }
    }
}
