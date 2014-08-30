// Original source code: https://github.com/AutoFixture/AutoFixture
// Copyright           : Copyright (c) 2013 Mark Seemann  
// License             : The MIT License
namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Reflection;
    using Ploeh.AutoFixture;

    public class DelegatingCustomizeAttribute : CustomizeAttribute
    {
        public Func<ParameterInfo, ICustomization> OnGetCustomization { get; set; }

        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return this.OnGetCustomization(parameter);
        }
    }
}