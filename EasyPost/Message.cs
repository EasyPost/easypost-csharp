// Message.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

namespace EasyPost
{
    public class Message : Resource
    {
        public string carrier { get; set; }

        public string message { get; set; }

        public string type { get; set; }
    }
}
