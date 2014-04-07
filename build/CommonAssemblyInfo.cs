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
[assembly: AssemblyVersion("0.9.0")]
[assembly: AssemblyInformationalVersion("0.9.0")]

/*
 * Version 0.9.0
 * 
 * Implemented FirstClassTheoremAttribute which supports providing auto data
 * using the AutoFixture library.
 * 
 * Addressed unhandled exception thrown when creating TestCommand instances
 * in BaseTheoremAttribute.EnumerateTestCommands(IMethodInfo).
 * 
 * Issue: https://github.com/jwChung/Experimentalism/issues/23
 * 
 * Pull request: https://github.com/jwChung/Experimentalism/pull/32
 */