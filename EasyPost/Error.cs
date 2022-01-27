// Error.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class Error : Resource
    {
        public string code { get; set; }

        public List<Error> errors { get; set; }

        public string field { get; set; }

        public string message { get; set; }

        public string suggestion { get; set; }


        /// <summary>
        ///     Create an Error from JSON.
        /// </summary>
        /// <param name="json">JSON data to use for Error creation.</param>
        /// <typeparam name="T">The type of object to deserialize the JSON data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        public static new T Load<T>(string json) where T : Resource => JsonConvert.DeserializeObject<T>(json);
    }
}
