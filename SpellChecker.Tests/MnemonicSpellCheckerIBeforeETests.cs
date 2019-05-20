using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            var serviceProvider = new ServiceCollection()
              .AddLogging((options) => {
                  options.AddConsole();
              })
              .AddSingleton<MnemonicSpellCheckerIBeforeE>()
              .BuildServiceProvider();

            spellChecker = serviceProvider.GetService<MnemonicSpellCheckerIBeforeE>();
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Correctly()
        {
            var output = await spellChecker.Check("believe");

            Assert.IsTrue(output);
        }

        [TestMethod]
        public async Task Check_Word_That_Contains_I_Before_E_Is_Spelled_Incorrectly()
        {
            var output = await spellChecker.Check("science");

            Assert.IsFalse(output);
        }

    }

}
