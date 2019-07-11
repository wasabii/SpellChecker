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

        [DataTestMethod]
        [DataRow("believe")]
        [DataRow("fierce")]
        [DataRow("collie")]
        [DataRow("die")]
        [DataRow("friend")]
        [DataRow("deceive")]
        [DataRow("ceiling")]
        [DataRow("receipt")]
        public async Task Check_Words_That_Follow_I_Before_E_Except_After_C_Are_Not_Misspelled(string word)
        {
            Assert.IsTrue(await spellChecker.CheckAsync(word));
        }

        [DataTestMethod]
        [DataRow("heir")]
        [DataRow("protein")]
        [DataRow("science")]
        [DataRow("seeing")]
        [DataRow("their")]
        [DataRow("veil")]
        public async Task Check_Words_That_Do_Not_Follow_I_Before_E_Except_After_C_Are_Misspelled(string word)
        {
            Assert.IsFalse(await spellChecker.CheckAsync(word));
        }

    }

}
