using System;
using System.Collections.Generic;
using System.Linq;

namespace SpellChecker.Core.Extension
{
    public static class StringExtension
    {
        public static string Cleanse(this string sentence)
        {
            return sentence;
        }

        public static string RemovePunctuators(this string sentence)
        {
            var sentencePunctuators = new string[] { ".", "!", "?", ";", "," };

            foreach (var punctuator in sentencePunctuators)
            {
                sentence = sentence.Replace(punctuator, string.Empty);
            }

            sentence = sentence.Replace("  ", " ");

            return sentence;
        }

        public static List<string> GetWordsFromSentence(this string sentence)
        {
            sentence = sentence.Cleanse();
            sentence = sentence.RemovePunctuators();

            return sentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
