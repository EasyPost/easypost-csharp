using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Exceptions.General;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Parameters.Shipment
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-form">Parameters</a> for <see cref="EasyPost.Services.ShipmentService.GenerateForm(string, GenerateForm, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GenerateForm : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     The type for the new form.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        ///     The data for the new form.
        /// </summary>
        // There's many different types of forms, so we just collect a generic dictionary of data to pass along rather than building per-form parameter sets.
        public Dictionary<string, object>? Data { get; set; }

        /// <summary>
        ///     Override the default <see cref="BaseParameters.ToDictionary"/> method to handle the unique serialization requirements for this parameter set.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey,TValue}"/>.</returns>
        /// <exception cref="MissingParameterError">Thrown when the form type was not provided.</exception>
        public override Dictionary<string, object> ToDictionary()
        {
            if (Type == null)
            {
                throw new MissingParameterError(nameof(Type));
            }

            Dictionary<string, object> data = Data ?? new Dictionary<string, object>();
            data.Add("type", Type!);

            return data.Wrap("form");
        }

        #endregion
    }
}
