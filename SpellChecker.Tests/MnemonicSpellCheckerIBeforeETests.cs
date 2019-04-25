using System;

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
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            //Arrange
            var test = "Believe";

            //Act
            var result = spellChecker.Check(test);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_Word_That_Contains_C_Before_E_Before_I_Is_Spelled_Correctly()
        {
            //Arrange
            var test = "Ceiling";

            //Act
            var result = spellChecker.Check(test);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_Word_That_Contains_E_Before_I_Is_Spelled_Incorrectly()
        {
            //Arrange
            var test = "Protein";

            //Act
            var result = spellChecker.Check(test);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Check_Word_That_Contains_C_Before_I_Before_E_Is_Spelled_Incorrectly()
        {
            //Arrange
            var test = "Science";

            //Act
            var result = spellChecker.Check(test);

            //Assert
            Assert.IsFalse(result);
        }

    }

}
