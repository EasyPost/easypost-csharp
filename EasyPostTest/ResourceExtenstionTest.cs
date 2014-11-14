using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    class Inner : IResource {
        public string qux { get; set; }
    }

    class Data : IResource {
        public string foo { get; set; }
        public int bar { get; set; }
        public Inner baz { get; set; }
    }

    [TestClass]
    public class ResourceExtenstionTest {
        Data dest, source;

        [TestInitialize]
        public void Initialize() {
            dest = new Data() { foo = "foo", bar = 10, baz = new Inner() { qux = "qux" } };
            source = new Data() { foo = "oof", bar = 42, baz = new Inner() { qux = "xuq" } };
        }

        [TestMethod]
        public void TestMerge() {
            dest.Merge(source);

            Assert.AreEqual(dest.foo, source.foo);
            Assert.AreEqual(dest.bar, source.bar);
            Assert.AreEqual(dest.baz.qux, source.baz.qux);
        }

        [TestMethod]
        public void TestAsDictionary() {
            Dictionary<string, object> dictionary = source.AsDictionary();

            Assert.AreEqual(dictionary["foo"], "oof");
            Assert.AreEqual(dictionary["bar"], 42);
            Assert.AreEqual(((Dictionary<string, object>)dictionary["baz"])["qux"], "xuq");
        }
    }
}
