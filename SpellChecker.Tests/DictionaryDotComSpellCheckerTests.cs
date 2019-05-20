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
    public class DictionaryDotComSpellCheckerTests
    {

        ISpellChecker spellChecker;

        [TestInitialize]
        public void TestFixureSetUp()
        {
            var serviceProvider = new ServiceCollection()
              .AddLogging((options) => {
                  options.AddConsole();
              })
              .AddSingleton<DictionaryDotComSpellChecker>()
              .BuildServiceProvider();

            spellChecker = serviceProvider.GetService<DictionaryDotComSpellChecker>();
        }

        [TestMethod]
        public async Task Check_That_FileAndServe_Is_Misspelled()
        {
            var output = await spellChecker.Check("FileandServe");

            Assert.IsFalse(output);
        }

        [TestMethod]
        public async Task Check_That_South_Is_Not_Misspelled()
        {
            var output = await spellChecker.Check("South");

            Assert.IsTrue(output);
        }

    }

}
