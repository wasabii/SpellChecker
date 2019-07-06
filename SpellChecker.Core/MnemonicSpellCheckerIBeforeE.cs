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
    public class MnemonicSpellCheckerIBeforeE : ISpellChecker
    {
        /// <summary>
        /// Returns false if the word contains a letter sequence of "ie" when it is immediately preceded by c.
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>true when the word is spelled correctly, false otherwise</returns>
        public Task<bool> Check(string word)
        {
            var ieIndex = word.IndexOf("ie");
            if (ieIndex != -1 && ieIndex != 0)
            {
                var previousChar = word[ieIndex - 1].ToString();
                if (previousChar.Equals("c", StringComparison.InvariantCultureIgnoreCase))
                {
                    return Task.FromResult(false);
                }
            }

            var eiIndex = word.IndexOf("ei");
            if (eiIndex != -1 && eiIndex != 0)
            {
                var previousChar = word[eiIndex - 1].ToString();
                if (!previousChar.Equals("c", StringComparison.InvariantCultureIgnoreCase))
                {
                    return Task.FromResult(false);
                }
            }

            return Task.FromResult(true);
        }
    }
}
