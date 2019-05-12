using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
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
        public async Task  Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            string word = "believe";
            Assert.AreEqual( true,await spellChecker.Check(word));

          //  throw new NotImplementedException();
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            string word = "Science";
            Assert.AreNotEqual(true,await spellChecker.Check(word));
            //throw new NotImplementedException();
        }

    }

}
