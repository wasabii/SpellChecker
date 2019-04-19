using System;
using System.Data;
using System.IO;
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
    public class DictionaryDotComSpellChecker : ISpellChecker
    {

        public bool Check(string word)
        {
            try
            {
                var url = "http://dictionary.reference.com/browse/" + word;
                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                var response = (HttpWebResponse)request.GetResponse();

                var responseStream = response.GetResponseStream();

                if (responseStream == null)
                    throw new NoNullAllowedException("Response is null");
                var reader = new StreamReader(responseStream);
                var parsedResponse = reader.ReadToEnd();

                // For some reason I can't hit breakpoints in this solution. In order for me to test, I had to write to the console.
                // I noticed that mispelled words had no string response at all, so this seems to work.
                if (string.IsNullOrEmpty(parsedResponse))
                    return false;
                else
                    return true;
            } catch(Exception ex)
            {
                // Log exception
            }
            return false;
        }

    }

}
