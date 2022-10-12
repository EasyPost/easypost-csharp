using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("EasyPost")]
[assembly: AssemblyDescription("EasyPost .NET/.NET Core Shipping API Client Library for .NET https://easypost.com/docs")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("EasyPost")]
[assembly: AssemblyProduct("EasyPost")]
[assembly: AssemblyCopyright("Copyright © 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Make "internal" methods testable.
#if DEBUG
[assembly: InternalsVisibleTo("EasyPost.Tests")]
#endif

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("754ac1cb-281c-4d4c-b242-5fb436ee1832")]
