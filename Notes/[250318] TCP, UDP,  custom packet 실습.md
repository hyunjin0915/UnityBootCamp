TCP- UDP- Thread- 멀티플렉싱

---

# 실습 과제

## 1. Client 에서 안녕하세요 보내면 Server에서 반가워요 보내는 프로그램 만들기 (메세지 확인, Newtonsoft Json 사용)

## 🚩 Server

### 포트 번호

- 1024 ~ 65534 까지 사용
    
    > 0번 ~ 1023번: 잘 알려진 포트 (well-known port)
    1024번 ~ 49151번: 등록된 포트 (registered port)
    49152번 ~ 65535번: 동적 포트 (dynamic port)
    > 
    - 보통 인자로 서버 포트랑 IP 받아서 사용함(하드코딩 NO)
    - sockaddr  https://learn.microsoft.com/ko-kr/windows/win32/winsock/sockaddr-2

- `listenSocket.Listen(10);`
    
    backLog 숫자가 크다고 좋은 게 X (10이어도 1000명 들어올 수 있음)
    
- `Receive - ArraySegment` 라는 것도 있음 https://learn.microsoft.com/ko-kr/dotnet/api/system.net.sockets.socket.receive?view=net-8.0
    
    
- `int RecvLength = clientSocket.Receive(buffer);`
    
    = OS 내부 버퍼에서 복사해옴(**TCP 버퍼**에서!)
    
    네트워크를 바로 받는 게 아니라 OS가 미리 받아온 것을 일정 크기 잘라오는 것
    
    ⭐  **TCP 는 신뢰성 보장  → 왔다는 건 제대로 순서대로 왔다는 것(속도 상관없이)** ⭐
    
- `clientSocket.Send(buffer);`
    
    = OS 내부 버퍼에 복사함
    
    자료의 전부를 보내는 것이 아님(컴터가 바쁠 때)
    
- **JObject** 사용 (그냥 클래스 만들어서 보내도 됨)

```csharp
JObject json = JObject.Parse(str);
if (json.Value<string>("message").ToString().CompareTo("안녕하세요") == 0)
{
    JObject result = new JObject();
    result.Add("message", "반가워요");

    byte[] message;
    message = Encoding.UTF8.GetBytes(result.ToString());
    int sendLength = clientSocket.Send(message);
}
```

- **인코딩** https://ko.wikipedia.org/wiki/%EB%AC%B8%EC%9E%90_%EC%9D%B8%EC%BD%94%EB%94%A9
    
    = 사용자가 입력한 문자나 기호를 컴퓨터가 이용할 수 있는 신호로 만드는 것 
    

## 🚩 Client

- 클래스 객체 안 만들고 이렇게 문자열로 보내도 됨(Json 장점 ㅎㅎ)
    
    ```csharp
    string jsonString = "{\"message\" : \"안녕하세요\"}";
    byte[] messsage = Encoding.UTF8.GetBytes(jsonString);
    int sendLength = severSocket.Send(messsage);    
    ```
    

## 주의

- 세 번 보낸다고 세 번 받는 게 X (네트워크 상황.. 등에 따라서 다름)
- 전체 패킷 사이즈가 얼마인지 확인하고 제대로 보내졌는지 확인하는 과정이 필요
    
    ⇒ TCP 특징 
    

## 2. Clinet가 접속하면 Server에서 이미지 파일 보내주고 Client 는 파일로 저장하는 프로그램 만들기

## webp ≠ text 파일 형식이 다름

### StreamReader

https://learn.microsoft.com/ko-kr/dotnet/api/system.io.streamreader?view=net-8.0

