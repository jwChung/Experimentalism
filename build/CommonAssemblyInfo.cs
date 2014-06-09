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
[assembly: AssemblyVersion("0.23.3")]
[assembly: AssemblyInformationalVersion("0.23.3")]

/*
 * Version 0.23.4
 * 
 * - [FIX] modified the unnecessary protected method 'VisitMethodBody' to
 *   private. (BREAKING-CHANGE)
 *   
 * - [FIX] fixed that ReferenceCollector does not collect references to custom
 *   attributes.
 *   
 * - [FIX] fixed that RestrictiveReferenceAssertion does not throw when unused
 *   reference is specified.
 *   
 * - [FIX] fixed that IndirectReferenceAssertion does not ignore verifying
 *   unexposed(private/internal) members.
 */