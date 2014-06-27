using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ploeh.AutoFixture;

namespace Jwc.Experiment
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "The field is to test.")]
    public class ClassForCollectingReference
    {
#pragma warning disable 649
        public ClassImplementingMultiple Field;
#pragma warning restore 649

        public ClassForCollectingReference(int arg)
        {
            this.PrivateMethod1(null);
        }

        public ClassForCollectingReference(object arg)
        {
        }

        public ClassImplementingMultiple ReturnMethod()
        {
            var typeImplementingMultiple = new ClassImplementingMultiple();
            return typeImplementingMultiple;
        }

        public ClassImplementingMultiple ReturnMethod(int arg)
        {
            return null;
        }

        public void ParameterizedMethod(ClassImplementingMultiple arg)
        {
        }

        public object MethodCallInMethodBody()
        {
            return new[] { "a", "b" }.ToArray();
        }

        public void ConstructInMethodBody()
        {
            this.PrivateMethod1(new Fixture());
        }

        public void RetrunValueInMethodBody()
        {
            this.PrivateMethod2();
        }

        public void PassParameterInMethodBody()
        {
            this.PrivateMethod1(null);
        }

        private void PrivateMethod1(Fixture fixture)
        {
        }

        private Fixture PrivateMethod2()
        {
            return null;
        }
    }
}