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
[assembly: AssemblyVersion("0.19.1")]
[assembly: AssemblyInformationalVersion("0.19.1")]

/*
 * Version 0.20.0
 * 
 * - [FIX] Removes the direct reference 'Jwc.Experiment' from
 *   'Jwc.Experiment.Xunit'. (BREAKING-CHANGE)
 *   
 * - [NEW] Improts some customize attributes from the AutoFixture project
 *   (https://github.com/AutoFixture/AutoFixture) to customize a test fixture
 *   through test parameters, and removes the reference 'AutoFixture.Xuit' from
 *   'Jwc.Experiment.Xunit'. (BREAKING-CHANGE)
 *   
 * - [NEW] Updates AutoFixture and AutoFixture.Idioms to v3.18.7 to support null
 *   gurad clauses for generic and to use debugging symbols and its source
 *   codes.
 */