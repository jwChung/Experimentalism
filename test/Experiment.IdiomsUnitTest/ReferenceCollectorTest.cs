using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using Ploeh.Albedo;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.Experiment
{
    public class ReferenceCollectorTest
    {
        private const BindingFlags Bindings =
            BindingFlags.Static | BindingFlags.Instance |
            BindingFlags.Public | BindingFlags.NonPublic;

        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new ReferenceCollector();
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<Assembly>>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            var sut = new ReferenceCollector();
            var actual = sut.Value;
            Assert.Empty(actual);
        }

        [Fact]
        public void VisitAssemblyElementCollectsCorrectReferencesToAttribute()
        {
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributes = i => { Assert.False(i); return new object[] { new FactAttribute() }; }
            };
            var sut = new ReferenceCollector();
            var expected = new[] { typeof(FactAttribute).Assembly, typeof(IDisposable).Assembly };

            sut.Visit(assembly.ToElement());

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitAssemblyElementCallsBaseMethod()
        {
            var assembly = new DelegatingAssembly
            {
                OnGetTypes = () => new[] { typeof(object), typeof(int) }
            };
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            var visitor1 = new Mock<ReferenceCollector>().Object;
            var visitor2 = new Mock<ReferenceCollector>().Object;
            sut.ToMock().Setup(x => x.Visit(typeof(object).ToElement())).Returns(visitor1);
            visitor1.ToMock().Setup(x => x.Visit(typeof(int).ToElement())).Returns(visitor2);

            var actual = sut.Visit(assembly.ToElement());

            Assert.Equal(visitor2, actual);
        }

        [Fact]
        public void VisitNullAssemblyElementThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((AssemblyElement)null));
        }

        [Fact]
        public void VisitTypeElementCollectsCorrectAssemblies()
        {
            var type = typeof(List<ClassImplementingHierarchical>);
            var expected = new[]
            {
                typeof(ClassImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(IReflectionElement).Assembly,
                typeof(Fixture).Assembly
            };
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<FieldInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ConstructorInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<EventInfoElement>())).Returns(visitor);

            var actual = sut.Visit(type.ToElement());

            Assert.Equal(visitor, actual);
            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullTypeElementThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement)null));
        }

        [Fact]
        public void VisitTypeElementCollectsCorrectReferencesToAttribute()
        {
            var type = Mock.Of<Type>(x =>
                x.GetCustomAttributes(false) == new object[] { new FactAttribute() } &&
                x.Assembly == typeof(object).Assembly);
            var sut = new ReferenceCollector();
            var expected = new[] { typeof(FactAttribute).Assembly, typeof(IDisposable).Assembly };

            sut.Visit(type.ToElement());

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitFieldInfoElementCollectsCorrectAssemblies()
        {
            var sut = new ReferenceCollector();
            var expected = new[]
            {
                typeof(ClassImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly
            };
            var fieldInfoElements = new Fields<ClassForCollectingReference>()
                .Select(x => x.Field).ToElement();

            var actual = sut.Visit(fieldInfoElements);

            Assert.Equal(expected.OrderBy(x => x.ToString()), actual.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullFieldInfoElementThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement)null));
        }

        [Fact]
        public void VisitNonDeclaredFieldInfoElementsFiltersIt()
        {
            var fieldInfoElements = typeof(SubClassWithMembers).GetFields(Bindings)
                .Select(f => f.ToElement()).ToArray();
            var sut = new ReferenceCollector();

            var actual = sut.Visit(fieldInfoElements);

            Assert.Empty(actual.Value);
        }

        [Fact]
        public void VisitDeclaredFieldInfoElementsDoesNotFilterIt()
        {
            var fieldInfoElements = typeof(ClassWithMembers).GetFields(Bindings)
                .Select(f => f.ToElement()).ToArray();
            var sut = new ReferenceCollector();

            var actual = sut.Visit(fieldInfoElements);

            Assert.NotEmpty(actual.Value);
        }

        [Fact]
        public void VisitNullFieldInfoElementsThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement[])null));
        }

        [Fact]
        public void VisitFieldInfoElementsCallsBaseMethod()
        {
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<FieldInfoElement>())).Returns(visitor);
            var fieldInfoElements = typeof(ClassWithMembers).GetFields(Bindings)
                .Select(x => x.ToElement()).ToArray();

            var actual = sut.Visit(fieldInfoElements);

            Assert.Equal(visitor, actual);
        }

        [Fact]
        public void VisitFieldInfoElementCollectsCorrectReferencesToAttribute()
        {
            var fieldInfo = Mock.Of<FieldInfo>(x =>
                x.GetCustomAttributes(false) == new object[] { new FactAttribute() } &&
                x.FieldType == typeof(object));
            var sut = new ReferenceCollector();
            var expected = new[] { typeof(FactAttribute).Assembly, typeof(IDisposable).Assembly };

            sut.Visit(fieldInfo.ToElement());

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitConstructorInfoElementCollectsCorrectAssembliesForMethodBody()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var constructorInfoElement = Constructors.Select(() => new ClassForCollectingReference(0)).ToElement();

            var actual = sut.Visit(constructorInfoElement);

            Assert.Equal(visitor, actual);
            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
        }

        [Fact]
        public void VisitConstructorInfoElementCollectsCorrectReferencesToAttribute()
        {
            var constructorInfo = Mock.Of<ConstructorInfo>(x =>
                x.GetCustomAttributes(false) == new object[] { new FactAttribute() });
            var sut = new ReferenceCollector();
            var expected = new[] { typeof(FactAttribute).Assembly, typeof(IDisposable).Assembly };

            sut.Visit(constructorInfo.ToElement());

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNonDeclaredPropertyInfoElementsFiltersIt()
        {
            var propertyInfoElements = typeof(SubClassWithMembers).GetProperties(Bindings)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollector();

            var actual = sut.Visit(propertyInfoElements);

            Assert.Empty(actual.Value);
        }

        [Fact]
        public void VisitDeclaredPropertyInfoElementsDoesNotFilterIt()
        {
            var propertyInfoElements = typeof(ClassWithMembers).GetProperties(Bindings)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollector();

            var actual = sut.Visit(propertyInfoElements);

            Assert.NotEmpty(actual.Value);
        }

        [Fact]
        public void VisitNullPropertyInfoElementsThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement[])null));
        }

        [Fact]
        public void VisitPropertyInfoElementsCallsBaseMethod()
        {
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<PropertyInfoElement>())).Returns(visitor);
            var propertyInfoElements = typeof(ClassWithMembers).GetProperties(Bindings)
                .Select(x => x.ToElement()).ToArray();

            var actual = sut.Visit(propertyInfoElements);

            Assert.Equal(visitor, actual);
        }

        [Fact]
        public void VisitPropertyInfoElementCollectsCorrectReferencesToAttribute()
        {
            var propertyInfo = Mock.Of<PropertyInfo>(x =>
                x.GetCustomAttributes(false) == new object[] { new FactAttribute() } &&
                x.PropertyType == typeof(object));
            var sut = new ReferenceCollector();
            var expected = new[] { typeof(FactAttribute).Assembly, typeof(IDisposable).Assembly };

            sut.Visit(propertyInfo.ToElement());

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullPropertyInfoElementThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement)null));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssemblies()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(ClassImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly
            };
            var methodInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.ReturnMethod()).ToElement();

            var actual = sut.Visit(methodInfoElement);

            Assert.Equal(visitor, actual);
            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
        }

        [Fact]
        public void VisitNonDeclaredMethodInfoElementsFiltersIt()
        {
            var methodInfoElements = typeof(SubClassWithMembers).GetMethods(Bindings)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollector();

            var actual = sut.Visit(methodInfoElements);

            Assert.Empty(actual.Value);
        }

        [Fact]
        public void VisitDeclaredMethodInfoElementsDoesNotFilterIt()
        {
            var methodInfoElements = typeof(ClassWithMembers).GetMethods(Bindings)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollector();

            var actual = sut.Visit(methodInfoElements);

            Assert.NotEmpty(actual.Value);
        }

        [Fact]
        public void VisitNullMethodInfoElementsThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement[])null));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForMethodCallInMethodBody()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                typeof(Enumerable).Assembly
            };
            var methodInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.MethodCallInMethodBody()).ToElement();

            sut.Visit(methodInfoElement);

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForConstructInMethodBody()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var methodInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.ConstructInMethodBody()).ToElement();

            sut.Visit(methodInfoElement);

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForReturnValueInMethodBody()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var methodInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.RetrunValueInMethodBody()).ToElement();

            sut.Visit(methodInfoElement);

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectAssembliesForPassParameterInMethodBody()
        {
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            sut.ToMock().Setup(x => x.Visit(It.IsAny<ParameterInfoElement>())).Returns(visitor);
            sut.ToMock().Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement>())).Returns(visitor);
            var expected = new[]
            {
                typeof(IDisposable).Assembly,
                GetType().Assembly,
                typeof(Fixture).Assembly
            };
            var methodInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.PassParameterInMethodBody()).ToElement();

            sut.Visit(methodInfoElement);

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitMethodInfoElementsCallsBaseMethod()
        {
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<MethodInfoElement>())).Returns(visitor);
            var methodInfoElements = typeof(ClassWithMembers).GetMethods(Bindings)
                .Select(x => x.ToElement()).ToArray();

            var actual = sut.Visit(methodInfoElements);

            Assert.Equal(visitor, actual);
        }

        [Fact]
        public void VisitMethodInfoElementCollectsCorrectReferencesToAttribute()
        {
            var methodInfo = Mock.Of<MethodInfo>(x =>
                x.GetCustomAttributes(false) == new object[] { new FactAttribute() } &&
                x.ReturnType == typeof(object));
            var sut = new ReferenceCollector();
            var expected = new[] { typeof(FactAttribute).Assembly, typeof(IDisposable).Assembly };

            sut.Visit(methodInfo.ToElement());

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNonDeclaredEventInfoElementsFiltersIt()
        {
            var eventInfoElements = typeof(SubClassWithMembers).GetEvents(Bindings)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollector();

            var actual = sut.Visit(eventInfoElements);

            Assert.Empty(actual.Value);
        }

        [Fact]
        public void VisitDeclaredEventInfoElementsDoesNotFilterIt()
        {
            var eventInfoElements = typeof(ClassWithMembers).GetEvents(Bindings)
                .Select(x => x.ToElement()).ToArray();
            var sut = new ReferenceCollector();

            var actual = sut.Visit(eventInfoElements);

            Assert.NotEmpty(actual.Value);
        }

        [Fact]
        public void VisitNullEventInfoElementsThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((EventInfoElement[])null));
        }

        [Fact]
        public void VisitEventInfoElementsCallsBaseMethod()
        {
            var sut = new Mock<ReferenceCollector> { CallBase = true }.Object;
            var visitor = new DelegatingReflectionVisitor<IEnumerable<Assembly>>();
            sut.ToMock().Setup(x => x.Visit(It.IsAny<EventInfoElement>())).Returns(visitor);
            var eventInfoElements = typeof(ClassWithMembers).GetEvents(Bindings)
                .Select(x => x.ToElement()).ToArray();

            var actual = sut.Visit(eventInfoElements);

            Assert.Equal(visitor, actual);
        }

        [Fact]
        public void VisitEventInfoElementCollectsCorrectReferencesToAttribute()
        {
            var eventInfo = Mock.Of<EventInfo>(x =>
                x.GetCustomAttributes(false) == new object[] { new FactAttribute() } &&
                x.GetAddMethod(true) == Mock.Of<MethodInfo>(m => m.ReturnType == typeof(void)) &&
                x.GetRemoveMethod(true) == Mock.Of<MethodInfo>(m => m.ReturnType == typeof(void)));
            var sut = new ReferenceCollector();
            var expected = new[] { typeof(FactAttribute).Assembly, typeof(IDisposable).Assembly };

            sut.Visit(eventInfo.ToElement());

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullEventInfoElementThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((EventInfoElement)null));
        }

        [Fact]
        public void VisitParameterInfoElementCollectsCorrectAssemblies()
        {
            var sut = new ReferenceCollector();
            var expected = new[]
            {
                typeof(ClassImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly
            };
            var parameterInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.ParameterizedMethod(null)).GetParameters()
                .First().ToElement();

            var actual = sut.Visit(parameterInfoElement);

            Assert.Equal(expected.OrderBy(x => x.ToString()), actual.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullParameterInfoElementThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((ParameterInfoElement)null));
        }

        [Fact]
        public void VisitParameterInfoElementCollectsCorrectReferencesToAttribute()
        {
            var parameterInfo = Mock.Of<ParameterInfo>(x =>
                x.GetCustomAttributes(false) == new object[] { new FactAttribute() } &&
                x.ParameterType == typeof(object));
            var sut = new ReferenceCollector();
            var expected = new[] { typeof(FactAttribute).Assembly, typeof(IDisposable).Assembly };

            sut.Visit(parameterInfo.ToElement());

            Assert.Equal(expected.OrderBy(x => x.ToString()), sut.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitLocalVariableInfoElementCollectsCorrectAssemblies()
        {
            var sut = new ReferenceCollector();
            var expected = new[]
            {
                typeof(ClassImplementingMultiple).Assembly,
                typeof(IDisposable).Assembly,
                typeof(ISpecimenContext).Assembly
            };
            var localVariableInfoElement = new Methods<ClassForCollectingReference>()
                .Select(x => x.ReturnMethod()).GetMethodBody()
                .LocalVariables.First().ToElement();

            var actual = sut.Visit(localVariableInfoElement);

            Assert.Equal(expected.OrderBy(x => x.ToString()), actual.Value.OrderBy(x => x.ToString()));
        }

        [Fact]
        public void VisitNullLocalVariableInfoElementThrows()
        {
            var sut = new ReferenceCollector();
            Assert.Throws<ArgumentNullException>(() => sut.Visit((LocalVariableInfoElement)null));
        }
    }
}