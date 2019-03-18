using System;
using System.Net;
using System.IO;

using SpellChecker.Contracts;

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
        const string baseUrl = "https://dictionary.reference.com/browse/";
        public bool Check(string word)
        {
            String url = baseUrl + word.ToLower();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest webRequest = WebRequest.Create(url.ToString());
            webRequest.Method = "Get";
            webRequest.Proxy = null;
            WebResponse webResp = null;
            try
            {
                webResp = webRequest.GetResponse();
            }
            catch 
            {
                return false;
            }
            string respText = "";
            Stream secDataStream = webResp.GetResponseStream();
            StreamReader reader = new StreamReader(secDataStream);
            respText = reader.ReadToEnd();
            if (respText.ToLower().IndexOf("no results found") >= 0)
                return false;
            else
                return true;
        }

    }

}
