using System.Collections.Generic;

namespace EasyPost {
    public class Verification : Resource {
#pragma warning disable IDE1006 // Naming Styles
        public bool success { get; set; }
        public List<Error> errors { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}