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
[assembly: AssemblyVersion("0.8.16")]
[assembly: AssemblyInformationalVersion("0.8.16")]

/*
 * Version 0.8.16
 * 
 * Change TestCase to accept delegate for instance method rather than
 * delegate for only static method.
 * 
 * BREAKING CHANGE
 *   TestCase
 *     before:
 *     TestCase.New(...);
 *     after:
 *     new TestCase(...);
 *     
 *     before:
 *     TestCase.New("anonymous", x => { x.ToString() });
 *     after: disable specifying explicit arguments.
 *     var value = "anonymous";
 *     new TestCase(() => { value.ToString() });
 *     
 *   FirstClassCommand
 *     before:
 *     FirstClassCommand(IMethodInfo, MethodInfo, object[])
 *     after:
 *     FirstClassCommand(IMethodInfo, Delegate, object[])
 *     
 *     before:
 *     DeclaredMethod
 *     after:
 *     Method
 *     
 *     before:
 *     TestMethod
 *     after:
 *     Delegate
 */