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
[assembly: AssemblyVersion("0.8.5")]
[assembly: AssemblyInformationalVersion("0.8.5")]

/*
 * Version 0.8.5
 * 
 * Introduce ITestFixtureFactory instead of Func<ITestFixture>.
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
 *  
 * Issue
 * - https://github.com/jwChung/Experimentalism/issues/30
 * 
 * Pull request
 * - https://github.com/jwChung/Experimentalism/pull/31
 */