using System;

using NUnit.Framework;

using SpellChecker.Contracts;
using SpellChecker.Core;

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
            Assert.IsTrue(spellChecker.Check("believe"));
            Assert.IsTrue(spellChecker.Check("fierce"));
            Assert.IsTrue(spellChecker.Check("collie"));
            Assert.IsTrue(spellChecker.Check("die"));
            Assert.IsTrue(spellChecker.Check("friend"));
            Assert.IsTrue(spellChecker.Check("deceive"));
            Assert.IsTrue(spellChecker.Check("ceiling"));
            Assert.IsTrue(spellChecker.Check("receipt"));

        }

        [Test]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            Assert.IsFalse(spellChecker.Check("heir"));
            Assert.IsFalse(spellChecker.Check("protein"));
            Assert.IsFalse(spellChecker.Check("science"));
            Assert.IsFalse(spellChecker.Check("seeing"));
            Assert.IsFalse(spellChecker.Check("their"));
            Assert.IsFalse(spellChecker.Check("veil"));       
        }

    }

}
