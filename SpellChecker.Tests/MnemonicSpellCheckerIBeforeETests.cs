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
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            string word = "fierce"; // correctly spelled
            bool result;

            result = spellChecker.Check(word).Result;

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            string word = "decieve"; // incorrectly spelled
            bool result;

            result = spellChecker.Check(word).Result;

            Assert.AreEqual(false, result);
        }

    }

}
