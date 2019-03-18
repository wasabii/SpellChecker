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
        /// Returns false if the word contains a letter sequence of "ie" when it is immediately preceded by c
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>true when the word is spelled correctly, false otherwise</returns>
        public bool Check(string word)
        {
            word = word.ToLower();
            for (int i=0; i < word.Length; i++)
            {
                char letter = word[i];
                if (letter == 'c')
                {
                    if (i+2 < word.Length)
                    {
                        if ((word[i + 1] == 'i') && (word[i + 2] == 'e'))
                            return false;
                    }
                }

            }

            return true;
        }

    }

}
