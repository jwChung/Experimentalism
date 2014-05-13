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
[assembly: AssemblyVersion("0.18.0")]
[assembly: AssemblyInformationalVersion("0.18.0")]

/*
 * Version 0.18.1
 * 
 * - [FIX-BREAKING-CHANGE] Renames some extension methods to clarify.
 *      ToMembers -> GetIdiomaticMembers
 *      
 * - [FIX-BREAKING-CHANGE] Renames `TypeMembers` to `IdiomaticMembers` to
 *   clarify.
 *   
 * - [FIX] Fixes the error for private accessors of a property in
 *   `MemberKindCollector`.
 *   
 * - [FIX-BREAKING-CHANGE] Simplifies the constructor of `IdiomaticMembers`.
 */