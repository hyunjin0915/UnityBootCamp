# 유니티 인터페이스

공통적인 특징에 대한 관리 구현 시 효과적

함수나 프로퍼티 등의 정의를 **구현 없이 진행할 수 있도록** 도와주는 기능

이는 명시만 하기 때문에 → 사용하기 위해서는 **반드시 상속을 통한 재구현으로** 진행

- 인터페이스는 상속처럼 등록 가능
- 다중 상속 가능

비슷한 형태의 데이터 양산이 매우 쉽게 진행 가능

---

## **+@) 개인 추가 내용**

- 자식 클래스 오버라이딩 강제
- 추상 메소드(abstract 키워드 포함X), 이벤트, 인덱서, 프로퍼티만 가질 수 있음
- 구현부가 없음
- 접근 제한 한정자 사용X - 모두 Public 으로 선언
- 자식 클래스에서 구현 할 메소드들은 public 한정자로 수식해야 함
- 인스턴스를 만들 수 X
- 자식 클래스는 인스턴스를 생성하면서 부모 클래스 형식으로 선언 가능(참조 형변환)
    
    `IAA Iaa = new AA();`
    
- 자식 객체 생성 이후에도 as 키워드, cast 연산자를 사용해 강제 형변환 가능
    
    `IBB Ibb = bb as IBB; IBB Ibb = (IBB)bb;`
    

### 인터페이스의 활용

### [**다형성](https://www.notion.so/e080f3cca4ae45e2bac8d1f5aed22def?pvs=21)** & 다중 상속

다형성에 의해 인터페이스를 상속한 클래스는 **인터페이스 인스턴스로서** 사용 가능

### Unity Game 개발에서의 예시

일반적으로 유니티 게임 오브젝트들은 Monobehaviour 클래스를 상속

→ 다른 클래스를 상속하기 어려운 경우가 많아 인터페이스를 적극 활용!

ex) 플레이어 공격 시 범위 안의 모든 공격 가능한 오브젝트에게 피해를 줄 때

```csharp
private void OnCollisionEnter(Collision collision)
 {
     if (CompareTag("Attackable"))
     {
         collision.gameObject.GetComponent<Status>().hp -= damage;
     }
 }
```

이렇게 작성할 수 있지만,

1. Tag를 너무 큰 범위로 묶는 것은 효율적이지 못함
2. 태그별로 조건문 일일이 달아줄 수도 없음

✅ **인터페이스를 사용하면** 

```csharp
private void OnCollisionEnter(Collision collision)
 {
     Attackable attackable = collision.gameObject.GetComponent<Attackable>();
     if (attackable != null)
     {
         attackable.Damaged();
     }
 }
```

1. 컴포넌트로써 읽는 것이 가능해지고
2. 다중 상속이 가능하기 때문에, 어떤 클래스라도 Attackable 인터페이스를 상속하여 공격 가능한 오브젝트임을 표현 O

---

# 유니티에서 제공해주는 Event, IPointer

## IPointer Interface

= Unity 의 EventSystem에서 기본적으로 제공되는 인터페이스

https://docs.unity3d.com/kr/530/Manual/SupportedEvents.html

- 클릭, 터치, 드래그 등의 이벤트를 구현할 때 사용
- **사용하기 위해서 필요한 조건들**
    1. UI 오브젝트에는 **Graphic Raycaster 컴포넌트**가 추가되어 있어야 함
        
        (Raycast Target 이 체크가 된 상태)
        
    2. Scene에는 **Event System 컴포넌트**가 존재해야 함
    3. 오브젝트에 대한 작업 시에는 **Collider 컴포넌트**가 추가 되어있어야 함
    4. MainCamera에 **Physics Raycaster 컴포넌트**가 추가되어 있어야 함
- **사용 방법**
    1. 이 기능을 사용할 오브젝트에 연결
    2. 씬에 EventSystem 오브젝트 배치
    만약 씬에 캔버스 생성했으면 자동으로 배치됨
    3. 오브젝트에 콜라이더 연결
    4. 카메라에 Physics Raycaster Component 연결

# Delegate

https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/delegates/

= 함수를 대입할 수 있는 변수

해당 변수에는 함수가 대입 되어 있으므로 해당 변수를 실행하면 대입한 함수가 실행되는 방식

`접근제한자 delegate 타입 이름(매개변수)`

- 유니티에서 제공해주는 Delegate 는 using System; 필요

## 사용이유

delegate는 함수가 아닌 **타입**이기 때문에 → **매개변수, return type**으로도 사용 가능

```csharp
void UseDelegate(DelegateTest dt)
{
	dt();
}

DelegateTest Selection(int value)
{
	if(value == 1) return Attak;
	else return Guard;
}
```

### **+@ 개인 추가 내용**

비동기 프로그래밍 가능

