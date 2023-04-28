using System.Collections.Generic;
using EasyPost.Utilities.Internal;

namespace EasyPost.Models.API
{
    public class SmartRateAccuracy : ValueEnum
    {
        public static readonly SmartRateAccuracy Percentile50 = new(1, "percentile_50");
        public static readonly SmartRateAccuracy Percentile75 = new(2, "percentile_75");
        public static readonly SmartRateAccuracy Percentile85 = new(3, "percentile_85");
        public static readonly SmartRateAccuracy Percentile90 = new(4, "percentile_90");
        public static readonly SmartRateAccuracy Percentile95 = new(5, "percentile_95");
        public static readonly SmartRateAccuracy Percentile97 = new(6, "percentile_97");
        public static readonly SmartRateAccuracy Percentile99 = new(7, "percentile_99");

        private SmartRateAccuracy(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<SmartRateAccuracy> All() => GetAll<SmartRateAccuracy>();
    }
}
