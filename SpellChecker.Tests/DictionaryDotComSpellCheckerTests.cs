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
            var checkDictionary = "fileandServe";

            Assert.IsFalse(spellChecker.Check(checkDictionary).Result, "Dictionary is not detecting correctly spelled words");
        }

        [TestMethod]
        public void Check_That_South_Is_Not_Misspelled()
        {
            var checkDictionary = "South";

            Assert.IsTrue(spellChecker.Check(checkDictionary).Result, "Dictionary is not detecting correctly spelled words");
        }

    }

}
