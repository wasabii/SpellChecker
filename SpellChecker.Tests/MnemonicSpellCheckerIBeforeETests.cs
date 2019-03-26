using System.Threading.Tasks;

using SpellChecker.Contracts;
using SpellChecker.Core.Checker;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpellChecker.Tests
{
    [TestClass]
    public class MnemonicSpellCheckerIBeforeETests
    {

        ISpellChecker _spellChecker;

        [TestInitialize]
        public void TestFixtureSetUp()
        {
            _spellChecker = new MnemonicSpellCheckerIBeforeE();
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            var result = await _spellChecker.CheckAsync("Conceive");
            Assert.IsTrue(result.IsCorrect);
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            var result = await _spellChecker.CheckAsync("protein");
            Assert.IsFalse(result.IsCorrect);
        }
    }
}
