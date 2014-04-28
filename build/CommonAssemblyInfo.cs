﻿using System.Reflection;
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
[assembly: AssemblyVersion("0.15.4")]
[assembly: AssemblyInformationalVersion("0.15.4")]

/*
 * Version 0.15.5
 * 
 * - [FIX] Lets the idiomatic test-cases(ConstructingMemberAssertionTestCases,
 *   GuardClauseAssertionTestCases) consider only class types rather than
 *   interface types.
 *   
 * - [FIX, BREAKING-CHANGE] Deletes the unused class -
 *   `ConstantEqualityComparer`.
 */