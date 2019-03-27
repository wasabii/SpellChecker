using System;
using System.Threading.Tasks;

using SpellChecker.Contracts;

namespace SpellChecker.Core
{

    /// <summary>
    /// This spell checker implements the following rule:
    /// "I before E, except after C" is a mnemonic rule of thumb for English spelling.
    /// If one is unsure whether a word is spelled with the sequence ei or ie, the rhyme
    /// suggests that the correct order is ie unless the preceding letter is c, in which case it is ei.
    /// 
    /// Examples: believe, fierce, collie, die, friend, deceive, ceiling, receipt would be evaulated as spelled correctly
    /// heir, protein, science, seeing, their, and veil would be evaluated as spelled incorrectly.
    /// </summary>
    public class MnemonicSpellCheckerIBeforeE :
        ISpellChecker
    {

        /// <summary>
        /// Returns false if the word contains a letter sequence of "ie" when it is immediately preceded by c.
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>true when the word is spelled correctly, false otherwise</returns>
        public async Task<bool> Check(string word)
        {
            int ie = word.IndexOf("ie");
            int ei = word.IndexOf("ei");
            // LSotelo: Find the ie/ei sequences (if applicable) and apply the desired spelling rules.
            if (ie != -1)
            {
                if (word.Substring(ie-1,1) != "c")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (ei != -1){
                if (word.Substring(ei-1,1) != "c")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                // LSotelo: Return true for all other words not containing the ie or ei sequence of letters.
                return true;
            }
        }

    }

}
