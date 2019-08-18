﻿using System;
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
    public class DictionaryDotComSpellChecker :
        ISpellChecker
    {

        public bool Check(string word)
        {
            var request = WebRequest.CreateHttp($"http://dictionary.reference.com/browse/{word}");
            // Note: this implementation only treats a 404 as a definitive
            // misspelling. If the dictionary.reference.com server has an
            // issue, it could return one of any number of status codes. In
            // that case, we don't know whether or not the word is misspelled.
            //
            // We assume here that a false positive is worse for UX than the
            // alternative. It depends on how this tool is used, but if I were
            // trying to submit files to a system, and it didn't let me because
            // it falsely identified misspelled words, I'd be really annoyed.
            // In this case I think it's best to give the users the benefit of
            // the doubt.
            //
            // But if the business decides they'd rather show false positives,
            // then we'd only return true if the status code is 200.
            //
            // Another option would be to throw an exception and let the user
            // know that one of the spell checker services is down.
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode != HttpStatusCode.NotFound;
            }
            catch (System.Net.WebException ex)
            {
                var response = (HttpWebResponse)(ex.Response);
                return response.StatusCode != HttpStatusCode.NotFound;
            }
        }

    }

}
