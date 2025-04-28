using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static int count = 0;

    static void SendTask(Socket socket)
    {
        while (true)
        {
            string text = "client->server" + count;
            count++;
            byte[] data = Encoding.UTF8.GetBytes(text);
            socket.Send(data);
            Console.WriteLine("Sent: " + text);
            Thread.Sleep(1000);
        }
    }

    static void RecvTask(Socket socket)
    {
        byte[] buffer = new byte[1024];
        while (true)
        {
            int bytesRead = socket.Receive(buffer);
            if (bytesRead > 0)
            {
                string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received: " + text);
            }
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Communicate with TCP/IP using Socket!");

        string serverIp = "127.0.0.1"; // サーバーのIPアドレス
        int serverPort = 2001;         // サーバーのポート番号

        try
        {
            // ソケットを作成
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // サーバーに接続
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse(serverIp), serverPort));
            Console.WriteLine("Connected to server...");

            // 送信・受信タスクを開始
            Thread threadSend = new Thread(() => SendTask(clientSocket));
            Thread threadRecv = new Thread(() => RecvTask(clientSocket));

            threadSend.Start();
            threadRecv.Start();

            threadSend.Join();
            threadRecv.Join();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}