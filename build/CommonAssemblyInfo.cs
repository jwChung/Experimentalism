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
[assembly: AssemblyVersion("1.4.0")]
[assembly: AssemblyInformationalVersion("1.4.0")]

/*
 * Version 1.4.0
 * 
 * - [Minor] Deprecated the Create<T>() and Freeze<T>(T) methods. Instead of it,
 *   use members of IFixture to custmize test fixture.
 *   
 * - [Minor] Deprecated the CreateFixture method. Instead, introuduced the
 *   GetCustomization method to customize test fixture.
 */