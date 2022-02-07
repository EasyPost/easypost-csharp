using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class RefundTest
    {
        // TODO: C# does not have a Refund class

        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "refund", true);
        }
    }
}
