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
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //내 자신에게 보내는 것 - 테스트 용(= "127.0.0.1" 이랑 같은 뜻)
            //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.22"), 4000);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 4000);
            serverSocket.Connect(serverEndPoint);

            byte[] buffer;

            String message = "100+200";
            buffer = Encoding.UTF8.GetBytes(message);
            int sendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            while(sendLength < buffer.Length)
            {
                int SendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            }

            byte[] buffer2 = new byte[1024];
            int RecvLength = serverSocket.Receive(buffer2);

            Console.WriteLine(Encoding.UTF8.GetString(buffer2));

            serverSocket.Close();
        }
    }
}
