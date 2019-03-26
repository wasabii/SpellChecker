using System;
using System.Threading.Tasks;

using SpellChecker.Contracts;

namespace SpellChecker.Core.Checker
{
    public class MnemonicSpellCheckerIBeforeE : ISpellChecker
    {
        public async Task<SpellCheckResult> CheckAsync(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word));

            var isCorrect = CheckIe(word) && CheckEi(word);
            var returnValue = new SpellCheckResult { WordChecked = word, IsCorrect = isCorrect };

            return await Task.FromResult(returnValue);
        }

        private bool CheckIe(string word)
        {
            var returnValue = true;
            var wordLowered = word.ToLower();
            var ieIndex = wordLowered.IndexOf("ie");

            while (ieIndex != -1)
            {
                if (wordLowered.Substring(ieIndex - 1, 1) == "c")
                {
                    returnValue = false;
                    break;
                }

                ieIndex = wordLowered.IndexOf("ie", ieIndex + 2);
            }

            return returnValue;
        }

        private bool CheckEi(string word)
        {
            var returnValue = true;
            var wordLowered = word.ToLower();
            var eiIndex = wordLowered.IndexOf("ei");

            while (eiIndex != -1)
            {
                if (wordLowered.Substring(eiIndex - 1, 1) != "c")
                {
                    returnValue = false;
                    break;
                }

                eiIndex = wordLowered.IndexOf("ei", eiIndex + 2);
            }

            return returnValue;
        }
    }
}
