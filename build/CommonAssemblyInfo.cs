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
[assembly: AssemblyVersion("0.16.0")]
[assembly: AssemblyInformationalVersion("0.16.0")]

/*
 * Version 0.16.1
 * 
 * - [FIX, BREAKING-CHANGE] Introduces the new `ITestFixtureFactory` insterface,
 *   which is replaced `Func<MethodInfo, ITestFixture>`.
 *   
 * - [FIX] Lets IndirectReferenceAssertion verify whether a base type of a
 *   certain type exposes any indirect reference.
 */