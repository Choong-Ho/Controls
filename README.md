# WPF Controls
`19년 프로젝트를 진행하며 구성한 일부 컨트롤`  
1. 외부 어셈블리 : ReactiveUI, NLog  
2. 내부 어셈블리 : MVVM, Controls, BaseResources, Extension.ReactiveUI

## 내부 어셈블리  
1. MVVM  
View의 기능 및 요구에 맞게 추상화된 ViewModel과 인터페이스로 구성 됨  
일부 ViewModel들은 GUID를 갖으며 이는 ReactiveUI의 MessageBus를 이용해 메시지를 송/수신할 때 키값으로 사용된다.(외부 어셈블리에 종속되지 않는다.)

2. Controls  
프로젝트를 진행하며 요구사항 및 디자인 요청에 의해 구성한 컨트롤들로 다른 프로젝트 팀에서도 공용으로 사용할 수 있도록 하는 것을 목표로 컨트롤즈 어셈블리 전체를 구성을 하였다.  
예제 코드는 Controls의 Window와 ListBox만을 대상으로 진행되었다.  
DialogWindow와 MessageBoxWindow는 현재 재직중인 회사의 대부분의 개발자들이 DevExpress에 익숙하여  
DevExpress의 IDialogService와 IMessageBoxService를 확인 후 진행했으며  
본래 고려하지 않던 부분이지만 기존 개발자들이 해당 인터페이스에 익숙해진 상태이고  
일부 클라이언트의 ViewModel에 프로젝트에서 구성한 MVVM 어셈블리의 ViewModel을 상속 받아 진행하는 것이 아닌  
DevExpress의 ViewModel을 상속받아 개발하는 부분이 발생해 구성을 하게 된 부분이다.  
그리고 DialogWindow와 MessageBoxWindow는 MessageBoxButton 타입의 의존 속성을 갖고 윈도우에 포함될 버튼을 구성한다.  
버튼은 MessageButtonsPanel과 MessageButtonsPanelExtension에 의해 MessageBoxButton 속성을 확인 후 구성되며  
MessageBoxButton 타입에 따라 종료 버튼을 표기하고 버튼을 누르는 것 이외도 Enter, Esc 키와 정의한 단축키(O, C, Y, N) 통해 응답 값을 결정할 수 있다.  
이외 컨틀롤의 경우 기본 탭 컨트롤은 ItemsSource에 컬렉션을 바인딩 시켜 구성하는 경우  
탭을 변경할 때 마다 콘텐츠가 계속 새롭게 생성되어 이전 상태를 유지하지 못하는 상황이 발생한다.  
탭 컨트롤의 경우 이러한 부분을 개선할 수 있도록 구성된 부분이다.  
이외 DateTimePicker, Calendar등이 특이 사항이 있는 컨트롤일 뿐 나머진 보편적인 구성을 갖는다.  
(컨트롤의 앞에 'Path' 가 붙는 컨트롤은 [https://materialdesignicons.com](https://materialdesignicons.com)을 참고해 구성하였다.)  

3. BaseResources  
Freezable을 상속 받는 객체들 Brush, Geometry, ImageSource들을 구성한 어셈블리이다.  
Geometry의 데이터는 위에서 언급한 [https://materialdesignicons.com](https://materialdesignicons.com)에서 확인 후 적용했다.  

4. Extension.ReactiveUI  
별다를 것은 없다.  
우선 ReactiveUI에 종속된 어셈블리이며 ReactiveUI와 관련된 확장 메서드를 구현할 목적으로 생성되었다.  
현잰 ReactiveCommand를 생성(동기/비동기)하는 메서드를 구성하고 해당 메서드에서 에러 로그와 IDisposable 객체를 수거할 수 있도록 구성된 상태이다.  
위에서 말한 메서드(CreateCommandWithThrownEx/CreateAsyncCommandWithThrownEx)를 구성한 목적은  
다른 개발자들이 커맨드를 실행시키는 메서드에 에러를 방치하는 부분이 있어서 구체화 시켰고  
또 다른 목적은 리소스 관리를 위함이다.  
예제 프로젝트엔 ReactiveUI의 IMessageBus등을 구성하지 않아 송/수신 받는 부분이 없지만  
메시지를 수신 받는 ViewModel이 해제될 땐 수신부도 해제가 되어야 한다.  
물론 GUID를 키값으로 비교 후 처리를 하여 오작동이 일어날 상황이 적지만  
이런 부분들이 쌓여 속도 저하와 GC 항목에서 빠지는 상황이 발생해 문제가 야기 되므로  
disposers를 구성 후 가이드를 하였다.  
하여 해당 메서드에도 disposers를 인자로 받아 IDisposable 객체를 수집하고  
커맨드가 멤버로 구성된 ViewModel이 해제될 때 같이 해제된다.