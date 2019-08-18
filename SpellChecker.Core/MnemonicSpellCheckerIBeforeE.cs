using System;

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
        public bool Check(string word)
        {
            var lower = word.ToLowerInvariant();
            if (lower.IndexOf("cie") > -1)
            {
                return false;
            }

            var eiIndex = lower.IndexOf("ei");
            if (eiIndex > -1)
            {
                // Word starts with ei
                if (eiIndex == 0)
                {
                    return false;
                }

                // Check if the ei is preceded by c
                return lower[eiIndex - 1] == 'c';
            }

            return true;
        }

    }

}
