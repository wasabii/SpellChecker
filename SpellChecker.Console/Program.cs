using Microsoft.Extensions.DependencyInjection;
using SpellChecker.Contracts;
using SpellChecker.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
    public class Program
    {
        /// <summary>
        /// This application is intended to allow a user enter some text (a sentence)
        /// and it will display a distinct list of incorrectly spelled words
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args)
        {
            //var serviceProvider = ConfigureServiceProvider();

            System.Console.Write("Please enter a sentence: ");
            var sentence = System.Console.ReadLine();

            // first break the sentence up into words, 
            // then iterate through the list of words using the spell checker
            // capturing distinct words that are misspelled
            var words = ParseSentence(sentence);

            // use this spellChecker to evaluate the words
            var spellChecker = new Core.SpellChecker(new ISpellChecker[]
            {
                new MnemonicSpellCheckerIBeforeE(),
                new DictionaryDotComSpellChecker(),
            });

            // TODO - Fix environmental issues with resolving the DI package in .net standard
            //var spellChecker = serviceProvider.GetService<Core.SpellChecker>();

            // Execute spell check
            var tasks = new List<Task<string>>();
            foreach (var word in words)
            {
                tasks.Add(RunCheck(word, spellChecker));
            }

            var misspelledWords = (await Task.WhenAll(tasks).ConfigureAwait(false)).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();

            // Print word list
            System.Console.WriteLine($"{PrintMisspelledWords(misspelledWords)}");

            System.Console.WriteLine("Spellcheck has finished. Press any key to exit...");
            System.Console.ReadLine();
        }

        //private static IServiceProvider ConfigureServiceProvider()
        //{
        //    var services = new ServiceCollection();

        //    return services
        //        .AddSingleton<ISpellChecker>(new Core.SpellChecker(new ISpellChecker[]
        //            {
        //                new MnemonicSpellCheckerIBeforeE(),
        //                new DictionaryDotComSpellChecker(),
        //            }))
        //        .BuildServiceProvider();
        //}

        private static async Task<string> RunCheck(string word, ISpellChecker checker)
        {
            var isCorrect = await checker.Check(word);

            return isCorrect ? "" : word;
        }

        private static List<string> ParseSentence(string sentence)
        {
            return sentence.Split(' ').Select(word => TrimPunctuation(word)).ToList();
        }

        private static string TrimPunctuation(string word)
        {
            return new string(word.Where(c => !char.IsPunctuation(c)).ToArray()).Trim();
        }

        private static string PrintMisspelledWords(List<string> words)
        {
            var builder = new StringBuilder();
            builder.Append("Mispelled words:");
            foreach (var word in words)
            {
                builder.Append($" {word}");
            }

            return builder.ToString();
        }
    }
}
