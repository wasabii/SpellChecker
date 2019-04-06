using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        /// <summary>
        /// Returns false if the word contains a letter sequence of "ie" when it is immediately preceded by c.
        /// </summary>
        /// <param name="word">The word to be checked</param>
        /// <returns>true when the word is spelled correctly, false otherwise</returns>
        public async Task<bool> Check(string word)
        {
            return await Task.Factory.StartNew(() =>
            {
                var wordContainsie = word.Contains("ie");
                var wordContainsEi = word.Contains("ei");

                // other possibilities considered.
                var rxContainsIe = Regex.IsMatch(word, "\\w[i][e]");
                var rxContainsEI = Regex.IsMatch(word, "c[e][i]");
                //--- 

                var wordArray = word.ToLowerInvariant().ToCharArray();
                var ieposition = word.IndexOf("ie");
                var eiposition = word.IndexOf("ei");
                if (ieposition > 0)
                {
                    // i before e except after c
                    if (wordArray[ieposition - 1] == 'c')
                        return false;
                    else
                        return true;
                }
                else if (eiposition > 0)
                {
                    // i before e except after c
                    if (wordArray[eiposition - 1] == 'c')
                        return true;
                    else
                        return false;
                }
                return true;
            });
        }

    }

}
