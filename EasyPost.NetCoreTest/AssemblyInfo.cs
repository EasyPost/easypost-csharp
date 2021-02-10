using System.Runtime.CompilerServices;
using System.Reflection;

// Make "private" methods testable.
#if !DEBUG
[assembly: AssemblyKeyFileAttribute("../EasyPost.snk")]
#endif