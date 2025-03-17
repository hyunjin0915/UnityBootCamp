목표 : Socket API , recv, send, listen, 채팅 만들어 봄

- JSON
- 네트워크란
- SOCKET
- TCP

---

### FSM(Finite - State - Mashine | 유한 상태 기계)

https://f-lab.kr/insight/understanding-and-applying-fsm-in-game-development

게임의 상태는 **무한** → 전부 구현할 수 없음

➡️ **유한하게 개수를 제한**, 그 상태일 때 무엇을 하는지 규정하고 구현하는 것

- 각 상태를 명확하게 정의하고 상태 간 전환을 쉽게 관리
- 게임 오브젝트의 행동 패턴을 더 쉽게 설계하고 구현할 수 있음

`OnEnter() - 로직 수행 - OnExit()`

- 상태 간의 연결 주의 (엑셀로 표 그려서 구멍 있는 지 확인해보기)
    
    어느 상태이던 수행해야 하는 State가 있을 때 이걸 모든 State 와 연결해줘야 함
    
    (유니티 애니메이션의 Any State) → 빼먹으면 멈춰버림
    

- 유니티 애니메이션 Mecanim

https://docs.unity3d.com/kr/2020.3/Manual/AnimationOverview.html

### 애니메이션 리타게팅 - 휴머노이드

https://docs.unity3d.com/kr/530/Manual/Retargeting.html

동일한 애니메이션 세트를 다양한 캐릭터 모델에 비교적 편리하게 적용

---

# 🌀 Json

- 다른 기종과 네트워크 하기 위해 사용
- 게임 데이터, 채팅, 자료, 실시간 성이 떨어지는 것에 사용
    
    (실시간ms로 사용되는 것들은 Byte Socket으로 만들어야 함)
    

## Serialize

https://learn.microsoft.com/ko-kr/dotnet/standard/serialization/

https://ahma.tistory.com/65

Class → Instance Memory → 파일 저장

**Serialize**(ToJson)

```csharp
    static void Main(string[] args)
    {
        HelloWorld h = new HelloWorld(20, 10);

        StreamWriter sw = new StreamWriter("./data.dat");
        sw.WriteLine(h.gold);
        sw.WriteLine(h.mp);

        sw.Close();
    }

```

**Deserialize**(FromJson)

```csharp
    static void Main(string[] args)
    {
        HelloWorld h = new HelloWorld(20, 10);

        StreamReader sr = new StreamReader("./data.dat");
        string DataGold = sr.ReadLine();
        string DataMp = sr.ReadLine();

        HelloWorld h2 = new HelloWorld(int.Parse(DataGold), int.Parse(DataMp));

        sr.Close();
    }

```

### Serialize 라이브러리 종류

회사에서 따로 만들어주기도 함

- **FlatBuffer**(json 보다 10000배 정도 빠름)
- **MessagePack**
- **BJson** ; 바이너리로 만든 바이트 단위 json - 잘 쓰지는 않음
- **YAML** ; 탭으로 나뉘는 - 유니티에서 Scene 파일 등에 사용됨/ 고치기 편한

## SGML(Standard Generalized Markup Language)

= 마크업 언어를 정의하기 위한 표준

자료를 저장하기 위해 

C# → 파이썬 ; serialize

### HTML/ XML

만든 이유 : 이기종 간 자료 전송을 위해

→ 속도가 느리고 복잡하고 메모리를 많이 씀,,,

### Json

이기종 간 자료 전송

간단/ 문자열로 되어 있어서 편집이 편하고/ XML 보단 빠름

