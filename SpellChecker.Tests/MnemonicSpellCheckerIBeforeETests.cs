using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SpellChecker.Contracts;
using SpellChecker.Core;

namespace SpellChecker.Tests
{

    [TestClass]
    public class MnemonicSpellCheckerIBeforeETests
    {

        ISpellChecker spellChecker;

        [TestInitialize]
        public void TestFixtureSetUp()
        {
            spellChecker = new MnemonicSpellCheckerIBeforeE();
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            //Arrange
            var wordToCheck = "believe";

            //Act
            var isCorrect = await spellChecker.Check(wordToCheck);

            //Assert
            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            //Arrange
            var wordToCheck = "science";

            //Act
            var isCorrect = await spellChecker.Check(wordToCheck);

            //Assert
            Assert.IsFalse(isCorrect);
        }

        [TestMethod]
        public async Task Check_Word_That_Does_Not_Contain_IE_Spelled_Correctly()
        {
            //Arrange
            var wordToCheck = "this";

            //Act
            var isCorrect = await spellChecker.Check(wordToCheck);

            //Assert
            Assert.IsTrue(isCorrect);
        }

    }

}
