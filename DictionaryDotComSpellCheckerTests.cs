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
            bool rslt = spellChecker.Check("FileAndServe");
            Assert.IsFalse(rslt);
        }

        [Test]
        public void Check_That_South_Is_Not_Misspelled()
        {
            bool rslt = spellChecker.Check("South");
            Assert.IsTrue(rslt);

        }

    }

}
