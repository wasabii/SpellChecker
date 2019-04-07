using System;

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
            var checkWordIE = "believe";
            var checkWordCEI = "deceive";

            Assert.IsTrue(spellChecker.Check(checkWordIE) && spellChecker.Check(checkWordCEI), "Correct 'I before E' words are not being marked incorrect");
        }

        [TestMethod]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            var checkWordIE = "beleive";
            var checkWordCEI = "decieve";

            Assert.IsFalse(spellChecker.Check(checkWordIE) && spellChecker.Check(checkWordCEI), "Incorrect 'I before E' words being marked correct");
        }

    }

}
