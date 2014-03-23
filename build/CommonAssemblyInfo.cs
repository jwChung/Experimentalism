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
[assembly: AssemblyVersion("0.6.0")]
[assembly: AssemblyInformationalVersion("0.6.0")]

/*
 * Version 0.6.0
 * 
 * - 생성자 NaiveTheoremAttribute(Func<MethodInfo, ITestFixture>)를
 *   오버로드함. ITestFixture 생성 시 MethodInfo(메타데이터)이용이 가능함.
 */