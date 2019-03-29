using System;
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
            string word = "FileAndServe";

            var result = await spellChecker.Check(word);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Check_That_South_Is_Not_Misspelled()
        {
            string word = "South";

            var result = await spellChecker.Check(word);

            Assert.IsTrue(result);
        }
    }
}
