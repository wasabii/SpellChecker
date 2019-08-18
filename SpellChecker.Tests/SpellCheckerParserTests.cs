using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellChecker.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellChecker.Tests
{
    [TestClass]
    public class SpellCheckerParserTests
    {
        SpellCheckerParser parser;

        [TestInitialize]
        public void TestFixureSetUp()
        {
            parser = new SpellCheckerParser();
        }

        [DataTestMethod]
        [DataRow("Salley sells seashellss by the seashore.", "Salley seashellss")]
        [DataRow("Please enter a sentence: Salley sells seashellss by the seashore.  The shells Salley sells are surely by the sea.", "Salley seashellss")]
        [DataRow("The shells Salley sells are surely by the sea", "Salley")]
        [DataRow("Either receive it or achieve it.", "Either")]
        [DataRow("The heir believes in science.", "heir science")]

        public void Check_Sentence_With_Misspellings_Returns_Misspellings(string sentence, string misspellings)
        {
            Assert.AreEqual(misspellings, parser.GetMisspelledWords(sentence));
        }

        [DataTestMethod]
        [DataRow("Hello Bob!")]
        [DataRow("The fierce brown fox jumped over the lazy collie.")]
        [DataRow("We deceived our friend into thinking the ceiling was actually a receipt.")]
        public void Check_Sentence_Without_Misspellings_Returns_No_Misspellings(string sentence)
        {
            Assert.AreEqual("", parser.GetMisspelledWords(sentence));
        }
    }
}
