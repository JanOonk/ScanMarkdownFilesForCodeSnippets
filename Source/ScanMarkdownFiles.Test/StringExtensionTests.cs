using NUnit.Framework;
using ScanMarkdownFiles.Extensions;
using System;

namespace ScanMarkdownFiles.Test
{
    [TestFixture]
    public class StringExtensionTests
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
        public void StringExtension_Indent()
        {
            Assert.IsTrue("ab".Indent("xx") == "xxab");
        }

        [Test]
        public void StringExtension_Like()
        {
            string[] testCases = new string[] { "Test", "test", "TEST", "TeSt" };
            foreach (string s1 in testCases)
            {
                for (int i = 0; i < s1.Length; i++)
                {
                    string s2 = testCases[i];
                    Console.WriteLine($"{s1} should be like {s2}");
                    Assert.IsTrue(s1.Like(s2));
                }
            }
        }

        [Test]
        public void StringExtension_IndentWithLevel()
        {
            //single line
            Assert.IsTrue("ab".IndentWithLevel(0, "xx") == "ab");
            Assert.IsTrue("ab".IndentWithLevel(1, "xx") == "xxab");
            Assert.IsTrue("ab".IndentWithLevel(2, "xx") == "xxxxab");

            //multi line Windows style
            Assert.IsTrue("ab\r\nab".IndentWithLevel(0, "xx") == $"ab{Environment.NewLine}ab");
            Assert.IsTrue("ab\r\nab".IndentWithLevel(1, "xx") == $"xxab{Environment.NewLine}xxab");
            Assert.IsTrue("ab\r\nab".IndentWithLevel(2, "xx") == $"xxxxab{Environment.NewLine}xxxxab");

            //multi line Unix style
            Assert.IsTrue("ab\nab".IndentWithLevel(0, "xx") == $"ab{Environment.NewLine}ab");
            Assert.IsTrue("ab\nab".IndentWithLevel(1, "xx") == $"xxab{Environment.NewLine}xxab");
            Assert.IsTrue("ab\nab".IndentWithLevel(2, "xx") == $"xxxxab{Environment.NewLine}xxxxab");
        }

        [Test]
        public void StringExtension_LineNrFromIndex()
        {
            //Non-Unix newline = \r\n 
            //    Unix newline = \n 
            string input = String.Join(Environment.NewLine,
                            "01", //line 0
                            "0",  //line 1
                            "",   //line 2
                            "012",//line 3
                            "",   //line 4
                            "",   //line 5
                            "0"); //line 6

            int l = Environment.NewLine.Length;

            Assert.IsTrue(input.LineIndexFromPos(l * 0 + 0 + 0) == 0);
            Assert.IsTrue(input.LineIndexFromPos(l * 0 + 0 + 1) == 0);
            Assert.IsTrue(input.LineIndexFromPos(l * 0 + 0 + 1 + l) == 1); //newline

            Assert.IsTrue(input.LineIndexFromPos(l * 1 + 2 + 0) == 1);
            Assert.IsTrue(input.LineIndexFromPos(l * 1 + 2 + 0 + l) == 2); //newline

            Assert.IsTrue(input.LineIndexFromPos(l * 3 + 3 + 0) == 3);
            Assert.IsTrue(input.LineIndexFromPos(l * 3 + 3 + 1) == 3);
            Assert.IsTrue(input.LineIndexFromPos(l * 3 + 3 + 2) == 3);
            Assert.IsTrue(input.LineIndexFromPos(l * 3 + 3 + 2 + l) == 4); //newline
            Assert.IsTrue(input.LineIndexFromPos(l * 3 + 3 + 2 + l + l) == 5); //newline

            Assert.IsTrue(input.LineIndexFromPos(l * 6 + 6 + 0) == 6);
        }
    }
}