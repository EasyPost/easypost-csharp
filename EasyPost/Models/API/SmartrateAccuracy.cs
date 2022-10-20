using System.Collections.Generic;
using EasyPost.Utilities;

namespace EasyPost.Models.API
{
    public class SmartrateAccuracy : ValueEnum
    {
        public static readonly SmartrateAccuracy Percentile50 = new SmartrateAccuracy(1, "percentile_50");
        public static readonly SmartrateAccuracy Percentile75 = new SmartrateAccuracy(2, "percentile_75");
        public static readonly SmartrateAccuracy Percentile85 = new SmartrateAccuracy(3, "percentile_85");
        public static readonly SmartrateAccuracy Percentile90 = new SmartrateAccuracy(4, "percentile_90");
        public static readonly SmartrateAccuracy Percentile95 = new SmartrateAccuracy(5, "percentile_95");
        public static readonly SmartrateAccuracy Percentile97 = new SmartrateAccuracy(6, "percentile_97");
        public static readonly SmartrateAccuracy Percentile99 = new SmartrateAccuracy(7, "percentile_99");

        private SmartrateAccuracy(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<SmartrateAccuracy> All()
        {
            return GetAll<SmartrateAccuracy>();
        }
    }
}
