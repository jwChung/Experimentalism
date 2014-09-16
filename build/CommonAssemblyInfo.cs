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
[assembly: AssemblyVersion("2.0.0")]
[assembly: AssemblyInformationalVersion("2.0.0-pre02")]

/*
 * Version 2.0.0-pre02
 * 
 * - [Major] Removed the obsolete types and members.
 * 
 *     * Experiment.Xunit
 *       - ITestFixture.Create<T>()
 *         TestFixture.Create<T>()
 *       - ITestFixture.Freeze<T>()
 *         TestFixture.Freeze<T>()
 *       - TestFixtureConfigurationAttribute
 *       - AutoFixtureConfigurationAttribute
 *       - TestFixtureFactory.CreateFixture(MethodInfo)
 *       - ITestCommandContext.ActualMethod
 *       - TestCase2(MethodInfo, object[])
 *       - TestCommandContext.TestObject
 *
 * - [Major] Removed discarded types.
 * 
 *     * Experiment
 *       - DefaultFixtureConfigurationAttribute
 *       - TestAssemblyConfigurationAttribute
 *       - DefaultFixtureFactory
 *       - NotSupportedFixtureFactory
 *       - FuncTestFixtureFactory
 *
 *     * Experiment.AutoFixture
 *       - AutoPropertiesAttribute
 *       - FrozenAttribute
 *       - GreedyAttribute
 *       - ModestAttribute
 *       - NoAutoPropertiesAttribute
 *       - CustomizeAttribute
 *       - TestFixtureConfigurationAttribute
 *
 *     * Experiment.Xunit
 *       - FirstClassTestAttribute
 *       - FirstClassCommand
 *       - TestAttribute
 *       
 * - [Major] Renamed some types.
 * 
 *     * Experiment.Idioms
 *       - NullGuardClauseAssertion -> GuardClauseAssertion.
 *       
 *     * Experiment.Xunit
 *       - ITestCase2 -> ITestCase
 *         TestCase2  -> TestCase
 *       - ITestFixtureFactory2 -> ITestFixtureFactory
 *         TestFixtureFactory2  -> TestFixtureFactory
 *         
 * - [Major] Renewed some types.
 * 
 *     * Experiment.Xunit
 *       - ITestCommandContext: Added the new GetStaticMethodContext method.
 *       - TestCommandContxt: Made this abstract and extracted some logic to
 *         their sub-clsses(ParameterizedCommandContext,
 *         StaticTestCaseCommandContext, TestCaseCommandContext).
 * 
 * - [Minor] Supported creating test cases using TestCases which is simpler
 *   than using TestCase.
 */