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
[assembly: AssemblyVersion("0.16.0")]
[assembly: AssemblyInformationalVersion("0.16.0")]

/*
 * Version 0.16.0
 * 
 * - [NEW, BREAKING-CHANGE] Replaces all the old idiomatic assertions with new
 *   assertions to provide simpler and more useful API.
 *   
 *      GuardClauseAssertion          -> NullGuardClauseAssertion
 *      ConstructingMemberAssertion   -> MemberInitializationAssertion
 *      RestrictingReferenceAssertion -> RestrictiveReferenceAssertion
 *      HidingReferenceAssertion      -> IndirectReferenceAssertion
 *      
 * - [FIX, BREAKING-CHANGE] Rearrange namespace:
 *   Introduces the new namespace 'Jwc.Experiment.Idioms.Assertions' and
 *   rearrages namespaces.
 */