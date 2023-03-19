using System.Collections;
using System.Collections.Generic;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Numerics;

public class ServerMidterm
{
    private static float[] pos;
    private static byte[] byteArray;
    private static byte[] buff = new byte[512];
    static int int1, int2, int3, int4, int5, int6;
    private static Socket server;
    private static Socket client, client2;
    private static EndPoint remoteClient, remoteClient2;
    private static float Xvalue, yvalue, zvalue, Xvalue2, yvalue2, zvalue2;

    //private string fo = "";
    //private static float[] ExampleArray;
    public static void StartServer()
    {
       // byte[] buff = new byte[512];
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        Console.WriteLine("Server name: {0}, ip");
        IPEndPoint localEP = new IPEndPoint(ip, 8889);
        IPEndPoint localEP2 = new IPEndPoint(ip, 8888);
        //IPEndPoint ChatEP = new IPEndPoint(ip, 8888);
        server = new Socket(ip.AddressFamily,SocketType.Dgram, ProtocolType.Udp);

        remoteClient = new IPEndPoint(IPAddress.Any, 8889);
        remoteClient2 = new IPEndPoint(IPAddress.Any, 8889);

        try
        {
            server.Bind(localEP);

            Console.WriteLine("Waiting for data....");
            while (true)
            {
                Console.WriteLine(Xvalue);
                int1 = server.ReceiveFrom(buff, ref remoteClient);
                float.TryParse(Encoding.ASCII.GetString(buff, 0, int1), out Xvalue);
                //server.Send(buff);

                int2 = server.ReceiveFrom(buff, ref remoteClient);
                float.TryParse(Encoding.ASCII.GetString(buff, 0, int2), out yvalue);
                //server.Send(buff);

                int3 = server.ReceiveFrom(buff, ref remoteClient);
                float.TryParse(Encoding.ASCII.GetString(buff, 0, int3), out zvalue);

                Console.WriteLine(Xvalue2);
                int4 = server.ReceiveFrom(buff, ref remoteClient2);
                float.TryParse(Encoding.ASCII.GetString(buff, 0, int4), out Xvalue2);

                int5 = server.ReceiveFrom(buff, ref remoteClient2);
                float.TryParse(Encoding.ASCII.GetString(buff, 0, int5), out yvalue2);

                int6 = server.ReceiveFrom(buff, ref remoteClient2);
                float.TryParse(Encoding.ASCII.GetString(buff, 0, int6), out zvalue2);
            }
            //shutdown
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void StartChat()
    {
        String userText;
        String fo = "hello";
        String fo2 = "there";
        byte[] buffer = new byte[512];
        byte[] buffer2 = new byte[512];
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        IPEndPoint ChatEP = new IPEndPoint(ip, 52030);
        Socket ServerChat = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            ServerChat.Bind(ChatEP);
            ServerChat.Listen(1);
            Console.WriteLine("Waiting for Chat Connections...");

            // socketHandler
            client = ServerChat.Accept();
            Console.WriteLine("Client Connected to Client 1");

            client2 = ServerChat.Accept();
            Console.WriteLine("Connected to Client 2");

            Console.WriteLine("Client: {0}  Port: {1}", ChatEP.Address, ChatEP.Port);
            //byte[] msg = Encoding.ASCII.GetBytes("it WORKS YASSSSSSSSSSSSSSSS");
            // Sending data to connected client
            //client.Send(msg);
            while (true)
            {
                buffer = new byte[512];
                buffer2 = new byte[512];
                userText = "<br>";
                //userText += " -0";
                byte[] userMSG = Encoding.ASCII.GetBytes(userText);
                client.Send(userMSG);
                client2.Send(userMSG);
                client.Receive(buffer);
                client2.Receive(buffer2);

                string clMSG = Encoding.ASCII.GetString(buffer);
                //Console.WriteLine(clMSG);
                string clMSG2 = Encoding.ASCII.GetString(buffer2);
                //Console.WriteLine(clMSG2);
                byte[] receivedMSG = Encoding.ASCII.GetBytes(clMSG);
                byte[] receivedMSG2 = Encoding.ASCII.GetBytes(clMSG2);

                client.Send(receivedMSG2);
                Console.WriteLine(clMSG);

                client2.Send(receivedMSG);
                Console.WriteLine(clMSG2);

                //Console.WriteLine("From Server: {0}", Encoding.ASCII.GetString(buffer, 0, client.Receive(buffer)));

                if (clMSG == "exit")
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


            client.Shutdown(SocketShutdown.Both);
            client.Close();
            client2.Shutdown(SocketShutdown.Both);
            client2.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    void Update()
    {
        //Console.WriteLine("testestetestt");
        //int1 = server.ReceiveFrom(buff, ref remoteClient);
        //float.TryParse(Encoding.ASCII.GetString(buff, 0, int1), out Xvalue);

        //int2 = server.ReceiveFrom(buff, ref remoteClient);
        //float.TryParse(Encoding.ASCII.GetString(buff, 0, int2), out yvalue);

        //int3 = server.ReceiveFrom(buff, ref remoteClient);
        //float.TryParse(Encoding.ASCII.GetString(buff, 0, int3), out zvalue);

        //Vector3 position = CubeObject.transform.position;

        //position.x = Xvalue;
        //position.y = yvalue;
        //position.z = zvalue;

        //CubeObject.transform.position = position;
    }

    void Start()
    {
        //serverThread = new Thread(StartServer);
      // serverThread.Start();
    }

    public static int Main(String[] args)
    {
        StartServer();
        StartChat();
        return 0;
    }
    }