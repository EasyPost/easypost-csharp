// VerificationDetails.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using Newtonsoft.Json;

namespace EasyPost
{
    public class VerificationDetails : Resource
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string time_zone { get; set; }

        /// <summary>
        ///     Deserialize JSON data into an object instance.
        /// </summary>
        /// <param name="json">JSON data to use for object creation.</param>
        /// <typeparam name="T">Type of object to generate.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        public static new T Load<T>(string json) where T : Resource => JsonConvert.DeserializeObject<T>(json);
    }
}
