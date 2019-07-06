using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SpellChecker.Contracts;
using SpellChecker.Core;

namespace SpellChecker.Tests
{
    [TestClass]
    public class MnemonicSpellCheckerIBeforeETests
    {
        private ISpellChecker _spellChecker;

        [TestInitialize]
        public void Initialize()
        {
            _spellChecker = new MnemonicSpellCheckerIBeforeE();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _spellChecker = null;
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            var isSpelledCorrectly = await _spellChecker.Check("believe");
            Assert.IsTrue(isSpelledCorrectly);
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            var isSpelledCorrectly = await _spellChecker.Check("science");
            Assert.IsFalse(isSpelledCorrectly);
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_Combination_Is_Spelled_Correctly()
        {
            var isSpelledCorrectly = await _spellChecker.Check("biecei");
            Assert.IsTrue(isSpelledCorrectly);
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_Combination_Is_Spelled_Incorrectly()
        {
            var isSpelledCorrectly = await _spellChecker.Check("beicie");
            Assert.IsFalse(isSpelledCorrectly);
        }
    }
}
