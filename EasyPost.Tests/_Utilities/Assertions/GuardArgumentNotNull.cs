using System;

namespace EasyPost.Tests._Utilities.Assertions
{
    // ReSharper disable once PartialTypeWithSinglePart
    public abstract partial class Assert
    {
        private static void GuardArgumentNotNull(string argName, object argValue)
        {
            if (argValue == null)
                throw new ArgumentNullException(argName);
        }
    }
}
