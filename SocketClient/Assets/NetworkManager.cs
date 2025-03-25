using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ChatPacket
{
    public string code;
    public string id;
    public string chatMessage;
}



[Serializable]
public class LogInPacket
{
    public string code;
    public string id;
    public string password;
}

[Serializable]
public class SignInPacket
{
    public string code;
    public string id;
    public string password;
    public string name;
    public string email;
}

public class PostBox
{
    private Queue<string> messageQueue;

    public PostBox()
    {
        this.messageQueue = new Queue<string>();
    }
}

public class NetworkManager : MonoBehaviour
{
    private Socket serverSocket;
    private IPEndPoint serverEndPoint;
    private Thread recvThread;

    public TMP_InputField idUI;
    public TMP_InputField passwordUI;

    public TMP_InputField NewidUI;
    public TMP_InputField NewpasswordUI; 
    public TMP_InputField NewNameUI;
    public TMP_InputField NewEmailUI;

    LogInPacket loginPacket;

    public TMP_InputField chatMessageUI;
    public TMP_Text chatBlockUI;
    private string chatTexts = "";

    Action update;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConnectedToServer();
    }
    void RecvPacket()
    {
        while (true)
        {
            byte[] lengthBuffer = new byte[2];

            int RecvLength = serverSocket.Receive(lengthBuffer, 2, SocketFlags.None);
            ushort length = BitConverter.ToUInt16(lengthBuffer, 0);
            length = (ushort)IPAddress.NetworkToHostOrder((short)length);
            byte[] recvBuffer = new byte[4096];
            RecvLength = serverSocket.Receive(recvBuffer, length, SocketFlags.None);

           string jsonString = Encoding.UTF8.GetString(recvBuffer);
            //여기서 json 분해해서  파싱하기 
            ChatPacket recvChat = JsonUtility.FromJson<ChatPacket>(jsonString);
            chatTexts += recvChat.chatMessage;
            update.Invoke();
            //chatBlockUI.text = chatTexts;
            Debug.Log(jsonString);
            Thread.Sleep(10);
        }
    }

    void ConnectedToServer()
    {
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverEndPoint = new IPEndPoint(IPAddress.Loopback, 4000);
        serverSocket.Connect(serverEndPoint);

        recvThread = new Thread(new ThreadStart(RecvPacket));
        recvThread.Start();
    }

    void SendPacket(string message)
    {
        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
        ushort length = (ushort)IPAddress.HostToNetworkOrder((short)messageBuffer.Length);

        byte[] headerBuffer = BitConverter.GetBytes(length);

        byte[] packetBuffer = new byte[headerBuffer.Length + messageBuffer.Length];
        Buffer.BlockCopy(headerBuffer, 0, packetBuffer, 0, headerBuffer.Length);
        Buffer.BlockCopy(messageBuffer, 0, packetBuffer, headerBuffer.Length, messageBuffer.Length);

        int SendLength = serverSocket.Send(packetBuffer, packetBuffer.Length, SocketFlags.None);
    }

    public void OnLogin()
    {
        loginPacket = new LogInPacket();
        loginPacket.code = "Login";
        loginPacket.id = idUI.text;
        loginPacket.password = passwordUI.text;

        SendPacket(JsonUtility.ToJson(loginPacket));
    }

    public void OnSignIn()
    {
        SignInPacket packet = new SignInPacket();
        packet.code = "SignIn";
        packet.id = NewidUI.text;
        packet.password = NewpasswordUI.text;
        packet.name = NewNameUI.text;
        packet.email = NewEmailUI.text;

        SendPacket(JsonUtility.ToJson(packet));
    }

    public void OnSendChat()
    {
        ChatPacket packet = new ChatPacket();
        packet.code = "Chat";
        packet.id = loginPacket.id;
        packet.chatMessage = chatMessageUI.text;

        SendPacket(JsonUtility.ToJson(packet));
    }

    private IEnumerator CheckQueue()
    {
        WaitForSeconds waitSec = new WaitForSeconds(1);
        while (true)
        {
            
        }
    }

    public void OnApplicationQuit()
    {
        if(recvThread != null)
        {
            recvThread.Abort();
        }
        if (serverSocket != null)
        {
            serverSocket.Shutdown(SocketShutdown.Both); //저나 끊을게요 하고 던져주고
            serverSocket.Close(); //끊기
        }
    }
}
