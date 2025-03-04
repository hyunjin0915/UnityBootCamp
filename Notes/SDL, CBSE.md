- OOP를 이용한 2D 렌더링 엔진 만들기 5
- SDL 사용법
- Component 베이스 변경
- 유니티 카메라 제어

---

# SDL 라이브러리

## nuget

https://www.nuget.org/

https://learn.microsoft.com/ko-kr/nuget/what-is-nuget

= .net 용 package manager

![image (2)](https://github.com/user-attachments/assets/fe621d6b-86a0-40ef-b704-663560478228)


설치하기

## code

```csharp
using SDL2;

namespace L250304
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Unity 초기화
            if(SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) <0) //timer, audio.. 모두 초기화하겠다는 뜻, | 연산, enum 형식
            {
                Console.WriteLine("Fail Init");
            }

            //창 만들기
            IntPtr myWindow = SDL.SDL_CreateWindow( //메모리 어디에 만들었는지 return
                "Game",
                100, 100,
                640, 480,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            //붓
            IntPtr myRenderer = SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC |
                SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);  //붓, gpu사용 | 주사율 맞춤 | 텍스쳐렌더링지원

            SDL.SDL_Event myEvent;
            bool isRunning = true;

            //메세지 처리(사용자 처리가 추가 구조를 바꿈)
            while (isRunning) //이벤트가 있는지 끝났는지 물어보는
            {
                SDL.SDL_PollEvent(out myEvent);
                switch (myEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        isRunning = false;
                        break;

                }

                SDL.SDL_SetRenderDrawColor(myRenderer, 0,0,0,0);
                SDL.SDL_RenderClear(myRenderer); //붓으로 화면 지우기

                SDL.SDL_RenderPresent(myRenderer); //CPU 에서 작업 → GPU에 작업하라고 던져주는 과정 필요
            }

            SDL.SDL_DestroyWindow(myWindow); //지우기

            SDL.SDL_Quit(); //라이브러리도 지워줘야 함
        }
    }
}

```

## Event Driven Programming

윈도우 메세지 큐

GetMessage 로 큐에 있는 메세지를 가져와서 → Translate, Dispatch → switch 문으로 해당하는 명령어 실행

이벤트 부분만 제작 : Scripting (유니티는 스크립팅)

https://docs.unity3d.com/kr/2019.4/Manual/ScriptingSection.html

- PeekMessage

https://wiki.libsdl.org/SDL2/SDL_RendererFlags

Open GL - 오래됨

Vulkan - 요즘 사용

metal - apple

direct x - 윈도우 사용

## 랜덤으로 사각형 그리기

```csharp
//랜덤 100개 사각형 그리기
for (int i = 0; i < 100; i++) 
{
    byte r = (byte)(random.Next() % 256);
    byte g = (byte)(random.Next() % 256);
    byte b = (byte)(random.Next() % 256);
    SDL.SDL_SetRenderDrawColor(myRenderer, r, g, b, 0);

    SDL.SDL_Rect myRect;
    myRect.x = random.Next() % 640;
    myRect.y = random.Next() % 480;
    myRect.w = random.Next() % 640;
    myRect.h = random.Next() % 480;

    int type = random.Next()%2;
    switch (type)
    {
        case 0:
            SDL.SDL_RenderDrawRect(myRenderer, ref myRect);
            break;
        case 1:
            SDL.SDL_RenderFillRect(myRenderer, ref myRect);
            break;
    }

         
}
```

## 원 그리기

```csharp
int x0 = 320;
int y0 = 240;
SDL.SDL_SetRenderDrawColor(myRenderer, 255, 255, 255, 255);

double r = 200.0f;

int nextX = (int)(r * Math.Cos(0 * (Math.PI / 180.0f)));
int nextY = (int)(r * Math.Sin(0 * (Math.PI / 180.0f)));

for (int i = 10; i <= 360; i+=10)
{
    int x = (int)(r * Math.Cos(i * (Math.PI/ 180.0f)));
    int y = (int)(r * Math.Sin(i* ( Math.PI/180.0f)));

    SDL.SDL_RenderDrawLine(myRenderer, x0 + nextX, y0 + nextY, x0 + x, y0 + y);
    nextX = x;
    nextY = y;
}

```

GPU - 실수

CPU - 정수

<aside>
🍥

2D 렌더링 엔진 만들기에 적용하기 ( ***📂 L250218*** )

</aside>

- **Key 입력**
    
    ConsoleKey → SDL_KeyCode 
    
    속도 더 빠름
    
    ```csharp
    static public bool GetKeyDown(SDL.SDL_Keycode key)
    {
        return (Engine.Instance.myEvent.key.keysym.sym == key);
    }
    ```
    
- **렌더링**
    
    Engine 클래스에서 기본 화면 그리고
    
    GameObject 클래스 - Render() 에서 각 오브젝트의 색상과 크기로 화면에 그리기
    

# 유니티

GameObject : 유니티 공간에서 하나의 **Entity**

위치, 회전, 크기를 갖는다는 뜻

`GameObject obj = new GameObject();`

`Instantiate(GameObject);` //특화 함수

## Component Base Programming

https://ko.wikipedia.org/wiki/%EC%BB%B4%ED%8F%AC%EB%84%8C%ED%8A%B8_%EA%B8%B0%EB%B0%98_%EC%86%8C%ED%94%84%ED%8A%B8%EC%9B%A8%EC%96%B4_%EA%B3%B5%ED%95%99

의존성 상쇄 → 조합해서 새로운 것 만들기 좋음

빈 게임 오브젝트 만들어서 해당하는 컴포넌트 고르기

### 특정 이벤트만 재정의해서 원하는 기능을 구현할 수 있게 되어있음

`class CustomComponent : Monobehaviour`

커스텀 컴포넌트 생성 : 원래 있던 컴포넌트 상속 받기

+) Hierarchy 계층구조가 내부에서 Transform 으로 되어있음 - 스크립트 상에서 자식 객체에 접근할 때 transform. 으로 해야 함 (원래는 gameObject. 로 되어야 하는데,,)

- 네트워크에서는 함수 실행 순서에 따라 문제가 발생할 수 있는 경우들이 있음

### Mesh Renderer

https://docs.unity3d.com/2022.3/Documentation/Manual/class-MeshRenderer.html

메시를 렌더링 

### Mesh Filter

https://docs.unity3d.com/2022.3/Documentation/Manual/class-MeshFilter.html

무엇을 그릴지

### Transform 이동

`transform.position += transform.up * v * Time.deltaTime * moveSpeed;`

Vector3.up 으로 쓰면 world 좌표계 기준으로 위로 올라가버림
`transform.Translate(Vector3.up * v * Time.deltaTime * moveSpeed);`

Translate 함수 두 번째 인자 = Space.Self 생략 → 자신을 기준으로 

### Transform 회전

`transform.eulerAngles += transform.forward * -h * Time.deltaTime * rotateSpeed;`

[***오일러 각도***](https://www.notion.so/1a09103cc1eb80a5a8a1ec74810dedf2?pvs=21) 

→ 부모 오브젝트에 - 위치 값, 충돌 정보

mesh 값 등은 자식 오브젝트에
