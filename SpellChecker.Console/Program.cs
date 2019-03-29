using SpellChecker.Contracts;
using SpellChecker.Core;
using System.Collections.Generic;
using System.Linq;
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
            var sentence = "";
            while (sentence != ":q")
            {
                System.Console.WriteLine("Please enter a sentence: ");
                sentence = System.Console.ReadLine();

                if(string.IsNullOrWhiteSpace(sentence))
                {
                    System.Console.WriteLine("Umm I need a sentence...");
                    continue;
                }

                // first break the sentence up into words, 
                // then iterate through the list of words using the spell checker
                // capturing distinct words that are misspelled
                var distinctWords = sentence.Split(' ').ToArray().Where(w => !string.IsNullOrWhiteSpace(w)).Distinct();
                
                // use this spellChecker to evaluate the words
                var spellChecker = new Core.SpellChecker(new ISpellChecker[]
                {
                new MnemonicSpellCheckerIBeforeE(),
                new DictionaryDotComSpellChecker(),
                });

                var mispelledWords = new List<string>();
                foreach (var word in distinctWords)
                {
                    if (await spellChecker.Check(word) == false)
                        mispelledWords.Add(word);
                }

                if(mispelledWords.Count > 0)
                {
                    System.Console.WriteLine("For shame! Mispelled words: ");
                    foreach (var word in mispelledWords)
                    {
                        System.Console.WriteLine(word);
                    }
                    
                }
                else
                    System.Console.WriteLine("No mispelled words! I'm impressed.");

            }
        }

    }

}
