using EasyPost;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    class Inner {
        public string qux { get; set; }
    }

    class Data {
        public string foo { get; set; }
        public int bar { get; set; }
        public Inner baz { get; set; }
    }

    [TestClass]
    public class ResourceTest {
        [TestMethod]
        public void TestMerge() {
            Data dest = new Data() {foo = "foo", bar = 10, baz = new Inner() {qux = "qux"}};
            Data source = new Data() {foo = "oof", bar = 42, baz = new Inner() {qux = "xuq"}};
            dest.Merge(source);

            Assert.AreEqual(dest.foo, source.foo);
            Assert.AreEqual(dest.bar, source.bar);
            Assert.AreEqual(dest.baz.qux, source.baz.qux);
        }
    }
}
