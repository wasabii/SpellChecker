using System;

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
        public void Check_That_FileAndServe_Is_Misspelled()
        {
            var word = "FileAndServe";
            var checkTask = spellChecker.Check(word);

            Assert.IsFalse(checkTask.Result);
        }

        [TestMethod]
        public void Check_That_South_Is_Not_Misspelled()
        {
            var word = "South";
            var checkTask = spellChecker.Check(word);

            Assert.IsTrue(checkTask.Result);
        }

    }

}
