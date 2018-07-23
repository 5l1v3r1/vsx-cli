using McMaster.Extensions.CommandLineUtils;
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

        public static void WriteResults<T>(this IConsole console, List<T> value) where T : class
        {

        }
    }
}
