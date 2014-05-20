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
[assembly: AssemblyVersion("0.21.0")]
[assembly: AssemblyInformationalVersion("0.21.0")]

/*
 * Version 0.21.0
 * 
 * - [FIX] Makes a test fixture creating a specimen without auto-properties as
 *   default. (BREAKING-CHANGE)
 *   
 * - [NEW] Implements AutoPropertiesAttribute to explicitly set auto-properties
 *   of a specimen.
 *   
 * - [NEW] Imports NoAutoPropertiesAttribute from AutoFixture.Xunit.
 * 
 * - [NEW] Makes the TestCase class accepting the Delegate parameter to avoid
 *   the ambiguousness between Action and Func supplied to constructor parameter
 *   of generic TestCase, and removes all the generic TestCase classes.
 *   (BREAKING-CHANGE)
 */