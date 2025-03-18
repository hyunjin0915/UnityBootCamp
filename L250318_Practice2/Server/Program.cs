using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        class Sample
        {
            public string imgStr;

            public Sample(string imgStr)
            {
                this.imgStr = imgStr;
            }
        }
        static void Main(string[] args)
        {
            Socket listenSocket = new Socket (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint listenEndPoiint = new IPEndPoint(IPAddress.Any, 4000);

            listenSocket.Bind (listenEndPoiint);
            listenSocket.Listen(10);

            bool isRunning = true;
            while(isRunning)
            {
                Socket clientSocket = listenSocket.Accept ();

                string baseImage = Convert.ToBase64String(File.ReadAllBytes("./1.webp"));
                Console.WriteLine(baseImage);

                byte[] buffer = new byte[1024];

                Sample sample = new Sample(baseImage);
                string jsonData = JsonConvert.SerializeObject(sample);
                byte[] buffer2 = Encoding.UTF8.GetBytes(jsonData);

                int sendLength = clientSocket.Send(buffer2);

                clientSocket.Close();
            }
        }
    }
}
