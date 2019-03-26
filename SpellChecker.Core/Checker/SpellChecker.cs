using System.Threading.Tasks;
using SpellChecker.Contracts;

namespace SpellChecker.Core.Checker
{
    public class SpellChecker : ISpellCheckerCore
    {
        readonly ISpellChecker[] _spellCheckers;

        public SpellChecker(ISpellChecker[] spellCheckers)
        {
            _spellCheckers = spellCheckers;
        }

        public async Task<SpellCheckResult> CheckAsync(string word)
        {
            var returnValue = new SpellCheckResult { WordChecked = word, IsCorrect = true };

            foreach(var spellChecker in _spellCheckers)
            {
                returnValue = await spellChecker.CheckAsync(word);

                if(!returnValue.IsCorrect)
                    break;
            }

            return await Task.FromResult(returnValue);
        }
    }
}
