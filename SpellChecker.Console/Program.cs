using SpellChecker.Contracts;
using SpellChecker.Core;
using System;
using System.Collections.Generic;
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
            
            System.Console.Write("Please enter a sentence: ");
            var sentence = System.Console.ReadLine();
            // LSotelo: Create a regular expression to remove punctuation from the string. Any punctuation,
            // as in the example above, would result in incorrect misspellings (ex, "seashore." and "sea.").
            sentence = System.Text.RegularExpressions.Regex.Replace(sentence, @"[^\w\s]", "");

            // first break the sentence up into words, 
            // then iterate through the list of words using the spell checker
            // capturing distinct words that are misspelled
            string[] words = sentence.Split();
            string badWords = null;

            // use this spellChecker to evaluate the words
            var spellChecker = new Core.SpellChecker(new ISpellChecker[]
            {
                new MnemonicSpellCheckerIBeforeE(),
                new DictionaryDotComSpellChecker(),
            });

            // Loop through the array and check each individual word.
            for (int x = 0; x < words.GetUpperBound(0) + 1; x++)
            {
                if (spellChecker.Check(words[x]).Result == false)
                {
                    // Duplication check. If the word already exists in the "bad" words string, ignore it.
                    if (badWords.IndexOf(words[x]) != 0)
                    {
                        badWords += words[x] + " ";
                    }
                }
            }

            if (badWords != "")
            {
                System.Console.WriteLine(badWords);
            }
        }

    }

}
