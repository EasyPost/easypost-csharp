﻿// WebhookTest.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.VisualStudio.TestTools.UnitTesting;

// namespace EasyPost.Tests {
//     [TestClass]
//     public class WebhookTest {
//         [TestInitialize]
//         public void Initialize() {
//             ClientManager.SetCurrent("GxhY479LTioDWsGcEtSAfQ");
//         }

//         [TestMethod]
//         public void TestCRUD() {
//             var webhook = Webhook.Create(new Dictionary<string, object>() { { "url", "https://www.foobar.com" } });
//             Assert.AreEqual(webhook.url, "https://www.foobar.com");

//             webhook.Update();

//             var webhooks = Webhook.List();
//             CollectionAssert.Contains(webhooks.Select(w => w.id).ToList(), webhook.id);

//             webhook.Destroy();
//             try {
//                 User.Retrieve(webhook.id);
//                 Assert.Fail();
//             }
//             catch (HttpException) { }
//         }
//     }
// }







