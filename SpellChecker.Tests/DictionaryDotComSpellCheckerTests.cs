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
            //Arrange
            var wordToCheck = "FileAndServe";

            //Act
            var isCorrect = await spellChecker.Check(wordToCheck);

            //Assert
            Assert.IsFalse(isCorrect);
        }

        [TestMethod]
        public async Task Check_That_South_Is_Not_Misspelled()
        {
            //Arrange
            var wordToCheck = "South";

            //Act
            var isCorrect = await spellChecker.Check(wordToCheck);

            //Assert
            Assert.IsTrue(isCorrect);
        }

    }

}
