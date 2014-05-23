﻿using System;

namespace Jwc.Experiment.Idioms
{
    public class DelegatingTestFixture : ITestFixture
    {
        public Func<object, object> OnCreate { get; set; }

        public object Create(object request)
        {
            return OnCreate(request);
        }
    }
}