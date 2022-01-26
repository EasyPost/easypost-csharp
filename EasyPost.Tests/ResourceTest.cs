using EasyPost;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EasyPost.Tests
{
    [TestClass]
    public class ResourceTest
    {
        class Inner : Resource
        {
            public string qux { get; set; }
        }

        class Data : Resource
        {
            public string foo { get; set; }
            public int bar { get; set; }
            public List<Inner> baz { get; set; }
        }

        [TestClass]
        public class ResourceExtenstionTest
        {
            Data dest, source;

            [TestInitialize]
            public void Initialize()
            {
                dest = new Data() { foo = "foo", bar = 10, baz = new List<Inner>() { new Inner() { qux = "qux" } } };
                source = new Data() { foo = "oof", bar = 42, baz = new List<Inner>() { new Inner() { qux = "xuq" } } };
            }

            [TestMethod]
            public void TestMerge()
            {
                dest.Merge(source);

                Assert.AreEqual(dest.foo, source.foo);
                Assert.AreEqual(dest.bar, source.bar);
                Assert.AreEqual(dest.baz[0].qux, source.baz[0].qux);
            }

            [TestMethod]
            public void TestAsDictionary()
            {
                Dictionary<string, object> dictionary = source.AsDictionary();

                Assert.AreEqual(dictionary["foo"], "oof");
                Assert.AreEqual(dictionary["bar"], 42);
                Assert.AreEqual(((List<Dictionary<string, object>>)dictionary["baz"])[0]["qux"], "xuq");
            }

            [TestMethod]
            public void TestLoad()
            {
                Assert.AreEqual(Resource.Load<Data>(JsonConvert.SerializeObject(source.AsDictionary())).foo, "oof");
                Assert.AreEqual(Resource.Load<Data>(JsonConvert.SerializeObject(source.AsDictionary())).baz[0].qux,
                    "xuq");
                Assert.AreEqual(Resource.LoadFromDictionary<Data>(source.AsDictionary()).foo, "oof");
                Assert.AreEqual(Resource.LoadFromDictionary<Data>(source.AsDictionary()).baz[0].qux, "xuq");
            }
        }
    }
}