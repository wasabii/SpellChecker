using System.Net;
using System.Threading.Tasks;

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
    public class DictionaryDotComSpellChecker : ISpellChecker
    {
        private readonly IHttpClient _http;

        public DictionaryDotComSpellChecker(IHttpClient http)
        {
            _http = http;
        }

        public async Task<bool> Check(string word)
        {
            var response = await _http.GetAsync($"http://dictionary.reference.com/browse/{word}");
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
