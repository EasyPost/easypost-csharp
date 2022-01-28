// Verifications.cs
// See LICENSE for licensing info.

namespace EasyPost
{
    public class Verifications : Resource
    {
        public Verification delivery { get; set; }
        public Verification zip4 { get; set; }
    }
}
