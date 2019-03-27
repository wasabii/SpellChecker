using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellChecker.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellChecker.Tests
{
    [TestClass]
    public class NormalizeSentenceTest
    {
        [TestMethod]
        public void Extract_Unique_Words_Ensure_Extra_Space_Skipped()
        {
            //Arrange
            var sentence = "Salley sells seashellss by the seashore.  The shells Salley sells are surely by the sea.";

            //Act
            var words = Program.ExtractUniqueWords(sentence);

            //Assert
            Assert.IsFalse(words.Contains(""));
        }

        [TestMethod]
        public void Extract_Unique_Words_Only_Return_Unique_Words()
        {
            //Arrange
            var sentence = "Salley Salley Salley Salley Salley";

            //Act
            var words = Program.ExtractUniqueWords(sentence);

            //Assert
            Assert.AreEqual(1, words.Count);
        }

        [TestMethod]
        public void Extract_Unique_Words_Strip_Punctuation()
        {
            //Arrange
            var sentence = "Salley.";

            //Act
            var words = Program.ExtractUniqueWords(sentence);

            //Assert
            Assert.AreEqual("salley", words[0]);
        }
    }
}
