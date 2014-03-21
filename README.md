Experimentalism
===============

* [Experiment](#Experiment)
    * [Parameterized test with auto data](#parameterized-test-with-auto-data)
    * [Base class library](#base-class-library)
    * [Inspiration](#inspiration)
* [Experiment.AutoFixture](#experimentautofixture)
    * [Source code transform](#source-code-transform)
    * [How to install](#how-to-install)
* [Questions](#questions)
* [Additional references](#additional-references)
* [Contributions](#contributions)

Experimentalism은 TDD를 용이하게 하기 위한 라이브러리와 툴을 제공하는 것이 목적입니다.

Experimentalism의 모든 프로젝트(Nuget패키지)는 [Semantic Versioning](http://semver.org/)을 따릅니다.

Experiment
----------
[Parameterized Test]에 대한 auto data를 제공하기 위한 [xUnit.net]의 확장라이브러리입니다. 여기서 auto data란, 테스트에 필요한 값 또는 객체를 anonymous(또는 non-deterministric) 값으로 자동생성하는 것을 말합니다. 생성된 auto data는 테스트 메소드의 파라메타를 통해 제공되게 됩니다.

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
        var name = "Foo";
        var sut = new Person(name);
        var something = "Bar";
        
        var actual = sut.Say(something);
        
        Assert.Equal(name + ": " + something, actual);
    }
}
```

`SayTest`를 Experiment를 이용하여 다시 작성해 보면 아래 `SayTest2`와 같은 테스트를 작성할 수 있습니다. `sut`, `name`과 `something`값을 파라메타로 넘겨받음으로써, 테스트에 필요한 데이터 생성의 번거로움을 덜 수 있을 뿐 아니라, 테스트가 무엇을 테스트하는지 그 의도를 좀 더 명확히 보여줄 수 있게 됩니다.

```c#
public class PersonTest2
{
    [Theorem]
    public void SayTest2(Person sut, string something)
    {
        var actual = sut.Say(something);
        
        Assert.Equal(sut.Name + ": " + something, actual);
    }
}
```


### Base class library
Experiment는 직접 auto data기능을 제공하고 있지 않으며, _Parameterized test with auto data_에 대한 base class library역할을 합니다. auto data 기능은 `Experiment는.ITestFixture` 인터페이스 구현을 통해 이루어집니다.

### Inspiration
Experiment는 [xUnit Test Patterns](by *Gerard Meszaros*)의 [Anonymous Creation Method]와 [Parameterized Anonymous Creation Method]에서 영감을 얻었으며, [AutoFixture]의 [AutoFixture.Xunit](https://www.nuget.org/packages/AutoFixture.Xunit/) 라이브러리로부터 많은 영향을 받았습니다.

Experiment.AutoFixture
----------------------
Experiment.AutoFixture auto data기능을 [AutoFixture]로부터 채용합니다. 따라서, Experiment.AutoFixture는 AutoFixture와 앞선  Experiment 라이브러리에 의존성을 가지게 됩니다.

### Source code transform
Experiment.AutoFixture은 *.dll 형태로 제공되지 않고, 해당 프로젝트에 소스코드 파일이 직접 추가되는 형태로 배포되어, 사용자로 하여금 특정 목적에 맞게 소스코드를 수정할 수 있게 해주는 이점을 제공합니다(customization). _Source code transform_에 대한 자세한 사항은 NuGet의 [도움말](http://docs.nuget.org/docs/creating-packages/configuration-file-and-source-code-transformations#Source_Code_Transformations)을 참고하세요.

### How to install
Experiment.AutoFixture은 [NuGet](https://www.nuget.org/packages/Experiment.AutoFixture/)에 등록이 되어있으므로,
NuGet package manager 혹은 [Package manager console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)을 통해 아래와 같은 명령으로 설치할 수 있습니다.

```
PM> Install-Package Experiment
```

Questions
---------
Experimentalism에 대한 도움말이나 설명 문서가 많이 부족합니다. 따라서, 궁금증이나 문의가 있으시면 **누구나**, **언제든지**  [GitHub issues](https://github.com/jwChung/Experimentalism/issues)를 통해 질문을 등록할 수 있습니다.

Additional references
---------------------
Experimentalism은 각 테스트 프로젝트에서 해당 프로젝트에 대한 시나리오 테스트(acceptance test와 동일개념)를 포함하고 있습니다. 이 시나리오 테스트들은 해당 프로젝트를 어떻게 이용하는지 간략히 잘 나타내고 있습니다. 아래 링크를 참고하세요.

* [Experiment scenario tests](https://github.com/jwChung/Experimentalism/blob/master/test/ExperimentUnitTest/Scenario.cs)
* [Experiment.AutoFixture scenario tests](https://github.com/jwChung/Experimentalism/blob/master/test/Experiment.AutoFixtureUnitTest/Scenario.cs)

Contributions
-------------
> Coming together is a beginning; keeping together is progress; working together is success. – Henry Ford

관심이 있으신 **누구나** Experimentalism에 참여하실 수 있습니다. 특별히 TDD에 관심있거나 또는 배우고자하는 분들이 참여하시면 많은 TDD에 관한 많은 insight를 얻을 수 있을 것으로 생각됩니다.

[xUnit.net]: <http://xunit.codeplex.com/>
[Parameterized test]: <http://xunitpatterns.com/Parameterized%20Test.html>
[AutoFixture]: <https://github.com/AutoFixture/AutoFixture>
[xUnit Test Patterns]: <http://xunitpatterns.com/index.html>
[Anonymous Creation Method]: <http://xunitpatterns.com/Creation%20Method.html#Anonymous%20Creation%20Method>
[Parameterized Anonymous Creation Method]: <http://xunitpatterns.com/Creation%20Method.html#Parameterized%20Anonymous%20Creation%20Method>