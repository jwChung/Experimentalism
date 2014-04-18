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
[assembly: AssemblyVersion("0.12.0")]
[assembly: AssemblyInformationalVersion("0.12.0")]

/*
 * Version 0.13.0
 * 
 * - [FIX] Changes constructor accessiblity of AccessibilityCollectingVisitor
 *   from private to protected.
 *
 * - [FIX] AccessibilityCollectingVisitor returns visitor itself instead of
 *   throwing NotSupprotedException, when Visit method is called with
 *   ReflectionElements which does not have any accessiblities.
 *   
 * - [NEW] Adds RestrictingReferenceAssertion to verify referenced assemblies.
 * 
 *   [Fact]
 *   public void Demo()
 *   {
 *       var assertion = new RestrictingReferenceAssertion();
 *       assertion.Visit(typeof(IEnumerable<object>).Assembly.ToElement());
 *   }  
 *   
 * - [FIX] Splits the Jwc.Experiment.Idioms namespace to Jwc.Experiment and
 *   Jwc.Experiment.Idioms. (BREAKING-CHANGE)
 */