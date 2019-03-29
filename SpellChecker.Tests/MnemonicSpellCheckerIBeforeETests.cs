using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SpellChecker.Contracts;
using SpellChecker.Core;

namespace SpellChecker.Tests
{

    [TestClass]
    public class MnemonicSpellCheckerIBeforeETests
    {

        ISpellChecker spellChecker;

        [TestInitialize]
        public void TestFixtureSetUp()
        {
            spellChecker = new MnemonicSpellCheckerIBeforeE();
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            string word = "Diet";

            var result = await spellChecker.Check(word);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            string word = "Science";

            var result = await spellChecker.Check(word);

            Assert.IsFalse(result);
        }
    }
}
