using System;
using System.IO;
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

        public bool Check(string word)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://dictionary.reference.com");

            HttpResponseMessage response = client.GetAsync("browse/" + word).Result;

            Task<string> result;
            string res = string.Empty;
            using (HttpContent content = response.Content)
            {
                result = content.ReadAsStringAsync();
                res = result.Result;
            }

            if (res.Contains("No results found for"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

}

