// <copyright file="TaxIdentifier.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

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
