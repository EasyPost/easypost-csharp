using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class PaymentMethodTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("payment_method", TestUtils.ApiKey.Production);
        }

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            PaymentMethod paymentMethods = await PaymentMethod.All();

            Assert.IsNotNull(paymentMethods.primary_payment_method);
            Assert.IsNotNull(paymentMethods.secondary_payment_method);
        }
    }
}
