using System;
using System.Collections.Generic;
using System.Text;
using SpellChecker.Contracts;
using System.Threading.Tasks;

namespace SpellChecker.Core
{
    //This class  is use for dependency injection to provide layer for spell checkers (dictionary and mneumoic)
    public class SpellCheckerInstance
    {
        private ISpellChecker _SpellCheck;

        public SpellCheckerInstance (ISpellChecker objSpellchecker)
        {
            _SpellCheck = objSpellchecker;
        }
        public async Task<bool> Check(string word)
        {
            return await _SpellCheck.Check(word);
        }
    }
}
