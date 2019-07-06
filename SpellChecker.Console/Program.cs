using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using SpellChecker.Contracts;
using SpellChecker.Core;
using SpellChecker.Core.DependencyInjection;

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
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// This application is intended to allow a user enter some text (a sentence)
        /// and it will display a distinct list of incorrectly spelled words
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args)
        {
            RegisterServices();

            System.Console.Write("Please enter a sentence: ");
            var sentence = System.Console.ReadLine();

            var words = GetWords(sentence);
            var spellChecker = _serviceProvider.GetService<Core.SpellChecker>();

            var misspelledWords = new HashSet<string>();

            foreach (var word in words)
            {
                var isSpelledCorrectly = await spellChecker.Check(word);
                if (!isSpelledCorrectly)
                {
                    misspelledWords.Add(word);
                }
            }

            if (!misspelledWords.Any())
            {
                System.Console.WriteLine("No misspelled words");
                return;
            }

            System.Console.WriteLine("");
            System.Console.WriteLine("Misspelled words:");

            foreach (var word in misspelledWords)
            {
                System.Console.WriteLine($"- {word}");
            }

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();

            collection.AddCoreServices();

            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            var disposableServiceProvider = _serviceProvider as IDisposable;
            if (disposableServiceProvider == null)
            {
                return;
            }

            disposableServiceProvider.Dispose();
        }

        private static string[] GetWords(string sentence)
        {
            var punctuation = sentence.Where(Char.IsPunctuation).Distinct().ToArray();
            return sentence.Split().Select(x => x.Trim(punctuation)).ToArray();
        }
    }
}
