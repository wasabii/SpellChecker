using System;
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
            var isValid = false;
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync($"http://dictionary.reference.com/browse/{word}");
                    isValid = response.IsSuccessStatusCode;
                }
                catch
                {
                    Console.WriteLine("Error while connecting to the dictionary.com endpoint.");
                }
            }
            return isValid;
        }
    }
}