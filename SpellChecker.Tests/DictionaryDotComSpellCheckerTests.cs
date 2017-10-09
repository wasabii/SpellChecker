
using NUnit.Framework;
using SpellChecker.Contracts;
using SpellChecker.Core;

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
            var result = spellChecker.Check("FileAndServe");

            Assert.IsFalse(result, "SpellChecker thinks FileAndServe is spelled correctly.");
        }

        [Test]
        public void Check_That_South_Is_Not_Misspelled()
        {
            var result = spellChecker.Check("South");

            Assert.IsTrue(result, "SpellChecker thinks South is spelled incorrectly.");
        }

    }

}
