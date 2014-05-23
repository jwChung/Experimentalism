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
[assembly: AssemblyVersion("0.22.1")]
[assembly: AssemblyInformationalVersion("0.22.1")]

/*
 * Version 0.22.1
 * 
 * - [FIX] Renames AssemblyFixtureConfigAttribute to
 *   AssemblyFixtureCustomizationAttribute. (BREAKING-CHANGE)
 *   
 * - [FIX] Fixes that idiomatic assertions throws an exception when verifying
 *   irrelevant static or abstract members.
 *   
 * - [FIX] Fixes that idiomatic assertions throws an exception when verifying
 *   irrelevant get or set properties.
 *   
 * - [FIX] Introdues the IdiomaticAssertion class to use it as base class of an
 *   idiomatic assertion.
 */