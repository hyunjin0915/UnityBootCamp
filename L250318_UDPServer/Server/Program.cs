using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
/*            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 6000);

            serverSocket.Bind(serverEndPoint);

            //택배를 보고 어디서 보낸건지 알아야 함
            byte[] buffer = new byte[1024]; //상대방이 1024보다 큰 거 보내면 못 받음 -> UDP는 쪼개서 못 받으니까!!, 못 받으면 날라가고 없음
            EndPoint clientEndPoint = (EndPoint)serverEndPoint; //??

            //이것도 blocking 함수! 
            int RecvLength = serverSocket.ReceiveFrom(buffer, ref clientEndPoint);

            int SendLength = serverSocket.SendTo(buffer, clientEndPoint);

            serverSocket.Close();
*/        }
    }
}
