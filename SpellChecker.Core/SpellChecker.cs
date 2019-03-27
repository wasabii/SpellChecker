using System;
using System.Net;
using SpellChecker.Contracts;
using System.Threading.Tasks;

namespace SpellChecker.Core
{

    /// <summary>
    /// This is a top level spell checker that is used by clients, it internally manages
    /// several spell checkers that it uses to evaluate whether a word is spelled correctly
    /// or not.
    /// </summary>
    public class SpellChecker :
        ISpellChecker
    {

        readonly ISpellChecker[] spellCheckers;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="spellCheckers"></param>
        public SpellChecker(ISpellChecker[] spellCheckers)
        {
            this.spellCheckers = spellCheckers;
        }

        /// <summary>
        /// Iterates through all the internal spell checkers and returns false if any one of them finds a word to be
        /// misspelled
        /// </summary>
        /// <param name="word">Word to check</param>
        /// <returns>True if all spell checkers agree that a word is spelled correctly, false otherwise</returns>
        public async Task<bool> Check(string word)
        {
            // LSotelo: Variable used to ensure the correct condition is returned.
            bool spellCheck = false;
            // Call both spell checkers ansynchronously
            Task<bool> mnemonic = spellCheckers[0].Check(word);
            Task<bool> dictionary = spellCheckers[1].Check(word);
            try
            {
                if (mnemonic.Result == true && dictionary.Result == true)
                {
                    spellCheck = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                spellCheck = false;
            }
            return spellCheck;
        }

    }

}
