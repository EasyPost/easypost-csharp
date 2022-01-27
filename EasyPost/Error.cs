// <copyright file="Error.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System.Collections.Generic;

namespace EasyPost
{
    public class Error : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public string code { get; set; }

        public string field { get; set; }

        public string suggestion { get; set; }

        public string message { get; set; }

        public List<Error> errors { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Create an Error from JSON.
        /// </summary>
        /// <param name="json">JSON data to use for Error creation.</param>
        /// <typeparam name="T">The type of object to deserialize the JSON data into.</typeparam>
        /// <returns>An instance of a T type object.</returns>
        public static new T Load<T>(string json) where T : Resource
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
