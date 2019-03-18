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
            bool rslt = spellChecker.Check("receive");
            Assert.IsTrue(rslt);
        }

        [Test]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            bool rslt = spellChecker.Check("recieve");
            Assert.IsFalse(rslt);
        }

    }

}
