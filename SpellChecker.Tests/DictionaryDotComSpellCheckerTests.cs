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
            Assert.AreEqual(false, spellChecker.Check("FileAndServe").Result);
        }

        [TestMethod]
        public void Check_That_South_Is_Not_Misspelled()
        {
            Assert.AreEqual(true, spellChecker.Check("South").Result);
        }

    }

}
