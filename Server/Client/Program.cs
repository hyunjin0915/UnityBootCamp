using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static Socket clientSocket;
        static void ChatInput()
        {
            while (true)
            {
                string InputChat;
                InputChat = Console.ReadLine();

                string jsonString = "{\"message\" : \"" + InputChat + ".\"}";
                byte[] messsage = Encoding.UTF8.GetBytes(jsonString);
                ushort length = ((ushort)messsage.Length); //메세지 전체 길이 저장 

                //길이 넣을 두 개짜리 버퍼 
                byte[] lengthBuffer = new byte[2];
                lengthBuffer = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)length));

                byte[] buffer = new byte[2 + length]; //앞에 2바이트 + 진짜 문자열의 길이

                Buffer.BlockCopy(lengthBuffer, 0, buffer, 0, 2);
                Buffer.BlockCopy(messsage, 0, buffer, 2, length);

                int sendLength = clientSocket.Send(buffer, buffer.Length, SocketFlags.None);
            }
            
        }

        static void ReceiveThread()
        {
            while (true)
            {
                byte[] lengthBuffer = new byte[2];

                int recvLength = clientSocket.Receive(lengthBuffer, 2, SocketFlags.None);
                ushort length = BitConverter.ToUInt16(lengthBuffer, 0); // 길이 정보를 받아와야 함
                length = (ushort)IPAddress.NetworkToHostOrder((ushort)length);

                byte[] receiveBuffer = new byte[4096];
                recvLength = clientSocket.Receive(receiveBuffer, length, SocketFlags.None);

                string jsonStr = Encoding.UTF8.GetString(receiveBuffer);
                Console.WriteLine(jsonStr);

                //Thread.Sleep(100);
            }
        }
        static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Loopback, 4000);

            clientSocket.Connect(clientEndPoint); //접속이 성공하면 

            Thread chatInputThread = new Thread(new ThreadStart(ChatInput));
            Thread recvThread = new Thread(new ThreadStart(ReceiveThread));

            chatInputThread.IsBackground = true;
            recvThread.IsBackground = true;

            chatInputThread.Start();
            recvThread.Start();

            chatInputThread.Join();


            clientSocket.Close();

        }
    }
}
