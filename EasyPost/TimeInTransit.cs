// <copyright file="TimeInTransit.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

namespace EasyPost
{
    public class TimeInTransit
    {
#pragma warning disable IDE1006 // Naming Styles
        public int? percentile_50 { get; set; }
        public int? percentile_75 { get; set; }
        public int? percentile_85 { get; set; }
        public int? percentile_90 { get; set; }
        public int? percentile_95 { get; set; }
        public int? percentile_97 { get; set; }
        public int? percentile_99 { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
