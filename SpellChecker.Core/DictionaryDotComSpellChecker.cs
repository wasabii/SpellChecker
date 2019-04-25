using System;
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

        public Task<bool> Check(string word)
        {
            string url = ("http://dictionary.reference.com/browse/" + word).ToLower();
            var request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    int misspellingIndex = request.Address.OriginalString.IndexOf("misspelling?");
                    return Task.FromResult(misspellingIndex != 32);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }

    }

}
