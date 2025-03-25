using MySqlConnector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

                        string connectionString = "server=localhost;user=root;database=membership;password=hj0428!!";
                        MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

                        JObject clientData = JObject.Parse(JsonString);
                        string code = clientData.Value<string>("code");
                        try
                        {
                            if (code.CompareTo("login") == 0)
                            {
                                string userId = clientData.Value<string>("id");
                                string userPassword = clientData.Value<string>("password");

                                mySqlConnection.Open();
                                MySqlCommand mySqlCommand = new MySqlCommand();
                                mySqlCommand.Connection = mySqlConnection;

                                mySqlCommand.CommandText = "select * from users where user_id = @user_id and user_password = @user_password";
                                mySqlCommand.Prepare();
                                mySqlCommand.Parameters.AddWithValue("@user_id", userId);
                                mySqlCommand.Parameters.AddWithValue("@user_password", userPassword);

                                MySqlDataReader dataReader = mySqlCommand.ExecuteReader();

                                if (dataReader.Read()) //로그인 성공
                                {
                                    JObject result = new JObject();
                                    result.Add("code", "loginresult");
                                    result.Add("message", "success");
                                    result.Add("name", dataReader["user_name"].ToString());
                                    result.Add("email", dataReader["user_email"].ToString());
                                    SendPacket(clientSocket, result.ToString());
                                }
                                else //실패
                                {
                                    JObject result = new JObject();
                                    result.Add("code", "loginresult");
                                    result.Add("message", "fail");
                                    SendPacket(clientSocket, result.ToString());
                                }
                            }
                            else if (code.CompareTo("signin") == 0)
                            {
                                string userId = clientData.Value<string>("id");
                                string userPassword = clientData.Value<string>("password");
                                string name = clientData.Value<string>("name");
                                string email = clientData.Value<string>("email");

                                mySqlConnection.Open();
                                MySqlCommand mySqlCommand2 = new MySqlCommand();
                                mySqlCommand2.Connection = mySqlConnection;

                                mySqlCommand2.CommandText = "insert into users (user_id, user_password, user_name, user_email) values ( @user_id, @user_password, @user_name, @user_email)";
                                mySqlCommand2.Prepare();
                                mySqlCommand2.Parameters.AddWithValue("@user_id", userId);
                                mySqlCommand2.Parameters.AddWithValue("@user_password", userPassword);
                                mySqlCommand2.Parameters.AddWithValue("@user_name", name);
                                mySqlCommand2.Parameters.AddWithValue("@user_email", email);
                                mySqlCommand2.ExecuteNonQuery();

                                //가입 성공
                                JObject result = new JObject();
                                result.Add("code", "signinresult");
                                result.Add("message", "success");
                                SendPacket(clientSocket, result.ToString());
                            }
                            else if (code.CompareTo("Chat") == 0)
                            {
                                string userId = clientData.Value<string>("id");
                                string chatMessage = clientData.Value<string>("chatMessage");

                                //채팅 성공
                                JObject result = new JObject();
                                result.Add("code", "ChatResult");
                                result.Add("id", userId);
                               
                                result.Add("chatMessage", chatMessage);
                                foreach (Socket sendSocket in clientSockets)
                                {
                                    SendPacket(sendSocket, result.ToString());
                                }
                                
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        finally
                        {
                            mySqlConnection.Close();
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

        static void SendPacket(Socket ToSocket, string message) //따로 전송 함수로 빼줌
        {
            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
            ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

            byte[] headerBuffer = BitConverter.GetBytes(length);

            headerBuffer = BitConverter.GetBytes(length);

            byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
            Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
            Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

            int SendLength = ToSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
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