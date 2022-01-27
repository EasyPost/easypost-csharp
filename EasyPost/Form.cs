// Form.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System;

namespace EasyPost
{
    public class Form : Resource
    {
        public DateTime? created_at { get; set; }
        public string form_type { get; set; }
        public string form_url { get; set; }
        public string id { get; set; }
        public string mode { get; set; }
        public bool submitted_electronically { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
