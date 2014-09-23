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
[assembly: AssemblyInformationalVersion("2.0.0-pre05")]

/*
 * Version 2.0.0-pre05
 * 
 * - [Major] Removed obsolete and discarded, types or members.
 *     * Jwc.Experiment.DefaultFixtureConfigurationAttribute
 *     * Jwc.Experiment.DefaultFixtureFactory
 *     * Jwc.Experiment.FuncTestFixtureFactory
 *     - Jwc.Experiment.ITestFixture.Create<T>()
 *     - Jwc.Experiment.ITestFixture.Freeze<T>()
 *     * Jwc.Experiment.ITestFixtureFactory(old)
 *     * Jwc.Experiment.NotSupportedFixtureFactory
 *     * Jwc.Experiment.TestAssemblyConfigurationAttribute
 *     * Jwc.Experiment.TestFixtureConfigurationAttribute
 *     * Jwc.Experiment.AutoFixture.AutoFixtureConfigurationAttribute
 *     * Jwc.Experiment.AutoFixture.AutoPropertiesAttribute
 *     * Jwc.Experiment.AutoFixture.CustomizeAttribute
 *     * Jwc.Experiment.AutoFixture.FrozenAttribute
 *     * Jwc.Experiment.AutoFixture.GreedyAttribute
 *     * Jwc.Experiment.AutoFixture.ModestAttribute
 *     * Jwc.Experiment.AutoFixture.NoAutoPropertiesAttribute
 *     - Jwc.Experiment.AutoFixture.TestFixture.Create<T>()
 *     - Jwc.Experiment.AutoFixture.TestFixture.Freeze<T>()
 *     * Jwc.Experiment.AutoFixture.TestFixtureConfigurationAttribute
 *     * Jwc.Experiment.Xunit.FirstClassCommand
 *     * Jwc.Experiment.Xunit.FirstClassTestAttribute
 *     * Jwc.Experiment.Xunit.ITestCase
 *     * Jwc.Experiment.Xunit.ITestFixtureFactory
 *     * Jwc.Experiment.Xunit.TargetInvocationExceptionUnwrappingCommand
 *     * Jwc.Experiment.Xunit.TestAttribute
 *     * Jwc.Experiment.Xunit.TestCase(old)
 *     
 * - [Major] Renewed some types or members.
 *     - Jwc.Experiment.AutoFixture.TestFixtureFactory.Create(MethodInfo) ->
 *       Jwc.Experiment.AutoFixture.TestFixtureFactory.Create(ITestMethodContext)
 *
 * - [Major] Renamed(or moved) some types or members.
 *     * Jwc.Experiment.AutoFixture.TestFixtureFactory
 *         : Slightly improved.
 *     * Jwc.Experiment.Idioms.NullGuardClauseAssertion -> 
 *       Jwc.Experiment.Idioms.GuardClauseAssertion
 *     * Jwc.Experiment.Xunit.ITestCase2 ->
 *       Jwc.Experiment.Xunit.ITestCase(new)
 *     * Jwc.Experiment.Xunit.ITestFixtureFactory ->
 *       Jwc.Experiment.ITestFixtureFactor(new)
 *     * Jwc.Experiment.Xunit.ITestMethodContext ->
 *       Jwc.Experiment.ITestMethodContext
 *     * Jwc.Experiment.Xunit.TestCase ->
 *       Jwc.Experiment.Xunit.TestCase(new)
 *     * Jwc.Experiment.Xunit.TestCommandContxt
 *         : Made this abstract and extracted some logic to their sub-clsses
 *           (ParameterizedCommandContext, StaticTestCaseCommandContext, TestCaseCommandContext).
 *     * Jwc.Experiment.Xunit.TestCommandCollection ->
 *       Jwc.Experiment.Xunit.TryCatchCommandCollection
 *       
 * - [Minor] Introduced new types or members(new features).
 *     * Jwc.Experiment.AutoFixture.OmitAutoPropertiesCustomization
 *     * Jwc.Experiment.AutoFixture.TestParametersCustomization
 *     * Jwc.Experiment.Xunit.TestCases
 *         : Supported creating test cases using TestCases which is simpler than
 *           using TestCase.
 *
 * - [Patch] Fixed that bindingRedirect elements aren't automatically updated.
 */