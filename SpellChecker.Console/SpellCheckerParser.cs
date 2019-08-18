using SpellChecker.Contracts;
using SpellChecker.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpellChecker.Console
{
    public class SpellCheckerParser
    {
        private class WordTask
        {
            public string Word { get; }
            public WordTask(string word)
            {
                Word = word;
            }
            public bool Result { get; set; } = true;
        }
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

            var words = cleanedSentence.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries).Distinct();
            var wordTasks = new List<WordTask>();

            foreach (var word in words)
            {
                wordTasks.Add(new WordTask(word));
            }
            await Task.WhenAll(wordTasks.Select(async word =>
            {
                word.Result = (await spellChecker.Check(word.Word));
            }));

            return string.Join(" ", wordTasks.Where(t => !t.Result).Select(t => t.Word));
        }
    }
}


