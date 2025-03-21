﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            length = (ushort)IPAddress.NetworkToHostOrder((ushort)length); 

            byte[] receiveBuffer = new byte[4096];
            recvLength = clientSocket.Receive(receiveBuffer, length, SocketFlags.None);

            string jsonStr = Encoding.UTF8.GetString(receiveBuffer);
            Console.WriteLine(jsonStr);

            clientSocket.Close();

        }
    }
}
