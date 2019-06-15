using System;
using System.Net;
using System.Net.Http;
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
    public class DictionaryDotComSpellChecker :
        ISpellChecker
    {
        

        public async Task<bool> Check(string word)
        {
            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://www.dictionary.com/browse/");
                HttpResponseMessage response = await httpClient.GetAsync(word);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                {
                    return false;
                }
                return false;
            }
        }

    }

}
