using System;
using System.Net;
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
            var uriBuilder = new UriBuilder(@"http://dictionary.reference.com/browse/" + word);
            var request = HttpWebRequest.Create(uriBuilder.Uri);

            // Let's attempt to get a respoonse
            // If we don't get one, the page (the word) doesn't exist and it's misspelled
            try
            {
                // If word does not exist in dictionary, we'll get a 404 back
                var response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return false;
            }
        }

    }

}
