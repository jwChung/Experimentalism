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
[assembly: AssemblyVersion("0.9.5")]
[assembly: AssemblyInformationalVersion("0.9.5")]

/*
 * Version 0.9.5
 * 
 * Changes the accessibility of CreateTestFixture from public to protected.
 * 
 * BREAKING CHANGE
 * - the member of BaseTheoremAttribute,
 *                 BaseFirstClassTheoremAttribute,
 *                 AutoFixtureTheoremAttribute,
 *                 AutoFixtureFirstClassTheoremAttribute
 *   
 *   public ITestFixture CreateTestFixture(MethodInfo) ->
 *   protected ITestFixture CreateTestFixture(MethodInfo)
 */