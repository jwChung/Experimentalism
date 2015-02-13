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
[assembly: AssemblyVersion("3.0.0")]
[assembly: AssemblyInformationalVersion("3.0.0")]

/*
 * Version 3.0.0
 * 
 * - [Major] Removed the Experiment.AutoFixture project. This project is not
 *   used in the next new major release as the new version depends on the
 *   AutoFixture abstractions. (IFixture, ISpecimenBuilder)
 *   
 * - [Major] Removed the old abstractions for test fixture, which is replaced
 *   with the AutoFixture abstractions.
 *   
 * - [Major] Renamed IndirectReferenceAssertion to NotExposedReferenceAssertion.
 *
 * - [Minor] Introduced the ParameterCustomization class to support the
 *   parameter customization attributes of AutoFixture.Xunit.
 */