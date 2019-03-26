using System.Threading.Tasks;
using SpellChecker.Contracts;

using SpellChecker.Core;
using SpellChecker.Core.Checker;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpellChecker.Tests
{

    [TestClass]
    public class DictionaryDotComSpellCheckerTests
    {
        ISpellChecker _spellChecker;

        [TestInitialize]
        public void TestFixureSetUp()
        {
            var config = new SpellCheckerConfig { DictionartUri = "http://dictionary.reference.com/browse/" }; // TODO: Fix This
            _spellChecker = new DictionaryDotComSpellChecker(config);
        }

        [TestMethod]
        public async Task Check_That_FileAndServe_Is_Misspelled()
        {
            var result = await _spellChecker.CheckAsync("FileAndServe");
            Assert.IsFalse(result.IsCorrect);
        }

        [TestMethod]
        public async Task Check_That_South_Is_Not_Misspelled()
        {
            var result = await _spellChecker.CheckAsync("South");
            Assert.IsTrue(result.IsCorrect);
        }
    }
}
