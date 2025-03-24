using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Message
    {
        public string message;
    }
    
    class Program
    {
        static Socket listenSocket;
        static List<Socket> clientSockets = new List<Socket>();

        static object _lock = new object();

        static void AcceptThread()
        {
            while (true)
            {
                Socket clientSocket = listenSocket.Accept();
                //탐색하는 동안 Add가 되면 안돼서 lock을 걸어줘야 함 -> clientSockets 사용되는 모든 부분에 lock
                //사실 Add 내부에 lock이 기본으로 구현되어 있음 
                lock (_lock)
                {
                    clientSockets.Add(clientSocket);
                }
                Console.WriteLine($"Connect client : {clientSocket.RemoteEndPoint}");

                Thread workThread = new Thread(new ParameterizedThreadStart(WorkThread)); //Accept thread 안에서 실행!! client 정보 넘겨줘야 함!!
                workThread.IsBackground = true; //isbackground 는 해야 되지만 join은 하면 안됨 
                workThread.Start(clientSocket);

            }

        }
        static void WorkThread(Object clientObjectSocket)
        {
            Socket clientSocket = clientObjectSocket as Socket;
            while(true)
            {
                try
                {
                    byte[] headerBuffer = new byte[2];
                    int RecvLength = clientSocket.Receive(headerBuffer, 2, SocketFlags.None);
                    if (RecvLength > 0)
                    {
                        short packetlength = BitConverter.ToInt16(headerBuffer, 0);
                        packetlength = IPAddress.NetworkToHostOrder(packetlength);

                        byte[] dataBuffer = new byte[4096];
                        RecvLength = clientSocket.Receive(dataBuffer, packetlength, SocketFlags.None);
                        string JsonString = Encoding.UTF8.GetString(dataBuffer);
                        Console.WriteLine(JsonString);

                        JObject clientData = JObject.Parse(JsonString);

                        string message = "{ \"message\" : \"" + clientData.Value<String>("message") + "\"}";
                        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                        ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                        headerBuffer = BitConverter.GetBytes(length);

                        byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
                        Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                        Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);
                        lock(_lock)
                        {
                            foreach (Socket sendSocket in clientSockets)
                            {
                                int SendLength = sendSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                            }
                        }
                        
                    }
                    else
                    {
                        string message = "{ \"message\" : \" Disconnect : " + clientSocket.RemoteEndPoint + " \"}";
                        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                        ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                        headerBuffer = BitConverter.GetBytes(length);

                        byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
                        Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                        Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

                        clientSocket.Close();
                        lock (_lock)
                        {
                            clientSockets.Remove(clientSocket);

                            foreach (Socket sendSocket in clientSockets)
                            {
                                int SendLength = sendSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                            }
                        }

                        return;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error 낸 놈 : {e.Message} {clientSocket.RemoteEndPoint}");

                    string message = "{ \"message\" : \" Disconnect : " + clientSocket.RemoteEndPoint + " \"}";
                    byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
                    ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

                    byte[] headerBuffer = new byte[2];

                    headerBuffer = BitConverter.GetBytes(length);

                    byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
                    Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
                    Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

                    clientSocket.Close();
                    lock (_lock)
                    {
                        clientSockets.Remove(clientSocket);

                        foreach (Socket sendSocket in clientSockets)
                        {
                            int SendLength = sendSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
                        }
                    }
                    return;
                }
            }
            
        }


        static void Main(string[] args)
        {
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listenEndPoint = new IPEndPoint(IPAddress.Any, 4000);

            listenSocket.Bind(listenEndPoint);

            listenSocket.Listen(10);

            Thread acceptThread = new Thread(new ThreadStart(AcceptThread));
            acceptThread.IsBackground = true;
            acceptThread.Start();

            acceptThread.Join();
            
            listenSocket.Close();
        }
    }
}