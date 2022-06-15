using System;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using Newtonsoft.Json;

namespace EasyPost.Interfaces
{
    public class ApiCompatibleFunctions
    {
        [JsonIgnore] internal Client? Client = null;

        internal void CheckFunctionalityCompatible(string methodName, Type[]? methodArgTypes = null)
        {
            ApiCompatibilityUtilities.CheckFunctionalityCompatible(methodName, GetType(), Client, methodArgTypes);
        }
    }
}
