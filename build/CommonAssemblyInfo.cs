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
[assembly: AssemblyVersion("0.11.0")]
[assembly: AssemblyInformationalVersion("0.11.0")]

/*
 * Version 0.11.0
 * 
 * Supports guard clause test-cases on assembly level.
 * 
 * [FirstClassTheorem]
 * public IEnumerable<ITestCase> Demo()
 * {
 *     return new GuardClauseAssertionTestCases(typeof(Foo).Assembly);
 * }
 */