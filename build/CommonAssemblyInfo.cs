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
[assembly: AssemblyVersion("0.12.0")]
[assembly: AssemblyInformationalVersion("0.12.0")]

/*
 * Version 0.12.0
 * 
 * - [NEW] ConstructingMemberAssertionTestCases
 *   This verifies members initialized correctly by a constructor.
 *   
 *   [FirstClassTheorem]
 *   public IEnumerable<ITestCase> DemoOnTypeLevel()
 *   {
 *       return new GuardClauseAssertionTestCases(typeof(Foo));
 *   }
 *   
 *   [FirstClassTheorem]
 *   public IEnumerable<ITestCase> DemoOnAssemblyLevel()
 *   {
 *       return new GuardClauseAssertionTestCases(typeof(Foo).Assembly);
 *   }
 */