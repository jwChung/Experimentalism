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
[assembly: AssemblyVersion("0.8.14")]
[assembly: AssemblyInformationalVersion("0.8.14")]

/*
 * Version 0.8.14
 * 
 * Change the base class of the FirstClassCommand class to the TestCommand
 * class. 
 * 
 * BREAKING CHANGE
 *   before:
 *   public class FirstClassCommand : FactCommand { ... }
 *   after:
 *   public class FirstClassCommand : TestCommand { ... }
 */