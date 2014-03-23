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
[assembly: AssemblyVersion("0.5.1")]
[assembly: AssemblyInformationalVersion("0.5.1")]

/*
 * Version 0.5.1
 * 
 * - FixtureType property를 노출함으로써, NaiveTheoremAttribute의 fixtureType
 *   argument 노출에 대한 code analysis warning(CA1019)을 해결하였음.
 */