using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SpellChecker.Contracts;

namespace SpellChecker.Core
{

    /// <summary>
    /// This is a dictionary based spell checker that uses dictionary.com to determine if
    /// a word is spelled correctly.
    /// 
    /// The URL to do this looks like this: http://dictionary.reference.com/browse/<word>
    /// where <word> is the word to be checked.
    /// 
    /// We look for something in the response that gives us a clear indication whether the
    /// word is spelled correctly or not.
    /// </summary>
    public class DictionaryDotComSpellChecker :
        ISpellChecker
    {
        readonly ILogger logger;
        public DictionaryDotComSpellChecker(ILogger<ISpellChecker> logger) => this.logger = logger;
        public async Task<bool> Check(string word)
        {
            logger.Log(LogLevel.Information, $"Running dotcom spell check for word: {word}");

            var request = new System.Net.Http.HttpRequestMessage()
            {
                Method = System.Net.Http.HttpMethod.Get,
                RequestUri = new Uri($"http://dictionary.reference.com/browse/{word}")
            };

            var client = new System.Net.Http.HttpClient();

            var response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;

        }

    }

}
