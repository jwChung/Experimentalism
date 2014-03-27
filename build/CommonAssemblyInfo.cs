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
[assembly: AssemblyVersion("0.8.3")]
[assembly: AssemblyInformationalVersion("0.8.3")]

/*
 * Version 0.8.3
 * 
 * Initializes a test-fixture only when necessary rather than every time.
 * 
 * Fixes that fixture factory passed from ConvertToTestCommand should take
 * MethodInfo of a test delegate rather than it of a test method, which is
 * adorned with FisrtClassTheoremAttribute.
 * 
 * BREAKING CHANGE
 * - ITestCase, TestCase
 *   ITestCommand ConvertToTestCommand(IMethodInfo, ITestFixture)
 *   -> ITestCommand ConvertToTestCommand(IMethodInfo, Func<MethodInfo, ITestFixture>)
 * 
 * Issue
 * - https://github.com/jwChung/Experimentalism/issues/28
 * 
 * Pull request
 * - https://github.com/jwChung/Experimentalism/pull/29
 */