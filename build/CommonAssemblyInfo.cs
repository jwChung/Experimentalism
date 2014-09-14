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
 *     * Experiment.Xunit
 *       - NullGuardClauseAssertion -> GuardClauseAssertion.
 * 
 * - [Major] Removed the renewed or discarded types.
 * 
 *     * Experiment.AutoFixture
 *       - AutoPropertiesAttribute
 *       - FrozenAttribute
 *       - GreedyAttribute
 *       - ModestAttribute
 *       - NoAutoPropertiesAttribute
 *
 *     * Experiment.Xunit
 *       - FirstClassTestAttribute
 *       - ITestCase, TestCase
 *       - FirstClassCommand
 *       - TestAttribute
 *       
 */