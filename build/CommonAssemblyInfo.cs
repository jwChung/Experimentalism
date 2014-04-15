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
[assembly: AssemblyVersion("0.10.1")]
[assembly: AssemblyInformationalVersion("0.10.1")]

/*
 * Version 0.10.1
 * 
 * Renames CompositeTestCases to CompositeEnumerables and makes it generic type.
 * 
 * BREAKING-CHANGE
 * - Rename: CompositeTestCases -> CompositeEnumerables
 * - Make generic type: CompositeEnumerables<T>
 */