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
[assembly: AssemblyInformationalVersion("2.0.0-pre01")]

/*
 * Version 2.0.0-pre01
 * 
 * - [Major] Removed the obsolete types and members.
 * 
 *     * Experiment.Xunit
 *       - ITestFixture.Create<T>(), TestFixture.Create<T>()
 *       - ITestFixture.Freeze<T>(), TestFixture.Freeze<T>()
 *       - TestFixtureConfigurationAttribute
 *       - AutoFixtureConfigurationAttribute
 *       - TestFixtureFactory.CreateFixture(MethodInfo)
 *       - ITestCommandContext.ActualMethod
 *       - TestCase2(MethodInfo, object[])
 *       - TestCommandContext.TestObject
 *
 * - [Major] Renamed.
 * 
 *     * Experiment.Idioms
 *       - NullGuardClauseAssertion -> GuardClauseAssertion.
 *       
 *     * Experiment.Xunit
 *       - ITestCase2 -> ITestCase
 *       - TestCase2 -> TestCase
 * 
 * - [Major] Removed the renewed or discarded types.
 * 
 *     * Experiment
 *       - DefaultFixtureConfigurationAttribute
 *       - TestAssemblyConfigurationAttribute
 *       - DefaultFixtureFactory
 *       - NotSupportedFixtureFactory
 *       - FuncTestFixtureFactory
 *       - ITestFixtureFactory
 *
 *     * Experiment.AutoFixture
 *       - AutoPropertiesAttribute
 *       - FrozenAttribute
 *       - GreedyAttribute
 *       - ModestAttribute
 *       - NoAutoPropertiesAttribute
 *       - CustomizeAttribute
 *       - TestFixtureConfigurationAttribute
 *       - TestFixtureFactory
 *
 *     * Experiment.Xunit
 *       - FirstClassTestAttribute
 *       - ITestCase, TestCase
 *       - FirstClassCommand
 *       - TestAttribute
 *       
 */