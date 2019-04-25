using Microsoft.Extensions.DependencyInjection;
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
            //spellChecker = new MnemonicSpellCheckerIBeforeE();
            spellChecker = new ServiceCollection()
                     .AddScoped<ISpellChecker, MnemonicSpellCheckerIBeforeE>()
                     .BuildServiceProvider().GetService<ISpellChecker>();
        }

        [TestMethod]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            Assert.IsTrue(spellChecker.Check("receipt").Result);
            Assert.IsTrue(spellChecker.Check("priest").Result);
            Assert.IsTrue(spellChecker.Check("niece").Result);
            Assert.IsTrue(spellChecker.Check("friend").Result);
        }

        [TestMethod]
        public void Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            Assert.IsFalse(spellChecker.Check("science").Result);
            Assert.IsFalse(spellChecker.Check("their").Result);
        }

    }

}
