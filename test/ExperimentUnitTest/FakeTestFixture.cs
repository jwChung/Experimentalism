﻿using System;

namespace Jwc.Experiment
{
    public class FakeTestFixture : ITestFixture
    {
        private readonly string _stringValue = Guid.NewGuid().ToString();
        private readonly int _intValue = new Random().Next();

        public string StringValue
        {
            get
            {
                return _stringValue;
            }
        }

        public int IntValue
        {
            get
            {
                return _intValue;
            }
        }
        
        public object Create(object request)
        {
            var type = request as Type;
            if (type != null)
            {
                if (type == typeof(string))
                {
                    return _stringValue;
                }
                if (type == typeof(int))
                {
                    return _intValue;
                }
            }

            throw new NotSupportedException();
        }
    }
}