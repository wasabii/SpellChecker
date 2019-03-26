using System;
using System.Collections.Generic;
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
            var spelledCorrectly = new List<string>()
                {"believe", "fierce", "collie", "die", "friend", "deceive", "ceiling", "receipt"};
            foreach (var word in spelledCorrectly)
            {
                Assert.IsTrue(spellChecker.Check(word));
            }
        }

        [TestMethod]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            var spelledIncorrectly = new List<string>() { "heir", "protein", "science", "seeing", "their",  "veil"};
            foreach (var word in spelledIncorrectly)
            {
                Assert.IsFalse(spellChecker.Check(word));
            }
        }
    }

}
