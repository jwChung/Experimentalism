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
[assembly: AssemblyVersion("0.18.2")]
[assembly: AssemblyInformationalVersion("0.18.2")]

/*
 * Version 0.19.0
 * 
 * - [NEW] Test fixture declaration using attribute:
 *   Introduces the new model to create test fixture. Using the
 *   TestFixtureDeclaration attribute, a type of ITestFixtureFactory can be
 *   declared and will be used to create a factory for test fixture.
 *   
 *      BREAKING-CHANGE: TheoremAttriute            -> ExamAttribute
 *      BREAKING-CHANGE: FirstClassTheoremAttriute  -> FirstClassExamAttribute
 *      NEW:             TestFixtureFactoryTypeAttribute
 */