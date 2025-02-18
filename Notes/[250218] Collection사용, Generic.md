## Dynamic Array

크기가 동적인 배열 만들고 값 넣고 빼기 실습

📂 ***L250218_2***

`using System.Collections;`

⇒ Data Structure 자료구조 (ArrayList)

- DownCasting 이 잦으면 성능의 저하가 있음(boxing, unboxing)
    
    ex) Object[] 배열 생성 후 안에 값 넣을 경우 
    
    ### ➡️ **Generic 사용하기** : 형태가 동일해져서 버그 발생이 적어짐
    

**+) 오버로딩**

이름은 같고 인자가 다른 함수들

→ 컴파일러에서 알아서 처리해줌 ( 그래도 함수를 여러 개 작성해줘야 함)

## Generic | 📕 373, 642pg

https://learn.microsoft.com/ko-kr/dotnet/csharp/programming-guide/generics/generic-methods

```csharp
static public void Print<T>(T[] data)
{
    for(int i = 0; i < data.Length; i++)
    {
        Console.WriteLine(data[i]);
    }
}
```

- 사용할 때 자료형 유추가 가능하면 <> 안에 자료형 안 써주는 것도 가능 (써주는 게 좋음)
    
    `Print<string>(numbersToString);`
    

```csharp
static public T Add<T>(T a, T b)
{
    return a + b;
}
```

- 이렇게 하면 오류 ! → a랑 b가 더할 수 없는 것일 수 있기 때문에
    
    → 뒤에 **제약조건** 줄 수도 있음 
    
    `static public T Add<T>(T a, T b) where T : struct { }, class{} ...`  
    
    `class TDynamicArray<T> : IComparable`
    
    - 특정 조건의 클래스만 넣을 수 있어서 오류를 방지할 수 있음

- == 연산자로 비교 가능한 자료형 : int, float, bool, char
- == 연산자로 비교 불가한 자료형 : string

### 정리

- Generic 사용의 장점
    1. 박싱(값 → 참조), 언박싱(참조 → 값) 작업을 하지 않아도 돼서 성능이 향상
    2. 필요한 데이터 형식만 사용하여 형식이 안정적
- 모든 값을 담을 수 있는 `ArrayList 클래스` 대신
필요한 값을 선택해서 담을 수 있는 `제네릭 클래스`가 
성능이 빠르고 사용하기도 편리하다.

---

**C#을 할 줄 안다**

= 기본 예약어, 객체지향프로그래밍, Collection(C++의 STL) 사용할 줄 알아야 함

게임은 특히 성능이 매우 중요하기 때문에 자료구조를 잘 선택해서 사용해야 함!!

(유니티 layer에서 비트 연산하듯이,, )

---

### 스마트 포인터

https://learn.microsoft.com/ko-kr/cpp/cpp/smart-pointers-modern-cpp?view=msvc-170

스마트 포인터 레퍼런스 카운트

ex) 배열을 가리키는 개수를 카운트해서 0이 되면 메모리에서 지워주는 것

- C++ : 구현해야 함
- C# : GC 에서 자동으로 처리
