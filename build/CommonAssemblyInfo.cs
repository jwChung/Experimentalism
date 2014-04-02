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
[assembly: AssemblyVersion("0.8.13")]
[assembly: AssemblyInformationalVersion("0.8.13")]

/*
 * Version 0.8.13
 * 
 * While publishing v0.8.12 to NuGet server, exception was thrown,
 * so v0.8.13 is published to ignore the exception.
 * 
 * Rename some properties of FirstClassCommand to clarify
 * 
 * BREAKING CHANGE
 *   FirstClassCommand class
 *     before:
 *     Method
 *     after:
 *     DeclaredMethod
 *   
 *     before:
 *     TestCase
 *     after:
 *     TestMethod
 */