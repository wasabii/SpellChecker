using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpellChecker.Console;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        [DataRow("Salley sells seashellss by the seashore.  The shells Salley sells are surely by the sea.", "Salley seashellss")]
        [DataRow("The shells Salley sells are surely by the sea", "Salley")]
        [DataRow("Either receive it or achieve it.", "Either")]
        [DataRow("The heir believes in science.", "heir science")]

        public async Task Check_Sentence_With_Misspellings_Returns_Misspellings(string sentence, string misspellings)
        {
            Assert.AreEqual(misspellings, await parser.GetMisspelledWords(sentence));
        }

        [DataTestMethod]
        [DataRow("Hello, Bob!")]
        [DataRow("The fierce brown fox jumped over the lazy collie.")]
        [DataRow("We deceived our friend into thinking the ceiling was a huge receipt.")]
        public async Task Check_Sentence_Without_Misspellings_Returns_No_Misspellings(string sentence)
        {
            Assert.AreEqual("", await parser.GetMisspelledWords(sentence));
        }

        //[TestMethod]
        public async Task Bench_Parser()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var result = await parser.GetMisspelledWords("Salley sells seashellss by the seashore.  The shells Salley sells are surely by the sea.");
            sw.Stop();
            System.Console.WriteLine($"Took {sw.ElapsedMilliseconds} milliseconds"); // -- 1184 ms
        }
    }
}
