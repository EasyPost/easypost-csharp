using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace EasyPost.Utilities
{
    internal static class RuntimeInfo
    {
        internal struct ApplicationInfo
        {
            /// <summary>
            ///     Get the version of the application as a string.
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
                    catch (Exception)
                    {
                        return "Unknown";
                    }
                }
            }

            /// <summary>
            ///     Get the .NET framework version as a string.
            /// </summary>
            /// <returns>The .NET framework version as a string.</returns>
            internal static string DotNetVersion => Environment.Version.ToString();
        }

        internal struct OperationSystemInfo
        {
            /// <summary>
            ///     Get details about the operating system.
            /// </summary>
            /// <returns>Details about the operating system.</returns>
            private static OperatingSystem OperatingSystem => Environment.OSVersion;

            /// <summary>
            ///     Get the name of the operating system.
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
                        PlatformID.MacOSX => // in newer versions, Mac OS X is PlatformID.Unix unfortunately
                            "Darwin",
                        PlatformID.Xbox => "Unknown",
                        var _ => "Unknown"
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

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        return "Windows";
                    }

                    return "Unknown";
#endif
                }
            }

            /// <summary>
            ///     Get the version of the operating system.
            /// </summary>
            /// <returns>Version of the operating system.</returns>
            internal static string Version => OperatingSystem.Version.ToString();

            /// <summary>
            ///     Get the architecture of the operating system.
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
                    var _ => "Unknown"
                };
#endif

        }
    }
}
