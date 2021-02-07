using EasyPost;
using Newtonsoft.Json;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EasyPostTest {
    [TestClass]
    public class ResourceTest {
        class Inner : Resource {
            public string qux { get; set; }
        }

        class Data : Resource {
            public string foo { get; set; }
            public int bar { get; set; }
            public List<Inner> baz { get; set; }
        }

        [TestClass]
        public class ResourceExtenstionTest {
            Data dest, source;

            [TestInitialize]
            public void Initialize() {
                dest = new Data() { foo = "foo", bar = 10, baz = new List<Inner>() { new Inner() { qux = "qux" } } };
                source = new Data() { foo = "oof", bar = 42, baz = new List<Inner>() { new Inner() { qux = "xuq" } } };
            }

            [TestMethod]
            public void TestMerge() {
                dest.Merge(source);

                Assert.AreEqual(source.foo, dest.foo);
                Assert.AreEqual(source.bar, dest.bar);
                Assert.AreEqual(source.baz[0].qux, dest.baz[0].qux);
            }

            [TestMethod]
            public void TestAsDictionary() {
                Dictionary<string, object> dictionary = source.AsDictionary();

                Assert.AreEqual("oof", dictionary["foo"]);
                Assert.AreEqual(42, dictionary["bar"]);
                Assert.AreEqual("xuq", ((List<Dictionary<string, object>>)dictionary["baz"])[0]["qux"]);
            }

            [TestMethod]
            public void TestLoad() {
                Assert.AreEqual("oof", Resource.Load<Data>(JsonConvert.SerializeObject(source.AsDictionary())).foo);
                Assert.AreEqual("xuq", Resource.Load<Data>(JsonConvert.SerializeObject(source.AsDictionary())).baz[0].qux);
                Assert.AreEqual("oof", Resource.LoadFromDictionary<Data>(source.AsDictionary()).foo);
                Assert.AreEqual("xuq", Resource.LoadFromDictionary<Data>(source.AsDictionary()).baz[0].qux);
            }
        }
    }
}
