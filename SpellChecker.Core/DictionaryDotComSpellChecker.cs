using SpellChecker.Contracts;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
        private const string SearchUrlBase = "http://dictionary.reference.com/browse/";
        public async Task<bool> Check(string word)
        {
            var strUrl = SearchUrlBase + word;
            var client = new HttpClient
            {
                BaseAddress = new Uri(strUrl)
            };
            var response = await client.GetAsync(strUrl);
            return (response.StatusCode == HttpStatusCode.OK) ? true : false;
        }

    }

}
