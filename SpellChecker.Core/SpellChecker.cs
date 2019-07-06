using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly ISpellChecker[] _spellCheckers;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="spellCheckers"></param>
        public SpellChecker(IEnumerable<ISpellChecker> spellCheckers)
        {
            _spellCheckers = spellCheckers.ToArray();
        }

        /// <summary>
        /// Iterates through all the internal spell checkers and returns false if any one of them finds a word to be
        /// misspelled
        /// </summary>
        /// <param name="word">Word to check</param>
        /// <returns>True if all spell checkers agree that a word is spelled correctly, false otherwise</returns>
        public async Task<bool> Check(string word)
        {
            foreach (var checker in _spellCheckers)
            {
                var isSpelledCorrectly = await checker.Check(word);

                if (!isSpelledCorrectly)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
