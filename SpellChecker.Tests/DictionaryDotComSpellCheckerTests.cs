using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellChecker.Contracts;
using SpellChecker.Core;
using System;
using System.Net;
using System.Net.Http;
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
        public async Task Check_That_Get_Method_Response_OK_True()
        {
            //Arrange
            const string searchUrlBase = "http://dictionary.reference.com/browse/";
            const string word = "word";
            const string strUrl = searchUrlBase + word;

            //Act

            var client = new HttpClient
            {
                BaseAddress = new Uri(strUrl)
            };
            var response = await client.GetAsync(strUrl);
            var result = (response.StatusCode == HttpStatusCode.OK) ? true : false;

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task Check_That_Get_Method_Response_OK_False()
        {
            //Arrange
            const string searchUrlBase = "http://dictionary.reference.com/browse/";
            const string word = "worwd";
            const string strUrl = searchUrlBase + word;

            //Act

            var client = new HttpClient
            {
                BaseAddress = new Uri(strUrl)
            };
            var response = await client.GetAsync(strUrl);
            var result = (response.StatusCode == HttpStatusCode.OK) ? true : false;

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Check_That_FileAndServe_Is_Misspelled()
        {
            //Arrange
            const string word = "worwd";

            //Act
            var result = await spellChecker.Check(word);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task Check_That_South_Is_Not_Misspelled()
        {
            //Arrange
            const string word = "word";

            //Act        
            var result = await spellChecker.Check(word);

            //Assert
            Assert.IsTrue(result);
        }

    }

}
