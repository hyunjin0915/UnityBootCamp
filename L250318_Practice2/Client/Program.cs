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

            FileStream fsOutputt = new FileStream("1_copy.webp", FileMode.CreateNew);

            byte[] buffer = new byte[1024];
            int ReceiveSize = 0;
            do
            {
                ReceiveSize = serverSocket.Receive(buffer);
                fsOutputt.Write(buffer, 0, ReceiveSize);
            } while (ReceiveSize > 0);

            fsOutputt.Close();
            serverSocket.Close();
            /*int recevLength =serverSocket.Receive(buffer);
        

            string jsonString = Encoding.UTF8.GetString(buffer);

            Sample receiveSample = JsonConvert.DeserializeObject<Sample>(jsonString);
            byte[] imageBytes = Convert.FromBase64String(receiveSample.imgStr);

            File.WriteAllBytes("receiveImg.webp", imageBytes);
            Console.WriteLine(receiveSample.imgStr);*/
        }
    }
}
