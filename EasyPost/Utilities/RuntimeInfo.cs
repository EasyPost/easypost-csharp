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
            internal static string DotNetVersion
            {
                get { return Environment.Version.ToString(); }
            }
        }

        internal struct OperationSystemInfo
        {
            /// <summary>
            ///     Get details about the operating system.
            /// </summary>
            /// <returns>Details about the operating system.</returns>
            private static OperatingSystem OperatingSystem
            {
                get { return Environment.OSVersion; }
            }

            /// <summary>
            ///     Get the name of the operating system.
            /// </summary>
            /// <returns>Name of the operating system.</returns>
            internal static string Name
            {
                get
                {
#if NETSTANDARD
                    switch (OperatingSystem.Platform)
                    {
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.Win32NT:
                        case PlatformID.WinCE:
                            return "Windows";
                        case PlatformID.Unix:
                            return "Linux";
                        case PlatformID.MacOSX: // in newer versions, Mac OS X is PlatformID.Unix unfortunately
                            return "Darwin";
                        default:
                            return "Unknown";
                    }
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
            internal static string Version
            {
                get { return OperatingSystem.Version.ToString(); }
            }

            /// <summary>
            ///     Get the architecture of the operating system.
            /// </summary>
            /// <returns>Architecture of the operating system.</returns>
            internal static string Architecture
            {
                get
                {
#if NET462
                    // Sorry, Windows ARM users (if you exist), best we can do is determine if we are running on a 64-bit or 32-bit
                    return Environment.Is64BitOperatingSystem ? "x64" : "x86";
#else
                    switch (RuntimeInformation.OSArchitecture)
                    {
                        case System.Runtime.InteropServices.Architecture.Arm:
                            return "arm";
                        case System.Runtime.InteropServices.Architecture.Arm64:
                            return "arm64";
                        case System.Runtime.InteropServices.Architecture.X64:
                            return "x64";
                        case System.Runtime.InteropServices.Architecture.X86:
                            return "x86";
                        default:
                            return "Unknown";
                    }
#endif
                }
            }
        }
    }
}
