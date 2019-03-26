using System;

using SpellChecker.Console.Bootstrap;
using SpellChecker.Contracts;

using Microsoft.Extensions.DependencyInjection;

namespace SpellChecker.Console
{
    public class Program
    {
        public Program(ISpellCheckerService spellCheckerService, IConsoleTool consoleTool)
        {
            SpellCheckerService = spellCheckerService;
            ConsoleTool = consoleTool;
        }

        private ISpellCheckerService SpellCheckerService { get; set; }

        private IConsoleTool ConsoleTool { get; set; }

        public static void Main(string[] args)
        {
            try
            {
                var serviceProvider = AutoBootstrapper.CreateServiceProvider();
                var program = serviceProvider.GetService<Program>();
                var sentence = program.GetSentenceToCheck();

                program.SpellCheckerService.RunSpellCheck(sentence);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

            System.Console.ReadKey();
        }

        public string GetSentenceToCheck()
        {
            ConsoleTool.WriteLine("Please enter a sentence...");
            return System.Console.ReadLine();
        }
    }
}
