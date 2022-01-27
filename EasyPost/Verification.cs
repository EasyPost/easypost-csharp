// <copyright file="Verification.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace EasyPost
{
    public class Verification : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public bool success { get; set; }

        public List<Error> errors { get; set; }

        public List<VerificationDetails> details { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
