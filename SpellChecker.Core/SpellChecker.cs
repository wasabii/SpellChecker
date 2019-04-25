﻿
using SpellChecker.Contracts;

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
        public bool Check(string word)
        {
#pragma warning disable IDE0059 // Value assigned to symbol is never used
            var containsSpellingErrors = false;
#pragma warning restore IDE0059 // Value assigned to symbol is never used
            var checkers = spellCheckers.GetEnumerator();

            while (checkers.MoveNext() && !containsSpellingErrors)
            {
                containsSpellingErrors = ((ISpellChecker)checkers.Current).Check(word);
            }

            return containsSpellingErrors;
        }

    }

}
