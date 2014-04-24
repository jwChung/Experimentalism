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
[assembly: AssemblyVersion("0.15.0")]
[assembly: AssemblyInformationalVersion("0.15.0")]

/*
 * Version 0.15.0
 * 
 * - [FIX] Slightly improves the message of `HidingReferenceException` in
 *   `HidingReferenceAssertion`.
 * 
 * - [NEW] Introduces the new `GuardClauseAssertion` class to verify that a
 *   method or constructor has appropriate Guard Clauses in place.
 *   
 * - [FIX] Lets `ConstructingMemberAssertion` accept only relevant elements.
 * 
 * - [FIX] Deletes the `Verify(Assemlby)` method in `HidingReferenceAssertion`
 *   and `RestrictingReferenceAssertion` to keep consistency with other
 *   assertions. Instead use the `AssemblyElement.Accept(IReflectionVisitor<object>)`
 *   method as the demo below. (BREAKING-CHANGE)
 *   
 *   [Fact]
 *   public void Demo()
 *   {
 *       var assertion = new RestrictingReferenceAssertion();
 *       typeof(IEnumerable<object>).Assembly.ToElement().Accept(assertion);
 *   }
 */