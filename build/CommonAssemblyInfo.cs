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
[assembly: AssemblyVersion("0.8.20")]
[assembly: AssemblyInformationalVersion("0.8.20")]

/*
 * Version 0.8.20
 * 
 * This version removes AutoFixture.Xunit dependency from
 * Experiment.AutoFixture. Instead, to support the way of customizing fixture
 * with attribute using AutoFixture.Xunit, this version uses reflection
 * through the `ICustomization GetCustomization(ParameterInfo)` method
 * signature.
 */