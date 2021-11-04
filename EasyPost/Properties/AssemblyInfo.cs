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
[assembly: AssemblyCopyright("Copyright © 2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Make "private" methods testable.
#if DEBUG
[assembly: InternalsVisibleTo("EasyPostTest")]
#else
[assembly: InternalsVisibleTo("EasyPostTest, PublicKey=0024000004800000140200000602000000240000525341310010000001000100e9eec615ced88b424c633434317f6afb7728f05dcc0bf40ce65a5b9b3e8e79a43081c4fe9e9a1eec595713637855ad7867d0c3b2aa9fe0756a0ecda9c9428d92fb862d3dd08400fe6ba9d2c54f2eb0aa59beaf581c32564b31288f521780e8f42747434a09f8748e72043eb41dd8ef23454eb1663ba85d24320ae51547a3a31fae62d3df7e6e5d334360acfbc7f296ca0183624b0e938b3c26d6fb38c5c61c3e1c8e907c80a5ee504d8f8999b678a4f67ed1856a865296e52e6849c8ffa86d9080b33a42be88973213b8f837bfcaadf2a09001ad1582f5a3824f594174bc3da410df3e98954320eaba69e81e5357cac146b9678c02ab0c2cc3362593e29845609fff6dbd32624fbe81b74a57dcbd8ca332e3ce0f3de75fe654bf78f33e2b79be6433448bccb202c4cd3f3a24bf35197fbcc64541fa7d1dfcb626f133671ef379c9a4da794d7e37393f30efa8a836bbccb50aa3034bcca18e436e2538fda49e0ab4eaa058a3e11bcb7a359ba89a6dfc0313a7fa04443ec2286f2bd3b57fa0e4616cd27ad116249fd0ccb0182482737b7879b3f5961029ac911f9c9c490dbaa6093546f0653d2bfe08adc029846d260e475fbad7c70aefb6fea5f84c93eb55cb9c285e75a74541447f6c53f79d969d41620da952526d2c6fa787fc19d13ae606c7d4638bba6d64874f000c25dbd600925851bb23838d5b82bfa59d7ce5d6068bb8")]
#endif

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("754ac1cb-281c-4d4c-b242-5fb436ee1832")]

