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
[assembly: AssemblyVersion("0.23.1")]
[assembly: AssemblyInformationalVersion("0.23.1")]

/*
 * Version 0.23.1
 * 
 * - [FIX] introduced Jwc.Expereiment.AssemblyFixtureConfigurationAttribute
 *   instead of Jwc.Expereiment.Xunit.AssemblyCustomizationAttribute.
 *   (BREAKING-CHANGE)
 *   
 * - [FIX] moved DefaultFixtureFactory from Jwc.Expereiment.Xunit to
 *   Jwc.Expereiment. (BREAKING-CHANGE)
 *   
 * - [FIX] introduced DefaultFixtureFactoryConfigurationAttribute to supply the
 *   default factory of test fixture.
 */