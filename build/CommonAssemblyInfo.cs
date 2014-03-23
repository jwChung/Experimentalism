using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("Jin-Wook Chung")]
[assembly: AssemblyCopyright("Copyright (c) 2014, Jin-Wook Chung")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyVersion("0.6.1")]
[assembly: AssemblyInformationalVersion("0.6.1")]

/*
 * Version 0.6.1
 * 
 * - TestFixtureAdapter의 디폴트 생성자는 
 *   "Anything more than field assignment in constructors"에 해당함으로 이를
 *   해결(삭제).
 *   
 * - TestFixtureAdapter의 디폴트 생성자 삭제는 breaking change에 해당하나,
 *   unstable major version(zero)이기 때문에 용인됨.
 */