using SpellChecker.Contracts;
using SpellChecker.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

            // use this spellChecker to evaluate the words
            var spellChecker = new Core.SpellChecker(new ISpellChecker[]
            {
                new MnemonicSpellCheckerIBeforeE(),
                new DictionaryDotComSpellChecker(),
            });

            // Remove all non alphanumeric characters from the sentence
            sentence = Regex.Replace(sentence, "[^a-zA-Z0-9 -]", "");

            // first break the sentence up into words, 
            string[] words = sentence.Split(" ");
            var misspelledWords = new List<string>();

            // then iterate through the list of words using the spell checker
            foreach (string word in words)
            {
                if (!spellChecker.Check(word))
                {
                    misspelledWords.Add(word);
                }
            }

            // capturing distinct words that are misspelled
            if (misspelledWords.Count() > 0)
            {
                System.Console.Write("Misspelled words: ");
                System.Console.WriteLine(string.Join(", ", misspelledWords.Distinct().ToArray()));
            } else
            {
                System.Console.WriteLine("No misspelled words.");
            }

            System.Console.ReadLine();
        }

    }

}
