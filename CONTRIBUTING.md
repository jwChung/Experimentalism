How to contribute to Experimentalism
====================================
Experimentalism에서는 타인과의 협업을 위한 규칙이 필요합니다. 본 문서에서는 규칙과 자율성(프로젝트 참여 용이성) 사이의 균형점을 찾기 위한 노력을 하고 있습니다.

본 문서에 대한 문의가 있을 시, [Issue list](https://github.com/jwChung/Experimentalism/issues)에 질문을 등록하실 수 있습니다.

개발환경
-------
Experimentalism는 현재 아래와 같은 환경에서 개발 중입니다.

* Visual Studio 2010 이상
* .NET Framework 4.0

Experimentalism 개발 가이드라인
-----------------------------
* **TDD**  
  Experimentalism은 100% TDD하에 개발되고 있습니다(코드커버리지 거의 100% 수준). Experimentalism에서는 TDD를 통한 개발을 독려하고 있습니다.
* **SRP**  
  소프트웨어 개발에 많은 원칙들이 있지만, Experimentalism은 그 중에서도 [SRP(Single Responsibility Principle)](http://en.wikipedia.org/wiki/Single_responsibility_principle) 원칙을 중요시하며 이를 지키려고 노력합니다. 개발에 참여를 원하시는 분은 이점을 고려하여 주시기 바랍니다.

Pull requests
-------------
개발참여는 GitHub의 [pull requeset](https://github.com/jwChung/Experimentalism/pulls)를 통하여 참여하실 수 있습니다. 아래와 같은 코딩스타일, 커밋메세지 포멧을 지켜야하며, 해당 pull request가 이상이 없는지 확인하는 과정이 필요합니다.

### Coding style
* **코드라인은 120자를 넘지 않아야 합니다.**  
  120자 길이 규칙은 요즘 통용되는 모니터를 고려했을 때, 그리 엄격한 제한이 아니라고 생각되며, GitHub의 코드리뷰에서도 **120자** 라인규칙을 기본으로 하고 있습니다.
* **현재 코딩되어 있는 스타일을 따라 주세요.**  
  개인마다 선호하는 코딩스타일이 있을 것입니다. 하지만, 현재 Experimentalism에서 코딩되어 있는 스타일을 따라 주시기 바랍니다. 예를 들어, 필드명은 현재 언더바를 prefix로 사용하고 있습니다. 따라서 `this.필드명`이 아니고 `_필드명`으로 코딩하여 주시기 바랍니다.

### Commit message format
커밋메세지는 협업에서 상당히 중요합니다. 작업에 대한 내용을 커밋메세지에 일목요연하게 담아냄으로써 타인이 직접 코딩을 보지 않고, 어떤 작업이 이루어졌는지 이해할 수 있게 해 주는 배려가 필요합니다. 이를 위해 Experimentalism에서는 다음과 같은 커밋메세지 포멧을 제시합니다.

```
<종류>: <제목>(70자 제한)
공백 줄 추가
<내용>
```

* <종류>  
    아래 여섯 가지 종류 중 하나를 반드시 명기합니다.
    * dev: 특정 feature 구현이나 버그 fix와 같은 일련의 개발코드가 추가 되었음을 의미합니다. dev는 리팩토링과 반대되는 개념으로 [Transformation](http://blog.8thlight.com/uncle-bob/2013/05/27/TheTransformationPriorityPremise.html)과 같은 의미로 사용됩니다.
    * refactor: 코드 리팩토링을 의미합니다.
    * test: dev와 refactor는 제품 코드와 테스트 코드가 동시에 변경이 되는 것이 일반적입니다. 반면, test는 오직 테스트 코드만 변경되었을 경우를 말합니다.
    * docs: XML document나 개발문서의 변경을 의미합니다.
    * style: 예를 들어, 공백, 띄어쓰기 등과 같이 dev, refactor과 상관 없는 코딩 스타일 변경을 의미합니다.
    * build: 빌드스크립트 변경과 같은 빌드에 관한 사항의 변경을 의미합니다.
* <제목>  
    해당 작업내용의 제목을 작성합니다. 이때, GitHub에서는 제목 라인을 **70자**를 기준으로 이를 초과 시 "..."으로 나타냅니다. 따라서 70자를 초과하지 않게 작성하는게 좋습니다.
* 공백 줄 추가  
    제목과 내용은 공백 줄로 분리되어야 합니다. (git commit message 가이드라인)
* <내용>  
    필요 시, 해당 커밋의 작업내용에 대한 의도, 방법 이유 및 해당 작업에 대한 side effect에 관한 사항을 작성합니다.
    컬럼 제한이 꼭 필요하다고 생각되지는 않지만, 되도록이면 제목과 같이 70자 제한을 따르는 것이 좋습니다. 컷밋메세지 포멧을 어떻게 하면 좋을지에 관해, [잘 알려진 문서](http://tbaggery.com/2008/04/19/a-note-about-git-commit-messages.html)가 있습니다. 참고하시길 바랍니다.

### pull request 검증  
Experimentalism의 루트 디렉토리에는 Run.cmd라는 파일이 있습니다. 이를 실행하여 에러가 발생하지 않으면, 이상이 없는 것으로 판단합니다.

Visual studio에서는 release모드에서 모든 테스트들이 성공하고, 솔루션 레벨 Code analysis실행 시(단축키: Alt+F11) 어떠한 경고도 없을 때 이상이 없는 것으로 간주합니다.

Jump in
--------
Experimentalism 오픈소스 참여에 익숙하지 않으신 분들은 [Issue list](https://github.com/jwChung/Experimentalism/issues)에서 **Jump in** 라벨이 붙은 이슈를 주목하시기 바랍니다. Jump in 라벨은 다음과 같은 의미이며, _Experimentalism 오픈소스 참여를 독려하기 위한 장치_입니다.

* 해당 이슈를 해결하는데 상대적으로 많은 시간을 할애하지 않아도 된다.
* 상대적으로 독립적인 이슈이기 때문에 이해하기 쉽다.
* 해당 이슈는 구현을 위한 핵심을 잘 지적하고 있다.
* 필요 시 해당 이슈에 대한 설명을 요청할 수 있다.

보다 상세한 Jump in 라벨에 대한 내용은 [Nik Molnar의 New Contributor? Jump In!](http://nikcodes.com/2013/05/10/new-contributor-jump-in/) 포스트를 참고하시기 바랍니다.

Additional resources
--------------------
아래는 오픈소스 프로젝트 참여에 도움을 줄 수 있는 글에 대한 링크들입니다.

* [Open Source Contribution Etiquette](http://tirania.org/blog/archive/2010/Dec-31.html)
* [The Rules of the Open Road](http://blog.half-ogre.com/posts/software/rules-of-the-open-road)