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
[assembly: AssemblyVersion("0.22.0")]
[assembly: AssemblyInformationalVersion("0.22.0")]

/*
 * Version 0.22.0
 * 
 * - [NEW] Implements setting up or tearing down test fixture on assembly level
 *   using AssemblyFixtureConfigAttribute.
 *   
 * - [NEW] Introduces DefaultFixtureFactory to support static factory of test
 *   fixture.
 * 
 * - [FIX] Removes TestFixtureFactoryTypeAttribute to use
 *   AssemblyFixtureConfigAttribute and DefaultFixtureFactory instead.
 *   (BREAKING-CHANGE)
 */