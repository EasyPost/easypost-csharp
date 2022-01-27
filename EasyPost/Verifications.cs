// Verifications.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

namespace EasyPost
{
    public class Verifications : Resource
    {
        public Verification delivery { get; set; }

        public Verification zip4 { get; set; }
    }
}
