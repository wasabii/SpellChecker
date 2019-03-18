using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using SpellChecker.Contracts;

namespace SpellChecker.Core
{

    /// <summary>
    /// This is a dictionary based spell checker that uses dictionary.com to determine if
    /// a word is spelled correctly5
    /// 
    /// The URL to do this looks like this: http://dictionary.reference.com/browse/<word>
    /// where <word> is the word to be checked
    /// 
    /// We look for something in the response that gives us a clear indication whether the
    /// word is spelled correctly or not
    /// </summary>
    public class DictionaryDotComSpellChecker : ISpellChecker
    {
        //Note: this can be problematic due to DNS updates for longer running applications
        //In large scale enterprise applications use DI and microsoft.extensions.http and polly libraries to handle lifetime of http
        private static HttpClient _httpClient;

        public DictionaryDotComSpellChecker()
        {
            Uri baseUri = new Uri("https://www.dictionary.com/browse/");

            _httpClient = new HttpClient
            {
                BaseAddress = baseUri
            };
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.ConnectionClose = false;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            ServicePointManager.FindServicePoint(baseUri).ConnectionLeaseTimeout = 60 * 1000;
        }

        public bool Check(string word)
        {           
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://www.dictionary.com/browse/{word}");
            HttpResponseMessage response =  _httpClient.SendAsync(httpRequest).Result;
            if (response.StatusCode == HttpStatusCode.Redirect) return false; //this known indication of misspelling
            return response.StatusCode == HttpStatusCode.OK;
        }

    }

}
