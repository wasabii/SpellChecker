using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SpellChecker.Contracts;
using SpellChecker.Core;
using System.Threading.Tasks;

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
            string word = "FileAndServe";
            bool bStatus = await spellChecker.Check(word);
            Assert.AreEqual(false,bStatus);

            //throw new NotImplementedException();
        }

        [TestMethod]
        public async Task Check_That_South_Is_Not_Misspelled()
        {
            string word = "South";
            Assert.AreNotEqual(false,await spellChecker.Check(word));
            //throw new NotImplementedException();
        }

    }

}
