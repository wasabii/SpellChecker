using SpellChecker.Contracts;
using SpellChecker.Core;
using System;
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
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            Print("Please enter a sentence: ");
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
            Print("");
            Print("Checking spelling...Please wait.");
            Print("");
            //Make a list of tasks to run through.
            var tasks = new List<Task<string>>();
            //distinct & lower so we don't have to look at the same word twice.
            foreach (string word in sentence.ToLower().Split(' ').Distinct())
            {
                tasks.Add(RunCheck(TrimPunctuation(word), spellChecker));
            }
            //Get all the words that failed.
            var badWords = (await Task.WhenAll(tasks).ConfigureAwait(false)).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();


            Print("");
            Print("");
            if(badWords.Count > 0)
            {
                //Print out the list.
                //Let the user know they did BAD.
                PrintInvalidMessage(badWords);   
            } else
            {
                //Let the user know they did good.
                PrintSuccessMessage();
            }

            Print("");
            Print("Press any key to exit.");
            System.Console.ReadKey();

        }

        private static void PrintInvalidMessage(List<string> badWords)
        {
            Print("===============================================");
            Print("= Miss Salley Spellings' List of Misspellings =");
            Print("===============================================");
            foreach (string word in badWords)
            {
                Print(word);
            }
        }

        private static void PrintSuccessMessage()
        {
            Print("================================================");
            Print("= Look at you, you're really good at spelling! =");
            Print("================================================");
            Print(" ________________________");
            Print("|.----------------------.|");
            Print("||                      ||");
            Print("||       ______         ||");
            Print("||     .;;;;;;;;.       ||");
            Print("||    /;;;;;;;;;;;\\     ||");
            Print("||   /;/`    `-;;;;; . .||");
            Print("||   |;|__  __  \\;;;|   ||");
            Print("||.-.|;| e`/e`  |;;;|   ||");
            Print("||   |;|  |     |;;;|'--||");
            Print("||   |;|  '-    |;;;|   ||");
            Print("||   |;;\\ --'  /|;;;|   ||");
            Print("||   |;;;;;---'\\|;;;|   ||");
            Print("||   |;;;;|     |;;;|   ||");
            Print("||   |;;.-'     |;;;|   ||");
            Print("||'--|/`        |;;;|--.||");
            Print("||;;;;    .     ;;;;.\\;;||");
            Print("||;;;;;-.;_    /.-;;;;;;||");
            Print("||;;;;;;;;;;;;;;;;;;;;;;||");
            Print("||jgs;;;;;;;;;;;;;;;;;;;||");
            Print("'------------------------'");
        }

        /// <summary>
        /// DRY method
        /// </summary>
        /// <param name="msg"></param>
        private static void Print(string msg)
        {
            System.Console.WriteLine(msg);
        }

        private static async Task<string> RunCheck(string word, ISpellChecker checker)
        {
            var isCorrect = await checker.CheckAsync(word);
            return isCorrect ? "" : word;
        }

        private static string TrimPunctuation(string word)
        {
            return new string(word.Where(c => !char.IsPunctuation(c)).ToArray()).Trim();
        }

    }

}
