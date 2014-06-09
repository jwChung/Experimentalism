using System.Linq;
using Ploeh.AutoFixture;

namespace Jwc.Experiment
{
    public class ClassForCollectingReference
    {
        public ClassForCollectingReference(int arg)
        {
            PrivateMethod1(null);
        }

        public ClassForCollectingReference(object arg)
        {
        }

#pragma warning disable 649
        public ClassImplementingMultiple Field;
#pragma warning restore 649

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
            PrivateMethod1(new Fixture());
        }

        public void RetrunValueInMethodBody()
        {
            PrivateMethod2();
        }

        public void PassParameterInMethodBody()
        {
            PrivateMethod1(null);
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