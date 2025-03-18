using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Data
    {
        public string message;

        public Data(string message)
        {
            this.message = message;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data("안녕하세요");

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Loopback, 4000);
            serverSocket.Connect(clientEndPoint);

            byte[] buffer = new byte[1024];

            //String message = "안녕하세요";

            String jsonData = JsonConvert.SerializeObject(data);

            buffer = Encoding.UTF8.GetBytes(jsonData);

            int sendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            while (sendLength < buffer.Length)
            {
                int SendLength = serverSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            }

            byte[] buffer2 = new byte[1024];

            int recvLength = serverSocket.Receive(buffer2);
            
            String str = Encoding.UTF8.GetString(buffer2);
            Console.WriteLine(str);

            serverSocket.Close();
        }
    }
}
