using SpellChecker.Contracts;
using System;
using System.Net.Http;

namespace SpellChecker.Core
{

    /// <summary>
    /// This is a dictionary based spell checker that uses dictionary.com to determine if
    /// a word is spelled correctly
    /// 
    /// The URL to do this looks like this: http://dictionary.reference.com/browse/<word>
    /// where <word> is the word to be checked
    /// 
    /// We look for something in the response that gives us a clear indication whether the
    /// word is spelled correctly or not
    /// </summary>
    public class DictionaryDotComSpellChecker :
        ISpellChecker
    {
    
        // NOTE: Change to async to perform calls ansynchronously
        public bool Check(string word)
        {
            var result = false;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://dictionary.reference.com/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();

                    var response = httpClient.GetAsync($"browse/{word}").Result;

                    result = response.IsSuccessStatusCode;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{word} spelling validation failed, error contacting dictionary reference server.");
                Console.WriteLine($"{e.Message}");
            }
            return (result);
        }

    }

}
