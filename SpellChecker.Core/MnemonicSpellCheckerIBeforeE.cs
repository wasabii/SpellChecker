using System;
using System.Collections.Generic;
using SpellChecker.Contracts;
using System.Linq;

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
            // If I is before E preceding the letter C, the word is misspelled
            if (word.Contains("cie"))
            {
                return false;
            }

            // Only evaluate if the word contains an I and an E
            if (word.Contains("i") && word.Contains("e"))
            {
                // Split the word into characters
                var characters = new List<char>();

                foreach (char character in word)
                {
                    characters.Add(character);
                }

                // Find the index of i, e, and c
                int intLocOfI = characters.IndexOf('i');
                int intLocOfE = characters.IndexOf('e', intLocOfI);
                int intLocOfC = characters.IndexOf('c');

                if (intLocOfI < intLocOfE)
                {
                    return true;
                }
                else if (word.Contains("cei"))
                {
                    return true;
                }
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

    }

}
