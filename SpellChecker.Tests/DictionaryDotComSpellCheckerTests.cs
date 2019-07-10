﻿using System;

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
            string word = "FileAndServe";
            bool result;

            result = spellChecker.Check(word).Result;

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Check_That_South_Is_Not_Misspelled()
        {
            string word = "South";
            bool result;

            result = spellChecker.Check(word).Result;

            Assert.AreEqual(true, result);
        }

    }

}
