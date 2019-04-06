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
        public void Check_That_FileAndServe_Is_Misspelled()
        {
            var result = spellChecker.Check("File And Serv ").GetAwaiter().GetResult();
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public async Task Check_That_FileAndServe_Is_Misspelled_Aync()
        {
            var result = await spellChecker.Check("File And Serv ");
            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void Check_That_South_Is_Not_Misspelled()
        {
            var result = spellChecker.Check("South").GetAwaiter().GetResult();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Check_That_South_Is_Not_Misspelled_Async()
        {
            var result = await spellChecker.Check("South");
            Assert.IsTrue(result);
        }

    }

}
