using NUnit.Framework;
using SpellChecker.Contracts;
using SpellChecker.Core;
using System.Linq;

namespace SpellChecker.Tests
{

    [TestFixture]
    public class MnemonicSpellCheckerIBeforeETests
    {

        ISpellChecker spellChecker;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            spellChecker = new MnemonicSpellCheckerIBeforeE();
        }

        [Test]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            var valid = new string[]
            {
                "believe", "fierce","collie", "die", "friend", "deceive", "ceiling", "receipt",
            };

            var misspelled = valid.Where(word => !spellChecker.Check(word));

            Assert.AreEqual(0, misspelled.Count(), "One or more correctly spelled words failed.");
        }

        [Test]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            var invalid = new string[]
            {
                "heir", "protein", "science", "seeing", "their", "veil"
            };

            var correct = invalid.Where(word => spellChecker.Check(word));

            Assert.AreEqual(0, correct.Count(), "One or more incorrectly spelled words succeeded.");
        }

    }

}
