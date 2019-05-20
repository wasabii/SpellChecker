using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SpellChecker.Contracts;

namespace SpellChecker.Core
{

    /// <summary>
    /// This spell checker implements the following rule:
    /// "I before E, except after C" is a mnemonic rule of thumb for English spelling.
    /// If one is unsure whether a word is spelled with the sequence ei or ie, the rhyme
    /// suggests that the correct order is ie unless the preceding letter is c, in which case it is ei.
    /// 
    /// Examples: believe, fierce, collie, die, friend, deceive, ceiling, receipt would be evaulated as spelled correctly
    /// heir, protein, science, seeing, their, and veil would be evaluated as spelled incorrectly.
    /// </summary>
    public class MnemonicSpellCheckerIBeforeE :
        ISpellChecker
    {

        readonly ILogger logger;

        public MnemonicSpellCheckerIBeforeE(ILogger<ISpellChecker> logger) => this.logger = logger;
        /// <summary>
        /// Returns false if the word contains a letter sequence of "ie" when it is immediately preceded by c.
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>true when the word is spelled correctly, false otherwise</returns>
        public Task<bool> Check(string word)
        {
            logger.Log(LogLevel.Information, $"Running Mnemonic spell checking for word: {word}");

            // convert to lowercase
            word = word.ToLower();

            // check if the word contains "ie" and if not default the ieIndex to a value of -1
            var ieIndex = word.Contains("ie") ? word.IndexOf("ie") : -1;

            // check if the word contains "c" and if not default the cIndex to a value of -1
            var cIndex = word.Contains("c") ? word.IndexOf("c") : -1;

            // check if both index are greater than zero, and if they are greater than zero
            // does "c" comes before "ie"?
            var result = !((ieIndex > 0 && cIndex > 0) && (ieIndex - cIndex == 1));

            return Task.FromResult(result);
        }

    }

}
