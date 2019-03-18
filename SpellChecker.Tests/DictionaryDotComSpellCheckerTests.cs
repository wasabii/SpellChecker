
using NUnit.Framework;
using SpellChecker.Contracts;
using SpellChecker.Core;
using System.Net;

namespace SpellChecker.Tests
{

    [TestFixture]
    public class DictionaryDotComSpellCheckerTests
    {

        ISpellChecker spellChecker;


        [OneTimeSetUp]
        public void TestFixureSetUp()
        {
            spellChecker = new DictionaryDotComSpellChecker();
            //Force to use TLS1.2 for network communication if possible          
            if (ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12) == false)
            {
                ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12;
            }
        }

        [Test]
        public void Check_That_FileAndServe_Is_Misspelled()
        {
            var result = spellChecker.Check("FileAndServe");
            Assert.IsFalse(result);
        }

        [Test]
        public void Check_That_South_Is_Not_Misspelled()
        {
            var result = spellChecker.Check("South");
            Assert.IsTrue(result);
        }

    }

}
