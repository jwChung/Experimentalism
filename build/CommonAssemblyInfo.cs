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
[assembly: AssemblyVersion("0.17.0")]
[assembly: AssemblyInformationalVersion("0.17.0")]

/*
 * Version 0.17.0
 * 
 * - [FIX-BEAKING-CHANGE] Conceals the SpecimenBuilder class from public API.
 * 
 * - [NEW] Provides ObjectDisposalAssertion to test that a member correctly
 *   implements IDisposable.
 *   
 * - [FIX-BEAKING-CHANGE] Renames DisplayNameVisitor to DisplayNameCollector.
 * 
 * - [NEW] Adds new extensions methods to IdiomaticExtensions for selecting
 *   members to verify idiomatic assertions.
 */