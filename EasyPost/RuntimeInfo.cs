using System;
using System.Diagnostics;
using System.Reflection;

namespace EasyPost
{
    public static class RuntimeInfo
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
                get
                {
                    string dotNetVersion = Environment.Version.ToString();
                    if (dotNetVersion == "4.0.30319.42000")
                    {
                        /*
                         * We're on a v4.6+ version (or pre-.NET Core 3.0, which we don't support),
                         * but we can't get the exact version.
                         * See: https://docs.microsoft.com/en-us/dotnet/api/system.environment.version?view=net-6.0#remarks
                         */
                        dotNetVersion = "4.6 or higher";
                    }

                    return dotNetVersion;
                }
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
                    // Because of how this library uses parent files, we can't easily target different frameworks and use different functions to get the architecture.
                    return "Unknown";
                }
            }
        }
    }
}
