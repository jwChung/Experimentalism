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
[assembly: AssemblyVersion("0.8.21")]
[assembly: AssemblyInformationalVersion("0.8.21")]

/*
 * Version 0.8.21
 * 
 * Refactor to simplify
 * - Makes BaseTheoremAttribute and BaseFirstClassTheoremAttribute
 *   abstract class.
 * - Removes all the parameterized constructors of BaseTheoremAttribute
 *   and BaseFirstClassTheoremAttribute, but instead, introduces
 *   the abstract method 'CreateTestFixture(MethodInfo)'.
 * 
 * BREAKING CHANGE
 * - Delete: ITestFixtureFactory
 * - Delete: TypeFixtureFactory
 * - Delete: NotSupportedFixture
 * - Delete: AutoFixtureFactory
 * - Rename: DefaultTheoremAttribute -> BaseTheoremAttribute
 * - Rename: DefaultFirstClassTheoremAttribute ->
 *           BaseFirstClassTheoremAttribute
 * - Delete: All the constructors of BaseTheoremAttribute
 *           and BaseFirstClassTheoremAttribute
 * - Delete: FixtureFactory and FixtureType properties of
 *           BaseTheoremAttribute and BaseFirstClassTheoremAttribute
 * - Change: ConvertToTestCommand(IMethodInfo, ITestFixtureFactory) ->
 *           ConvertToTestCommand(IMethodInfo, Func<MethodInfo, ITestFixture>)
 */