using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static int c = 0;

    static void SendTask(Socket clientSocket)
    {
        while (true)
        {
            string text = "server->client" + c;
            c++;
            byte[] data = Encoding.UTF8.GetBytes(text);
            clientSocket.Send(data);
            Console.WriteLine("Sent: " + text);
            Thread.Sleep(1000);
        }
    }

    static void RecvTask(Socket clientSocket)
    {
        byte[] buffer = new byte[1024];
        while (true)
        {
            int bytesRead = clientSocket.Receive(buffer);
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

        int socketPort = 2001;
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            // サーバーソケットをバインドしてリッスン開始
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, socketPort));
            serverSocket.Listen(1);
            Console.WriteLine("Server started... Waiting for client connection...");

            // クライアント接続を受け入れる
            Socket clientSocket = serverSocket.Accept();
            Console.WriteLine("Client connected...");

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
        finally
        {
            serverSocket.Close();
        }
    }
}