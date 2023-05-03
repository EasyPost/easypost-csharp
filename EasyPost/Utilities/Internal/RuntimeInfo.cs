using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace EasyPost.Utilities.Internal
{
    /// <summary>
    ///     Methods to retrieve runtime information.
    /// </summary>
    internal static class RuntimeInfo
    {
        /// <summary>
        ///     Methods to retrieve application information.
        /// </summary>
        internal struct ApplicationInfo
        {
            /// <summary>
            ///     Gets the version of the application as a string.
            /// </summary>
            /// <returns>The version of the application as a string.</returns>
            internal static string ApplicationVersion
            {
                get
                {
                    try
                    {
                        Assembly assembly = typeof(ApplicationInfo).Assembly;
                        FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
                        return info.FileVersion ?? "Unknown";
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                    catch (Exception)
                    {
                        return "Unknown";
                    }
#pragma warning disable CA1031 // Do not catch general exception types
                }
            }

            /// <summary>
            ///     Gets the .NET framework version as a string.
            /// </summary>
            /// <returns>The .NET framework version as a string.</returns>
            internal static string DotNetVersion => Environment.Version.ToString();
        }

        /// <summary>
        ///     Methods to retrieve operating system information.
        /// </summary>
        internal struct OperationSystemInfo
        {
            /// <summary>
            ///     Gets details about the operating system.
            /// </summary>
            /// <returns>Details about the operating system.</returns>
            private static OperatingSystem OperatingSystem => Environment.OSVersion;

            /// <summary>
            ///     Gets the name of the operating system.
            /// </summary>
            /// <returns>Name of the operating system.</returns>
#pragma warning disable IDE0025
            internal static string Name
#pragma warning restore IDE0025
            {
                get
                {
#if NETSTANDARD2_0
                    return OperatingSystem.Platform switch
                    {
                        PlatformID.Win32S => "Windows",
                        PlatformID.Win32Windows => "Windows",
                        PlatformID.Win32NT => "Windows",
                        PlatformID.WinCE => "Windows",
                        PlatformID.Unix => "Linux",
                        PlatformID.MacOSX => "Darwin",  // in newer versions, Mac OS X is PlatformID.Unix unfortunately
                        PlatformID.Xbox => "Unknown",  // how are you doing this? Tell me!
                        var _ => "Unknown",
                    };
#else
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        return "Linux";
                    }

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        return "Darwin";
                    }

                    // ReSharper disable once ConvertIfStatementToReturnStatement
#pragma warning disable IDE0046
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
#pragma warning restore IDE0046
                    {
                        return "Windows";
                    }

                    return "Unknown";
#endif
                }
            }

            /// <summary>
            ///     Gets the version of the operating system.
            /// </summary>
            /// <returns>Version of the operating system.</returns>
            internal static string Version => OperatingSystem.Version.ToString();

            /// <summary>
            ///     Gets the architecture of the operating system.
            /// </summary>
            /// <returns>Architecture of the operating system.</returns>
            internal static string Architecture =>
#if NET462
                    // Sorry, Windows ARM users (if you exist), best we can do is determine if we are running on a 64-bit or 32-bit
                    return Environment.Is64BitOperatingSystem ? "x64" : "x86";
#else
#pragma warning disable IDE0072 // Disable to avoid unnecessary enums on specific .NET versions
                RuntimeInformation.OSArchitecture switch
#pragma warning restore IDE0072
                {
                    System.Runtime.InteropServices.Architecture.Arm => "arm",
                    System.Runtime.InteropServices.Architecture.Arm64 => "arm64",
                    System.Runtime.InteropServices.Architecture.X64 => "x64",
                    System.Runtime.InteropServices.Architecture.X86 => "x86",
                    var _ => "Unknown",
                };
#endif

        }
    }
}
