using System;

using SpellChecker.Contracts;

namespace SpellChecker.Core
{

    /// <summary>
    /// This is a top level spell checker that is used by clients, it internally manages
    /// several spell checkers that it uses to evaluate whether a word is spelled correctly
    /// or not.
    /// </summary>
    public class SpellChecker : ISpellChecker
    {

        readonly ISpellChecker[] spellCheckers;
        readonly ISpellChecker _mnemonicSpellCheckerIBeforeE;
        readonly ISpellChecker _dictionaryDotComSpellChecker;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="spellCheckers"></param>
        public SpellChecker(ISpellChecker[] spellCheckers)
        {
            foreach(var spellchecker in spellCheckers)
            {
                if(spellchecker is MnemonicSpellCheckerIBeforeE)
                {
                    _mnemonicSpellCheckerIBeforeE = spellchecker;
                } else if(spellchecker is DictionaryDotComSpellChecker)
                {
                    _dictionaryDotComSpellChecker = spellchecker;
                }
            }
        }

        /// <summary>
        /// Iterates through all the internal spell checkers and returns false if any one of them finds a word to be
        /// misspelled
        /// </summary>
        /// <param name="word">Word to check</param>
        /// <returns>True if all spell checkers agree that a word is spelled correctly, false otherwise</returns>
        public bool Check(string word)
        {
            if(_mnemonicSpellCheckerIBeforeE != null)
            {
                if (!_mnemonicSpellCheckerIBeforeE.Check(word))
                    return false;
            }
            if(_dictionaryDotComSpellChecker != null)
            {
                if (!_dictionaryDotComSpellChecker.Check(word))
                    return false;
            }
            return true;
        }

    }

}
