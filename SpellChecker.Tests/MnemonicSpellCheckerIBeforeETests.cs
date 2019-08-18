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

        [DataTestMethod]
        [DataRow("believe")]
        [DataRow("Believe")]
        [DataRow("BELIEVE")]
        [DataRow("fierce")]
        [DataRow("collie")]
        [DataRow("die")]
        [DataRow("friend")]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly(string word)
        {
            Assert.IsTrue(spellChecker.Check(word));
        }

        [DataTestMethod]
        [DataRow("recieve")]
        [DataRow("Recieve")]
        [DataRow("RECIEVE")]
        [DataRow("acieve")]
        [DataRow("cieve")]
        [DataRow("decieve")]
        [DataRow("cieling")]
        [DataRow("reciept")]
        [DataRow("science")]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly(string word)
        {
            Assert.IsFalse(spellChecker.Check(word));
        }

        [DataTestMethod]
        [DataRow("deceive")]
        [DataRow("Deceive")]
        [DataRow("DECEIVE")]
        [DataRow("ceiling")]
        [DataRow("receipt")]
        public void Check_Word_That_Contains_E_Before_I_Is_Spelled_Correctly(string word)
        {
            Assert.IsTrue(spellChecker.Check(word));
        }

        [DataTestMethod]
        [DataRow("heir")]
        [DataRow("Heir")]
        [DataRow("HEIR")]
        [DataRow("protein")]
        [DataRow("seeing")]
        [DataRow("their")]
        [DataRow("veil")]
        public void Check_Word_That_Contains_E_Before_I_Is_Spelled_Incorrectly(string word)
        {
            Assert.IsFalse(spellChecker.Check(word));
        }

    }

}
