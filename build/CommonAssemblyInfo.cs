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
[assembly: AssemblyVersion("0.10.0")]
[assembly: AssemblyInformationalVersion("0.10.0")]

/*
 * Version 0.10.0
 * 
 * Publishes a new nuget package called 'Experiment.Idioms', which facilitates
 * running idiomatic, regular unit-tests.
 * 
 * This release includes the first idiomatic-test as
 * `GuardClauseAssertionTestCases`. This assertion verifies guard-clause
 * assertions for all public API.
 * 
 * [FirstClassTheorem]
 * public IEnumerable<ITestCase> Demo()
 * {
 *     return new GuardClauseAssertionTestCases(typeof(Foo));
 * }
 */