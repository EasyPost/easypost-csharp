// using EasyPost;
// using Microsoft.VisualStudio.TestTools.UnitTesting;

// namespace EasyPost.Tests {
//     [TestClass]
//     public class ClientManagerTest {
//         [TestMethod]
//         [ExpectedException(typeof(ClientNotConfigured))]
//         public void TestNotConfigured() {
//             ClientManager.Unconfigure();
//             ClientManager.Build();
//         }

//         [TestMethod]
//         public void TestApiKey() {
//             ClientManager.SetCurrent("apiKey");
//             Client client = ClientManager.Build();
//             Assert.AreEqual("apiKey", client.configuration.ApiKey);
//         }

//         [TestMethod]
//         public void TestApiKeyForUniqueObjects() {
//             ClientManager.SetCurrent("apiKey");
//             Client client1 = ClientManager.Build();
//             Client client2 = ClientManager.Build();

//             Assert.AreNotSame(client1, client2);
//         }

//         [TestMethod]
//         public void TestFactoryDelegate() {
//             ClientManager.SetCurrent(() => new Client(new ClientConfiguration("apiKey")));
//             Client client = ClientManager.Build();
//             Assert.AreEqual("apiKey", client.configuration.ApiKey);
//         }

//         [TestMethod]
//         public void TestFactoryDelegateForUniqueObjects() {
//             ClientManager.SetCurrent(() => new Client(new ClientConfiguration("apiKey")));
//             Client client1 = ClientManager.Build();
//             Client client2 = ClientManager.Build();

//             Assert.AreNotSame(client1, client2);
//         }
//     }
// }

