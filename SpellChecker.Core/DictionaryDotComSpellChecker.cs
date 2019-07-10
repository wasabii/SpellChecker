using System;
using System.IO;
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
    public class DictionaryDotComSpellChecker :
        ISpellChecker
    {

        public async Task<bool> Check(string word)
        {
            string url = "http://dictionary.reference.com/browse/" + word;

            try
            {
                var request = HttpWebRequest.Create(url);
                var webResponse = await request.GetResponseAsync() as HttpWebResponse;
                bool result = webResponse.StatusCode == HttpStatusCode.OK;
                webResponse.Close();

                return result;
            }
            catch (WebException)
            {
                return false;
            }
        }

    }

}
