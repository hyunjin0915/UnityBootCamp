using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Sample
    {
        public string imgStr;

        public Sample(string imgStr)
        {
            this.imgStr = imgStr;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Loopback, 4000);

            serverSocket.Connect(clientEndPoint);

            byte[] buffer = new byte[1024];
            int recevLength =serverSocket.Receive(buffer);
        

            string jsonString = Encoding.UTF8.GetString(buffer);

            Sample receiveSample = JsonConvert.DeserializeObject<Sample>(jsonString);
            byte[] imageBytes = Convert.FromBase64String(receiveSample.imgStr);

            File.WriteAllBytes("receiveImg.webp", imageBytes);
            Console.WriteLine(receiveSample.imgStr);
        }
    }
}
