using Microsoft.VisualStudio.TestTools.UnitTesting;
using Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.Tests
{
    [TestClass()]
    public class ArrayDictionaryTests
    {
        [TestMethod()]
        public void PutSameKeyTest()
        {
            IDictionary<string, string> dict = new ArrayDictionary<string, string>();
            dict.Add("a", "b");
            var size = dict.Count;

            dict.Add("a", "c");
            Assert.AreEqual(size, dict.Count);
        }

        [TestMethod()]
        public void ContainsKeyTest()
        {
            IDictionary<string, int> dict = new ArrayDictionary<string, int>();

            dict.Add("a", 1);
            dict.Add("b", 2);
            dict.Add("c", 3);
            dict.Add("a", 4);
            dict.RemoveKeyValuePair("c");
            dict.Add("c", 5);
            dict.Add("d", 6);
            dict.Add("a", 5);
            dict.RemoveKeyValuePair("c");

            Assert.IsTrue(dict.ContainsKey("a"));
            Assert.IsTrue(dict.ContainsKey("b"));
            Assert.IsFalse(dict.ContainsKey("c"));
            Assert.IsTrue(dict.ContainsKey("d"));
            Assert.IsFalse(dict.ContainsKey("e"));
        }

        [TestMethod()]
        public void UpdatesSizeTest()
        {
            IDictionary<string, string> dict = new ArrayDictionary<string, string>();
            var initSize = dict.Count;
            dict.Add("keyA", "valA");

            Assert.AreEqual(initSize + 1, dict.Count);
        }
    }
}