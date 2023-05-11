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
        public Dictionary<string, object>? Data { get; set; }
        
        internal override Dictionary<string, object> ToDictionary()
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
