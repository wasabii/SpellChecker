using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpellChecker.Contracts;
using SpellChecker.Core;
using System;
using System.Threading.Tasks;

namespace SpellChecker.Console
{

    /// <summary>
    /// The following are the "requirements" for this project:
    /// 
    /// 1. Implement Main() below so that a user can input a sentence.  Each word in that
    ///    sentence will be evaluated with the SpellChecker, which returns true for a word
    ///    that is spelled correctly and false for a word that is spelled incorrectly.  Display
    ///    out each *distinct* word that is misspelled.  That is, if a user uses the same misspelled
    ///    word more than once, simply output that word one time.
    ///    
    ///    Example:
    ///    Please enter a sentence: Salley sells seashellss by the seashore.  The shells Salley sells are surely by the sea.
    ///    Misspelled words: Salley seashellss
    ///    
    /// 2. The concrete implementation of SpellChecker depends on two other implementations of <see cref="ISpellChecker"/>, <see cref="DictionaryDotComSpellChecker"/>
    ///    and MnemonicSpellCheckerIBeforeE.  You will need to implement those classes.  See those classes for details.
    ///    
    /// 3. There are covering unit tests in the SpellChecker.Tests library that should be implemented as well.
    /// 
    /// For extra credit, consider the following modifications:
    /// 
    /// 1. Convert to async.
    /// 2. Implement Dependency Injection (framework of your choice).
    /// 3. Dynamic loading of checking instances.
    /// 
    /// </summary>
    public static class Program
    {

        /// <summary>
        /// This application is intended to allow a user enter some text (a sentence)
        /// and it will display a distinct list of incorrectly spelled words
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging((options) =>
                {
                    options.AddConsole();
                })
                .AddSingleton<MnemonicSpellCheckerIBeforeE>()
                .AddTransient<DictionaryDotComSpellChecker>()
                .BuildServiceProvider();

            System.Console.Write("Please enter a sentence: ");
            var sentence = System.Console.ReadLine();

            // first break the sentence up into words, 
            // then iterate through the list of words using the spell checker
            // capturing distinct words that are misspelled

            var words = sentence.Split(' ');

            System.Console.WriteLine("Select spell checker to use. [d]otcom, [m]nemonic, or press any key to use both.");

            var key = System.Console.ReadKey().Key;

            var checkersList = new System.Collections.Generic.List<ISpellChecker>();

            switch (key)
            {
                case ConsoleKey.D:
                    checkersList.Add(serviceProvider.GetService<DictionaryDotComSpellChecker>());
                    break;
                case ConsoleKey.M:
                    checkersList.Add(serviceProvider.GetService<MnemonicSpellCheckerIBeforeE>());
                    break;
                default:
                    checkersList.Add(serviceProvider.GetService<MnemonicSpellCheckerIBeforeE>());
                    checkersList.Add(serviceProvider.GetService<DictionaryDotComSpellChecker>());
                    break;
            }

            var spellChecker = new Core.SpellChecker(checkersList.ToArray());

            var misspelledWords = new System.Collections.Generic.List<string>();

            foreach (var word in words)
            {
                if (!await spellChecker.Check(word))
                {
                    if (!misspelledWords.Contains(word))
                        misspelledWords.Add(word);
                }
            }

            foreach (var misspelledWord in misspelledWords)
                System.Console.WriteLine(misspelledWord);

        }

    }

}
