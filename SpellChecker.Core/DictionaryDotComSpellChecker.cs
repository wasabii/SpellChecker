using System;
using System.Collections.Generic;
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
            word = word.ToLower(); //normalize
            if (WordCache.ContainsKey(word)) return WordCache.Value(word); //if known word, skip web stuff
            //https://www.dictionary.com/browse/<word> use status code to save time... maybe?
            var request = (HttpWebRequest)WebRequest.Create($"https://www.dictionary.com/browse/{word}");
            request.Method = "HEAD"; //HEAD method doesn't return a body
            try
            {
                using (var response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    return ProcessResponse(word, (int)response.StatusCode);
                }
            }
            catch (WebException ex) //404 throws
            {
                var response = (HttpWebResponse)ex.Response;
                if (response == null)
                    throw (ex); //if something else went wrong
                return ProcessResponse(word, (int)response.StatusCode);
            }
            catch (Exception ex)
            {
                throw (ex); // throw real exceptions
            }
        }

        private bool EvalStatus(int status) => status >= 200 && status < 300; //anything in the 200 range is good
        private bool ProcessResponse(string word, int status)
        {
            WordCache.Add(word, EvalStatus(status)); //cache new words
            return EvalStatus(status); //anything in the 200 range is good
        }
    }
    static class WordCache // I did this instead of including MemoryCache. It's an extra Nuget for console apps
    {
        public static Dictionary<string, bool> Cache = new Dictionary<string, bool>();
        public static bool ContainsKey(string key) => Cache.ContainsKey(key);
        public static void Add(string key, bool status) => Cache.Add(key,status);
        public static bool Value(string key) => Cache[key];
    }
}
