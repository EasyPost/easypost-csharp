// <copyright file="ClientManagerTest.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

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
//             var client = ClientManager.Build();
//             Assert.AreEqual("apiKey", client.configuration.ApiKey);
//         }

//         [TestMethod]
//         public void TestApiKeyForUniqueObjects() {
//             ClientManager.SetCurrent("apiKey");
//             var client1 = ClientManager.Build();
//             var client2 = ClientManager.Build();

//             Assert.AreNotSame(client1, client2);
//         }

//         [TestMethod]
//         public void TestFactoryDelegate() {
//             ClientManager.SetCurrent(() => new Client(new ClientConfiguration("apiKey")));
//             var client = ClientManager.Build();
//             Assert.AreEqual("apiKey", client.configuration.ApiKey);
//         }

//         [TestMethod]
//         public void TestFactoryDelegateForUniqueObjects() {
//             ClientManager.SetCurrent(() => new Client(new ClientConfiguration("apiKey")));
//             var client1 = ClientManager.Build();
//             var client2 = ClientManager.Build();

//             Assert.AreNotSame(client1, client2);
//         }
//     }
// }



