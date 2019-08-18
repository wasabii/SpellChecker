using SpellChecker.Contracts;
using SpellChecker.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpellChecker.Console
{
    public class SpellCheckerParser
    {
        public async Task<string> GetMisspelledWords(string sentence)
        {
            // Remove punctuation. If we were accepting entire documents
            // instead of single sentences, we might want to use
            // something else.
            var regex = new Regex(@"[\p{P}]");
            var cleanedSentence = regex.Replace(sentence, "");

            // first break the sentence up into words, 
            // then iterate through the list of words using the spell checker
            // capturing distinct words that are misspelled

            // use this spellChecker to evaluate the words
            var spellChecker = new Core.SpellChecker(new ISpellChecker[]
            {
                new MnemonicSpellCheckerIBeforeE(),
                new DictionaryDotComSpellChecker(),
            });

            var words = cleanedSentence.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);

            var misspellings = new List<string>();
            foreach (var word in words)
            {
                if (!(await spellChecker.Check(word)))
                {
                    misspellings.Add(word);
                }
            }

            return string.Join(" ", misspellings.Distinct());
        }
    }
}
