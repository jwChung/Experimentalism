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
[assembly: AssemblyVersion("0.8.6")]
[assembly: AssemblyInformationalVersion("0.8.6")]

/*
 * Version 0.8.6
 * 
 * Change the FirstClassCommand constructor to use MethodInfo directly
 * instead of Delegate.
 * 
 * BREAKING CHNAGE
 *   FirstClassCommand
 *   - before:
 *     FirstClassCommand(IMethodInfo, Delegate, object[])
 *     after:
 *     FirstClassCommand(IMethodInfo, MethodInfo, object[])
 *   
 *   - before:
 *     Delegate Delegate
 *     after:
 *     MethodInfo TestCase
 */