(Unity - Json Utility https://docs.unity3d.com/kr/2019.4/Manual/JSONSerialization.html)

## 실습

- Newtonsoft.Json 설치
- 
![image (2)](https://github.com/user-attachments/assets/0939ab3d-9cfd-4172-8136-01cf19a06b6a)


```csharp
        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.Add(new GameObject(10, 30));
        gameObjects.Add(new GameObject(4, 30));
        gameObjects.Add(new GameObject(10, 5));
        gameObjects.Add(new GameObject(79, 30));
        gameObjects.Add(new GameObject(8, 56));
        string jsonData = JsonConvert.SerializeObject(gameObjects);
        Console.WriteLine(jsonData);

        List<GameObject> gameObjects2 = JsonConvert.DeserializeObject<List<GameObject>>(jsonData);
        foreach(var go in gameObjects2)
        {
            Console.WriteLine(go.Gold);
        }

```

### 프로젝트에 써보기

- world 클래스에서 게임의 모든 오브젝트를 갖고 있음
    
    → 이걸 Engine.cs에서 Serialize 
    
- C#의 Reflection으로 가져오는 것
    - TextureRenderer 클래스의  IntPtr로 저장된 값들은 C 언어라서 reflection 안됨 - 따로 처리 필요

```csharp
        string SceneFile = JsonConvert.SerializeObject(world.GetAllGameObjects, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        Console.WriteLine(SceneFile);
        StreamWriter sw = new StreamWriter("sample.uasset");
        sw.WriteLine(SceneFile);
        sw.Close();

        List<GameObject> gos = JsonConvert.DeserializeObject<List<GameObject>>(SceneFile, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

```

### +@

### UnityWebRequest

https://docs.unity3d.com/kr/2022.3/Manual/UnityWebRequest.html

### 유니티 Netcode

= 네트워킹 로직을 추상화 할 수 있도록 Unity를 위해 만들어진 네트워킹 라이브러리

> **인게임 로직**은 이걸 사용
**나머지는** 소켓 프로그래밍(TCP)
> 

### 네트워크 데드레커닝 | Dead Reckoning

https://cocoa106.tistory.com/entry/%EC%98%A8%EB%9D%BC%EC%9D%B8-%EA%B2%8C%EC%9E%84%EC%9D%98-%EC%9D%B4%EB%8F%99%EC%B2%98%EB%A6%AC-%EA%B8%B0%EB%B2%95-%EB%8D%B0%EB%93%9C-%EB%A0%88%EC%BB%A4%EB%8B%9D-2-%EB%B3%B4%EA%B0%84-%EC%B2%98%EB%A6%AC

= 신호가 오지 않는 동안의 행동을 서버가 추측하여 상태를 갱신하는 알고리즘

- 클라이언트에서 방향을 전환할 때만 추가 패킷을 보내고 서버는 그 사이에서 클라이언트의 게임 캐릭터 위치를 예측하여 동기화를 맞추는 기법(보간 처리)
- 게임 서버에서 게임 클라이언트와 실시간 동기화를 위한 알고리즘
- 네트워크 통신량을 줄이고 데이터 정확도 보장

---

# 🌀 네트워크

**Socket** https://ko.wikipedia.org/wiki/%EB%84%A4%ED%8A%B8%EC%9B%8C%ED%81%AC_%EC%86%8C%EC%BC%93

**포트** https://ko.wikipedia.org/wiki/%ED%8F%AC%ED%8A%B8_(%EC%BB%B4%ED%93%A8%ED%84%B0_%EB%84%A4%ED%8A%B8%EC%9B%8C%ED%82%B9)

### IPEndPoint

https://learn.microsoft.com/ko-kr/dotnet/api/system.net.ipendpoint?view=net-8.0

⭐ 이 구조는 통으로 외워야 함 ⭐

## Server

```csharp
internal class Program
{
    static void Main(string[] args)
    {
        Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				//랜카드 상관 없이, 포트번호 4000으로 오는 것 다 받는다는 뜻
        IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);
        //그걸 소켓에 IP주소와 포트번호를 묶어(Bind)줘야 함
        listenSocket.Bind(listenEndPoint);

        listenSocket.Listen(10); //몇 개를 듣고 있을 건지

        bool isRunning = true;
        while(isRunning)
        {
		        //동기 방식(Blocking) : 이게 끝날 때까지 다른 일X, 멈춰있게
            Socket clientSocket = listenSocket.Accept(); //동기, 블록킹

            byte[] buffer = new byte[1024]; //임시 기억 장치로
            int RecvLength = clientSocket.Receive(buffer); //얼마나 줄지는 모르겠지만 일단 받음
            if (RecvLength == 0)
            {
                //close
                isRunning = false;
            }
            else if(RecvLength < 0)
            {
                // error
                isRunning= false;
            }
            int SendLength = clientSocket.Send(buffer);
            if (SendLength == 0)
            {
                //받는 편에서 끊어버린
                isRunning = false ;
            }
            else if(SendLength < 0)
            {
                //통신이 안되는 것
            }
            //keep alive time
            clientSocket.Close();
        }

        listenSocket.Close();

    }
}

```

<aside>
💥

socket > bind >listen >accept | recv send

</aside>

## Client

```csharp
internal class Program
{
    static void Main(string[] args)
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //내 자신에게 보내는 것 - 테스트 용(= "127.0.0.1" 이랑 같은 뜻)
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 4000);
        serverSocket.Connect(serverEndPoint);

        byte[] buffer;

        String message = "Hello world";
        buffer = Encoding.UTF8.GetBytes(message);
        int sendLength = serverSocket.Send(buffer);

        byte[] buffer2 = new byte[1024];
        int RecvLength = serverSocket.Receive(buffer2);

        Console.WriteLine(Encoding.UTF8.GetString(buffer2));

        serverSocket.Close();
    }
}

```

<aside>
💥

socket > connect | send recv

</aside>

- 원래는 보내는 데이터를 다 보냈는지 보장해주는 코드를 따로 다 만들어줘야 함
(문자열의 일부만 가고 뒷부분 안 갈 수도 있으니,,)
    
    ```csharp
    int sendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
    while(sendLength < buffer.Length)
    {
         int SendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
    }
    ```
    

→ Receive 함수에도 Offset 이랑 Size 지정 가능!

- 문자열 패킷은 끝이 어디인지 알려줘야 함
    - html 은 줄바꿈으로 나눠줌
- 게임에서는 실제로는 문자열로 사용하지 않음
    - **바이트 단위로 !!!**
        
        ex) 9바이트 - int 면 4바이트, 4바이트 숫자 4바이트 숫자 연산자 1개 
        
    - Json

### 소켓 통신

- 운영체제가 중요함
- 비동기 입출력 방법 → OS API가 바뀜

IOCP

---

### 🗞️ 내일

- 서버 - select
- 클라이언트는 무조건 멀티스레드 써야 함
    
    ex)싱글스레드면,, 클라에서 Console.readline 하면 작업이 멈춤,, 
    
    → 네트워크 못 쓰니까 
    
- C# 동기화
