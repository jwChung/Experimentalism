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
[assembly: AssemblyVersion("1.6.0")]
[assembly: AssemblyInformationalVersion("1.6.0")]

/*
 * Version 2.0.0
 * 
 * 
 * - [Major] Removed the obsolete types and members.
 * 
 *     - ITestFixture.Create<T>(), TestFixture.Create<T>()
 *     - ITestFixture.Freeze<T>(), TestFixture.Freeze<T>()
 *     - TestFixtureConfigurationAttribute
 *     - AutoFixtureConfigurationAttribute
 *     - TestFixtureFactory.CreateFixture(MethodInfo)
 *
 * - [Major] Renamed NullGuardClauseAssertion to GuardClauseAssertion.
 */