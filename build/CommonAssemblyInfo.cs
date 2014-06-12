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
[assembly: AssemblyVersion("0.23.7")]
[assembly: AssemblyInformationalVersion("0.23.7")]

/*
 * Version 0.23.8
 * 
 * - [FIX] fixed the type name 'TestFixtureFactory' to
 *   'DefaultFixtureFactory' in the message of the exception thrown by
 *   NotSupportedFixtureFactory.Create.
 *   
 * - [FIX] fixed that test attributes do not handle exception thrown by the
 *   Setup method of TestAssemblyConfigurationAttribute.
 */