// TaxIdentifier.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

namespace EasyPost
{
    public class TaxIdentifier
    {
        public string entity { get; set; }

        public string issuing_country { get; set; }

        public string tax_id { get; set; }

        public string tax_id_type { get; set; }
    }
}
