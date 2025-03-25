using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        static void SendPacket(Socket ToSocket, string message) //따로 전송 함수로 빼줌
        {
            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
            ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

            byte[] headerBuffer = BitConverter.GetBytes(length);

            byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
            Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
            Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

            int SendLength = ToSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
        }
        static void RecvPacket(Socket toSocket, out string jsonString)
        {
            byte[] lengthBuffer = new byte[2];

            int RecvLength = clientSocket.Receive(lengthBuffer, 2, SocketFlags.None);
            ushort length = BitConverter.ToUInt16(lengthBuffer, 0);
            length = (ushort)IPAddress.NetworkToHostOrder((short)length);
            byte[] recvBuffer = new byte[4096];
            RecvLength = clientSocket.Receive(recvBuffer, length, SocketFlags.None);

            jsonString = Encoding.UTF8.GetString(recvBuffer);
        }

        static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Loopback, 4000);

            clientSocket.Connect(clientEndPoint); //접속이 성공하면 

            /*JObject result = new JObject();
            result.Add("code", "signin");
            result.Add("id", "apple");
            result.Add("password", "1234");
            result.Add("name", "mumu");
            result.Add("email", "star@gmail.com");
            SendPacket(clientSocket,result.ToString());*/


            string JsonString;
            RecvPacket(clientSocket, out JsonString);

            Console.WriteLine(JsonString);

            clientSocket.Close();

        }
    }
}
