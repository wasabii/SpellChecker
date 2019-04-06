using SpellChecker.Contracts;
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

        public async Task<bool> Check(string word)
        {
            return await Task.Factory.StartNew(() =>
            {

                var client = new HttpClient();
                var result = client.GetAsync($"http://dictionary.reference.com/browse/{word}");
                var newresult = result.GetAwaiter().GetResult();
                var resultingString = newresult.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (newresult.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(resultingString))
                    return true;
                else
                    return false;
            });
        }

    }

}
