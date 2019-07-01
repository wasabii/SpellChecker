using System;
using System.Net;
using System.IO;
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
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://dictionary.reference.com/browse/" + word);
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            return myResponse.ResponseUri.AbsolutePath.Contains("misspelling");
        }

    }

}
