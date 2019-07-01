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
           Assert.IsFalse(spellChecker.Check("FileAndServe"));
        }

        [TestMethod]
        public void Check_That_South_Is_Not_Misspelled()
        {
            Assert.IsTrue(spellChecker.Check("South"));
        }

    }

}
