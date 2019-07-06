using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SpellChecker.Contracts;
using SpellChecker.Core;

using Moq;

namespace SpellChecker.Tests
{
    [TestClass]
    public class DictionaryDotComSpellCheckerTests
    {
        private ISpellChecker _spellChecker;
        private Mock<IHttpClient> _mockHttpClient;

        [TestInitialize]
        public void Initialize()
        {
            _mockHttpClient = new Mock<IHttpClient>();
            _spellChecker = new DictionaryDotComSpellChecker(_mockHttpClient.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockHttpClient = null;
            _spellChecker = null;
        }

        [TestMethod]
        public async Task Check_That_FileAndServe_Is_Misspelled()
        {
            _mockHttpClient
                .Setup(m => m.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                }));


            var isSpelledCorrectly = await _spellChecker.Check("FileAndServe");
            Assert.IsFalse(isSpelledCorrectly);
        }

        [TestMethod]
        public async Task Check_That_South_Is_Not_Misspelled()
        {
            _mockHttpClient
                .Setup(m => m.GetAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                }));

            var isSpelledCorrectly = await _spellChecker.Check("south");
            Assert.IsTrue(isSpelledCorrectly);
        }
    }
}
