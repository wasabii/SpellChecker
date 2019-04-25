using Microsoft.Extensions.DependencyInjection;
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
            //spellChecker = new DictionaryDotComSpellChecker();
            spellChecker = new ServiceCollection()
        .AddScoped<ISpellChecker, DictionaryDotComSpellChecker>()
        .BuildServiceProvider().GetService<ISpellChecker>();
        }

        [TestMethod]
        public void Check_That_FileAndServe_Is_Misspelled()
        {
            Assert.IsFalse(spellChecker.Check("Beacch").Result);
        }

        [TestMethod]
        public void Check_That_South_Is_Not_Misspelled()
        {
            Assert.IsTrue(spellChecker.Check("Green").Result);
        }

    }

}
