using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Scotch
{
    public static class Tools
    {
        private static string GetSourceFileDirectory([CallerFilePath] string sourceFilePath = "")
        {
            if (string.IsNullOrEmpty(sourceFilePath))
            {
                throw new ArgumentNullException(nameof(sourceFilePath));
            }

            return Path.GetDirectoryName(sourceFilePath);
        }

        public static string GetFilePathInCurrentDirectory(string fileName)
        {
            return Path.Combine(GetSourceFileDirectory(), fileName);
        }
    }
}
