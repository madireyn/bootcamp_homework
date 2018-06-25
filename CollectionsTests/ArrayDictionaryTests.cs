using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Collections.Tests
{
    [TestClass()]
    public class ArrayDictionaryTests
    {
        private IDictionary<string, string> MakeBasicDictionary()
        {
            var dict = new ArrayDictionary<string, string>
            {
                { "keyA", "valA" },
                { "keyB", "valB" },
                { "keyC", "valC" }
            };
            return dict;
        }

        [TestMethod()]
        public void AddTest()
        {
            var dict = MakeBasicDictionary();
            dict.Add("keyD", "valD");
            dict.Add("keyE", "valE");

            Assert.IsTrue(dict.ContainsKey("keyA"));
            Assert.IsFalse(dict.ContainsKey("keyM"));
            Assert.IsTrue(dict.ContainsKey("keyC"));
            Assert.IsTrue(dict.ContainsKey("keyD"));
        }

        [TestMethod()]
        public void ClearTest()
        {
            var dict = MakeBasicDictionary();
            var initSize = dict.Count;
            Assert.IsTrue(dict.Count == initSize);

            dict.Clear();
            Assert.IsTrue(dict.Count == 0);
        }

        [TestMethod()]
        public void ContainsKeyTest()
        {
            var dict = MakeBasicDictionary();

            Assert.IsTrue(dict.ContainsKey("keyA"));
            Assert.IsFalse(dict.ContainsKey("keyD"));
            Assert.IsTrue(dict.ContainsKey("keyC"));
        }

        [TestMethod()]
        public void IsEmptyTest()
        {
            var dict = MakeBasicDictionary();
            dict.Clear();
            Assert.IsTrue(dict.IsEmpty());
        }

        [TestMethod()]
        public void RemoveTest()
        {
            var dict = MakeBasicDictionary();

            Assert.IsTrue(dict.ContainsKey("keyA"));
            Assert.IsTrue(dict.ContainsKey("keyB"));

            dict.Remove("keyA");
            dict.Remove("keyB");

            Assert.IsFalse(dict.ContainsKey("keyA"));
            Assert.IsFalse(dict.ContainsKey("keyB"));

        }

        [TestMethod()]
        public void ConstructorTest()
        {
            var dict1 = MakeBasicDictionary();
            var dict2 = MakeBasicDictionary();

            CollectionAssert.AreEqual(dict1.ToList(), dict2.ToList());
        }

        [TestMethod()]
        public void TryGetValueTest()
       {
           var returnValue = "";
           var dict = MakeBasicDictionary();
           dict.TryGetValue("keyA", out returnValue);
           Assert.IsTrue(returnValue.Equals("valA"));
       }

        [TestMethod()]
        public void CopyToTest()
        {
            var dict = MakeBasicDictionary();
            var collection = new KeyValuePair<string, string>[dict.Count];
            dict.CopyTo(collection, 0);
            Assert.IsTrue(collection.Length == dict.Count);
        }

        [TestMethod()]
        public void UpdatesSizeTest()
        {
            var dict = MakeBasicDictionary();
            var initSize = dict.Count;
            dict.Add("keyD", "valD");

            Assert.AreEqual(initSize + 1, dict.Count);
        }

        [TestMethod()]
        public void PutSameKeyTest()
        {
            var dict = new ArrayDictionary<string, string>();
            dict.Add("a", "b");
            var size = dict.Size();

            dict.Add("a", "c");
            Assert.AreEqual(dict.Count, size);
            Assert.AreEqual("c", dict.GetValue("a"));
            
        }
    }
}