= 특정 인코딩의 바이트 스트림에서 **문자를 읽는** [TextReader](https://learn.microsoft.com/ko-kr/dotnet/api/system.io.textreader?view=net-8.0) 를 구현

문자(Char)단위로 데이터를 읽음

파일에서 **텍스트**를 읽음 → ~~아스키코드 중에서 띄어쓰기는 읽지 않음~~

그냥 파일을 읽을 때는 StreamReader를 쓰면 안됨

![image (11)](https://github.com/user-attachments/assets/32f0b3db-1a73-4abe-ac88-075aed37fd4a)

> 특정 아스키 값이 필요할 때 - **BinaryReader** 필수
> 
> 
> 바이너리 파일 다룰 때 - 반드시 **BinaryReader/ FileStream** 사용
> 

### ➡️ **FileStream** 쓰기

https://learn.microsoft.com/ko-kr/dotnet/api/system.io.filestream?view=net-8.0

파일을 Byte로 불러옴 

<aside>
❔

서버가 1096으로 보냈는데 클라이언트에서 1바이트씩 받으면?

→ 그냥 잘 실행됨

서버가 40kb 씩 보냈는데 클라이언트에서 1바이트씩 받으면?

→ 그래도 잘 실행됨

서버가 3바이트씩 보내는데 클라이언트에서 40kb씩 받으면?

→ 그래도 상관 없음

</aside>

➡️ **TCP** 의 흐름제어! (슬라이딩 윈도우)

그런데 10번 보낸다고 10번 받는 건 X

내가 자료를 들이부으면 그곳에 쌓이게 되고, 그걸 OS가 퍼오는 것임

→ 그래서 받은 만큼 써주는 코드가 필요하고/ Json의 경우 전체 문자열이 다 와야 하니까 그것도 체크 필요

header

[][]      [][]      [][]

코드   길이    64kb

trailer(checksum)

### 파일 불러와서 복사해보기

```csharp
do
{
    ReadSize = fsInput.Read(buffer, 0, buffer.Length);
    fsOutputt.Write(buffer, 0, ReadSize);
} while (ReadSize > 0);
```

전체 파일 크기가 딱 나눠 떨어지지 않음 → 마지막에는 남은 바이트 크기만큼 읽어오고 그 크기를 Return

### + ) Base64

https://ko.wikipedia.org/wiki/%EB%B2%A0%EC%9D%B4%EC%8A%A464

= 바이너리 데이터를 문자 코드에 영향을 받지 않는 공통 아스키 문자로 표현하기 위해 만들어진 인코딩

Json의 text 에 binary 넣을 수 없음 

→ 냅다 다 binary를 text 처럼 바꾸는 것

- HTTP는 다 이걸로 작업
- C#에서 Convert로 사용 가능

---

### 게임에서는

- TCP, UDP 모두 열어서 가장 적절한 것을 선택해서 설계
    
    (뭐가 더 좋고 무조건 무엇을 쓴다는 것 X )
    
- 총 마구 쏘는 건 UDP 사용
    
    → 총 맞았는지 모르면 ?? → RUDP 라는 게 있긴 하지만 힘들게 구현하지 말고
    그냥 TCP 쓰는 게 나음
    

- 서버 여러 개가 있음

<aside>
💥

신뢰성과 속도는 모두 TCP, UDP 서로에 대해 상대적인 것!

</aside>

### ==================================================

# 🌀 UDP

- 소포(**중간에 자르지 않음**)
- 집밖에 내두면 택배사에서 가져감(OS)
- TCP에 비해 패킷 길이가 짧음(기능이 더 적음)
    
    → 비교적 속도가 빠름
    
- 순서 보장 Nope

## 실습 해보기

### Server

```csharp
Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 6000);

serverSocket.Bind(serverEndPoint);

//택배를 보고 어디서 보낸건지 알아야 함
byte[] buffer = new byte[1024]; 
EndPoint clientEndPoint = (EndPoint)serverEndPoint; //??

int RecvLength = serverSocket.ReceiveFrom(buffer, ref clientEndPoint);

int SendLength = serverSocket.SendTo(buffer, clientEndPoint);

serverSocket.Close();
```

- TCP와 다르게 연결 필요 없음(바로 데이터 수신, 전송)
    - Bind()만 하면 됨, Listen()필요 없음
- `EndPoint clientEndPoint` 에 아무 값이나 들어가 있어야 동작하므로 처음에 serverEndPoint를 캐스팅해서 넣은 것
    - TCP에서는 Accept() 하면 Socket 객체가 생성되지만,
    UDP는 ReceiveFrom() 을 호출해야 보낸 사람 정보를 알 수 있음
    - UDP는 클라이언트와 미리 연결되지 X
    → 메세지 받을 때마다 누가 보냈는지 확인 필요

<aside>
💥

- 소켓을 만들고 특정 포트에 바인딩 (`Bind()`)
- 클라이언트의 메시지를 받을 공간(`buffer`)과 보낸 사람 주소(`clientEndPoint`) 준비
- `ReceiveFrom()`을 호출하여 데이터 수신 (클라이언트 주소도 자동으로 저장됨)
- `SendTo()`를 사용하여 받은 클라이언트에게 응답 전송
- 필요하면 소켓을 닫음 (`Close()`)
</aside>

### Client

```csharp
Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 6000);

byte[] buffer = new byte[1024];
string message = "안녕하세요";
buffer = Encoding.UTF8.GetBytes(message);
int SendLength = serverSocket.SendTo(buffer,buffer.Length, SocketFlags.None, serverEndPoint); 
//바로 그냥 던져버리면 됨

byte[] buffer2 = new byte[1024];
EndPoint remoteEndPoint = serverEndPoint;
int RecvLength = serverSocket.ReceiveFrom(buffer2, ref remoteEndPoint);

string message2 = Encoding.UTF8.GetString(buffer2);
Console.WriteLine(message2);
serverSocket.Close();
```

- TCP와 다르게 연결 필요 없음
    - SendTo()하면 됨, Connect() 필요 없음
- `EndPoint remoteEndPoint` : 응답을 보낸 서버의 주소 정보가 저장될 공간
    - UDP는 연결이 없으니까 데이터를 받으려면 누가 보냈는지 매번 확인해야 함 (ReceiveFrom()에서 EndPoint 사용)

- UDP는 쪼개서 못 받음
    
    → 내부 버퍼 사이즈보다 큰 거 보내면 못 받고 오류 남!
    
    ![image (12)](https://github.com/user-attachments/assets/33f31372-0fa8-430f-977e-42bb0870ad72)


- 유니티 netcode는 UDP

### 실제로 IP 주소를 직접 작성하지 않음 ➡️ DNS 사용

```csharp
IPHostEntry host = Dns.GetHostEntry("naver.com");
foreach(IPAddress address in host.AddressList)
{
    Console.WriteLine(address);
}
```

이렇게 받아온 주소를 매개변수로 사용 

- 여기부터 확인해서 사용
  
    ![image (13)](https://github.com/user-attachments/assets/8841c637-2003-4082-83de-afbc7524278d)
    

# 🌀 TCP

- 보내면 가는 거지 끊김이 없음 (stream)
- 길이를 모름 → 앞에 두 바이트를 크기를 두고 보냄
    
    Blocking mode 로 하면 그 두 바이트를 받을 때까지는 기다리라고 할 수 있음
    
    None Blocking 은 그냥 계속 반복문 
    

## Server

```csharp
Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

listenSocket.Bind(listenEndPoint);
listenSocket.Listen(10);
Socket clientSocket = listenSocket.Accept();

//패킷 = 헤더 + 데이터 

//패킷 길이(header) 받기
byte[] headerBuffer = new byte[2]; //바이트 수는 선택사항, 원하는 위치 조정은 ArraySegment 사용 C#은 포인터가 없어서
//원래 여기에 길이수 맞는지 예외처리 추가해줘야 함
int recvLength = clientSocket.Receive(headerBuffer, 2, SocketFlags.None);
short packetLength = BitConverter.ToInt16(headerBuffer, 0);
packetLength = IPAddress.NetworkToHostOrder(packetLength);   

//실제 패킷 (header 길이 만큼)
byte[] dataBuffer = new byte[4096];
//여기도 n비트만큼 받으라고 예외처리 필요
recvLength = clientSocket.Receive(dataBuffer, packetLength, SocketFlags.None);

string jsonStr = Encoding.UTF8.GetString(dataBuffer);

Console.WriteLine(jsonStr);

//Custom Packet  만들기! -> 함수로 만들어주면 됨
//다시 전송 메세지
string message = "{\"message\" : \"잘 받았옹\"}";
byte[] messsageBuffer = Encoding.UTF8.GetBytes(message);
//ushort length = (ushort)messsageBuffer.Length; //메세지 전체 길이 저장 
ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messsageBuffer.Length);

//전체 길이 자료는
headerBuffer = BitConverter.GetBytes(length);

byte[] packetBuffer = new byte[headerBuffer.Length + messsageBuffer.Length]; //앞에 2바이트 + 진짜 문자열의 길이

Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
Buffer.BlockCopy(messsageBuffer, 0, packetBuffer, headerBuffer.Length, messsageBuffer.Length);

int sendLength = clientSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);

clientSocket.Close();
listenSocket.Close();
```

## Client

```csharp
string jsonString = "{\"message\" : \"안녕하세요\"}";
byte[] messsage = Encoding.UTF8.GetBytes(jsonString);
ushort length = ((ushort)messsage.Length); //메세지 전체 길이 저장 

//길이 넣을 두 개짜리 버퍼 
byte[] lengthBuffer = new byte[2];
lengthBuffer = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)length));

byte[] buffer = new byte[2 + length]; //앞에 2바이트 + 진짜 문자열의 길이

Buffer.BlockCopy(lengthBuffer, 0, buffer, 0, 2);
Buffer.BlockCopy(messsage, 0, buffer, 2, length);

Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Loopback, 4000);
clientSocket.Connect(clientEndPoint);

int sendLength = clientSocket.Send(buffer, buffer.Length, SocketFlags.None);
//받아올 때도 앞에 두 개 받아와야 함 
//원래 만약 1바이트만 받으면 무조건 2개 받으라고 while문 받으라고 처리해줘야 함
int recvLength = clientSocket.Receive(lengthBuffer, 2, SocketFlags.None);
length = BitConverter.ToUInt16(lengthBuffer, 0); // 길이 정보를 받아와야 함
length = (ushort)IPAddress.NetworkToHostOrder((short)length); 

byte[] receiveBuffer = new byte[4096];
recvLength = clientSocket.Receive(receiveBuffer, length, SocketFlags.None);

string jsonStr = Encoding.UTF8.GetString(receiveBuffer);
Console.WriteLine(jsonStr);

clientSocket.Close();
```

### Byte Order

https://ko.wikipedia.org/wiki/%EC%97%94%EB%94%94%EC%96%B8

= 메모리 읽을 때 어디서부터 읽을지 

`CPU마다 설정이 다르면 → 공유기를 지나갈 때 → 결과값이 바뀔 수 있음`

- C#은 리틀엔디안이 기본
    
    유니티에서 C# 통신만 한다고 하면 문제 X
    
    → 만약 그게 아니면 문제가 발생할 수 있음
    
- 정수 형 숫자일 때 문제
    
    (바이트는 한 글자라 문제 X)
    

☑️ 네트워크 바이트 오더가 하나로 정해져 있으면

Host에서 보낼 때 이를 네트워크 바이트 오더에 맞춰서 보내기

```csharp
ushort length = (ushort)IPAddress.HostToNetworkOrder(messsage.Length);
```

이런 식으로 바꿔줘야 함

---

# +@)

### 네이글 알고리즘

네트워크 상태를 보고 같은 곳을 가면 모아서 한 번에 보내는 알고리즘

게임에서는 반응성이 떨어질 수 있음 

끌 수 있는데 끄면 TCP 성능이 확 떨어짐 

### IOCP (Input/Output Completion Port)

= 윈도우에서 제공하는 비동기 입출력 라이브러리 

무조건 게임 서버는 이걸로 제작함 

https://oliveyoung.tech/2023-10-02/c10-problem/

Docker

## TTL (Time To Live)

https://www.ibm.com/kr-ko/topics/time-to-live

이게 없으면 라우터가 계속 돌게 됨
