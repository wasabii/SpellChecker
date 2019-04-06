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
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly_Async()
        {
            var bresult = await spellChecker.Check("believe");
            var fresult = await spellChecker.Check("fierce");
            var cresult = await spellChecker.Check("ceiling");
            Task.WaitAll();

            Assert.IsTrue(bresult && fresult && cresult);
        }

        [TestMethod]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            bool bresult = spellChecker.Check("believe").GetAwaiter().GetResult();
            bool fresult = spellChecker.Check("fierce").GetAwaiter().GetResult();
            var cresult = spellChecker.Check("ceiling").GetAwaiter().GetResult();
            Assert.IsTrue(bresult && fresult && cresult);
        }

        [TestMethod]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            var bresult = spellChecker.Check("beleive").GetAwaiter().GetResult();
            var fresult = spellChecker.Check("feirce").GetAwaiter().GetResult();
            var cresult = spellChecker.Check("cieling").GetAwaiter().GetResult();
            Assert.IsTrue(!bresult && !fresult && !cresult);
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly_Async()
        {
            var bresult = await spellChecker.Check("beleive");
            var fresult = await spellChecker.Check("feirce");
            var cresult = await spellChecker.Check("cieling");
            Task.WaitAll();
            Assert.IsTrue(!bresult && !fresult && !cresult);
        }

    }

}
