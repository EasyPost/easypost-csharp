// Verification.cs
// See LICENSE for licensing info.

using System.Collections.Generic;

namespace EasyPost
{
    public class Verification : Resource
    {
        public List<VerificationDetails> details { get; set; }
        public List<Error> errors { get; set; }
        public bool success { get; set; }
    }
}
