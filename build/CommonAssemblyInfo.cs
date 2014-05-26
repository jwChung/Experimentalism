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
[assembly: AssemblyVersion("0.22.3")]
[assembly: AssemblyInformationalVersion("0.22.3")]

/*
 * Version 0.22.4
 * 
 * - [FIX] Made the construtor of FirstClassCommand accept a testParameterName
 *   argument. (BREAKING-CHANGE)
 *   
 *   Before:
 *       public FirstClassCommand(
 *           IMethodInfo method,
 *           Delegate @delegate,
 *           object[] arguments)
 *   After:        
 *       public FirstClassCommand(
 *           IMethodInfo method,
 *           string testParameterName,
 *           Delegate @delegate,
 *           object[] arguments)
 */