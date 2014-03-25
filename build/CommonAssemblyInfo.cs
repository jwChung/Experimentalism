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
[assembly: AssemblyVersion("0.8.1")]
[assembly: AssemblyInformationalVersion("0.8.1")]

/*
 * Version 0.8.1
 * 
 * Rename naive to default:
 * - NaiveTheoremAttribute -> DefaultTheoremAttribute
 * - NaiveFirstClassTheoremAttribute - NaiveFirstClassTheoremAttribute
 * 
 * This renaming is breaking change but as the current major version is
 * zero(unstable), the change is acceptable according to Semantic Versioning.
 * 
 * Issue
 * - https://github.com/jwChung/Experimentalism/issues/24
 * 
 * Pull requests
 * - https://github.com/jwChung/Experimentalism/pull/25
 */