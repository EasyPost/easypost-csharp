using System.Collections.Generic;
using EasyPost.Utilities;

namespace EasyPost.Models.API;

public class SmartrateAccuracy : ValueEnum
{
    public static readonly SmartrateAccuracy Percentile50 = new(1, "percentile_50");
    public static readonly SmartrateAccuracy Percentile75 = new(2, "percentile_75");
    public static readonly SmartrateAccuracy Percentile85 = new(3, "percentile_85");
    public static readonly SmartrateAccuracy Percentile90 = new(4, "percentile_90");
    public static readonly SmartrateAccuracy Percentile95 = new(5, "percentile_95");
    public static readonly SmartrateAccuracy Percentile97 = new(6, "percentile_97");
    public static readonly SmartrateAccuracy Percentile99 = new(7, "percentile_99");

    private SmartrateAccuracy(int id, string name)
        : base(id, name)
    {
    }

    public static IEnumerable<SmartrateAccuracy> All() => GetAll<SmartrateAccuracy>();
}
