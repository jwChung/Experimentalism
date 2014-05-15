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
 * - [NEW] Declaration of test fixture factory using attribute
 *   Introduces the new model to create test fixture. Using the
 *   TestFixtureFactoryType attribute, a type of ITestFixtureFactory can be
 *   defined and will be used to create a factory for test fixture.
 *   
 * - [FIX] Renames the test attributes. (BREAKING CHANGE)
 *   TheoremAttriute -> ExamAttribute
 *   FirstClassTheoremAttriute -> FirstClassExamAttribute
 *
 * - [FIX] Splits the Experiment project to Experiment and Experiment.Xunit
 *   projects. (BREAKING CHANGE)
 * 
 * - [FIX] Rearrages nuget packages to reflect the splited projects.
 *   (BREAKING CHANGE)
 *   Publishes the new package 'Experiment.Xunit', and deletes the packages
 *   'Experiment.AutoFixtureWithExample' and 'Experiment'.
 */