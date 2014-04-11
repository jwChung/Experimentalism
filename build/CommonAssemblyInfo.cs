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
[assembly: AssemblyVersion("0.9.7")]
[assembly: AssemblyInformationalVersion("0.9.7")]

/*
 * Version 0.9.7
 * 
 * Adds the MethodInfo parameter to the CreateFixture method of
 * TheoremAttribute and FirstClassTheoremAttribute to be used when creating
 * a fixture instance.
 * 
 * BREAKING CHANGES
 * - TheoremAttribute and FirstClassTheoremAttribute:
 *     protected IFixture CreateFixture() ->
 *     protected IFixture CreateFixture(MethodInfo)
 */