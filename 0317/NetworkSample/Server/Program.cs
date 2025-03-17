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

            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.22"), 4000);
            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);
            listenSocket.Bind(listenEndPoint);

            listenSocket.Listen(10); //몇 개를 듣고 있을 건지 

            bool isRunning = true;
            while(isRunning)
            {
                Socket clientSocket = listenSocket.Accept(); //동기, 블록킹 

                byte[] buffer = new byte[1024]; //임시 기억 장치로 
                int RecvLength = clientSocket.Receive(buffer); //얼마나 줄지는 모르겠지만 일단 받음
                if (RecvLength == 0)
                {
                    //close
                    isRunning = false;
                }
                else if(RecvLength < 0)
                {
                    // error
                    isRunning= false;
                }

                int SendLength = clientSocket.Send(buffer);
                if (SendLength == 0)
                {
                    //받는 편에서 끊어버린
                    isRunning = false ;
                }
                else if(SendLength < 0)
                {
                    //통신이 안되는 것 
                }

                string str = Encoding.UTF8.GetString(buffer);
                string[] split_data = str.Split('+');
                int result = int.Parse( split_data[0]) + int.Parse( split_data[1]);
                Console.WriteLine(result);

                buffer = Encoding.UTF8.GetBytes(result.ToString());
                int send = clientSocket.Send(buffer);

                //keep alive time 
                clientSocket.Close();
            }

            listenSocket.Close();
            
        }
    }
}
