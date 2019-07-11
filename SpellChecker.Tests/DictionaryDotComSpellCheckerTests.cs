using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SpellChecker.Contracts;
using SpellChecker.Core;

namespace SpellChecker.Tests
{

    [TestClass]
    public class DictionaryDotComSpellCheckerTests
    {

        ISpellChecker spellChecker;

        [TestInitialize]
        public void TestFixureSetUp()
        {
            spellChecker = new DictionaryDotComSpellChecker();
        }

        [TestMethod]
        public async Task Check_That_FileAndServe_Is_Misspelled()
        {
            Assert.IsFalse(await spellChecker.CheckAsync("FileAndServe"));
        }

        [TestMethod]
        public async Task Check_That_South_Is_Not_Misspelled()
        {
            Assert.IsTrue(await spellChecker.CheckAsync("South"));
        }

    }

}
