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
[assembly: AssemblyVersion("0.23.2")]
[assembly: AssemblyInformationalVersion("0.23.2")]

/*
 * Version 0.23.3
 * 
 * - [FIX] moved out of the namespace 'Experiment.Idioms.Assertions'.
 *   (BREAKING-CHANGE)
 *   
 * - [FIX] fixed an exception thrown when verifying member initialization for
 *   the field 'value__' of enum types.
 */