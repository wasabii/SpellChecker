using System;
using System.Net;
using SpellChecker.Contracts;
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
    public class DictionaryDotComSpellChecker : ISpellChecker
    {

        public async Task<bool> Check(string word)
        {
            var request = WebRequest.Create("http://dictionary.reference.com/browse/" + word);
            try
            {
                WebResponse response = request.GetResponse();
                if (response.ResponseUri.AbsolutePath.IndexOf("-") == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
        }

    }

}
