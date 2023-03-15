﻿using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class ServerMidterm
{
    private static float[] pos;
    private static byte[] byteArray;
    //private static float[] ExampleArray;
    public static void StartServer()
    {
        byte[] buffer = new byte[512];
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        Console.WriteLine("Server name: {0}, ip");
        IPEndPoint localEP = new IPEndPoint(ip, 8889);
        //IPEndPoint ChatEP = new IPEndPoint(ip, 8888);
        Socket server = new Socket(ip.AddressFamily,
            SocketType.Dgram, ProtocolType.Udp);
        Socket server2 = new Socket(ip.AddressFamily,
            SocketType.Dgram, ProtocolType.Udp);
        Socket ServerChat = new Socket(ip.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        EndPoint remoteClient = new IPEndPoint(IPAddress.Any, 0);

        try
        {
            server.Bind(localEP);

            Console.WriteLine("Waiting for data....");
            while (true)
            {
                Console.WriteLine("Being tested");
                int recv = server.ReceiveFrom(buffer, ref remoteClient);

                pos = new float[recv / 4];
                Buffer.BlockCopy(buffer, 0, pos, 0, recv);

                // server.SendTo()
                Console.WriteLine("X: " + pos[0] + " Y: " + pos[2] + " Z: " + pos[1]);

                //server.Send();
            }
            //server shutdown
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void StartChat()
    {
        String userText;
        byte[] buffer = new byte[512];
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        IPEndPoint ChatEP = new IPEndPoint(ip, 8888);
        Socket ServerChat = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            ServerChat.Bind(ChatEP);
            ServerChat.Listen(1);
            Console.WriteLine("Waiting for Chat Connections...");

            // socketHandler
            Socket client = ServerChat.Accept();
            Console.WriteLine("Client Connected to chat");
            IPEndPoint CEP = (IPEndPoint)client.RemoteEndPoint;

            Console.WriteLine("Client: {0}  Port: {1}", CEP.Address, CEP.Port);
            byte[] msg = Encoding.ASCII.GetBytes("it WORKS YASSSSSSSSSSSSSSSS");
            // Sending data to connected client
            client.Send(msg);
            while (true)
            {
                buffer = new byte[512];
                userText = "test, test, plz work";
                userText += " -From Server";
                byte[] userMSG = Encoding.ASCII.GetBytes(userText);
                client.Send(userMSG);
                client.Receive(buffer);
                String clMSG = Encoding.ASCII.GetString(buffer);
                Console.WriteLine(clMSG);
                //Console.WriteLine("From Server: {0}", Encoding.ASCII.GetString(buffer, 0, client.Receive(buffer)));

                if (userText == "exit")
                {
                    break;
                }
            }
            // Loop
            // User types a msg. Get input as a string
            // Convert to bytes
            // send to client
            // print msg from client
            // end loop


            //client.Shutdown(SocketShutdown.Both);
            //client.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        //StartServer();
        StartChat();
        return 0;
    }
}