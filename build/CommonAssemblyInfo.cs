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
[assembly: AssemblyVersion("1.5.1")]
[assembly: AssemblyInformationalVersion("1.5.1")]

/*
 * Version 1.5.1
 * 
 * - [Patch] Fixed that FirstClassTestAttribute doesn't tear down, which is to
 *   execute a Dispose method.
 *   
 * - [Patch] Fixed that TestBaseAttribute does not tear down. This patch
 *   introduced breaking-changes but was included to this patch-version release
 *   because this bug should be addressed as soon.
 *   
 *     BREAKING-CHANGES
 *       - NEW: ITestCase2.Target
 *   
 * - [Patch] Fixed that the TestCommandContext.GetArguments method isn't
 *   implemeted accidentally.
 *   
 * - [Patch] Fixed that CompositeTestCommandFactory creates any commands when
 *   returning just an Enumerable object.
 */