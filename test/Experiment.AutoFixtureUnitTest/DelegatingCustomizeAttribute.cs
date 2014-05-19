// Original source code is from https://github.com/AutoFixture/AutoFixture.

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