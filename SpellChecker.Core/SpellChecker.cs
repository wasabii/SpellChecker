using System;
using System.Reflection;
using SpellChecker.Contracts;
using System.Threading.Tasks;

namespace SpellChecker.Core
{

    /// <summary>
    /// This is a top level spell checker that is used by clients, it internally manages
    /// several spell checkers that it uses to evaluate whether a word is spelled correctly
    /// or not.
    /// </summary>
    public class SpellChecker :
        ISpellChecker
    {
        //dependency injection using a layer for spellchecker 
        public SpellCheckerInstance[] _SpellCheck;

     
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="spellCheckers"></param>
        public SpellChecker(SpellCheckerInstance[] spellCheck)
        {
            _SpellCheck = spellCheck;
        }

        /// <summary>
        /// Iterates through all the internal spell checkers and returns false if any one of them finds a word to be
        /// misspelled
        /// </summary>
        /// <param name="word">Word to check</param>
        /// <returns>True if all spell checkers agree that a word is spelled correctly, false otherwise</returns>
        public async Task<bool> Check(string word)
        {
            bool bValid = true;
          
            for (int iCount=0;iCount<_SpellCheck.Length;iCount++)
            {
          //  dynamic s=    Activator.CreateInstance(spellCheckers[iCount].GetType());
                
                bool bStatus = await _SpellCheck[iCount].Check(word);
             
                if (!bStatus)
                {
                   
                    bValid = false;
                    break;
                }
            }
          
            return bValid;
        }

    }

}
