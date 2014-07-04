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
[assembly: AssemblyVersion("0.24.1")]
[assembly: AssemblyInformationalVersion("0.24.1")]

/*
 * Version 0.25.0
 * 
 * - [FIX] reordered the parameters of the constructor
 *   `TestCase(string, Delegate)`. (BREAKING-CHANGE)
 *   
 * - [FIX] removed the unnecessary guard clause as checking compiste delegate.
 *   (BREAKING-CHANGE)
 *   
 * - [FIX] removed the constructor `TestCase(Func<objec>)` to prevent
 *   the ambiguousness between Action and Func parameters. (BREAKING-CHANGE)
 *   
 * - [NEW] removed the constructor `TestCase(Action)` but instead, introduced
 *   the New method. (BREAKING-CHANGE)
 */