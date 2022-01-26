// <copyright file="Verifications.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

namespace EasyPost
{
    public class Verifications : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public Verification zip4 { get; set; }
        public Verification delivery { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
