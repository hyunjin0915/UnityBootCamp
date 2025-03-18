using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace L250318_UDPClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 6000);

                        byte[] buffer = new byte[1024];
                        string message = "안녕하세요";
                        buffer = Encoding.UTF8.GetBytes(message);
                        int SendLength = serverSocket.SendTo(buffer,buffer.Length, SocketFlags.None, serverEndPoint); //바로 그냥 던져버리면 됨

                        byte[] buffer2 = new byte[1024];
                        EndPoint remoteEndPoint = serverEndPoint;
                        int RecvLength = serverSocket.ReceiveFrom(buffer2, ref remoteEndPoint);

                        string message2 = Encoding.UTF8.GetString(buffer2);
                        Console.WriteLine(message2);
                        serverSocket.Close();
            */

            IPHostEntry host = Dns.GetHostEntry("naver.com");
            foreach(IPAddress address in host.AddressList)
            {
                Console.WriteLine(address);
            }
        }
    }
}
