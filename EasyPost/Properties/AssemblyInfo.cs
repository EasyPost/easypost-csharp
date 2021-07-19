using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("EasyPost")]
[assembly: AssemblyDescription("EasyPost Shipping API Client Library for .NET https://easypost.com/docs")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("EasyPost")]
[assembly: AssemblyProduct("EasyPost")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Make "private" methods testable.
//[assembly: InternalsVisibleTo("EasyPostTest, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b9d909875738653bb31cbe8e83089f3b748eaaed7fbf54fd6601f73803a24005cac0cea99143a6ef6183a98cdeda360c198fb1d72c130c1de6dd7475c9cc7c01cb9fe2dae674b9531e6d431f8343496f023f356919c007f155254d1862f1734c2ba73441e252dc1dbd7a9b6e4fe048086e12d8742db955894d89685bb1bfb5c1")]
#if DEBUG
[assembly: InternalsVisibleTo("EasyPostTest")]
#else
[assembly: InternalsVisibleTo("EasyPostTest, PublicKey=0021000004800000940000000602000000240000525341310004000001000100b5839748408d94224af8d33b200b8b5cd3e5df1e02a3d67544cdfa96f6a520bdf315c18dd720a31fb57943a8410b4ea03ef38e4aedd0e5f454ee9902eafd247bc42f33af8a2e6b33734b109cd43372af6de8e94a6fc2c603ff945a91d5a592b813316015fac9dc4ba2000ad6ff027b0de0f88894498eb92a99e27ff8e0e495cc")]
#endif

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("754ac1cb-281c-4d4c-b242-5fb436ee1832")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("2.5.1.3")]
[assembly: AssemblyInformationalVersion("2.5.1.3")]
