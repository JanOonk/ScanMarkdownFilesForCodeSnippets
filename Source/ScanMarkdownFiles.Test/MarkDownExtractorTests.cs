using NUnit.Framework;
using ScanMarkdownFiles.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ScanMarkdownFiles.Test
{
    [TestFixture]
    public class MarkDownExtractorTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
        }

        [OneTimeTearDown]
        public void TearDown()
        {
        }

        [SetUp]
        public void InitializeTest()
        {
            //Called BEFORE each test
        }

        [TearDown]
        public void DeInitializeTest()
        {
            //Called AFTER each test
        }


        [Test]
        public static void TestMarkDig1()
        {
            string contents;
            string filename = @"TestFiles\Test.md";
            contents = File.ReadAllText(filename);

            List<Models.CodeBlock> codeBlocks = MarkDownExtractor.ExtractCodeBlocks(contents);
            MarkDownFile markDownFile = new MarkDownFile(filename, codeBlocks);
            Assert.IsTrue(markDownFile.CodeBlocks.Count == 9);
            Assert.IsTrue(markDownFile.InlineCodeBlocksCount == 2);
            Assert.IsTrue(markDownFile.FencedCodeBlocksCount == 7);
            Assert.IsTrue(markDownFile.UniqueFencedCodeBlockTypes == 2);
            CollectionAssert.AreEquivalent(markDownFile.CountPerFencedCodeBlockType, new Dictionary<string, int>() {
                { "", 1},
                { "console", 6}
            });

            Assert.IsTrue(codeBlocks[0] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[1] is InlineCodeBlock);

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).StartLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).EndLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).Contents == "singleline codeblock1");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).StartLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).EndLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).Contents == "singleline codeblock2");

            Assert.IsTrue(codeBlocks[2] is FencedCodeBlock);
            Assert.IsTrue(codeBlocks[3] is FencedCodeBlock);
            Assert.IsTrue(codeBlocks[4] is FencedCodeBlock);
            Assert.IsTrue(codeBlocks[5] is FencedCodeBlock);
            Assert.IsTrue(codeBlocks[6] is FencedCodeBlock);

            Assert.IsTrue(((FencedCodeBlock)codeBlocks[2]).NrOfLines == 3);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[2]).StartLineNr == 3);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[2]).EndLineNr == 5);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[2]).Contents == "multiline1-line1/1");

            Assert.IsTrue(((FencedCodeBlock)codeBlocks[3]).NrOfLines == 4);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[3]).StartLineNr == 7);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[3]).EndLineNr == 10);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[3]).Type == "console");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[3]).Arguments == "");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[3]).Contents == "multiline2-line1/2\r\nmultiline2-line2/2");

            Assert.IsTrue(((FencedCodeBlock)codeBlocks[4]).NrOfLines == 4);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[4]).StartLineNr == 12);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[4]).EndLineNr == 15);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[4]).Type == "console");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[4]).Arguments == "");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[4]).Contents == "multiline3-line1/2\r\nmultiline3-line2/2");

            Assert.IsTrue(((FencedCodeBlock)codeBlocks[5]).NrOfLines == 4);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[5]).StartLineNr == 17);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[5]).EndLineNr == 20);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[5]).Type == "console");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[5]).Arguments == "argument1");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[5]).Contents == "multiline4-line1/2\r\nmultiline4-line2/2");

            Assert.IsTrue(((FencedCodeBlock)codeBlocks[6]).NrOfLines == 4);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[6]).StartLineNr == 22);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[6]).EndLineNr == 25);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[6]).Type == "console");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[6]).Arguments == "");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[6]).Contents == " multiline5-line1/2\r\nmultiline5-line2/2");

            Assert.IsTrue(((FencedCodeBlock)codeBlocks[7]).NrOfLines == 4);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[7]).StartLineNr == 27);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[7]).EndLineNr == 30);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[7]).Type == "console");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[7]).Arguments == "");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[7]).Contents == "multiline6-line1/2\r\n multiline6-line2/2");

            Assert.IsTrue(((FencedCodeBlock)codeBlocks[8]).NrOfLines == 4);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[8]).StartLineNr == 32);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[8]).EndLineNr == 35);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[8]).Type == "console");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[8]).Arguments == "argument1");
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[8]).Contents == " multiline7-line1/2\r\n multiline7-line2/2");
        }

        [Test]
        public static void TestMarkDig2()
        {
            string filename = @"TestFiles\OneSingleLineBlock1.md";
            string contents = File.ReadAllText(filename);

            List<Models.CodeBlock> codeBlocks = MarkDownExtractor.ExtractCodeBlocks(contents);
            MarkDownFile markDownFile = new MarkDownFile(filename, codeBlocks);
            Assert.IsTrue(markDownFile.CodeBlocks.Count == 4);
            Assert.IsTrue(markDownFile.InlineCodeBlocksCount == 4);
            Assert.IsTrue(markDownFile.FencedCodeBlocksCount == 0);
            Assert.IsTrue(markDownFile.UniqueFencedCodeBlockTypes == 0);
            CollectionAssert.AreEquivalent(markDownFile.CountPerFencedCodeBlockType, new Dictionary<string, int>()
            {
            });

            Assert.IsTrue(codeBlocks[0] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[1] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[2] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[3] is InlineCodeBlock);

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).StartLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).EndLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).Contents == "This is a test");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).StartLineNr == 3);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).EndLineNr == 3);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).Contents == "This is a test");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).StartLineNr == 5);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).EndLineNr == 5);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).Contents == "This is a test");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).StartLineNr == 7);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).EndLineNr == 7);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).Contents == "This is a test");
        }

        [Test]
        public static void TestMarkDig3()
        {
            string filename = @"TestFiles\OneSingleLineBlock2.md";
            string contents = File.ReadAllText(filename);

            List<Models.CodeBlock> codeBlocks = MarkDownExtractor.ExtractCodeBlocks(contents);
            MarkDownFile markDownFile = new MarkDownFile(filename, codeBlocks);
            Assert.IsTrue(markDownFile.CodeBlocks.Count == 7);
            Assert.IsTrue(markDownFile.InlineCodeBlocksCount == 7);
            Assert.IsTrue(markDownFile.FencedCodeBlocksCount == 0);
            Assert.IsTrue(markDownFile.UniqueFencedCodeBlockTypes == 0);
            CollectionAssert.AreEquivalent(markDownFile.CountPerFencedCodeBlockType, new Dictionary<string, int>()
            {
            });

            Assert.IsTrue(codeBlocks[0] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[1] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[2] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[3] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[4] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[5] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[6] is InlineCodeBlock);

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).StartLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).EndLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).Contents == "This is one``````singleline block");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).StartLineNr == 3);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).EndLineNr == 3);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).Contents == "This is one ``````singleline block");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).StartLineNr == 5);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).EndLineNr == 5);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).Contents == "This is one`````` singleline block");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).StartLineNr == 7);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).EndLineNr == 7);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).Contents == "This is one `````` singleline block");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[4]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[4]).StartLineNr == 9);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[4]).EndLineNr == 9);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[4]).Contents == "This is one``````singleline block");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[5]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[5]).StartLineNr == 11);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[5]).EndLineNr == 11);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[5]).Contents == "This is one ``````singleline block");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[6]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[6]).StartLineNr == 13);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[6]).EndLineNr == 13);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[6]).Contents == "This is one`````` singleline block");

        }

        [Test]
        public static void TestMarkDig4()
        {
            string filename = @"TestFiles\MultipleSingleLineBlocks1.md";
            string contents = File.ReadAllText(filename);

            List<Models.CodeBlock> codeBlocks = MarkDownExtractor.ExtractCodeBlocks(contents);
            MarkDownFile markDownFile = new MarkDownFile(filename, codeBlocks);
            Assert.IsTrue(markDownFile.CodeBlocks.Count == 8);
            Assert.IsTrue(markDownFile.InlineCodeBlocksCount == 8);
            Assert.IsTrue(markDownFile.FencedCodeBlocksCount == 0);
            Assert.IsTrue(markDownFile.UniqueFencedCodeBlockTypes == 0);
            CollectionAssert.AreEquivalent(markDownFile.CountPerFencedCodeBlockType, new Dictionary<string, int>()
            {
            });

            Assert.IsTrue(codeBlocks[0] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[1] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[2] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[3] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[4] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[5] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[6] is InlineCodeBlock);
            Assert.IsTrue(codeBlocks[7] is InlineCodeBlock);

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).StartLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).EndLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[0]).Contents == "This is a test");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).StartLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).EndLineNr == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[1]).Contents == "This is a test");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).StartLineNr == 3);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).EndLineNr == 3);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[2]).Contents == "This is a test");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).StartLineNr == 3);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).EndLineNr == 3);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[3]).Contents == "This is a test");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[4]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[4]).StartLineNr == 5);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[4]).EndLineNr == 5);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[4]).Contents == "This is a test");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[5]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[5]).StartLineNr == 5);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[5]).EndLineNr == 5);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[5]).Contents == "This is a test");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[6]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[6]).StartLineNr == 7);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[6]).EndLineNr == 7);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[6]).Contents == "This is a test");

            Assert.IsTrue(((InlineCodeBlock)codeBlocks[7]).NrOfLines == 1);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[7]).StartLineNr == 7);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[7]).EndLineNr == 7);
            Assert.IsTrue(((InlineCodeBlock)codeBlocks[7]).Contents == "This is a test");

        }

        [Test]
        public static void TestMarkDig5()
        {
            string filename = @"TestFiles\Test2.md";
            string contents = File.ReadAllText(filename);

            List<Models.CodeBlock> codeBlocks = MarkDownExtractor.ExtractCodeBlocks(contents);
            MarkDownFile markDownFile = new MarkDownFile(filename, codeBlocks);
            Assert.IsTrue(markDownFile.CodeBlocks.Count == 3);
            Assert.IsTrue(markDownFile.InlineCodeBlocksCount == 0);
            Assert.IsTrue(markDownFile.FencedCodeBlocksCount == 3);
            Assert.IsTrue(markDownFile.UniqueFencedCodeBlockTypes == 1);
            CollectionAssert.AreEquivalent(markDownFile.CountPerFencedCodeBlockType, new Dictionary<string, int>()
            {
                { "", 3 }
            });

            Assert.IsTrue(((FencedCodeBlock)codeBlocks[0]).NrOfLines == 5);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[0]).StartLineNr == 1);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[0]).EndLineNr == 5);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[0]).Contents == "```\r\nLook! You can see my backticks.\r\n```");

            Assert.IsTrue(((FencedCodeBlock)codeBlocks[1]).NrOfLines == 3);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[1]).StartLineNr == 7);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[1]).EndLineNr == 9);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[1]).Contents == "Should work");

            Assert.IsTrue(((FencedCodeBlock)codeBlocks[2]).NrOfLines == 3);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[2]).StartLineNr == 11);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[2]).EndLineNr == 13);
            Assert.IsTrue(((FencedCodeBlock)codeBlocks[2]).Contents == "Should work");
        }

    }
}