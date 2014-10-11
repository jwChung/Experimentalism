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
[assembly: AssemblyVersion("2.0.3")]
[assembly: AssemblyInformationalVersion("2.0.3")]

/*
 * Version 2.0.3
 * 
 * - [Patch] Provided default constructor of TestFixture.
 * 
 * - [Patch] Supported passing auto-data to methods of first-class-test style.
 * 
 * - [Patch] Fixed auto-data requests of test methods weren't ParameterInfo(s),
 *   but rather types of the parameters.
 */