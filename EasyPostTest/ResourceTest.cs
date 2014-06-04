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
            Data left = new Data() {foo = "foo", bar = 10, baz = new Inner() {qux = "qux"}};
            Data right = new Data() {foo = "oof", bar = 42, baz = new Inner() {qux = "xuq"}};
            Resource.Merge(left, right);

            Assert.AreEqual(left.foo, right.foo);
            Assert.AreEqual(left.bar, right.bar);
            Assert.AreEqual(left.baz.qux, right.baz.qux);
        }
    }
}