콜백 메커니즘 구현

이벤트 처리

https://kimstack.tistory.com/9

### Delegate Chain

> **+=** 를 통해 대리할 함수를 더 추가할 수 있고
> 
> 
> **-=** 을 통해 대리한 함수를 제거하는 것도 가능
> 

# Observer Pattern

한 오브젝트의 상태가 변경되면 그 객체에 의존하고 있는 다른 객체들에게 자동으로 내용이 갱신되는 설계 방식

### **+@ 개인 추가 내용)**

주체가 객체(subject) 상태 변화를 관찰하다가 상태 변화가 있을 때마다 메서드 등을 통해 옵저버 목록에 있는 옵저버들에게 변화를 알려주는 디자인 패턴

**일대다의 종속성**을 정의 

주로 **이벤트 기반 시스템**에 사용

# Action Delegate

반환 타입이 따로 없는(void) 형태의 대리자

### 1) 매개변수가 없는 경우

```csharp
Action action;
//…
action = Sample;
```

### 2) 매개변수가 있는 경우

```csharp
Action<T> action2 ;
//...
action2 = Sample2;
```

- 함수 오버로딩해서 등록하는 것도 가능

- 오버로딩, 오버라이딩
    
    **+) 오버로딩(Overloading)**
    
    일반적으로 함수명은 고유하게 존재
    
    매개변수의 개수, 타입, 순서가 다르면 같은 이름으로 메소드 정의 가능
    
    **+) 오버라이딩(Overriding)**
    
    부모 클래스로부터 상속 받은 메소드를 자식 클래스에서 재정의하는 것
    
    (인터페이스, 추상 클래스에서 선언만 되어있는 메소드 전달받은 경우 강제로 구현해야 함)
    
    하위 클래스에서 원하는 기능에 대한 구현
    
    인터페이스, 추상 클래스 등에서 제공받은 틀에 맞춰 정돈된 코드 설계 가능
    
    하나의 객체로 여러 형태를 표현하는 다형성 구현에도 효과적
    

# Func Delegate

= 반환 타입을 가지는 형태의 대리자

```csharp
//매개변수 X
Func<int> func01;
func01 = () => 10;
Func<int> test = () => 25;

//매개변수 O
Func<int, int, int> func02; //마지막이반환 타입
func02 = (a, b) => (a + b);

//식을 여러 개 작성
func02 = (a, b) =>
{
    if (a > b)
    {
        a = b;
    }
    return a + b;
};
```

# Event

개체에 작업 실행을 알리는 메세지

이벤트는 이벤트 가입자에게 특정 작업을 알려주는 기능

<aside>
💡

이벤트 클래스를 잘 설계하는 것이 중요

</aside>

## Event Handler

이벤트가 발생할 때 어떤 명령을 실행할지 지정해주는 것

- += 연산자를 통해 이벤트 핸들러를 이벤트에 추가
- -= 연산자를 통해 이벤트 핸들러를 삭제
- 하나의 이벤트에는 여러 개의 이벤트 핸들러를 추가할 수 있음
- 추가한 핸들러들은 순차적으로 호출됨

### 1)

```csharp
using UnityEngine;
using System;

public class SpecialPortalEvent
{
    public event EventHandler Kill;
    int count = 0;

    public void OnKill()
    {
        CountPlus();
        if (count == 5)
        {
            count = 0;
            Kill(this, EventArgs.Empty); //이벤트 핸들러들을 호출
        }
        else
        {
            Debug.Log("킬이벤트 진행 중");
        }
    }

    public void CountPlus() => count++;
}

public class UnityEventSample : MonoBehaviour
{
    // 1. 이벤트 정의
    SpecialPortalEvent specialPortalEvent = new SpecialPortalEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 2. 이벤트 핸들러에 이벤트 연결
        specialPortalEvent.Kill += new EventHandler(MonsterKill);

        for (int i = 0; i < 5; i++)
        {
            specialPortalEvent.OnKill(); //3. 이벤트 진행을 위해 기능 진행 
        }
    }

    // 이벤트가 발생했을 때 실행될 코드
    private void MonsterKill(object sender, EventArgs e)
    {
        Debug.Log("pp"); 
    }
}

```

### 2)

```csharp
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class MeetEvent
{
    public delegate void MeetEventHandler(string message);

    public event MeetEventHandler meetHandler;

    public void Meet()
    {
        meetHandler("Hamburger");
    }
}

public class UnityDelegateEventSample : MonoBehaviour
{
    public TMP_Text messageUI;
    MeetEvent meetEvent = new MeetEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        meetEvent.meetHandler += EventMessage;
    }

    private void EventMessage(string message)
    {
        messageUI.text = message;
        Debug.Log(message);
    }

    public void OnMeetButtonClicked()
    {
        meetEvent.Meet();
    }
}

```
