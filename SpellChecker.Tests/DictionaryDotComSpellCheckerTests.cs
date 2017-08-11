using System;

using NUnit.Framework;

using SpellChecker.Core;

using SpellChecker.Contracts;

namespace SpellChecker.Tests
{

    [TestFixture]
    public class DictionaryDotComSpellCheckerTests
    {

        ISpellChecker spellChecker;

        [OneTimeSetUp]
        public void TestFixureSetUp()
        {
            spellChecker = new DictionaryDotComSpellChecker();
        }

        [Test]
        public void Check_That_FileAndServe_Is_Misspelled()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Check_That_South_Is_Not_Misspelled()
        {
            throw new NotImplementedException();
        }

    }

}
