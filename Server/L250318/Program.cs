using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace L250318
{
    internal class Program
    {

        static void Main(string[] args)
        {
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
            
        }
    }
}
