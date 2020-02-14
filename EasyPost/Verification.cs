using System.Collections.Generic;

namespace EasyPost {
    public class Verification : Resource {
        public bool success { get; set; }
        public List<Error> errors { get; set; }
        public List<VerificationDetails> details { get; set; }
    }
}
