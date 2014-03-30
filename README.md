Experimentalism [![Build status](https://ci.appveyor.com/api/projects/status/29e9eyt7whxmoq6a)](https://ci.appveyor.com/project/jwChung/experimentalism)
===============

* [Just start with examples](#just-start-with-examples)
* [Experiment](#experiment)
    * [Parameterized test with auto data](#parameterized-test-with-auto-data)
    * [First class test with auto data](#first-class-test-with-auto-data)
    * [Base class library](#base-class-library)
    * [Inspiration](#inspiration)
* [Experiment.AutoFixture](#experimentautofixture)
    * [Source code transformations](#source-code-transformations)
    * [How to install](#how-to-install)
* [Questions](#questions)
* [Additional references](#additional-references)
* [Contributions](#contributions)
    * [New Contributor? Jump In!](#new-contributor-jump-in)

Experimentalism은 TDD에 도움을 줄 수 있는 라이브러리와 툴을 제공하는 것이 목적입니다. 

Experimentalism의 모든 프로젝트(NuGet패키지)는 [Semantic Versioning](http://semver.org/)을 따릅니다.

Experimentalism는 [AppVeyor](http://www.appveyor.com/)에서 제공하는 CI 서비스를 이용하고 있습니다. 위 Experimentalism 옆에 붙어있는 [빌드 상태 뺏지(status badge)](https://ci.appveyor.com/api/projects/status/29e9eyt7whxmoq6a)를 통해서 현재 빌드상태를 확인할 수 있습니다.

Just start with examples
------------------------
Experimentalism을 빠르게 이해는 가장  좋은 방법은, 예제를 직접 실행해 보는 것이 아닐까 합니다. 아래의 순서에 따라 예제를 실행해 볼 수 있습니다.

* Visual Studio 2010이상 버전에서 클래스 라이브러리 프로젝트(.NET Famework 4.0 이상 기반)를 하나 생성한다.
* NuGet package manager 혹은 [Package manager console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)을 통해 아래와 같은 명령으로 Experiment.AutoFixtureWithExample을 설치한다.
    ```
    PM> Install-Package Experiment.AutoFixtureWithExample
    ```
* Xunit test runner 들 중([TestDriven.net](http://testdriven.net/), [xUnit.net runner for Visual Studio](http://visualstudiogallery.msdn.microsoft.com/463c5987-f82b-46c8-a97e-b1cde42b9099?SRC=VSIDE), 또는 [Resharper test runner](http://www.jetbrains.com/resharper/features/unit_testing.html)) 하나로 Scenario.cs안에 있는 테스트들을 실행한다.

Experiment
----------
[Parameterized Test](http://xunitpatterns.com/Parameterized%20Test.html)에 대한 auto data를 제공하기 위한 [xUnit.net](http://xunit.codeplex.com/)의 확장라이브러리입니다. 여기서 auto data란 테스트에 참여하는 객체 또는 값들이 anonymous 형태로 자동 생성된 것을 말합니다. 생성된 auto data는 테스트 메소드의 파라메타를 통해 제공되게 됩니다.

### Parameterized test with auto data

이 기능이 테스트작성에 어떤 장점을 가져다주는지, 아래의 예제코드를 통해 살펴보도록 하겠습니다. 예를 들어, `Person` 클래스의 `Say` 메소드를 테스트한다면, 아마 우리는 `SayTest`와 같이 작성할 수 있을 것입니다.

```c#
public class Person
{
    private string _name;
    
    public Person(string name)
	{
        _name = name;
	}
    
    public string Name
    {
        get { return _name; }
    }
    
    public string Say(string something)
    {
        return _name + ": " + something;
    }
}

public class PersonTest
{
    [Fact]
    public void SayTest()
    {
        // Fixture setup
        var name = "Foo";
        var sut = new Person(name);
        var something = "Bar";
        var expected = name + ": " + something;
        
        // Exercise system
        var actual = sut.Say(something);
        
        // Verify outcome
        Assert.Equal(expected, actual);
    }
}
```

`SayTest`를 Experiment를 이용하여 다시 작성해 보면 아래 `SayTestUsingTheorem`와 같은 테스트를 작성할 수 있습니다. `sut`, `name`과 `something`값을 파라메타로 넘겨받음으로써, 테스트 데이터 생성의 번거로움을 덜 수 있을 뿐 아니라, 테스트가 무엇을 테스트하는지 그 의도를 좀 더 명확히 보여줄 수 있게 됩니다.

```c#
public class PersonTest
{
    [Theorem]
    public void SayTestUsingTheorem(Person sut, string something)
    {
        // Fixture setup
        var expected = sut.Name + ": " + something;
        
        // Exercise system
        var actual = sut.Say(something);
        
        // Verify outcome
        Assert.Equal(expected, actual);
    }
}
```

### First class test with auto data
우리는 종종 아래와 같이 하나의 테스트에서 여러 테스트를 동시에 실행하는 것으로 Parameterized test 효과를 얻고자 합니다.

```c#
[Fact]
public void AddTest()
{
    AddTestCase(1, 2, 3);
    AddTestCase(2, 3, 5);
    AddTestCase(10, 2, 12);
}

public void AddTestCase(int a, int b, int expected)
{
    // Fixture setup
    var sut = new Calc();
    
    // Exercise system
    var actual = sut.Add(a, b);
    
    // Verify outcome
    Assert.Equal(expected, actual);
}
```

이러한 테스트를 [xUnit Patterns](http://xunitpatterns.com/index.html)에서는 [Tabular Test](http://xunitpatterns.com/Parameterized%20Test.html#Tabular%20Test)라 하는데, 모든 테스트가 통과할 때는 문제가 없지만, 만약 하나라도 실패하게 된다면 우리는 그 실패가 3가지 중 어느 테스트에서 발생하였는지 쉽게 알아차릴 수 없게 됩니다([Eager Test](http://xunitpatterns.com/Assertion%20Roulette.html#Eager%20Test), [Defect Localization](http://xunitpatterns.com/Goals%20of%20Test%20Automation.html#Defect%20Localization)).

이 문제를 해결하기 위해서 Experiment에서는 아래와 같이 xUnit.net의 `DataAttribute`를 사용하여 각각의 테스트를 분리하였습니다(Attribute Tabular Test). 이 경우 `AddTest`는 하나의 테스트가 아니라 arguments 별로 3개의 테스트로 작동하게 됩니다.

```c#
[Theorem]
[InlineData(1, 2, 3)]
[InlineData(2, 3, 5)]
[InlineData(10, 2, 12)]
public void AddTest(int a, int b, int expected)
{
    // Fixture setup
    var sut = new Calc();
    
    // Exercise system
    var actual = sut.Add(a, b);
    
    // Verify outcome
    Assert.Equal(expected, actual);
}
```

하지만 Attribute Tabular Test는 Tabular Test에서는 없는 문제점이 있는데, 그것은 type-safe 방식이 아니라는 점이다. 그래서 Experiment에서는 Tabular Test와 Attribute Tabular Test의 장점만을 살릴 수 있는 First class test 방식을 지원합니다. First class test 방식에서는 Eager Test 문제를 해결함과 동시에 type-safe 방식을 지원하는 장점을 가집니다.

```c#
[FirstClassTheorem]
public IEnumerable<ITestCase> AddTest()
{
    var testCases = new[]
    {
        new { X = 1, Y = 2, Z = 3 },
        new { X = 3, Y = 7, Z = 10 },
        new { X = 100, Y = 23, Z = 123 }
    };

    return testCases.Select(tc => TestCase.New(
            tc,
            ptc => Assert.Equal(ptc.Z, ptc.X + ptc.Y)));
}
```

또한, First class test 방식은 argument 값을 명시하지 않으면 anonymous 값을 넘겨주는 auto data 기능도 제공합니다.

```c#
[FirstClassTheorem]
public IEnumerable<ITestCase> FirstClassTestWithAutoDataTest()
{
    yield return TestCase.New<string, object>("anonymous", (x, y) =>
    {
        Assert.Equal("anonymous", x);
        Assert.NotNull(y);
    });
}
```

### Base class library
Experiment는 직접 auto data기능을 제공하고 있지 않으며, _Parameterized/First-Class test with auto data_에 대한 base class library역할을 합니다. auto data 기능은 `Experiment.ITestFixture` 인터페이스 구현을 통해 이루어집니다.

### Inspiration
Experiment의 auto data 기능은 [xUnit Test Patterns(*by Gerard Meszaros*)](http://xunitpatterns.com/index.html)의 [Anonymous Creation Method](http://xunitpatterns.com/Creation%20Method.html#Anonymous%20Creation%20Method)와 [Parameterized Anonymous Creation Method](http://xunitpatterns.com/Creation%20Method.html#Parameterized%20Anonymous%20Creation%20Method)에서 영감을 얻었으며, [AutoFixture](https://github.com/AutoFixture/AutoFixture)의 [AutoFixture.Xunit](https://www.nuget.org/packages/AutoFixture.Xunit/)로부터 많은 영향을 받았습니다. First class test 방식은 [Bug squash의 First-class tests in MbUnit](http://bugsquash.blogspot.dk/2012/05/first-class-tests-in-mbunit.html)라는 글에서 영감을 얻었으며, [Exude](https://github.com/GreanTech/Exude)로부터 영향을 받았습니다.

Experiment.AutoFixture
----------------------
Experiment.AutoFixture auto data기능을 AutoFixture로부터 채용합니다. 따라서, Experiment.AutoFixture는 AutoFixture와 앞선 Experiment 라이브러리에 의존성을 가지게 됩니다.

### Source code transformations
Experiment.AutoFixture은 *.dll 형태로 제공되지 않고, 해당 프로젝트에 소스코드 파일이 직접 추가되는 형태로 배포되어, 사용자로 하여금 특정 목적에 맞게 소스코드를 수정할 수 있게 해주는 이점을 제공합니다(customization). _Source code transformations_에 대한 자세한 사항은 NuGet의 [도움말](http://docs.nuget.org/docs/creating-packages/configuration-file-and-source-code-transformations#Source_Code_Transformations)을 참고하세요.

### How to install
Experiment.AutoFixture은 [NuGet](https://www.nuget.org/packages/Experiment.AutoFixture/)에 등록이 되어있으므로,
NuGet package manager 혹은 [Package manager console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)을 통해 아래와 같은 명령으로 설치할 수 있습니다.

```
PM> Install-Package Experiment.AutoFixture
```

Questions
---------
Experimentalism에 대한 도움말이나 설명 문서가 많이 부족합니다. 따라서, 궁금증이나 문의가 있으시면 [Issue list](https://github.com/jwChung/Experimentalism/issues)에 질문을 등록하실 수 있습니다.

Additional references
---------------------
Experimentalism은 각 테스트 프로젝트에서 해당 프로젝트에 대한 시나리오 테스트(acceptance test와 동일개념)를 포함하고 있습니다. 이 시나리오 테스트들은 해당 프로젝트를 어떻게 이용하는지 간략히 잘 나타내고 있습니다. 아래 링크를 참고하세요.

* [Experiment scenario tests](https://github.com/jwChung/Experimentalism/blob/master/test/ExperimentUnitTest/Scenario.cs)
* [Experiment.AutoFixture scenario tests](https://github.com/jwChung/Experimentalism/blob/master/test/Experiment.AutoFixtureUnitTest/Scenario.cs)

Contributions
-------------
> Coming together is a beginning; keeping together is progress; working together is success. – Henry Ford

관심이 있으신 **누구나** Experimentalism에 참여하실 수 있습니다. 특히, TDD에 관심있거나 또는 배우고자하는 분들이 참여하시면 TDD에 관한 insight를 얻을 수 있을 것으로 생각됩니다. 참여방법은 [CONTRIBUTING 문서](https://github.com/jwChung/Experimentalism/blob/master/CONTRIBUTING.md)를 통해서 확인하실 수 있습니다.

###New Contributor? Jump In!
Experimentalism 오픈소스 참여에 익숙하지 않으신 분들은 [Issue list](https://github.com/jwChung/Experimentalism/issues?labels=jump+in)에서 **Jump in** 라벨이 붙은 이슈를 주목하시기 바랍니다. Jump in 라벨은 다음과 같은 의미이며, _Experimentalism 오픈소스 참여를 독려하기 위한 장치_입니다.

* 해당 이슈를 해결하는데 상대적으로 많은 시간을 할애하지 않아도 된다.
* 상대적으로 독립적인 이슈이기 때문에 이해하기 쉽다.
* 해당 이슈는 구현을 위한 핵심을 잘 지적하고 있다.
* 필요 시 해당 이슈에 대한 설명을 요청할 수 있다.