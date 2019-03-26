using System;
using System.Net.Http;
using System.Threading.Tasks;

using SpellChecker.Contracts;

namespace SpellChecker.Core.Checker
{
    public class DictionaryDotComSpellChecker : ISpellChecker
    {
        private const string NotFoundMessage = "not found";

        public DictionaryDotComSpellChecker(SpellCheckerConfig spellCheckerConfig)
        {
            SpellCheckerConfig = spellCheckerConfig;
        }

        protected SpellCheckerConfig SpellCheckerConfig { get; set; }

        public async Task<SpellCheckResult> CheckAsync(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException(nameof(word));

            var returnValue = new SpellCheckResult { WordChecked = word, IsCorrect = true };
            var url = SpellCheckerConfig.DictionartUri + word;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if(!response.IsSuccessStatusCode && response.ReasonPhrase.ToLower() == NotFoundMessage)
                {
                    returnValue.IsCorrect = false;
                }
            }

            return await Task.FromResult(returnValue);
        }
    }
}
