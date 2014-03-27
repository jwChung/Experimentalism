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
[assembly: AssemblyVersion("0.8.4")]
[assembly: AssemblyInformationalVersion("0.8.4")]

/*
 * Version 0.8.5
 * 
 * BREAKING CHNAGE
 *   DefaultTheoremAttribute
 *   - delete:
 *     DefaultTheoremAttribute(Func<ITestFixture>)
 *   
 *   - before:
 *     DefaultTheoremAttribute(Func<MethodInfo, ITestFixture>)
 *     after:
 *     DefaultTheoremAttribute(ITestFixtureFactory)
 *   
 *   - before:
 *     Func<MethodInfo, ITestFixture> FixtureFactory
 *     after:
 *     ITestFixtureFactory FixtureFactory
 *   
 *   - before:
 *     object FixtureType
 *     after:
 *     Type FixtureType
 *   
 *   ITestCase and TestCase
 *   - before:
 *     ITestCommand ConvertToTestCommand(IMethodInfo, Func<MethodInfo, ITestFixture>);
 *     after:
 *     ITestCommand ConvertToTestCommand(IMethodInfo, ITestFixtureFactory);
 *     
 *   DefaultFirstClassTheoremAttribute
 *   - delete:
 *     DefaultTheoremAttribute(Func<ITestFixture>)
 *   
 *   - before:
 *     DefaultTheoremAttribute(Func<MethodInfo, ITestFixture>)
 *     after:
 *     DefaultTheoremAttribute(ITestFixtureFactory)
 *   
 *   - before:
 *     Func<MethodInfo, ITestFixture> FixtureFactory
 *     after:
 *     ITestFixtureFactory FixtureFactory
 */