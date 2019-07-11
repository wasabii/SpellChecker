using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpellChecker.Contracts;
using SpellChecker.Core;

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
        public static void Main(string[] args)
        {
            System.Console.Write("Please enter a sentence: ");
            var sentence = System.Console.ReadLine() ?? "";
            var words = sentence.ToLower().Split(new[] { ' ', '.', ',', '!', '?', ';' }, StringSplitOptions.RemoveEmptyEntries);
            var distinctWords = new HashSet<string>(words).ToList();
            
            var spellChecker = new Core.SpellChecker(new ISpellChecker[]
            {
                new MnemonicSpellCheckerIBeforeE(),
                new DictionaryDotComSpellChecker(),
            });

            var spellChecks = distinctWords.Select(distinctWord => spellChecker.CheckAsync(distinctWord)).ToList();
            Task.WaitAll(spellChecks.ToArray());

            var distinctMisspelledWords = distinctWords.Where((distinctWord, index) => !spellChecks[index].Result).ToList();
            if (distinctMisspelledWords.Any())
            {
                var misspelledWords = string.Join(" ", distinctMisspelledWords);
                System.Console.WriteLine($"Misspelled words: {misspelledWords}");
            }
            else
            {
                System.Console.WriteLine("There were no misspelled words!");
            }

            System.Console.ReadLine();
        }

    }
}
