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
 * Version 0.18.0
 * 
 * - [NEW] Introduces the FixtureFactory class to centralize creating a fixture.
 * 
 * - [FIX-BREAKING-CHANGE] Renames AutoFixtureAdapter to AutoFixture.
 * 
 * - [FIX-BREAKING-CHANGE] Renames some extension methods to clarify.
 *      GetIdiomaticMembers         -> ToMembers
 *      GetIdiomaticInstanceMembers -> ToInstanceMembers
 *      GetIdiomaticStaticMembers   -> ToStaticMembers
 */