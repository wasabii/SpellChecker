using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var sentence = System.Console.ReadLine();

            // first break the sentence up into words, 
            // then iterate through the list of words using the spell checker
            // capturing distinct words that are misspelled

            // use this spellChecker to evaluate the words
            var spellChecker = new Core.SpellChecker(new ISpellChecker[]
            {
                new MnemonicSpellCheckerIBeforeE(),
                new DictionaryDotComSpellChecker(),
            });

            
            Task.Run(() => TestSentence(sentence, spellChecker));
            System.Console.WriteLine("\nSpellcheckers are running...");

            System.Console.ReadKey();
        }

        #region Added Methods

        /// <summary>
        /// The asynchronous method to use inside Main.
        /// </summary>
        /// <param name="sentence">The sentence to test.</param>
        /// <param name="spellChecker">The spell checker to use.</param>
        public static async void TestSentence(string sentence, Core.SpellChecker spellChecker)
        {
            var badWords = new StringBuilder("Misspelled words: ");
            Dictionary<string, int> uniqueWords = GetUniqueWords(sentence);

            foreach (var word in uniqueWords.Keys)
            {
                bool isSpelledCorrectly = await spellChecker.Check(word);
                if (!isSpelledCorrectly)
                {
                    badWords.Append($" {word},");
                }
            }

            // Remove the last comma if present.
            if (badWords[badWords.Length - 1] == ',')
            {
                badWords.Remove(badWords.Length - 1, 1);
            }

            System.Console.WriteLine(badWords);
            System.Console.WriteLine("\n\nPress any key to quit.");
        }

        /// <summary>
        /// Get all unique words in the sentence. 
        /// </summary>
        /// <param name="sentence">The sentence to parse.</param>
        /// <returns>Collection of all unique words and how frequent they were.</returns>
        /// <remarks>
        /// I decided to go ahead and track which of the words are unique. It's not truly necessary,
        /// but could be useful for statistics. If memory is really an issue, use a List instead.
        /// </remarks>
        public static Dictionary<string, int> GetUniqueWords(string sentence)
        {
            var runOnSentence = new string(sentence.Where(c => !char.IsPunctuation(c) && !char.IsSymbol(c)).ToArray());
            string[] words = runOnSentence.Split(' ');
            var wordsDic = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!wordsDic.ContainsKey(word))
                {
                    wordsDic.Add(word, 1);
                }
                else
                {
                    wordsDic[word]++;
                }
            }

            return wordsDic;
        }

        #endregion
    }
}
