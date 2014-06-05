// Original source code: https://github.com/AutoFixture/AutoFixture
// Copyright           : Copyright (c) 2013 Mark Seemann  
// License             : The MIT License

using System;
using System.Reflection;
using Ploeh.AutoFixture;

namespace Jwc.Experiment.AutoFixture
{
    public class DelegatingCustomizeAttribute : CustomizeAttribute
    {
        public Func<ParameterInfo, ICustomization> OnGetCustomization { get; set; }

        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            return OnGetCustomization(parameter);
        }
    }
}