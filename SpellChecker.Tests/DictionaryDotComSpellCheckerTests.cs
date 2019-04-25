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
            //Arrange
            var test = "FileAndServe";

            //Act
            var result = spellChecker.Check(test);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Check_That_South_Is_Not_Misspelled()
        {
            //Arrange
            var test = "South";

            //Act
            var result = spellChecker.Check(test);

            //Assert
            Assert.IsTrue(result);
        }

    }

}
