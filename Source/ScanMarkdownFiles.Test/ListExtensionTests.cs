using NUnit.Framework;
using ScanMarkdownFiles.Extensions;
using System;
using System.Collections.Generic;

namespace ScanMarkdownFiles.Test
{
    [TestFixture]
    public class ListExtensionTests
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
        public void ListExtension_Indent()
        {
            List<string> input = new List<string>() { "1", "2", "3" };
            List<string> expected = new List<string>() { "xx1", "xx2", "xx3" };
            List<string> actual = input.Indent("xx");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ListExtension_IndentWithLevel()
        {
            List<string> input = new List<string>() { "1", "2", "3" };
            List<string> expected = new List<string>() { "xxxx1", "xxxx2", "xxxx3" };
            List<string> actual = input.IndentWithLevel(2, "xx");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ListExtension_MakeIndentedString()
        {
            List<string> input = new List<string>() { "1", "2", "3" };
            string expected = $"xx1{Environment.NewLine}xx2{Environment.NewLine}xx3";
            string actual = input.MakeIndentedString("xx");

            Assert.IsTrue(actual == expected);
        }

        [Test]
        public void ListExtension_MakeIndentedStringWithLevel()
        {
            List<string> input = new List<string>() { "1", "2", "3" };
            string expected = $"xxxx1{Environment.NewLine}xxxx2{Environment.NewLine}xxxx3";
            string actual = input.MakeIndentedStringWithLevel(2, "xx");

            Assert.IsTrue(actual == expected);
        }
    }
}