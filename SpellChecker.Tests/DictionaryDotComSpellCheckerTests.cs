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
            var checkDictionary = "sentense";

            Assert.IsFalse(spellChecker.Check(checkDictionary), "Dictionary not detecting misspelled words");
        }

        [TestMethod]
        public void Check_That_South_Is_Not_Misspelled()
        {
            var checkDictionary = "sentence";

            Assert.IsTrue(spellChecker.Check(checkDictionary), "Dictionary is not detecting correctly spelled words");
        }

    }

}
