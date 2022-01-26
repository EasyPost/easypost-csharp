// <copyright file="Verifications.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

namespace EasyPost
{
    public class Verifications : Resource
    {
        public Verification delivery { get; set; }
        public Verification zip4 { get; set; }
    }
}
