using System;
using SpellChecker.Contracts;
using System.Net;
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

        static string _address = "http://dictionary.reference.com/browse/";


        public async Task<bool> Check(string word)
        {
            try

            { 
                var client = new HttpClient();
               var response = await client.GetAsync(_address + word).ConfigureAwait(false);         
              
                bool bSuccess = response.IsSuccessStatusCode;
                return bSuccess;             
             
            }
           catch
            {
                throw new HttpRequestException();
            }
            
           
        }
    
    }

}
