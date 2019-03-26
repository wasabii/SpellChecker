using System;
using System.Net.Http;
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
            using (var httpClient = new HttpClient())
            {

                var response = httpClient.GetAsync(new Uri("http://dictionary.reference.com/browse/" + word)).Result;
                
                // Chose to look for the phrase "No results found for" in the body instead of "Misspelled" ion the Uri
                // because looking for misspelled could cause issues with the word misspelled
                return !response.Content.ReadAsStringAsync().Result.Contains("No results found for");
            }
        }

    }

}
