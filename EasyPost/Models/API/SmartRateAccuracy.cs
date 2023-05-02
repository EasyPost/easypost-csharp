namespace EasyPost.Models.API
{
    /// <summary>
    ///     Enum representing the available smart rate accuracy levels.
    /// </summary>
    public enum SmartRateAccuracy
    {
        /// <summary>
        ///     The 50th percentile.
        /// </summary>
        Percentile50,

        /// <summary>
        ///     The 75th percentile.
        /// </summary>
        Percentile75,

        /// <summary>
        ///     The 85th percentile.
        /// </summary>
        Percentile85,

        /// <summary>
        ///     The 90th percentile.
        /// </summary>
        Percentile90,

        /// <summary>
        ///     The 95th percentile.
        /// </summary>
        Percentile95,

        /// <summary>
        ///     The 97th percentile.
        /// </summary>
        Percentile97,

        /// <summary>
        ///     The 99th percentile.
        /// </summary>
        Percentile99,
    }
}
