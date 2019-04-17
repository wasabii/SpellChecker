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

        public async Task<bool> CheckAsync(string word)
        {
            //Try to get the response back - if we get a 404, then the word is wrong.
            var uri = "https://dictionary.reference.com/browse/" + word;
            var request = (HttpWebRequest)WebRequest.Create(uri);
            try
            {
                var response = await request.GetResponseAsync() as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                } else {
                    return true;
                }
            }
            catch (WebException)
            {
                //404 also raises an exception so...
                return false;
            }
        }
    }

}
