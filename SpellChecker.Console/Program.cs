using Microsoft.Extensions.DependencyInjection;
using SpellChecker.Contracts;
using SpellChecker.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            var sentence = System.Console.ReadLine();

            var misspelledWords = new List<string>();

            string[] words = sentence.Split(' ');

            var MnemonicServiceProvider = new ServiceCollection()
                    .AddScoped<ISpellChecker, MnemonicSpellCheckerIBeforeE>()
                    .BuildServiceProvider();

            var DictionaryServiceProvider = new ServiceCollection()
                    .AddScoped<ISpellChecker, DictionaryDotComSpellChecker>()
                    .BuildServiceProvider();
            var spellChecker = new Core.SpellChecker(new ISpellChecker[]
            {
                MnemonicServiceProvider.GetService<ISpellChecker>(),
                DictionaryServiceProvider.GetService<ISpellChecker>()
                //new MnemonicSpellCheckerIBeforeE(),
                //new DictionaryDotComSpellChecker(),
            });

            foreach (string word in words)
            {
                var sb = new StringBuilder();
                foreach (var c in word.Where(c => char.IsLetter(c)).Select(c => c))
                {
                    sb.Append(c);
                }
                var strippedWord = sb.ToString();

                System.Console.WriteLine("CHECKING \"{0}\"", strippedWord);
                if (!spellChecker.Check(strippedWord).Result && !misspelledWords.Contains(strippedWord))
                {
                    misspelledWords.Add(strippedWord);
                }
            }

            if (misspelledWords.Count == 0)
            {
                System.Console.Write("NO SPELLING MISTAKES FOUND.");
            }
            else
            {
                System.Console.Write("WORDS WITH SPELLING MISTAKES: ");
                foreach (var misspelledWord in misspelledWords)
                {
                    System.Console.Write("{0} ", misspelledWord);
                }
            }

            System.Console.ReadLine();

        }

    }

}
