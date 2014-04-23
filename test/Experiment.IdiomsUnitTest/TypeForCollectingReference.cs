using System.Linq;
using Ploeh.AutoFixture;

namespace Jwc.Experiment
{
    public class TypeForCollectingReference
    {
        public TypeForCollectingReference(int arg)
        {
            PrivateMethod1(null);
        }

        public TypeForCollectingReference(object arg)
        {
        }

#pragma warning disable 649
        public TypeImplementingMultiple Field;
#pragma warning restore 649

        public TypeImplementingMultiple ReturnMethod()
        {
            var typeImplementingMultiple = new TypeImplementingMultiple();
            return typeImplementingMultiple;
        }

        public TypeImplementingMultiple ReturnMethod(int arg)
        {
            return null;
        }

        public void ParameterizedMethod(TypeImplementingMultiple arg)
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