using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace L250318
{
    internal class Program
    {
        class Data
        {
            public string message;

            public Data(string message)
            {
                this.message = message;
            }
        }

        static void Main(string[] args)
        {
            Data data = new Data("반가워요");

            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

            listenSocket.Bind(listenEndPoint);
            listenSocket.Listen(10);

            bool isRunning = true;
            while(isRunning)
            {
                Socket clientSocket = listenSocket.Accept();

                byte[] buffer = new byte[1024];
                int RecvLength = clientSocket.Receive(buffer);

                if(RecvLength <= 0)
                {
                    isRunning = false;
                }

                String str = Encoding.UTF8.GetString(buffer);
                Data receiveData = JsonConvert.DeserializeObject<Data>(str);
                if(receiveData.message.Equals("안녕하세요"))
                {
                    string jsonData = JsonConvert.SerializeObject(data);

                    byte[] buffer2 = new byte[1024];
                    buffer2 = Encoding.UTF8.GetBytes(jsonData);
                    clientSocket.Send(buffer2, 0, buffer2.Length, SocketFlags.None);
                }
                Console.WriteLine(str);

                //string message = "반가워요";

                clientSocket.Close();
            }
            listenSocket.Close();
        }
    }
}
