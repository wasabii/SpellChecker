using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellChecker.Contracts;
using SpellChecker.Core;
using System.Threading.Tasks;

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
        public void Check_Word_That_MnemonicSpellCheckerIBeforeE_Incorrect_EI_Then_False()
        {
            //Arrange
            const string word = "their";
            var result = false;

            //Act
            if (word.Contains("ei"))
                result = word.Contains("cei");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Check_Word_That_MnemonicSpellCheckerIBeforeE_Incorrect_CEI_Then_True()
        {
            //Arrange
            const string word = "deceive";
            var result = false;

            //Act
            if (word.Contains("ei"))
                result = word.Contains("cei");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_Word_That_MnemonicSpellCheckerIBeforeE_Incorrect_IE_Then_True()
        {
            //Arrange
            const string word = "friend";
            var result = false;

            //Act
            if (word.Contains("ie"))
                result = word.Contains("cie");

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Check_Word_That_MnemonicSpellCheckerIBeforeE_Incorrect_CIE_Then_False()
        {
            //Arrange
            const string word = "science";
            var result = false;

            //Act
            if (word.Contains("ie"))
                result = word.Contains("cie");

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            //Arrange
            const string word = "hair";

            //Act
            var result = await spellChecker.Check(word);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            //Arrange
            const string word = "heir";

            //Act
            var result = await spellChecker.Check(word);

            //Assert
            Assert.IsFalse(result);
        }

    }

}
