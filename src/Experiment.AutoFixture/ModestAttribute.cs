﻿// Original source code: https://github.com/AutoFixture/AutoFixture
// Copyright           : Copyright (c) 2013 Mark Seemann  
// License             : The MIT License

using System;
using System.Reflection;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment.AutoFixture
{
    /// <summary>
    /// An attribute that can be applied to parameters to indicate that the parameter value should
    /// be created using the most modest constructor that can be satisfied by an
    /// <see cref="IFixture" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class ModestAttribute : CustomizeAttribute
    {
        /// <summary>
        /// Gets a customization that associates a <see cref="ModestConstructorQuery" /> with the
        /// <see cref="Type" /> of the parameter.
        /// </summary>
        /// <param name="parameter">
        /// The parameter for which the customization is requested.
        /// </param>
        /// <returns>
        /// A customization that associates a <see cref="ModestConstructorQuery" /> with the
        /// <see cref="Type" /> of the parameter.
        /// </returns>
        public override ICustomization GetCustomization(ParameterInfo parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException("parameter");
            }

            return new ConstructorCustomization(parameter.ParameterType, new ModestConstructorQuery());
        }
    }
}