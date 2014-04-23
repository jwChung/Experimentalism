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
[assembly: AssemblyVersion("0.14.0")]
[assembly: AssemblyInformationalVersion("0.14.0")]

/*
 * Version 0.14.0
 * 
 * - [FIX] Makes `ReferenceCollectingVisitor` tread-safe and improves its
 *   performance.
 *   
 * - [NEW] Introduces the new `RestrictingReferenceAssertion.Verify(Assembly)`
 *   method to be used to verify that all references of a given assembly are
 *   correctly specified, instead of using
 *   `RestrictingReferenceAssertion.Visit(AssemblyElement)`.
 *   
 *   [Fact]
 *   public void Demo()
 *   {
 *       var assertion = new RestrictingReferenceAssertion();
 *       assertion.Verify(typeof(IEnumerable<object>).Assembly);
 *   }
 *   
 * - [NEW] Introduces the assertion class `HidingReferenceAssertion` to verify
 *   that specified assemblies are not directly referenced.
 *   
 *   [Fact]
 *   public void Demo()
 *   {
 *       var assertion = new HidingReferenceAssertion(typeof(Enumerable).Assembly);
 *       assertion.Verify(typeof(IEnumerable<object>).Assembly);
 *   }
 */